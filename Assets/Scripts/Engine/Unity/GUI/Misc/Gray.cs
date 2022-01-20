using Framework;
using UnityEngine;

/// <summary>
/// 中途更新受限于OnTransformChildrenChanged机制，只能动态影响到挂载Gray的根节点子物体改变
/// </summary>
public class Gray : MonoBehaviour
{
    IGrayMember[] imgs;
    IGrayMember[] c_imgs;
    bool isEnable;
    bool isGray;

    void Awake()
    {
        if (!isEnable)
        {
            isEnable = true;
            imgs = transform.GetInterfacesInChlidren<IGrayMember>();
            SetGrayEffect(isGray);
        }
    }

    /// <summary>
    /// 是否置灰
    /// </summary>
    /// <param name="value">是否置灰</param>
    public void SetGrayEffect(bool value)
    {
        isGray = value;
        if (isEnable && imgs != null)
        {
            for (int i = 0; i < imgs.Length; i++)
            {
                if (imgs[i] != null)
                {
                    imgs[i].SetGrayEffect(value);
                }
            }
        }
    }

    void OnTransformChildrenChanged()
    {
        c_imgs = gameObject.GetInterfacesInChlidren<IGrayMember>();
        if (c_imgs != null)
        {
            //中途移入的
            for (int i = 0; i < c_imgs.Length; i++)
            {
                int id = IsInChilid(c_imgs[i], imgs);
                if (id == -1)
                {
                    c_imgs[i].SetGrayEffect(true);
                }
            }
        }
        //中途移出的
        for (int i = 0; i < imgs.Length; i++)
        {
            int id = c_imgs == null ? i : IsInChilid(imgs[i], c_imgs);
            if (id == -1)
            {
                imgs[i].SetGrayEffect(false);
            }
        }
        imgs = c_imgs;
    }

    int IsInChilid(IGrayMember igm, IGrayMember[] imgs)
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            if (imgs[i] == igm)
            {
                return i;
            }
        }
        return -1;
    }

    //void OnDestroy()
    //{
    //    if (isEnable)
    //    {
    //        isEnable = false;
    //        for (int i = 0; i < imgs.Length; i++)
    //        {
    //            if (imgs[i] != null)
    //            {
    //                imgs[i].SetGrayEffect(false);
    //            }
    //        }
    //    }
    //}
}
