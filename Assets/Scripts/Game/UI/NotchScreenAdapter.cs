using System.Collections;
using UnityEngine;

/// <summary>
/// 刘海屏适配
/// </summary>
public class NotchScreenAdapter : MonoBehaviour
{
    private RectTransform mCurRect;
    private float initoffsetMinX, initoffsetMaxX, initoffsetMinY, initoffsetMaxY;
    private Vector2 tempMinV2, tempMaxV2;
    private bool isStartInit = false;

    private void Start()
    {
        if (mCurRect == null)
            mCurRect = transform.GetComponent<RectTransform>();
        initoffsetMinX = mCurRect.offsetMin.x;
        initoffsetMinY = mCurRect.offsetMin.y;
        initoffsetMaxX = mCurRect.offsetMax.x;
        initoffsetMaxY = mCurRect.offsetMax.y;
        tempMinV2 = new Vector2();
        tempMaxV2 = new Vector2();
        isStartInit = true;
        ChangeSize();
    }

    public void ChangeSize()
    {
        if (!isStartInit)
            return;
        if (mCurRect == null)
            mCurRect = transform.GetComponent<RectTransform>();
        tempMinV2.Set(initoffsetMinX + UIModel.Inst.NotchScreenPixel / 2f, initoffsetMinY);
        tempMaxV2.Set(initoffsetMaxX - UIModel.Inst.NotchScreenPixel / 2f, initoffsetMaxY);
        mCurRect.offsetMin = tempMinV2;
        mCurRect.offsetMax = tempMaxV2;
    }
}
