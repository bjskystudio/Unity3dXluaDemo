-------------------------------------------------------
-- File Name:       SDKDef.lua
-- Description:     SDK枚举类
-- Author:          csw
-- Create Date:     2020/07/09
-------------------------------------------------------

local EventNumberUtils = require("EventNumberUtils")

---@class SDKDef SDK枚举类
---@field GetMsg SDKDef_GetMsg 接收SDK消息
---@field BIStep SDKDef_BIStep BI步骤
local SDKDef = {}

---@class SDKDef_GetMsg 接收SDK消息
local GetMsg = {

    --region -------------登录-------------

    --login/loginEx
    ---用户登录成功
    login_success = EventNumberUtils.NewEventId(),
    ---用户登录失败
    login_failed = EventNumberUtils.NewEventId(),
    ---用户登录取消
    login_cancle = EventNumberUtils.NewEventId(),

    --getServerList
    ---获取服务器列表成功
    get_server_list_success = EventNumberUtils.NewEventId(),
    ---获取服务器列表失败
    get_server_list_failed = EventNumberUtils.NewEventId(),

    --loginServer
    ---登录挂接中心成功
    auth_success = EventNumberUtils.NewEventId(),
    ---登录挂接中心失败
    auth_failed = EventNumberUtils.NewEventId(),

    ---获取维护信息成功(长度限制128bit)
    get_maintainnotice = EventNumberUtils.NewEventId(),

    ---得到应用的信息
    get_app_info = EventNumberUtils.NewEventId(),

    --endregion

    --region -------------退出-------------

    --loginout
    ---注销
    auth_logout = EventNumberUtils.NewEventId(),

    --showExit
    ---showExit后，如果返回show_exit，说明渠道没有退出框，需要调用游戏内的
    show_exit = EventNumberUtils.NewEventId(),

    --endregion

    --region -------------防沉迷-------------

    ---实名认证成功
    realname_success = EventNumberUtils.NewEventId(),
    ---此账号已经绑定
    realname_failed = EventNumberUtils.NewEventId(),
    ---是否未成年返回
    --- -1： 未获取到数据时的默认值
    ---0： 未认证
    ---1： <8岁
    ---2： 8岁> 年龄 <16岁
    ---3： 16岁> 年龄 < 18岁
    ---4： >18岁
    fangchenmi_flag = EventNumberUtils.NewEventId(),

    --endregion

    --region -------------商品-------------

    --getGoodsList
    ---得到充值商品列表
    pay_goods_list = EventNumberUtils.NewEventId(),

    --getGoodsList_pro
    ---得到限时商品列表
    pay_goods_list_pro = EventNumberUtils.NewEventId(),

    ---在调用充值页面的时候，发送youkia唯一充值订单号，主要为了处理 活动在0点之前点击充值，0点之后完成充值导致活动问题
    get_change_order = EventNumberUtils.NewEventId(),

    --buy/buy_pro
    ---支付成功
    pay_success = EventNumberUtils.NewEventId(),
    ---支付失败
    pay_failed = EventNumberUtils.NewEventId(),

    --endregion

    --region -------------功能-------------

    record_vedio_sucess = EventNumberUtils.NewEventId(),
    record_vedio_failed = EventNumberUtils.NewEventId(),

    ---重新激活游戏
    show_game = EventNumberUtils.NewEventId(),

    ---获取内存（totalMem：手机总内存（单位MB），availMem：手机可用内存（单位MB））
    get_memory = EventNumberUtils.NewEventId(),

    ---得到当前电池电量百分比
    current_battery = EventNumberUtils.NewEventId(),

    --endregion

}
SDKDef.GetMsg = GetMsg

---@class SDKDef_BIStep BI步骤
local BIStep = {

    ---登陆SDK帐号
    SDKLoginUserEx = -3000,
    ---SDK帐号登录失败
    SDKLoginUserExFailed = -3001,
    ---SDK帐号登录取消
    SDKLoginUserExCancel = -3002,
    ---SDK帐号登录成功
    SDKLoginUserExSuccess = -3003,

    ---获取服务器列表
    SDKGetServerListInfo = -3010,
    ---得到服务器列表失败
    SDKServerListFailed = -3011,
    ---得到服务器列表成功
    SDKServerListSuccess = -3012,

    ---查询是否为未成年
    SDKIsMinor = -3015,
    ---查询是否为未成年成功
    SDKIsMinorSuccess = -3016,
    ---实名认证
    SDKRealNameRegister = -3017,
    ---实名认证成功
    SDKRealNameRegisterSuccess = -3018,
    ---实名认证失败
    SDKRealNameRegisterFailed = -3019,

    ---无目标服务器
    NoServerDataAlert = -3020,
    ---维护中的服务器
    MaintenanceServer = -3021,
    ---登录挂接中心
    SDKLoginServer = -3022,
    ---登录挂接中心失败
    SDKLoginServerFailed = -3023,
    ---登录挂接中心失败
    SDKLoginServerException = -3024,
    ---登录挂接中心OK
    SDKLoginServerSuccess = -3025,

    ---取APP信息
    SDKGetAppInfo = -3030,
    ---取APP信息
    SDKGetAppInfoSuccess = -3031,

    ---获取商品列表成功
    SDKGetGoodList = -3035,
    ---获取商品列表有效
    SDKGetGoodListSucceed = -3036,

    ---连接游戏服务器
    LoginGameServerConnect = -3040,
    ---游戏服务器登录
    LoginGameServer = -3041,
    ---游戏服务器登录失败
    LoginGameServerFailed = -3042,
    ---游戏服务器版本号失败
    LoginServerVersionFailed = -3043,
    ---游戏服务器登录成功
    LoginGameServerSuccess = -3044,

    ---取用户数据
    LoginGetUserData = -3050,
    ---取用户数据成功
    LoginGetUserDataSuccess = -3051,
}
SDKDef.BIStep = BIStep

---@return SDKDef SDKDef
return SDKDef