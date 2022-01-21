using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouseHit : MonoBehaviour
{
    Camera useCamera;
    Ray cameraRay;
    Vector3 mousePos = new Vector3();
    RaycastHit cameraHit;

    void Start()
    {
        useCamera = CSTianMagicCityManager.Instance.TianMagicCityRoot.transform.Find("Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (CSTianMagicCityManager.Instance.LockClick)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;
            mousePos.z = 0;
            cameraRay = useCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(cameraRay, out cameraHit, 10))
            {
                GameObject go = cameraHit.transform.gameObject;
                if (go.name.Contains("Grid"))
                {
                    int gridNum = Convert.ToInt32(go.name.Replace("Grid", ""));
                    CSEventToLuaHelp.BroadcastLua("TMCOnRayHit", gridNum);
                }
            }
        }
    }
}
