using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.Universal
{
    class CShaderData : IDisposable
    {
        static CShaderData m_Instance = null;
        ComputeBuffer m_LightDataBuffer = null;
        ComputeBuffer m_LightIndicesBuffer = null;

        ComputeBuffer m_ShadowDataBuffer = null;
        ComputeBuffer m_ShadowIndicesBuffer = null;

        CShaderData()
        {
        }

        internal static CShaderData instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new CShaderData();

                return m_Instance;
            }
        }

        public void Dispose()
        {
            DisposeBuffer(ref m_LightDataBuffer);
            DisposeBuffer(ref m_LightIndicesBuffer);
            DisposeBuffer(ref m_ShadowDataBuffer);
            DisposeBuffer(ref m_ShadowIndicesBuffer);
        }

        internal ComputeBuffer GetLightDataBuffer(int size)
        {
            return GetOrUpdateBuffer<ShaderInput.LightData>(ref m_LightDataBuffer, size);
        }

        internal ComputeBuffer GetLightIndicesBuffer(int size)
        {
            return GetOrUpdateBuffer<int>(ref m_LightIndicesBuffer, size);
        }

        internal ComputeBuffer GetShadowDataBuffer(int size)
        {
            return GetOrUpdateBuffer<ShaderInput.ShadowData>(ref m_ShadowDataBuffer, size);
        }

        internal ComputeBuffer GetShadowIndicesBuffer(int size)
        {
            return GetOrUpdateBuffer<int>(ref m_ShadowIndicesBuffer, size);
        }

        ComputeBuffer GetOrUpdateBuffer<T>(ref ComputeBuffer buffer, int size) where T : struct
        {
            if (buffer == null)
            {
                buffer = new ComputeBuffer(size, Marshal.SizeOf<T>());
            }
            else if (size > buffer.count)
            {
                buffer.Dispose();
                buffer = new ComputeBuffer(size, Marshal.SizeOf<T>());
            }

            return buffer;
        }

        void DisposeBuffer(ref ComputeBuffer buffer)
        {
            if (buffer != null)
            {
                buffer.Dispose();
                buffer = null;
            }
        }
    }
}
