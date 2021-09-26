using DG.Tweening;
using Framework;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 拖拽控制器
/// <para>支持：</para>
/// <para> 1、拖拽相关事件。通过ScreenPointToLocalPointInRectangle同步坐标，实时更新拖拽对象的位置；各种拖拽限制</para>
/// <para> 2、点击事件</para>
/// <para> 3、长按事件</para>
/// </summary>
public class AorDrag : MonoBehaviour
{
    protected static GameObject DragTarget;
    /// 当移动中 args: 偏移值x, 偏移值y, 当前位置x, 当前位置y
    /// </summary>
    public Action<float, float, float, float> OnDragEx;
    /// <summary>
    /// 当移动中
    /// </summary>
    public Action<string> OnDrag;
    /// <summary>
    /// 当开始移动
    /// </summary>
    public Action<string> OnDragBegin;
    /// <summary>
    /// 当结束移动
    /// </summary>
    public Action<string> OnDragEnd;
    /// <summary>
    /// 当指针进入
    /// </summary>
    public Action<string> OnEnter;
    /// <summary>
    /// 当指针离开
    /// </summary>
    public Action<string> OnExit;
    /// <summary>
    /// 当点击
    /// </summary>
    public Action<string> OnClick;
    /// <summary>
    /// 当指针按住
    /// </summary>
    public Action<string> OnPress;
    /// <summary>
    /// 当指针按下
    /// </summary>
    public Action<string> OnDown;
    /// <summary>
    /// 当指针不按
    /// </summary>
    public Action<string> OnUp;

    [Label("响应拖拽的RaycasTarget组件")]
    public Graphic GraphicTarget;
    [Label("向下穿透事件的目标")]
    public GameObject NewGraphicTarget;
    [Label("拖拽时的父节点")]
    public Transform ParentRoot;
    [Label("拖拽结束是否归位")]
    public bool DragEndResetPos = false;
    [Label("拖拽像素距离限制")]
    [Range(10f, 100000f)]
    public float LimitDistance = 100000f;
    [Label("拖拽Y坐标限制")]
    public Vector2 LimitTopBottom = new Vector2(-100000f, 100000f);
    [Label("拖拽X坐标限制")]
    public Vector2 LimitLeftRight = new Vector2(-100000f, 100000f);
    [Label("本地或父节点缩放值")]
    public float ScaleValue = 1;
    [Label("是否是克隆拖拽")]
    public bool CloneDrag;
    [Label("纵向拖拽阈值")]
    public float DragingInterval = 20f;
    [Label("是否可以向下移动")]
    public bool MoveDown = true;

    [HideInInspector]
    public RectTransform MoveTarget;
    [HideInInspector]
    public Camera cam;

    /// <summary>
    /// 卡隆拖拽对象
    /// </summary>
    private GameObject cloneDragItem;
    /// <summary>
    /// 开始拖拽的位置
    /// </summary>
    private Vector2 DragBeginPos;
    /// <summary>
    /// 初始父节点
    /// </summary>
    private RectTransform initParentRoot;
    /// <summary>
    /// 初始位置
    /// </summary>
    private int initSiblingIndex;

    /// <summary>
    /// 是否横向滑动
    /// </summary>
    private bool isScrolling = false;
    /// <summary>
    /// 是否纵向拖拽
    /// </summary>
    private bool isDragging = false;
    /// <summary>
    /// 屏幕开始拖拽时初始目标
    /// </summary>
    private Vector2 oldPostion = Vector2.zero;
    /// <summary>
    /// 拖拽对象默认位置
    /// </summary>
    private Vector2 resetPos;
    /// <summary>
    /// 内联参数
    /// </summary>
    private Vector2 tempNewPostion;

    private string dragID;
    /// <summary>
    /// 拖拽标识
    /// </summary>
    public string DragID
    {
        get
        {
            if (string.IsNullOrEmpty(dragID))
            {
                dragID = gameObject.name;
            }
            return dragID;
        }
        set
        {
            dragID = value;
        }
    }

