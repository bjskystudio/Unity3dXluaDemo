-----------------------------------------------------------------------
-- File Name:       GlobalDefine
-- Author:          csw
-- Create Date:     2021/04/28
-- Description:     全局数据枚举定义，注意：全局枚举名为e开头
-----------------------------------------------------------------------

---@class GlobalDefine
local GlobalDefine = {}
_G.GlobalDefine = GlobalDefine

--region -------------路径常量-------------

---@class GlobalDefine.eGamePath 游戏路径常量
GlobalDefine.eGamePath = {
    --图集Atlas
    ---公共图集
    AtlasCommon = "AtlasSprites/Common/",
    ---卡牌相关图集
    AtlasCard = "AtlasSprites/Card/",
    ---卡片选择相关图集
    AtlasCardSelect = "AtlasSprites/CardSelect",
    ---剧情相关
    Plot = "AtlasSprites/Juqing/",
    ---技能
    AtlasSkill = "AtlasSprites/Skill/",
    ---战斗图集
    AtlasBattle = "AtlasSprites/Battle/",
    ---竞技场图集
    AtlasArena = "AtlasSprites/Arena/",
    ---时空裂隙副本图集
    PveDup = "AtlasSprites/PveDup/",
    ---小地图图集
    AtlasMiniMap = "AtlasSprites/MiniMap/",
    ---任务图集
    Task = "AtlasSprites/Task/",
    ---创建角色界面
    CreateRole = "AtlasSprites/CreateRole/",
    ---每日活动
    Active = "AtlasSprites/Active/",
    ---排行
    Rank = "AtlasSprites/Rank/",

    --散图Texture
    ---图标
    Icon = "Texture/PropIcon/",
    ---玩家头像
    PlayerHead = "Texture/Player/HeadIcon/",
    ---卡牌Spine立绘
    CardDraw = "CardSpine/",
    ---卡牌头像
    CardHeadIcon = "Texture/Role/HeadIcon/",
    ---卡牌静态立绘
    CardImage = "Texture/Role/Image/",
    ---技能图标
    SkillIcon = "Texture/Skill/",
    ---页签图标
    TagIcon = "Texture/Tag/",
    ---背景
    BG = "Texture/BG/",
    ---装备图标
    EquipIcon = "Texture/Equip/",
    ---剧情相关
    JuQing = "Texture/JuQing/",
    ---竞技场图标
    ArenaIcon = "Texture/Equip/",
    ---每日活动
    ActiveImage = "Texture/Active/",
    ---每日活动
    AllStar = "Texture/AllStar/",
}

--endregion

--region -------------网络-------------

---@class GlobalDefine.eConnectType 连接类型
GlobalDefine.eConnectType = {
    ---游戏主连接
    Game = 1,
    ---聊天连接
    Chat = 2,
}

--endregion

--region ------------- 对象池 -------------

---@class GlobalDefine.ePoolType 预制体池类型
GlobalDefine.ePoolType = {
    ---UI模块对象池
    UIModel = "UIModelPool",
    ---剧情模块对象池
    StoryModel = "StoryModelPool",
    ---战斗模块对象池
    BattleModule = "BattleModulePool",
}

--endregion ----------- 对象池 end -----------

--region -------------登录-------------

---@class GlobalDefine.eLoginState 登录状态
GlobalDefine.eLoginState = {
    ---初始,刚进游戏登录界面
    Init = "Init",
    ---登录SDK第三方
    LoginSDKEx = "LoginSDKEx",
    ---登录SDK挂接中心(登录挂接中心，得到必要参数进入游戏)
    LoginSDKServer = "LoginSDKServer",
    ---已连接服务器
    Connected = "Connected",
    ---连接服务器失败
    ConnectFail = "ConnectFail",
    ---登录服务器成功
    LoginSucceed = "LoginSucceed",
    ---重新登录服务器成功
    ReLoginSucceed = "ReLoginSucceed",
    ---登录服务器失败
    LoginError = "LoginError",
    ---登录成功，但是没有角色
    NoRole = "NoRole",
    ---创建角色成功
    CreateRole = "CreateRole",
    ---获取用户数据完成
    GetUserData = "GetUserData",
    ---登录成功，正常进入游戏主界面
    EnterGame = "EnterGame",
    ---重新进入游戏
    ReEnterGame = "ReEnterGame",
}

--endregion

--region ------------- 本地提示 -------------

