using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    [ExecuteInEditMode]
    public class SymmetryMirror : BaseMirror
    {
        protected Vector3 mPlaneNormal;
        protected Vector3 mPlanePoint;
        [HideInInspector]public GameObject mSymmetryObj;
        protected MirrorData mMirrorData;
        protected MirrorData mSymmetryMirrorData;

        public override bool IsMirror
        { 
            get
            {
                return mIsMirror;
            }

            set
            {
                if (mIsMirror != value)
                {
                    mIsMirror = value;
                    if (mIsMirror)
                    {
                        Init();
                    }
                    else
                    {
                        Destroy();
                    }
                }
            }
        }

        // Start is called before the first frame update
        protected override void Init()
        {
            mMirrorData = GetComponent<MirrorData>();
            if(mMirrorData != null)
            {
                mPlaneNormal = mMirrorData.mPlaneNormal;
                mPlanePoint = mMirrorData.mPlanePoint;       

                mSymmetryObj = GameObject.Instantiate(gameObject);
                SymmetryMirror symmetryMirror = mSymmetryObj.GetComponent<SymmetryMirror>();
                if (symmetryMirror != null)
                {
                    GameObject.DestroyImmediate(symmetryMirror);
                }

                mSymmetryMirrorData = mSymmetryObj.GetComponent<MirrorData>();
                mSymmetryObj.transform.parent = transform.parent;
                mSymmetryObj.transform.localScale = transform.localScale;
            }
            else
            {
                Debug.LogError("error, symmetryMirror not find mirrorData!!");
            }
        }

        protected override void Destroy()
        {
            if (mSymmetryObj != null)
            {
                GameObject.DestroyImmediate(mSymmetryObj);
            }
        }

        public void OnDestroy()
        {
            Destroy();
        }

        protected virtual void LateUpdate()
        {
            if(IsMirror)
            {
                if (mMirrorData != null)
                {
                    mPlaneNormal = mMirrorData.mPlaneNormal;
                    mPlanePoint = mMirrorData.mPlanePoint;
                }

                if (mSymmetryObj != null)
                {
                    Vector3 p1 = GetPlaneSymmetryPoint(transform.position, mPlaneNormal, mPlanePoint);
                    Vector3 p2 = GetPlaneSymmetryPoint(transform.position + transform.forward, mPlaneNormal, mPlanePoint);
                    mSymmetryObj.transform.position = p1;
                    mSymmetryObj.transform.forward = p2 - p1;
                }
            }
        }

        Vector3 GetPlaneSymmetryPoint(Vector3 point, Vector3 planeNormal, Vector3 planePoint)
        {
            float t1 = Vector3.Dot(point, planeNormal) - Vector3.Dot(planePoint, planeNormal);
            float t2 = Mathf.Sqrt(Vector3.Dot(planeNormal, planeNormal));
            float distance = t1 / t2;
            return point - planeNormal.normalized * distance * 2;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Matrix4x4 m = Matrix4x4.TRS(mPlanePoint, Quaternion.LookRotation(mPlaneNormal), new Vector3(5, 5, 0.2f));
            Gizmos.matrix = m;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
#endif
    }

}