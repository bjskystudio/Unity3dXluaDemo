using UnityEngine;
using UnityEditor;

public class AorUIUtils
{
    #region UI
    /// <summary>
    /// Unity默认UI Shader
    /// </summary>
    public static readonly string UnityUIDefaultShaderName = "UI/Default";
    /// <summary>
    /// Unity默认UI 材质球名字
    /// </summary>
    public static readonly string UnityUIDefaultMatName = "Default UI Material";

    /// <summary>
    /// 默认图片材质球路径
    /// </summary>
    private const string ImageDefaultMatPath = "Assets/Res/Material/Default_UI.mat";

    private const string ImageDefaultWorldMatPath = "Assets/Res/Material/Default_World_UI.mat";

    /// <summary>
    /// 默认图片材质球名
    /// </summary>
    public const string ImageDefaultMatName = "Default_UI";

    public const string ImageDefaultWorldMatName = "Default_World_UI";

    /// <summary>
    /// 默认图片材质球
    /// </summary>
    public static readonly Material ImageDefaultMat;

    public static readonly Material ImageDefaultWorldMat;
    #endregion

    #region Font
    /// <summary>
    /// 默认字体材质球路径
    /// </summary>
    private const string FontDefaultMatPath = "Assets/Res/Material/Default_Font.mat";

    private const string FontDefaultWorldMatPath = "Assets/Res/Material/Default_World_Font.mat";

    /// <summary>
    /// 默认字体材质球名
    /// </summary>
    public const string FontDefaultMatName = "Default_Font";

    public const string FontDefaultWorldMatName = "Default_World_Font";

    /// <summary>
    /// 默认字体材质球
    /// </summary>
    public static readonly Material FontDefaultMat;

    public static readonly Material FontDefaultWorldMat;
    #endregion

    #region Spine
    /// <summary>
    /// 默认Spine材质球路径
    /// </summary>
    private const string SpineDefaultMatPath = "Assets/Res/Material/Default_Spine.mat";

    /// <summary>
    /// 默认Spine材质球名
    /// </summary>
    public const string SpineDefaultMatName = "Default_Spine";

    /// <summary>
    /// 默认Spine材质球
    /// </summary>
    public static readonly Material SpineDefaultMat;
    #endregion

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static AorUIUtils()
    {
        ImageDefaultMat = AssetDatabase.LoadAssetAtPath<Material>(ImageDefaultMatPath);
        ImageDefaultWorldMat = AssetDatabase.LoadAssetAtPath<Material>(ImageDefaultWorldMatPath);
        FontDefaultMat = AssetDatabase.LoadAssetAtPath<Material>(FontDefaultMatPath);
        FontDefaultWorldMat = AssetDatabase.LoadAssetAtPath<Material>(FontDefaultWorldMatPath);
        SpineDefaultMat = AssetDatabase.LoadAssetAtPath<Material>(SpineDefaultMatPath);
    }
}