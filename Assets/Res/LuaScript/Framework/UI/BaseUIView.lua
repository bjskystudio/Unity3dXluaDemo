-------------------------------------------------------
-- File Name:       BaseUIView.lua
-- Author:          TangHuan,csw
-- Create Date:     2021/03/30
-- Description:     窗口基类

-- UI窗口流程介绍:
-- 1. __init()                         -- 构造函数调用
-- 2. _Init()                          -- 初始化
-- 3. LoadRes()                        -- 加载资源
-- 4. OnLoadResCompleted()             -- 资源加载完成
-- 5. UpdateOrder()                    -- 更新Order
-- 6. InitCommonComponent()            -- 初始化通用组件
-- 7. Awake()                          -- Awake(子类重写实现)
-- 8. OnCreate()                       -- 窗口显示(子类重写实现)
-- 9. OnRefresh()                       -- 窗口二次打开(子类重写实现)
-- 10. OnEnable()                       -- 组件可用(子类重写实现)
-- 11. OnDisable()                     -- 组件不可用(子类重写实现)
-- 12. Destroy()                       -- 窗口清理销毁
-- 13. ClearRootViewComponent()        -- 清除根组件
-- 14. OnDestroy()                     -- 自定义清理(子类重写实现)
-- 15. RemoveAllListener()             -- 移除所有事件监听

-- UI窗口组件使用介绍:
-- 1. 静态绑定子组件接口(BaseUIView:GetOrAddComponent())
-- 3. 子组件移除销毁接口(BaseUIView:RemoveComponentInstance() or BaseViewComponent:Close())
-------------------------------------------------------

local EventManager = require("EventManager")
local BaseViewComponent = require("BaseViewComponent")
local UIData = require("UIData")
local GameLog = require("GameLog")
local ViewActiveState = require("ViewActiveState")
local EventID = require("EventID")
local Logger = require("Logger")
local UIManager = require("UIManager")
local AssetLoadManager = require("AssetLoadManager")
--local PopupManager = require("PopupManager")
local ConfigManager = require("ConfigManager")
local CurrencyBar = require("CurrencyBar")
local GameUtil = require("GameUtil")
local GlobalDefine = require("GlobalDefine")


local TexCoord1 = CS.UnityEngine.AdditionalCanvasShaderChannels.TexCoord1
local UIModule = GameLog.Module.UIModule
local UpdateUIGameObjectOrder = CS.UIUtils.UpdateUIGameObjectOrder
local GetAutoGoTable = GetAutoGoTable
local LanguagePackage = LanguagePackage
local handler = handler
local AppSetting = AppSetting
local IsNil = IsNil
local typeof = typeof
local GraphicRaycaster = GraphicRaycaster
local Canvas = Canvas
local table_pack = table.pack
local table_unpack = table.unpack
local string_format = string.format
local GameObject = GameObject

---@class BaseUIView @窗口基类
---@field public ViewName string @窗口名
---@field public ViewConfig UIConfig_Item @窗口配置信息
---@field private ParentNode UnityEngine.Transform @挂载父节点
---@field public ViewOrder number @窗口Order
---@field private OnOpencompletedCB fun(view:BaseUIView) @窗口完全打开后回调(OnCreate之后--用于支持异步资源加载确保窗口真正打开)
---@field private Datas table @参数数据
---@field public IsLoaded boolean @是否加载完成
---@field public IsOpened boolean @窗口是否打开
---@field private HideFlag ViewActiveState @隐藏标志位
---@field private ViewInstance UnityEngine.GameObject @窗口实体资源对象
---@field public ViewTransform UnityEngine.Transform @窗口实体资源对象节点
---@field private ViewCanvas UnityEngine.Canvas @窗口Canvas组件
---@field private ViewRootComponent BaseViewComponent @窗口根组件(用于支持嵌套子组件增删)
---@field protected EventIDList table<EventID> @窗口注册的监听事件ID集合
---@field private go_table FullView_GoTable|PopupView_GoTable @窗口组件绑定对象
---@field protected HaveCloseAni boolean @是否有关闭动画
---@field private IsBlur boolean @是否被模糊
---@field protected CurrencyBar CurrencyBar @通用资源栏组件
---@field private CurrentHelpKeyIndex number 当前帮助文本键值索引
local BaseUIView = Class("BaseUIView")

