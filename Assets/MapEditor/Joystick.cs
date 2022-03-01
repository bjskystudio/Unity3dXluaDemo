using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : ScrollRect, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private GameObject touchBgGo;
    private GameObject touchPointGo;
    protected float radius = 100;
    public NavMeshAgent _navAgent;
    public Vector2 offset;
    private Animator Anim;
    private TouchView.TouchState State;
    private float DowTime = 0;
    protected override void Start()
    {
        touchPointGo = transform.Find("bg/Point").gameObject;
        touchBgGo = transform.Find("bg").gameObject;
        radius = touchPointGo.GetComponent<RectTransform>().sizeDelta.x * 0.7f;
        _navAgent = GameObject.Find("角色").GetComponentInChildren<NavMeshAgent>();
        _navAgent.radius = 20;
        _navAgent.acceleration = 200;
        _navAgent.autoTraverseOffMeshLink = false;
        Anim = GameObject.Find("角色").GetComponentInChildren<Animator>();
        State = TouchView.TouchState.Idle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPointGo.SetActive(true);
        touchBgGo.transform.position = eventData.position;
        State = TouchView.TouchState.Down;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        DowTime = 0;
        touchPointGo.SetActive(false);
        touchBgGo.transform.localPosition = Vector2.zero;
        offset = Vector2.zero;
        if (State == TouchView.TouchState.Run)
        {
            Anim.Play("map_stand");
            State = TouchView.TouchState.Idle;
        }
        else
        {
            State = TouchView.TouchState.Idle;
            Debug.LogError("发射线寻路");
        }
        
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        var contentPos = this.content.anchoredPosition;
        if (contentPos.magnitude > radius)
        {
            contentPos = contentPos.normalized * radius;
            SetContentAnchoredPosition(contentPos);
        }
        offset += eventData.delta;
    }
    void Update()
    {
        if (State == TouchView.TouchState.Down)
        {
            DowTime += Time.deltaTime;
            if (DowTime>=0.3f)
            {
                Anim.Play("map_run");
                State = TouchView.TouchState.Run;
            }
        }
        else if (State == TouchView.TouchState.Run && offset.sqrMagnitude>0.1f)
        {
            Vector2 _normal = offset.normalized;
            Vector3 Dir = Vector3.zero;
            Dir.z = _normal.y;
            Dir.x = _normal.x;
            Dir.y = 0;
            _navAgent.Move(Dir * Time.deltaTime * 5);
            GameObject.Find("角色").transform.forward = Dir;
            //if (Dir.x != 0)
            //{
            //    GameObject.Find("角色").transform.forward = Vector3.right * (Dir.x > 0 ? 1 : -1);
            //}
        }

    }
}
