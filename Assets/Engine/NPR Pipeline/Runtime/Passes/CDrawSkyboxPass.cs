using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
    /// <summary>
    /// Draw the skybox into the given color buffer using the given depth buffer for depth testing.
    ///
    /// This pass renders the standard Unity skybox.
    /// </summary>
    [MovedFrom("UnityEngine.Rendering.Internal.Universal")]
    public class CDrawSkyboxPass : AScriptableRenderPass
    {
        public CDrawSkyboxPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
        }

        /// <inheritdoc/>
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            context.DrawSkybox(renderingData.cameraData.camera);
        }
    }
}
