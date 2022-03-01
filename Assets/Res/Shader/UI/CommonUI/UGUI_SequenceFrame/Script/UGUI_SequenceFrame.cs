using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI_SequenceFrame : MonoBehaviour
{
    public int FrameRate = 10;
    public int AtlasWidth;
    public int AtlasHeight;
    public int AtlasFrameCount;

    private float frameTime;
    private int curFrame;
    private float tempTime;
    private float mFrameWidth;
    private float mFrameHeight;

    private Material mat;
    private int mOriginUV_FrameWH_ID;

    private void Start()
    {
        mat = GetComponent<Graphic>().material;
        mOriginUV_FrameWH_ID = Shader.PropertyToID("_OriginUV_FrameWH");

        if (FrameRate == 0 || AtlasWidth == 0 || AtlasHeight == 0)
        {
            return;
        }
        frameTime = 1.0f / FrameRate;
        if(AtlasFrameCount == 0)
        {
            AtlasFrameCount = AtlasWidth * AtlasHeight;
        }       
        mFrameWidth = 1.0f / AtlasWidth;
        mFrameHeight = 1.0f / AtlasHeight;

        SetUV();
    }

    private void Update()
    {
        if(FrameRate == 0 || AtlasWidth == 0 || AtlasHeight == 0)
        {
            return;
        }
#if UNITY_EDITOR
        frameTime = 1.0f / FrameRate;
#endif
        tempTime += Time.deltaTime;
        if(tempTime >= frameTime)
        {
            tempTime -= frameTime;
            curFrame++;
            curFrame %= AtlasFrameCount;
            SetUV();
        }
    }

    private void SetUV()
    {
        if(mat != null)
        {
            float row = curFrame / AtlasWidth;
            float col = curFrame % AtlasWidth;

            float frameU = col * mFrameWidth;
            float frameV = 1.0f - row * mFrameHeight - mFrameHeight;

            mat.SetVector(mOriginUV_FrameWH_ID, new Vector4(frameU, frameV, mFrameWidth, mFrameHeight));
        }
    }
}