---窗口构造函数
---@private
function BaseUIView:__init()
    self.IsLoaded = false
    self.IsOpened = false
    self.HaveCloseAni = false
    self.IsBlur = false
    self.HideFlag = ViewActiveState.None
    self.CurrentHelpKeyIndex = self.CurrentHelpKeyIndex or 1
end

---是否可用(动态加载在未加载完成前被使用可能需要判定)
---@return boolean @是否可用
function BaseUIView:IsAvalible()
    return self.IsOpened == true and self.IsLoaded == true
end

---是否可见(必须通过此接口判定窗口显隐)
---@return boolean @是否可见
function BaseUIView:IsActive()
    return self.HideFlag == ViewActiveState.None
end

---设置可见
---@param active boolean @设置是否可见
---@param flag ViewActiveState @设置显隐原因标志位
function BaseUIView:SetActive(active, flag)
    active = active or false
    flag = flag or ViewActiveState.LogicalState
    local preactive = self:IsActive()
    if active == false then
        self.HideFlag = self.HideFlag | flag
    else
        local hasflag = (self.HideFlag & flag > 0)
        if hasflag then
            local reverseflag = ~flag
            self.HideFlag = self.HideFlag & reverseflag
        end
    end

    GameLog.Info(UIModule, "窗口名:%s 显隐操作:%s 标志位:%s 最终隐藏标志位:%s!", self.ViewName, active, flag, self.HideFlag)
    local isactive = self:IsActive()
    --if self:IsAvalible() and self.ViewInstance.activeSelf ~= isactive then
    --    self.ViewInstance:SetActive(Bool2Num(isactive))
    --end
    if preactive ~= isactive then
        if self:IsAvalible() then
            self.ViewInstance:SetActive(Bool2Num(isactive))
        end
        if isactive then
            self:OnEnable()
            EventManager:GetInstance():Broadcast(EventID.OnViewShow, self, flag)
        else
            self:OnDisable()
            EventManager:GetInstance():Broadcast(EventID.OnViewHide, self, flag)
        end
    end
end

---设置模糊
---@param value boolean @设置是否模糊
function BaseUIView:SetBlur(value)
    self.IsBlur = value
    if value then
        self.ViewInstance:SetParent(UIManager:GetInstance().BlurUIRoot)
    else
        self.ViewInstance:SetParent(UIManager:GetInstance().NormalUIRoot)
    end
end

---是否已被模糊
---@return boolean
function BaseUIView:GetBlurState()
    return self.IsBlur
end

---是否采用固定Order
---@return boolean @是否采用固定Order
function BaseUIView:IsUsingConstOrder()
    return self.IsOpened == true and self.ViewConfig.RelativeViewName ~= ""
end

---初始化窗口(子类请勿使用)
---@private
---@param viewconfig UIConfig_Item @窗口信息
---@param parentnode UnityEngine.Transform @窗口挂载节点
---@param vieworder number @窗口Order
---@param onopencompletedcb fun(BaseUIView) @窗口完全打开回调(OnCreate之后)
---@param ... any @窗口自定义参数
function BaseUIView:_Init(viewconfig, parentnode, vieworder, onopencompletedcb, ...)
    self.ViewName = viewconfig.Name
    self.ViewConfig = viewconfig
    self.ParentNode = parentnode
    self.ViewOrder = vieworder
    self.OnOpencompletedCB = onopencompletedcb
    self.Datas = table_pack(...)
    self.IsLoaded = false
    self.IsOpened = true
    self:LoadRes()
end

---资源加载
---@private
function BaseUIView:LoadRes()
    AssetLoadManager:GetInstance():LoadObj(self.ViewConfig.ResPath, AssetLoadManager.AssetType.ePrefab, true, handler(self, self.OnLoadResCompleted))
