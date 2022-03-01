using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    public class ModelSymmetryMirror : SymmetryMirror
    {
        private SkinnedMeshRenderer[] mMeshRenderers = new SkinnedMeshRenderer[0];

        protected override void Init()
        {
            base.Init();
            mMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            if(mMeshRenderers != null)
            {
                for (int i = 0; i < mMeshRenderers.Length; i++)
                {
                    if(mMeshRenderers[i] != null)
                    {
                        mMeshRenderers[i].enabled = false;
                    }
                }
            }

            ScaleMirror.Scale(mSymmetryMirrorData, mIsMirror);
        }

        protected override void Destroy()
        {
            if(this == null)
            {
                return;
            }

            base.Destroy();
            if(mMeshRenderers != null)
            {
                for (int i = 0; i < mMeshRenderers.Length; i++)
                {
                    if(mMeshRenderers[i] != null)
                    {
                        mMeshRenderers[i].enabled = true;
                    }
                }
            }

            ScaleMirror.Scale(mSymmetryMirrorData, mIsMirror);
        }
    }
}
