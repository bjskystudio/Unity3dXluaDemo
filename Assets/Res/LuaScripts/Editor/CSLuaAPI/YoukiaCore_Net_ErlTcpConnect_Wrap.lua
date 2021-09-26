---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Net.ErlTcpConnect : YoukiaCore.Net.TcpConnect
---@field public DefCode System.Boolean
---@field public DefCrc System.Boolean
---@field public DefCompress System.Boolean
---@field public WaitBcSize int32
---@field public BcSize int32
---@field static SerialNumber int32
local ErlTcpConnect = {}

---@param bytes System.Byte[]
---@param cmd string
---@param number int32
function ErlTcpConnect:Send(bytes,cmd,number) end

---@param bytes System.Byte[]
---@param cmd string
---@param number int32
---@param isCode System.Boolean
---@param isCrc System.Boolean
---@param isCompress System.Boolean
function ErlTcpConnect:Send(bytes,cmd,number,isCode,isCrc,isCompress) end

function ErlTcpConnect:Update() end

return ErlTcpConnect
