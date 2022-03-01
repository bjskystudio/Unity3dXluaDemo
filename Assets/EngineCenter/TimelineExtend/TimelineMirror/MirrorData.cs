using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    public enum ScaleType
    {
        eNone,
        eX,
        eY,
        eZ,
    }

    [System.Serializable]
    public class MirrorDataConfig
    {
        public MirrorType mMirrorType = MirrorType.eNone;
        public Vector3 mPlaneNormal = new Vector3(1, 0, 0);
        public Vector3 mPlanePoint = new Vector3(0, 0, 0);
        public ScaleType mScaleType;
    }

    [System.Serializable]
    public class MirrorData : MonoBehaviour
    {
        public MirrorType mMirrorType = MirrorType.eNone;
        public Vector3 mPlaneNormal = new Vector3(1, 0, 0);
        public Vector3 mPlanePoint = new Vector3(0, 0, 0);
        public ScaleType mScaleType;

        [System.Serializable]
        public class ScaleInfo
        {
            public GameObject mObj;
            public ScaleType mScaleType;
            public Mesh mMesh;
        }

        public List<ScaleInfo> mSubObjScaleList = new List<ScaleInfo>();

#if UNITY_EDITOR
        public bool mIsPreview;
        public List<BaseMirror> mMirrorList = new List<BaseMirror>();
#endif

#if UNITY_EDITOR
        public void StartPreview()
        {
            mMirrorList = MirrorUtility.AddMirrorComponent(gameObject);
            MirrorUtility.SwitchMirror(mMirrorList, true);
        }

        public void CancelPreview()
        {
            MirrorUtility.SwitchMirror(mMirrorList, false);
        }
#endif

        //copy都不拷贝子对象的 mSubObjScaleList，子对象使用顶点缩放的方式，所以是在模型空间下操作的，可以固定死，不用随timeline变动
        public static void Copy(MirrorDataConfig data1, MirrorData data2)
        {
            data1.mMirrorType = data2.mMirrorType;
            data1.mPlaneNormal = data2.mPlaneNormal;
            data1.mPlanePoint = data2.mPlanePoint;
            data1.mScaleType = data2.mScaleType;
        }

        public static void Copy(MirrorData data1, MirrorDataConfig data2)
        {
            data1.mMirrorType = data2.mMirrorType;
            data1.mPlaneNormal = data2.mPlaneNormal;
            data1.mPlanePoint = data2.mPlanePoint;
            data1.mScaleType = data2.mScaleType;
        }
    }
}
