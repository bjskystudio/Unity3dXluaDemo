using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YoukiaEngine
{
    /// <summary>
    /// 注意:多个几何实例化物体公用相同包围盒时会自动合并相机裁剪,可用于大数量物体时的效率提升
    /// </summary>
    public class InstanceBounds
    {
        public Bounds BoundingBox;

        internal bool IsVisible = false;

        public bool IsVisible1 => IsVisible;

        internal uint CheckCode = 0;

        public InstanceBounds(Bounds boundingBox)
        {
            BoundingBox = boundingBox;
        }

		public bool ImpostorStatus = false;
	    public uint SharedCount = 0;

	    public void ResetSharedBounds()
	    {
		    IsVisible = false;
		    CheckCode = 0;
	    }
    }
}
