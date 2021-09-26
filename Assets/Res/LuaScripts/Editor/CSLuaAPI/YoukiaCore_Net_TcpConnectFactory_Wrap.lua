---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.Net.TcpConnectFactory
local TcpConnectFactory = {}

---@param address string
---@param port int32
---@return YoukiaCore.Net.TcpConnect
function TcpConnectFactory.GetConnect(address,port) end

---@param connect YoukiaCore.Net.TcpConnect
function TcpConnectFactory.Close(connect) end

function TcpConnectFactory.CloseAll() end

return TcpConnectFactory
