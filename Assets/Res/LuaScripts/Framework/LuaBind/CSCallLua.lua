-----------------------------------------------------------------------
-- Created wangchao on 24. 八月 2019 15:58
--
-- @Description cs与lua 全局委托

local EventManager = require("EventManager")
local EventID = require("EventID")
local CallCSTimerManager = require("CallCSTimerManager")

local tab_unpack = table.unpack
local tableRemove = table.remove
local CS = _G.CS
local GetLangPackValue = GetLangPackValue

--region ------------- 通用回调绑定 -------------
--[[
    使用说明：通过添加一个回调，抓住新生成的绑定ID，传递到c#端抓住。当c#端触发回调时响应绑定ID，从而达到触发lua端的回调。
    注意：为了避免内存泄露，只要添加，就必须有移除(除了Once,因为已经自动处理)
--]]
local CSCallLua = {}
_G.CSCallLua = CSCallLua
CSCallLua.AllLuaCall = {}
CSCallLua.AllLuaCallType = {}
local LuaCallID = 0

---获取新的回调绑定ID
---@return number
CSCallLua.GetNewLuaCallId = function()
    LuaCallID = LuaCallID + 1
    if LuaCallID >= 2147483647 then
        LuaCallID = 1
    end
    --已绑定ID重新要一个
    if CSCallLua.AllLuaCallType[LuaCallID] ~= nil then
        return CSCallLua.GetNewLuaCallId()
    end
    return LuaCallID
end

---添加一个可以让c#回调的方法
---@param callback function 回调
---@param isOnce boolean 是否一次性
---@return number 绑定ID
CSCallLua.AddLuaCall = function(callback, isOnce)
    if isOnce == nil then
        isOnce = true
    end
    local callID = CSCallLua.GetNewLuaCallId()
    CSCallLua.AllLuaCallType[callID] = isOnce
    CSCallLua.AllLuaCall[callID] = callback
    return callID
end

---移除绑定回调
---@param callID function 绑定ID
function CSCallLua.RemoveLuaCall(callID)
    CSCallLua.AllLuaCall[callID] = nil
    CSCallLua.AllLuaCallType[callID] = nil
end

---绑定到CS侧的方法
CSCallLua.Call = function(callID, ...)
    local isOnce = CSCallLua.AllLuaCallType[callID]
    local fun = CSCallLua.AllLuaCall[callID]
    if fun ~= nil then
        fun(...)
    end
    if isOnce == true then
        CSCallLua.RemoveLuaCall(callID)
    end
end
--绑定到C#侧
CS.CSCallLuaHelp.CallLua = CSCallLua.Call
CS.CSCallLuaHelp.CallLuaInt = CSCallLua.Call
CS.CSCallLuaHelp.CallLuaStr = CSCallLua.Call
CS.CSCallLuaHelp.CallLuaFloat = CSCallLua.Call
CS.CSCallLuaHelp.CallLuaGameObject = CSCallLua.Call
CS.CSCallLuaHelp.CallLuaAssetResRef = CSCallLua.Call
CS.CSCallLuaHelp.CallLuaTransInt = CSCallLua.Call

--绑定CS侧方法
local CSRegisterFunAction = CS.CSCallLuaHelp.RegisterFunAction
local CSUnregisterFunAction = CS.CSCallLuaHelp.UnregisterFunAction
--提醒，绑定功能回调，是需要自己维护移除的
---添加一个指定c#功能的非一次性回调方法
---@param funID number C#侧功能ID，在CSCallLuaHelp.cs中定义，可以通过API调用
---@param callback function 回调
---@return number 绑定ID
CSCallLua.AddLuaCallToCSFun = function(funID, callback)
    local callID = CSCallLua.AddLuaCall(callback, false)
    CSRegisterFunAction(funID, callID)
    return callID
end

---添加一个指定c#功能的非一次性回调方法
---@param funID number C#侧功能ID，在CSCallLuaHelp.cs中定义，可以通过API调用
---@param callID number 绑定ID
CSCallLua.RemoveLuaCallToCSFun = function(funID, callID)
    CSCallLua.RemoveLuaCall(callID)
    CSUnregisterFunAction(funID)
end

--endregion ----------- 通用回调绑定 end -----------

--region ------------- 键盘输入事件 -------------

--local UnityInputListener = {}
--function UnityInputListener.CsCallLuaInputHandle(key)
--    EventManager:GetInstance():Broadcast(EventID.UnityInput, key)
--end
--CS.GameManager.GlobalInputHandler = UnityInputListener.CsCallLuaInputHandle

--endregion ----------- 键盘输入事件 end -----------

--region -------------点击事件-------------

local BtnClickCsListener = {}
_G.BtnClickCsListener = BtnClickCsListener

