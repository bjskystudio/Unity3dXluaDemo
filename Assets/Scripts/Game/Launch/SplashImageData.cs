using Framework;
using UnityEngine;
using UnityEngine.UI;

public class SplashImageData : MonoBehaviour
{
    public RawImage SplashImage;
    [Label("渐现持续时间")]
    public float ShowDuration;
    [Label("停留持续时间")]
    public float StayDuration;
    [Label("渐隐持续时间")]
    public float HideDuration;
}
