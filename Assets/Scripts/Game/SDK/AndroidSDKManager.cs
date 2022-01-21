using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
using YoukiaCore.Event;
using YoukiaCore.Log;
using YoukiaCore.Utils;

/// <summary>
/// SDK管理器
/// <para>不能改类名，SDK那边要往这个类抛数据</para>
/// </summary>
[XLua.LuaCallCSharp]
public class AndroidSDKManager : MonoSingleton<AndroidSDKManager>
{
    [CSharpCallLua]
    public static Action<string, string> LuaOnGetMessage;

    public SDKInterface SDK;

    #region SDK请求与返回
    /// <summary>
    /// 步骤统计:递减接口（小的统计）
    /// <para>游戏到达步骤统计（没有返回）发送给bi3平台</para>
    /// <code>public void gameStepInfo(final String step)</code>
    /// <para>传参为数字 当发送-2时，如果之前有发送过-1，则-2将-1覆盖</para>
    /// </summary>
    public const string SendGameSetpInfo = "gameStepInfo";
    /// <summary>
    /// 步骤统计:覆盖接口（小的统计）
    /// <para>游戏到达步骤统计（没有返回）和上一个接口相同功能</para>
    /// <code>public void gameStepInfoFlag(final String step, final String s)</code>
    /// <para>传参为数字 当发送-2时，如果之前有发送过-1，则-2将-1覆盖，第二个参数没有什么实际用处，方便区分接口</para>
    /// <para>备注：和gameStepInfo会有相同记录时的覆盖操作：bi那边的会，大数据那边好像不会(新版本未确定)</para>
    /// </summary>
    public const string SendGameSetpInfoFlag = "gameStepInfoFlag";
    /// <summary>
    /// 获取本地热更地址
    /// </summary>
    public const string GetLocalDynamicUpdatePath = "get_local_dynamic_update_path";
    /// <summary>
    /// 请求热更信息
    /// <para>检查热更接口</para>
    /// <code>public void getDynamicUpdate(final String type)</code>
    /// <para>参数： type 是热更的文件夹名字 例如：要热更的GameRes文件夹 则传入 “GameRes”留有参数是为了方便有热更几个文件夹的方式</para>
    /// <para>备注1：GameRes目前是热更打包后的文件夹名称，产品配置热更时指向的热更目录必须包含(即打包热更包时就要把热更都放在此目录下)</para>
    /// <para>返回Key：成功时有2个：<see cref="GetDynamicUpdateSuccess"/><see cref="GetDynamicUpdateTotalSize"/>,失败：<see cref="GetDynamicUpdateFailed"/></para>
    /// </summary>
    public const string SendGetDynamicUpdate = "getDynamicUpdate";
    /// <summary>
    /// 请求热更信息返回-失败
    /// <para>返回实例：{"head":"get_dynamic_update_failed","body":"\/storage\/sdcard0\/youkia\/com.game.saint.a360\/GameRes\/"}</para>
    /// </summary>
    public const string GetDynamicUpdateFailed = "get_dynamic_update_failed";
    /// <summary>
    /// 请求热更信息返回-成功
    /// <para>返回实例：{"head":"get_dynamic_update_success","body":"\/storage\/sdcard0\/youkia\/com.game.saint.a360\/GameRes\/"}</para>
    /// </summary>
    public const string GetDynamicUpdateSuccess = "get_dynamic_update_success";
    /// <summary>
    /// 请求热更信息返回-热更大小
    /// <para>返回实例：{"head":"get_dynamic_update_total_size","body":"452550"}//单位字节</para>
    /// </summary>
    public const string GetDynamicUpdateTotalSize = "get_dynamic_update_total_size";
    /// <summary>
    /// 下载热更文件
    /// <para>说明：在请求热更信息返回-热更大小大于0的时候，调用该接口，sdk会开始下载热更文件，并返回下载完成的大小</para>
    /// <para>返回Key：<see cref="GetDynamicUpdatePer"/></para>
    /// </summary>
    public const string SendDownDynamicUpdate = "downDynamicUpdate";
    /// <summary>
    /// 下载热更文件返回
    /// <para>返回实例：{"head":"get_dynamic_update_per","body":"4096"}//单位字节</para>
    /// </summary>
    public const string GetDynamicUpdatePer = "get_dynamic_update_per";
    /// <summary>
    /// 请求下载完成后的文件md5
    /// <para>该接口主要用于热更完成的文件对应的md5</para>
    /// <para>返回Key：<see cref="GetDataSucess"/></para>
    /// </summary>
    public const string SendUpdateInfo = "updateInfo";
    /// <summary>
    /// 请求下载完成后的文件md5返回
    ///  <para>返回实例：{"head":"data_sucess","body":"{\"Config.unity3d\":\"3942656fb02c93346de5a72d255522d0\"}"},body是个json数组，动态更新的文件名字和当前动态更新对于的md5值</para>
    /// </summary>
    public const string GetDataSucess = "data_sucess";
    /// <summary>
    /// 异形屏适配接口
    /// <para>在刘海屏显示内容，并返回刘海相关信息</para>
    /// <para>返回Key：<see cref="GetNotchScreenInfo"/></para>
    /// </summary>
    public const string SendOpenNotchScreen = "openNotchScreen";
    /// <summary>
    /// 请求异形屏适配接口返回
    /// <para>有刘海，notchWight和notchHeight是刘海的宽和高，notchLocation是刘海的位置，left或right，竖屏应用对应的位置是 top或 bottom。</para>
    /// <para>返回实例：{"head":"notchScreenInfo","body":"{\"hasNotchScreen\":\"true\",\"notchWight\":75,\"notchHeight\":154,\"notchLocation\":\"left\"}"}</para>
    /// <para>无刘海，hasNotchScreen 是false</para>
    /// <para>返回实例：{"head":"notchScreenInfo","body":"{\"hasNotchScreen\":\"false\"}"}</para>
    /// </summary>
    public const string GetNotchScreenInfo = "notchScreenInfo";
    #endregion

