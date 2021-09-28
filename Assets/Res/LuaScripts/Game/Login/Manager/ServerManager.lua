-----------------------------------------------------------------------
-- File Name:       ServerManager
-- Author:          csw
-- Create Date:     2021/04/27
-- Description:     服务器信息管理
-----------------------------------------------------------------------

local SDKAppInfo = require("SDKAppInfo")
local SDKServerShowInfo = require("SDKServerShowInfo")
local UIManager = require("UIManager")
local Logger = require("Logger")
local PlayerPrefsUtil = require("PlayerPrefsUtil")
local ConfigManager = require("ConfigManager")

local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage
local AppSetting = AppSetting

---@class ServerManager : Singleton 服务器信息管理器
---@field public ServerList SDKServerShowInfo[][] 服务器列表
---@field public TagList string[] 标签列表
---@field public CurServer SDKServerShowInfo 当前选中服务器
---@field public AppInfo SDKAppInfo 当前应用信息
local ServerManager = Class("ServerManager", Singleton)

--这样定义是防止外部访问
---@param ConstKey string 本地存档关键字
local ConstKey = "LoginInfo"
---@param InitTagNum number 预留标签页
local InitTagNum = 2
---@param PageNum number 每页显示数量
local PageNum = 10
---@param RecommendList SDKServerShowInfo[] 推荐服务器列表
local RecommendList = {}
---@param NearList SDKServerShowInfo[] 最近服务器列表
local NearList = {}
---@param LoginList SDKServerShowInfo[] 已注册服务器列表
local LoginList = {}
---@param AllServerList SDKServerShowInfo[] 所有服务器列表
local AllServerList = {}

---创建对象时,入口构造函数
function ServerManager:__init()
    self:SetCurServer(nil)
    self.AppInfo = nil
end

---设置当前服务器
---@param server SDKServerShowInfo 当前服务器
function ServerManager:SetCurServer(server)
    self.CurServer = server
end

--region -------------初始化服务器数据-------------

