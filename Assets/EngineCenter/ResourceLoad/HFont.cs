using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ResourceLoad
{
    public class HFont : HRes
    {
        public HFont()
        {
        }

        public override Type GetRealType()
        {
            return typeof(Font);
        }

#if UNITY_EDITOR

        public override List<string> GetExtesions()
        {
            return new List<string>() { ".ttf", ".fontsettings" };
        }
#endif
    }
}
