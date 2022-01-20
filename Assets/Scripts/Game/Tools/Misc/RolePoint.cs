using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型挂点
/// </summary>
public class RolePoint : MonoBehaviour
{
    [HeaderAttribute("血条挂点-跟随头部骨骼")]
    public Transform Point_Head;
    [HeaderAttribute("血条挂点-只跟随人物位移")]
    public Transform Point_Head_S;
    [HeaderAttribute("左手挂点")]
    public Transform Point_Hand_L;
    [HeaderAttribute("右手挂点")]
    public Transform Point_Hand_R;
    [HeaderAttribute("胸部挂点")]
    public Transform Point_Chest;
    [HeaderAttribute("摄像机挂点")]
    public Transform Point_Camera;
    [HeaderAttribute("影子挂点")]
    public Transform Point_Shadow;
    /// <summary>
    /// 目前仅战斗使用
    /// </summary>
    [HeaderAttribute("特效挂点")]
    public Transform Point_Effect;

    [HeaderAttribute("阴影碰撞挂点")]
    public Transform Point_ShadowCollider;
    [HeaderAttribute("左脚挂点")]
    public Transform Point_Foot_L;
    [HeaderAttribute("右脚挂点")]
    public Transform Point_Foot_R;
    public void Awake()
    {

    }

    [ContextMenu("自动关联")]
    private void AutoFindAll()
    {
        FindAll();
    }

    public void FindAll()
    {
        Transform[] allChild = GetComponentsInChildren<Transform>(); //直接遍历 防止 模型搞错节点
        foreach (Transform child in allChild)
        {
            string name = child.name.ToLower();
            switch (name)
            {
                case "point_head":
                    Point_Head = child;
                    break;
                case "point_head_s":
                    Point_Head_S = child;
                    break;
                case "point_hand_l":
                    Point_Hand_L = child;
                    break;
                case "point_hand_r":
                    Point_Hand_R = child;
                    break;
                case "point_chest":
                    Point_Chest = child;
                    break;
                case "point_camera":
                    Point_Camera = child;
                    break;
                case "point_shadow":
                    Point_Shadow = child;
                    break;
                case "point_effect":
                    Point_Effect = child;
                    break;
                case "point_shadowcollider":
                    Point_ShadowCollider = child;
                    break;
                case "point_foot_l":
                    Point_Foot_L = child;
                    break;
                case "point_foot_r":
                    Point_Foot_R = child;
                    break;
            }
        }
    }
}
