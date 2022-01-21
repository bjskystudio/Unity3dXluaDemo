using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 启动Loading界面
/// </summary>
public class LauncherLoading : MonoBehaviour
{
    /// <summary>
    /// 提示框
    /// </summary>
    [SerializeField]
    private LauncherAlert Alert;
    /// <summary>
    /// 标题
    /// </summary>
    [SerializeField]
    private Text LoadingTitle;
    /// <summary>
    /// 描述
    /// </summary>
    [SerializeField]
    private Text LoadingText;
    /// <summary>
    /// 进度显示
    /// </summary>
    [SerializeField]
    private Text ProgressNum;
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider Progress;
    /// <summary>
    /// 目标进度
    /// </summary>
    private float targetProgress;
    /// <summary>
    /// 当前进度
    /// </summary>
    private float currentProgress;
    /// <summary>
    /// 文本配置
    /// </summary>
    private Dictionary<string, string> launcherLang;

    private void Awake()
    {
        ResetProgress();
        Alert.gameObject.SetActive(false);
        //加载语言配置
        launcherLang = new Dictionary<string, string>();
        var langAsset = Resources.Load("Launcher/LauncherLanguage") as TextAsset;
        var strlst = langAsset.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        string str;
        int index;
        for (int i = 0; i < strlst.Length; i++)
        {
            str = strlst[i];
            if (string.IsNullOrEmpty(str))
                continue;
            index = str.IndexOf("=");
            launcherLang.Add(str.Substring(0, index), str.Substring(index + 1));
        }
        Resources.UnloadAsset(langAsset);
        LoadingTitle.text = launcherLang["loadingTitle"];
    }

    /// <summary>
    /// 设置描述内容
    /// </summary>
    /// <param name="key">内容key</param>
    /// <param name="param">额外参数</param>
    public void SetLoadingTextByLangKey(string key, params string[] param)
    {
        SetLoadingText(string.Format(launcherLang[key], param));
    }

    /// <summary>
    /// 设置描述内容
    /// </summary>
    /// <param name="desc">内容</param>
    public void SetLoadingText(string desc)
    {
        LoadingText.text = desc;
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    /// <param name="key">文本key</param>
    /// <param name="sureCall">确定回调</param>
    /// <param name="cancelCall">取消回调</param>
    /// <param name="param">额外参数</param>
    public void ShowAlertByLangKey(string key, string sureBtnName, Action sureCall, string cancelBtnName, Action cancelCall, params string[] param)
    {
        ShowAlert(string.Format(launcherLang[key], param), sureBtnName, sureCall, cancelBtnName, cancelCall);
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    /// <param name="contentStr">文本内容</param>
    /// <param name="sureCall">确定回调</param>
    /// <param name="cancelCall">取消回调</param>
    private void ShowAlert(string contentStr, string sureBtnName, Action sureCall, string cancelBtnName, Action cancelCall)
    {
        Alert.gameObject.SetActive(true);
        Alert.ShowAlert(launcherLang["title"], contentStr, sureBtnName, sureCall, cancelBtnName, cancelCall);
    }

    /// <summary>
    /// 设置当前进度
    /// </summary>
    /// <param name="progress"></param>
    public void SetProgress(float progress)
    {
        if (progress < targetProgress)
            return;
        progress = Mathf.Clamp01(progress);
        targetProgress = progress;
    }

    private void ResetProgress()
    {
        currentProgress = 0;
        targetProgress = 0;
        ProgressNum.text = "0";
        Progress.value = 0;
    }

    /// <summary>
    /// 更新进度条位置
    /// </summary>
    /// <param name="progress"></param>
    private void UpdateProgress(float progress)
    {
        Progress.value = progress;
        ProgressNum.text = (int)(progress * 100) + "";
    }

    /// <summary>
    /// 是否Loading完成
    /// </summary>
    public bool IsComplete()
    {
        return currentProgress >= 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetProgress == 0)
            return;
        if (currentProgress <= targetProgress)
        {
            currentProgress = Mathf.Lerp(currentProgress += 0.05f, Mathf.Clamp01(targetProgress), 0.1f);
            //currentProgress = Mathf.Min(currentProgress += 0.01f, targetProgress);
        }
        UpdateProgress(currentProgress);
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}
