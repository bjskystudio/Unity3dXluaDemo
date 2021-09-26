-------------------------------------------------------
-- File Name:       UIManager.lua
-- Description:     UI管理单例类
-- Author:          TangHuan,csw
-- Create Date:     2021/03/30
-------------------------------------------------------

local UIData = require("UIData")
local EUIType = require("EUIType")
local EventManager = require("EventManager")
local GameLog = require("GameLog")
local ViewActiveState = require("ViewActiveState")
local EventID = require("EventID")
local ConfigManager = require("ConfigManager")
local Logger = require("Logger")

local UIConfig = ConfigManager.UIConfig
local UIModule = GameLog.Module.UIModule
local tableCount = table.count
local handler = handler
local tableInsert = table.insert
local tableRemovebyvalue = table.removebyvalue
local AppSetting = AppSetting
local Bool2Num = Bool2Num

---@class UIManager : Singleton @UI管理单例类
---@field public UICamera UnityEngine.Camera @UI摄像机
---@field public BlurCamera UnityEngine.Camera @BlurUI摄像机
---@field public SceneUIRoot UnityEngine.Transform @SceneUIRoot根节点
---@field public NormalUIRoot UnityEngine.Transform @NormalUI根节点
---@field public BlurUIRoot UnityEngine.Transform @BlurUI根节点
---@field private ViewRootMap table<EUIType, UnityEngine.Transform> @层级节点映射Map(Key为层级值，Value为对应层级节点)
---@field private LayerAllOpenedViewMap table<EUIType, table<string, BaseUIView>> @UI Layer对应所有已打开的窗口集合(Key为UI Layer值,Value为该Layer已打开窗口映射Map--为了获取Layer最高Order时避免不必要的窗口遍历)
---@field private AllOpenedViewList BaseUIView[] @当前所有已打开的窗口列表(用列表的原因是为了确保和打开顺序一致)
---@field private NextTopFullScreenView BaseUIView @下一个Order最高的全屏(全屏策略有用)
---@field private BlurCount number @快速计算已经模糊掉的UI数量
local UIManager = Class("UIManager", Singleton)

---@private
function UIManager:__init()
    self.ViewRootMap = {}
    self.LayerAllOpenedViewMap = {}
    self.BlurCount = 0

    for _, uilayer in pairs(EUIType) do
        self.LayerAllOpenedViewMap[uilayer] = {}
    end

    self.AllOpenedViewList = {}
    self:InitUIRoot()
end

---初始化UI根节点
---@private
function UIManager:InitUIRoot()
    self.UICamera = CSUIModel.UICamera
    self.BlurCamera = CSUIModel.BlurCamera
    self.SceneUIRoot = CSUIModel.SceneUIRoot
    self.NormalUIRoot = CSUIModel.NormalUIRoot
    self.BlurUIRoot = CSUIModel.BlurUIRoot

    self.ViewRootMap[EUIType.NormalUI] = CSUIModel.NormalUIRoot
    self.ViewRootMap[EUIType.ConstUI] = CSUIModel.ConstUIRoot
end

---打开指定窗口
---@param viewname string @窗口名
---@param ... any @窗口传参
---@return BaseUIView @窗口对象
function UIManager:OpenWindow(viewname, ...)
    if not self:IsWindowOpened(viewname) then
        -- Note:
        -- 1. 相对Order窗口参考的窗口必须打开的情况下才允许打开当前窗口
        ---@type UIConfig_Item
        local uiconfig = UIConfig[viewname]
        assert(uiconfig, "找不到窗口:%s信息!", viewname)
        if uiconfig.RelativeViewName ~= "" then
            if not self:IsWindowOpened(uiconfig.RelativeViewName) then
                Logger.Error("窗口名:%s参考窗口:%s未打开,不允许打开!", viewname, uiconfig.RelativeViewName)
                return nil
            end
        end

        local uiviewparentnode = self:GetLayerTransform(uiconfig.UIType)
        local viewclass = require(uiconfig.Name)
        -- 获取窗口Order必须在添加到LayerAllOpenedViewMap之前,因为里面会访问
        local vieworder = self:GetUIViewWindowOrder(uiconfig)

        -- 进入模糊
        if uiconfig.IsBlur then
            self:SetCurViewBlur(true)
        end

        -- 每一次都New一个对象来支持异步加载等概念
        ---@type BaseUIView
        local viewinstance = viewclass.New()
        tableInsert(self.AllOpenedViewList, viewinstance)
        self.LayerAllOpenedViewMap[uiconfig.UIType][uiconfig.Name] = viewinstance
        GameLog.Info(UIModule, "UIType:%s打开窗口:%s", uiconfig.UIType, uiconfig.Name)
        GameLog.Info(UIModule, "UIType:%s当前已打开窗口数量:%s", uiconfig.UIType, tableCount(self.LayerAllOpenedViewMap[uiconfig.UIType]))
        viewinstance:_Init(uiconfig, uiviewparentnode, vieworder, handler(self, self.OnWindowOpenCompleted), ...)
        EventManager:GetInstance():Broadcast(EventID.OnViewOpened, uiconfig.Name)
        return viewinstance
    else
        Logger.Error("窗口名:%s已打开,无法重复打开!", viewname)
        return nil
    end
