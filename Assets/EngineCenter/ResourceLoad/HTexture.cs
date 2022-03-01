
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class HTexture : HRes
    {
        public HTexture() 
        {
        }

        public override Type GetRealType()
        {
            return typeof(Texture2D);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".png", ".psd", ".jpg", ".jpeg", ".bmp"};
        }
#endif
    }
}
