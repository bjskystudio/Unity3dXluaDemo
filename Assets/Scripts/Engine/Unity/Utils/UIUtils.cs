using UnityEngine;

public static class UIUtils
{
    /// <summary>
    /// 更新UI实体对象Order
    /// </summary>
    /// <param name="go">实体对象</param>
    /// <param name="limitordervalue">Order范围限制</param>
    /// <param name="includeself">获取参考Canvas是否包含自身</param>
    public static void UpdateUIGameObjectOrder(GameObject go, int limitordervalue, bool includeself)
    {
        Debug.Assert(go != null, "不允许传空实体对象,更新UI实体对象Order失败!");
        Canvas referencecanvas = null;
        if(includeself)
        {
            referencecanvas = go.GetComponent<Canvas>();
        }
        if(referencecanvas == null)
        {
            referencecanvas = go.GetComponentInParent<Canvas>();
        }
        // Note:
        // 取余Order范围限制是为了确保不会因为多次计算累加导致Order值计算过大问题
        if (referencecanvas != null)
        {
            var gotransform = go.transform;
            var referencecanvasorder = referencecanvas.sortingOrder;
            foreach (ParticleSystem p in gotransform.GetComponentsInChildren<ParticleSystem>(true))
            {
                Renderer render = p.GetComponent<Renderer>();
                if (null != render)
                {
                    render.sortingOrder = render.sortingOrder % limitordervalue;
                    render.sortingOrder += referencecanvasorder;
                }
            }
            //UI特效也可能存在图片文字
            foreach (var render in gotransform.GetComponentsInChildren<SpriteRenderer>(true))
            {
                render.sortingOrder = render.sortingOrder % limitordervalue;
                render.sortingOrder += referencecanvasorder;
            }
            //也可能加了Canvas
            foreach (var item in gotransform.GetComponentsInChildren<Canvas>(true))
            {
                // 如果参考对象有自身，那么排除自身，避免自身Canvas Order计算错误
                if(includeself)
                {
                    if(item.gameObject == go)
                    {
                        continue;
                    }
                }
                item.sortingOrder = item.sortingOrder % limitordervalue;
                item.sortingOrder += referencecanvasorder;
            }
            //也可能加了MeshRenderer
            foreach (var item in gotransform.GetComponentsInChildren<MeshRenderer>(true))
            {
                item.sortingOrder = item.sortingOrder % limitordervalue;
                item.sortingOrder += (referencecanvasorder + 1);//不加1是看不到的(?)
            }
        }
        else
        {
            Debug.LogError($"对象Go:{go.name}找不到有效的参考Canvas,更新UI实体对象Order失败!");
        }
    }
}