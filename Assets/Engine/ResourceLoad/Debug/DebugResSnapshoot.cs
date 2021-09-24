using ResourceLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugResSnapshoot : MonoBehaviour
{
    public bool mStartSnapshoot;
    public float mUpdateIntervalTime = 3;
    public List<string> mIncreaseList = new List<string>();
    public List<string> mReduceList = new List<string>();
    private bool mPreStartSnapshoot;
    private float mUpdateStartTime = 0;
    private bool mStartUpdate;
    private Dictionary<string, string> mSnapshootResMap = new Dictionary<string, string>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SnapshootRes()
    {
        Dictionary<string, HRes> resMap = ResourceManager.Instance.mResMap;
        mSnapshootResMap.Clear();
        foreach (var item in resMap)
        {
            mSnapshootResMap.Add(item.Key, item.Key);
        }
    }

    private void UpdateResInfo()
    {
        Dictionary<string, HRes> resMap = ResourceManager.Instance.mResMap;
        mIncreaseList.Clear();
        foreach (var item in resMap)
        {
            if(!mSnapshootResMap.ContainsKey(item.Key))
            {
                mIncreaseList.Add(item.Key);
            }
        }

        mReduceList.Clear();
        foreach (var item in mSnapshootResMap)
        {
            if (!resMap.ContainsKey(item.Key))
            {
                mReduceList.Add(item.Key);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mStartSnapshoot != mPreStartSnapshoot)
        {         
            mPreStartSnapshoot = mStartSnapshoot;
            if(mStartSnapshoot)
            {
                SnapshootRes();
            }
        }
        
        if(mStartSnapshoot)
        {
            if (Time.realtimeSinceStartup - mUpdateStartTime > mUpdateIntervalTime)
            {
                mUpdateStartTime = Time.realtimeSinceStartup;
                UpdateResInfo();
            }
        }
    }
}
