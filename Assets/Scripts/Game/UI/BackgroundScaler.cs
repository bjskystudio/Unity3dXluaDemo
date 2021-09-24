using UnityEngine;

[RequireComponent(typeof(RectTransform))]
/// <summary>
/// 背景自适应
/// </summary>
public class BackgroundScaler : MonoBehaviour
{
    /// <summary>
    /// 是否需要适应宽
    /// </summary>
    public bool IsAdapterX = true;
    /// <summary>
    /// 是否需要适应高
    /// </summary>
    public bool IsAdapterY = true;

    private RectTransform mCurRect;
    private float initX, initY;
    private bool isStartInit = false;

    private void Start()
    {
        if (mCurRect == null)
            mCurRect = transform.GetComponent<RectTransform>();
        initX = mCurRect.sizeDelta.x;
        initY = mCurRect.sizeDelta.y;
        isStartInit = true;
        ChangeSize();
    }

    public void ChangeSize()
    {
        if (!isStartInit)
            return;
        if (mCurRect == null)
            mCurRect = transform.GetComponent<RectTransform>();
        float x = initX;
        float y = initY;
        if (IsAdapterX)
        {
            if (mCurRect.anchorMin.x == 0 && mCurRect.anchorMax.x == 1)
            {
                x = x + UIModel.Inst.NotchScreenPixel;
            }
            else
            {
                x = UIModel.Inst.ScreenSize.x;
            }
        }
        if (IsAdapterY)
        {
            if (mCurRect.anchorMin.y == 0 && mCurRect.anchorMax.y == 1)
            {
                y = y + UIModel.Inst.NotchScreenPixel;
            }
            else
            {
                y = UIModel.Inst.ScreenSize.x;
            }
        }
        mCurRect.sizeDelta = new Vector2(x, y);
    }
}
