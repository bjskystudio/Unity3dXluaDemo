-------------------------------------------------------
-- File Name:       BaseViewComponent.lua
-- Author:          TangHuan,csw
-- Create Date:     2021/04/20
-- Description:     Lua窗口组件基类(类似原来的Component但脱离Mono且和View强绑定)

-- 窗口组件绑定流程介绍
-- 1. __init()                 -- 构造函数调用
-- 2. UpdateOrder()            -- 更新Order
-- 3. Awake()                  -- Awake(子类重写实现)
-- 4. OnCreate()               -- 组件显示(子类重写实现)
-- 5. OnEnable()               -- 组件可用(子类重写实现)
-- 6. OnDisable()              -- 组件不可用(子类重写实现)
-- 7. Destroy()                -- 组件清理或销毁
-- 8. OnDestroy()              -- 组件自定义清理(子类重写实现)
-- 9. ClearAllComponents()     -- 清理嵌套组件
-- 10. RemoveAllListener()     -- 移除所有事件监听

-- 和原来Component的区别介绍:
-- 1. 脱离Mono,纯逻辑驱动
-- 2. 强耦合View,通过View和嵌套BaseComponent来驱动管理
-- 3. 支持手动移除组件绑定接口

-- 注意事项:
-- 1. 组件移除和清理(清除组件绑定以及触发组件逻辑清理)都不会主动销毁实体绑定对象,如果实体对象不在窗口上需要自行清理(比如在OnClear里)
-- 2. 如果组件绑定对象节点位置变化造成显示Order问题，请主动调用UpdateOrder方法更新Order
-------------------------------------------------------


local EventManager = require("EventManager")
local UIData = require("UIData")
local GameLog = require("GameLog")
local Logger = require("Logger")
local UpdateUIGameObjectOrder = CS.UIUtils.UpdateUIGameObjectOrder
local handler = handler
local GetAutoGoTableOrNil = GetAutoGoTableOrNil
local Guard = Guard
local IsClass = IsClass

---@class BaseViewComponent @Lua组件类(类似原来的Component但脱离Mono且和View强绑定)
---@field private IsValide boolean @是否是有效组件
---@field protected Owner BaseViewComponent @所属组件(支持组件套组件，窗口的第一个组件(哪怕是继承)的Owner是BaseViewComponent,第二个组件开始就是上一个组件)
---@field protected OwnerView BaseUIView @所属窗口
---@field protected transform UnityEngine.Transform 物体对应的transform
---@field protected gameObject UnityEngine.GameObject 物体对应的gameObject
---@field private GlobalEventListenerMap table<number, table<number,function>> @全局事件监听回调Map(Key为事件名,Value为对应监听回调列表)
---@field protected go_table any @组件绑定
---@field private Components table<UnityEngine.GameObject, table<string, BaseViewComponent>> @子组件缓存映射Map(Key为GameObject，Value为类名和绑定的组件实例对象)
---@field protected EventIDList EventID[] @窗口注册的监听事件ID集合
local BaseViewComponent = Class("BaseViewComponent")

---BaseViewComponent构造函数
---@private
---@param owner BaseViewComponent @所属组件
---@param ownerview BaseUIView @所属窗口
---@param gameobject UnityEngine.GameObject @绑定的GameObject
---@param ... any @参数
function BaseViewComponent:__init(owner, ownerview, gameobject, ...)
    self.IsValide = true
    self.Owner = owner
    self.OwnerView = ownerview
    self.transform = gameobject.transform
    self.gameObject = gameobject
    self.Components = {}
    self.GlobalEventListenerMap = {}
    self.go_table = GetAutoGoTableOrNil(self.gameObject, handler(self, self.OnClickBtn), handler(self, self.OnClickToggle))
    self:UpdateOrder()
    self:Awake()
    EventManager:GetInstance():Register(self)
    self:OnCreate(...)
    self:OnEnable()
end

---是否可用(动态加载在未加载完成前被使用可能需要判定)
---@return boolean @是否可用
function BaseViewComponent:IsAvalible()
    return self.IsValide
end

---是否可见
---@return boolean @是否可见
function BaseViewComponent:IsActive()
    return self.IsShow == true
end

