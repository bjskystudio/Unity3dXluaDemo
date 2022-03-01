using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    [ExecuteInEditMode]
    public abstract class AEffect : MonoBehaviour
    {
        #region 公共成员变量
        public int m_iClassPriority = 0;
        public LayerMask m_CameraCulling = 1;
        #endregion

        #region 私有成员变量
        int m_iClassPriorityOld = -1;
        string m_sIdentify;
        #endregion

        #region 属性访问器
        public int class_priority
        {
            get { return m_iClassPriority; }
            set { m_iClassPriority = value; }
        }
        #endregion

        #region 构造函数
        public AEffect(string identify)
        {
            m_sIdentify = identify;
        }
        #endregion

        #region 重载u3d函数
        void Awake()
        {
            OnInitialized();
        }

        void OnEnable()
        {
            CPostProcessManager.instance.AddEffect(this);
            OnEnabled();
        }

        void OnDisable()
        {
            CPostProcessManager.instance.RemoveEffect(this);
            OnDisabled();
        }

        void Update()
        {
            if (m_iClassPriorityOld != m_iClassPriority)
            {
                CPostProcessManager.instance.dirty_class_priority = true;
                m_iClassPriorityOld = m_iClassPriority;
            }
        }
        #endregion

        #region 保护函数
        protected bool CheckDisabledEffect()
        {
            //if (CNPRPipeline.asset.CheckDisabledEffect(m_sIdentify))
            //{
            //    enabled = false;
            //    return true;
            //}
            //else
            //    return false;

            return CNPRPipeline.asset != null? CNPRPipeline.asset.CheckDisabledEffect(m_sIdentify) : false;
        }

        protected BuiltinRenderTextureType BlitDstDiscardContent(CommandBuffer cmd, RenderTargetIdentifier rt)
        {
            // We set depth to DontCare because rt might be the source of PostProcessing used as a temporary target
            // Source typically comes with a depth buffer and right now we don't have a way to only bind the color attachment of a RenderTargetIdentifier
            cmd.SetRenderTarget(new RenderTargetIdentifier(rt, 0, CubemapFace.Unknown, -1),
                RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store,
                RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
            return BuiltinRenderTextureType.CurrentActive;
        }

        protected RenderTextureDescriptor GetStereoCompatibleDescriptor(RenderTextureDescriptor descriptor, int width, int height, GraphicsFormat format, int depthBufferBits = 0)
        {
            // Inherit the VR setup from the camera descriptor
            RenderTextureDescriptor desc = descriptor;
            desc.depthBufferBits = depthBufferBits;
            desc.msaaSamples = 1;
            desc.width = width;
            desc.height = height;
            desc.graphicsFormat = format;
            return desc;
        }
        #endregion

        #region 派生类必须覆盖的方法
        public abstract EFFECT_TYPE GetEffectType();
        public abstract bool IsActive();
        public abstract bool IsTileCompatible();
        #endregion

        #region 派生类可选覆盖的方法
        protected virtual bool OnInitialized() { return true; }
        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        #endregion
    }
}