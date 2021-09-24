using ResourceLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class HScriptableObject : HRes
    {
        public HScriptableObject()
        {
        }

        public override Type GetRealType()
        {
            return typeof(UnityEngine.ScriptableObject);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".asset" };
        }
#endif
    }
}
