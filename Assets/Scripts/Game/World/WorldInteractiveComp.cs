using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using XLua;

// [LuaCallCSharp]
public class WorldInteractiveEvent : UnityEvent<Transform, Transform> { }

// [LuaCallCSharp]
public enum InteractiveColliderType
{
    // 无
    None = 0,
    // 球形
    Sphere = 1,
    // 盒子
    Box = 2,
    // 胶囊
    Capsule = 3
}

// [LuaCallCSharp]
public class WorldInteractiveComp : MonoBehaviour
{
    // 触发器名字
    public string TriggerName = "";

    private InteractiveColliderType OldColliderType = InteractiveColliderType.None;
    // 触发器类型
    public InteractiveColliderType ColliderType = InteractiveColliderType.None;
    private Collider ColliderItem = null;

    // 半径 （Sphere、Capsule）
    public int radius = 0;
    // 高度 （Capsule）
    public int height = 0;
    // 垂直方向 （Capsule）
    public int direction = 1;
    // 触发器盒子
    public Vector3 size = Vector3.zero;

    // 点击盒子
    public Vector3 TouchBox = Vector3.zero;

    public WorldInteractiveEvent OnTriggerEnterCall = new WorldInteractiveEvent();
    public WorldInteractiveEvent OnTriggerStayCall = new WorldInteractiveEvent();
    public WorldInteractiveEvent OnTriggerExitCall = new WorldInteractiveEvent();
    public WorldInteractiveEvent OnTouchTriggerCall = new WorldInteractiveEvent();

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.SetTouchTrigger();
        this.CreateCollider();
        this.ResetCollider();
    }

    // 设定点击事件相关
    private void SetTouchTrigger()
    {
        if (this.TouchBox != Vector3.zero && this.ColliderType == InteractiveColliderType.Box)
        {
            this.size = this.TouchBox;
        }
        if ((this.TouchBox != Vector3.zero) && (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1)))
        {
            this.ColliderType = InteractiveColliderType.Box;

            this.OnClick();
        }
    }
    protected void OnClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject != this.gameObject) { continue; }

                this.OnTouchTriggerCall.Invoke(this.transform, Camera.main.transform);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.OnTriggerEnterCall.Invoke(this.transform, other.transform);
    }
    private void OnTriggerStay(Collider other)
    {
        this.OnTriggerStayCall.Invoke(this.transform, other.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        this.OnTriggerExitCall.Invoke(this.transform, other.transform);
    }

    private void CreateCollider()
    {
        if (this.OldColliderType == this.ColliderType) { return; }
        this.OldColliderType = this.ColliderType;

        switch (this.ColliderType)
        {
            case InteractiveColliderType.Box:
                this.ClearCollider();
                this.CreateBoxCollider();
                break;
            case InteractiveColliderType.Sphere:
                this.ClearCollider();
                this.CreateSphereCollider();
                break;
            case InteractiveColliderType.Capsule:
                this.ClearCollider();
                this.CreateCapsuleCollider();
                break;
            case InteractiveColliderType.None:
            default:
                this.ClearCollider();
                break;
        }
    }

    private void ResetCollider()
    {
        if (this.ColliderItem == null) { return; }

        switch (this.ColliderType)
        {
            case InteractiveColliderType.Box:
                this.ResetBoxCollider();
                break;
            case InteractiveColliderType.Sphere:
                this.ResetSphereCollider();
                break;
            case InteractiveColliderType.Capsule:
                this.ResetCapsuleCollider();
                break;
            case InteractiveColliderType.None:
            default:
                this.ClearCollider();
                break;
        }
    }

    private void ResetBoxCollider()
    {
        BoxCollider tBoxCollider = this.ColliderItem as BoxCollider;
        if (tBoxCollider.size != this.size)
        {
            tBoxCollider.size = this.size;
            Vector3 tCenterVec = tBoxCollider.center;
            tCenterVec.Set(tCenterVec.x, (this.size.y / 2), tCenterVec.z);
            tBoxCollider.center = tCenterVec;
        }
    }
    private void CreateBoxCollider()
    {
        BoxCollider tBoxCollider = this.gameObject.AddComponent<BoxCollider>();
        this.ColliderItem = tBoxCollider;
        this.ColliderItem.isTrigger = true;
    }

    private void ResetSphereCollider()
    {
        SphereCollider tSphereCollider = this.ColliderItem as SphereCollider;
        if (tSphereCollider.radius != this.radius)
        {
            tSphereCollider.radius = this.radius;
        }
    }
    private void CreateSphereCollider()
    {
        SphereCollider tSphereCollider = this.gameObject.AddComponent<SphereCollider>();
        this.ColliderItem = tSphereCollider;
        this.ColliderItem.isTrigger = true;
    }

    private void ResetCapsuleCollider()
    {
        CapsuleCollider tCapsuleCollider = this.ColliderItem as CapsuleCollider;
        if (tCapsuleCollider.radius != this.radius)
        {
            tCapsuleCollider.radius = this.radius;
        }
        if (tCapsuleCollider.height != this.height)
        {
            tCapsuleCollider.height = this.height;
            Vector3 tCenterVec = tCapsuleCollider.center;
            tCenterVec.Set(tCenterVec.x, (this.size.y / 2), tCenterVec.z);
            tCapsuleCollider.center = tCenterVec;
        }
        if (tCapsuleCollider.direction != this.direction)
        {
            tCapsuleCollider.direction = this.direction;
        }
    }
    private void CreateCapsuleCollider()
    {
        CapsuleCollider tCapsuleCollider = this.gameObject.AddComponent<CapsuleCollider>();
        this.ColliderItem = tCapsuleCollider;
        this.ColliderItem.isTrigger = true;
    }

    private void ClearCollider()
    {
        if (this.ColliderItem == null) { return; }
        UnityEngine.Object.Destroy(this.ColliderItem);
        this.ColliderItem = null;
    }

    private void OnDestroy()
    {
        this.TouchBox = Vector3.zero;
        this.OnTouchTriggerCall.RemoveAllListeners();
        this.OnTriggerEnterCall.RemoveAllListeners();
        this.OnTriggerStayCall.RemoveAllListeners();
        this.OnTriggerExitCall.RemoveAllListeners();
        this.ClearCollider();
    }
}