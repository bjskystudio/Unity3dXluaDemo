using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 携带语言表数据的Key;
/// </summary>
[ExecuteInEditMode]
public class AorText : Text, IGrayMember
{
    public string languageKey;
    public override string text
    {
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(m_Text))
                {
                    m_Text = "";
                    SetVerticesDirty();
                }
            }
            else if (m_Text != value)
            {
                m_Text = value;
                SetVerticesDirty();
                SetLayoutDirty();
            }
        }
        get
        {
            return m_Text;
        }
    }

    public static Func<string, string> OnAwake = null;

    protected override void Awake()
    {
        if (Application.isPlaying && !string.IsNullOrEmpty(languageKey))
        {
            if (OnAwake != null)
            {
                m_Text = OnAwake(languageKey);
            }
        }
        base.Awake();
    }

    protected override void OnDestroy()
    {
        if (!string.IsNullOrEmpty(languageKey))
        {
            m_Text = null;
            SetVerticesDirty();
            base.OnDestroy();
        }
        base.OnDestroy();
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
            ColorUtility.TryParseHtmlString("#585858", out Color newColor);
            color = newColor;
        }
        else
        {
            color = oldColor;
        }
    }
}
