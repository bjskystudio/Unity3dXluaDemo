-----------------------------------------------------------------------
-- File Name:       PopupManager.lua
-- Author:          Administrator
-- Create Date:     2022/01/21
-- Description:     描述
-----------------------------------------------------------------------
local ConfigManager = require("ConfigManager")
local UIQueueManager = require("UIQueueManager")
local UIManager = require("UIManager")

---@class DialogParam 对话框参数
---@field public Title string 标题
---@field public Content string 正文内容
---@field public Alignment number 内容排版(左1中2右3)
---@field public SureText string 确定按钮文本
---@field public CancelText string 取消按钮文本
---@field public ShowCancel number 是否显示取消按钮（1不显示:一个按钮, 2显示:两个按钮）
---@field public SureCallBack function 确定回调
---@field public CancelCallBack function 取消回调
---@field public TipsDesc string 小提示内容
---@field public SureDelayDoTime number 确定按钮保护时间
---@field public NoMorePrompts string 本次登录不再提醒
---@field public NoDayPromptsKey string 本日登录不再提醒的本地存储key（本地没有key则创建一个，value为当天零点的时间戳）
---@field public IsClickBgClose boolean 是否点击背景关闭
---@field public TextSize number 文本字体大小(不传默认为18)
---
---@class PopupManager PopupManager
local PopupManager = Class("PopupManager", Singleton)

---本次登录不再提醒缓存
local NoMorePromptsDic = {}
--region ------------- Tips -------------

---飘字
---@public
---@param languageCfg LanguagePackage_Item|ErrorLanguagePackage_Item 本地化条目
function PopupManager.ShowTips(languageCfg, ...)
    if languageCfg == nil then
        Logger.Error("请配置本地化条目!")
        return
    end

    UIQueueManager:GetInstance():AddUIQueue(ConfigManager.UIQueueConfig.CommonTip.Name, formatStrArr(languageCfg.CN, ...))
end

---不同类型的飘字
---@param Tip string 内容
function PopupManager.ShowTipsByType(Tip)
    UIQueueManager:GetInstance():AddUIQueue(ConfigManager.UIQueueConfig.CommonTip.Name, Tip)
end

--endregion ------------- Tips -------------


--region ------------- 对话弹窗 -------------

---通用对话框
---@param param DialogParam tips参数设置
---@param languageCfg LanguagePackage_Item 本地化条目
function PopupManager.ShowAlertDialog(param, languageCfg, ...)
    if param == nil then
        param = {}
    end
    ---如果是支持不再提醒，如果勾选不再提示直接返回，不再弹提示
    if param.NoMorePrompts then
        --if 1 == PlayerPrefsUtil.GetInt(param.NoMorePrompts, 0) then
        --    if param.SureCallBack then
        --        param.SureCallBack()
        --    end
        --    return
        --end
        ---键放在key位，减少找value的循环开销
        if table.ContainsKey(NoMorePromptsDic, param.NoMorePrompts) then
            if param.SureCallBack then
                param.SureCallBack()
            end
            return
        end
    end

    if languageCfg ~= nil then
        --param.Content = formatStrArr(languageCfg.CN, ...)
        param.Content = GetLangPackValue(languageCfg, ...)
    end

    ---@type AlertDialogView
    local view = UIManager:GetInstance():GetOpenedWindow(ConfigManager.UIConfig.AlertDialogView.Name)
    if view ~= nil then
        view:Show(param)
    else
        UIManager:GetInstance():OpenWindow(ConfigManager.UIConfig.AlertDialogView.Name, param)
    end
end

--endregion ----------- 对话弹窗 end -----------
---@return PopupManager
return PopupManager