---@class GlobalDefine.eLocalTipsType 本地提示存储 (需要存储在本地做判断是否显示提示，如：本日不再提示. 配合PlayerPrefsUtil使用)
GlobalDefine.eLocalTipsType = {
    ---禁手秘卷重新研究提示（本日）
    ScrollsResearchAfresh = "ScrollsResearchAfresh",
    ---禁手秘卷自动研究勾选
    ScrollsResearchAuto = "ScrollsResearchAuto",
}

--endregion


--region -------------仓库、道具、奖励-------------

---@class GlobalDefine.eStorageType 仓库类型(必须和后台的仓库类型保持一致)
GlobalDefine.eStorageType = {
    ---资源仓库(虚拟的仓库，后台不存在)
    Resources = "res",
    ---卡牌仓库
    Card = "card",
    ---道具仓库
    Goods = "goods",
    ---代币仓库
    ResProp = "res_prop",
    ---公仔仓库
    Doll = "doll",
    ---体力仓库
    AutoRec = "auto_rec",
    ---材料仓库
    Material = "material",
    ---装备仓库
    Equip = "equip",
    ---碎片仓库
    CardFrag = "card_frag",
    ---宝石仓库
    Gem = "gem",
    ---任务道具仓库
    TaskProp = "task_goods",
    ---地下格斗场背包
    PvpTowerProp = "pvp_tower_prop",
    -- ---头像框仓库
    -- AvatarBorder = "avatar_border",
}

---@class GlobalDefine.eAwardType 奖励类型和后台发送字段同步
GlobalDefine.eAwardType = {
    ---钻石
    RMB = "rmb",
    ---卡牌
    Card = "card",
    ---道具
    Prop = "prop",
    ---代币
    ResProp = "res_prop",
    ---体力
    Pve = "pve",
    ---角色经验
    RoleExp = "role_exp",
    ---英雄经验
    CardExp = "card_exp",
    ---整卡转换成的碎片，类型是Prop
    CardFragment = "card_fragment",
    ---装备
    Equip = "equip",
    ---任务道具
    TaskProp = "task_prop",
    ---地下格斗场仓库
    PvpTowerProp = "pvp_tower_prop",
}

---@class GlobalDefine.eAwardShowType 奖励展示类型枚举
GlobalDefine.eAwardShowType = {
    ---不展示
    DontShow = "DontShow",
    ---通用展示窗口-立即展示
    Common = "Common",
    ---飘字提示-立即展示
    Tips = "Tips",
}

---@class GlobalDefine.eItemType 物品类型
GlobalDefine.eItemType = {
    ---钻石资源
    Rmb = "rmb",
    ---体力
    Vit = "vit",
    ---卡牌经验
    CardExp = "card_exp",
    ---玩家经验
    RoleExp = "role_exp",
    ---代币资源
    ResProp = "res_prop",
    ---普通道具
    NormalProp = "normal_prop",
    ---任务道具
    TaskProp = "task_prop",
    ---经验药
    ExpPotion = "exp_potion_prop",
    ---魂石
    GemProp = "gem",
    ---宝箱
    BoxAward = "box_award",
    ---宝箱
    ConditionBox = "condition_box",
    ---卡牌
    Card = "card",
    ---装备
    Equip = "equip",
    ---材料
    Material = "material",
    ---头像框
    AvatarBorder = "avatar_border",
    ---力量之轮
    DreamPower = "dream_power",
    ---财富之轮
    DreamRich = "dream_rich",
    ---权利之轮
    DreamRight = "dream_right",
    ---名声之轮
    DreamRepute = "dream_repute",
    ---称号
    Title = "title",
    ---使用道具
    Vp_drug = "vp_drug",
    ---食物
    Vp_food = "vp_food",
    ---卡牌碎片
    CardFragment = "card_frag",
    ---地下格斗场物品
    PvpTowerProp = "pvp_tower_prop",
}

