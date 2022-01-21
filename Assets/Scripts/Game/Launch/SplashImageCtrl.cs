using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Framework;

public class SplashImageCtrl : MonoBehaviour
{
    [Label("背景")]
    public Image BgImage;
    [Label("闪屏显示顺序")]
    public SplashImageData[] SplashImageDatas;
    public bool IsFinish { get; set; } = false;

    // Use this for initialization
    void Start()
    {
        if (SplashImageDatas == null || SplashImageDatas.Length == 0)
            IsFinish = true;
        else
        {
            IsFinish = false;
            StartCoroutine(ExecuteStep());
        }
    }

    private IEnumerator ExecuteStep()
    {
        bool ignoreTimeScale = true;
        RawImage image, image2;
        float duration, duration2;
        int length = SplashImageDatas.Length;
        for (int i = 0; i < SplashImageDatas.Length; i++)
        {
            image = SplashImageDatas[i].SplashImage;
            image.CrossFadeAlpha(0, 0, true);
            image.gameObject.SetActive(i == 0);
        }
        for (int i = 0; i < SplashImageDatas.Length; i++)
        {
            image = SplashImageDatas[i].SplashImage;
            if (i == 0)
            {
                //显示
                duration = SplashImageDatas[i].ShowDuration;
                image.CrossFadeAlpha(1, duration, ignoreTimeScale);
                yield return new WaitForSeconds(duration);
            }
            //停留
            duration = SplashImageDatas[i].StayDuration;
            yield return new WaitForSeconds(duration);
            //渐隐&下一个显示
            duration2 = 0;
            duration = SplashImageDatas[i].HideDuration;
            image.CrossFadeAlpha(0, duration, ignoreTimeScale);
            if (i + 1 < length)
            {
                image2 = SplashImageDatas[i + 1].SplashImage;
                duration2 = SplashImageDatas[i + 1].HideDuration;
                image2.gameObject.SetActive(true);
                image2.CrossFadeAlpha(1, duration2, ignoreTimeScale);
            }
            else
            {
                BgImage.CrossFadeAlpha(0, duration, ignoreTimeScale);
                StartUpManager.Instance.IsSplashFinish = true;
            }
            yield return new WaitForSeconds(Mathf.Max(duration, duration2));
        }
        gameObject.DestroyGameObj();
    }
}