---设置可见
---@param active boolean @设置可见
function BaseViewComponent:SetActive(active)
    if self:IsAvalible() then
        self.gameObject:SetActive(Bool2Num(active))
        self.IsShow = active
        if active then
            self:OnEnable()
            --EventManager:GetInstance():Register(self)
        else
            self:OnDisable()
            --EventManager:GetInstance():Unregister(self)
        end
    else
        Logger.Error("组件:%s对象已无效，不应该进入这里!", self.__cname)
    end
end

---更新Order
function BaseViewComponent:UpdateOrder()
    UpdateUIGameObjectOrder(self.gameObject, UIData.PerWindowOrder, false)
end

---模拟Unity Component的 Awake
---@protected
function BaseViewComponent:Awake()
end

---初始化
---@protected
---@param ... any @参数
function BaseViewComponent:OnCreate(...)
end

---可用
---@protected
function BaseViewComponent:OnEnable()
end

---不可用
---@protected
function BaseViewComponent:OnDisable()
end

---关闭组件(带销毁预制体过程)
function BaseViewComponent:Close()
    if self.Owner ~= nil then
        self.Owner:DestoryComponentGameObj(self.gameObject)
    else
        Logger.Error("按照嵌套使用方式,只有根节点不存在数据拥有者")
    end
end

---子类重写实现自定义清理[virtual]
---@protected
function BaseViewComponent:OnDestroy()
end

---清除所有子组件绑定
---@private
function BaseViewComponent:ClearAllComponents()
    ---@param classinstancemap table<string, BaseViewComponent>
    for bindtarget, classinstancemap in pairs(self.Components) do
        ---@param component BaseViewComponent
        for classname, component in pairs(classinstancemap) do
            GameLog.Info(GameLog.Module.UIModule, "清理窗口组件:%s实例对象:%s绑定窗口组件:%s!", self.__cname, bindtarget.name, classname)
            component:Destroy()
        end
    end
    self.Components = {}
end

---逻辑销毁(窗口关闭时和RemoveComponent时调用)
---@private
function BaseViewComponent:Destroy()
    if self.IsValide then
        self:OnDisable()
        self:OnDestroy()
        self:RemoveAllListener()
        self:ClearAllComponents()
        self.Owner = nil
        self.OwnerView = nil
        self.transform = nil
        self.gameObject = nil
        self.Components = nil
        self.go_table = nil
        self.GlobalEventListenerMap = nil
        self.IsValide = false
    else
        Logger.Error("组件类:%s对象已经被销毁,不应该再次进入,请检查代码!", self.__cname)
    end
end

---点击Button回调
---@protected
---@param btn AorButton 按钮
function BaseViewComponent:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function BaseViewComponent:OnClickToggle(toggle, isOn)
end

---获取所属窗口
---@return BaseUIView @所属窗口
function BaseViewComponent:GetOwnerView()
    return self.OwnerView
end

--region    -----------------事件监听部分开始--------------------------

---添加事件（在Awake中添加才生效）
---protected
---@param id EventID
function BaseViewComponent:AddEvent(id)
    if self.EventIDList == nil then
        self.EventIDList = {}
    end
    table.insert(self.EventIDList, id)
end

---获取注册监听的事件ID列表
---@return EventID[]
function BaseViewComponent:GetEventIDList()
    return self.EventIDList
end

---事件处理
---@protected
---@param id EventID EventID
function BaseViewComponent:EventHandle(id, ...)
    --Logger.Warning("[%s:EventHandle]__此函数需要再子类覆盖使用！", self:GetClassName())
end

---移除所有监听
---@private
function BaseViewComponent:RemoveAllListener()
    EventManager:GetInstance():Unregister(self)
    self.EventIDList = nil
end

--endregion -----------------事件监听部分结束------------------------

--region -----------------组件添加获取移除接口开始--------------------