    public float TouchAngle
    {
        get
        {
            return MathEx.AngleXY(Vector2.zero, MoveTarget.anchoredPosition);
        }
    }

    /// <summary>
    /// 是否能拖拽
    /// </summary>
    public bool IsCanDrag;

    public bool IsTouching
    {
        get;
        private set;
    }

    private void Awake()
    {
        cam = UIModel.Inst.UICamera;
        MoveTarget = GetComponent<RectTransform>();
        initParentRoot = transform.parent.GetComponent<RectTransform>();
        initSiblingIndex = transform.GetSiblingIndex();
        var target = EventTriggerListener.Get(gameObject);
        target.onDrag = OnDragHandle;
        target.onDragBegin = OnDragBeginHandle;
        target.onDragEnd = OnDragEndHandle;
        target.onEnter = OnEnterHandle;
        target.onExit = OnExitHandle;
        target.onClick = OnClickHandle;
        target.onDown = OnDownHandle;
        target.onUp = OnUpHandle;
        target.onLongPress = OnPressHandle;
        DragBeginPos = MoveTarget.localPosition;
    }

    private void OnDragHandle(GameObject go, PointerEventData eventData)
    {
        if (!IsCanDrag)
            return;

        // 若有向下穿透目标且在横向滑动中，继续横向滑动
        if (NewGraphicTarget != null && isScrolling)
        {
            ExecuteEvents.Execute(NewGraphicTarget, eventData, ExecuteEvents.dragHandler);
            return;
        }

        if (NewGraphicTarget == null)
        {
            isDragging = true;
        }
        else
        {
            // 判断屏幕新的拖拽位置和初始拖拽位置，若大于一定阈值，则表示开始拖拽

            RectTransformUtility.ScreenPointToLocalPointInRectangle(initParentRoot, eventData.position, cam, out tempNewPostion);
            if (Mathf.Abs(tempNewPostion.y - oldPostion.y) > DragingInterval)
            {
                isDragging = true;
            }
        }

        // 拖拽中则处理拖拽的逻辑
        if (isDragging)
        {
            if (CloneDrag)
            {
                if (!cloneDragItem.activeSelf)
                {
                    cloneDragItem.SetActive(true);
                }
            }

            Vector2 pos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(initParentRoot, eventData.position, cam, out pos);

            if (MoveTarget.localPosition.x == pos.x && MoveTarget.localPosition.y == pos.y)
                return;
            if (MathEx.Distance(DragBeginPos, pos) > LimitDistance)
            {
                pos = MathEx.ForwardXY(LimitDistance, MathEx.RadianUni(DragBeginPos, pos));
            }
            pos.x = Mathf.Clamp(pos.x, LimitLeftRight.x, LimitLeftRight.y);
            pos.y = Mathf.Clamp(pos.y, LimitTopBottom.x, LimitTopBottom.y);

            MoveTarget.localPosition = pos;
            OnDragEx?.Invoke(eventData.delta.x, eventData.delta.y, MoveTarget.localPosition.x, MoveTarget.localPosition.y);
            OnDrag?.Invoke(DragID);
        }
    }