function BtnClickCsListener.CsCallLuaBtnClick(go_table, go, name)
    if go_table._onClickBtn ~= nil then
        go_table._onClickBtn(go, name)
    end
end

CS.UIGoTable.luaOnClickBtn = BtnClickCsListener.CsCallLuaBtnClick

function BtnClickCsListener.CsCallLuaToggleClick(go_table, go, isOn, name)
    if go_table._onClickToggle ~= nil then
        go_table._onClickToggle(go, isOn ~= 0, name)
    end
end

CS.UIGoTable.luaOnClickToggle = BtnClickCsListener.CsCallLuaToggleClick

function BtnClickCsListener.CsCallLuaTmpClick(go_table, go, linkId)
    if go_table._onClickTmp ~= nil then
        go_table._onClickTmp(go, linkId)
    end
end
CS.UIGoTable.luaOnClickTmp = BtnClickCsListener.CsCallLuaTmpClick
--endregion

--region -------------Text组件绑定-------------

CS.AorText.OnAwake = GetLangPackValue
CS.AorTMP.OnAwake = GetLangPackValue
CS.AorTMP3D.OnAwake = GetLangPackValue

--endregion

--region -------------CS事件派发-------------

local BroadcastToLua = function(eventname, ...)
    EventManager:GetInstance():Broadcast(EventID[eventname], ...)
end
_G.CS.CSEventToLuaHelp.BroadcastToLua = BroadcastToLua

--endregion

--region -------------对象池-------------

---@class PoolsManagerCsListener 对象池加载类
local PoolsManagerCsListener = {
    ---@param dic table<string,table<function>> 对象池加载物体的回调
    dic = {}
}
_G.PoolsManagerCsListener = PoolsManagerCsListener

---AddCalls 从对象池获取物体时，注册加载方法
---@param path string 加载对象路径
---@param fun function 加载对象成功后的回调
---@return function 加载对象成功后的回调
function PoolsManagerCsListener.AddCalls(path, fun)
    local calls = PoolsManagerCsListener.dic[path] or {}
    PoolsManagerCsListener.dic[path] = calls
    table.insert(calls, fun)
    return calls
end

function PoolsManagerCsListener.CSCallLuaLoad(path, obj)
    local waite_list = PoolsManagerCsListener.dic[path]
    local c = tableRemove(waite_list, 1)
    c(obj)
end

--CS.FrameWork.FPoolManager.ConstLuaCallBack = PoolsManagerCsListener.CSCallLuaLoad

--endregion

--region -------------战斗-------------
---获取战斗信息显示
---@param index number @0左方 1 右方
---@return table
local CSCallGetBattleData = function(index)
    local data = require("BattleManager"):GetInstance().Data
    if data then
        return data:GetBattleState(index)
    end
    return nil
end

CS.DrawBattleGizmos.LuaCallBattleState = CSCallGetBattleData

---获取战斗者数据
---@param uid string 战斗者uid
---@return table
local CSCallGetFightData = function(uid)
    local data = require("BattleManager"):GetInstance().Data
    if data then
        return data:GetFighterState(tonumber(uid))
    end
    return nil
end
CS.DrawBattleGizmos.LuaCallFightState = CSCallGetFightData
---开始战报战斗
local CSCallStartLogBattle = function(msg)
    require("BattleServerMsgManager"):GetInstance():StartLogBattle(msg)
end
CS.DrawBattleGizmos.LuaCallStartLogBattle = CSCallStartLogBattle

---保存战报战斗数据
local SaveLogBattleData = function(msg, fileName, path)
    local pbManager = require("PBManager"):GetInstance()
    local util = require("util")
    local ConfigManager = require("ConfigManager")
    local NetMap = ConfigManager.NetMap
    ---@type pro_unity_bin_log
    local data = pbManager:Decode("unity_bin_log", msg)
    local resultStr = string.format("local %s = \n{\np2c_fs_info ={", fileName)
    for i = 1, #data.p2c_fs_info do
        local info = pbManager:Decode(NetMap.fs_info.ServerPB, data.p2c_fs_info[i])
        resultStr = resultStr .. _G.serpent.getdumptext(info, { nocode = true, name = nil }) .. ","
    end
    resultStr = resultStr .. string.format("\n}\n}\nreturn %s", fileName)
    util.file_save(path, resultStr)
end

CS.DrawBattleGizmos.LuaCallSaveLogBattle = SaveLogBattleData

local EncodeLogBattle = function(fileName)
    local pbManager = require("PBManager"):GetInstance()
    local data = require(fileName)
    local ConfigManager = require("ConfigManager")
    local NetMap = ConfigManager.NetMap
    local sendArgs = {}
    sendArgs["p2c_fs_info"] = {}
    for i = 1, #data.p2c_fs_info do
        local bytes = pbManager:Encode(NetMap.fs_info.ServerPB, data.p2c_fs_info[i])
        table.insert(sendArgs["p2c_fs_info"], bytes)
    end
    return pbManager:Encode("unity_bin_log", sendArgs)
