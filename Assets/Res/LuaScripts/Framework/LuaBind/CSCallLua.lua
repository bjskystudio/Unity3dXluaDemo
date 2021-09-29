-----------------------------------------------------------------------
-- Created wangchao on 24. 八月 2019 15:58
--
-- @Description cs与lua 全局委托

local EventManager = require("EventManager")
local EventID = require("EventID")
local CallCSTimerManager = require("CallCSTimerManager")

local tab_unpack = table.unpack
local tableContainsKey = table.ContainsKey
local tableRemove = table.remove

--region ------------- 通用回调绑定 -------------
--[[
使用说明：通过添加一个回调，抓住新生成的绑定ID，传递到c#端抓住。当c#端触发回调时响应绑定ID，从而达到触发lua端的回调。
注意：为了避免内存泄露，只要添加，就必须有移除
--]]
local CSCallLua = {}
_G.CSCallLua = CSCallLua
CSCallLua.AllLuaCall = {}
local ID_LuaCallback_Start = 0

CSCallLua.Call = function(id)
    local fun = CSCallLua.AllLuaCall[id]
    if fun ~= nil then
        fun()
    end
    CSCallLua.RemoveLuaCall(id)
end
CS.CSCallLuaHelp.LuaAction = CSCallLua.Call

---获取新的回调绑定ID
---@return number
CSCallLua.GetNewLuaCallId = function()
    ID_LuaCallback_Start = ID_LuaCallback_Start + 1
    if ID_LuaCallback_Start >= 2147483647 then
        ID_LuaCallback_Start = 1
    end
    return ID_LuaCallback_Start
end

---添加一个可以让c#回调的方法1
---@param callback fun:void
---@return table
CSCallLua.AddLuaCall = function(callback)
    local id = CSCallLua.GetNewLuaCallId()
    CSCallLua.AllLuaCall[id] = callback
    return id
end

---移除指定绑定ID回调
---@param id number
---@return table
CSCallLua.RemoveLuaCall = function(id)
    if tableContainsKey(CSCallLua.AllLuaCall, id) then
        tableRemove(CSCallLua.AllLuaCall, id)
    end
end

--endregion ----------- 通用回调绑定 end -----------

--region -------------点击事件-------------

local BtnClickCsListener = {}
_G.BtnClickCsListener = BtnClickCsListener
local CS = _G.CS

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

--endregion

--region -------------AorText组件绑定-------------

CS.AorText.OnAwake = GetLangPackValue

--endregion

--region -------------资源加载-------------

---@class ResourceManagerCsListener 资源加载类，用于Lua和C#侧交互
_G.ResourceManagerCsListener = {
    ---@param dic table<string,table<function>> 缓存的加载物体的回调
    dic = {},
    ---@param objdic table<string,table<function>> 缓存的加载资源的回调
    objdic = {}
}
-- _G.ResourceManagerCsListener = ResourceManagerCsListener

---AddCalls 加载prefab时注册回调方法
---@param path string 加载prefab路径
---@param fun function 加载prefab成功后的回调
---@return function 加载prefab成功后的回调
function _G.ResourceManagerCsListener.AddCalls(path, fun)
    local calls = _G.ResourceManagerCsListener.dic[path] or {}
    _G.ResourceManagerCsListener.dic[path] = calls
    table.insert(calls, fun)
    return calls
end

---CSCallLuaLoad 映射到C#侧的静态委托，不会在Lua侧调用
function _G.ResourceManagerCsListener.CSCallLuaLoad(path, obj)
    local waite_list = _G.ResourceManagerCsListener.dic[path]
    local c = tableRemove(waite_list, 1)
    c(obj)
end

CS.AssetLoadManager.ConstLuaCallBack = _G.ResourceManagerCsListener.CSCallLuaLoad

-----AddObjectCalls 加载资源时注册回调方法
-----@param path string 加载资源路径
-----@param fun function 加载资源成功后的回调ConstLuaCallBack
-----@return function 加载资源成功后的回调
--function _G.ResourceManagerCsListener.AddObjectCalls(path, fun)
--    local calls = _G.ResourceManagerCsListener.objdic[path] or {}
--    _G.ResourceManagerCsListener.objdic[path] = calls
--    table.insert(calls, fun)
--    return calls
--end

-----CSCallLuaObjectLoad 映射到C#侧的静态委托，不会在Lua侧调用
--function _G.ResourceManagerCsListener.CSCallLuaObjectLoad(path, obj)
--    local waite_list = _G.ResourceManagerCsListener.objdic[path]
--    local c = tableRemove(waite_list, 1)
--    c(obj)
--end

