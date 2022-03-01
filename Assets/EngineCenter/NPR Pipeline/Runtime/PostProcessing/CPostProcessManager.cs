using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public class CPostProcessManager
    {
        #region 私有成员变量
        bool m_bEnable;
        List<AEffect>[] m_Effects;
        bool m_bDirtyClassPriority;
        #endregion

        #region 单例相关
        static CPostProcessManager s_Instance;

        public static CPostProcessManager instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new CPostProcessManager();
                return s_Instance;
            }
        }
        #endregion

        #region 构造函数
        public CPostProcessManager()
        {
            m_bEnable = true;
            m_Effects = new List<AEffect>[(int)EFFECT_TYPE.MAX];
        }
        #endregion

        #region 属性访问器
        public bool enable
        {
            get => m_bEnable;
            set => m_bEnable = value;
        }
        public List<AEffect>[] effects
        {
            get { return m_Effects; }
        }
        public bool dirty_class_priority
        {
            set => m_bDirtyClassPriority = value;
        }
        #endregion

        #region 公共函数
        public bool IsActiveByEffect(EFFECT_TYPE e)
        {
            if (m_Effects[(int)e] == null || m_Effects[(int)e].Count == 0)
                return false;

            return m_Effects[(int)e][0].IsActive();
        }

        public AEffect RequestEffect(EFFECT_TYPE e, LayerMask post_processing_culling_mask/*, bool need_culling = true*/)
        {
            List<AEffect> list = m_Effects[(int)e];
            if (list == null || list.Count == 0 || list[0] == null)
                return null;

            AEffect effect = null;

            //if (need_culling)
            //{
                for (int i = 0; i < list.Count; ++i)
                {
                    if ((post_processing_culling_mask.value & 1 << list[i].gameObject.layer) != 0)
                    {
                        effect = list[i];
                        break;
                    }
                }
            //}else
            //{
            //    effect = list.Count > 0 ? list[0] : null;
            //}
            return effect;
        }

        public void AddEffect(AEffect effect)
        {
            if (m_Effects[(int)effect.GetEffectType()] == null)
                m_Effects[(int)effect.GetEffectType()] = new List<AEffect>();
            m_Effects[(int)effect.GetEffectType()].Insert(0, effect);
            SortEffects();
        }

        public void RemoveEffect(AEffect effect)
        {
            if (m_Effects[(int)effect.GetEffectType()] == null)
                m_Effects[(int)effect.GetEffectType()] = new List<AEffect>();
            m_Effects[(int)effect.GetEffectType()].Remove(effect);
        }

        public void Update()
        {
            if (m_bDirtyClassPriority)
                SortEffects();
        }
        #endregion

        #region 私有函数
        internal void SortEffects()
        {
            for (int i = 0; i < m_Effects.Length; ++i)
            {
                if (m_Effects[i] != null &&
                    m_Effects[i].Count > 1)
                {
                    m_Effects[i].Sort((a, b) =>
                    {
                        return b.class_priority.CompareTo(a.class_priority);
                    });
                }
            }
        }
        #endregion
    }
}