using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Utils;

public class SDKInterfaceDefault : SDKInterface
{
    private const string DefaultKey = "default";
    public override void Init()
    {

    }

    #region 热更

    public override void getLocalDynamicUpdatePath()
    {
        SDKCall("get_local_dynamic_update_path", ResUtils.SDKUpdatePath);
    }

    //单位字节
    private long size;

    public override void getDynamicUpdate(string type)
    {
        SDKCall("get_dynamic_update_success", ResUtils.SDKUpdatePath);
        //TODO 获取更新大小
        //size = 10 * 1024 * 1024;
        SDKCall("get_dynamic_update_total_size", size + "");
    }

    public override void downDynamicUpdate()
    {
        //if (size > 0)
        //{
        //    int n = 0;
        //    while (n < size)
        //    {
        //        n += 1024 * 1024;
        //        SDKCall("get_dynamic_update_per", n + "");
        //    }
        //}
        //else
        //{
        //    SDKCall("get_dynamic_update_per", size + "");
        //}
    }

    public override void updateInfo()
    {
        string[][] temp = new string[][]
        {
            new string[2]{ "Config.unity3d", "3942656fb02c93346de5a72d255522d0"},
        };
        SDKCall("data_sucess", temp);
    }

    #endregion

    #region 登录

    public override void loginEx(string serverSid, int flags)
    {
        SDKCall("login_success", "");
    }

    public override void getServerList()
    {
        //TODO 读取Lua配置的服务器列表返回

        //Dictionary<string, object> item = new Dictionary<string, object>();

        //List<object> serverUserinfo = new List<object>();
        //Dictionary<string, object> user = new Dictionary<string, object>
        //{
        //    { "serverid", "1001" }
        //};
        //Dictionary<string, object> userInfo = new Dictionary<string, object>
        //{
        //    { "rolename", "角色名字-测试" },
        //    { "rolelevel", "11" },
        //    { "icon", "icon" }
        //};

        //user.Add("userinfo", Json.Encode(userInfo));
        //serverUserinfo.Add(user);
        //serverUserinfo.Add(user);


        //item.Add("serverUserinfo", serverUserinfo);
        //List<object> servers = new List<object>();
        //List<string> s = new List<string>() { "1001", "SDK-1服-测试", "0", "0", "0", "xxx", "1595498202" };
        //servers.Add(s);
        //item.Add("servers", servers);

        //item.Add("currLoginServers", "1001");

        //string temp = Json.Encode(item);
        SDKCall("get_server_list_success", DefaultKey);
    }

    public override void loginServer(string serverSid)
    {
        //TODO 模拟选择服务器操作
        //string[][] temp = new string[][]
        //{
        //    new string[2]{ "loginurl", ""},
        //    new string[2]{ "game_base_url", "http://192.168.5.211:7601"},
        //};
        SDKCall("auth_success", DefaultKey);
    }

    public override void getMaintainNotice()
    {
        SDKCall("get_maintainnotice", DefaultKey);
    }

    #endregion

    #region 退出

    public override void loginout()
    {
        SDKCall("auth_logout", DefaultKey);
    }

    public override void showExit()
    {
        SDKCall("show_exit", DefaultKey);
    }

