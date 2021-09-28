-----------------------------------------------------------------------
-- Created by ODEI on 08. 七月 2019 15:50
--
-- @Description 时间扩展,单位秒
-----------------------------------------------------------------------
local EventManager = require("EventManager")
local TimerManager = require("TimerManager")
local EventID = require("EventID")
local Logger = require("Logger")

local Time = Time
local math = math
local tonumber = tonumber
local os = os
local os_date = os.date
local os_time = os.time
local string_format = string.format
local Now = CS.System.DateTime.Now

---@class TimeUtil
local TimeUtil = {}

local HOUR_OF_DAY = 24
local SECOND_OF_MINUTE = 60
local SECOND_OF_HOUR = 3600
local SECOND_OF_DAY = HOUR_OF_DAY * SECOND_OF_HOUR
local WEEK_OF_DAY = 7

local _serverTime = 0
local _realtimeSinceStartup = 0
local _disServerTime = 0
local _fdisServerTime = 0
local _currServerTime = 0
local _updateFunctionRuning = false
local _seocdsTimer = nil

---时间差(秒)
local TimeSecOffset = 0
---时间差(毫秒)
local TimeMsecOffset = 0
---服务器时区
local TimeZoneServer = 8
---客户端时区
local TimeZoneClient = os_date("%z") / 100
---服务器时区和客户端时区的差值
local TimeZoneOff = TimeZoneServer - TimeZoneClient
---开服时间
local OpenServerTime = 0

local function LocalTimeSec()
    return os_time()
end

local function LocalTimeMsec()
    return LocalTimeSec() * 1000 + (Now.Ticks / 10000) % 1000
end

function TimeUtil.UpdateServerTimeNow(serverTime)
    -- _serverTime = serverTime
    -- _realtimeSinceStartup = Time.unity_time.realtimeSinceStartup
    -- _disServerTime = _serverTime - _realtimeSinceStartup
    if (not _updateFunctionRuning) then
        _updateFunctionRuning = true
        -- UpdateManager:GetInstance():AddBeforeUpdate(TimeUtil._UpdateFunction)
        -- 秒更新事件
        if (_seocdsTimer ~= nil) then
            _seocdsTimer:Stop()
        end
        _seocdsTimer = TimerManager:GetInstance():GetTimer(1, TimeUtil._UpdaeSeconds, TimeUtil, false)
        _seocdsTimer:Start()
    end
    -- TimeUtil._UpdateFunction()

    serverTime = serverTime - 100
    TimeMsecOffset = LocalTimeMsec() - serverTime
    TimeSecOffset = math.floor(LocalTimeSec() - serverTime / 1000)
end

function TimeUtil._UpdaeSeconds()
    EventManager:GetInstance():Broadcast(EventID.SecondsUpdateMsg)
end

-- function TimeUtil._UpdateFunction()
--    _fdisServerTime = _disServerTime + Time.unity_time.realtimeSinceStartup
--    _currServerTime = math.floor(_fdisServerTime)
-- end

---切后台回来调时间刷新
function TimeUtil.OnApplicationFocusHandle()
    -- TimeUtil._UpdateFunction()
    TimeUtil._UpdaeSeconds()
end

---设置开服时间
---@param time number 开服时间戳
function TimeUtil.SetOpenServerTime(time)
    OpenServerTime = time
end

---获取开服时间
---@return number 开服时间戳
function TimeUtil.GetOpenServerTime()
    return OpenServerTime
end

---获取当前时间戳(秒)
function TimeUtil.GetSecTime()
    -- return _currServerTime
    return LocalTimeSec() - TimeSecOffset
end

---获取当前时间戳(毫秒)
function TimeUtil.GetMSecTime()
    return LocalTimeMsec() - TimeMsecOffset
end

-- region -------------业务扩展方法-------------

---通过秒时间戳返回描述
---@param format string 时间格式:%H:%M:%S or *t
---@param timeSec number 时间戳(秒)
---@return string|TimeTab
function TimeUtil.GetDateBySec(format, timeSec)
    timeSec = timeSec or TimeUtil.GetSecTime()
    return os_date(format, timeSec + TimeZoneOff * 3600)
