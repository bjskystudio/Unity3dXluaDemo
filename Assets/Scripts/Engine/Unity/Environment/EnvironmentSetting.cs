using UnityEngine;
using UnityEngine.Rendering;

namespace Framework.Environment
{
    [ExecuteInEditMode]
    /// <summary>
    /// 场景环境设置
    /// </summary>
    public class EnvironmentSetting : MonoBehaviour
    {
        private bool m_dirty;

        public bool IsDirty
        {
            get { return m_dirty; }
            set { m_dirty = value; }
        }

        /// <summary>
        /// 选择的Layer
        /// </summary>
        [Label("摄像机所看的层级")]
        public LayerMask CameraCullingMask = 1 << 0 | 1 << 1 | 1 << 2 | 1 << 4 | 1 << 8 | 1 << 9 | 1 << 12;

        /// <summary>
        /// LightMap设置
        /// </summary>
        public LightMapSetting LightMap;

        //天空盒
        public Renderer SkySphere;
        public AnimationCurve CloudSizeyCurve = new AnimationCurve(new Keyframe(0f, 2f), new Keyframe(10f, 0.7f));
        float mActiveTime;
        MaterialPropertyBlock mMPB;
        static int mID_CloudSize = Shader.PropertyToID("_CloudSize");

        //粒子云环境光
        [SerializeField]
        [ColorUsage(false, true)]
        private Color m_ParticleCloudAmbientColor = Color.gray;
        static int mID_ParticleCloudAmbientColor = Shader.PropertyToID("_ParticleCloudAmbientColor");

        //环境光
        [SerializeField]
        [ColorUsage(false, true)]
        private Color m_AmbientColor;
        public Color AmbientColor
        {
            get { return m_AmbientColor; }
            set
            {
                if (m_AmbientColor != value)
                {
                    m_AmbientColor = value;
                    m_dirty = true;
                }
            }
        }

        #region 边缘光设置
        [Header("这是边缘光颜色和强度哦")]
        [SerializeField]
        [ColorUsage(false, true)]
        private Color m_rimLight;
        static int mID_RimCol = Shader.PropertyToID("_RimCol");
        #endregion

        #region 后处理

        public GameObject BloomPostTargetGo;

        #endregion

        private void Start()
        {
            InitAttr();
        }


        protected void OnEnable()
        {
            if (Application.isPlaying)
            {
                mActiveTime = Time.time;
            }
            CameraManager.Get()?.AddEnvironmentSetting(this);
            UpdateSetting();
        }

        public void UpdateSetting()
        {
            InitAttr();
            m_UpdateEnvironmentSetting();
            SetBloomPostActive(true);
        }

        public void InitAttr()
        {
            if (BloomPostTargetGo == null)
            {
                BloomPostTargetGo = GameObject.Find("Global Volume");
                if (BloomPostTargetGo == null)
                {
                    var bloom = gameObject.GetComponentInChildren<EngineCenter.NPRPipeline.PostProcessing.CBloom>();
                    if (bloom != null)
                    {
                        BloomPostTargetGo = bloom.gameObject;
                    }
                }
            }
            if (LightMap == null)
            {
                LightMap = gameObject.GetComponent<LightMapSetting>();
                if (LightMap == null)
                {
                    LightMap = gameObject.GetComponentInChildren<LightMapSetting>();
                }
            }
        }

        /// <summary>
        /// 设置后处理开关
        /// </summary>
        /// <param name="active"></param>
        public void SetBloomPostActive(bool active)
        {
            BloomPostTargetGo?.SetActive(active);
        }

        private void OnDisable()
        {
            CameraManager.Get()?.RemoveEnvironmentSetting(this);
        }

        protected void m_UpdateEnvironmentSetting()
        {
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = m_AmbientColor;

            Shader.SetGlobalColor(mID_ParticleCloudAmbientColor, m_ParticleCloudAmbientColor);
            Shader.SetGlobalColor(mID_RimCol, m_rimLight);
        }

        protected void Update()
        {
            if (m_dirty) { m_UpdateEnvironmentSetting(); m_dirty = false; }

            //更新天空盒
            if (Application.isPlaying && SkySphere != null && CloudSizeyCurve != null)
            {
                if (mMPB == null)
                {
                    mMPB = new MaterialPropertyBlock();
                }
                float ratio = Time.time - mActiveTime;
                float density = CloudSizeyCurve.Evaluate(ratio);
                mMPB.SetFloat(mID_CloudSize, density);
                SkySphere.SetPropertyBlock(mMPB);
            }

            //粒子云环境光
#if UNITY_EDITOR
            Shader.SetGlobalColor(mID_ParticleCloudAmbientColor, m_ParticleCloudAmbientColor);
            Shader.SetGlobalColor(mID_RimCol, m_rimLight);
#endif
        }
    }
}

