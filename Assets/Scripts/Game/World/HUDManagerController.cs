using System.Collections.Generic; 
using UnityEngine;

public class HUDManagerController
{
    public static HUDManagerController _instance;

    public static HUDManagerController GetInstance()
    {
        if(_instance==null)
        {
            _instance=new HUDManagerController();
        }
        return _instance;
    }
    public HUDManagerController()
    {
        autouid = int.MinValue;
    }
    private Camera uicamera;
    private int autouid;
    public int AutoUid
    {
        get
        {
            return autouid++;
        } 
    }
    public bool UpdateSwitch=false;
    public bool IsNotUpdatePos=false;
    public Camera Uicamera { 
        get 
        {
            if(uicamera==null)
            {
                uicamera=GameObject.Find("UICamera").GetComponent<Camera>();
            } 
            return uicamera;
        } 
    }

    public Dictionary<int,HUDItemController> HUDItemList=new Dictionary<int, HUDItemController>();
    public void AddHUDItem(int uid, RectTransform rect,Transform target,float offsetx=0f,float offsety=0f)
    {
        rect.transform.SetParent(CameraManager.Get().HudRoot,false);
        HUDItemController item=new HUDItemController(uid,rect,target,offsetx,offsety);
        if(HUDItemList.ContainsKey(uid))
        {
            HUDItemList[uid]=item;
        }
        else
        {
            HUDItemList.Add(uid,item);
        }
    }
    public HUDItemController AddHUDItem(RectTransform rect, Transform target, float offsetx=0f, float offsety=0f)
    {
        rect.transform.SetParent(CameraManager.Get().HudRoot, false);
        int uid = AutoUid;
        HUDItemController item = new HUDItemController(uid, rect, target, offsetx, offsety);
        if (HUDItemList.ContainsKey(uid))
        {
            HUDItemList[uid] = item;
        }
        else
        {
            HUDItemList.Add(uid, item);
        }
        return item;
    }
    public void Remove(int uid)
    {
        if(HUDItemList.ContainsKey(uid))
        {
            HUDItemList.Remove(uid);
        }
    }

    public void Update()
    { 
        //if(UpdateSwitch==false)return;
        //if(IsNotUpdatePos)
        //    return;
        foreach (var item in HUDItemList.Values)
        {
            item.Update();
        }
    }
}
