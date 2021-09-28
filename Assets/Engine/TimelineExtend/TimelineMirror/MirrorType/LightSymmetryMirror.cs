using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Timeline
{
    public class LightSymmetryMirror : SymmetryMirror
    {
        private Light mLight;

        protected override void Init()
        {
            base.Init();
            mLight = gameObject.GetComponent<Light>();
            if(mLight != null)
            {
                mLight.enabled = false;
            }
        }

        protected override void Destroy()
        {
            if (this == null)
            {
                return;
            }

            base.Destroy();
            if (mLight != null)
            {
                mLight.enabled = true;
            }
        }
    }
}
