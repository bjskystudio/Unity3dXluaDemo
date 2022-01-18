---@class ModuleStartupConfig_Item
---@field public Sid string @模块名称，同文件名
---@field public SendFunc string[] @发消息函数名，不能用冒号方法，且必须带一个回调参数
---@field public ReLoginFunc string[] @重登函数，可不填
---@field public WaitList string[] @等待模块名称列表，没有就不填
---@field public InitFunc string @数据初始化函数名，发消息完成后执行，可不填
---@field public ClearFunc string @清理函数，可不填


---@class ModuleStartupConfig
---@field public LoginManager  ModuleStartupConfig_Item
