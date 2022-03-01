using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using YoukiaEngine;
using Random = UnityEngine.Random;

namespace YoukiaEngine
{
    public class GPUParticleRenderer : MonoBehaviour
    {
        public ComputeShader _computeShader;
        public Material _material;
        public int Count = 1;
        public Vector3 EmitRange = new Vector3(5, 1, 5);
        public float Duration = 1;
        public float Speed = 5;
        public float SpeedRandomRange = 0;
        public Vector3 Direction = Vector3.down;
        public Vector3 DirectionRandomRange = Vector3.zero;

        private ComputeBuffer _infoBuffer;
        private ComputeBuffer _resultBuffer;
        private RenderTexture _resultTexture;
        private int _kernel = -1;
        private Mesh _mesh;

        private string _strCSPath = "YoukiaEngine/Weather/GPUParticle/GPUParticle_Weather";

        private int BLOCK_SIZE_X = 32;
        private int _lastCount = 0;
        private int _bufferSize = 0;
        private int _texSize = 0;
        private bool _bUseTexBuffer = false;

        public struct ParticleResult
        {
            public Vector4 Position;
            public float Life;
        }

        public struct ParticleInfo
        {
            public Vector4 Random;
            public Vector4 Speed;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!SystemInfo.supportsComputeShaders)
            {
                CPUParticleRenderer cpuParticle = gameObject.GetComponent<CPUParticleRenderer>();
                if (cpuParticle == null)
                {
                    cpuParticle = gameObject.AddComponent<CPUParticleRenderer>();
                    cpuParticle._material = _material;
                    cpuParticle.Count = Count;
                    cpuParticle.EmitRange = EmitRange;
                    cpuParticle.Duration = Duration;
                    cpuParticle.Speed = Speed;
                    cpuParticle.SpeedRandomRange = SpeedRandomRange;
                    cpuParticle.Direction = Direction;
                    cpuParticle.DirectionRandomRange = DirectionRandomRange;
                }

                Debug.LogWarning("Use CPU Particle!");
                return;
            }

            Application.runInBackground = true;

            if (_computeShader == null)
            {
               // _computeShader = EngineIO.Instance.ComputeShaderFind(_strCSPath);
            }

            _kernel = _computeShader.FindKernel("CSMain");

            // 如果是华为GPU,则不支持vert里面访问buffer,使用RenderTexture做buffer输出
            _bUseTexBuffer = (GPUDeviceInfo.GetDeviceType() == GPUDeviceInfo.DeviceType.HuaWei);

            if (_bUseTexBuffer)
            {
                int k = _computeShader.FindKernel("CSMain_HuaWei");
                if (k >= 0)
                {
                    _kernel = k;
                }
                else
                {
                    _bUseTexBuffer = false;
                }
            }
            
            if (_bUseTexBuffer)
            {
                _material.EnableKeyword("USE_TEX_BUFFER");
            }
            else
            {
                _material.DisableKeyword("USE_TEX_BUFFER");
            }
           
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            if (renderer == null)
            {
                renderer = gameObject.AddComponent<MeshRenderer>();
            }
            renderer.material = _material;

            RefuseBuffer();
        }

        private void _initBuffer()
        {
            _infoBuffer = new ComputeBuffer(_bufferSize, Marshal.SizeOf(typeof(ParticleInfo)), ComputeBufferType.Default);
            ParticleInfo[] infoBuffer = new ParticleInfo[_bufferSize];
            _resultBuffer = new ComputeBuffer(_bufferSize, Marshal.SizeOf(typeof(ParticleResult)), ComputeBufferType.Default);
            ParticleResult[] resultBuffer = new ParticleResult[_bufferSize];
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
            }

            _infoBuffer.SetData(infoBuffer);
            _resultBuffer.SetData(resultBuffer);


            _computeShader.SetBuffer(_kernel, "Info", _infoBuffer);
            _computeShader.SetBuffer(_kernel, "Result", _resultBuffer);
            if (_bUseTexBuffer)
            {
                _computeShader.SetTexture(_kernel, "ResultTex", _resultTexture);
                _material.SetTexture("_ParticleTexBuffer", _resultTexture);
            }
            else
            {
                _material.SetBuffer("_ParticleBuffer", _resultBuffer);
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

                int newBufferSize = Count / BLOCK_SIZE_X * BLOCK_SIZE_X;
                if (Count % BLOCK_SIZE_X > 0)
                {
                    newBufferSize += BLOCK_SIZE_X;
                }

                if (_bUseTexBuffer)
                {
                    int newTexSize = GetTexSize(newBufferSize);
                    if (_texSize != newTexSize)
                    {
                        _texSize = newTexSize;

                        if (_resultTexture != null)
                        {
                            _resultTexture.Release();
                            _resultTexture = null;
                        }
                        // 华为不支持ARGBFloat，现在利用加减_EmitPosition值，来使得ARGBHalf精度足够
                        //if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat))
                        //{
                        //    _resultTexture = new RenderTexture(_texSize, _texSize, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                        //}
                        //else
                        //{
                        _resultTexture = new RenderTexture(_texSize, _texSize, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
                        //}
                        _resultTexture.wrapMode = TextureWrapMode.Clamp;
                        _resultTexture.filterMode = FilterMode.Point;
                        _resultTexture.enableRandomWrite = true;
                        _resultTexture.Create();
                    }
                }

                if (_bufferSize != newBufferSize)
                {
                    _bufferSize = newBufferSize;
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
            if (_infoBuffer != null)
            {
                _infoBuffer.Release();
                _infoBuffer = null;
            }
            if (_resultBuffer != null)
            {
                _resultBuffer.Release();
                _resultBuffer = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_kernel < 0)
            {
                return;
            }

            RefuseBuffer();

            if (_material && _bufferSize > 0)
            {
                _computeShader.SetInt("_texSize", _texSize);
                _computeShader.SetFloat("_Duration", Duration);
                _computeShader.SetVector("_EmitRange", EmitRange);
                _computeShader.SetVector("_EmitPosition", transform.position);
                _computeShader.SetFloat("_DeltaTime", Time.deltaTime);
                _computeShader.SetBuffer(_kernel, "Info", _infoBuffer);
                _computeShader.SetBuffer(_kernel, "Result", _resultBuffer);
                if (_bUseTexBuffer)
                {
                    _computeShader.SetTexture(_kernel, "ResultTex", _resultTexture);
                }
                _computeShader.Dispatch(_kernel, _bufferSize / BLOCK_SIZE_X, 1, 1);

                if (_bUseTexBuffer)
                {
                    _material.SetInt("_texSize", _texSize);
                    _material.SetVector("_EmitPosition", transform.position);
                    _material.SetTexture("_ParticleTexBuffer", _resultTexture);
                }
                else
                {
                    _material.SetBuffer("_ParticleBuffer", _resultBuffer);
                }
            }
        }

        void OnDestroy()
        {
            if (_resultTexture != null)
            {
                _resultTexture.Release();
                _resultTexture = null;
            }

            ClearBuffer();

            if (_mesh != null)
            {
                Destroy(_mesh);
                _mesh = null;
            }
        }
    }
}