end

---窗口完全打开回调(OnShow之后)
---@private
---@param viewinstance BaseUIView @打开的窗口
function UIManager:OnWindowOpenCompleted(viewinstance)
    EventManager:GetInstance():Broadcast(EventID.OnViewCompletedOpened, viewinstance)
    --- 处理全屏窗口优化
    self:DoFullScreenStrategy(viewinstance, true)
end

---关闭指定窗口
---@return boolean @是否关闭成功
function UIManager:CloseWindow(viewname)
    if self:IsWindowOpened(viewname) then
        ---@type UIConfig_Item
        local uiconfig = UIConfig[viewname]
        local viewinstance = self.LayerAllOpenedViewMap[uiconfig.UIType][viewname]
        local openedwindow = self.LayerAllOpenedViewMap[viewinstance.ViewConfig.UIType][viewname]
        tableRemovebyvalue(self.AllOpenedViewList, openedwindow)
        self.LayerAllOpenedViewMap[viewinstance.ViewConfig.UIType][viewname] = nil
        GameLog.Info(UIModule, "关闭窗口:%s", viewname)
        GameLog.Info(UIModule, "UIType:%s当前已打开窗口数量:%s", viewinstance.ViewConfig.UIType, tableCount(self.LayerAllOpenedViewMap[viewinstance.ViewConfig.UIType]))
        GameLog.Info(UIModule, "当前已打开窗口数量:%s", tableCount(self.AllOpenedViewList))
        -- 移除后再处理，避免对自身进行重复判定设置
        -- 只有加载完成后才会处理全屏窗口打开，所以关闭也得确保是资源加载完成后关闭才需要处理
        if viewinstance.IsLoaded then
            self:DoFullScreenStrategy(viewinstance, false)
        end

        -- 移出模糊
        if uiconfig.IsBlur then
            self:SetCurViewBlur(false)
        end

        viewinstance:Destroy()
        EventManager:GetInstance():Broadcast(EventID.OnViewClosed, viewname)
        return true
    else
        Logger.Error("窗口名:%s未打开,无法关闭!", viewname)
        return false
    end
end

---关闭所有窗口
function UIManager:CloseAllWindow()
    for i = #self.AllOpenedViewList, 1, -1 do
        self:CloseWindow(self.AllOpenedViewList[i].ViewName)
    end
end

---定义主窗口类型
local MainWinType = {
    [UIConfig.MainView.Name] = true,
    [UIConfig.LoginView.Name] = true,
}

---关闭除了主窗口以外的所有窗口
function UIManager:CloseAllExceptMainWindow()
    for i = #self.AllOpenedViewList, 1, -1 do
        if not MainWinType[self.AllOpenedViewList[i].ViewName] then
            self:CloseWindow(self.AllOpenedViewList[i].ViewName)
        end
    end
end

---回退到指定界面
---@param viewname string @窗口名
function UIManager:BackToWindow(viewname, ...)
    for i = #self.AllOpenedViewList, 1, -1 do
        if self.AllOpenedViewList[i].ViewName == viewname then
            self.AllOpenedViewList[i]:OnRefresh(...)
            return
        end
        self:CloseWindow(self.AllOpenedViewList[i].ViewName)
    end
    self:OpenWindow(viewname, ...)
end

---指定窗口是否打开
---@param viewname string @窗口名
---@return boolean @指定窗口是否打开
function UIManager:IsWindowOpened(viewname)
    return self:GetOpenedWindow(viewname) ~= nil
end

