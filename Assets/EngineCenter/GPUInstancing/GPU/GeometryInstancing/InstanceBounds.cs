using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YoukiaEngine
{
    /// <summary>
    /// ע��:�������ʵ�������幫����ͬ��Χ��ʱ���Զ��ϲ�����ü�,�����ڴ���������ʱ��Ч������
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
