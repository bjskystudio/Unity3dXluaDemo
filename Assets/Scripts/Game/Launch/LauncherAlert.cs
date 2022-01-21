using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 提示框
/// </summary>
public class LauncherAlert : MonoBehaviour
{
    /// <summary>
    /// 标题
    /// </summary>
    [SerializeField]
    private AorText TitleText;
    /// <summary>
    /// 内容
    /// </summary>
    [SerializeField]
    private AorText ContentText;
    /// <summary>
    /// 确定按钮
    /// </summary>
    [SerializeField]
    private AorButton SureBtn;
    /// <summary>
    /// 取消按钮
    /// </summary>
    [SerializeField]
    private AorButton CancelBtn;

    public void ShowAlert(string title, string contentStr, string sureText, Action sureCall, string cancelText, Action cancelCall)
    {
        TitleText.text = title;
        ContentText.text = contentStr;
        SureBtn.BtnText.text = sureText;
        SureBtn.onClick.RemoveAllListeners();
        SureBtn.onClick.AddListener(() =>
        {
            sureCall?.Invoke();
            gameObject.SetActive(false);
        });
        CancelBtn.BtnText.text = sureText;
        CancelBtn.onClick.RemoveAllListeners();
        CancelBtn.onClick.AddListener(() =>
        {
            cancelCall?.Invoke();
            gameObject.SetActive(false);
        });
    }
}
