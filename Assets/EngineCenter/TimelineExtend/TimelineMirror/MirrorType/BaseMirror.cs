using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    public enum MirrorType
    {
        eNone,
        eCameraSymmetryMirror,
        eLightSymmetryMirror,
        eModelSymmetryMirror,
        eScaleMirror,
        eEffectSymmetryMirror,
    }
        

    public class BaseMirror : MonoBehaviour
    {
        protected bool mIsMirror;
        public virtual bool IsMirror
        {
            get
            {
                return mIsMirror;
            }

            set
            {
                mIsMirror = value;
            }
        }


        public virtual void Reset()
        {
            IsMirror = false;
        }

        protected virtual void Init()
        {

        }

        protected virtual void Destroy()
        {

        }

        protected void CopyComponent(Component original, Component target)
        {
            if (original == null || target == null)
            {
                return;
            }

            System.Type type = original.GetType();
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if(!field.IsStatic)
                {
                    field.SetValue(target, field.GetValue(original));
                }

            }
        }
    }
}
