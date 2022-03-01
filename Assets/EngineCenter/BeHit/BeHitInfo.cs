using System;
using System.Collections;
using System.Collections.Generic;
using ResourceLoad;
using Framework;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
public class BeHitInfo : MonoBehaviour
{
    public enum HitType
    {
        eNone = 0,
        eReplace = 1,
        eAdd = 2,
    }

    //受击得类型
    public HitType mBeHitType = HitType.eNone;
    //受击颜色
    public Color mBeHitColor;
    //受击阈值(叠加类型)
    public float mBeHitThreshodAdd;
    //受击阈值(替换类型)
    public float mBeHitThreshodReaplace;
    //受击强度
    public float mBeHitStrength;
    //受击边缘光颜色
    public Color mBeHitRimColor;
    //受击边缘光阈值
    public float mBeHitRimThreshod;
    //受击边缘光范围
    public float mBeHitRimPower;
    //受击边缘光颜色
    public float mBeHitRimStrength;
    //受击边缘光平滑
    public float mBeHitRimSmooth;
    //Trigger
    public bool mIsTrigger;
    private bool mIsTriggerLast;

    private int mBeHitColorID;
    private int mBeHitThreshodAddID;
    private int mBeHitThreshodReaplaceID;
    private int mBeHitStrengthID;
    private int mBeHitRimColorID;
    private int mBeHitRimThreshodID;
    private int mBeHitRimPowerID;
    private int mBeHitRimStrengthID;
    private int mBeHitRimSmoothID;

    private Animator mAnimator;
    private PlayableGraph mGraph;
    private AnimationClipPlayable mClipPlayable;
    private List<Material> mMaterialList = new List<Material>();
    private ResRef mResRef;
    private string mCurClipPath;

    public void Start()
    {
        if (Application.isPlaying)
        {
            SkinnedMeshRenderer[] smr = GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < smr.Length; i++)
            {
                if (smr[i].material != null)
                {
                    mMaterialList.Add(smr[i].material);
                }
            }

            MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < mr.Length; i++)
            {
                if (mr[i].material != null)
                {
                    mMaterialList.Add(mr[i].material);
                }
            }
        }
        else
        {
#if UNITY_EDITOR
            //不做拷贝材质了，这样回导致在保存prefabd的时候，材质丢失，角色变紫色
            SkinnedMeshRenderer[] smr = GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < smr.Length; i++)
            {
                //Material tempMaterial = new Material(smr[i].sharedMaterial);
                //smr[i].sharedMaterial = tempMaterial;
                if (smr[i].sharedMaterial != null)
                {
                    mMaterialList.Add(smr[i].sharedMaterial);
                }
            }

            MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < mr.Length; i++)
            {
                //Material tempMaterial = new Material(mr[i].sharedMaterial);
                //mr[i].sharedMaterial = tempMaterial;
                if (mr[i].sharedMaterial != null)
                {
                    mMaterialList.Add(mr[i].sharedMaterial);
                }
            }
