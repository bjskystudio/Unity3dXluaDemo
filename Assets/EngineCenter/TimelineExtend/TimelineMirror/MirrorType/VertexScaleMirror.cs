using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    [ExecuteInEditMode]
    public class VertexScaleMirror : BaseMirror
    {
        private MeshFilter mMeshFilter;
        private SkinnedMeshRenderer mSMR;
        private MeshRenderer mMR;
        private MeshFilter mMF;
        private Mesh mOldMesh;
        private bool mIsInit;

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
            MirrorData mirrorData = GetComponent<MirrorData>();
            if(!mIsInit)
            {
                mIsInit = true;
                mSMR = GetComponent<SkinnedMeshRenderer>();
                if(mSMR != null)
                {
                    mOldMesh = mSMR.sharedMesh;
                }
                else
                {
                    mMR = GetComponent<MeshRenderer>();
                    mMF = GetComponent<MeshFilter>();
                    mOldMesh = mMF.sharedMesh;
                }
            }

            if(mSMR != null)
            {
                if (mIsMirror)
                {
                    mSMR.sharedMesh = Scale(mOldMesh, mirrorData.mScaleType);
                }
                else
                {
                    mSMR.sharedMesh = mOldMesh;
                }
            }
            else if(mMR != null)
            {
                if (mIsMirror)
                {
                    mMF.sharedMesh = Scale(mOldMesh, mirrorData.mScaleType);
                }
                else
                {
                    mMF.sharedMesh = mOldMesh;
                }
            }
        }

        Mesh Scale(Mesh mesh, ScaleType scaleType)
        {
            if (mesh == null)
            {
                return null;
            }

            Mesh newMesh = new Mesh();
            newMesh.Clear();

            Vector3[] newVertices = mesh.vertices;
            switch (scaleType)
            {
                case ScaleType.eX:
                    {
                        for(int i = 0; i < newVertices.Length; i++)
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

            int[] oldTriangles = mesh.triangles;
            int[] newTriangles = new int[mesh.triangles.Length];
            for(int i = 0; i < newTriangles.Length; i++)
            {
                newTriangles[newTriangles.Length - i - 1] = oldTriangles[i];
            }
            newMesh.triangles = newTriangles;
            newMesh.uv = mesh.uv;
            newMesh.RecalculateNormals();

            return newMesh;
        }
    }
}