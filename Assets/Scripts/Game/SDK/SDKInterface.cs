using System;
using System.Collections.Generic;

[XLua.LuaCallCSharp]
public abstract class SDKInterface
{
    public Dictionary<string, int> StepKeyValue = new Dictionary<string, int>();

    /// <summary>
    /// 添加SDK自动化打点
    /// </summary>
    /// <param name="key">交互关键字</param>
    /// <param name="step">步骤</param>
    public void AddAutoSetpFlag(string key, int step)
    {
        if (StepKeyValue.ContainsKey(key))
        {
            StepKeyValue[key] = step;
        }
        else
        {
            StepKeyValue.Add(key, step);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public abstract void Init();

    #region 热更

    /// <summary>
    /// 获取本地热更地址
    /// <para>返回Key：get_local_dynamic_update_path</para>
    /// </summary>
    public abstract void getLocalDynamicUpdatePath();

    /// <summary>
    /// 请求热更信息
    /// <para>检查热更接口</para>
    /// <code>public void getDynamicUpdate(final String type)</code>
    /// <para>参数： type 是热更的文件夹名字 例如：要热更的GameRes文件夹 则传入 “GameRes”留有参数是为了方便有热更几个文件夹的方式</para>
    /// <para>备注1：GameRes目前是热更打包后的文件夹名称，产品配置热更时指向的热更目录必须包含(即打包热更包时就要把热更都放在此目录下)</para>
    /// <para>返回Key：成功时有2个：get_dynamic_update_success|get_dynamic_update_total_size,失败：get_dynamic_update_failed</para>
    /// </summary>
    public abstract void getDynamicUpdate(string type);
    /// <summary>
    /// 下载热更文件
    /// <para>说明：在请求热更信息返回-热更大小大于0的时候，调用该接口，sdk会开始下载热更文件，并返回下载完成的大小</para>
    /// <para>返回Key：get_dynamic_update_per</para>
    /// </summary>
    public abstract void downDynamicUpdate();
    /// <summary>
    /// 请求下载完成后的文件md5
    /// <para>该接口主要用于热更完成的文件对应的md5</para>
    /// <para>返回Key：data_sucess</para>
    /// </summary>
    public abstract void updateInfo();

    #endregion

    #region 登录

    /// <summary>
    /// 登陆第三方渠道接口
    /// </summary>
    public abstract void loginEx(string serverSid, int flags);
    /// <summary>
    /// 得到服务器列表信息，在登录之前，必须得到服务器列表地址
    /// </summary>
    public abstract void getServerList();
    /// <summary>
    /// 登录挂接中心，得到必要参数进入游戏（该接口必须在登录第三方成功后才能调用）
    /// </summary>
    public abstract void loginServer(string serverSid);
    /// <summary>
    /// 请求当服务器维护的维护信息，产品在挂接中心后台填写的信息
    /// </summary>
    public abstract void getMaintainNotice();

    #endregion

    #region 退出

    /// <summary>
    /// 注销登录接口，当游戏内注销的时候调用该接口
    /// </summary>
    public abstract void loginout();
    /// <summary>
    /// 当点击返回键的时候，调用该接口，得到"show_exit"消息则调用起游戏自己的退出框。当没有收到该消息，则说明渠道有自己的退出框。游戏前台则不管后面的流程
    /// </summary>
    public abstract void showExit();
    /// <summary>
    /// 当游戏退出框点击确定的时候调用该接口
    /// </summary>
    public abstract void exit();

    #endregion

    #region 防沉迷

    /// <summary>
    /// 获取协议是否同意
    /// <para>返回Key:get_agreeement_result,值:0取消/1同意</para>
    /// </summary>
    public abstract void getAgreementResult();
    /// <summary>
    /// 显示Web协议弹窗
    /// <para>返回Key:agreeement_result,值:0取消/1同意</para>
    /// </summary>
    public abstract void showWebAgreementDialog();
    /// <summary>
    /// 是否未成年
    /// </summary>
    public abstract void isMinor();

    #endregion

    #region 商品

    /// <summary>
    /// 普通商品列表
    /// </summary>
    public abstract void getGoodsList();
    /// <summary>
    /// 购买商品(充值)
    /// </summary>
    /// <param name="productId">充值档位ID</param>
    /// <param name="gameProductId">游戏商品ID</param>
    /// <param name="gameProductName">游戏商品名称</param>
    public abstract void buy(string productId, string gameProductId, string gameProductName);
    /// <summary>
    /// 直购商品列表
    /// </summary>
    public abstract void getGoodsList_pro();
    /// <summary>
    /// 直购商品（礼包）
    /// </summary>
    /// <param name="productId">充值档位ID</param>
    /// <param name="gameProductId">游戏商品ID</param>
    /// <param name="gameProductName">游戏商品名称</param>
    /// <param name="extra">附加数据</param>
    public abstract void buy_pro(string productId, string gameProductId, string gameProductName, string extra);

    #endregion

    #region 数据上传

    /// <summary>
    /// 步骤统计:递减接口（小的统计）
    /// </summary>
    /// <param name="step">传参为数字 当发送-2时，如果之前有发送过-1，则-2将-1覆盖></param>
    public abstract void gameStepInfo(int step);
    /// <summary>
    /// 步骤统计:覆盖接口（小的统计）
    /// <para>备注：和gameStepInfo会有相同记录时的覆盖操作：bi那边的会，大数据那边好像不会(新版本未确定)</para>
    /// </summary>
    /// <param name="step">传参为数字 当发送-2时，如果之前有发送过-1，则-2将-1覆盖</param>
    /// <param name="s">没有什么实际用处，方便区分接口</param>
    public abstract void gameStepInfoFlag(int step, string s);
    /// <summary>
    /// 创建角色，接口在创建角色完成后需要游戏调用
    /// </summary>
    public abstract void createRole(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra);
    /// <summary>
    /// 进入游戏
    /// </summary>
    public abstract void enterGame(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra);
    /// <summary>
    /// 游戏角色数据
    /// </summary>
    public abstract void gameRoleInfo(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra);
    /// <summary>
    /// 升级
    /// </summary>
    public abstract void levelUp(int level);
    #endregion

    #region 推送

    /// <summary>
    /// 本地推送接口
    /// </summary>
    /// <param name="content">推送内容</param>
    /// <param name="seconds">是多少时间后推送该消息，单位S（秒）</param>
    /// <param name="jobId">推送id</param>
    public abstract void pushNotification(string content, int seconds, int jobId);
    /// <summary>
    /// 本地推送清除单个
    /// </summary>
    /// <param name="jobId">推送id</param>
    public abstract void cleanAllNotifi(int jobId);
    /// <summary>
    /// 清理本地推送消息
    /// </summary>
    public abstract void cleanAllNotifi();
    /// <summary>
    /// 网络推送:android推送接口，开启推送
    /// </summary>
    /// <param name="serverIp">推送服务器ip或者域名</param>
    /// <param name="serverPort">推送服务器的端口</param>
    /// <param name="userid">游戏角色id（注意，这里和推送服务器商定了，必须是全数字的字符串）</param>
    public abstract void udpPush(string serverIp, int serverPort, string userid);

    #endregion

    #region 语音

    /// <summary>
    /// 开始录制
    /// </summary>
    /// <param name="url">语音服务器地址，（项目组服务端提供，需要前台传入）</param>
    /// <param name="quality">语音质量，分成1 2 3 数字越大，语音质量越好，当然文件越大</param>
    /// <param name="vedioMaxTime">语音的最长录制时间，当到时间时候，sdk自动提交语音</param>
    public abstract void startRecordVedio(string url, int quality, int vedioMaxTime);
    /// <summary>
    /// 停止录制
    /// <para>录制完成的时候，调用该接口，停止录制语音，sdk，自动上传语音并发送消息个客户端，告诉该条语音的服务器地址，在调用play的时候，返回给sdk，播放录制的语音</para>
    /// </summary>
    public abstract void stopRecordVedio();
    /// <summary>
    /// 播放语音
    /// </summary>
    /// <param name="vedioUrl">播放的url地址，在stop的时候给的url地址</param>
    public abstract void playVedio(string vedioUrl);
    /// <summary>
    /// 停止播放语音
    /// </summary>
    public abstract void stopPlayingVideo();

    #endregion

    #region 其他
    /// <summary>
    /// 异形屏适配接口
    /// <para>在刘海屏显示内容，并返回刘海相关信息</para>
    /// <para>返回Key：notchScreenInfo</para>
    /// </summary>
    public abstract void openNotchScreen();
    /// <summary>
    /// 请求应用信息
    /// </summary>
    public abstract void getAppInfo();
    /// <summary>
    /// 进入用户中心（第三方有用户中心则进入），youkia用户，进入youkia用户中心
    /// </summary>
    public abstract void enterSocial();
    /// <summary>
    /// 得到手机内存
    /// </summary>
    public abstract void getMemory();
    /// <summary>
    /// 得到电池电量
    /// </summary>
    public abstract void getBattery();
    /// <summary>
    /// 判断是否为模拟器
    /// </summary>
    public abstract void isEmulator();
    /// <summary>
    /// 复制内容到其他地方可以粘贴
    /// </summary>
    public abstract void setClipboard(string content);

    #endregion
}