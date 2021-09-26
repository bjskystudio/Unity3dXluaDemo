---===================== Author Qcbf 这是自动生成的代码 =====================

---@class YoukiaCore.IO.ByteBuffer
---@field public Top int32
---@field public Position int32
---@field public bytesAvailable uint32
---@field static maxDataLength int32
---@field static EMPTY_ARRAY System.Byte[]
---@field static emptyString string
local ByteBuffer = {}

---@return int32
function ByteBuffer:Capacity() end

---@param len int32
function ByteBuffer:SetCapacity(len) end

---@return int32
function ByteBuffer:Length() end

---@return System.Byte[]
function ByteBuffer:GetArray() end

---@return int32
function ByteBuffer:GetHashCode() end

---@param pos int32
---@return byte
function ByteBuffer:Read(pos) end

---@param b int32
---@param pos int32
function ByteBuffer:Write(b,pos) end

---@param data System.Byte[]
---@param pos int32
---@param len int32
function ByteBuffer:Read(data,pos,len) end

---@return System.Boolean
function ByteBuffer:ReadBoolean() end

---@return byte
function ByteBuffer:ReadByte() end

---@return int32
function ByteBuffer:ReadUnsignedByte() end

---@return System.Char
function ByteBuffer:ReadChar() end

---@return int16
function ByteBuffer:ReadShort() end

---@return int32
function ByteBuffer:ReadUnsignedShort() end

---@return int32
function ByteBuffer:ReadInt() end

---@return number
function ByteBuffer:ReadFloat() end

---@return int64
function ByteBuffer:ReadLong() end

---@return number
function ByteBuffer:ReadDouble() end

---@return int32
function ByteBuffer:ReadLength() end

---@return System.Byte[]
function ByteBuffer:ReadData() end

---@return string
function ByteBuffer:ReadString() end

---@param charsetName string
---@return string
function ByteBuffer:ReadString(charsetName) end

---@return string
function ByteBuffer:ReadUtf() end

---@param data System.Byte[]
---@param pos int32
---@param len int32
function ByteBuffer:Write(data,pos,len) end

---@param b System.Boolean
function ByteBuffer:WriteBoolean(b) end

---@param b int32
function ByteBuffer:WriteByte(b) end

---@param c int32
function ByteBuffer:WriteChar(c) end

---@param s int32
function ByteBuffer:WriteShort(s) end

---@param i int32
function ByteBuffer:WriteInt(i) end

---@param f number
function ByteBuffer:WriteFloat(f) end

---@param l int64
function ByteBuffer:WriteLong(l) end

---@param d number
function ByteBuffer:WriteDouble(d) end

---@param len int32
function ByteBuffer:WriteLength(len) end

---@param data System.Byte[]
function ByteBuffer:WriteData(data) end

---@param data System.Byte[]
---@param pos int32
---@param len int32
function ByteBuffer:WriteData(data,pos,len) end

---@param str string
function ByteBuffer:WriteString(str) end

---@param str string
---@param charsetName string
function ByteBuffer:WriteString(str,charsetName) end

---@param str string
function ByteBuffer:WriteUtf(str) end

---@return System.Byte[]
function ByteBuffer:ToArray() end

function ByteBuffer:Clear() end

---@param data YoukiaCore.IO.ByteBuffer
---@return System.Object
function ByteBuffer:BytesRead(data) end

---@param data YoukiaCore.IO.ByteBuffer
function ByteBuffer:BytesWrite(data) end

---@return System.Object
function ByteBuffer:Clone() end

---@param obj System.Object
---@return System.Boolean
function ByteBuffer:Equals(obj) end

---@return string
function ByteBuffer:ToString() end

---@param len int32
---@return string
function ByteBuffer:ReadUTFBytes(len) end

---@param str string
function ByteBuffer:WriteUTFBytes(str) end

---@param b System.Byte[]
function ByteBuffer:WriteBytes(b) end

---@param b System.Byte[]
---@param offset int32
---@param len int32
function ByteBuffer:WriteBytes(b,offset,len) end

---@param b YoukiaCore.IO.ByteBuffer
---@param offset uint32
---@param len uint32
function ByteBuffer:WriteBytes(b,offset,len) end

---@param data YoukiaCore.IO.ByteBuffer
---@param pos int32
---@param len int32
function ByteBuffer:ReadBytes(data,pos,len) end

---@param bytes System.Byte[]
---@param pos int32
---@param len int32
function ByteBuffer:ReadBytes(bytes,pos,len) end

return ByteBuffer
