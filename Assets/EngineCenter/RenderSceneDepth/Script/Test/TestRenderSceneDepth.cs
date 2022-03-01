using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRenderSceneDepth : MonoBehaviour
{
    public RenderSceneDepth mRenderSceneDepth;
    public GameObject mTargetObj;
    public GameObject mObj;
    public Texture2D mSceneDepthTex;

    // Start is called before the first frame update
    void Start()
    {
        mSceneDepthTex = mRenderSceneDepth.GetSceneDepthTex();
    }

    // Update is called once per frame
    void Update()
    {
        float height = mRenderSceneDepth.GetDepth(mSceneDepthTex, mTargetObj.transform.position);
        mObj.transform.position = new Vector3(mTargetObj.transform.position.x, height, mTargetObj.transform.position.z);
    }
}