---@class GlobalDefine.eResourceSid 常用资源Sid
GlobalDefine.eResourceSid = {
    ---金币
    Money = 1,
    ---钻石卡
    Crystal = 2,
    ---碎金块
    GoldChip = 3,
    ---天赋点
    Talent = 4,
    ---友情点
    FriendShip = 5,
    ---航海金币（兑换商城）
    NavigationGold = 6, --航海金币（兑换商城）
    ---竞技场货币
    ArenaMoney = 9, --技场货币（兑换商城）
    ---大船团资源
    CorpsMoney = 94,
    ---体力
    Vit = 97,
    ---黑卡
    RMB = 99,
    ---月华精尘
    MoonDust = 3100,
    ---招募券
    DrawToken = 3650, -- 普通招募券 与 Sid 保持一致
    ---爬塔金币
    PvpTowerRes = 14,
    ---暗金
    DarkCrystal = 13,
    ---爬塔积分（纯展示）
    PvpTowerScore = 6604,
    ---免传印章·极限
    LimitSeal = 2705,
    ---免传印章·不知火
    BuZhiHuoSeal = 2706,
    ---免传印章·八极
    BaJiSeal = 2707,
    ---免传印章·我流
    WoSeal = 2708,
    ---免传印章·秘仪
    SecretSeal = 2709,
    ---领域点
    AreaPoint = 1,
}

--endregion

--region -------------泛功能块-------------

---不再需要该KEY值表，将KEY缓存在内存中，退出即清空
---@class GlobalDefine.eDialogNoMorePrompts 不再提示KEY值列表
GlobalDefine.eDialogNoMorePrompts = {
    ---更换装备
    RecastChangeEquip = "RecastChangeEquip",
    ---再次重铸
    RecastAgian = "RecastAgian",
    ---更换重铸属性
    RecastChange = "RecastChangeAttr"
}

---@class GlobalDefine.eLevelExp 等级经验表索引
GlobalDefine.eLevelExp = {
    ---玩家等级经验
    PlayerLevelExp = 1,
    ---卡片等级经验
    CardLevelExp = 2,
    ---玩家VIP等级经验
    PlayerVipLevelExp = 3
}

--endregion

--region ------------- 音频 -------------

---@class GlobalDefine.eAudioSid 音频配置Sid映射
GlobalDefine.eAudioSid = {
    ---背景音乐
    BGM_DengLu = 1,
    ---背景音乐
    BGM2 = 2,
    ---背景音乐
    BGM3 = 3,

    ---界面打开
    JieMianDakai = 20,
    ---界面关闭
    JieMianGuanBi = 21,
    ---无效操作
    WuXiaoCaoZuo = 22,
    ---按钮点击
    AnNiuDianJi = 23,

    ---新消息
    XinXiaoXi = 50,
    ---新邮件
    XinYouJian = 51,
    ---传送
    ChuanSong = 52,
}

--endregion ----------- 音频 end -----------

--region -------------卡牌相关-------------

