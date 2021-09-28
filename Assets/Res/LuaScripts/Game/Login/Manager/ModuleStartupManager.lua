-----------------------------------------------------------------------
-- File Name:       ModuleStartupManager
-- Author:          csw
-- Create Date:     2021/05/15
-- Description:     功能初始化管理器
-----------------------------------------------------------------------

---@class ModuleStartupSample 功能模块信息
---@field config FuncInitConfig_Item 配置
---@field waiting table<string,boolean> 是否完成
---@field state number 状态
---@field module Singleton 模块代码实例

local Logger = require("Logger")
local GameLog = require("GameLog")
local ConfigManager = require("ConfigManager")

local LoginModule = GameLog.Module.LoginModule
local isTableEmpty = table.IsNilOrEmpty
local isStringEmpty = string.IsNullOrEmpty
local tableRapidClear = table.RapidClear
local tableCount = table.count
local IsNil = IsNil

---@class ModuleStartupManager : Singleton 功能初始化管理器
---@field private ModuleDic table<string, ModuleStartupSample> 配置好的功能模块
---@field private WaitModules table<string> 等待完成的模块
---@field private WaitCallback function 完成回调
---@field private MaxCount number 模块总数
---@field private CurDoneCount number 当前模块初始化完成数量
---@field private IsRunning boolean 是否进行中
---@field private IsReLogin boolean 是否重新登录
---@field private SendFuncMap table<ModuleStartupSample, number> 发送消息的字典映射，value为最后操作的步骤
local ModuleStartupManager = Class("ModuleStartupManager", Singleton)

---状态
local State = {
    StateInit = 1,
    StateSelf = 2,
    StateDone = 3
}

---创建对象时,入口构造函数
function ModuleStartupManager:__init()
    self.ModuleDic = {}
    self.WaitModules = {}
    self.WaitCallback = nil
    local cfg = ConfigManager.ModuleStartupConfig
    self.MaxCount = 0
    self.CurDoneCount = 0
    self.IsRunning = false
    self.IsReLogin = false

    for key, value in pairs(cfg) do
        ---@type ModuleStartupSample
        local tab = {
            config = value,
            waiting = {},
            state = State.StateInit,
            module = realRequire(key):GetInstance()
        }
        self.ModuleDic[key] = tab

        self.MaxCount = self.MaxCount + 1
    end
end

---是否模块数据初始化成功
function ModuleStartupManager:IsDone()
    return self.CurDoneCount >= self.MaxCount
end

---获取进度显示
function ModuleStartupManager:GetPreDesc()
    return self.CurDoneCount .. "/" .. self.MaxCount, self.CurDoneCount, self.MaxCount
end

---开始初始化功能模块数据
---@param isReLogin boolean 是否重新登录
---@param callback fun():void 完成回调
function ModuleStartupManager:StartInitData(isReLogin, callback)
    if self.IsRunning then
        Logger.Error("功能模块初始化中，不能重复进行")
        return
    end

    self.IsRunning = true
    self.IsReLogin = isReLogin
    self.CurDoneCount = 0
    self.WaitModules = tableRapidClear(self.WaitModules)
    self.WaitCallback = callback

    ---@param tab ModuleStartupSample
    for name, tab in pairs(self.ModuleDic) do
        self.WaitModules[name] = true
        local module = tab.module
        if not module then
            module = realRequire(name):GetInstance()
            tab.module = module
        end

        local wait_list = tab.config.WaitList
        if wait_list then
            for _, v in pairs(wait_list) do
                tab.waiting[v] = true
            end
        end

        tab.state = State.StateInit
    end

    ---@param tab ModuleStartupSample
    for _, tab in pairs(self.ModuleDic) do
        if tableCount(tab.config.WaitList) == 0 then
            GameLog.Info(LoginModule, "<color=red>" .. "不需要等待完成>>>" .. "</color>", tab.config.Sid)
            self:RequestInfo(tab)
        end
    end
end

