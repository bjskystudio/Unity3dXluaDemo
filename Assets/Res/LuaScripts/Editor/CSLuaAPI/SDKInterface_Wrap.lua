---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SDKInterface
---@field public StepKeyValue System.Collections.Generic.Dictionary
local SDKInterface = {}

---@param key string
---@param step int32
function SDKInterface:AddAutoSetpFlag(key,step) end

function SDKInterface:Init() end

---@param type string
function SDKInterface:getDynamicUpdate(type) end

function SDKInterface:downDynamicUpdate() end

function SDKInterface:updateInfo() end

---@param serverSid string
---@param flags int32
function SDKInterface:loginEx(serverSid,flags) end

function SDKInterface:getServerList() end

---@param serverSid string
function SDKInterface:loginServer(serverSid) end

function SDKInterface:getMaintainNotice() end

function SDKInterface:loginout() end

function SDKInterface:showExit() end

function SDKInterface:exit() end

function SDKInterface:isMinor() end

---@param name string
---@param idcard string
function SDKInterface:realNameRegister(name,idcard) end

function SDKInterface:getGoodsList() end

---@param productId string
function SDKInterface:buy(productId) end

function SDKInterface:getGoodsList_pro() end

---@param productId string
---@param extra string
function SDKInterface:buy_pro(productId,extra) end

---@param step int32
function SDKInterface:gameStepInfo(step) end

---@param step int32
---@param s string
function SDKInterface:gameStepInfoFlag(step,s) end

---@param roleId string
---@param roleName string
---@param roleLevel string
---@param zoneId string
---@param zoneName string
---@param createRoleTime string
---@param extra string
function SDKInterface:createRole(roleId,roleName,roleLevel,zoneId,zoneName,createRoleTime,extra) end

---@param roleId string
---@param roleName string
---@param roleLevel string
---@param zoneId string
---@param zoneName string
---@param createRoleTime string
---@param extra string
function SDKInterface:enterGame(roleId,roleName,roleLevel,zoneId,zoneName,createRoleTime,extra) end

---@param roleId string
---@param roleName string
---@param roleLevel string
---@param zoneId string
---@param zoneName string
---@param createRoleTime string
---@param extra string
function SDKInterface:gameRoleInfo(roleId,roleName,roleLevel,zoneId,zoneName,createRoleTime,extra) end

---@param level int32
function SDKInterface:levelUp(level) end

---@param content string
---@param seconds int32
---@param jobId int32
function SDKInterface:pushNotification(content,seconds,jobId) end

---@param jobId int32
function SDKInterface:cleanAllNotifi(jobId) end

function SDKInterface:cleanAllNotifi() end

---@param serverIp string
---@param serverPort int32
---@param userid string
function SDKInterface:udpPush(serverIp,serverPort,userid) end

---@param url string
---@param quality int32
---@param vedioMaxTime int32
function SDKInterface:startRecordVedio(url,quality,vedioMaxTime) end

function SDKInterface:stopRecordVedio() end

---@param vedioUrl string
function SDKInterface:playVedio(vedioUrl) end

function SDKInterface:stopPlayingVideo() end

function SDKInterface:openNotchScreen() end

function SDKInterface:getAppInfo() end

function SDKInterface:enterSocial() end

function SDKInterface:getMemory() end

function SDKInterface:getBattery() end

function SDKInterface:isEmulator() end

---@param content string
function SDKInterface:setClipboard(content) end

return SDKInterface