---@class GlobalDefine.eAttrType 属性类型(此处增加枚举后，需要在CardAttributeConfig添加对应条目)
---@field public attack number 攻击
---@field public normaldefense number 普防
---@field public specialdefense number 特防
---@field public defense number 防御
---@field public hp number 生命
---@field public maxhp number 生命
---@field public speed number 速度
---@field public rage number 怒气
---@field public maxrage number 最大怒气
---@field public level number 等级
---@field public breaks number 破防
---@field public hard number 体魄
---@field public aim number 瞄准
---@field public skew number 偏移
---@field public hit number 命中
---@field public dodge number 闪避
---@field public crit number 会心
---@field public tenacity number 坚韧
---@field public critintensity number 会心强化
---@field public critavianize number 会心抗性
---@field public block number 格挡
---@field public dexterity number 灵巧
---@field public blockintensity number 格挡强化
---@field public blockavianize number 格挡穿透
---@field public damageamplify number 伤害
---@field public damagediminish number 免伤
---@field public normaldamageamplify number 物理伤害
---@field public normaldamagediminish number 物理免伤
---@field public specialdamageamplify number 魔法伤害
---@field public specialdamagediminish number 魔法免伤
---@field public attackdamageamplify number 普攻伤害
---@field public attackdamagediminish number 普攻免伤
---@field public limitdamageamplify number 必杀伤害
---@field public limitdamagediminish number 必杀免伤
---@field public ragedamageamplify number 怒气伤害
---@field public ragedamagediminish number 怒气免伤
---@field public pvpdamageamplify number 杀意
---@field public pvpdamagediminish number 根性
---@field public pvedamageamplify number 强袭
---@field public pvedamagediminish number 残心
---@field public hematophagia number 吸血
---@field public thorns number 反伤
---@field public healpintensity number 治疗强度
---@field public recoverypintensity number 回复强度
---@field public effecthit number 效果命中
---@field public effectdodge number 效果抵抗
---@field public punchdamageamplify number 寸拳伤害 对斗拳类格斗家伤害提升
---@field public occultdamageamplify number 秘流伤害 对秘流类格斗家伤害提升
---@field public psionicdamageamplify number 超能伤害 对超能类格斗家伤害提升
---@field public armordamageamplify number 武具伤害
---@field public punchdamagediminish number 寸拳免伤
---@field public occultdamagediminish number 秘流免伤
---@field public psionicdamagediminish number 超能免伤
---@field public armordamagediminish number 武具免伤
---@field public pvepunchdamageamplify number 初始寸拳伤害
---@field public pveoccultdamageamplify number 初始秘流伤害
---@field public pvepsionicdamageamplify number 初始超能伤害
---@field public pvearmordamageamplify number 初始武具伤害
---@field public pvepunchdamagediminish number 初始寸拳免伤
---@field public pveoccultdamagediminish number 初始秘流免伤
---@field public pvepsionicdamagediminish number 初始超能免伤
---@field public pvearmordamagediminish number 初始武具免伤
---@field public finaldamageamplify number 最终伤害
---@field public finaldamagediminish number 最终免伤
---@field public selfattackrage number 自己普攻回怒
---@field public selfcampattackrage number 友方攻击回怒
---@field public selfcampsufferrage number 友方受击回怒
---@field public punchtimes number 重拳行动力
---@field public uniquepoints number 必杀技能量值
---@field public ended number 结束标记
local eAttrType1 = {
    ---攻击
    'attack',
    ---普防
    'normaldefense',
    ---特防
    'specialdefense',
    ---防御
    'defense',
    ---生命
    'hp',
    ---生命
    'maxhp',
    ---速度
    'speed',
    ---怒气
    'rage',
    ---最大怒气
    'maxrage',
    ---等级
    'level',
    ---破防
    'breaks',
    ---体魄
    'hard',
    ---瞄准
    'aim',
    ---偏移
    'skew',
    ---命中
    'hit',
    ---闪避
    'dodge',
    ---会心
    'crit',
    ---坚韧
    'tenacity',
    ---会心强化
    'critintensity',
    ---会心抗性
    'critavianize',
    ---格挡
    'block',
    ---灵巧
    'dexterity',
    ---格挡强化
    'blockintensity',
    ---格挡穿透
    'blockavianize',
    ---伤害
    'damageamplify',
    ---免伤
    'damagediminish',
    ---物理伤害
    'normaldamageamplify',
    ---物理免伤
    'normaldamagediminish',
    ---魔法伤害
    'specialdamageamplify',
    ---魔法免伤
    'specialdamagediminish',
    ---普攻伤害
    'attackdamageamplify',
    ---普攻免伤
    'attackdamagediminish',
    ---必杀伤害
    'limitdamageamplify',
    ---必杀免伤
    'limitdamagediminish',
    ---怒气伤害
    'ragedamageamplify',
    ---怒气免伤
    'ragedamagediminish',
    ---杀意
    'pvpdamageamplify',
    ---根性
    'pvpdamagediminish',
    ---强袭
    'pvedamageamplify',
    ---残心
    'pvedamagediminish',
    ---吸血
    'hematophagia',
    ---反伤
    'thorns',
    ---治疗强度
    'healpintensity',
    ---回复强度
    'recoverypintensity',
    ---效果命中
    'effecthit',
    ---效果抵抗
    'effectdodge',
    ---寸拳伤害 对斗拳类格斗家伤害提升
    'punchdamageamplify',
    ---秘流伤害 对秘流类格斗家伤害提升
    'occultdamageamplify',
    ---超能伤害 对超能类格斗家伤害提升
    'psionicdamageamplify',
    ---武具伤害
    'armordamageamplify',
    ---寸拳免伤
    'punchdamagediminish',
    ---秘流免伤
    'occultdamagediminish',
    ---超能免伤
    'psionicdamagediminish',
    ---武具免伤
    'armordamagediminish',
    ---初始寸拳伤害
    'pvepunchdamageamplify',
    ---初始秘流伤害
    'pveoccultdamageamplify',
    ---初始超能伤害
    'pvepsionicdamageamplify',
    ---初始武具伤害
    'pvearmordamageamplify',
    ---初始寸拳免伤
    'pvepunchdamagediminish',
    ---初始秘流免伤
    'pveoccultdamagediminish',
    ---初始超能免伤
    'pvepsionicdamagediminish',
    ---初始武具免伤
    'pvearmordamagediminish',
    ---最终伤害
    'finaldamageamplify',
    ---最终免伤
    'finaldamagediminish',
    ---自己普攻回怒
    'selfattackrage',
    ---友方攻击回怒
    'selfcampattackrage',
    ---友方受击回怒
    'selfcampsufferrage',
    ---重拳行动力
    'punchtimes',
    ---必杀技能量值
    'uniquepoints',
    ---结束标记
    'ended',
}
---@type GlobalDefine.eAttrType
GlobalDefine.eAttrType = read_only(CreateEnumTable(eAttrType1, 1))

