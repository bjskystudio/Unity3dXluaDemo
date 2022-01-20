using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchView : MonoBehaviour
{
    public enum TouchState
    {
        Idle = 0,
        Down = 1,
        Run = 2
    } 
    private GameObject fullobj;
    private RectTransform arrows;
    private RectTransform node;
    private GameObject remote;
    private float DownTime;
    public float TimeMax = 0.5f;
    private TouchState State;
    public Vector2 Offset;

    void Awake()
    {
        fullobj = transform.Find("full").gameObject;
        remote = transform.Find("remote").gameObject;
        arrows = remote.transform.Find("arrows").GetComponent<RectTransform>();
        node = remote.transform.Find("node").GetComponent<RectTransform>();
        EventTriggerListener e = EventTriggerListener.Get(fullobj);
        e.penetrate = true;
        e.onDown = OnDown;
        e.onDrag = OnDrag;
        e.onUp = OnUp;
    }
    void Start()
    {
    }
    void Update()
    {
        if (State == TouchState.Idle) return;
        DownTime += Time.deltaTime;
        if (State == TouchState.Down && DownTime >= TimeMax)
        {
            SetRemotePos();
            SetState(TouchState.Run);
            Debug.LogError("开始拖拽");
        }else if (State == TouchState.Run)
        {
            Vector2 _normal = Offset.normalized;
            node.anchoredPosition = Vector2.ClampMagnitude(Offset, 24);
            arrows.anchoredPosition = _normal * 100;
            if (WorldManager.Instance.Player && Offset.sqrMagnitude > 0.1f && WorldManager.Instance.Player.Root)
            {
                arrows.gameObject.SetActive(true);
                arrows.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_normal.y, _normal.x) * Mathf.Rad2Deg);
                Vector3 dir = new Vector3(_normal.x, 0, _normal.y);
                WorldManager.Instance.Player.DoBehavior<MoveControlBehavior>().Dir = dir;
            }
            else
            {
                SetState(TouchState.Idle);
            }
        }

    }
    public void OnDisable()
    {
        SetState(TouchState.Idle);
    }
    public void OnDown(GameObject go, PointerEventData eventData)
    {
        Offset = Vector2.zero;
        DownTime = 0;
        SetState(TouchState.Down);
    }
    public void OnDrag(GameObject go, PointerEventData eventData)
    {
        Offset += eventData.delta;
    }
    public void OnUp(GameObject go, PointerEventData eventData)
    {
        if (State == TouchState.Down && go != null)
        {
            WorldManager.TouchClick?.Invoke();
            Debug.LogError("点击");
        }
        SetState(TouchState.Idle);
    }

    public void SetRemotePos()
    {
        Vector2 screenpos = Input.mousePosition;
        Vector2 localpos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), screenpos, UIModel.Inst.UICamera, out localpos);
        remote.GetComponent<RectTransform>().anchoredPosition = localpos;
    }
    public void SetState(TouchState state)
    {
        State = state;
        if (State == TouchState.Idle) WorldManager.Instance.Player?.DoBehavior<IdleBehavior>();
        remote.SetActive(State == TouchState.Run);
    }
}
