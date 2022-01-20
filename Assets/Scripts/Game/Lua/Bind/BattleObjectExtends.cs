using Cinemachine;
using DG.Tweening;
using EngineCenter.Timeline;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using XLua;
using YoukiaCore.Log;
using YoukiaCore.Utils;
using UObject = UnityEngine.Object;


/// <summary>
/// 说明：xlua中的战斗专用扩展方法
/// </summary>
[LuaCallCSharp]
public static class BattleObjectExtends
{
    private static int renderqueueOffset = 800;
    private const string BattleEffectTag = "BattleEffect";
    #region RushToTarget 冲刺到目标位置distance处 参数：目标物体,攻击距离+战斗者size,速度s/m    返回:跑步时间（0为不跑步）

    public static float RushToTarget(this Transform self, Transform target, float distance, float speedRate)
    {
        if (self == null || target == null)
        {
            return 0;
        }
        Vector3 selfPos = self.position;
        selfPos.y = 0;
        Vector3 targetPos = target.position;
        targetPos.y = 0;
        float dis = Vector3.Distance(selfPos, targetPos);
        if (dis > distance)
        {
            Vector3 pos = targetPos + target.forward * distance;
            float time = speedRate * (dis - distance);
            self.DOMove(pos, time);
            return time;
        }
        return 0;
    }

    public static float RushToTarget(this Transform self, GameObject target, float distance, float speedRate)
    {
        return RushToTarget(self, target?.transform, distance, speedRate);
    }

    public static float RushToTarget(this Transform self, Component target, float distance, float speedRate)
    {
        return RushToTarget(self, target?.transform, distance, speedRate);
    }

    public static float RushToTarget(this GameObject self, GameObject target, float distance, float speedRate)
    {
        return RushToTarget(self?.transform, target?.transform, distance, speedRate);
    }

    public static float RushToTarget(this GameObject self, Component target, float distance, float speedRate)
    {
        return RushToTarget(self?.transform, target?.transform, distance, speedRate);
    }

    public static float RushToTarget(this GameObject self, Transform target, float distance, float speedRate)
    {
        return RushToTarget(self?.transform, target, distance, speedRate);
    }

    public static float RushToTarget(this Component self, GameObject target, float distance, float speedRate)
    {
        return RushToTarget(self?.transform, target?.transform, distance, speedRate);
    }

    public static float RushToTarget(this Component self, Component target, float distance, float speedRate)
    {
        return RushToTarget(self?.transform, target?.transform, distance, speedRate);
    }

    public static float RushToTarget(this Component self, Transform target, float distance, float speedRate)
    {
        return RushToTarget(self?.transform, target, distance, speedRate);
    }

    #endregion

    #region GetPosByTargetForwardDistance 获取物体朝向距离distance的位置 参数：距离、战斗者尺寸、限制物体(不超过)

    public static void GetPosByTargetForwardDistance(this Transform target, float distance, Transform limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        x = 0;
        y = 0;
        z = 0;
        if (target == null)
        {
            return;
        }
        Vector3 targetPos = target.position;
        targetPos.y = 0;
        targetPos = target.forward * distance + targetPos;
        if (limitObj != null)
        {
            Vector3 limmitTransPos;
            if (limitPosx != float.NegativeInfinity && limitPosy != float.NegativeInfinity && limitPosz != float.NegativeInfinity)
            {
                limmitTransPos = new Vector3(limitPosx, limitPosy, limitPosz);
            }
            else
            {
                limmitTransPos = limitObj.position;
            }
            limmitTransPos.y = 0;
            Vector3 limitPos = limitObj.forward * size + limmitTransPos;
            if (Vector3.Dot(target.forward, limitPos - targetPos) <= 0)
            {
                targetPos = limitPos;
            }
        }
        x = targetPos.x;
        y = targetPos.y;
        z = targetPos.z;
    }

