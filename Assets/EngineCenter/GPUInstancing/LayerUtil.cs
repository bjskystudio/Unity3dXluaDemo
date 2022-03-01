using UnityEngine;

public class LayerUtil
{
    public const int LayerDefault = 0;
    public const int LayerWater = 4;
    public const int LayerUI = 5;
    public const int LayerPostProcessing = 8;
    public const int LayerTerrain = 9;
    public const int LayerSceneObject = 10;
    public const int LayerPlayer = 11;
    public const int LayerMonster = 12;
    public const int LayerSceneEffect = 13;
    public const int LayerSkillEffect = 14;
    public const int LayerUIHide = 15;
    public const int LayerNavMesh = 16;
	public const int LayerShadowCaster = 17;        // 投影层级
    public const int LayerFarScene = 18;        // 远景

    public static LayerMask Default = 1 << LayerDefault;
    public static LayerMask Water = 1 << LayerWater;
    public static LayerMask UI = 1 << LayerUI;
    public static LayerMask PostProcessing = 1 << LayerPostProcessing;
    public static LayerMask Terrain = 1 << LayerTerrain;
    public static LayerMask SceneObject = 1 << LayerSceneObject;
    public static LayerMask Player = 1 << LayerPlayer;
    public static LayerMask Monster = 1 << LayerMonster;
    public static LayerMask SceneEffect = 1 << LayerSceneEffect;
    public static LayerMask SkillEffect = 1 << LayerSkillEffect;
    public static LayerMask UIHide = 1 << LayerUIHide;
    public static LayerMask NavMesh = 1 << LayerNavMesh;
	public static LayerMask ShadowCaster = 1 << LayerShadowCaster;
    public static LayerMask FarScene = 1 << LayerFarScene;


    public static void SetLayer(GameObject obj, int layer)
    {
#if ENABLE_PROFILER
        UnityEngine.Profiling.Profiler.BeginSample("LayerUtil.SetLayer");
#endif
        obj.layer = layer;
        Transform[] childs = obj.GetComponentsInChildren<Transform>(true);
        int len = childs.Length;
        for (int i = 0; i < len; i++)
        {
            Transform transChild = childs[i];
            transChild.gameObject.layer = layer;
        }
#if ENABLE_PROFILER
        UnityEngine.Profiling.Profiler.EndSample();
#endif
    }

    /// <summary>
    /// 正常时候 主摄像机所看的层级
    /// </summary>
    /// <returns></returns>
    public static LayerMask GetNormalMapLayer()
    {
        return Default.value + Water.value + Monster.value + Player.value + Terrain.value + SceneEffect.value + SkillEffect.value + SceneObject.value;
    }

	/// <summary>
	/// 动态阴影相机 culling mask
	/// </summary>
	/// <returns></returns>
	public static LayerMask GetDynamicShadowLayer()
	{
		return Monster.value + ShadowCaster.value;
	}

	/// <summary>
	/// 静态阴影相机 culling mask
	/// </summary>
	/// <returns></returns>
	public static LayerMask GetStaticShadowLayer()
	{
		return  SceneObject.value + ShadowCaster.value;
	}

	/// <summary>
	/// 屏幕空间阴影相机 culling mask
	/// </summary>
	/// <returns></returns>
	public static LayerMask GetScreenSpaceShadowLayer()
	{
		return Monster.value + Player.value + Terrain.value + SceneObject.value;
	}

	/// <summary>
	/// 反射相机 culling mask
	/// </summary>
	public static LayerMask LayerMaskGetReflectLayer()
	{
		return Terrain.value + SceneObject.value;
	}

    ///// <summary>
    ///// 获取剧情专用层级
    ///// </summary>
    ///// <returns></returns>
    //public static int GetStoryLayerCullingMask()
    //{
    //    return LayerUtil.Terrain.value + LayerUtil.Story.value + LayerUtil.Pendant.value + LayerUtil.Model.value + LayerUtil.SceneEffect.value + LayerUtil.Default.value + LayerUtil.ShadowGround.value; // + LayerUtil.SceneGameObjectHide.value
    //}

    ///// <summary>
    ///// NPC对话时候 主摄像机的层级
    ///// </summary>
    ///// <returns></returns>
    //public static LayerMask GetNpcCamLayer()
    //{
    //    return 1 << LayerNpc | 1 << TerrainLayer | 1 << SceneEffectLayer | 1 << ScenePendantLayer | 1 << LayerShadowGround | 1 << SceneModelLayer | 1 << LayerModelNodeEffect;
    //}

    //public static LayerMask GetSceneRoleLayer()
    //{
    //    return 1 << LayerNpc | 1 << LayerMonster | 1 << LayerPlayer;
    //}


}
