---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Net.TcpConnect : YoukiaCore.Event.BaseEvent
---@field public Address string
---@field public Port int32
---@field public IsCertify System.Boolean
---@field public ConnectTimeOut int32
---@field public PingIntervalTime int32
---@field static DEBUG_LOG System.Boolean
---@field static SEND_BREAK System.Boolean
---@field static RECIVE_BREAK System.Boolean
local TcpConnect = {}

---@return System.Boolean
function TcpConnect:CanConnect() end

function TcpConnect:BeginConnect() end

---@param bytes System.Byte[]
---@param index int32
---@param length int32
function TcpConnect:Send(bytes,index,length) end

function TcpConnect:Update() end

return TcpConnect