end

---通过毫秒时间戳返回描述
---@param format string 时间格式:%H:%M:%S or *t
---@param timeMsec number 时间戳(毫秒)
---@return string|TimeTab
function TimeUtil.GetDateByMsec(format, timeMsec)
    timeMsec = timeMsec or TimeUtil.GetMSecTime()
    return os_date(format, math.floor(timeMsec / 1000) + TimeZoneOff * 3600)
end

---通过TimeTab返回秒时间戳
---@param table TimeTab
---@return number 时间戳(秒)
function TimeUtil.GetSecTimeByTab(table)
    return math.floor(os_time(table) - TimeZoneOff * 3600)
end

---通过TimeTab返回毫秒时间戳
---@param table TimeTab
---@return number 时间戳(毫秒)
function TimeUtil.GetMsecTimeByTab(table)
    return TimeUtil.GetSecTimeByTab(table) * 1000
end

---设置指定时间，返回时间戳
---@param year number 年(4位数)
---@param month number 月(1-12)
---@param day number 日(1-31)
---@param hour number 时(0-23)
---@param min number 分(0-59)
---@param sec number 秒(0-59)
---@return number 时间戳
function TimeUtil.SetTime(year, month, day, hour, min, sec)
    local osTime = os_time({
        year = year,
        month = month,
        day = day,
        hour = hour,
        min = min,
        sec = sec
    })
    return osTime
end

---获取当前几月
---@return number 几月
function TimeUtil.GetMonth(nowTime)
    return tonumber(os_date("%m", nowTime))
end

---获取当前几号
---@return number 几号
function TimeUtil.GetMonthDay(nowTime)
    return tonumber(os_date("%d", nowTime))
end

---获取当前时间对应月份总共天数
function TimeUtil.GetMonthForDay(nowTime)
    local tYear = tonumber(os_date("%Y", nowTime))
    local tMonth = tonumber(os_date("%m", nowTime))
    local tCurDate = {}
    tCurDate.year = tYear
    tCurDate.month = tMonth + 1
    tCurDate.day = 0
    return os_date("%d", os_time(tCurDate))
end

---获取星期（1-7， 周日返回7）
---@param timeSec number 时间戳(秒)
function TimeUtil.GetWeek(timeSec)
    local date = TimeUtil.GetDateBySec("*t", timeSec)
    local week = date.wday - 1
    return (week == 0) and 7 or week
end

---获取周一零点的时间戳
---@return number 秒
function TimeUtil:GetWeekStartSec(nowTime)
    nowTime = nowTime or TimeUtil.GetSecTime()
    local date = TimeUtil.GetDateBySec("*t", nowTime)
    return TimeUtil.GetSecondsOfDay(nowTime - (date.wday * SECOND_OF_DAY))
end

-- endregion
---当前是一天的第几秒
function TimeUtil.GetSecondsOfDay(now)
    local now = now or TimeUtil.GetSecTime()
    local zero = TimeUtil.GetDayBegin(now)
    return now - zero
end
---往前追朔到某一天的零点, 单位秒数
---@param time number 时间戳 单位秒
---@param useTimeZone boolean 是否计算时区，默认不计算
function TimeUtil.GetDayBegin(time, useTimeZone)
    local timetable = os_date("*t", time)
    timetable.hour = 0
    timetable.min = 0
    timetable.sec = 0
    if (useTimeZone) then
        local tTimeZone = TimeUtil.GetTimeZone()
        timetable.hour = timetable.hour + tTimeZone
    end
    return os_time(timetable)
end

