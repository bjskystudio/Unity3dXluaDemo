-----------------------------------------------------------------------
-- File Name:       NetUtil.lua
-- Author:          csw
-- Create Date:     2021/06/03
-- Description:     通信工具类
-----------------------------------------------------------------------

local NetManager = require("NetManager")
--local PopupManager = require("PopupManager")
local Logger = require("Logger")
local ErrorLanguagePackage = require("ErrorLanguagePackage")

local GetLangPackValue = GetLangPackValue
local LanguagePackage = LanguagePackage
local IsNil = IsNil

---@class NetUtil 通信工具类
local NetUtil = {}

---注册消息返回处理方法（处理公共数据）
---@param netMapItem NetMap_Item 通信协议配置
---@param funRecv fun(recvArgs:table, sendArgs:table) 正常消息回调
---@param funcMsg fun(recvArgs:string, sendArgs:table) 异常消息回调
---@param funcErr fun(recvArgs:string, sendArgs:table) 错误消息回调
function NetUtil.Register(netMapItem, funRecv, funcMsg, funcErr)
    NetManager:GetInstance():Register(netMapItem, funRecv, funcMsg, funcErr)
end

---发送请求
---@param netMapItem NetMap_Item 通信协议配置
---@param sendArgs table 发送数据
---@param callOK fun(recvArgs:table, sendArgs:table) 完成回调
---@param callMsg fun(recvArgs:string, sendArgs:table) 异常回调
---@param callError fun(recvArgs:string, sendArgs:table) 错误回调
function NetUtil.Send(netMapItem, sendArgs, callOK, callMsg, callError)
    if not netMapItem then
        Logger.Info(
                {
                    netMapItem = netMapItem,
                    pbData = sendArgs,
                    callOK = callOK
                })
        return
    end
    if sendArgs == nil then
        sendArgs = NetUtil.GetEmptyPB()
    end

    NetManager:GetInstance():Send(netMapItem, sendArgs, callOK, callMsg, callError)
end

---异常提示
function NetUtil.ShowTips(strKey)
    local err = ErrorLanguagePackage[strKey]
    ---@type DialogParam
    local param = {}
    param.Title = GetLangPackValue(LanguagePackage.Common_Title_1)
    param.ShowCancel = 1
    if IsNil(err) then
        param.Content = strKey
    end
    param.SureCallBack = function()
    end
    --PopupManager.ShowSystemDialog(param, err)
    --PopupManager.ShowAlertDialog(GetErrorLangPackValue(ErrorLanguagePackage[strKey]))
end

--region ------------- 数据转换与填充 -------------

---@type pro_single_str
local single_str = { reply = "" }

---返回一个通用的发送空PB
---@return pro_single_str 返回一个通用的发送空PB
function NetUtil.GetEmptyPB()
    return single_str
end

--endregion ----------- 数据转换与填充 -----------

---@return NetUtil
return NetUtil