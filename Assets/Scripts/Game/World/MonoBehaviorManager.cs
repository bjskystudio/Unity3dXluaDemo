using System.Threading.Tasks;
using UnityEngine;
using YoukiaCore;
using YoukiaCore.Utils;

public class MonoBehaviorManager:MonoBehaviour
{
    CallBack updateDelegate;
    CallBack lateUpdateDelegate;
    CallBack fixedUpdateDelegate;
    public static float lastTime;
    public void Awake()
    {
        MoveSyncUpdate();
        AddUpdateDelegate(RoleControlerManager.Get().Update);//必须保证在 角色移动和相机移动后
    //    AddLateUpdateDelegate(HUDManagerController.GetInstance().Update);//必须保证在 角色移动和相机移动后
    }

    public void Start()
    {
        lastTime = Time.realtimeSinceStartup;
    }
    private async void MoveSyncUpdate()
    {
        while (Application.isPlaying && this)
        {
            MainRolePositionSync.Get().LateUpdate();
            await Task.Delay(200);
        }
    }

    public void OnGUI()
    {
        
    }
    public void Update()
    {
        if(updateDelegate != null)
            updateDelegate.Invoke();
        lastTime = Time.realtimeSinceStartup;
    }
    public void LateUpdate()
    {
        if (lateUpdateDelegate != null)
            lateUpdateDelegate.Invoke();
    }

    public void FixedUpdate()
    {
        if (fixedUpdateDelegate != null)
            fixedUpdateDelegate.Invoke();
    }

    public void AddUpdateDelegate(CallBack inDelegete)
    { 
        updateDelegate += inDelegete;
    }
    public void RemoveUpdateDelegate(CallBack inDelegete)
    {
        updateDelegate -= inDelegete;
    }
    public void AddLateUpdateDelegate(CallBack inDelegete)
    {
        lateUpdateDelegate += inDelegete;
    }
    public void RemoveLateUpdateDelegate(CallBack inDelegete)
    {
        lateUpdateDelegate -= inDelegete;
    }
    public void AddFixedUpdateDelegate(CallBack inDelegete)
    {
        fixedUpdateDelegate += inDelegete;
    }
    public void RemoveFixedUpdateDelegate(CallBack inDelegete)
    {
        fixedUpdateDelegate -= inDelegete;
    } 
    public virtual void OnDrawGizmosSelected()
    {
        
    }
}
#if UNITY_EDITOR

#endif