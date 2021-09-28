using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    [ExecuteInEditMode]
    public class ScaleMirror : BaseMirror
    {
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
                    Init();
                }
            }
        }

        protected override void Init()
        {
            if(this == null)
            {
                return;
            }

            MirrorData mirrorData = GetComponent<MirrorData>();
            Scale(mirrorData, mIsMirror);
        }

        public static void Scale(MirrorData mirrorData, bool isMirror)
        {
            if(mirrorData != null)
            {
                ScaleByTrans(mirrorData.transform, mirrorData.mScaleType);
                for (int i = 0; i < mirrorData.mSubObjScaleList.Count; i++)
                {
                    ScaleByVertex(mirrorData.mSubObjScaleList[i], isMirror);
                }
            }
        }

        private static void ScaleByVertex(MirrorData.ScaleInfo scaleInfo, bool isMirror)
        {
            GameObject obj = scaleInfo.mObj;
            ScaleType scaleType = scaleInfo.mScaleType;
            SkinnedMeshRenderer smr = obj.GetComponent<SkinnedMeshRenderer>();
            MeshRenderer mr = obj.GetComponent<MeshRenderer>();
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            Mesh oldMesh = null;
            if (smr != null)
            {
                oldMesh = smr.sharedMesh;
            }
            else
            {
                oldMesh = mf.sharedMesh;
            }

            if(isMirror)
            {
                Mesh newMesh = new Mesh();
                newMesh.Clear();

                Vector3[] newVertices = oldMesh.vertices;
                switch (scaleType)
                {
                    case ScaleType.eX:
                        {
                            for (int i = 0; i < newVertices.Length; i++)
                            {
                                newVertices[i].x = newVertices[i].x * -1;
                            }
                        }
                        break;
                    case ScaleType.eY:
                        {
                            for (int i = 0; i < newVertices.Length; i++)
                            {
                                newVertices[i].y = newVertices[i].y * -1;
                            }
                        }
                        break;
                    case ScaleType.eZ:
                        {
                            for (int i = 0; i < newVertices.Length; i++)
                            {
                                newVertices[i].z = newVertices[i].z * -1;
                            }
                        }
                        break;
                }
                newMesh.vertices = newVertices;

                int[] oldTriangles = oldMesh.triangles;
                int[] newTriangles = new int[oldMesh.triangles.Length];
                for (int i = 0; i < newTriangles.Length; i++)
                {
                    newTriangles[newTriangles.Length - i - 1] = oldTriangles[i];
                }
                newMesh.triangles = newTriangles;
                newMesh.uv = oldMesh.uv;
                newMesh.boneWeights = oldMesh.boneWeights;
                newMesh.bindposes = oldMesh.bindposes;
                newMesh.RecalculateNormals();

                if (smr != null)
                {
                    smr.sharedMesh = newMesh;
                }
                else if (mr != null)
                {
                    mf.sharedMesh = newMesh;
                }
            }
            else
            {
                if (smr != null)
                {
                    smr.sharedMesh = scaleInfo.mMesh;
                }
                else if (mr != null)
                {
                    mf.sharedMesh = scaleInfo.mMesh;
                }
            }
           
        }

        private static void ScaleByTrans(Transform trans, ScaleType scaleType)
        {
            if (trans == null)
            {
                return;
            }

            switch (scaleType)
            {
                case ScaleType.eX:
                    {
                        Vector3 scale = trans.localScale;
                        trans.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    }
                    break;
                case ScaleType.eY:
                    {
                        Vector3 scale = trans.localScale;
                        trans.localScale = new Vector3(scale.x, -scale.y, scale.z);
                    }
                    break;
                case ScaleType.eZ:
                    {
                        Vector3 scale = trans.localScale;
                        trans.localScale = new Vector3(scale.x, scale.y, -scale.z);
                    }
                    break;
            }
        }
    }
}