    public override void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif
    }

    #endregion

    #region 防沉迷

    public override void getAgreementResult()
    {
        //是否同意协议：0取消，1同意
        SDKCall("get_agreement_result", PlayerPrefs.GetString("UserAgreement", "0"));
    }

    public override void showWebAgreementDialog()
    {
        //是否同意协议：0取消，1同意
        SDKCall("agreement_result", PlayerPrefs.GetString("UserAgreement", "0"));
    }

    public override void isMinor()
    {
        //玩家年龄段 > (-1:未获取到数据时的默认值) (0:未认证) (1:<8岁) (2:8岁>年龄<16岁) (3:16岁>年龄<18岁 4:>18岁)
        SDKCall("fangchenmi_flag", "4");
    }

    #endregion

    #region 商品

    public override void getGoodsList()
    {
        SDKCall("pay_goods_list", DefaultKey);
    }

    public override void buy(string productId, string gameProductId, string gameProductName)
    {
        SDKCall("pay_success", DefaultKey);
        SDKCall("get_change_order", productId + "|p360_14425|" + DateTime.UtcNow.Ticks);
    }

    public override void getGoodsList_pro()
    {
        SDKCall("pay_goods_list_pro", DefaultKey);
    }

    public override void buy_pro(string productId, string gameProductId, string gameProductName, string extra)
    {
        SDKCall("pay_success", DefaultKey);
        SDKCall("get_change_order", productId + "|p360_14425|" + DateTime.UtcNow.Ticks);
    }

    #endregion

    #region 数据上传

    public override void gameStepInfo(int step)
    {

    }

    public override void gameStepInfoFlag(int step, string s)
    {

    }

    public override void createRole(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {

    }
    public override void enterGame(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {

    }

    public override void gameRoleInfo(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {

    }

    public override void levelUp(int level)
    {

    }

    #endregion

    #region 推送

    public override void pushNotification(string content, int seconds, int jobId)
    {
        throw new System.NotImplementedException();
    }

    public override void cleanAllNotifi(int jobId)
    {
        throw new System.NotImplementedException();
    }

    public override void cleanAllNotifi()
    {
        throw new System.NotImplementedException();
    }

    public override void udpPush(string serverIp, int serverPort, string userid)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region 语音

    public override void startRecordVedio(string url, int quality, int vedioMaxTime)
    {
        throw new System.NotImplementedException();
    }

    public override void stopRecordVedio()
    {
        throw new System.NotImplementedException();
    }

    public override void playVedio(string vedioUrl)
    {
        throw new System.NotImplementedException();
    }

    public override void stopPlayingVideo()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region 其他

    public override void openNotchScreen()
    {
        string[][] temp = null;
        if (Launcher.Instance.TestNotchScreen)
        {
            temp = new string[][]
            {
                new string[2]{ "hasNotchScreen", "true"},
                new string[2]{ "notchWight", "120"},
            };
        }
        else
        {
            temp = new string[][]
            {
                new string[2]{ "hasNotchScreen", "false"},
            };
        }
        SDKCall("notchScreenInfo", temp);
    }

    public override void getAppInfo()
    {
        string[][] temp = new string[][]
        {
            new string[2]{ "appName", "appName"},
            new string[2]{ "platform", "1"},
            new string[2]{ "appId", "appId"},
            new string[2]{ "youkia_deviceId", "youkia_deviceId"},
            new string[2]{ "deviceId", "deviceId"},
            new string[2]{ "version", "version"},
        };
        SDKCall("get_app_info", temp);
    }

    public override void enterSocial()
    {
        Log.Debug("enterSocial");
    }

    public override void getMemory()
    {
        string[][] temp = new string[][]
        {
            new string[2]{ "totalMem", SystemInfo.systemMemorySize.ToString()},
            new string[2]{ "availMem", SystemInfo.systemMemorySize.ToString()},
        };
        SDKCall("get_memory", temp);
    }

    public override void getBattery()
    {
        string[][] temp = new string[][]
        {
            new string[2]{ "level", "100"},
            new string[2]{ "scale", "100"},
        };
        SDKCall("current_battery", temp);
    }

    public override void isEmulator()
    {
        SDKCall("is_emulator", "1");
    }

    public override void setClipboard(string content)
    {
        GUIUtility.systemCopyBuffer = content;
    }

    #endregion

    #region SDKCall

    private void SDKCall(string head, string[][] body)
    {
        SDKCall(head, ToJsonString(body));
    }

    private void SDKCall(string head, string body)
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "head", head }
        };
        data.Add("body", body);
        AndroidSDKManager.Instance.OnGetMessage(Json.Encode(data));
    }

    private string ToJsonString(string[][] body)
    {
        Dictionary<string, object> item = new Dictionary<string, object>();
        for (int i = 0; i < body.Length; i++)
        {
            item.Add(body[i][0], body[i][1]);
        }
        return Json.Encode(item);
    }

    #endregion
}