---为GameObject获得或添加lua组件(嵌套子组件统一添加入口，不允许直接New)
---@param bindtarget UnityEngine.GameObject 绑定的GameObject
---@param class BaseViewComponent  lua组件类
---@param ... any @参数
---@return BaseViewComponent @组件实例化类
function BaseViewComponent:GetOrAddComponent(bindtarget, class, ...)
    Guard.AssertException(IsClass(class, BaseViewComponent), string.format("%s未继承BaseViewComponent", class.__cname))
    -- behaviour 记录数据是否存在
    local cl = self:GetComponent(bindtarget, class)
    -- 检测组件是否已经添加过
    if cl == nil then
        cl = class.New(self, self.OwnerView, bindtarget, ...)
        -- 添加缓存到本地集合中
        self.Components[bindtarget] = self.Components[bindtarget] or {}
        self.Components[bindtarget][class.__cname] = cl
    end
    return cl
end

---GetComponent 获取GameObject中挂载的lua组件
---@param bindtarget UnityEngine.GameObject 绑定的GameObject
---@param class BaseViewComponent  lua组件类
---@return  BaseViewComponent 实例化类 或nil
function BaseViewComponent:GetComponent(bindtarget, class)
    local tager = self.Components[bindtarget]
    if tager ~= nil and tager[class.__cname] then
        return tager[class.__cname]
    end
    return nil
end

---销毁GameObject,并移除其中挂载的所有lua组件
---@param bindtarget UnityEngine.GameObject @绑定的GameObject
function BaseViewComponent:DestoryComponentGameObj(bindtarget)
    ---@type table<string, BaseViewComponent>
    local tager = self.Components[bindtarget]
    if tager ~= nil then
        GameLog.Info(GameLog.Module.UIModule, "移除之前:", self.Components)
        for i, v in pairs(tager) do
            GameLog.Info(GameLog.Module.UIModule, "组件类:%s移除实体对象名:%s绑定的组件脚本:%s!", self.__cname, bindtarget.name, v.__cname)
            v:Destroy()
        end
        self.Components[bindtarget] = nil
        bindtarget:DestroyGameObj()
        GameLog.Info(GameLog.Module.UIModule, "移除之后:", self.Components)
        return true
    else
        Logger.Error("当前组件类:%s实例对象:%s未绑定组件类，清除失败!", self.__cname, bindtarget.name)
        return false
    end
end

---移除GameObject中挂载的指定lua组件
---@param bindtarget UnityEngine.GameObject @绑定的GameObject
---@param class BaseViewComponent @lua组件类
---@return boolean @移除是否成功
function BaseViewComponent:RemoveComponent(bindtarget, class)
    Guard.AssertException(IsClass(class, BaseViewComponent), "未继承BaseViewComponent")
    local tager = self.Components[bindtarget]
    if tager ~= nil and tager[class.__cname] then
        GameLog.Info(GameLog.Module.UIModule, "组件类:%s移除实体对象名:%s绑定的组件脚本:%s!", self.__cname, bindtarget.name, class.__cname)
        tager[class.__cname]:Destroy()
        tager[class.__cname] = nil
        return true
    else
        Logger.Error("当前组件类:%s实例对象:%s未绑定组件类:%s，清除失败!", self.__cname, bindtarget.name, class.__cname)
        return false
    end
end

---移除指定lua组件实例对象
---@param componentinstance BaseViewComponent  @lua组件实例对象
---@return boolean @移除是否成功
function BaseViewComponent:RemoveComponentInstance(componentinstance)
    Guard.AssertException(IsClass(componentinstance, BaseViewComponent), "未继承BaseViewComponent")
    local tager = self.Components[componentinstance.gameObject]
    if tager ~= nil and tager[componentinstance.__cname] then
        GameLog.Info(GameLog.Module.UIModule, "组件类:%s移除实体对象名:%s绑定的组件脚本:%s实体!", self.__cname, componentinstance.gameObject.name, componentinstance.__cname)
        tager[componentinstance.__cname]:Destroy()
        tager[componentinstance.__cname] = nil
        return true
    else
        Logger.Error("当前组件类:%s实例对象:%s未绑定组件类:%s，清除失败!", self.__cname, componentinstance.gameObject.name, componentinstance.__cname)
        return false
    end
end

--endregion -----------------组件添加获取移除接口结束--------------------

-- ---@type BaseViewComponent @Lua组件类(类似原来的Component但脱离Mono且和View强绑定)
-- _G.BaseViewComponent = BaseViewComponent

---@return BaseViewComponent @Lua组件类(类似原来的Component但脱离Mono且和View强绑定)
return BaseViewComponent
