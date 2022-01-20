using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.XR.WSA;
using XLua;

public class TouchControll : MonoBehaviour
{
    [CSharpCallLua]
    public static Action TouchClick;
    [CSharpCallLua]
    public static Action TouchRun; //摇杆被移动
    public enum TouchViewState
    {
        none = 0,
        beforerun = 1,
        run = 2
    }
    GameObject fullobj;
    RectTransform arrows;
    RectTransform node;
    GameObject remote;
    float presstime;
    public float SensitiveTime = 0.5f;
    public Vector2 offset;
    private TouchViewState state = TouchViewState.none;
    Ray ray;
    private RoleControler controler
    {
        get
        {
            return RoleControlerManager.Get().mainControler;
        }
    }
    public void Awake()
    {
        fullobj = transform.Find("full").gameObject;
        remote = transform.Find("remote").gameObject;
        arrows = remote.transform.Find("arrows").GetComponent<RectTransform>();
        node = remote.transform.Find("node").GetComponent<RectTransform>();
        EventTriggerListener e = EventTriggerListener.Get(fullobj);
        e.penetrate = true;
        e.onDown = OnDown;
        e.onDrag = onDrag;
        e.onUp = onUp;
    }
    public void OnDown(GameObject go, PointerEventData eventData)
    {
        if (RoleControlerManager.Get().GetMainRole() != null)
        {
            if (RoleControlerManager.Get().GetMainRole()._action is FollowAction)
            {
                return;
            }
        }
        presstime = 0;
        SetState(TouchViewState.beforerun);
    }
    public void SetRemotePosition()
    {
        Vector2 screenpos = Input.mousePosition;
        Vector2 localpos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), screenpos, UIModel.Inst.UICamera, out localpos);
        remote.GetComponent<RectTransform>().anchoredPosition = localpos;
    }
    public void RefreshRemoteActive()
    {
        remote.SetActive(state == TouchViewState.run);
    }

    public void Update()
    {
        if (RoleControlerManager.Get().GetMainRole() != null)
        {
            if (RoleControlerManager.Get().GetMainRole()._action is FollowAction)
            {
                return;
            }
        }
        if (ray.direction != null)
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10000, Color.red);
        }
        if (state == TouchViewState.none)
        {
            return;
        }
        if (state != TouchViewState.none)
            presstime += Time.deltaTime;
        if (state == TouchViewState.beforerun)
        {
            if (presstime > SensitiveTime)
            {
                offset = Vector2.zero;
                SetState(TouchViewState.run);
                SetRemotePosition();
                TouchRun?.Invoke();
            }
        }
        else if (state == TouchViewState.run)
        {
            Vector2 _normal = offset.normalized;
            node.anchoredPosition = Vector2.ClampMagnitude(offset, 24);
            arrows.anchoredPosition = _normal * 100;
            if (offset.sqrMagnitude > 0.1f && controler._bodytrans)
            {
                arrows.gameObject.SetActive(true);
                arrows.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_normal.y, _normal.x) * Mathf.Rad2Deg);
                Vector3 dir = Vector3.zero;
                dir.z = _normal.y;
                dir.x = _normal.x;
                dir.y = 0;
                //string CurrentDire = "移动";

                //float currentAngle = CalculaAngle(_normal.x, _normal.y);
                //if (currentAngle <= 22.5f && currentAngle >= 0f || currentAngle <= 360f && currentAngle >= 337.5f)//0;左
                //    CurrentDire = "A";
                //else if (currentAngle <= 67.5f && currentAngle >= 22.5f)//45;左上
                //    CurrentDire = "WA";
                //else if (currentAngle <= 112.5f && currentAngle >= 67.5f)//90;上
                //    CurrentDire = "W";
                //else if (currentAngle <= 157.5f && currentAngle >= 112.5f)//135;右上
                //    CurrentDire = "WD";
                //else if (currentAngle <= 202.5f && currentAngle >= 157.5f)//180;右
                //    CurrentDire = "D";
                //else if (currentAngle <= 247.5f && currentAngle >= 202.5f)//225;右下
                //    CurrentDire = "SD";
                //else if (currentAngle <= 292.5f && currentAngle >= 247.5f)//270;下
                //    CurrentDire = "S";
                //else if (currentAngle <= 337.5f && currentAngle >= 292.5f)//315;左下
                //    CurrentDire = "SA";
                //Debug.LogError("当前移动方向："+ CurrentDire);
                controler.MoveForwardAction(dir, (flag) =>
                {
                    if (!flag)
                    {
                        onUp(null, null);
                    }
                });
            }
            else
            {
                arrows.gameObject.SetActive(false);
                controler.ToNormalAction();
            }
        }
    }

    private float CalculaAngle(float _joyPositionX, float _joyPositionY)
    {
        float currentAngleX = _joyPositionX * 90f + 90f; //X轴 当前角度
        float currentAngleY = _joyPositionY * 90f + 90f; //Y轴 当前角度
        if (currentAngleY < 90f)
        {
            if (currentAngleX < 90f)
            {
                return 270f + currentAngleY;
            }
            else if (currentAngleX > 90f)
            {
                return 180f + (90f - currentAngleY);
            }
        }
        return currentAngleX;
    }


    public void OnDisable()
    {
        onUp(null, null);
    }
    public void onUp(GameObject go, PointerEventData eventData)
    {
        if (RoleControlerManager.Get().GetMainRole() != null)
        {
            if (RoleControlerManager.Get().GetMainRole()._action is FollowAction)
            {
                return;
            }
        }
        if (state == TouchViewState.beforerun && go != null)
        {
            TouchClick();
            OnClick();
        }
        SetState(TouchViewState.none);
        arrows.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        var ray = CameraManager.Get().maincamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, 1000, (1 << 10 | 1 << 8))) //对应lua中 WorldDefine.LayerMask
        {
            if (hitInfo.transform.gameObject.layer == 8) //点击NPC 
            {
                Debug.LogError("点击Npc");
            }
            else
            {
                Debug.LogError("点击寻路");
            }
        }
    }
    public void SetState(TouchViewState _state)
    {
        //if (_state == TouchViewState.run)
        //{
        //    controler.MoveForwardAction();
        //}
        if (state == TouchViewState.run)
        {
            controler.ToNormalAction();
        }
        state = _state;
        RefreshRemoteActive();
    }
    public void onDrag(GameObject go, PointerEventData eventData)
    {
        if (RoleControlerManager.Get().GetMainRole() != null)
        {
            if (RoleControlerManager.Get().GetMainRole()._action is FollowAction)
            {
                return;
            }
        }
        offset += eventData.delta;
    }
}
