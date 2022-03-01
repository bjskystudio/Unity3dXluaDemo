using System;
using UnityEngine;

namespace Framework.TimelineExtend
{
    public class TimelineExtend
    {

        /// <summary>
        /// LoadDynamicCharacter方法注入钩子
        /// </summary>
        public static Func<string, GameObject> LoadDynamicCharacterHook;
        /// <summary>
        /// 根据字符串指令加载动态角色
        /// </summary>
        public static GameObject LoadDynamicCharacter(string command)
        {
            if (LoadDynamicCharacterHook != null) return LoadDynamicCharacterHook(command);
            return null;
        }
    }

}