---获得一个时间 time 距离时间 ref_time 的相对时间 比如 1小时前 2天后 ...
---@param time number 和参考时间比较的时间
---@param ref_time number 参考时间
function TimeUtil.RelativeTime(time, ref_time)
    local SECOND_OF_MONTH = SECOND_OF_DAY * 30
    local SECOND_OF_YEAR = SECOND_OF_DAY * 365

    local timeInSec = os.difftime(time, ref_time)
    local relative = "后"
    if timeInSec < 0 then
        timeInSec = 0 - timeInSec
        relative = "前"
    end

    local result = ""

    if timeInSec < SECOND_OF_YEAR then
        if timeInSec < SECOND_OF_MONTH then
            if timeInSec < SECOND_OF_DAY then
                if timeInSec < SECOND_OF_HOUR then
                    if timeInSec < SECOND_OF_MINUTE then
                        result = math.floor(timeInSec) .. "秒"
                    else
                        --- 超过一分钟
                        result = math.floor(timeInSec / SECOND_OF_MINUTE) .. "分钟"
                    end
                else
                    --- 超过一小时
                    result = math.floor(timeInSec / SECOND_OF_HOUR) .. "小时"
                end
            else
                --- 超过一天
                result = math.floor(timeInSec / SECOND_OF_DAY) .. "天"
            end
        else
            --- 超过一个月
            result = math.floor(timeInSec / SECOND_OF_MONTH) .. "个月"
        end
    else
        --- 超过一年
        result = math.floor(timeInSec / SECOND_OF_YEAR) .. "年"
    end

    return result .. relative
end

---天%s小时%s分%s秒
function TimeUtil.GetDateTimeStr(nowTime)
    local str = ""
    if (nowTime > 86400) then
        str = string.format("%s天%s小时%s分%s秒", math.floor(nowTime / 86400), math.floor((nowTime % 86400) / 3600), math.floor(((nowTime % 86400) % 3600) / 60), math.floor(((nowTime % 86400) % 3600) % 60));
    elseif (nowTime > 3600) then
        str = string.format("%s小时%s分%s秒", math.floor((nowTime % 86400) / 3600), math.floor(((nowTime % 86400) % 3600) / 60), math.floor(((nowTime % 86400) % 3600) % 60));
    elseif (nowTime > 60) then
        str = string.format("%s分%s秒", math.floor(((nowTime % 86400) % 3600) / 60), math.floor(((nowTime % 86400) % 3600) % 60));
    else
        str = string.format("%s秒", math.floor(nowTime % 86400))
    end
    return str
end

---只显示最大单位时间，小于1分钟显示1分钟
function TimeUtil.GetDateTimeStr1(nowTime)
    local str = ""
    if (nowTime > 86400) then
        str = string.format("%s天", math.floor(nowTime / 86400));
    elseif (nowTime > 3600) then
        str = string.format("%s小时", math.floor((nowTime % 86400) / 3600));
    elseif (nowTime > 60) then
        str = string.format("%s分钟", math.floor(((nowTime % 86400) % 3600) / 60));
    else
        str = string.format("1分钟")
    end
    return str
end

---剩余时间
function TimeUtil.FormatUnixTime(_time)
    if _time and _time >= 0 then
        ---一天的秒数86400
        local day = math.floor(_time / 60 / 60 / 24)
        local hour = math.floor(_time / 3600) % 24
        local minute = math.floor(_time / 60) % 60
        local second = math.floor(_time % 60)
        local dayStr = day > 0 and string.format("%s天", day) or ""
        local hourStr = hour > 0 and string.format("%s小时", hour) or ""
        local minuteStr = minute > 0 and string.format("%s分钟", minute) or ""
        local secondStr = second > 0 and string.format("%s秒", second) or ""
        return dayStr .. hourStr .. minuteStr .. secondStr
    end
end

---剩余时间
function TimeUtil.FormatUnixTime2(_time)
    if _time and _time >= 0 then
        ---一天的秒数86400
        local day = math.floor(_time / 60 / 60 / 24)
        local hour = math.floor(_time / 3600) % 24
        local minute = math.floor(_time / 60) % 60
        local second = math.floor(_time % 60)
        local dayStr = day > 0 and string.format("%s天", day) or ""
        local hourStr = hour > 0 and string.format("%s小时", hour) or ""
        local minuteStr = minute > 0 and string.format("%s分钟", minute) or ""
        local secondStr = second > 0 and string.format("%s秒", second) or ""
        if day > 0 then
            return dayStr .. hourStr
        elseif hour > 0 then
            return hourStr .. minuteStr
        elseif minute > 0 then
            return minuteStr .. secondStr
        elseif secondStr > 0 then
            return secondStr
        end
        --return dayStr .. hourStr .. minuteStr .. secondStr
    end
