using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FogBetterMgr : MonoBehaviour
{
#if UNITY_EDITOR
    private bool mRealTimeUpdate = true;
#else
    private bool mRealTimeUpdate = false;
#endif

    public static string _FOG_BETTER_ON = "_FOG_BETTER_ON";
    public static string _FOG_BETTER_SINGLE_ON = "_FOG_BETTER_SINGLE_ON";

    [Header("开关")]
    public bool mEnableFog = false;

    [Header("颜色")]
    [Space(10)]
    [ColorUsage(true, true)]
    public Color mFogDistColor = new Color(1, 1, 1, 1);
    [ColorUsage(true, true)]
    public Color mFogHeightColor = new Color(1, 1, 1, 1);

    [Header("扰动")]
    [Space(10)]
    public Texture2D mFogDisturbTex;
    public float mFogDisturbIntensity = 0.1f;
    [Range(-2, 2)]
    public float mFogDisturbSpeedX = 0.3f;
    [Range(-2, 2)]
    public float mFogDisturbSpeedY = 0.8f;

    [Header("距离雾")]
    [Space(10)]
    [Range(0, 10)]
    public float mFogDistDensity = 1;
    public float mFogDistIntensity = 1;

    [Header("高度雾")]
    [Space(10)]
    [Range(0, 50)]
    public float mFogHeightDensity = 1;
    [Range(0, 10)]
    public float mFogHeightDistDensity = 0;
    public float mFogHeightIntensity = 1;
    public float mFogHeightBase = 0;

    [Header("整体偏移")]
    public float mFogDistOffset = 0;

    private int mFogDistColorID;
    private int mFogHeightColorID;

    private int mFogDisturbTexID;
    private int mFogDisturbIntensityID;
    private int mFogDisturbSpeedXID;
    private int mFogDisturbSpeedYID;

    private int mFogDistDensityID;
    private int mFogDistIntensityID;

    private int mFogHeightDensityID;
    private int mFogHeightDistDensityID;
    private int mFogHeightIntensityID;
    private int mFogHeightBaseID;

    private int mFogDistOffsetID;
    void Start()
    {
        mFogDistColorID = Shader.PropertyToID("_FogDistColor");
        mFogHeightColorID = Shader.PropertyToID("_FogHeightColor");

        mFogDisturbTexID = Shader.PropertyToID("_FogDisturbTex");
        mFogDisturbIntensityID = Shader.PropertyToID("_FogDisturbIntensity");
        mFogDisturbSpeedXID = Shader.PropertyToID("_FogDisturbSpeedX");
        mFogDisturbSpeedYID = Shader.PropertyToID("_FogDisturbSpeedY");

        mFogDistDensityID = Shader.PropertyToID("_FogDistDensity");
        mFogDistIntensityID = Shader.PropertyToID("_FogDistIntensity");

        mFogHeightDensityID = Shader.PropertyToID("_FogHeightDensity");
        mFogHeightDistDensityID = Shader.PropertyToID("_FogHeightDistDensity");
        mFogHeightIntensityID = Shader.PropertyToID("_FogHeightIntensity");
        mFogHeightBaseID = Shader.PropertyToID("_FogHeightBase");

        mFogDistOffsetID = Shader.PropertyToID("_FogDistOffset");

        SetFog();
    }

    private void OnDestroy()
    {
        Shader.DisableKeyword(_FOG_BETTER_ON);
    }

    void SetFog()
    {
        if(mEnableFog)
        {
            Shader.EnableKeyword(_FOG_BETTER_ON);
        }
        else
        {
            Shader.DisableKeyword(_FOG_BETTER_ON);
        }

        Shader.SetGlobalColor(mFogDistColorID, mFogDistColor);
        Shader.SetGlobalColor(mFogHeightColorID, mFogHeightColor);

        Shader.SetGlobalTexture(mFogDisturbTexID, mFogDisturbTex);
        Shader.SetGlobalFloat(mFogDisturbIntensityID, mFogDisturbIntensity);
        Shader.SetGlobalFloat(mFogDisturbSpeedXID, mFogDisturbSpeedX);
        Shader.SetGlobalFloat(mFogDisturbSpeedYID, mFogDisturbSpeedY);

        Shader.SetGlobalFloat(mFogDistDensityID, mFogDistDensity);
        Shader.SetGlobalFloat(mFogDistIntensityID, mFogDistIntensity);

        Shader.SetGlobalFloat(mFogHeightDensityID, mFogHeightDensity);
        Shader.SetGlobalFloat(mFogHeightDistDensityID, mFogHeightDistDensity);
        Shader.SetGlobalFloat(mFogHeightIntensityID, mFogHeightIntensity);
        Shader.SetGlobalFloat(mFogHeightBaseID, mFogHeightBase);

        Shader.SetGlobalFloat(mFogDistOffsetID, mFogDistOffset);
    }

    void Update()
    {
        if (!mRealTimeUpdate)
        {
            return;
        }

        SetFog();
    }
}