---获取指定已打开窗口
---@param viewname string @窗口名
---@return BaseUIView @已打开的对应窗口
function UIManager:GetOpenedWindow(viewname)
    local uilayer = UIConfig[viewname].UIType
    return self.LayerAllOpenedViewMap[uilayer][viewname]
end

--- 获取指定UILayer的挂载节点
---@private
---@param uilayer EUIType @UI层级
---@return UnityEngine.Transform @指定UILayer对应的挂载节点
function UIManager:GetLayerTransform(uilayer)
    return self.ViewRootMap[uilayer]
end

---获取指定UI层级已打开窗口Order最高的值(不含固定Order窗口,没有有效窗口则返回0)
---@private
---@param uilayer number @UI层级
---@return number @获取指定UI层级Order最高的值(不含固定Order窗口,没有有效窗口则返回0)
function UIManager:GetUILayerHighestWindowOrder(uilayer)
    local highestorder = 0

    ---@param viewinstance BaseUIView
    for _, viewinstance in pairs(self.LayerAllOpenedViewMap[uilayer]) do
        if not viewinstance:IsUsingConstOrder() then
            if viewinstance.ViewOrder > highestorder then
                highestorder = viewinstance.ViewOrder
            end
        end
    end
    return highestorder
end

---获取指定窗口信息的窗口的Order
---@private
---@param viewconfig UIConfig_Item @窗口配置信息
---@return number @获取指定窗口信息的窗口的Order
function UIManager:GetUIViewWindowOrder(viewconfig)
    local windowcanvasorder = 0
    if viewconfig.UIType == EUIType.ConstUI then
        -- 常驻窗口Order读取指定的Order配置
        windowcanvasorder = viewconfig.ConstOrder
        if AppSetting.IsEditor and windowcanvasorder <= 0 then
            Logger.Error("常驻窗口ConstOrder不能为0", viewconfig.Name)
        end
    elseif viewconfig.UIType == EUIType.NormalUI then
        -- 普通窗口Order有两种情况：
        -- 未指定相对Order窗口规则如下:
        -- 窗口Order = 当前Layer最高Order窗口(不含固定Order窗口)(没有窗口则为Layer起始Order) + 单窗口之前相差的SortingOrder
        -- 指定相对Order窗口规则如下:
        -- 窗口Order = 相对窗口Order + 相对偏移Order设置

        if viewconfig.RelativeViewName == "" then
            local uilayerhighestorder = self:GetUILayerHighestWindowOrder(viewconfig.UIType)
            if uilayerhighestorder ~= 0 then
                windowcanvasorder = uilayerhighestorder + UIData.PerWindowOrder
            else
                windowcanvasorder = UIData.LayerStartOrder
            end
        else
            local relativewindow = self:GetOpenedWindow(viewconfig.RelativeViewName)
            -- 设置了相对Order的直接按照设定的相对值取值
            windowcanvasorder = relativewindow.ViewOrder + viewconfig.ConstOrder
        end
    end
    GameLog.Info(UIModule, "UIType:%s获取Window Order:%s", viewconfig.UIType, windowcanvasorder)
    return windowcanvasorder
end

