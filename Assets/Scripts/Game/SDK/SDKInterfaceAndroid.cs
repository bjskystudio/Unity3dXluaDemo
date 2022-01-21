using System;
using UnityEngine;
using YoukiaCore.Utils;

//#if UNITY_ANDROID
public class SDKInterfaceAndroid : SDKInterface
{
    private AndroidJavaObject jo;

    public SDKInterfaceAndroid()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    public override void Init()
    {

    }

    private void SDKCall(string method, params object[] param)
    {
        if (param != null)
        {
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] != null)
                {
                    if (param[i].GetType() == typeof(Int64))
                    {
                        param[i] = param[i].ToString().ToInt();
                        Debug.Log("转换：" + method + "值:" + param[i] + ",类型:" + param[i].GetType());
                    }
                }
            }
        }
        if (YoukiaCore.Log.Log.Level == YoukiaCore.Log.Log.LogLevel.All)
        {
            string logArgs = "";
            if (param != null)
            {
                logArgs = "[";
                for (int i = 0; i < param.Length; i++)
                {
                    if (i < param.Length - 1)
                        logArgs += param[i] + ",";
                    else
                        logArgs += param[i];
                }
                logArgs += "]";
            }
            UnityEngine.Debug.Log("SDKSendMsg:" + method + " " + logArgs);
        }
        try
        {
            if (StepKeyValue.ContainsKey(method))
            {
                gameStepInfo(StepKeyValue[method]);
            }
            jo.Call(method, param);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    #region 热更

    public override void getLocalDynamicUpdatePath()
    {
        SDKCall("getLocalDynamicUpdatePath");
    }

    public override void getDynamicUpdate(string type)
    {
        SDKCall("getDynamicUpdate", type);
    }

    public override void downDynamicUpdate()
    {
        SDKCall("downDynamicUpdate");
    }

    public override void updateInfo()
    {
        SDKCall("updateInfo");
    }

    #endregion

    #region 登录

    public override void loginEx(string serverSid, int flags)
    {
        SDKCall("loginEx", serverSid, flags);
    }

    public override void getServerList()
    {
        SDKCall("getServerList");
    }

    public override void loginServer(string serverSid)
    {
        SDKCall("loginServer", serverSid);
    }

    public override void getMaintainNotice()
    {
        SDKCall("getMaintainNotice");
    }

    #endregion

    #region 退出

    public override void loginout()
    {
        SDKCall("loginout");
    }

    public override void showExit()
    {
        SDKCall("showExit");
    }

    public override void exit()
    {
        SDKCall("exit");
    }

    #endregion

    #region 防沉迷

    public override void getAgreementResult()
    {
        SDKCall("getAgreementResult");
    }

    public override void showWebAgreementDialog()
    {
        SDKCall("showWebAgreementDialog");
    }

    public override void isMinor()
    {
        SDKCall("isMinor");
    }

    #endregion

    #region 商品

    public override void getGoodsList()
    {
        SDKCall("getGoodsList");
    }

    public override void buy(string productId, string gameProductId, string gameProductName)
    {
        SDKCall("buy", gameProductId, gameProductName, productId);
    }

    public override void getGoodsList_pro()
    {
        SDKCall("getGoodsList_pro");
    }

    public override void buy_pro(string productId, string gameProductId, string gameProductName, string extra)
    {
        SDKCall("buy_pro", productId, gameProductId, gameProductName, extra);
    }

    #endregion

    #region 数据上传

    public override void gameStepInfo(int step)
    {
        SDKCall("gameStepInfo", step.ToString());
    }

    public override void gameStepInfoFlag(int step, string s)
    {
        SDKCall("gameStepInfo", step.ToString(), s);
    }

    public override void createRole(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {
        SDKCall("createRole", roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra);
    }
    public override void enterGame(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {
        SDKCall("enterGame", roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra);
    }

    public override void gameRoleInfo(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {
        SDKCall("gameRoleInfo", roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra);
    }

    public override void levelUp(int level)
    {
        SDKCall("levelUp", level.ToString());
    }

    #endregion

    #region 推送

    public override void pushNotification(string content, int seconds, int jobId)
    {
        SDKCall("pushNotification", content, seconds, jobId);
    }

    public override void cleanAllNotifi(int jobId)
    {
        SDKCall("cleanAllNotifi", jobId);
    }

    public override void cleanAllNotifi()
    {
        SDKCall("cleanAllNotifi");
    }

    public override void udpPush(string serverIp, int serverPort, string userid)
    {
        SDKCall("udpPush", serverIp, serverPort.ToString(), userid);
    }

    #endregion

    #region 语音

    public override void startRecordVedio(string url, int quality, int vedioMaxTime)
    {
        SDKCall("startRecordVedio", url, quality.ToString(), vedioMaxTime.ToString());
    }

    public override void stopRecordVedio()
    {
        SDKCall("startRecordVedio");
    }

    public override void playVedio(string vedioUrl)
    {
        SDKCall("playVedio", vedioUrl);
    }

    public override void stopPlayingVideo()
    {
        SDKCall("stopPlayingVideo");
    }

    #endregion

    #region 其他

    public override void openNotchScreen()
    {
        SDKCall("openNotchScreen");
    }

    public override void getAppInfo()
    {
        SDKCall("getAppInfo");
    }

    public override void enterSocial()
    {
        SDKCall("enterSocial");
    }

    public override void getMemory()
    {
        SDKCall("getMemory");
    }

    public override void getBattery()
    {
        SDKCall("getBattery");
    }

    public override void isEmulator()
    {
        SDKCall("isEmulator");
    }

    public override void setClipboard(string content)
    {
        SDKCall("setClipboard", content);
    }
    #endregion
}
//#endif