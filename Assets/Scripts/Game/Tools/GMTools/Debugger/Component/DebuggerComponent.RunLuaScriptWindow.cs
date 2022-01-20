using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Debugger
{
    public partial class DebuggerComponent
    {
        private sealed class RunLuaScriptWindow : ScrollableDebuggerWindowBase
        {
            string luaScriptStr;

            protected override void OnDrawScrollableWindow()
            {
                Show();
            }
            void Show()
            {

                GUILayout.BeginVertical();
                if (GUILayout.Button("清除代码"))
                {
                    luaScriptStr = string.Empty;
                }

                if (GUILayout.Button("运行将要执行的Lua代码"))
                {
                    if (string.IsNullOrEmpty(luaScriptStr))
                    {
                        return;
                    }
                    string script = @luaScriptStr;
                    XLuaManager.Instance.GetLuaEnv().DoString(script);
                }

                GUILayout.Label("输入将要执行的Lua代码");
                luaScriptStr = GUILayout.TextArea(luaScriptStr);
                GUILayout.EndVertical();
            }
        }
    }
}