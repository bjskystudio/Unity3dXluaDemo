using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;

namespace EngineCenter.Timeline
{
    public class CameraSymmetryMirror : SymmetryMirror
    {
        private CinemachineVirtualCamera mVirtualCamera;
        private CinemachineVirtualCamera mSymmetryVirtualCamera;

        protected override void Init()
        {
            base.Init();
            mVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
            if(mVirtualCamera != null)
            {
                mVirtualCamera.enabled = false;
            }

            mSymmetryVirtualCamera = mSymmetryObj.GetComponent<CinemachineVirtualCamera>();

            Component[] components = mSymmetryObj.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                Type type = components[i].GetType();
                string typeName = type.Name;
                if (typeName != typeof(Transform).Name &&
                    typeName != typeof(CinemachineVirtualCamera).Name
                    )
                {
                    GameObject.DestroyImmediate(components[i]);
                }
            }
        }

        protected override void Destroy()
        {
            if (this == null)
            {
                return;
            }

            base.Destroy();
            if (mVirtualCamera != null)
            {
                mVirtualCamera.enabled = true;
            }
        }

        protected override void LateUpdate() 
        {
            base.LateUpdate();
            CopyComponent(mVirtualCamera, mSymmetryVirtualCamera);
        }

    }
}
