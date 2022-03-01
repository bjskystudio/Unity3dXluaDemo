using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using YoukiaEngine;
using Random = UnityEngine.Random;

namespace YoukiaEngine
{
    public class CPUParticleRenderer : MonoBehaviour
    {
        public Material _material;
        public int Count = 1;
        public Vector3 EmitRange = new Vector3(5, 1, 5);
        public float Duration = 1;
        public float Speed = 5;
        public float SpeedRandomRange = 0;
        public Vector3 Direction = Vector3.down;
        public Vector3 DirectionRandomRange = Vector3.zero;

        private Mesh _mesh;

        private int _lastCount = 0;
        private int _bufferSize = 0;

        public struct ParticleResult
        {
            public Vector3 Position;
            public float Life;
        }

        public struct ParticleInfo
        {
            public Vector3 Random;
            public Vector3 Speed;
        }

        // Start is called before the first frame update
        void Start()
        {
            Shader shader = null;//YoukiaEngine.EngineIO.Instance.ShaderFind("YoukiaEngine/Weather/GPUParticle/CPUParticle_Stander");
            if (shader == null)
            {
                Debug.LogWarning("CPUParticle_Stander Shader Not Find!");
                return;
            }

            Application.runInBackground = true;

            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            if (renderer == null)
            {
                renderer = gameObject.AddComponent<MeshRenderer>();
            }
            renderer.material = _material;
            renderer.material.shader = shader;

            RefuseBuffer();
        }

        private ParticleInfo[] infoBuffer;
        private ParticleResult[] resultBuffer;
        private Vector4[] bufferVer;
        private void _initBuffer()
        {
            infoBuffer = new ParticleInfo[_bufferSize];
            resultBuffer = new ParticleResult[_bufferSize];
            bufferVer = new Vector4[_bufferSize * 4];
            for (int i = 0; i < _bufferSize; i++)
            {
                infoBuffer[i].Random = new Vector4(Random.value, Random.value, Random.value, Random.value);
                Vector3 direction = Direction + new Vector3(
                                        Random.Range(-DirectionRandomRange.x / 2, DirectionRandomRange.x / 2),
                                        Random.Range(-DirectionRandomRange.y / 2, DirectionRandomRange.y / 2),
                                        Random.Range(-DirectionRandomRange.z / 2, DirectionRandomRange.z / 2));
                infoBuffer[i].Speed = direction.normalized *
                                      (Speed + Random.Range(-SpeedRandomRange / 2, SpeedRandomRange / 2));

                resultBuffer[i].Life = -Random.value * Duration;
                resultBuffer[i].Position = transform.position + new Vector3((Random.value - 0.5f) * EmitRange.x,
                                               (Random.value - 0.5f) * EmitRange.y,
                                               (Random.value - 0.5f) * EmitRange.z);

                bufferVer[i * 4] = resultBuffer[i].Position;
                bufferVer[i * 4].w = resultBuffer[i].Life;
                bufferVer[i * 4 + 1] = resultBuffer[i].Position;
                bufferVer[i * 4 + 1].w = resultBuffer[i].Life;
                bufferVer[i * 4 + 2] = resultBuffer[i].Position;
                bufferVer[i * 4 + 2].w = resultBuffer[i].Life;
                bufferVer[i * 4 + 3] = resultBuffer[i].Position;
                bufferVer[i * 4 + 3].w = resultBuffer[i].Life;
            }
        }

        private void _initMesh()
        {
            _mesh = new Mesh();

            Vector3[] vertices = new Vector3[4 * Count];
            Vector2[] uvs = new Vector2[4 * Count];
            int[] indices = new int[6 * Count];
            int vertixIndex = 0;
            for (int i = 0; i < Count; i++)
            {
                uvs[i * 4] = new Vector2(0, 0);
                uvs[i * 4 + 1] = new Vector2(1, 0);
                uvs[i * 4 + 2] = new Vector2(1, 1);
                uvs[i * 4 + 3] = new Vector2(0, 1);

                indices[i * 6] = vertixIndex;
                indices[i * 6 + 1] = vertixIndex + 2;
                indices[i * 6 + 2] = vertixIndex + 1;
                indices[i * 6 + 3] = vertixIndex + 0;
                indices[i * 6 + 4] = vertixIndex + 3;
                indices[i * 6 + 5] = vertixIndex + 2;
                vertixIndex += 4;
            }

            _mesh.vertices = vertices;
            _mesh.uv = uvs;
            _mesh.tangents = bufferVer;
            _mesh.SetIndices(indices, MeshTopology.Triangles, 0, false);

            // 设置一个包围盒，避免对象中心点绑定到摄像机范围外时效果不可见
            float fDis = Speed * Duration;
            Vector3 vSize = EmitRange * 0.5f;
            if (vSize.x < fDis) vSize.x = fDis;
            if (vSize.y < fDis) vSize.y = fDis;
            if (vSize.z < fDis) vSize.z = fDis;
            _mesh.bounds = new Bounds(Vector3.zero, vSize);
        }

