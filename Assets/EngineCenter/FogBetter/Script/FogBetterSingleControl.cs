using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FogBetterSingleControl : MonoBehaviour
{
    public Material mMaterial;

    private static FogBetterMgr mFogMgr;
    private static FogBetterMgr FogMgr
    {
        get
        {
            if(mFogMgr == null)
            {
                mFogMgr = GameObject.FindObjectOfType<FogBetterMgr>();
            }

            if(mFogMgr != null)
            {
                return mFogMgr;
            }
            else
            {
                Debug.LogError("错误！！没有找到FogBetterControl,请添加一个！");
                return null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if(mr != null)
        {
            mMaterial = mr.sharedMaterial;
        }

        AddControl();
    }

    private void OnEnable()
    {
        AddControl();
    }

    private void OnDisable()
    {
        RemoveControl();
    }

    private void OnDestroy()
    {
        RemoveControl();
    }

    private void AddControl()
    {
        if (mMaterial != null)
        {
            mMaterial.EnableKeyword(FogBetterMgr._FOG_BETTER_SINGLE_ON);
        }
    }

    private void RemoveControl()
    {
        if (mMaterial != null)
        {
            mMaterial.DisableKeyword(FogBetterMgr._FOG_BETTER_SINGLE_ON);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
