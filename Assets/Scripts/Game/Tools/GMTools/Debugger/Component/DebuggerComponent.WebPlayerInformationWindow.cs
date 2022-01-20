//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2017 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace Framework.Debugger
{
    public partial class DebuggerComponent
    {
        private sealed class WebPlayerInformationWindow : ScrollableDebuggerWindowBase
        {
            protected override void OnDrawScrollableWindow()
            {
                GUILayout.Label("<b>Web Player Information</b>");
                GUILayout.BeginVertical("box");
                {

                    DrawItem("Absolute URL:", Application.absoluteURL);
#if UNITY_5_3 || UNITY_5_4
                    DrawItem("Streamed Bytes:", Application.streamedBytes.ToString());
                     DrawItem("Source Value:", Application.srcValue);
                     DrawItem("Is Web Player:", Application.isWebPlayer.ToString());
                    DrawItem("Web Security Enabled:", Application.webSecurityEnabled.ToString());
                    DrawItem("Web Security Host URL:", Application.webSecurityHostUrl.ToString());
#endif
                }
                GUILayout.EndVertical();
            }
        }
    }
}
