using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ResourceLoad
{
    public class HAnimationClip : HRes
    {
        public HAnimationClip()
        { 
        }

        public override Type GetRealType()
        {
            return typeof(AnimationClip);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".anim" };
        }
#endif
    }
}
