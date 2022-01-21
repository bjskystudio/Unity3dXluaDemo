---@class UIConfig_Item
---@field public Name string @界面名称
---@field public ResPath string @预设路径
---@field public UIType lua @界面类型
---@field public RelativeViewName string @固定Order参考窗口(不填代表不启用相对Order)
---@field public ConstOrder number @固定相对Order
---@field public IsFullScreen boolean @是否全屏
---@field public IsBlur boolean @模糊背景
---@field public IsShowChat boolean @聊天按钮
---@field public ResList number[] @货币信息
---@field public HelpKey string[] @帮助信息


---@class UIConfig
---@field public LoadingView  UIConfig_Item
---@field public MaskView  UIConfig_Item
---@field public LoginView  UIConfig_Item
---@field public CreateRoleView  UIConfig_Item
---@field public MainView  UIConfig_Item
---@field public AlertDialogView  UIConfig_Item