end

CS.DrawBattleGizmos.LuaCallEncodeLogBattle = EncodeLogBattle
--endregion

--region -------------c#端定时器-------------
---@class TimerManagerCsListener c#端定时器，用于Lua和C#侧交互
local TimerManagerCsListener = {
    ---@param dic table<string,function> 缓存的加载物体的回调
    dic = {},
}
_G.TimerManagerCsListener = TimerManagerCsListener

---添加回调
---@param timerName string 名字
---@param call fun 回调方法
---@param obj table lua对象
---@param one_shot boolean 是否一次
---@param ... any 参数
function _G.TimerManagerCsListener.AddCalls(timerName, call, obj, one_shot, ...)
    TimerManagerCsListener.dic[timerName] = { call, one_shot, { obj, ... } }
end

---调用回调
function _G.TimerManagerCsListener.CSCallLua(timerName)
    local callParam = TimerManagerCsListener.dic[timerName]
    if callParam and #callParam > 2 then
        callParam[1](tab_unpack(callParam[3]))
    end
    if callParam and callParam[2] == true then
        CallCSTimerManager:GetInstance():RemoveTimer(timerName)
    end
end

CS.CSTimerManager.ConstTimerLuaCallBack = _G.TimerManagerCsListener.CSCallLua

---移除回调
function _G.TimerManagerCsListener.RemoveCall(timerName)
    if TimerManagerCsListener.dic then
        TimerManagerCsListener.dic[timerName] = nil
    end
end

--endregion

--region -------------c#端战斗定时器, 支持战斗暂停定时器-------------
---@class BattleTimerManagerCsListener c#端战斗定时器，用于Lua和C#侧交互, 支持战斗暂停定时器
local BattleTimerManagerCsListener = {
    ---@param dic table<number,function> 缓存的加载物体的回调
    dic = {},
}
_G.BattleTimerManagerCsListener = BattleTimerManagerCsListener

---添加回调
---@param callId number 名字
---@param call fun 回调方法
---@param obj table lua对象
---@param bOnce boolean 是否一次
---@param ... any 参数
function BattleTimerManagerCsListener.AddCalls(callId, call, obj, bOnce, ...)
    if callId == nil then
        return
    end
    BattleTimerManagerCsListener.dic[callId] = { call, bOnce, { obj, ... } }
end

---调用回调
---@param callId number
function BattleTimerManagerCsListener.CSCallLua(callId)
    if callId == nil then
        return
    end
    local callParam = BattleTimerManagerCsListener.dic[callId]
    if callParam and #callParam > 2 then
        callParam[1](tab_unpack(callParam[3]))
    end
    if callParam and callParam[2] == true then
        BattleTimerManagerCsListener.dic[callId] = nil
    end
end

CS.BattleTimerManager.CallLuaBack = BattleTimerManagerCsListener.CSCallLua

---移除回调
---@param callId number
function BattleTimerManagerCsListener.RemoveCall(callId)
    if callId and BattleTimerManagerCsListener.dic then
        BattleTimerManagerCsListener.dic[callId] = nil
    end
end

--endregion

--region ----------循环列表-------------
local LoopListViewListener = {}
_G.LoopListViewListener = LoopListViewListener
LoopListViewListener.RefreshFunction = {}
LoopListViewListener.DragFunction = {}
LoopListViewListener.SelectCallBack = {}
---添加循环列表刷新事件
---@param loopListView SuperScrollView.LoopListView
---@param getItemEvent fun(index:number):UnityEngine.Transform
function LoopListViewListener.AddListener(loopListView, getItemEvent)
    LoopListViewListener.RefreshFunction[loopListView:GetInstanceID()] = getItemEvent
end
---添加无限循环列表的中间选中事件
function LoopListViewListener.AddSelectLister(loopListView, call)
    LoopListViewListener.SelectCallBack[loopListView:GetInstanceID()] = call
end
---移除循环列表刷新事件
---@param loopListView SuperScrollView.LoopListView
function LoopListViewListener.RemoveListener(loopListView)
    LoopListViewListener.RefreshFunction[loopListView:GetInstanceID()] = nil
end

---注册停止拖拽事件
---@param loopListView SuperScrollView.LoopListView
---@param callback fun()
function LoopListViewListener.RegisterEndDragListener(loopListView, callback)
    local comId = loopListView:GetInstanceID()
    LoopListViewListener.DragFunction[comId] = callback
    CS.LoopListViewHelp.RegisterEndDragEvent(loopListView, comId)
end