end

---资源加载完成回调
---@private
---@param go UnityEngine.GameObject @资源实体对象
function BaseUIView:OnLoadResCompleted(go)
    if not self.IsOpened then
        Logger.Warning("窗口已关闭，直接销毁加载完成资源对象:%s!", go.name)
        -- 资源加载回来后子组件已经无效了，直接删除实体对象
        go:DestroyGameObj()
        return
    end

    self.IsLoaded = true
    self.ViewInstance = go
    self.ViewTransform = self.ViewInstance.transform
    self.ViewTransform:SetParent(self.ParentNode, false)
    self.ViewInstance:SetActive(self:IsActive())
    self:CreateViewRootComponent()
    self.go_table = GetAutoGoTable(self.ViewInstance, handler(self, self.OnBaseClickBtn), handler(self, self.OnClickToggle))
    self:UpdateOrder()
    self:InitCommonComponent()
    self:Awake()
    --self.ViewInstance:SetAnimatorTrigger("Open")
    --GameUtil.PlayOneShotAuido(GlobalDefine.eAudioSid.JieMianDakai)
    EventManager:GetInstance():Register(self)
    self:OnCreate(table_unpack(self.Datas))
    self:OnEnable()
    if self.OnOpencompletedCB ~= nil then
        self.OnOpencompletedCB(self)
        self.OnOpencompletedCB = nil
    end
end

---更新Order
---@private
function BaseUIView:UpdateOrder()
    local canvas = self.ViewInstance:GetComponent(typeof(Canvas))
    if IsNil(canvas) then
        Logger.LogFatal("%s 自行添加Canvas组件", self.ViewName)
    end
    if AppSetting.IsEditor == true then
        local gr = self.ViewInstance:GetComponent(typeof(GraphicRaycaster))
        if IsNil(gr) then
            Logger.LogFatal("%s 自行添加 GraphicRaycaster", self.ViewName)
        end
    end

    -- 设置它的canvas深度
    canvas.overrideSorting = true
    canvas.additionalShaderChannels = TexCoord1
    canvas.sortingOrder = self.ViewOrder
    GameLog.Info(UIModule, "UIType:%s窗口名:%s设置Order:%s", self.ViewConfig.UIType, self.ViewName, self.ViewOrder)
    UpdateUIGameObjectOrder(self.ViewInstance, UIData.PerWindowOrder, true)
end

---模拟Unity Component的 Awake
---@protected
function BaseUIView:Awake()
end

---窗口显示(子类重写自定义每个参数)
---@protected
---@param ... any @窗口传参
function BaseUIView:OnCreate(...)
end

---窗口二次打开(子类重写自定义每个参数)
---@protected
function BaseUIView:OnRefresh(...)

end

---可用
---@protected
function BaseUIView:OnEnable()
end

---不可用
---@protected
function BaseUIView:OnDisable()
end

---清除根组件(递归清除嵌套组件绑定)
function BaseUIView:ClearRootViewComponent()
    if self.ViewRootComponent ~= nil then
        self.ViewRootComponent:Destroy()
        self.ViewRootComponent = nil
    end
end

---窗口清理销毁
function BaseUIView:Destroy()
    self.IsOpened = false
    self:ClearRootViewComponent()
    --资源加载未完成的情况下不走清理流程(资源异步加载才有可能发生)
    if self.IsLoaded == true then
        self:OnDisable()
        self:OnDestroy()
        self:RemoveAllListener()
        self.ViewInstance:SetAnimatorTrigger("Close")
        --GameUtil.PlayOneShotAuido(GlobalDefine.eAudioSid.JieMianGuanBi)
        if self.HaveCloseAni then
            self.ViewInstance:DestroyGameObjDelay(0.2)
        else
            self.ViewInstance:DestroyGameObj()
        end
    else
        Logger.Error("出现未加载完成又被关闭的情况!", self.ViewName)
    end

    self.ViewName = nil
    self.ViewConfig = nil
    self.ParentNode = nil
    self.ViewOrder = nil
    self.OnOpencompletedCB = nil
    self.Datas = nil
    self.IsLoaded = nil
    self.IsOpened = nil
    self.ViewInstance = nil
    self.ViewTransform = nil
    self.ViewCanvas = nil
    self.ChildWidgetList = nil
    self.go_table = nil
    self.CurrencyBar = nil
    self.HaveCloseAni = nil
    self.CurrentHelpKeyIndex = nil
