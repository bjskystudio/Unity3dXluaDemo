using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CreateCinemachline : Editor
{
    [MenuItem("GameObject/创建虚拟相机", priority = 0)]
    static void Create()
    {
        GameObject obj = new GameObject("CM vcam");
        obj.AddComponent<CinemachineVirtualCamera>();
        Selection.activeObject = obj;
    }
}