end

---将秒数转换为：00:00:00
---@param time number 时间戳秒数
---@param flag number nil为 时:分:秒 2为 时:分
---@param useTimeZone boolean 是否使用时区 默认不使用
---@return string
function TimeUtil.TimeToString(time, flag, useTimeZone)
    -- function TimeUtil.TimeToString(time, flag)
    local sec = time % SECOND_OF_MINUTE
    local min = math.floor(time / 60) % 60
    local hour = math.floor(time / SECOND_OF_HOUR)
    if (useTimeZone) then
        hour = hour + TimeUtil.GetTimeZone()
    end
    if flag == 2 then
        return string.format("%02d:%02d", hour, min)
        -- elseif flag == 1 then
    else
        return string.format("%02d:%02d:%02d", hour, min, sec)
    end
end

---将时间戳的秒数转换为今日的：00:00:00
---@param time number 时间戳秒数
---@param flag number nil为 时:分:秒 2为 时:分
---@param useTimeZone boolean 是否使用时区 默认不使用
---@return string
function TimeUtil.TimeStampToString(time, flag, useTimeZone)
    time = time % SECOND_OF_DAY
    -- function TimeUtil.TimeToString(time, flag)
    local sec = time % SECOND_OF_MINUTE
    local min = math.floor(time / 60) % 60
    local hour = math.floor(time / SECOND_OF_HOUR)
    if (useTimeZone) then
        hour = hour + TimeUtil.GetTimeZone()
    end
    if flag == 2 then
        return string.format("%02d:%02d", hour, min)
    else
        -- elseif flag == 1 then
        return string.format("%02d:%02d:%02d", hour, min, sec)
    end
end

-- 返回包含从年到秒的表
function TimeUtil.GetDateTime(nowTime)
    local tb = {}
    tb.year = tonumber(os_date("%Y", nowTime))
    tb.month = tonumber(os_date("%m", nowTime))
    tb.day = tonumber(os_date("%d", nowTime))
    tb.week = tonumber(os_date("%w", nowTime))
    tb.hour = tonumber(os_date("%H", nowTime))
    tb.minute = tonumber(os_date("%M", nowTime))
    tb.second = tonumber(os_date("%S", nowTime))
    return tb
end

---计算出服务器时间的时区
---@return number @时区
function TimeUtil.GetTimeZone()
    local time = TimeUtil.GetSecTime()
    local a = os_date('!*t', time) -- 中时区的时间
    local b = os_date('*t', time)
    local timeZone = (b.hour - a.hour) * 3600 + (b.min - a.min) * 60
    timeZone = timeZone / 3600
    return timeZone
end

---通过时间戳及本系统时区算出其他时区时间
---@param time_zone number @时区
---@param time number @时间戳
---@return string @时区时间
function TimeUtil.GetTimeZoneDesc(time_zone, time)
    if not time_zone or time_zone < -12 or time_zone > 12 then
        Logger.Warning("<color=red>" .. "时区参数传入错误" .. "</color>", "")
        return ""
    end

    time = time or TimeUtil.GetSecTime()
    if time <= 0 then
        Logger.Warning("<color=red>" .. "时间戳参数传入错误" .. "</color>", "")
        return ""
    end

    local curTimeZone = TimeUtil.GetTimeZone()
    -- 算出对应时区秒数
    time = time - (curTimeZone - time_zone) * SECOND_OF_HOUR
    local date = TimeUtil.GetDateTime(time)
    local str = ""

    if time_zone >= 0 then
        str = "+" .. tostring(time_zone)
    else
        str = tostring(time_zone)
    end

    local second = TimeUtil.GetSecondsOfDay(time)
    local timeDes = TimeUtil.TimeToString(second, 1)

    return "UTC " .. str .. " " .. date.year .. "/" .. date.month .. "/" .. date.day .. " " .. timeDes
end

