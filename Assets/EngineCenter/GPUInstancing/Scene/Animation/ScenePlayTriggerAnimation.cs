using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 场景物件根据触发播放动画
/// </summary>
[RequireComponent(typeof(Animation))]
public class ScenePlayTriggerAnimation : MonoBehaviour
{
    public Animation Anim;
    public LayerMask TargetLayer = LayerUtil.Player;

    private void Awake()
    {
        if(Anim == null)
        {
            Anim = this.GetComponent<Animation>();
        }
        if(Anim.enabled == true)
        {
            Anim.enabled = false;
        }
    }

    private void OnDisable()
    {
        Anim.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == TargetLayer.value)
        {
            Anim.enabled = true;
        }
    }
}