    public static void GetPosByTargetForwardDistance(this Transform target, float distance, GameObject limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this Transform target, float distance, Component limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this GameObject target, float distance, GameObject limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target?.transform, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this GameObject target, float distance, Component limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target?.transform, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this GameObject target, float distance, Transform limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target?.transform, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this Component target, float distance, GameObject limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target?.transform, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this Component target, float distance, Component limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target?.transform, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    public static void GetPosByTargetForwardDistance(this Component target, float distance, Transform limitObj, out float x, out float y, out float z, float size = 0, float limitPosx = float.NegativeInfinity, float limitPosy = float.NegativeInfinity, float limitPosz = float.NegativeInfinity)
    {
        GetPosByTargetForwardDistance(target?.transform, distance, limitObj?.transform, out x, out y, out z, size, limitPosx, limitPosy, limitPosz);
    }

    #endregion

    #region HideModel 模型半透明消失 参数：callBack,endAlpha,time

    /// <summary>
    /// 模型半透明消失
    /// </summary>
    /// <param name="target"></param>
    /// <param name="endAlpha"></param>
    /// <param name="time"></param>
    /// <param name="callBack"></param>
    public static void HideModel(this GameObject target, Action callBack = null, float endAlpha = 0, float time = 0.5f)
    {
        //Debug.LogError("endAlpha-->" + endAlpha);
        if (target == null)
        {
            return;
        }
        var trans = target.transform;
        Renderer[] renderers = trans.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            //特效不修改,否则显示有问题
            if (renderer.CompareTag(BattleEffectTag))
            {
                continue;
            }
            if (renderer.material.HasProperty("_srcBlend"))
            {
                renderer.material.SetFloat("_srcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
            }
            if (renderer.material.HasProperty("_dstBlend"))
            {
                renderer.material.SetFloat("_dstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            }
            if (renderer.material.HasProperty("_srcAlphaBlend"))
            {
                renderer.material.SetFloat("_srcAlphaBlend", (float)UnityEngine.Rendering.BlendMode.One);
            }
            if (renderer.material.HasProperty("_dstAlphaBlend"))
            {
                renderer.material.SetFloat("_dstAlphaBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            }

            int oldQueue = renderer.material.renderQueue;
            if (oldQueue < 3000)
            {
                renderer.material.renderQueue = oldQueue + renderqueueOffset;
            }
            //Debug.LogError(renderer.material.renderQueue);
            if (renderer.material.HasProperty("_AlphaCtrl"))
            {
                if (time > 0)
                {
                    DOTween.To(() => renderer.material.GetFloat("_AlphaCtrl"), curr => { renderer.material.SetFloat("_AlphaCtrl", curr); }, endAlpha, time).onComplete = () =>
                    {
                        //Debug.LogError("renderer.material.GetFloat(_AlphaCtrl)-->" + renderer.material.GetFloat("_AlphaCtrl"));
                        if (renderer.material.HasProperty("_StencilOp"))
                        {
                            renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Keep);
                        }
                        if (renderer.material.HasProperty("_ZWriteCtrl"))
                        {
                            renderer.material.SetFloat("_ZWriteCtrl", 1f);
                        }
                        if (callBack != null)
                        {
                            callBack.Invoke();
                        }
                    };
                }
                else
                {
                    renderer.material.SetFloat("_AlphaCtrl", endAlpha);
                    if (renderer.material.HasProperty("_StencilOp"))
                    {
                        renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Keep);
                    }
                    if (renderer.material.HasProperty("_ZWriteCtrl"))
                    {
                        renderer.material.SetFloat("_ZWriteCtrl", 1f);
                    }
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                }

            }
        }
    }

    public static void HideModel(this Component target, Action callBack = null, float endAlpha = 0, float time = 0.5f)
    {
        HideModel(target?.gameObject, callBack, endAlpha, time);
    }

    #endregion

    #region 模型显示 参数：目标物体,是否显示残影
    /// <summary>
    /// 模型显示
    /// </summary>
    /// <param name="target"></param>
    /// <param name="showShadow">是否显示残影</param>
    public static void ShowModel(this GameObject target, Action callBack = null, float time = 0)
    {
        var trans = target.transform;
        Renderer[] renderers = trans.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            //特效不修改,否则显示有问题
            if (renderer.CompareTag(BattleEffectTag))
            {
                continue;
            }
            if (renderer.material.HasProperty("_srcBlend"))
            {
                renderer.material.SetFloat("_srcBlend", (float)UnityEngine.Rendering.BlendMode.One);
            }
            if (renderer.material.HasProperty("_dstBlend"))
            {
                renderer.material.SetFloat("_dstBlend", (float)UnityEngine.Rendering.BlendMode.Zero);
            }
            if (renderer.material.HasProperty("_srcAlphaBlend"))
            {
                renderer.material.SetFloat("_srcAlphaBlend", (float)UnityEngine.Rendering.BlendMode.One);
            }
            if (renderer.material.HasProperty("_dstAlphaBlend"))
            {
                renderer.material.SetFloat("_dstAlphaBlend", (float)UnityEngine.Rendering.BlendMode.Zero);
            }
            //if (renderer.material.HasProperty("_AlphaCtrl"))
            //{
            //    renderer.material.SetFloat("_AlphaCtrl", 1);
            //}
            //if (renderer.material.HasProperty("_StencilOp"))
            //{
            //    renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Replace);
            //}
            //if (renderer.material.HasProperty("_ZWriteCtrl"))
            //{
            //    renderer.material.SetFloat("_ZWriteCtrl", 1f);
            //}

            int oldQueue = renderer.material.renderQueue;
            if (oldQueue >= 3000)
            {
                renderer.material.renderQueue = oldQueue - renderqueueOffset;
                //Debug.LogError(renderer.material.renderQueue);
            }

            if (renderer.material.HasProperty("_AlphaCtrl"))
            {
                if (time > 0)
                {
                    DOTween.To(() => renderer.material.GetFloat("_AlphaCtrl"), curr => { renderer.material.SetFloat("_AlphaCtrl", curr); }, 1, time).onComplete = () =>
                    {
                        //Debug.LogError("renderer.material.GetFloat(_AlphaCtrl)-->" + renderer.material.GetFloat("_AlphaCtrl"));
                        if (renderer.material.HasProperty("_StencilOp"))
                        {
                            renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Replace);
                        }
                        if (renderer.material.HasProperty("_ZWriteCtrl"))
                        {
                            renderer.material.SetFloat("_ZWriteCtrl", 1);
                        }
                        if (callBack != null)
                        {
                            callBack.Invoke();
                        }
                    };
                }
                else
                {
                    renderer.material.SetFloat("_AlphaCtrl", 1);
                    if (renderer.material.HasProperty("_StencilOp"))
                    {
                        renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Replace);
                    }
                    if (renderer.material.HasProperty("_ZWriteCtrl"))
                    {
                        renderer.material.SetFloat("_ZWriteCtrl", 1);
                    }
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                }

            }
        }
    }

