using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AorTMP : TextMeshProUGUI, IGrayMember,IPointerClickHandler
{
    public static Func<string, string> OnAwake = null;

    public string languageKey;

    [Serializable]
    public class LinkClickEvent : UnityEvent<string> { }
    [SerializeField]
    private LinkClickEvent m_OnLinkClick = new LinkClickEvent();
    /// <summary>
    /// 超链接点击事件
    /// </summary>
    public LinkClickEvent onLinkClick
    {
        get { return m_OnLinkClick; }
        set { m_OnLinkClick = value; }
    }

    protected override void Awake()
    {
        if (Application.isPlaying && !string.IsNullOrEmpty(languageKey))
        {
            if (OnAwake != null)
            {
                text = OnAwake(languageKey);
            }
        }
        base.Awake();
    }

    public bool IsGray { get; private set; }

    private Color oldColor;

    public void SetGrayEffect(bool isGray)
    {
        if (IsGray == isGray)
            return;
        IsGray = isGray;
        if (isGray)
        {
            oldColor = color;
            ColorUtility.TryParseHtmlString(AorText.GrayColor, out Color newColor);
            color = newColor;
        }
        else
        {
            color = oldColor;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(this, Input.mousePosition, UIModel.Inst.UICamera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = this.textInfo.linkInfo[linkIndex];
            string linkId = linkInfo.GetLinkID();
            m_OnLinkClick?.Invoke(linkId);
        }
    }
}