using UnityEngine;
using UnityEngine.Rendering;

namespace EngineCenter.NPRPipeline
{
    public struct SRenderTargetHandle
    {
        public int id;
        public RenderTargetIdentifier rti;

        public void Initialize(int name_id)
        {
            id = name_id;
            rti = new RenderTargetIdentifier(id);
        }

        public void Initialize(string shader_property)
        {
            id = Shader.PropertyToID(shader_property);
            rti = new RenderTargetIdentifier(id);
        }
    }
}
