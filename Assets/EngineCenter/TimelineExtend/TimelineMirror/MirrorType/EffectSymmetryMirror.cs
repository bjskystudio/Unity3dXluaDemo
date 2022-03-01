using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    public class EffectSymmetryMirror : SymmetryMirror
    {
        protected override void Init()
        {
            base.Init();
            if (transform != null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform trans = transform.GetChild(i);
                    trans.gameObject.SetActive(false);
                }
            }
            ScaleMirror.Scale(mSymmetryMirrorData, mIsMirror);
        }

        protected override void Destroy()
        {
            if (this == null)
            {
                return;
            }

            base.Destroy();
            if(transform != null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform trans = transform.GetChild(i);
                    trans.gameObject.SetActive(true);
                }
            }
            ScaleMirror.Scale(mSymmetryMirrorData, mIsMirror);
        }
    }
}
