using Framework;
using UnityEngine;

public class Gray : MonoBehaviour
{
    IGrayMember[] imgs;
    IGrayMember[] c_imgs;
    bool isEnable;

    void Awake()
    {
        if (!isEnable)
        {
            isEnable = true;
            imgs = transform.GetInterfacesInChlidren<IGrayMember>();
            if (imgs != null && imgs.Length > 0)
            {
                for (int i = 0; i < imgs.Length; i++)
                {
                    imgs[i].SetGrayEffect(true);
                }
            }
        }
    }
    
    void OnTransformChildrenChanged()
    {
        c_imgs = gameObject.GetInterfacesInChlidren<IGrayMember>();
        //中途移入的
        for (int i = 0; i < c_imgs.Length; i++)
        {
            int id = IsInChilid(c_imgs[i], imgs);
            if (id == -1)
            {
                c_imgs[i].SetGrayEffect(true);
            }
        }
        //中途移出的
        for (int i = 0; i < imgs.Length; i++)
        {
            int id = IsInChilid(imgs[i], c_imgs);
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

    void OnDestroy()
    {
        if (isEnable)
        {
            isEnable = false;
            for (int i = 0; i < imgs.Length; i++)
            {
                if (imgs[i] != null)
                {
                    imgs[i].SetGrayEffect(false);
                }
            }
        }
    }
}