    /// <summary>
    /// 模型显示
    /// </summary>
    /// <param name="target"></param>
    /// <param name="showShadow">是否显示残影</param>
    public static void ShowModel(this Component target, Action callBack = null, float time = 0)
    {
        ShowModel(target?.gameObject, callBack, time);
    }

    #endregion

    #region 设置剪辑镜像 参数：目标，是否镜像
    /// <summary>
    /// 设置剪辑镜像
    /// </summary>
    public static void SetTimeLineMirror(this GameObject target, int Mirror = 0)
    {
        TimelineMirror mirror = target.transform.GetComponent<TimelineMirror>();
        if (mirror == null)
        {
            // Debug.LogError("没有找到TimelineMirror:" + target.name);
            return;
        }
        mirror.mIsMirror = Mirror == 1;
    }

    public static void SetTimeLineMirror(this Component target, int Mirror = 0)
    {
        SetTimeLineMirror(target?.gameObject, Mirror);
    }

    #endregion

    #region 设置角色镜像 参数：目标，是否镜像
    /// <summary>
    /// 设置角色镜像
    /// </summary>
    public static void SetRoleMirror(this GameObject target, int Mirror = 0)
    {
        MirrorUtility.SwitchMirror(MirrorUtility.AddMirrorComponent(target), Mirror == 1);
    }
    public static void SetRoleMirror(this Component target, int Mirror = 0)
    {
        SetRoleMirror(target?.gameObject, Mirror);
    }
    #endregion