---@class GlobalDefine.eAttrGroupType 属性组类型
GlobalDefine.eAttrGroupType = {
    ---不显示
    Empty = 0,
    ---基础
    Base = 1,
    ---战斗
    Battle = 2,
    ---特殊
    Special = 3,
    ---伤害
    Damage = 4,
}

---@class GlobalDefine.eTalentType 天赋状态
GlobalDefine.eTalentType = {
    ---天赋点
    Talent = 1,
    ---技能点
    Skill = 2,
    ---隐藏点
    HidePoint = 3
}

---@class GlobalDefine.eSkillType 技能类型
GlobalDefine.eSkillType = {
    ---普攻(轻拳)
    Attack = 1,
    ---必杀
    UniqueSkill = 2,
    ---怒攻
    FuryAttack = 3,
    ---觉醒
    Awaken = 4,
    ---羁绊
    Fetters = 5,
    ---被动
    Passive = 6,
    ---战意
    Warth = 7,
    ---重拳
    Punch = 8,
    ---其他
    Other = 9,
}

--endregion ----------卡牌-------------

--region -------------邮件-------------
---@class GlobalDefine.eMailReadState 邮件读取状态
GlobalDefine.eMailReadState = {
    ---未读
    UnRead = 0,
    ---已读
    Read = 1,
    ---已领
    GetReward = 2,
}
--endregion

--region ------------- 剧请对话 -------------
---@class GlobalDefine.eDialogue 剧请对话
GlobalDefine.eDialogue = {
    ---正常对话
    Dialogue = 0,
    ---播放剧情
    Plot = 1,
}

---@class GlobalDefine.eDialogueOptionType 对话选项类型
GlobalDefine.eDialogueOptionType = {
    ---对话选项
    Dialogue = 1,
    ---功能选项
    Function = 2,
}
--endregion

--region ------------- 装备 -------------
---@class GlobalDefine.eEquipPlace 装备部位
GlobalDefine.eEquipPlace = {
    ---套装卡1
    Equip1 = 1,
    ---套装卡2
    Equip2 = 2,
    ---套装卡3
    Equip3 = 3,
    ---珍卡
    Treasure = 4,
}

---@class GlobalDefine.eGemPlace 宝石部位
GlobalDefine.eGemPlace = {
    ---宝石位置1
    Gem1 = 1,
    ---宝石位置2
    Gem2 = 2,
    ---宝石位置3
    Gem3 = 3,
    ---宝石位置4
    Gem4 = 4,
}
--endregion ------------- 装备 end -------------

--region ------------- 商店 -------------
---@class GlobalDefine.eShopType 商店类型
GlobalDefine.eShopType = {
    ---隐藏商店
    HideShop = 0,
    ---交易商店
    DealShop = 1,
    ---正常商店
    NormalShop = 2,
    ---招募兑换商店
    CallShop = 3
}

---@class GlobalDefine.eShopLimitType 商品购买限制类型
GlobalDefine.eShopLimitType = {
    ---限购时间为秒（测试使用）
    S = "S",
    ---每日限购
    D = "D",
    ---每周限购
    W = "W",
    ---每月限购
    M = "M",
    ---永久限购
    F = "F",
    ---没有购买限制
    N = "N",
}

---@class GlobalDefine.eShopBuyErrorState 商品购买失败错误状态码
GlobalDefine.eShopBuyErrorState = {
    ---限购了
    PurchaseLimit = "PurchaseLimit",
    ---资源不足
    ResLimit = "ResLimit",
}
--endregion ------------- 商店 end -------------

