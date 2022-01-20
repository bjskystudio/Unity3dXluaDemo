using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MainRoleHandling : MonoBehaviour
{
    public NavMeshAgent navagent;
    public Animator animator;
    public Vector3 dir;
    public string currentanimation;
    public float speed = 5;
    public RoleControler controler;
    public void Awake()
    {
        if (navagent == null)
        {
            navagent = gameObject.AddComponent<NavMeshAgent>();
        }
        animator = GetComponentInChildren<Animator>();
        transform.position=CSGoHelp.GetSamplePosition(transform.position);
        GameObject.FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
    }
    public void PlayAnimation(string name)
    {
        if (currentanimation == name)
        {
            return;
        }
        currentanimation = name;
        animator.Play(currentanimation);
        animator.Update(0);
    }
    public void MoveTo(Vector2 _dir)
    {
        dir.z = _dir.y;
        dir.x = _dir.x;
        dir.y = 0;
        if(dir.sqrMagnitude>0.1f)
            dir = dir.normalized;
    }
    private void Update()
    {
        Vector3 angle=transform.eulerAngles;
        Vector3 tempdir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            tempdir = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            tempdir = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            tempdir = Vector3.forward;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            tempdir = Vector3.back;
        }
        dir += tempdir;
        if (dir.sqrMagnitude > 0)
        {
            navagent.Move(dir.normalized*Time.deltaTime*speed);
            if (dir.x != 0)
            {
                transform.forward = Vector3.right*(dir.x>0?1:-1);
            }
            PlayAnimation("run");
        }
        else
        {
            PlayAnimation("stand");
        }
        dir -= tempdir;
    }
}