end

---窗口清理(子类重写实现自定义清理流程)
---@protected
function BaseUIView:OnDestroy()
end

---关闭自身(子类重写实现自定义关闭流程)
---@protected
function BaseUIView:Close()
    UIManager:GetInstance():CloseWindow(self.ViewName)
end

---点击Button回调
---@private
function BaseUIView:OnBaseClickBtn(btn)
    self:OnClickBtn(btn)
    if self.IsOpened then
        if btn == self.go_table.aorbtn_close then
            self:Close()
        elseif self.ViewConfig.IsFullScreen then
            if btn == self.go_table.aorbtn_home then
                UIManager:GetInstance():BackToWindow(ConfigManager.UIConfig.MainView.Name)
            elseif btn == self.go_table.aorbtn_more then
                --PopupManager.ShowTips(LanguagePackage.Common_ComingSoon)
            elseif btn == self.go_table.aorbtn_help then
                if table.IsNilOrEmpty(self.ViewConfig.HelpKey) then
                    Logger.Error("该界面未配置HelpKey")
                    return
                end
                if self.CurrentHelpKeyIndex < 1 or self.CurrentHelpKeyIndex > #self.ViewConfig.HelpKey then
                    Logger.Error("当前帮助文本键值索引越界，Key: ", self.CurrentHelpKeyIndex)
                    return
                end
                --PopupManager.ShowHelp(self.ViewConfig.HelpKey[self.CurrentHelpKeyIndex])
            end
        else
            if btn == self.go_table.aorbtn_anyClose then
                self:Close()
            end
        end
    end
end

---点击Button回调,子类扩展使用
---@protected
---@param btn AorButton 按钮
function BaseUIView:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function BaseUIView:OnClickToggle(toggle, isOn)
end

--region ------------- 通用组件模块 -------------

---初始化通用组件
---@protected
function BaseUIView:InitCommonComponent()
    if self.ViewConfig.IsFullScreen and not IsNil(self.go_table.obj_CurrencyBar) then
        self.CurrencyBar = self:GetOrAddComponent(self.go_table.obj_CurrencyBar, CurrencyBar)
        self.CurrencyBar:ShowChatBtn(self.ViewConfig.IsShowChat)
        self.CurrencyBar:UpdateResList(self.ViewConfig.ResList)
    elseif not self.ViewConfig.IsFullScreen then
        self.HaveCloseAni = true
    end
end

---修改标题
---@param title string 标题
function BaseUIView:UpdateTitle(title)
    if not IsNil(self.go_table.aortext_title) then
        self.go_table.aortext_title.text = title
    end
end

---是否显示聊天按钮
---@param isShowChatBtn boolean 是否显示
function BaseUIView:ShowChatBtn(isShowChatBtn)
    if self.CurrencyBar ~= nil then
        self.CurrencyBar:ShowChatBtn(isShowChatBtn)
    end
end

---更新显示资源条目
---@param resList GlobalDefine.eResourceSid[] 显示资源集合
function BaseUIView:UpdateResList(resList)
    if self.CurrencyBar ~= nil then
        self.CurrencyBar:UpdateResList(resList)
    end
end

---更新帮助文本键值索引
---@param helpKeyIndex number
function BaseUIView:UpdateHelpKeyIndex(helpKeyIndex)
    assert(helpKeyIndex, "HelpKeyIndex为空")
    self.CurrentHelpKeyIndex = helpKeyIndex
    if helpKeyIndex < 1 or helpKeyIndex > #self.ViewConfig.HelpKey then
        Logger.Error("帮助文本键值索引越界，Key: ", helpKeyIndex)
    end