    private void OnDragBeginHandle(GameObject go, PointerEventData eventData)
    {
        if (!IsCanDrag)
            return;

        if (NewGraphicTarget != null)
        {
            // 设置开始拖拽角度
            RectTransformUtility.ScreenPointToLocalPointInRectangle(initParentRoot, eventData.position, cam, out oldPostion);

            Vector2 ray = oldPostion - DragBeginPos;
            var angle = Vector2.Angle(ray, Vector2.up);
            // 与向上角度大于45且小于135度时，表示横向滑动，若有向下穿透对象，则开始横向滑动
            if (angle < 135 && angle > 45)
            {
                if (MoveDown)
                {
                    ray = oldPostion - DragBeginPos;
                    angle = Vector2.Angle(ray, Vector2.down);
                    if (angle < 135 && angle > 45)
                    {
                        isScrolling = true;
                        ExecuteEvents.Execute(NewGraphicTarget, eventData, ExecuteEvents.beginDragHandler);
                        return;
                    }
                }
                else
                {
                    isScrolling = true;
                    ExecuteEvents.Execute(NewGraphicTarget, eventData, ExecuteEvents.beginDragHandler);
                    return;
                }
            }
        }

        if (string.IsNullOrEmpty(DragID))
            DragID = gameObject.name;
        IsTouching = true;
        if (CloneDrag)
        {
            // 处理克隆对象
            cloneDragItem = Instantiate(gameObject, transform);
            cloneDragItem.SetLocalPositionToZero();
            cloneDragItem.SetLocalScaleToOne();
            cloneDragItem.SetCanvasGroupRaycast(0);
            MoveTarget = cloneDragItem.GetComponent<RectTransform>();
            DragTarget = cloneDragItem;
            if (ParentRoot)
            {
                ParentRoot.position = initParentRoot.position;
                cloneDragItem.transform.SetParent(ParentRoot);
            }
            cloneDragItem.SetActive(false);
            GraphicTarget.raycastTarget = true;
        }
        else
        {
            // 正常处理对象
            DragTarget = gameObject;
            if (ParentRoot)
            {
                ParentRoot.position = initParentRoot.position;
                transform.SetParent(ParentRoot);
            }
            GraphicTarget.raycastTarget = false;
        }
        OnDragBegin?.Invoke(DragID);

    }

    private void OnDragEndHandle(GameObject go, PointerEventData eventData)
    {
        if (!IsCanDrag)
            return;

        // 若有向下穿透目标且在横向滑动中，停止横向滑动
        if (NewGraphicTarget != null && isScrolling)
        {
            ExecuteEvents.Execute(NewGraphicTarget, eventData, ExecuteEvents.endDragHandler);
            isScrolling = false;
            return;
        }

        // 若是纵向拖拽则执行纵向拖拽的逻辑
        if (isDragging)
        {
            isDragging = false;
            if (CloneDrag)
            {
                // 处理克隆对象
                Destroy(cloneDragItem);
                cloneDragItem = null;
                MoveTarget = GetComponent<RectTransform>();
            }
            else
            {
                // 正常处理对象
                if (ParentRoot && initParentRoot)
                {
                    transform.SetParent(initParentRoot);
                    transform.SetSiblingIndex(initSiblingIndex);
                }
                if (DragEndResetPos)
                    MoveTarget.localPosition = DragBeginPos;
            }
            GraphicTarget.raycastTarget = true;
            IsTouching = false;
            OnDragEnd?.Invoke(DragID);
            DragTarget = null;
        }
    }

    private void OnEnterHandle(GameObject go, PointerEventData eventData)
    {
        if (eventData.dragging && DragTarget != null && go != DragTarget)
        {
            OnEnter?.Invoke(DragID);
        }
    }

    private void OnExitHandle(GameObject go, PointerEventData eventData)
    {
        if (eventData.dragging && DragTarget != null && go != DragTarget)
        {
            OnExit?.Invoke(DragID);
        }
    }

    private void OnPressHandle(GameObject go)
    {
        OnPress?.Invoke(DragID);
    }

    private void OnDownHandle(GameObject go, PointerEventData eventData)
    {
        OnDown?.Invoke(DragID);
    }

    private void OnUpHandle(GameObject go, PointerEventData eventData)
    {
        OnUp?.Invoke(DragID);
    }

    public void OnClickHandle(GameObject go, PointerEventData eventData)
    {
        OnClick?.Invoke(DragID);
    }

    //TODO，后期使用Spine之后优化
    /// <summary>
    /// 移动到某处
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="interval"></param>
    /// <param name="callBack"></param>
    public void SetOriginAnchorposMoveToTarget(Vector2 origin, Vector2 target, float interval, Action callBack = null)
    {
        DragEndResetPos = false;
        MoveTarget.localPosition = origin;
        MoveTarget.DOAnchorPos(target, interval).OnComplete(() =>
        {
            DragEndResetPos = true;
            callBack?.Invoke();
        });
    }
}