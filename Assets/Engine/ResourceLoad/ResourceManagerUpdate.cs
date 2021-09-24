using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    public class ResourceManagerUpdate : MonoBehaviour
    {
        void Update()
        {
            ResourceManager.Instance.Update();
        }
    }
}