---请求模块数据
---@private
---@param tab ModuleStartupSample 模块信息
function ModuleStartupManager:RequestInfo(tab)
    local funcNames
    if not self.IsReLogin then
        GameLog.Info(LoginModule, "<color=red>" .. "发送消息>>>" .. "</color>", tab.config.Sid)
        funcNames = tab.config.SendFunc
    else
        GameLog.Info(LoginModule, "<color=red>" .. "发送重连消息>>>" .. "</color>", tab.config.Sid)
        funcNames = tab.config.ReLoginFunc
    end
    if IsNil(funcNames) or isStringEmpty(funcNames) then
        self:OnRequestOver(tab)
    else
        local count = #funcNames
        if count == 0 then
            if not self.IsReLogin then
                Logger.Error("模块%s没有请求方法", tab.config.Sid)
                return
            end
            self:OnRequestOver(tab)
        elseif count == 1 then
            local func = self:GetFunc(tab, funcNames[1])
            if func then
                func(function()
                    self:OnRequestOver(tab)
                end)
            else
                self:OnRequestOver(tab)
            end
        else
            self.SendFuncMap = self.SendFuncMap or {}
            local lastStep = self.SendFuncMap[tab]
            if lastStep == nil then
                lastStep = 1
            else
                lastStep = lastStep + 1
            end
            self.SendFuncMap[tab] = lastStep
            local func = self:GetFunc(tab, funcNames[lastStep])
            if lastStep == count then
                if func then
                    func(function()
                        self:OnRequestOver(tab)
                        self.SendFuncMap[tab] = nil
                    end)
                else
                    self:OnRequestOver(tab)
                    self.SendFuncMap[tab] = nil
                end
            else
                if func then
                    func(function()
                        self:RequestInfo(tab)
                    end)
                else
                    self:RequestInfo(tab)
                end
            end
        end
    end
end

---收到模块数据
---@private
---@param tab ModuleStartupSample 模块信息
function ModuleStartupManager:OnRequestOver(tab)
    GameLog.Info(LoginModule, "<color=red>" .. "收到消息>>>" .. "</color>", tab.config.Sid)
    tab.state = State.StateSelf
    if isTableEmpty(tab.waiting) then
        self:InitFunc(tab)
    end
end

---初始化模块
---@private
---@param tab ModuleStartupSample 模块信息
function ModuleStartupManager:InitFunc(tab)
    GameLog.Info(LoginModule, "初始化", tab.config.Sid)
    local funcName = tab.config.InitFunc
    if not isStringEmpty(funcName) then
        local func = self:GetFunc(tab, funcName)
        func()
    end

    tab.state = State.StateDone
    self:OnInitFuncOver(tab)
end

---初始化模块完成
---@private
---@param tab ModuleStartupSample 模块信息
function ModuleStartupManager:OnInitFuncOver(tab)
    local name = tab.config.Sid
    GameLog.Info(LoginModule, "初始化完成", name)
    self.CurDoneCount = self.CurDoneCount + 1
    GameLog.Info(LoginModule, self.CurDoneCount .. "/" .. self.MaxCount)
    if self.WaitModules[name] then
        self.WaitModules[name] = nil
        if isTableEmpty(self.WaitModules) then
            if self.WaitCallback ~= nil then
                self.WaitCallback()
                self.WaitCallback = nil
            end

            GameLog.Info(LoginModule, "全部模块初始化完成", "<color=red>" .. ">>>" .. "</color>")
            self.IsRunning = false
            self.IsReLogin = false
            return
        end
    else
        Logger.Error("重复初始化模块", name)
        return
    end

    for _, tab in pairs(self.ModuleDic) do
        if tab.waiting[name] then
            tab.waiting[name] = nil
            if isTableEmpty(tab.waiting) then
                if tab.state == State.StateSelf then
                    self:InitFunc(tab)
                elseif tableCount(tab.config.WaitList) > 0 then
                    self:RequestInfo(tab)
                end
            end
        end
    end
end

---获取方法
---@private
---@param tab ModuleStartupSample 模块信息
---@param funcName string 方法名称
function ModuleStartupManager:GetFunc(tab, funcName)
    local module = tab.module
    if not module then
        Logger.Error("模块%s为空", tab.config.Sid)
        return
    end

    local func = module[funcName]
    if not func and not self.IsReLogin then
        Logger.Error("模块%s没有%s方法", tab.config.Sid, funcName)
        return
    end
    return func
end

---清理
---@private
---@param tab ModuleStartupSample 模块信息
function ModuleStartupManager:Clear(tab)
    GameLog.Info(LoginModule, "<color=red>" .. "清理数据>>>" .. "</color>", tab.config.Sid)
    local funcName = tab.config.ClearFunc
    if not isStringEmpty(funcName) then
        local func = self:GetFunc(tab, funcName)
        func()
    end

    tab.state = State.StateInit
end

---销毁对象时,析构函数
function ModuleStartupManager:Dispose()
    if self.ModuleDic ~= nil then
        for _, v in pairs(self.ModuleDic) do
            self:Clear(v)
        end
        self.ModuleDic = tableRapidClear(self.ModuleDic)
        self.ModuleDic = nil
    end
    if self.WaitModules ~= nil then
        self.WaitModules = tableRapidClear(self.WaitModules)
        self.WaitModules = nil
    end
    self.WaitCallback = nil
end

---@return ModuleStartupManager
return ModuleStartupManager
