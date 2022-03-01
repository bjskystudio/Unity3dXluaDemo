
using UnityEngine;
using System.Net.NetworkInformation;

namespace YoukiaEngine
{
    public class GPUDeviceInfo
    {
        public enum DeviceType
        {
            Unknown,
            HuaWei,
            XiaoMi,
            Oppo,
            Vivo,
            Samsung,
            Apple,
        }

        private static DeviceType _Type = DeviceType.Unknown;
        private static int _CheckSimulator = -1;
        private static int _CheckInstancing = -1;

        public static void SetDeviceType(DeviceType type)
        {
            _Type = type;
        }

        public static DeviceType GetDeviceType()
        {
            if (_Type == DeviceType.Unknown)
            {// 如果客户端没有设置，则内部自己检查一次
                if (CheckIsMali())
                {
                    _Type = DeviceType.HuaWei;
                }
            }
            return _Type;
        }

        public static bool CheckIsHuaWei()
        {
            if (SystemInfo.deviceModel.Contains("HUAWEI") && SystemInfo.graphicsDeviceName.Contains("Mali"))
            {
                return true;
            }
            return false;
        }

        public static bool CheckIsMali()
        {
            if (SystemInfo.graphicsDeviceName.Contains("Mali"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是模拟器
        /// </summary>
        /// <returns></returns>
        public static bool IsSimulator()
        {
            if (_CheckSimulator == -1)
            {
                _CheckSimulator = HasNetInfo() ? 1 : 0;
            }
            return (_CheckSimulator == 0);
        }

        /// <summary>
        /// 不能获取网卡信息，一般就是虚拟机
        /// </summary>
        /// <returns></returns>
        private static bool HasNetInfo()
        {
            NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
            if (nis != null && nis.Length > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否支持Instancing
        /// </summary>
        /// <returns></returns>
        public static bool SupportsInstancing()
        {
            if (_CheckInstancing == -1)
            {
                if (!SystemInfo.supportsInstancing)
                {
                    _CheckInstancing = 0;
                    return false;
                }

                if (IsSimulator())
                {
                    if (SystemInfo.deviceModel.Contains("Tencent") && !SystemInfo.graphicsDeviceName.Contains("OpenGL"))
                    {
                        // 腾讯模拟器DX模式supportsInstancing为true，但不支持
                        //Debug.LogWarning("Simulator Instancing false");

                        _CheckInstancing = 0;
                        return false;
                    }
                }

               _CheckInstancing = 1;
            }

            return _CheckInstancing == 1;
        }

        /// <summary>
        /// 设置是否支持Instancing，0不支持，1支持，-1内部自动检测
        /// </summary>
        public static void SetSupportsInstancing(int state = -1)
        {
            _CheckInstancing = state;
        }

        /// <summary>
        /// 是否支持MSAA
        /// </summary>
        /// <returns></returns>
        public static bool SupportsMSAA()
        {
            if (GPUDeviceInfo.IsSimulator())
            {// 多款模拟器有问题，直接屏蔽所有模拟器
                return false;
            }
            if (!SupportsInstancing())
            {// 蓝叠模拟器判断不出来是否是模拟器，默认设置（不开高级模式时，不支持Instancing和MSAA）
                return false;
            }
            if(GPUDeviceInfo.GetDeviceType() == GPUDeviceInfo.DeviceType.HuaWei)
            {// 华为手机和空气扭曲特效冲突
                return false;
            }
            if (GPUDeviceInfo.CheckIsMali())
            {
                return false;
            }
            return true;
        }
    }
}