--region -------------好友-------------
---@class GlobalDefine.eRelationType 好友列表类型
GlobalDefine.eRelationType = {
    ---好友
    Friend = 1,
    ---黑名单
    Black = 2,
    ---推荐
    Recommend = 3,
    ---申请
    Apply = 4,
    ---搜索
    Search = 5,
}
---@class GlobalDefine.eFriendshipState 友情点领取状态
GlobalDefine.eFriendshipState = {
    ---不可点击状态
    Forbid = 1,
    ---可领取
    CanGet = 2,
    ---可赠送
    CanGive = 3,
}

--endregion


--region ------------- 竞技场奖励 -------------
---@class GlobalDefine.eArenaRewardType 奖励类型
GlobalDefine.eArenaRewardType = {
    ---活跃度
    Activity = 1,
    ---每日排名
    Daily = 2,
    ---段位奖励
    Rank = 3,
    ---赛季排名
    Total = 4,
}
---@class GlobalDefine.eArenaRewardState 奖励类型
GlobalDefine.eArenaRewardState = {
    ---可领取
    CanGet = 1,
    ---不可领取
    NoGet = 2,
    ---已领取
    Received = 3,
}

--endregion
--region ------------- 招募 -------------
---@class GlobalDefine.eRecruitType 招募类型
GlobalDefine.eRecruitType = {

    ---普通招募
    NormalRecruit = 1,
    ---友情招募
    FriendRecruit = 2,
    ---写真卡招募
    PortrayRecruit = 3,
    ---限时招募
    LimitRecruit = 4,

}
--endregion ----------- 招募 end -----------

--region ------------- 禁手秘卷 -------------
---@class GlobalDefine.ePrductLockType 秘卷品阶解锁状态
GlobalDefine.ePrductLockType = {
    ---已解锁
    UnLock = 1,
    ---可解锁
    Locking = 2,
    ---未解锁
    Lock = 3,
}
--endregion ----------- 禁手秘卷 end -----------

--region ------------- 全明星 -------------
---@class GlobalDefine.eAllStarCardType 卡牌解锁状态
GlobalDefine.eAllStarCardType = {
    ---已解锁
    UnLock = 1,
    ---可解锁
    Locking = 2,
    ---未解锁
    Lock = 3,
}

---@class GlobalDefine.eAllStarAoYiState    奥义值状态
GlobalDefine.eAllStarAoYiState = {
    ---最大值
    Max = 1,
    ---可增加
    Add = 2,
    ---无变化
    Normal = 3,
}
--endregion ----------- 全明星 end -----------

--region ------------- 特质、阿卡那启示 -------------
---@class GlobalDefine.eAcanaType 阿卡那启示解锁状态
GlobalDefine.eAcanaType = {
    ---已解锁
    UnLock = 1,
    ---解锁两条
    LockTow = 2,
    ---解锁一条
    LockOne = 3,
    ---未解锁
    Lock = 4,
}

---@class GlobalDefine.eTraitsType 特质类型
GlobalDefine.eTraitsType = {
    ---暴怒
    Fury = "Fury",
    ---节制
    Contrl = "Contrl",
    ---傲慢
    Arroganece = "Arrogance",
    ---坚韧
    Tough = "Tough",
    ---贪婪
    Greed = "Greed",
    ---谦逊
    Modesty = "Modesty",
    ---色欲
    Letch = "Letch",
    ---慷慨
    Bounty = "Bounty",
    ---暴食
    Gluttony = "Gluttony",
    ---忠诚
    Loyal = "Loyal",
    ---懒惰
    Lazy = "Lazy",
    ---宽容
    Bear = "Bear",
}

---@class GlobalDefine.eTraitsBuildType 特质城镇建筑类型
GlobalDefine.eTraitsBuildType = {
    ---酒吧
    Bar = "Bar",
    ---游戏厅
    GameRoom = "GameRoom",
    ---影院
    Cinema = "Cinema",
    ---公园
    Park = "Park",
    ---酒店
    Hotel = "Hotel",
    ---健身房
    Gym = "Gym",
}

---@class GlobalDefine.eTraitsLimitType 特质限定类型
GlobalDefine.eTraitsLimitType = {
    ---无限制
    None = "None",
    ---限定卡牌
    CardSId = "CardSId",
    ---限定职业
    Career = "Career",
    ---限定阵营
    Camp = "Camp",
}



--endregion ----------- 特质、阿卡那启示 end -----------
return GlobalDefine
