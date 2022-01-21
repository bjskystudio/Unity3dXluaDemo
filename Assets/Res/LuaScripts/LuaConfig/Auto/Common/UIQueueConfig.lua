

return _G.ConstClass("UIQueueConfig",{
    CommonTip = { Name = "CommonTip", Desc = "普通提示", Group = 1, Priority = 1, Script = "TipsUIQueue", UIName = "", QueueType = EUIQueueType.Concurrent, ConcurrentDelay = 0.15, ConcurrentMaxCount = 4 },
    MoreTips = { Name = "MoreTips", Desc = "多行提示", Group = 1, Priority = 2, Script = "MoreTipsUIQueue", UIName = "", QueueType = EUIQueueType.Sequence, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },
    HorseLamp = { Name = "HorseLamp", Desc = "跑马灯", Group = 2, Priority = 1, Script = "HorseLampUIQueue", UIName = "", QueueType = EUIQueueType.Sequence, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },
    PlayRoleLevelUp = { Name = "PlayRoleLevelUp", Desc = "升级提示", Group = 9, Priority = 1, Script = "PlayRoleLevelUpUIQueue", UIName = "", QueueType = EUIQueueType.Sequence, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },
    FuncOpen = { Name = "FuncOpen", Desc = "功能开放提示", Group = 9, Priority = 3, Script = "FunctionOpenUIQueue", UIName = "", QueueType = EUIQueueType.Sequence, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },
    Achievement = { Name = "Achievement", Desc = "成就提示", Group = 9, Priority = 2, Script = "CommonOpenUIQueue", UIName = "AchievementTipPopupView", QueueType = EUIQueueType.Sequence, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },
    TitleTips = { Name = "TitleTips", Desc = "称号解锁", Group = 9, Priority = 2, Script = "TitleTipsUIQueue", UIName = "", QueueType = EUIQueueType.Sequence, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },
    Power = { Name = "Power", Desc = "战斗力提示", Group = 9, Priority = 2, Script = "PowerChangeUIQueue", UIName = "", QueueType = EUIQueueType.Immediate, ConcurrentDelay = 0, ConcurrentMaxCount = 0 },

})