---初始化本地服务器配置信息
function ServerManager:InitLocalServerList()
    AllServerList = {}
    local cfg = ConfigManager.ServerListConfig

    for _, v in pairs(cfg) do
        local info = SDKServerShowInfo.New()
        info:InitLocalCfgInfo(v)
        AllServerList[#AllServerList + 1] = info
    end

    table.sort(
            AllServerList,
            function(a, b)
                return a.Sid < b.Sid
            end)
    LoginList = {}

    ---@type LocalServerSaveInfo[]
    local tab = PlayerPrefsUtil.GetTable(ConstKey)
    local account_name = ""
    local server_sid = -1

    --兼容旧的配置格式
    if not table.IsNilOrEmpty(tab) then
        if tab.AccountName ~= nil then
            --本地服把上次登录的,当成已存在角色服务器
            LoginList[#LoginList + 1] = self:GetLastServer()
        else
            ---@param value LocalServerSaveInfo info
            for index, value in ipairs(tab) do
                local s = self:GetServerBySid(value.ServerSid)
                if s ~= nil then
                    LoginList[#LoginList + 1] = s
                end
            end
        end
    end

    self:UpdateServerList()
end

---初始化SDK服务器配置信息
---@param json string SDK返回的服务器Json数据
function ServerManager:InitSDKServerList(json)
    --local json_data = _G.Json2Table("{\"servers\":[[\"10001\",\"1服\",0,0,0,\"维护公告\"]],\"serverUserinfo\":[{\"serverid\":\"10001\",\"userinfo\":{\"rolename\":\"1111\",\"rolelevel\":\"1111\"}}],\"currLoginServers\":\"10001\"}")
    local json_data = _G.Json2Table(json)
    Logger.Info("<color=red>" .. "InitSDKServerList" .. "</color>", json_data)

    AllServerList = {}

    --所有服务器
    local temp = json_data["servers"]
    Logger.Info("<color=red>" .. "servers" .. "</color>", temp)
    if temp then
        for _, v in ipairs(temp) do
            local info = SDKServerShowInfo.New()
            info:InitSDKInfo(v)
            AllServerList[#AllServerList + 1] = info
        end
    end

    Logger.Info("<color=red>" .. "AllServerList" .. "</color>", AllServerList)

    --已注册服务器
    LoginList = {}
    temp = json_data["serverUserinfo"]
    Logger.Info("<color=red>" .. "serverUserinfo" .. "</color>", temp)
    if temp then
        for _, v in ipairs(temp) do
            local server_sid = _G.tonumber(v["serverid"])
            ---@type SDKServerShowInfo info
            local info = self:GetServerBySid(server_sid)
            if info then
                info:InitUserInfo(v["userinfo"])
                LoginList[#LoginList + 1] = info
            else
                Logger.Info("<color=red>" .. "注册了一个不存在的server_sid" .. "</color>", server_sid)
            end
        end
    end

    Logger.Info("<color=red>" .. "已注册服务器" .. "</color>", LoginList)

    --最近登录服务器
    NearList = {}
    temp = json_data["currLoginServers"]
    Logger.Info("<color=red>" .. "currLoginServers" .. "</color>", temp)
    if temp then
        local near_server_sids = string.split(temp, "|")
        Logger.Info("<color=red>" .. "near_server_sids" .. "</color>", near_server_sids)

        for i, v in pairs(near_server_sids) do
            local server_sid = _G.tonumber(v)
            ---@type SDKServerShowInfo info
            local info = self:GetServerBySid(server_sid)
            if info then
                NearList[#NearList + 1] = info
            else
                Logger.Info("<color=red>" .. "登录了一个不存在的server_sid" .. "</color>", server_sid)
            end
        end
    end

    Logger.Info("<color=red>" .. "最近登录服务器" .. "</color>", NearList)

    self:UpdateServerList()
end

---初始化登录挂接中心返回的服务器信息
function ServerManager:InitLoginServerInfo(json)
    ---@type SDKServerShowInfo info
    local info = self:GetServerBySid(self.CurServer.Sid)
    if info then
        info:InitLoginServerInfo(json)
    else
        Logger.Info("<color=red>" .. "登录了一个不存在的server_sid" .. "</color>", self.CurServer.Sid)
    end

    self:SetCurServer(info)
end

---初始化登录本地服务器信息
---@param account_name string 本地登录帐号
function ServerManager:InitLocalServerInfo(account_name)
    ---@type SDKServerShowInfo info
    local info = self:GetServerBySid(self.CurServer.Sid)
    --dumpInFile(info, "info")
    if info then
        info.AccountName = account_name
        info.IP = self.CurServer.IP
        info.Port = self.CurServer.Port
    else
        Logger.Info("<color=red>" .. "登录了一个不存在的server_sid" .. "</color>", self.CurServer.Sid)
    end

    self:SetCurServer(info)
end

---更新服务器数据
function ServerManager:UpdateServerList()
    self.TagList = {}
    self.ServerList = {}

    local tag_list = {}
    local server_list = {}

    --已注册服务器
    tag_list[#tag_list + 1] = GetLangPackValue(LanguagePackage.Login_HaveRole)
    server_list[#server_list + 1] = LoginList

    --推荐服务器
    tag_list[#tag_list + 1] = GetLangPackValue(LanguagePackage.Login_BestServer)
    RecommendList = {}

    for i, v in ipairs(AllServerList) do
        if v.IsHot or v.IsNew then
            RecommendList[#RecommendList + 1] = v
        end
    end

    server_list[#server_list + 1] = RecommendList

    --其他服务器分页显示
    local tmpList = {}
    tmpList[1] = AllServerList[#AllServerList]
    local tmpRange = (tmpList[1].Sid - 1) // 10
    local max = tmpList[1].Sid
    local min = tmpList[1].Sid

    for i = #AllServerList - 1, 1, -1 do
        local curSid = AllServerList[i].Sid
        local curRange = (curSid - 1) // 10

        if curRange ~= tmpRange or i == 1 then
            if i == 1 then
                tmpList[#tmpList + 1] = AllServerList[i]
                min = curSid
            end
            server_list[#server_list + 1] = tmpList
            if min % 10 > 1 then
                min = min // 10 * 10 + 1
            end
            if max % 10 ~= 0 then
                max = max // 10 * 10 + 10
            end
            tag_list[#tag_list + 1] = GetLangPackValue(LanguagePackage.Login_ServerArea_1, min, max)
            tmpList = {}
            tmpList[1] = AllServerList[i]
            max = curSid
            min = curSid
        else
            tmpList[#tmpList + 1] = AllServerList[i]
            min = curSid
        end
        tmpRange = curRange
    end

    self.TagList = tag_list
    self.ServerList = server_list
    Logger.Info("<color=red>" .. "TagList" .. "</color>", self.TagList)
    Logger.Info("<color=red>" .. "ServerList" .. "</color>", self.ServerList)
end

---初始化应用信息
function ServerManager:InitAppInfo(json)
    self.AppInfo = SDKAppInfo.New(json)
end

--endregion

--region -------------本地持久化-------------

---获取上次登陆的服务器，没有就给推荐，再没有就给最后一个服务器
---@return SDKServerShowInfo 服务器信息，登录账号
function ServerManager:GetLastServer()
    if AppSetting.IsSDK then
        if NearList and #NearList > 0 then
            return NearList[1]
        elseif RecommendList and #RecommendList > 0 then
            return RecommendList[1]
        end
        return AllServerList[1]
    else
        local tab = PlayerPrefsUtil.GetTable(ConstKey)
        local account_name = ""
        local server_sid = -1
        if not table.IsNilOrEmpty(tab) then
            account_name = tab[1].AccountName
            server_sid = tab[1].ServerSid
        end

        local server_info = nil
        if server_sid == -1 and RecommendList and #RecommendList > 0 then
            server_info = RecommendList[1]
        elseif server_sid ~= -1 then
            server_info = self:GetServerBySid(server_sid)
        end
        if not server_info then
            server_info = AllServerList[1]
        end
        if server_info.AccountName == nil and (string.IsNullOrEmpty(account_name) == false) then
            server_info.AccountName = account_name
            Logger.Info("从本地读取账户名称", account_name)
        end

        return server_info
    end
end

---本地登陆数据存档
function ServerManager:SaveLastServer()
    if not AppSetting.IsSDK then
        ---@type LocalServerSaveInfo[]
        local newSaveList = {}
        ---@type LocalServerSaveInfo
        local cur = {}
        cur.AccountName = self.CurServer.AccountName
        cur.ServerSid = self.CurServer.Sid

        newSaveList[1] = cur
        ---@type LocalServerSaveInfo[]
        local localSaveList = PlayerPrefsUtil.GetTable(ConstKey)
        if localSaveList == nil then
            localSaveList = nil
        else
            local pos = 0

            for index, value in ipairs(localSaveList) do
                if value.ServerSid == cur.ServerSid then
                    pos = index
                end
            end
            if pos ~= 0 then
                table.remove(localSaveList, pos)
            end
        end
        if localSaveList ~= nil then
            table.AddRange(newSaveList, localSaveList)
        end

        PlayerPrefsUtil.SetTable(ConstKey, newSaveList)
    end
end

--endregion

--region -------------功能方法------------

---打开选服界面
---@param info SDKServerShowInfo 上次登录的服务器
function ServerManager:OpenUI(info)
    UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.SelectServerView.Name, self.CurServer, info)
end

---通过Sid查找服务器
---@param server_sid number 服务器sid
function ServerManager:GetServerBySid(server_sid)
    local server_info = table.find(
            AllServerList,
            function(a)
                return a.Sid == server_sid
            end)
    return server_info
end

---根据服务器状态获取显示信息
---@param server_info SDKServerShowInfo 服务器信息
---@return string,string 状态图标名字，状态信息
function ServerManager:GetServerState(server_info)
    local state_icon = ""
    local state_desc = ""
    if not server_info.IsOpen then
        state_icon = "dl_icon_zhuangtai_4"
        state_desc = "<color=#ffffff>" .. GetLangPackValue(LanguagePackage.ServerState_Close) .. "</color>"
    elseif server_info.IsNew then
        state_icon = "dl_icon_zhuangtai_2"
        state_desc = "<color=#47f6a4>" .. GetLangPackValue(LanguagePackage.ServerState_New) .. "</color>"
    elseif server_info.IsHot then
        state_icon = "dl_icon_zhuangtai_1"
        state_desc = "<color=#ed2d3e>" .. GetLangPackValue(LanguagePackage.ServerState_Hot) .. "</color>"
    else
        state_icon = "dl_icon_zhuangtai_2"
        state_desc = "<color=#47f6a4>" .. GetLangPackValue(LanguagePackage.ServerState_Open) .. "</color>"
    end

    return state_icon, state_desc
end

--endregion

---销毁对象时,析构函数
function ServerManager:Dispose()
    ConstKey = nil
    InitTagNum = nil
    PageNum = nil
    InitTagNum = nil
    PageNum = nil
    RecommendList = nil
    NearList = nil
    LoginList = nil
end

---@type ServerManager
_G.ServerManager = ServerManager
---@return ServerManager
return ServerManager
