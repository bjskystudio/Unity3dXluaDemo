using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class HSprite : HRes
    {
        public HSprite()
        {
        }

        public override Type GetRealType()
        {
            return typeof(Sprite);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".png" };
        }
#endif
    }
}
