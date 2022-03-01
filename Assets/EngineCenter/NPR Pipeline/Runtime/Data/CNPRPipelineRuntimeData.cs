namespace UnityEngine.Rendering.Universal
{
    public class CNPRPipelineRuntimeData
    {
        #region 参数类相关定义和操作
        public class AParameter
        {
            public bool m_bIsOverrided = false;
            public bool is_overrided { get { return m_bIsOverrided; } }
            public void Invalid() { m_bIsOverrided = false; }
        }

        public class CParameter<T> : AParameter
        {
            public T m_Value;

            public CParameter(T value) { m_Value = value; }

            public void SetValue(T value)
            {
                m_bIsOverrided = true;
                m_Value = value;
            }
        }

        [System.Serializable]
        public class CParameterFloat : AParameter
        {
            public float m_Value;
            public CParameterFloat(float f) { m_Value = f; }
            public float value { get { return m_Value; } }
        }

        [System.Serializable]
        public class CParameterInt : AParameter
        {
            public int m_Value;
            public CParameterInt(int i) { m_Value = i; }
            public float value { get { return m_Value; } }
        }
        public static T Value<T>(T runtime, bool overrided, T asset_value) { return overrided ? runtime : asset_value; }
        #endregion

        #region 运行时数据
        bool m_bStandardCheckEnvironment = false;
        #endregion

        #region 运行时覆盖数据
        CParameter<float> m_ShadowDistance;
        CParameter<ShadowCascades> m_ShadowCascades;
        CParameter<float> m_Cascade2Split;
        #endregion

        #region 属性访问器
        public bool standard_check_environment
        {
            get { return m_bStandardCheckEnvironment; }
            set { m_bStandardCheckEnvironment = value; }
        }

        public float shadow_distance
        {
            get { return CNPRPipeline.asset == null ? m_ShadowDistance.m_Value : Value(m_ShadowDistance.m_Value, m_ShadowDistance.is_overrided, CNPRPipeline.asset.shadowDistance); }
            set { m_ShadowDistance.SetValue(Mathf.Max(0.0f, value)); }
        }

        public ShadowCascades shadow_cascades
        {
            get { return CNPRPipeline.asset == null ? m_ShadowCascades.m_Value : Value(m_ShadowCascades.m_Value, m_ShadowCascades.is_overrided, CNPRPipeline.asset.shadowCascade); }
            set { m_ShadowCascades.SetValue(value); }
        }

        public float cascade_2_split
        {
            get { return CNPRPipeline.asset == null ? m_Cascade2Split.m_Value : Value(m_Cascade2Split.m_Value, m_Cascade2Split.is_overrided, CNPRPipeline.asset.cascade2Split); }
            set { m_Cascade2Split.SetValue(Mathf.Max(0.0f, value)); }
        }
        #endregion

        #region 构造函数
        public CNPRPipelineRuntimeData()
        {
            m_ShadowDistance = new CParameter<float>(0.0f);
            m_ShadowCascades = new CParameter<ShadowCascades>(ShadowCascades.NoCascades);
            m_Cascade2Split = new CParameter<float>(0.25f);
        }
        #endregion

        #region 处理字段覆盖无效
        public void InvalidShadowDistance() { m_ShadowDistance.Invalid(); }
        public void InvalidShadowCascades() { m_ShadowCascades.Invalid(); }
        public void InvalidCascade2Split() { m_Cascade2Split.Invalid(); }
        #endregion
    }
}