    #region 获取剪辑镜像后物体(镜像会新创建一个物体)
    public static GameObject GetMirrorGameObject(this GameObject target)
    {
        if (target == null)
        {
            return null;
        }
        SymmetryMirror mirror = target.GetComponent<SymmetryMirror>();
        if (mirror == null)
        {
            return null;
        }
        return mirror.mSymmetryObj;
    }

    public static GameObject GetMirrorGameObject(this Transform target)
    {
        return GetMirrorGameObject(target?.gameObject);
    }

    public static GameObject GetMirrorGameObject(this Component target)
    {
        return GetMirrorGameObject(target?.gameObject);
    }
    #endregion

    #region 当前是否正在播放指定名称的动作
    public static int IsCurrStateName(this Animator target, int layer, string name)
    {
        if (target == null)
        {
            return 0;
        }
        AnimatorStateInfo stateInfo = target.GetCurrentAnimatorStateInfo(layer);
        return stateInfo.IsName(name) ? 1 : 0;
    }
    #endregion

    #region 显示残影 参数：目标，是否显示，颜色下标，颜色值，透明度下标，透明度值
    /// <summary>
    /// 显示残影
    /// </summary>
    public static void ShowAfterImage(this GameObject target, int show, float[] colorKeys = null, float[] colors = null, float[] alphaKeys = null, float[] alphas = null)
    {
        if (target == null)
        {
            return;
        }
        AfterImageCreator imageCreator = target.GetComponent<AfterImageCreator>();
        if (imageCreator == null)
        {
            return;
        }
        bool IsCreate = show == 1;
        if (IsCreate)
        {
            GradientColorKey[] gradientColorKeys = null;
            if (colorKeys != null && colors.Length == colorKeys.Length * 3)
            {
                gradientColorKeys = new GradientColorKey[colorKeys.Length];
                for (int i = 0; i < colorKeys.Length; i++)
                {
                    int colorIndex = i * 3;
                    GradientColorKey gradientColor = new GradientColorKey(new Color(colors[colorIndex], colors[colorIndex + 1], colors[colorIndex + 2]), colorKeys[i]);
                    gradientColorKeys[i] = gradientColor;
                }
            }
            GradientAlphaKey[] gradientAlphaKeys = null;
            if (alphaKeys != null && alphaKeys.Length == alphas.Length)
            {
                gradientAlphaKeys = new GradientAlphaKey[alphaKeys.Length];
                for (int i = 0; i < alphaKeys.Length; i++)
                {
                    GradientAlphaKey gradientAlphaKey = new GradientAlphaKey(alphas[i], alphaKeys[i]);
                    gradientAlphaKeys[i] = gradientAlphaKey;
                }
            }
            if (gradientColorKeys != null || gradientAlphaKeys != null)
            {
                imageCreator.AfterImageColor.SetKeys(gradientColorKeys, gradientAlphaKeys);
            }
        }
        imageCreator.IsCreate = IsCreate;

    }

    #endregion

    #region 获取战斗中世界中物体尺寸转化为scale的尺寸比率 参数：z 距离
    public static void GetBattleWorldToUIScale(float z, out float w, out float h)
    {
        Vector3 start = new Vector3(0, 0, z);
        Vector3 end = new Vector3(Screen.width, Screen.height, z);
        Vector3 startW = Camera.main.ScreenToWorldPoint(start);
        Vector3 endW = Camera.main.ScreenToWorldPoint(end);
        w = Screen.width / (Mathf.Abs(endW.x - startW.x));
        h = Screen.height / (Mathf.Abs(endW.y - startW.y));
    }
    #endregion
}