    /// <summary>
    /// SDK的热更目录
    /// </summary>
    public string SDKUpdateResRoot = "";


    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
        InitSDK();
        AddAutoSetpFlag();
    }

    private void InitSDK()
    {
#if UNITY_EDITOR || UNITY_STANDLONE
        SDK = new SDKInterfaceDefault();
#elif UNITY_ANDROID
        if (Launcher.Instance.IsSDK)
            SDK = new SDKInterfaceAndroid();
        else
            SDK = new SDKInterfaceDefault();
#elif UNITY_IOS
        if (Launcher.Instance.IsSDK)
            SDK = new SDKInterfaceIOS();
        else
            SDK = new SDKInterfaceDefault();
#endif
        SDK.Init();
    }

    /// <summary>
    /// 初始化SDK自动化打点
    /// </summary>
    private void AddAutoSetpFlag()
    {
        SDK.AddAutoSetpFlag("loginEx", -1001);
        SDK.AddAutoSetpFlag("loginout", -1002);
        SDK.AddAutoSetpFlag("getServerList", -1003);
        SDK.AddAutoSetpFlag("getDynamicUpdate", -1004);
        SDK.AddAutoSetpFlag("getAppInfo", -1005);
        SDK.AddAutoSetpFlag("downDynamicUpdate", -1006);
        SDK.AddAutoSetpFlag("getMaintainNotice", -1007);
        SDK.AddAutoSetpFlag("loginServer", -1008);
        SDK.AddAutoSetpFlag("openUrl", -1009);
        SDK.AddAutoSetpFlag("updateInfo", -1010);
        SDK.AddAutoSetpFlag("getMemory", -1011);
        SDK.AddAutoSetpFlag("clearUpdateCache", -1012);

        SDK.AddAutoSetpFlag("login_success", -1013);
        SDK.AddAutoSetpFlag("login_failed", -1014);
        SDK.AddAutoSetpFlag("login_cancel", -1015);
        SDK.AddAutoSetpFlag("auth_failed", -1016);

        SDK.AddAutoSetpFlag("auth_logout", -1017);
        SDK.AddAutoSetpFlag("get_server_list_success", -1018);
        SDK.AddAutoSetpFlag("get_dynamic_update_total_size", -1019);
        SDK.AddAutoSetpFlag("get_dynamic_update_success", -1020);

        SDK.AddAutoSetpFlag("get_dynamic_update_failed", -1021);
        //SDK.AddAutoSetpFlag("get_dynamic_update_per", -1022);
        SDK.AddAutoSetpFlag("get_maintainnotice", -1023);
        SDK.AddAutoSetpFlag("auth_success", -1024);

        SDK.AddAutoSetpFlag("get_app_info", -1025);
    }

    public void OnGetMessage(string msg)
    {
        Log.Debug("SDKReceiveMsg:" + msg);
        try
        {
            Dictionary<string, object> jsonData = Json.Decode(msg) as Dictionary<string, object>;
            string head = jsonData["head"].ToString();
            string body = null;
            if (jsonData.ContainsKey("body"))
                body = jsonData["body"].ToString();

            switch (head)
            {
                case GetDynamicUpdatePer:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdate, body);
                    break;
                case GetDynamicUpdateTotalSize:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdateSuccess, body);
                    break;
                case GetDynamicUpdateSuccess:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdatePath, body);
                    break;
                case GetDynamicUpdateFailed:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetDynamicUpdateFailed, body);
                    break;
                case GetNotchScreenInfo:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetNotchScreenInfo, body);
                    break;
                case GetLocalDynamicUpdatePath:
                    GlobalEvent.DispatchEventSingle(EventDef.SDKGetLocalDynamicUpdatePathSuccess, body);
                    break;
            }
            if (LuaOnGetMessage != null)
            {
                LuaOnGetMessage.Invoke(head, body);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("SDKReceiveError:" + e);
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        LuaOnGetMessage = null;
    }
}
