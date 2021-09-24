---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.Input
---@field static simulateMouseWithTouches System.Boolean
---@field static anyKey System.Boolean
---@field static anyKeyDown System.Boolean
---@field static inputString string
---@field static mousePosition UnityEngine.Vector3
---@field static mouseScrollDelta UnityEngine.Vector2
---@field static imeCompositionMode UnityEngine.IMECompositionMode
---@field static compositionString string
---@field static imeIsSelected System.Boolean
---@field static compositionCursorPos UnityEngine.Vector2
---@field static mousePresent System.Boolean
---@field static touchCount int32
---@field static touchPressureSupported System.Boolean
---@field static stylusTouchSupported System.Boolean
---@field static touchSupported System.Boolean
---@field static multiTouchEnabled System.Boolean
---@field static deviceOrientation UnityEngine.DeviceOrientation
---@field static acceleration UnityEngine.Vector3
---@field static compensateSensors System.Boolean
---@field static accelerationEventCount int32
---@field static backButtonLeavesApp System.Boolean
---@field static location UnityEngine.LocationService
---@field static compass UnityEngine.Compass
---@field static gyro UnityEngine.Gyroscope
---@field static touches UnityEngine.Touch[]
---@field static accelerationEvents UnityEngine.AccelerationEvent[]
---@field static simulateMouseWithTouches System.Boolean
---@field static anyKey System.Boolean
---@field static anyKeyDown System.Boolean
---@field static inputString string
---@field static mousePosition UnityEngine.Vector3
---@field static mouseScrollDelta UnityEngine.Vector2
---@field static imeCompositionMode UnityEngine.IMECompositionMode
---@field static compositionString string
---@field static imeIsSelected System.Boolean
---@field static compositionCursorPos UnityEngine.Vector2
---@field static mousePresent System.Boolean
---@field static touchCount int32
---@field static touchPressureSupported System.Boolean
---@field static stylusTouchSupported System.Boolean
---@field static touchSupported System.Boolean
---@field static multiTouchEnabled System.Boolean
---@field static deviceOrientation UnityEngine.DeviceOrientation
---@field static acceleration UnityEngine.Vector3
---@field static compensateSensors System.Boolean
---@field static accelerationEventCount int32
---@field static backButtonLeavesApp System.Boolean
---@field static location UnityEngine.LocationService
---@field static compass UnityEngine.Compass
---@field static gyro UnityEngine.Gyroscope
---@field static touches UnityEngine.Touch[]
---@field static accelerationEvents UnityEngine.AccelerationEvent[]
local Input = {}

---@param axisName string
---@return number
function Input.GetAxis(axisName) end

---@param axisName string
---@return number
function Input.GetAxisRaw(axisName) end

---@param buttonName string
---@return System.Boolean
function Input.GetButton(buttonName) end

---@param buttonName string
---@return System.Boolean
function Input.GetButtonDown(buttonName) end

---@param buttonName string
---@return System.Boolean
function Input.GetButtonUp(buttonName) end

---@param button int32
---@return System.Boolean
function Input.GetMouseButton(button) end

---@param button int32
---@return System.Boolean
function Input.GetMouseButtonDown(button) end

---@param button int32
---@return System.Boolean
function Input.GetMouseButtonUp(button) end

function Input.ResetInputAxes() end

---@param joystickName string
---@return System.Boolean
function Input.IsJoystickPreconfigured(joystickName) end

---@return System.String[]
function Input.GetJoystickNames() end

---@param index int32
---@return UnityEngine.Touch
function Input.GetTouch(index) end

---@param index int32
---@return UnityEngine.AccelerationEvent
function Input.GetAccelerationEvent(index) end

---@param key UnityEngine.KeyCode
---@return System.Boolean
function Input.GetKey(key) end

---@param name string
---@return System.Boolean
function Input.GetKey(name) end

---@param key UnityEngine.KeyCode
---@return System.Boolean
function Input.GetKeyUp(key) end

---@param name string
---@return System.Boolean
function Input.GetKeyUp(name) end

---@param key UnityEngine.KeyCode
---@return System.Boolean
function Input.GetKeyDown(key) end

---@param name string
---@return System.Boolean
function Input.GetKeyDown(name) end

---@param axisName string
---@return number
function Input.GetAxis(axisName) end

---@param axisName string
---@return number
function Input.GetAxisRaw(axisName) end

---@param buttonName string
---@return System.Boolean
function Input.GetButton(buttonName) end

---@param buttonName string
---@return System.Boolean
function Input.GetButtonDown(buttonName) end

---@param buttonName string
---@return System.Boolean
function Input.GetButtonUp(buttonName) end

---@param button int32
---@return System.Boolean
function Input.GetMouseButton(button) end

---@param button int32
---@return System.Boolean
function Input.GetMouseButtonDown(button) end

---@param button int32
---@return System.Boolean
function Input.GetMouseButtonUp(button) end

function Input.ResetInputAxes() end

---@param joystickName string
---@return System.Boolean
function Input.IsJoystickPreconfigured(joystickName) end

---@return System.String[]
function Input.GetJoystickNames() end

---@param index int32
---@return UnityEngine.Touch
function Input.GetTouch(index) end

---@param index int32
---@return UnityEngine.AccelerationEvent
function Input.GetAccelerationEvent(index) end

---@param key UnityEngine.KeyCode
---@return System.Boolean
function Input.GetKey(key) end

---@param name string
---@return System.Boolean
function Input.GetKey(name) end

---@param key UnityEngine.KeyCode
---@return System.Boolean
function Input.GetKeyUp(key) end

---@param name string
---@return System.Boolean
function Input.GetKeyUp(name) end

---@param key UnityEngine.KeyCode
---@return System.Boolean
function Input.GetKeyDown(key) end

---@param name string
---@return System.Boolean
function Input.GetKeyDown(name) end

return Input
