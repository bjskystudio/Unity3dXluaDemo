-----------------------------------------------------------------------
-- File Name:       EventID
-- Author:          csw
-- Create Date:     2021/04/28
-- Description:     事件枚举定义，注意：只可以定义事件相关枚举，且是string属性，且所有字段不能重复
-----------------------------------------------------------------------

local EventNumberUtils = require("EventNumberUtils")

---@class EventID
local EventID = {
    --region ------------- 公共 -------------
    ---每秒通知
    SecondsUpdateMsg = EventNumberUtils.NewEventId(),
    --endregion ----------- 公共 end -----------

    --region ------------- 测试事件 -------------
    TestEvent1 = EventNumberUtils.NewEventId(),
    TestEvent2 = EventNumberUtils.NewEventId(),
    --endregion ----------- 测试事件 end -----------

    --region ------------- 遮罩 -------------
    ---显示通信遮罩
    ConnectMask = EventNumberUtils.NewEventId(),
    ---显示通信动画
    ConnectAnimMask = EventNumberUtils.NewEventId(),
    ---打开UI前的遮罩
    UIMask = EventNumberUtils.NewEventId(),
    --endregion ----------- 遮罩 end -----------

    --region ------------- 网络连接 -------------
    ---Lua端开始对远程主机连接
    ConnectStart = EventNumberUtils.NewEventId(),
    ---Lua端连接成功
    ConnectOK = EventNumberUtils.NewEventId(),
    ---Lua端连接失败
    ConnectFail = EventNumberUtils.NewEventId(),
    ---Lua端连接关闭
    ConnectClose = EventNumberUtils.NewEventId(),
    ---Lua端重连成功
    ReconnectComplete = EventNumberUtils.NewEventId(),
    ---Lua端发出队列消息
    SendMsg = EventNumberUtils.NewEventId(),
    ---Lua端发出消息
    SendParallelMsg = EventNumberUtils.NewEventId(),
    ---Lua端接收队列消息
    ReceiveMsg = EventNumberUtils.NewEventId(),
    ---Lua端接收消息
    ReceiveParallelMsg = EventNumberUtils.NewEventId(),
    ---Lua端接收数据出错
    ReceiveMsgError = EventNumberUtils.NewEventId(),
    ---后台需求，登录时需要等待后台向SDK请求数据，防沉迷验证通过才允许登录
    FangchenmiCheckPass = EventNumberUtils.NewEventId(),
    ---主动断开连接
    BreakConnect = EventNumberUtils.NewEventId(),
    --endregion ----------- 网络连接 end -----------

    --region ------------- 登录 -------------
    ---登录状态变更
    LoginStateChanged = EventNumberUtils.NewEventId(),
    ---登录服务器成功
    LoginServerSucceed = EventNumberUtils.NewEventId(),
    ---创建角色成功
    CreateRoleSucceed = EventNumberUtils.NewEventId(),
    ---获取角色信息成功
    GetUserInfoSucceed = EventNumberUtils.NewEventId(),
    ---登录初始化数据完毕完成,进入主界面
    LoginComplete = EventNumberUtils.NewEventId(),
    --endregion ----------- 登录 end -----------

    --region ------------- UI -------------
    ---界面打开完成(纯逻辑打开时抛和OnUIClosed配对)
    OnViewOpened = EventNumberUtils.NewEventId(),
    ---界面完全打开完成(资源加载完成OnShow之后抛)
    OnViewCompletedOpened = EventNumberUtils.NewEventId(),
    ---界面逻辑关闭完成
    OnViewClosed = EventNumberUtils.NewEventId(),
    ---界面激活显示(Note:第一次打开窗口不会触发)
    OnViewShow = EventNumberUtils.NewEventId(),
    ---界面隐藏显示
    OnViewHide = EventNumberUtils.NewEventId(),
    ---显示自动导航文本(show:boolean)
    ShowAutoEnvTip = EventNumberUtils.NewEventId(),
    --endregion ----------- UI end -----------

}
_G.EventID = EventID
---@type EventID
return EventID
