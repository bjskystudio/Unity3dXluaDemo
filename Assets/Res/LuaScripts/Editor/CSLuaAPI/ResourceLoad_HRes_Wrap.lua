---===================== Author Qcbf 这是自动生成的代码 =====================

---@class ResourceLoad.HRes
---@field public Asset System.Object
---@field public ABDep ResourceLoad.HAssetBundle
---@field public IsLoadAll System.Boolean
---@field public IsReturnAll System.Boolean
---@field public AssetPathInit string
---@field public AssetPath string
---@field public AssetName string
---@field public ResName string
---@field public AssetType ResourceLoad.AssetType
---@field public RefCount int32
---@field public RecycleBinPutInTime number
local HRes = {}

---@param assetPath string
---@param assetName string
---@param resName string
---@param assetType ResourceLoad.AssetType
---@param isAll System.Boolean
function HRes:Init(assetPath,assetName,resName,assetType,isAll) end

---@param isSync System.Boolean
---@param isAll System.Boolean
---@param isPreLoad System.Boolean
---@param callback System.Action
function HRes:StartLoad(isSync,isAll,isPreLoad,callback) end

function HRes:ReleaseAll() end

function HRes:AddRef() end

---@param isImmediately System.Boolean
function HRes:Release(isImmediately) end

function HRes:ReleaseReal() end

---@return System.Type
function HRes:GetRealType() end

---@return System.Collections.Generic.List
function HRes:GetExtesions() end

return HRes
