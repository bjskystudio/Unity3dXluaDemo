namespace UnityEngine.Rendering.Universal
{
    /// <summary>
    /// Invokes OnRenderObject callback
    /// </summary>

    internal class CInvokeOnRenderObjectCallbackPass : AScriptableRenderPass
    {
        public CInvokeOnRenderObjectCallbackPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
        }

        /// <inheritdoc/>
        public override void Execute(ScriptableRenderContext context, ref SRenderingData renderingData)
        {
            context.InvokeOnRenderObjectCallback();
        }
    }
}