end

--endregion ----------- 通用组件模块 end -----------

--region ------------- 全局事件监听部分 -------------

---添加事件（在Awake中添加才生效）
---protected
---@param id EventID
function BaseUIView:AddEvent(id)
    if self.EventIDList == nil then
        self.EventIDList = {}
    end
    table.insert(self.EventIDList, id)
end

---获取注册监听的事件ID列表
---@return table<EventID>
function BaseUIView:GetEventIDList()
    return self.EventIDList
end

---事件处理
---@protected
---@param id EventID EventID
function BaseUIView:EventHandle(id, ...)
    --Logger.Warning("[%s:EventHandle]__此函数需要再子类覆盖使用！", self:GetClassName())
end

---移除所有监听
---@private
function BaseUIView:RemoveAllListener()
    EventManager:GetInstance():Unregister(self)
    self.EventIDList = nil
end

--endregion

--region ------------- 窗口组件相关部分开始 -------------

---创建窗口根组件
---@private
function BaseUIView:CreateViewRootComponent()
    if self.ViewRootComponent == nil then
        ---TODO:采用对象池
        ---@type UnityEngine.GameObject
        local viewrootcomponentinstance = GameObject(string_format("%s_ViewRootComponent", self.ViewName))
        viewrootcomponentinstance.transform:SetParent(self.ViewTransform, false)
        self.ViewRootComponent = BaseViewComponent.New(nil, self, viewrootcomponentinstance)
    else
        Logger.Error("窗口名:%s根组件已经创建,请勿重复创建!", self.ViewName)
    end
end

---为GameObject获得或添加lua组件(窗口子组件统一添加入口，除了ViewRootComponent不允许直接New)
---@param bindtarget UnityEngine.GameObject @绑定的GameObject
---@param class BaseViewComponent @lua组件类
---@param ... any @参数
---@return BaseViewComponent @组件实例化类
function BaseUIView:GetOrAddComponent(bindtarget, class, ...)
    if self.ViewRootComponent ~= nil then
        return self.ViewRootComponent:GetOrAddComponent(bindtarget, class, ...)
    else
        Logger.Error("窗口名:%s根组件已经被清理,无法进行组件绑定!", self.ViewName)
        return nil
    end
end

---获取GameObject中挂载的lua组件
---@param bindtarget UnityEngine.GameObject @绑定的GameObject
---@param class BaseViewComponent  @lua组件类
---@return  BaseViewComponent @实例化类 或nil
function BaseUIView:GetComponent(bindtarget, class)
    if self.ViewRootComponent ~= nil then
        return self.ViewRootComponent:GetComponent(bindtarget, class)
    else
        Logger.Error("窗口名:%s根组件已经被清理,无法进行组件获取!", self.ViewName)
        return nil
    end
end

---移除GameObject中挂载的指定lua组件
---@param bindtarget UnityEngine.GameObject @绑定的GameObject
---@param class BaseViewComponent  @lua组件类
---@return boolean @移除是否成功
function BaseUIView:RemoveComponent(bindtarget, class)
    if self.ViewRootComponent ~= nil then
        return self.ViewRootComponent:RemoveComponent(bindtarget, class)
    else
        Logger.Error("窗口名:%s根组件已经被清理,无法进行组件清理!", self.ViewName)
        return false
    end
end

---移除指定lua组件实例对象
---@param componentinstance BaseViewComponent @lua组件实例对象
---@return boolean @移除是否成功
function BaseUIView:RemoveComponentInstance(componentinstance)
    if self.ViewRootComponent ~= nil then
        return self.ViewRootComponent:RemoveComponentInstance(componentinstance)
    else
        Logger.Error("窗口名:%s根组件已经被清理,无法进行组件实例对象清理!", self.ViewName)
        return false
    end
end

--endregion

---@return BaseUIView @窗口基类
return BaseUIView
