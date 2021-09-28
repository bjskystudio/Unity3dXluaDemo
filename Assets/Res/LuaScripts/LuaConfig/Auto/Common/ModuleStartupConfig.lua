

return _G.ConstClass("ModuleStartupConfig",{
    LoginManager = { Sid = "LoginManager", SendFunc = { "InitLoginData" }, ReLoginFunc = {}, WaitList = {}, InitFunc = "", ClearFunc = "" },
    ShopNet = { Sid = "ShopNet", SendFunc = { "GetAllShop" }, ReLoginFunc = { "GetAllShop" }, WaitList = { "LoginManager" }, InitFunc = "", ClearFunc = "" },
    TaskNet = { Sid = "TaskNet", SendFunc = { "GetTaskData" }, ReLoginFunc = { "GetTaskData" }, WaitList = { "LoginManager" }, InitFunc = "", ClearFunc = "" },
    PlayerNet = { Sid = "PlayerNet", SendFunc = { "GetPlayerEquip" }, ReLoginFunc = { "GetPlayerEquip" }, WaitList = { "LoginManager" }, InitFunc = "", ClearFunc = "" }
})