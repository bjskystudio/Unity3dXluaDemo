public enum EBIStep
{
    /// <summary>
    /// 启动unity
    /// </summary>
    Startup = -1900,
    /// <summary>
    /// 启动CG
    /// </summary>
    StarCG = -1910,
    /// <summary>
    /// 启动CG完成
    /// </summary>
    StarCGOver = -1911,
    /// <summary>
    /// 开始请求热更
    /// </summary>
    StartReqUpdateRes = -1950,
    /// <summary>
    /// 成功得到资源大小
    /// </summary>
    ReceiveResSize = -1951,
    /// <summary>
    /// 成功请求下载热更资源
    /// </summary>
    ReqDownRes = -1952,
    /// <summary>
    /// 成功开始下载热更资源
    /// </summary>
    ReceiveStartDown = -1953,
    /// <summary>
    /// 成功下载完成人数
    /// </summary>
    ReceiveDownComplate = -1954,

    /// <summary>
    /// 登陆SDK帐号
    /// </summary>
    SDKLoginUserEx = -3000,
    /// <summary>
    /// SDK帐号登录失败
    /// </summary>
    SDKLoginUserExFailed = -3001,
    /// <summary>
    /// SDK帐号登录取消
    /// </summary>
    SDKLoginUserExCancel = -3002,
    /// <summary>
    /// SDK帐号登录成功
    /// </summary>
    SDKLoginUserExSuccess = -3003,

    /// <summary>
    /// 获取服务器列表
    /// </summary>
    SDKGetServerListInfo = -3010,
    /// <summary>
    /// 得到服务器列表失败
    /// </summary>
    SDKServerListFailed = -3011,
    /// <summary>
    /// 得到服务器列表成功
    /// </summary>
    SDKServerListSuccess = -3012,

    /// <summary>
    /// 查询是否为未成年
    /// </summary>
    SDKIsMinor = -3015,
    /// <summary>
    /// 查询是否为未成年成功
    /// </summary>
    SDKIsMinorSuccess = -3016,
    /// <summary>
    /// 实名认证
    /// </summary>
    SDKRealNameRegister = -3017,
    /// <summary>
    /// 实名认证成功
    /// </summary>
    SDKRealNameRegisterSuccess = -3018,
    /// <summary>
    /// 实名认证失败
    /// </summary>
    SDKRealNameRegisterFailed = -3019,

    /// <summary>
    /// 无目标服务器
    /// </summary>
    NoServerDataAlert = -3020,
    /// <summary>
    /// 维护中的服务器
    /// </summary>
    MaintenanceServer = -3021,
    /// <summary>
    /// 登录挂接中心
    /// </summary>
    SDKLoginServer = -3022,
    /// <summary>
    /// 登录挂接中心失败
    /// </summary>
    SDKLoginServerFailed = -3023,
    /// <summary>
    /// 登录挂接中心失败
    /// </summary>
    SDKLoginServerException = -3024,
    /// <summary>
    /// 登录挂接中心OK
    /// </summary>
    SDKLoginServerSuccess = -3025,

    /// <summary>
    /// 取APP信息
    /// </summary>
    SDKGetAppInfo = -3030,
    /// <summary>
    /// 取APP信息成功
    /// </summary>
    SDKGetAppInfoSuccess = -3031,

    /// <summary>
    /// 获取商品列表成功
    /// </summary>
    SDKGetGoodList = -3035,
    /// <summary>
    /// 获取商品列表有效
    /// </summary>
    SDKGetGoodListSucceed = -3036,

    /// <summary>
    /// 连接游戏服务器
    /// </summary>
    LoginGameServerConnect = -3040,
    /// <summary>
    /// 游戏服务器登录
    /// </summary>
    LoginGameServer = -3041,
    /// <summary>
    /// 游戏服务器登录失败
    /// </summary>
    LoginGameServerFailed = -3042,
    /// <summary>
    /// 游戏服务器版本号失败
    /// </summary>
    LoginServerVersionFailed = -3043,
    /// <summary>
    /// 游戏服务器登录成功
    /// </summary>
    LoginGameServerSuccess = -3044,

    /// <summary>
    /// 取用户数据
    /// </summary>
    LoginGetUserData = -3050,
    /// <summary>
    /// 取用户数据成功
    /// </summary>
    LoginGetUserDataSuccess = -3051,
}