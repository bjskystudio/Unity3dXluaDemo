using System.Collections.Generic;
using UnityEngine;

namespace YoukiaEngine
{
    public class ImpostorSnapshotUtils : MonoBehaviour
    {
        private struct SnapshotTask
        {
            public ImpostorSnapshotAtlas.Snapshot Atlas;
            public RenderTexture Target;
            public Material Material;
            public Mesh Mesh;

            public SnapshotTask(ImpostorSnapshotAtlas.Snapshot obj, Mesh mesh, Material mat)
            {
                Atlas = obj;
                Target = obj.Target;
                Material = mat;
                Mesh = mesh;
            }
        }

        private static ImpostorSnapshotUtils _instance;
        private static GameObject _rootObject;
        private Camera _camera;
        private Vector3 _verticeScale;

        private ConcurrentQueue<SnapshotTask> _tasksQueue = new ConcurrentQueue<SnapshotTask>(); // 替换List避免编辑器嵌套调用时错误

        public static ImpostorSnapshotUtils Instance
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

        private static void Init()
        {
            _rootObject = new GameObject("ImpostorSnapshotUtils");
            _rootObject.transform.position = new Vector3(0, 0, -10);
            DontDestroyOnLoad(_rootObject);
            _instance = _rootObject.AddComponent<ImpostorSnapshotUtils>();
            _instance._camera = _rootObject.AddComponent<Camera>();
            _instance._camera.allowMSAA = false;
            _instance._camera.useOcclusionCulling = false;
            _instance._camera.orthographic = true;
            _instance._camera.orthographicSize = 1f;
            _instance._camera.cullingMask = LayerMask.GetMask("Nothing");
            _instance._camera.clearFlags = CameraClearFlags.Color;
            _instance._camera.backgroundColor = Color.black;
            _instance._camera.depth = 99999;// 截屏摄像机需要后渲染，否则切换环境会导致第一帧数据错误
            _instance._camera.targetTexture = new RenderTexture(512, 512, 0);
        }

        public void Process(ImpostorSnapshotAtlas.Snapshot target, Mesh mesh, Material mat)
        {
            SnapshotTask task = new SnapshotTask(target, mesh, mat);
            _tasksQueue.Enqueue(task);
            if (!_camera.enabled)
            {
                _camera.enabled = true;
            }
        }

        Vector3 _lastEulerAngles = Vector3.zero;
        private void Update()
        {
            // 判断摄像机角度改变，主动刷新一次
            if(GeometryInstancingManager.Instance.RenderCamera)
            {
                Vector3 eulerAngles = GeometryInstancingManager.Instance.RenderCamera.transform.eulerAngles;
                if (Vector3.Distance(_lastEulerAngles, eulerAngles) > 5)
                {
                    _lastEulerAngles = eulerAngles;
                    GeometryInstancingManager.Instance.ForceUpdateAllGeometryInstancingGroup();
                }
            }
        }
        
        private void OnPostRender()
        {
            if (_tasksQueue.Count > 0)
            {
                Queue<SnapshotTask> queue = _tasksQueue.Switch();
                if(GeometryInstancingManager.Instance.RenderCamera != null)
                {
                    _camera.transform.rotation = GeometryInstancingManager.Instance.RenderCamera.transform.rotation;
                    _camera.transform.position = -GeometryInstancingManager.Instance.RenderCamera.transform.forward * 15;

                    //Vector3 eulerAngles = GeometryInstancingManager.Instance.RenderCamera.transform.eulerAngles;
                    //eulerAngles.x = 0;
                    //_camera.transform.eulerAngles = eulerAngles;
                    //_camera.transform.position = -_camera.transform.transform.forward * 15;

                    while (queue.Count > 0)
                    {
                        SnapshotTask task = queue.Dequeue();
                        if (task.Mesh != null)
                        {
                            Bounds aabb = task.Mesh.bounds;
                            float scale = 1 / Mathf.Max(Mathf.Max(aabb.extents.x, aabb.extents.y), aabb.extents.z);
                            //float scale = 1 / Mathf.Max(aabb.extents.x, aabb.extents.y);
                            _verticeScale = new Vector3(scale, scale, scale);

                            Matrix4x4 trnasform = Matrix4x4.Scale(_verticeScale) * Matrix4x4.Translate(-aabb.center);
                   
                            Graphics.SetRenderTarget(task.Target);
                            GL.Clear(true, true, new Color(0, 0, 0, 0));
                            task.Material.SetPass(0);
                            Graphics.DrawMeshNow(task.Mesh, trnasform);

                            if (task.Atlas != null)
                            {
                                ImpostorSnapshotAtlas.Instance.Blit(task.Atlas);
                            }
                        }
                    }
                }
            }
            _camera.enabled = false;
        }

        public void OnDestroy()
        {
            Destroy(_camera.targetTexture);
            _instance = null;
        }
    }
}