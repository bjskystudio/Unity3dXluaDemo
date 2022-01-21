#if UNITY_IOS
using System.Runtime.InteropServices;

public class SDKInterfaceIOS : SDKInterface
{
    public override void Init()
    {

    }

    #region 热更

    [DllImport("__Internal")]
    public static extern void _getLocalDynamicUpdatePath();
    public override void getLocalDynamicUpdatePath()
    {
        _getLocalDynamicUpdatePath();
    }

    [DllImport("__Internal")]
    public static extern void _getDynamicUpdate(string type);
    public override void getDynamicUpdate(string type)
    {
        _getDynamicUpdate(type);
    }

    [DllImport("__Internal")]
    public static extern void _downDynamicUpdate();
    public override void downDynamicUpdate()
    {
        _downDynamicUpdate();
    }

    [DllImport("__Internal")]
    public static extern void _updateInfo();
    public override void updateInfo()
    {
        _updateInfo();
    }

    #endregion

    #region 登录

    [DllImport("__Internal")]
    public static extern void _loginEx(string serverSid, int flags);
    public override void loginEx(string serverSid, int flags)
    {
        _loginEx(serverSid, flags);
    }

    [DllImport("__Internal")]
    public static extern void _getServerList();
    public override void getServerList()
    {
        _getServerList();
    }

    [DllImport("__Internal")]
    public static extern void _loginServer(string serverSid);
    public override void loginServer(string serverSid)
    {
        _loginServer(serverSid);
    }

    [DllImport("__Internal")]
    public static extern void _getMaintainNotice();
    public override void getMaintainNotice()
    {
        _getMaintainNotice();
    }

    #endregion

    #region 退出

    [DllImport("__Internal")]
    public static extern void _loginout();
    public override void loginout()
    {
        _loginout();
    }

    [DllImport("__Internal")]
    public static extern void _showExit();
    public override void showExit()
    {
        _showExit();
    }

    [DllImport("__Internal")]
    public static extern void _exit();
    public override void exit()
    {
        _exit();
    }

    #endregion

    #region 防沉迷

    [DllImport("__Internal")]
    public static extern void _getAgreementResult();
    public override void getAgreementResult()
    {
        _getAgreementResult();
    }

    [DllImport("__Internal")]
    public static extern void _showWebAgreementDialog();
    public override void showWebAgreementDialog()
    {
        _showWebAgreementDialog();
    }

    [DllImport("__Internal")]
    public static extern void _isMinor();
    public override void isMinor()
    {
        _isMinor();
    }

    #endregion

    #region 商品

    [DllImport("__Internal")]
    public static extern void _getGoodsList();
    public override void getGoodsList()
    {
        _getGoodsList();
    }

    [DllImport("__Internal")]
    public static extern void _buy(string productId, string gameProductId, string gameProductName);
    public override void buy(string productId, string gameProductId, string gameProductName)
    {
        _buy(productId, gameProductId, gameProductName);
    }

    [DllImport("__Internal")]
    public static extern void _getGoodsList_pro();
    public override void getGoodsList_pro()
    {
        _getGoodsList_pro();
    }

    [DllImport("__Internal")]
    public static extern void _buy_pro(string productId, string gameProductId, string gameProductName, string extra);
    public override void buy_pro(string productId, string gameProductId, string gameProductName, string extra)
    {
        _buy_pro(productId, gameProductId, gameProductName, extra);
    }

    #endregion

    #region 数据上传

    [DllImport("__Internal")]
    public static extern void _gameStepInfo(string step);
    public override void gameStepInfo(int step)
    {
        _gameStepInfo(step.ToString());
    }

    [DllImport("__Internal")]
    public static extern void _gameStepInfoFlag(string step, string flag);
    public override void gameStepInfoFlag(int step, string s)
    {
        _gameStepInfoFlag(step.ToString(), s);
    }

    [DllImport("__Internal")]
    public static extern void _createRole(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra);
    public override void createRole(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {
        _createRole(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra);
    }


    [DllImport("__Internal")]
    public static extern void _enterGame(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra);
    public override void enterGame(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {
        _enterGame(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra);
    }

    [DllImport("__Internal")]
    public static extern void _gameRoleInfo(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra);
    public override void gameRoleInfo(string roleId, string roleName, string roleLevel, string zoneId, string zoneName, string createRoleTime, string extra)
    {
        _gameRoleInfo(roleId, roleName, roleLevel, zoneId, zoneName, createRoleTime, extra);
    }

    [DllImport("__Internal")]
    public static extern void _levelUp(string level);
    public override void levelUp(int level)
    {
        _levelUp(level.ToString());
    }

    #endregion

    #region 推送

    [DllImport("__Internal")]
    public static extern void _pushNotification(string content, int seconds, int jobId);
    public override void pushNotification(string content, int seconds, int jobId)
    {
        _pushNotification(content, seconds, jobId);
    }

    [DllImport("__Internal")]
    public static extern void _cleanAllNotifi(int jobId);
    public override void cleanAllNotifi(int jobId)
    {
        _cleanAllNotifi(jobId);
    }

    [DllImport("__Internal")]
    public static extern void _cleanAllNotifi();
    public override void cleanAllNotifi()
    {
        _cleanAllNotifi();
    }

    [DllImport("__Internal")]
    public static extern void _udpPush(string serverIp, string serverPort, string userid);
    public override void udpPush(string serverIp, int serverPort, string userid)
    {
        _udpPush(serverIp, serverPort.ToString(), userid);
    }

    #endregion

    #region 语音

    [DllImport("__Internal")]
    public static extern void _startRecordVedio(string url, string quality, string vedioMaxTime);
    public override void startRecordVedio(string url, int quality, int vedioMaxTime)
    {
        _startRecordVedio(url, quality.ToString(), vedioMaxTime.ToString());
    }

    [DllImport("__Internal")]
    public static extern void _stopRecordVedio();
    public override void stopRecordVedio()
    {
        _stopRecordVedio();
    }

    [DllImport("__Internal")]
    public static extern void _playVedio(string vedioUrl);
    public override void playVedio(string vedioUrl)
    {
        _playVedio(vedioUrl);
    }

    [DllImport("__Internal")]
    public static extern void _stopPlayingVideo();
    public override void stopPlayingVideo()
    {
        _stopPlayingVideo();
    }

    #endregion

    #region 其他

    [DllImport("__Internal")]
    public static extern void _openNotchScreen();
    public override void openNotchScreen()
    {
        _openNotchScreen();
    }

    [DllImport("__Internal")]
    public static extern void _getAppInfo();
    public override void getAppInfo()
    {
        _getAppInfo();
    }

    [DllImport("__Internal")]
    public static extern void _enterSocial();
    public override void enterSocial()
    {
        _enterSocial();
    }

    [DllImport("__Internal")]
    public static extern void _getMemory();
    public override void getMemory()
    {
        _getMemory();
    }

    [DllImport("__Internal")]
    public static extern void _getBattery();
    public override void getBattery()
    {
        _getBattery();
    }

    [DllImport("__Internal")]
    public static extern void _isEmulator();
    public override void isEmulator()
    {
        _isEmulator();
    }

    [DllImport("__Internal")]
    public static extern void _setClipboard(string content);
    public override void setClipboard(string content)
    {
        _setClipboard(content);
    }

    #endregion
}
#endif