--[[
---获取当前实时的时间戳 天
function TimeUtil.GetNowUnixTimeToDay()
    return TimeUtil.GetDay(TimeUtil.GetSecTime())
end

---获取当前实时的时间戳 周
function TimeUtil.GetNowUnixTimeToWeek()
    return TimeUtil.GetWeek(TimeUtil.GetSecTime())
end





---获取间隔的天数
function TimeUtil.GetIntervalDay(timeBegin, tiemEnd)
    local dayBegTime1 = TimeUtil.GetDayBegin(timeBegin)
    local dayBegTime2 = TimeUtil.GetDayBegin(tiemEnd)
    return math.floor((math.abs(dayBegTime2 - dayBegTime1)) / SECOND_OF_DAY)
end



---检查两个时间是否为同一天
function TimeUtil.CheckTheSameDay(time1, time2)
    return TimeUtil.GetIntervalDay(time1, time2) == 0
end

---获取当前服务器 年，月，日，时，分，秒
---@return { year:number, month:number, day: number, hour : number, min:number, sec:number }
function TimeUtil.GetCurrentTimeDate()
    local timetable = osdate("*t", TimeUtil.GetSecTime())
    return timetable
end

---获取当前服务器 星期几 1,2,3,4,5,6,0
function TimeUtil.GetNowDayOfWeek()
    local dw = osdate("%w", TimeUtil.GetSecTime())
    return tonumber(dw)
end

---@param time number
function TimeUtil.GetDateTime(time)
    if time == nil then
        return osdate("*t", TimeUtil.GetSecTime())
    end

    return osdate("*t", math.modf(time))
end

function TimeUtil.GetNowDateTime()
    return osdate("*t", TimeUtil.GetSecTime())
end

function TimeUtil.GetDay(time)
    local timetable = osdate("*t", time)
    timetable.hour = 0
    timetable.min = 0
    timetable.sec = 0
    return math.floor(ostime(timetable) / SECOND_OF_DAY)
end

---@param time number
function TimeUtil.GetWeek(time)
    --时间戳七点距离下周时间
    local StartNextTime = 3 * SECOND_OF_DAY + (16 * SECOND_OF_HOUR)
    --大于第一周时间则为2开头计算
    if time >= StartNextTime then
        local timetable = osdate("*t", time - StartNextTime)
        return math.floor((ostime(timetable)) / (WEEK_OF_DAY * SECOND_OF_DAY)) + 2
    else
        return 1
    end
end

function TimeUtil.DayOfMonth(time)
    return osdate("%d", math.modf(time))
end

function TimeUtil.DayOfWeek(time)
    return osdate("%w", math.modf(time))
end

function TimeUtil.DayOfYear(time)
    local normalYear = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }
    local speciaYear = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }

    local currentYear = {}
    local year = TimeUtil.GetNowDateTime().year
    local dayOfMonth = TimeUtil.GetNowDateTime().month
    if not TimeUtil.isLeapYear(year) then
        --平年
        currentYear = normalYear
    else
        --闰年
        currentYear = speciaYear
    end

    local day = TimeUtil.DayOfMonth(time)
    for i = 1, dayOfMonth do
        day = day + currentYear[i]
    end

    return day
end

function TimeUtil.isLeapYear(year)
    return year % 4 == 0 and (year % 100 ~= 0 or year % 400 == 0)
end
--]]

--- 每天的小时数
TimeUtil.HOUR_OF_DAY = HOUR_OF_DAY
--- 每分钟的描述
TimeUtil.SECOND_OF_MINUTE = SECOND_OF_MINUTE
--- 每小时的描述
TimeUtil.SECOND_OF_HOUR = SECOND_OF_HOUR
--- 每天的秒数
TimeUtil.SECOND_OF_DAY = SECOND_OF_DAY
--- 每周的天数
TimeUtil.WEEK_OF_DAY = WEEK_OF_DAY

---@return TimeUtil TimeUtil
return TimeUtil

---@class TimeTab
---@field yday number 每年第几天
---@field wday number 星期1–7, 星期天是1
---@field year number 年(4位数)
---@field month number 月(1-12)
---@field day number 日(1-31)
---@field hour number 时(0-23)
---@field min number 分(0-59)
---@field sec number 秒(0-59)