---处理全屏窗口策略
---@private
---@param viewinstance BaseUIView @对应窗口实体对象
---@param isopen boolean @是否是打开(反之关闭)
function UIManager:DoFullScreenStrategy(viewinstance, isopen)
    -- 原本打算采用改Layer的方式来做全屏窗口优化，但粒子不受根节点Layer修改影响,又不想通过GetComponentsInChidren的方式来设置Layer
    -- 所以修改成SetActive的方式来做全屏优化(SetActive支持标志位来表示显隐原因来支持不同的设置显隐缘由)
    if viewinstance.ViewConfig.IsFullScreen then
        -- 策略如下:
        -- 1. 如果是打开全屏窗口就找到当前Order最高的全屏作为Active状态设置参考对象
        -- 2. 如果是关闭全屏窗口就找到排除当前关闭窗口以外的Order最高的全屏作为Active状态设置参考对象
        isopen = isopen or false
        ---@type BaseUIView
        local nexttopmostfullscreenwindow = nil

        for i = #self.AllOpenedViewList, 1, -1 do
            local openedview = self.AllOpenedViewList[i]
            if (isopen or (not isopen and viewinstance ~= openedview)) and openedview.ViewConfig.IsFullScreen then
                if nexttopmostfullscreenwindow == nil or (nexttopmostfullscreenwindow ~= nil and nexttopmostfullscreenwindow.ViewOrder < openedview.ViewOrder) then
                    nexttopmostfullscreenwindow = openedview
                end
            end
        end

        self.NextTopFullScreenView = nexttopmostfullscreenwindow
        if self.NextTopFullScreenView ~= nil then
            GameLog.Info(UIModule, "下一个有效全屏窗口:%s", self.NextTopFullScreenView.ViewName)
        else
            GameLog.Info(UIModule, "无下一个有效全屏窗口!")
        end

        for i = #self.AllOpenedViewList, 1, -1 do
            ---@type BaseUIView
            local uiview = self.AllOpenedViewList[i]
            if self.NextTopFullScreenView ~= nil then
                if uiview.ViewOrder < self.NextTopFullScreenView.ViewOrder then
                    uiview:SetActive(false, ViewActiveState.FullScreenStrategyState)
                    GameLog.Info(UIModule, "全屏窗口:%s 操作:%s 设置已打开窗口:%s的隐藏标志位!", viewinstance.ViewName, isopen, uiview.ViewName)
                else
                    uiview:SetActive(true, ViewActiveState.FullScreenStrategyState)
                    GameLog.Info(UIModule, "全屏窗口:%s 操作:%s 设置已打开窗口:%s的隐藏标志位!", viewinstance.ViewName, isopen, uiview.ViewName)
                end
            else
                -- 没有有效全屏窗口说明是关闭全屏窗口后找不到任何全屏窗口了
                uiview:SetActive(true, ViewActiveState.FullScreenStrategyState)
                GameLog.Info(UIModule, "全屏窗口:%s 操作:%s 设置已打开窗口:%s的隐藏标志位!", viewinstance.ViewName, isopen, uiview.ViewName)
            end
        end
    else
        -- 策略如下:
        -- 1. 如果打开的是非全屏窗口需要根据当前下一个最高全屏窗口决定新开窗口的Active状态,确保新开窗口处于正确的Active状态
        if self.NextTopFullScreenView ~= nil and isopen then
            if self.NextTopFullScreenView.ViewOrder > viewinstance.ViewOrder then
                viewinstance:SetActive(false, ViewActiveState.FullScreenStrategyState)
                GameLog.Info(UIModule, "非全屏窗口:%s 操作:%s 低于下一个全屏窗口:%sOrder 设置已打开窗口:%s的隐藏标志位!", viewinstance.ViewName, isopen, self.NextTopFullScreenView.ViewName, viewinstance.ViewName)
            end
        end
    end
end

---设置模糊
---@param value boolean @设置是否模糊
---@private
function UIManager:SetCurViewBlur(value)
    local openedview = nil
    if value then
        for i = #self.AllOpenedViewList, 1, -1 do
            local uiview = self.AllOpenedViewList[i]
            if uiview.ViewConfig.UIType == EUIType.NormalUI then
                openedview = uiview
                break
            end
        end
    else
        for i = #self.AllOpenedViewList, 1, -1 do
            local uiview = self.AllOpenedViewList[i]
            if uiview:GetBlurState() then
                openedview = uiview
                break
            end
        end
    end

    if openedview ~= nil then
        openedview:SetBlur(value)
        if value then
            self.BlurCount = self.BlurCount + 1
        else
            self.BlurCount = self.BlurCount - 1
            if self.BlurCount < 0 then
                self.BlurCount = 0
                if AppSetting then
                    Logger.Error("<color=red> self.BlurCount>>> </color>", self.AllOpenedViewList)
                end
            end
        end
        --Logger.Info("<color=red> SetCurViewBlur>>> </color>", openedview.ViewName, value, self.BlurCount)
    end
    self.SceneUIRoot:SetActive(Bool2Num(self.BlurCount <= 0))
    self.BlurCamera:SetActive(Bool2Num(self.BlurCount > 0))
end

---析构函数
---@private
function UIManager:Dispose()
    self:CloseAllWindow()
    self.UICamera = nil
    self.BlurCamera = nil
    self.SceneUIRoot = nil
    self.NormalUIRoot = nil
    self.BlurUIRoot = nil
    self.ViewRootMap = nil
    self.LayerAllOpenedViewMap = nil
    self.AllOpenedViewList = nil
    self.NextTopFullScreenView = nil
end

---@return UIManager @UI管理单例类
return UIManager
