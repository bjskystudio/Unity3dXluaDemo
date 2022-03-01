using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GPUSceneObjectData
{
    public GameObject mPrefab;
    public Vector3 mPos;
    public Quaternion mRotation;
    public Vector3 mScale;
    public Bounds mBounds;
    public Vector4 mLightScaleOffset;
    public int mLightMapIndex;
}