---移除停止拖拽事件
---@param loopListView SuperScrollView.LoopListView
function LoopListViewListener.UnRegisterEndDragListener(loopListView)
    local comId = loopListView:GetInstanceID()
    LoopListViewListener.DragFunction[comId] = nil
    CS.LoopListViewHelp.UnRegisterEndDragEvent(loopListView)
end

---初测到顶或到底了
---@param loopListView SuperScrollView.LoopListView
---@param callback fun(IsToTop:boolean)
function LoopListViewListener.RegisterToTopAndBottomListener(loopListView, callback)
    LoopListViewListener.RegisterEndDragListener(loopListView, function()
        if (loopListView.EndDragDelta < 0) then
            if (loopListView.ItemViewFirstIndex == 0) then
                if (callback ~= nil) then
                    callback(true)
                end
            end
        elseif (loopListView.EndDragDelta > 0) then
            if (loopListView.ItemViewLastIndex == loopListView.ItemTotalCount - 1) then
                if (callback ~= nil) then
                    callback(false)
                end
            end
        end
    end)
end

CS.LoopListViewHelp.onDestroyEvent = function(comId)
    LoopListViewListener.RefreshFunction[comId] = nil
    LoopListViewListener.DragFunction[comId] = nil
end

CS.LoopListViewHelp.onRefreshEvent = function(comId, index)
    local callback = LoopListViewListener.RefreshFunction[comId]
    if (callback ~= nil) then
        return callback(index)
    end
end

CS.LoopListViewHelp.OnCenterDragEvent = function(comId, index)
    local callback = LoopListViewListener.SelectCallBack[comId]
    if (callback ~= nil) then
        return callback(index)
    end
end

CS.LoopListViewHelp.onEndDragEvent = function(comId)
    local func = LoopListViewListener.DragFunction[comId]
    if (func ~= nil) then
        func()
    end
end
--endregion

--region ------------循环网格列表----------------
local LoopGridViewListener = {}
_G.LoopGridViewListener = LoopGridViewListener
LoopGridViewListener.RefreshFunction = {}
LoopGridViewListener.DragFunction = {}

---开始监听更新事件
---@param loopGridView SuperScrollView.LoopGridView
---@param callback fun()
function LoopGridViewListener.AddRefreshListener(loopGridView, callback)
    LoopGridViewListener.RefreshFunction[loopGridView:GetInstanceID()] = callback
end

---结束监听更新事件
---@param loopGridView SuperScrollView.LoopGridView
function LoopGridViewListener.RemoveRefreshListener(loopGridView)
    LoopGridViewListener.RefreshFunction[loopGridView:GetInstanceID()] = nil
end

---注册结束事件监听
---@param loopGridView SuperScrollView.LoopGridView
---@param callback fun()
function LoopGridViewListener.RegisterEndDragListener(loopGridView, callback)
    local comId = loopGridView:GetInstanceID()
    LoopGridViewListener.DragFunction[comId] = callback
    CS.LoopGridViewHelp.RegisterEndDragEvent(loopGridView, comId)
end

---注销结束事件监听
---@param loopGridView SuperScrollView.LoopGridView
function LoopGridViewListener.UnRegisterEndDragListener(loopGridView)
    local comId = loopGridView:GetInstanceID()
    LoopGridViewListener.DragFunction[comId] = nil
    CS.LoopGridViewHelp.UnRegisterEndDragEvent(loopGridView)
end

---初测到顶或到底了
---@param loopGridView SuperScrollView.LoopGridView
---@param callback fun(IsToTop:boolean)
function LoopGridViewListener.RegisterToTopAndBottomListener(loopGridView, callback)
    LoopGridViewListener.RegisterEndDragListener(loopGridView, function()
        if (loopGridView.EndDragDelta < 0) then
            if (loopGridView.ItemViewFirstIndex == 0) then
                if (callback ~= nil) then
                    callback(true)
                end
            end
        elseif (loopGridView.EndDragDelta > 0) then
            if (loopGridView.ItemViewLastIndex == loopGridView.ItemTotalCount - 1) then
                if (callback ~= nil) then
                    callback(false)
                end
            end
        end
    end)
end

---C#view销毁时
CS.LoopGridViewHelp.onDestroyEvent = function(comId)
    LoopGridViewListener.RefreshFunction[comId] = nil
    LoopGridViewListener.DragFunction[comId] = nil
end

---C#刷新事件回调
CS.LoopGridViewHelp.onRefreshEvent = function(comId, index)
    local callback = LoopGridViewListener.RefreshFunction[comId]
    if (callback) then
        return callback(index)
    end
end

---C#拖拽结束事件回调
CS.LoopGridViewHelp.onEndDragEvent = function(comId)
    local func = LoopGridViewListener.DragFunction[comId]
    if (func ~= nil) then
        func()
    end
end
--endregion