#endif
        }

        mAnimator = GetComponent<Animator>();
        mBeHitColorID = Shader.PropertyToID("_BeHitColor");
        mBeHitThreshodAddID = Shader.PropertyToID("_BeHitThreshodAdd");
        mBeHitThreshodReaplaceID = Shader.PropertyToID("_BeHitThreshodReplace");
        mBeHitStrengthID = Shader.PropertyToID("_BeHitStrength");
        mBeHitRimColorID = Shader.PropertyToID("_BeHitRimColor");
        mBeHitRimThreshodID = Shader.PropertyToID("_BeHitRimThreshod");
        mBeHitRimPowerID = Shader.PropertyToID("_BeHitRimPower");
        mBeHitRimStrengthID = Shader.PropertyToID("_BeHitRimStrength");
        mBeHitRimSmoothID = Shader.PropertyToID("_BeHitRimSmooth");
    }

    public void OnDestroy()
    {
        if (mGraph.IsValid())
        {
            mGraph.Destroy();
        }

        if (mResRef != null)
        {
            mResRef.Release();
        }
    }

    public void ShowHit(string path)
    {
        SetTrigger(true);
        mCurClipPath = path;


        AssetLoadManager.Instance.LoadAnimationClip(path, (clip, asset) =>
        {
            if (mGraph.IsValid())
            {
                mGraph.Destroy();
            }
            mGraph = PlayableGraph.Create();
            AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(mGraph, "Animation", mAnimator);
            mClipPlayable = AnimationClipPlayable.Create(mGraph, clip);
            playableOutput.SetSourcePlayable(mClipPlayable);
            mGraph.Play();
        });


        //ResourceManager.Instance.LoadAnimationClip(path, (clip, resRef) =>
        //{
        //    if (this == null)
        //    {
        //        resRef.Release();
        //        return;
        //    }

        //    if(mCurClipPath != path)
        //    {
        //        resRef.Release();
        //        return;
        //    }

        //    if (mResRef != null)
        //    {
        //        mResRef.Release();
        //    }

        //    mResRef = resRef;
        //    if (mGraph.IsValid())
        //    {
        //        mGraph.Destroy();
        //    }

        //    mGraph = PlayableGraph.Create();
        //    AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(mGraph, "Animation", mAnimator);
        //    mClipPlayable = AnimationClipPlayable.Create(mGraph, clip);
        //    playableOutput.SetSourcePlayable(mClipPlayable);
        //    mGraph.Play();
        //});
    }

    public void ResetHit()
    {
        SetTrigger(false);
        mBeHitType = HitType.eNone;
        for (int i = 0; i < mMaterialList.Count; i++)
        {
            mMaterialList[i].DisableKeyword("BEHITREPLACE");
            mMaterialList[i].DisableKeyword("BEHIT_RIM");

            mMaterialList[i].SetColor(mBeHitColorID, Color.white);
            mMaterialList[i].SetFloat(mBeHitThreshodAddID, 0);
            mMaterialList[i].SetFloat(mBeHitThreshodReaplaceID, 0);
            mMaterialList[i].SetFloat(mBeHitStrengthID, 1);

            mMaterialList[i].SetColor(mBeHitRimColorID, Color.black);
            mMaterialList[i].SetFloat(mBeHitRimThreshodID, 1);
            mMaterialList[i].SetFloat(mBeHitRimPowerID, 8);
            mMaterialList[i].SetFloat(mBeHitRimStrengthID, 0);
            mMaterialList[i].SetFloat(mBeHitRimSmoothID, 0);
        }
    }

    public void CopyBeHitInfo(BeHitInfo beHitInfo)
    {
        if (beHitInfo != null)
        {
            mBeHitType = beHitInfo.mBeHitType;
            mBeHitColor = beHitInfo.mBeHitColor;
            mBeHitThreshodAdd = beHitInfo.mBeHitThreshodAdd;
            mBeHitThreshodReaplace = beHitInfo.mBeHitThreshodReaplace;
            mBeHitStrength = beHitInfo.mBeHitStrength;
            mBeHitRimColor = beHitInfo.mBeHitRimColor;
            mBeHitRimThreshod = beHitInfo.mBeHitRimThreshod;
            mBeHitRimPower = beHitInfo.mBeHitRimPower;
            mBeHitRimStrength = beHitInfo.mBeHitRimStrength;
            mBeHitRimSmooth = beHitInfo.mBeHitRimSmooth;
            mIsTrigger = beHitInfo.mIsTrigger;
        }
    }

    public void SetTrigger(bool isTrigger)
    {
        mIsTrigger = isTrigger;
    }

    public void Update()
    {
        if (mIsTrigger)
        {
            mIsTriggerLast = mIsTrigger;
            switch (mBeHitType)
            {
                case HitType.eAdd:
                    {
                        //叠加模式
                        for (int i = 0; i < mMaterialList.Count; i++)
                        {
                            mMaterialList[i].DisableKeyword("BEHITREPLACE");
                            mMaterialList[i].EnableKeyword("BEHIT_RIM");

                            mMaterialList[i].SetColor(mBeHitColorID, mBeHitColor);
                            mMaterialList[i].SetFloat(mBeHitThreshodAddID, mBeHitThreshodAdd);
                            mMaterialList[i].SetFloat(mBeHitStrengthID, mBeHitStrength);

                            mMaterialList[i].SetColor(mBeHitRimColorID, mBeHitRimColor);
                            mMaterialList[i].SetFloat(mBeHitRimThreshodID, mBeHitRimThreshod);
                            mMaterialList[i].SetFloat(mBeHitRimPowerID, mBeHitRimPower);
                            mMaterialList[i].SetFloat(mBeHitRimStrengthID, mBeHitRimStrength);
                            mMaterialList[i].SetFloat(mBeHitRimSmoothID, mBeHitRimSmooth);
                        }
                    }
                    break;
                case HitType.eReplace:
                    {
                        //替换模式
                        for (int i = 0; i < mMaterialList.Count; i++)
                        {
                            mMaterialList[i].EnableKeyword("BEHITREPLACE");
                            mMaterialList[i].DisableKeyword("BEHIT_RIM");

                            mMaterialList[i].SetColor(mBeHitColorID, mBeHitColor);
                            mMaterialList[i].SetFloat(mBeHitThreshodReaplaceID, mBeHitThreshodReaplace);
                            mMaterialList[i].SetFloat(mBeHitStrengthID, mBeHitStrength);

                            mMaterialList[i].SetColor(mBeHitRimColorID, mBeHitRimColor);
                            mMaterialList[i].SetFloat(mBeHitRimThreshodID, mBeHitRimThreshod);
                            mMaterialList[i].SetFloat(mBeHitRimPowerID, mBeHitRimPower);
                            mMaterialList[i].SetFloat(mBeHitRimStrengthID, mBeHitRimStrength);
                            mMaterialList[i].SetFloat(mBeHitRimSmoothID, mBeHitRimSmooth);
                        }
                    }
                    break;
            }
        }
        else
        {
            if (mIsTrigger != mIsTriggerLast)
            {
                mIsTriggerLast = mIsTrigger;
                ResetHit();
            }
        }
    }
}
