using ResourceLoad;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using YoukiaCore.Log;

/// <summary>
/// 携带语言表数据的Key;
/// </summary>
[ExecuteInEditMode]
public class AorText : Text, IGrayMember
{
    //Lua侧注入
    public static Func<string, string> OnAwake = null;
    public const string GrayColor = "#585858";

    /// <summary>
    /// 是否使用默认shader
    /// </summary>
    public bool UseDefaultShader = false;
    /// <summary>
    /// 本地化Key
    /// </summary>
    public string languageKey;

    private string mCurrentPath = null;
    private string CurrentPath
    {
        get
        {
            if (string.IsNullOrEmpty(mCurrentPath) && font != null)
            {
                mCurrentPath = font.name;
            }
            return mCurrentPath;
        }
        set
        {
            mCurrentPath = value;
        }
    }
    private ResRef mResRef = null;

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

    public void LoadFont(string path, bool isSync = true, Action callback = null)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        if (!string.IsNullOrEmpty(CurrentPath) && (CurrentPath.Equals(path) || CurrentPath.Equals(Path.GetFileName(path))))
        {
            Log.Debug($"相同字体，无需加载,CurrentPath:{CurrentPath},path:{path}");
        }
        else
        {
            CurrentPath = path;

            AssetLoadManager.Instance.LoadABFont(path, (newFont, resRef) =>
            {
                if (newFont != null)
                {
                    if (this == null || gameObject == null)
                    {
                        resRef.Release();
                        return;
                    }
                    if (mResRef != null)
                    {
                        mResRef.Release();
                        mResRef = null;
                    }
                    font = newFont;
                    CurrentPath = path;
                    mResRef = resRef;
                    callback?.Invoke();
                }
                else
                {
                    if (resRef != null)
                    {
                        resRef.Release();
                    }
#if UNITY_EDITOR
                    var parentGoTab = transform.GetComponentInParent<UIGoTable>();
                    if (parentGoTab)
                        Log.Error($"LoadABFont Error 不存在:{path}, this:{name}, parent:{parentGoTab.name}");
                    else
                        Log.Error($"LoadABFont Error 不存在:{path}, this:{name}, parent:{transform.parent.name}");
#else
                    Log.Error($"LoadABFont Error 不存在:{path}, this:{name}, parent:{transform.parent}");
#endif
                }
            });
        }
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
        if (mResRef != null)
        {
            font = null;
            mResRef.Release();
        }
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
            ColorUtility.TryParseHtmlString(GrayColor, out Color newColor);
            color = newColor;
        }
        else
        {
            color = oldColor;
        }
    }

    public float PreferredHeight
    {
        get { return this.preferredHeight; }
    }
}
