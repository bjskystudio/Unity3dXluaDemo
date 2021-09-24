using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace ResourceLoad
{
    public class HVideoClip : HRes
    {
        public HVideoClip()
        {
        }

        public override Type GetRealType()
        {
            return typeof(VideoClip);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".mp4", ".avi", ".wmv", ".flv" };
        }
#endif
    }
}
