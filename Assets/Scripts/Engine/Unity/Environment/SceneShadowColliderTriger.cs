using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 区域阴影碰撞检查器,进入区域的物体具有对应shader关键字的会被处理
/// </summary>
public class SceneShadowColliderTriger : MonoBehaviour
{
    [Header("现从角色碰撞体父级向下查找Render")]
    public float ShadowAreaColor = 0.5f;
    public bool CloseShadow = true;

    //所以碰撞体视为一个整体
    static Dictionary<Collider, Renderer[]> Objdic = new Dictionary<Collider, Renderer[]>();
    static Dictionary<Renderer, UnityEngine.Rendering.ShadowCastingMode> OrgShadowEnableDic = new Dictionary<Renderer, UnityEngine.Rendering.ShadowCastingMode>();

    static int mID_ShadowArea = Shader.PropertyToID("_ShadowArea");
    MaterialPropertyBlock mMpb;
    private void Start()
    {
        mMpb = new MaterialPropertyBlock();
    }

    void OnTriggerExit(Collider collider)
    {
        if (Objdic.ContainsKey(collider))
        {
            Renderer[] renders = Objdic[collider];
            if (renders != null)
            {
                for (int i = 0; i < renders.Length; i++)
                {
                    if (renders[i] == null || renders[i].material == null || renders[i] is ParticleSystemRenderer)
                    {
                        continue;
                    }
                    mMpb.Clear();
                    renders[i].GetPropertyBlock(mMpb);
                    mMpb.SetFloat(mID_ShadowArea, 0f);
                    renders[i].SetPropertyBlock(mMpb);

                    //还原
                    if (OrgShadowEnableDic.ContainsKey(renders[i]))
                    {
                        renders[i].shadowCastingMode = OrgShadowEnableDic[renders[i]];
                    }
                }
            }
        }
    }


    Renderer[] renders = null;

    /// <summary>
    /// 不用OnTriggerEnable是因为重合物体的重复触发容易出显示bug
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerStay(Collider collider)
    {
        //不包含才检查,减少成本
        if (!Objdic.ContainsKey(collider))
        {
            renders = collider.transform.parent.GetComponentsInChildren<Renderer>();
            if (renders.Length == 0)
                return;

            Objdic.Add(collider, renders);

            //记录原来的阴影状态
            if (CloseShadow)
            {
                for (int i = 0; i < renders.Length; i++)
                {
                    if (!OrgShadowEnableDic.ContainsKey(renders[i]))
                    {
                        OrgShadowEnableDic.Add(renders[i], renders[i].shadowCastingMode);
                    }
                }
            }
        }
        else
        {
            renders = Objdic[collider];
        }

        if (renders != null)
        {
            for (int i = 0; i < renders.Length; i++)
            {
                if (renders[i] == null || renders[i].material == null || (renders[i] is ParticleSystemRenderer))
                {
                    continue;
                }
                mMpb.Clear();
                renders[i].GetPropertyBlock(mMpb);
                mMpb.SetFloat(mID_ShadowArea, Mathf.Abs(1 - ShadowAreaColor));
                renders[i].SetPropertyBlock(mMpb);

                if (CloseShadow)
                {
                    renders[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }

            }
        }
    }

    private void OnDestroy()
    {
        if(Objdic.Count > 0)
        {
            Objdic.Clear();
        }

        if(OrgShadowEnableDic.Count > 0)
        {
            OrgShadowEnableDic.Clear();
        }
    }
}
