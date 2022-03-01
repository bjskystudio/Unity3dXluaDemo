using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

namespace YoukiaEngine
{
    /// <summary>
    /// ImpostorSnapshot图集管理
    /// </summary>
    public class ImpostorSnapshotAtlas : MonoBehaviour
    {
        private static ImpostorSnapshotAtlas _instance;

        public static ImpostorSnapshotAtlas Instance
        {
            get
            {
                if (!_instance)
                {
                    Init();
                }

                return _instance;
            }
        }

        public static int _RT_SIZE = 128;
        public static int _RT_ATLAS_SIZE = 512;

        public class SnapshotAtlas
        {
            public RenderTexture Target;
            public int blockX;
            public int blockY;
            public int blockNum;
            public int objCount;
            public bool[] objRef;

            public SnapshotAtlas(int nX, int nY)
            {
                blockX = nX;
                blockY = nY;
                blockNum = nX * nY;
                objCount = 0;

                RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
                if (!SystemInfo.SupportsRenderTextureFormat(rtFormat))
                {
                    rtFormat = RenderTextureFormat.ARGB32;
                }

                Target = new RenderTexture(_RT_ATLAS_SIZE, _RT_ATLAS_SIZE, 0, rtFormat, RenderTextureReadWrite.Linear);
                Target.name = "SnapshotAtlas";
                Target.useMipMap = false;
                Target.filterMode = FilterMode.Bilinear;

                objRef = new bool[blockNum];
                for (int i = 0; i < blockNum; i++)
                {
                    objRef[i] = false;
                }
            }
        }

        private List<SnapshotAtlas> _listAtlas = new List<SnapshotAtlas>();
       
        public class Snapshot
        {
            public RenderTexture Target;
            public SnapshotAtlas atlas;
            public int index;
            public float u;
            public float v;
            public float size;

            // RT写到图集
            public void Blit(Material material)
            {
                if (Target == null || atlas == null)
                    return;

                Graphics.SetRenderTarget(atlas.Target);

                //Rect ViewPort = new Rect();
                //ViewPort.xMin = u;
                //ViewPort.yMin = v;
                //ViewPort.xMax = u + size;
                //ViewPort.yMax = v + size;
                //camera.pixelRect = new Rect(ViewPort);
                ////GL.Viewport(ViewPort);
                //GL.Clear(true, true, new Color(0, 0, 0, 0));
                // 这里设置Viewport后仍然Clear的全部，利用material拷贝实现只修改一块

                Rect rect = new Rect();
                rect.xMin = u * 2 - 1.0f;
                rect.yMin = v * 2 - 0.5f;
                rect.xMax = rect.xMin + size * 2;
                rect.yMax = rect.yMin - size * 2;
                Graphics.DrawTexture(rect, Target, material);
            }
        }
        private ConcurrentQueue<Snapshot> _tasksQueue = new ConcurrentQueue<Snapshot>(); // 替换List避免编辑器嵌套调用时错误

        private static GameObject _rootObject;
        private Camera _camera;
        private Material _material;

        private static void Init()
        {
            _rootObject = new GameObject("ImpostorSnapshotAtlas");
            _rootObject.transform.position = new Vector3(0, 0, -10);
            DontDestroyOnLoad(_rootObject);
            _instance = _rootObject.AddComponent<ImpostorSnapshotAtlas>();
            _instance._camera = _rootObject.AddComponent<Camera>();
            _instance._camera.allowMSAA = false;
            _instance._camera.useOcclusionCulling = false;
            _instance._camera.orthographic = true;
            _instance._camera.orthographicSize = 1f;
            _instance._camera.cullingMask = LayerMask.GetMask("Nothing");
            _instance._camera.clearFlags = CameraClearFlags.Nothing;
            _instance._camera.backgroundColor = Color.black;
            _instance._camera.depth = 99999 + 1; // 截屏摄像机需要后渲染，否则切换环境会导致第一帧数据错误
            _instance._camera.targetTexture = new RenderTexture(1, 1, 0);

            //Shader shd = EngineIO.Instance.ShaderFind("YoukiaEngine/Impostor/CopyTarget");
            //_instance._material = new Material(shd);
        }

        public Snapshot GetSnapshot()
        {
            RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;
            if (!SystemInfo.SupportsRenderTextureFormat(rtFormat))
            {
                rtFormat = RenderTextureFormat.ARGB32;
            }

            Snapshot s = new Snapshot();
            s.Target = new RenderTexture(_RT_SIZE, _RT_SIZE, 24, rtFormat, RenderTextureReadWrite.Linear);
            s.Target.name = "Snapshot";
            s.Target.useMipMap = true;
            s.Target.filterMode = FilterMode.Bilinear;

            SnapshotAtlas sa = GetAtlas();
            s.atlas = sa;
            s.index = 0;
            for (int i = 0; i < sa.blockNum; i++)
            {
                if (!sa.objRef[i])
                {
                    sa.objRef[i] = true;
                    s.index = i;
                    break;
                }
            }

            s.u = ((float) (s.index / sa.blockX)) / sa.blockX;
            s.v = ((float) (s.index % sa.blockX)) / sa.blockY;
            s.size = 1.0f / sa.blockX;

            sa.objCount++;
            return s;
        }

        public void Release(Snapshot s)
        {
            if (s.Target)
            {
                UnityEngine.Object.Destroy(s.Target);
                s.Target = null;
            }

            SnapshotAtlas sa = s.atlas;
            if (sa != null)
            {
                if (sa.objRef[s.index])
                {
                    sa.objRef[s.index] = false;
                    sa.objCount -= 1;
                    if (sa.objCount == 0)
                    {// 清空图集，调试的时候才需要
                        Graphics.SetRenderTarget(sa.Target);
                        GL.Clear(true, true, new Color(0, 0, 0, 0));
                    }
                }
            }

            s.atlas = null;
        }

        public void Clear()
        {
            SnapshotAtlas sa;
            for (int i = 0; i < _listAtlas.Count; i++)
            {
                sa = _listAtlas[i];
                UnityEngine.Object.Destroy(sa.Target);
            }

            _listAtlas.Clear();
        }

        private SnapshotAtlas GetAtlas()
        {
            SnapshotAtlas sa;
            for (int i = 0; i < _listAtlas.Count; i++)
            {
                sa = _listAtlas[i];
                if (sa.objCount < sa.blockNum)
                {
                    return sa;
                }
            }

            sa = new SnapshotAtlas(4, 4);
            _listAtlas.Add(sa);
            return sa;
        }

        public void Blit(Snapshot s)
        {
            _tasksQueue.Enqueue(s);
            if (!_camera.enabled)
            {
                _camera.enabled = true;
            }
        }

        private void OnPostRender()
        {
            if (_tasksQueue.Count > 0)
            {
                Queue<Snapshot> queue = _tasksQueue.Switch();
                while (queue.Count > 0)
                {
                    Snapshot task = queue.Dequeue();
                    task.Blit(_material);
                }
            }
            _camera.enabled = false;
        }

        public void OnDestroy()
        {
            Clear();
        }
    }
}