        private int GetTexSize(int bufferSize)
        {
            int nSize = 16;
            while (nSize * nSize < bufferSize)
            {
                nSize *= 2;
            }
            return nSize;
        }

        private void RefuseBuffer()
        {
            if (Count < 1) Count = 1;
            if (_lastCount != Count)
            {
                _lastCount = Count;

                int newBufferSize = Count;
                if (_bufferSize != Count)
                {
                    _bufferSize = Count;
                    ClearBuffer();
                    _initBuffer();
                }
                
                // 刷新Mesh
                if (_mesh != null)
                {
                    Destroy(_mesh);
                    _mesh = null;
                }
                _initMesh();

                MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
                if (meshFilter == null)
                {
                    meshFilter = gameObject.AddComponent<MeshFilter>();
                }
                meshFilter.mesh = _mesh;
            }
        }

        private void ClearBuffer()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RefuseBuffer();

            if (_material && _bufferSize > 0)
            {
                UpdateMove(Time.deltaTime);
            }
        }

        private void UpdateMove(float delayTime)
        {
            Vector3 position = Vector2.zero;
            Vector3 speed = Vector2.zero;
            Vector3 random = Vector2.zero;

            Vector4 EmitPosition = transform.position;
            Vector2 repeatOffset = Vector2.zero;
            Vector2 repeatDisp = Vector2.zero;
            Vector2 quadrant = Vector2.zero;
            Vector3 respawnRandom = Vector2.zero;
            repeatOffset.x = EmitPosition.x - EmitRange.x / 2;
            repeatOffset.y = EmitPosition.z - EmitRange.z / 2;

            for (int i=0;i< _bufferSize;++i)
            {
                position = resultBuffer[i].Position;
                speed = infoBuffer[i].Speed;
                random = infoBuffer[i].Random;
                float life = resultBuffer[i].Life + delayTime;

                //Move
                if (life > 0)
                {
                    position += speed * delayTime;
                }

                //XZ Repeat
                repeatDisp.x = position.x - repeatOffset.x;
                repeatDisp.y = position.z - repeatOffset.y;
                quadrant.x = repeatDisp.x <= 0 ? 1 : 0;
                quadrant.y = repeatDisp.y <= 0 ? 1 : 0;
                position.x = (repeatDisp.x) % EmitRange.x + EmitRange.x * quadrant.x + repeatOffset.x;
                position.z = (repeatDisp.y) % EmitRange.z + EmitRange.z * quadrant.y + repeatOffset.y;

                //Respawn
                if (life > Duration)
                {
                    life = life % Duration;
                    respawnRandom.x = (float)Math.Sin((random.x + position.x) * 17);
                    respawnRandom.y = (float)Math.Sin((random.y + position.y) * 257);
                    respawnRandom.z = (float)Math.Sin((random.z + position.z) * 457);
                    //float3 respawnRandom = (random.xyz - 0.5) * 2;
                    position = EmitPosition;
                    position += Vector3.Scale(respawnRandom, EmitRange) / 2;
                }

                resultBuffer[i].Position = position;
                resultBuffer[i].Life = life;

                bufferVer[i * 4] = resultBuffer[i].Position;
                bufferVer[i * 4].w = resultBuffer[i].Life;
                bufferVer[i * 4 + 1] = resultBuffer[i].Position;
                bufferVer[i * 4 + 1].w = resultBuffer[i].Life;
                bufferVer[i * 4 + 2] = resultBuffer[i].Position;
                bufferVer[i * 4 + 2].w = resultBuffer[i].Life;
                bufferVer[i * 4 + 3] = resultBuffer[i].Position;
                bufferVer[i * 4 + 3].w = resultBuffer[i].Life;
            }

            _mesh.tangents = bufferVer;
        }

        void OnDestroy()
        {
            ClearBuffer();

            if (_mesh != null)
            {
                Destroy(_mesh);
                _mesh = null;
            }
        }
    }
}