--CS.FrameWork.AssetBundleManager.ConstLuaLoadObjectCallBack = _G.ResourceManagerCsListener.CSCallLuaObjectLoad--endregion

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
---战斗报错注掉 by xin.liu
----region -------------战斗-------------
-----获取战斗信息显示
-----@param index number @0左方 1 右方
-----@return table
--local CSCallGetBattleData = function(index)
--    local data = require("BattleManager"):GetInstance().Data
--    if data then
--        return data:GetBattleState(index)
--    end
--    return nil
--end
--
--CS.DrawBattleGizmos.LuaCallBattleState = CSCallGetBattleData
--
-----获取战斗者数据
-----@param uid string 战斗者uid
-----@return table
--local CSCallGetFightData = function(uid)
--    local data = require("BattleManager"):GetInstance().Data
--    if data then
--        return data:GetFighterState(tonumber(uid))
--    end
--    return nil
--end
--CS.DrawBattleGizmos.LuaCallFightState = CSCallGetFightData
----endregion

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

---战斗定时器报错注掉 by xin.liu
----region -------------c#端战斗定时器, 支持战斗暂停定时器-------------
-----@class BattleTimerManagerCsListener c#端战斗定时器，用于Lua和C#侧交互, 支持战斗暂停定时器
--local BattleTimerManagerCsListener = {
--    ---@param dic table<number,function> 缓存的加载物体的回调
--    dic = {},
--}
--_G.BattleTimerManagerCsListener = BattleTimerManagerCsListener
--
-----添加回调
-----@param callId number 名字
-----@param call fun 回调方法
-----@param obj table lua对象
-----@param bOnce boolean 是否一次
-----@param ... any 参数
--function BattleTimerManagerCsListener.AddCalls(callId, call, obj, bOnce, ...)
--    BattleTimerManagerCsListener.dic[callId] = { call, bOnce, { obj, ... } }
--end
--
-----调用回调
-----@param callId number
--function BattleTimerManagerCsListener.CSCallLua(callId)
--    local callParam = BattleTimerManagerCsListener.dic[callId]
--    if callParam and #callParam > 2 then
--        callParam[1](tab_unpack(callParam[3]))
--    end
--    if callParam and callParam[2] == true then
--        BattleTimerManagerCsListener.dic[callId] = nil
--    end
--end
--
--CS.BattleTimerManager.CallLuaBack = BattleTimerManagerCsListener.CSCallLua
--
-----移除回调
-----@param callId number
--function BattleTimerManagerCsListener.RemoveCall(callId)
--    if BattleTimerManagerCsListener.dic then
--        BattleTimerManagerCsListener.dic[callId] = nil
--    end
--end
--
----endregion

--region ----------循环列表-------------
local LoopListViewListener = {}
_G.LoopListViewListener = LoopListViewListener
LoopListViewListener.RefreshFunction = {}
LoopListViewListener.DragFunction = {}

---添加循环列表刷新事件
---@param loopListView SuperScrollView.LoopListView
---@param getItemEvent fun(index:number):UnityEngine.Transform
function LoopListViewListener.AddListener(loopListView, getItemEvent)
    LoopListViewListener.RefreshFunction[loopListView:GetInstanceID()] = getItemEvent
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
CS.LoopListViewHelp.onDestroyEvent = function(comId)
    LoopListViewListener.RefreshFunction[comId] = nil
    LoopListViewListener.DragFunction[comId] = nil
end

---初测到顶部了
---@param loopListView SuperScrollView.LoopListView
---@param callback fun()
function LoopListViewListener.RegisterToTopListener(loopListView, callback)
    LoopListViewListener.RegisterEndDragListener(loopListView, function()
        if (loopListView.EndDragDelta < 0) then
            if (loopListView.ItemViewFirstIndex == 0) then
                if (callback ~= nil) then
                    callback()
                end
            end
        end
    end)
end

---初测到底部了
---@param loopListView SuperScrollView.LoopListView
---@param callback fun()
function LoopListViewListener.RegisterToBottomListener(loopListView, callback)
    LoopListViewListener.RegisterEndDragListener(loopListView, function()
        if (loopListView.EndDragDelta > 0) then
            if (loopListView.ItemViewLastIndex == loopListView.ItemTotalCount - 1) then
                if (callback ~= nil) then
                    callback()
                end
            end
        end
    end)
end

CS.LoopListViewHelp.onRefreshEvent = function(comId, index)
    local callback = LoopListViewListener.RefreshFunction[comId]
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

---初测到顶部了
---@param loopGridView SuperScrollView.LoopGridView
---@param callback fun()
function LoopGridViewListener.RegisterToTopListener(loopGridView, callback)
    LoopGridViewListener.RegisterEndDragListener(loopGridView, function()
        if (loopGridView.EndDragDelta < 0) then
            if (loopGridView.ItemViewFirstIndex == 0) then
                if (callback ~= nil) then
                    callback()
                end
            end
        end
    end)
end

---初测到底部了
---@param loopGridView SuperScrollView.LoopGridView
---@param callback fun()
function LoopGridViewListener.RegisterToBottomListener(loopGridView, callback)
    LoopGridViewListener.RegisterEndDragListener(loopGridView, function()
        if (loopGridView.EndDragDelta > 0) then
            if (loopGridView.ItemViewLastIndex == loopGridView.ItemTotalCount - 1) then
                if (callback ~= nil) then
                    callback()
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
