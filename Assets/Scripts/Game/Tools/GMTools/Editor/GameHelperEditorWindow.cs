/*
 * Description:             GameHelperEditorWindow.cs
 * Author:                  TANGHUAN
 * Create Date:             2019/08/29
 */

using LuaBehaviourTree;
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Log;
using YoukiaCore.Net;

namespace Game
{
    /// <summary>
    /// 功能模块类型
    /// </summary>
    public enum EModuleType
    {
        LuaSystem = 0,              //Lua系统
        CommonSystem,               //公用系统
        BehaviourTreeSystem,        //行为树系統
        GuideSystem,                //新手引导系统
        UIAdaptation,               //UI适配
    }

    /// <summary>
    /// 游戏辅助工具
    /// </summary>
    public class GameHelperEditorWindow : EditorWindow
    {
        /// <summary>
        /// 模块选项
        /// </summary>
        private string[] mModuleOptions = Enum.GetNames(typeof(EModuleType));

        /// <summary>
        /// 当前模块选择索引
        /// </summary>
        private int mCurrentSelectedModuleIndex = 0;

        /// <summary>
        /// 当前滚动位置
        /// </summary>
        private Vector2 mCurrentScrollPos;

        /// <summary>
        /// 参数1
        /// </summary>
        private string mParam1;

        /// <summary>
        /// 参数2
        /// </summary>
        private string mParam2;

        /// <summary>
        /// 参数3
        /// </summary>
        private string mParam3;

        /// <summary>
        /// 参数4
        /// </summary>
        private string mParam4;

        /// <summary>
        /// 对象1
        /// </summary>
        private GameObject mGameObejct1;

        /// <summary>
        /// 对象2
        /// </summary>
        private GameObject mGameObejct2;

        /// <summary>
        /// 资源1
        /// </summary>
        private UnityEngine.Object mAsset1;

        /// <summary>
        /// 资源2
        /// </summary>
        private UnityEngine.Object mAsset2;

        /// <summary>
        /// 游戏速度
        /// </summary>
        private float mTimeScale = 1f;

        /// <summary>
        /// 参数配置说明
        /// </summary>
        private string mParamConfigIntroduction;

        /// <summary>
        /// 自定义GUISkin
        /// </summary>
        private GUISkin mCustomGUISkin;

        /// <summary>
        /// 自定义GUISkin Asset路径
        /// </summary>
        private const string mCustomGUISkinAssetPath = "Assets/Scripts/Game/Tools/GMTools/Editor/Res/GameHelperGUISkin.guiskin";

        [MenuItem("Tools/游戏/游戏辅助工具", false, 101)]
        static void Init()
        {
            GameHelperEditorWindow window = (GameHelperEditorWindow)EditorWindow.GetWindow(typeof(GameHelperEditorWindow), false, "游戏辅助工具");
            window.Show();
        }

        public GameHelperEditorWindow()
        {
            //Debug.Log("GameHelperEditorWindow()");
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void Awake()
        {
            //Debug.Log("GameHelperEditorWindow:Awake()");
            mCustomGUISkin = AssetDatabase.LoadAssetAtPath<GUISkin>(mCustomGUISkinAssetPath);
            mCustomGUISkin.textArea.fontSize = 16;
            mCustomGUISkin.textField.fontSize = 16;
            mCustomGUISkin.label.fontSize = 14;
            mCustomGUISkin.button.fontSize = 14;
            mCustomGUISkin.toggle.fontSize = 14;
            mCustomGUISkin.label.alignment = TextAnchor.MiddleLeft;

        }

        private void OnDestroy()
        {
            Debug.Log("GameHelperEditorWindow:OnDestroy()");
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            //Debug.Log(state);
            if (state == PlayModeStateChange.EnteredPlayMode)
            {

            }
        }

        void OnGUI()
        {
            if (Launcher.Instance != null && Launcher.Instance.UsedLuaAssetBundle)
            {
                return;
            }
            GUI.skin = mCustomGUISkin;
            mModuleOptions = Enum.GetNames(typeof(EModuleType));
            mCurrentScrollPos = EditorGUILayout.BeginScrollView(mCurrentScrollPos);
            EditorGUILayout.BeginVertical();
            DisplayParamsArea();
            DisplayUniversalFunctionArea();
            DisplayFunctionArea();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 显示参数区
        /// </summary>
        private void DisplayParamsArea()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("数据区:", GUILayout.Width(80.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("参数1:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mParam1 = EditorGUILayout.TextField(mParam1, GUILayout.Width(400.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("参数2:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mParam2 = EditorGUILayout.TextField(mParam2, GUILayout.Width(400.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("参数3:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mParam3 = EditorGUILayout.TextField(mParam3, GUILayout.Width(400.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("参数4:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mParam4 = EditorGUILayout.TextField(mParam4, GUILayout.Width(400.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("对象1:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mGameObejct1 = (GameObject)EditorGUILayout.ObjectField(mGameObejct1, typeof(GameObject), true, GUILayout.Width(170.0f), GUILayout.Height(20));
            GUILayout.Space(10);
            GUILayout.Label("对象2:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mGameObejct2 = (GameObject)EditorGUILayout.ObjectField(mGameObejct2, typeof(GameObject), true, GUILayout.Width(170.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("资源1:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mAsset1 = EditorGUILayout.ObjectField(mAsset1, typeof(UnityEngine.Object), true, GUILayout.Width(170.0f), GUILayout.Height(20));
            GUILayout.Space(10);
            GUILayout.Label("资源2:", GUILayout.Width(50.0f), GUILayout.Height(20));
            mAsset2 = EditorGUILayout.ObjectField(mAsset2, typeof(UnityEngine.Object), true, GUILayout.Width(170.0f), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            // 经测试发现GUIContent的提示功能在runtime时不可用，所以采用单独的Lable显示提示的方式
            if (!string.IsNullOrEmpty(mParamConfigIntroduction))
            {
                EditorGUILayout.BeginHorizontal();
                EditorUtilities.DisplayDIYGUILable(mParamConfigIntroduction, Color.yellow, 10, 450.0f, 40.0f);
                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// 显示通用功能区
        /// </summary>
        private void DisplayUniversalFunctionArea()
        {
            GUILayout.Space(10);
            //DisplayTitle("常驻功能区:");
            //EditorGUILayout.BeginHorizontal();
            //GUILayout.Space(10);

            ////TODO 常驻添加

            //EditorGUILayout.EndHorizontal();
            if (!Application.isPlaying)
            {
                // 非运行时才提供的功能
                #region Editor Run Function
                DisplayTitle("非运行时功能区:");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("打开主场景", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    GameHelperUtilities.OpenScene("Assets/Scenes/Main.unity");
                }
                EditorGUILayout.EndHorizontal();
                #endregion
            }
            else
            {
                #region RunTimeFunction
                // 运行时才提供的功能
                DisplayTitle("运行时功能区:");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUILayout.Label("游戏速度调整:", GUILayout.Width(100.0f), GUILayout.Height(20));
                mTimeScale = EditorGUILayout.Slider(mTimeScale, 0.01f, 5.0f, GUILayout.Width(200.0f));
                Time.timeScale = mTimeScale;
                EditorGUILayout.EndHorizontal();
                DisplayTitle("系统功能测试:");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUI.color = TcpConnect.SEND_BREAK ? Color.red : Color.white;
                if (GUILayout.Button(TcpConnect.SEND_BREAK ? "恢复发送" : "断开发送", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    TcpConnect.SEND_BREAK = !TcpConnect.SEND_BREAK;
                }
                GUI.color = TcpConnect.RECIVE_BREAK ? Color.red : Color.white;
                if (GUILayout.Button(TcpConnect.RECIVE_BREAK ? "恢复接收" : "断开接收", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    TcpConnect.RECIVE_BREAK = !TcpConnect.RECIVE_BREAK;
                }
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();
                DisplayTitle("Log系统:");
                DisplayLogSystem();
                #endregion
            }

            #region 一直提供的功能
            DisplayTitle("常驻功能区:");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("获取节点层级路径", GUILayout.Width(150), GUILayout.Height(20)))
            {
                mParamConfigIntroduction = "获取节点层级路径说明:\n 对象1: 拖入父节点 对象2: 拖入子节点";
                if (mGameObejct1 == null || mGameObejct2 == null)
                {
                    Log.Info("节点信息不能赋空!");
                }
                else
                {
                    EditorGUIUtility.systemCopyBuffer = CSGoHelp.GetNodeRelativeParentPath(mGameObejct1, mGameObejct2);
                }
            }
            if (GUILayout.Button("获取两对象间距离", GUILayout.Width(150), GUILayout.Height(20)))
            {
                mParamConfigIntroduction = "获取两个对象之间的距离说明:\n 对象1: 拖入判定节点1 对象2: 拖入判定节点2";
                if (mGameObejct1 == null || mGameObejct2 == null)
                {
                    Log.Info("对象信息不能赋空!");
                }
                else
                {
                    float distance = Vector3.Distance(mGameObejct1.transform.position, mGameObejct2.transform.position);
                    string distanceInfo = "两个对象之间的距离 " + distance.ToString();
                    Log.Info(distanceInfo);
                    EditorGUIUtility.systemCopyBuffer = distance.ToString();
                }
            }
            EditorGUILayout.EndHorizontal();
            #endregion
        }

        /// <summary>
        /// 显示选择模块功能区
        /// </summary>
        private void DisplayFunctionArea()
        {
            EditorGUILayout.BeginHorizontal();
            EditorUtilities.DisplayDIYGUILable("功能模块选择:", Color.yellow, 10.0f, 120, 20);
            mCurrentSelectedModuleIndex = EditorGUILayout.Popup(mCurrentSelectedModuleIndex, mModuleOptions, GUILayout.Width(150), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorUtilities.DisplayDIYGUILable(string.Format("{0}功能区:", mModuleOptions[mCurrentSelectedModuleIndex]), Color.yellow, 10.0f, 200.0f, 20);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            switch ((EModuleType)mCurrentSelectedModuleIndex)
            {
                case EModuleType.LuaSystem:
                    DisplayLuaSystem();
                    break;
                case EModuleType.CommonSystem:
                    DisplayCommonSystem();
                    break;
                case EModuleType.BehaviourTreeSystem:
                    DisplayBehaviourTreeSystem();
                    break;
                case EModuleType.GuideSystem:
                    DisplayGuideSystem();
                    break;
                case EModuleType.UIAdaptation:
                    DisplayUIAdaptation();
                    break;
            }
            EditorGUILayout.EndVertical();
        }

        #region Lua Module
        /// <summary>
        /// 显示Lua模块功能
        /// </summary>
        private void DisplayLuaSystem()
        {
            if (Application.isPlaying)
            {
                // 运行时才提供的功能
                #region RunTimeFunction
                DisplayTitle("运行时功能区:");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("热重载选定Lua脚本", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "热重载选定Lua脚本说明:\n 选中Lua脚本Asset";
                    var selectedasset = Selection.activeObject;
                    if (selectedasset != null)
                    {
                        var selectedassetpath = AssetDatabase.GetAssetPath(selectedasset);
                        Debug.Log($"选中Asset路径:{selectedassetpath}");
                        var luarelativefolder = "Assets/Res/LuaScripts/";
                        if (selectedassetpath.StartsWith(luarelativefolder))
                        {
                            if (selectedassetpath.EndsWith(".lua"))
                            {
                                var luafilename = Path.GetFileNameWithoutExtension(selectedassetpath);
                                //var relativeluafilepath = selectedassetpath.Replace(luarelativefolder, string.Empty);
                                //relativeluafilepath = relativeluafilepath.Remove(relativeluafilepath.IndexOf(".lua"), 4);
                                //Debug.Log($"Lua文件相对路径:{relativeluafilepath}");
                                //var luafilepackagename = relativeluafilepath.Replace('/', '.');
                                //Debug.Log($"Lua相对require包名:{luafilepackagename}");
                                //Debug.Log($"Lua文件名:{luafilename}");
                                EditorGUIUtility.systemCopyBuffer = luafilename;
                                XLuaManager.Instance.ReLoadLuaFileCaching(selectedassetpath);
                                if (XLuaHelper.HotLoadLuaFile(luafilename))
                                {
                                    Debug.Log($"热重载Lua文件:{luafilename}成功!");
                                }
                                else
                                {
                                    Debug.Log($"热重载Lua文件:{luafilename}失败!");
                                }
                            }
                            else
                            {
                                Debug.LogError("选中的Asset并非Lua脚本!");
                            }
                        }
                        else
                        {
                            Debug.LogError("请选中Res/LuaScripts/目录下的Lua脚本!");
                        }
                    }
                    else
                    {
                        Debug.LogError("未选中有效Asset对象!");
                    }
                }
                if (GUILayout.Button("执行指定Lua代码", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "执行指定Lua代码说明:\n 参数1: 写入Lua代码";
                    var luacode = mParam1;
                    //Debug.Log($"Lua代码:{luacode}");
                    if (XLuaHelper.DoLuaCode(luacode))
                    {
                        Debug.Log($"执行Lua代码:{luacode}成功!");
                    }
                    else
                    {
                        Debug.Log($"执行Lua代码:{luacode}失败!");
                    }
                }
                EditorGUILayout.EndHorizontal();
                #endregion
            }
            else
            {
                // 非运行时才提供的功能
                DisplayTitle("非运行时功能区:");
            }
        }
        #endregion

        #region Log Module
        private void DisplayLogSystem()
        {
            DisplayTitle("运行时功能区:");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("UI Log开关", GUILayout.Width(150), GUILayout.Height(20)))
            {
                TryGetLuaGM();
                Debug.Log($"切換UIModule Log开关");
                mGM.ChangeSwitchModuleLog("UIModule");
            }
            if (GUILayout.Button("通用模块开关", GUILayout.Width(150), GUILayout.Height(20)))
            {
                TryGetLuaGM();
                Debug.Log($"切通用模块开关");
                mGM.ChangeSwitchModuleLog("CommonModule");
            }
            if (GUILayout.Button("登录模块开关", GUILayout.Width(150), GUILayout.Height(20)))
            {
                TryGetLuaGM();
                Debug.Log($"切登录模块开关");
                mGM.ChangeSwitchModuleLog("LoginModule");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("网络模块开关", GUILayout.Width(150), GUILayout.Height(20)))
            {
                TryGetLuaGM();
                Debug.Log($"切网络模块开关");
                mGM.ChangeSwitchModuleLog("NetModule");
            }
            if (GUILayout.Button("AI行为树模块开关", GUILayout.Width(150), GUILayout.Height(20)))
            {
                TryGetLuaGM();
                Debug.Log($"切AI行为树模块开关");
                mGM.ChangeSwitchModuleLog("BehaviourTreeModule");
            }
            if (GUILayout.Button($"战斗模块Log开关", GUILayout.Width(150), GUILayout.Height(20)))
            {
                TryGetLuaGM();
                Debug.Log($"切战斗模块Log开关");
                mGM.ChangeSwitchModuleLog("BattleModule");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
        }
        #endregion

        #region 公用Model
        /// <summary>
        /// 显示公用模块
        /// </summary>
        private void DisplayCommonSystem()
        {
            if (Application.isPlaying)
            {
                // 运行时才提供的功能
                #region RunTimeFunction
                DisplayTitle("运行时功能区:");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                EditorGUILayout.EndHorizontal();
                #endregion
            }
            else
            {
                // 非运行时才提供的功能
                DisplayTitle("非运行时功能区:");
            }
        }
        #endregion

        #region 行为树Model
        /// <summary>
        /// 行为树模块
        /// </summary>
        private void DisplayBehaviourTreeSystem()
        {
            if (Application.isPlaying)
            {
                // 运行时才提供的功能
                #region RunTimeFunction
                DisplayRuntimeAreaTitle();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("添加指定AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "添加指定AI数据说明:\n 参数1:对象UID 参数2:行为树资源路径";
                    if (long.TryParse(mParam1, out long uid) && !string.IsNullOrEmpty(mParam2))
                    {
                        TryGetLuaGM();
                        mGM.AddAIToWorldObject(uid, mParam2);
                    }
                    else
                    {
                        Debug.LogError($"无效参数:{mParam1},{mParam2}");
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("移除指定AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "移除指定AI数据说明:\n 参数1:对象UID";
                    if (long.TryParse(mParam1, out long uid))
                    {
                        TryGetLuaGM();
                        mGM.RemoveWorldObjectAI(uid);
                    }
                    else
                    {
                        Debug.LogError($"无效参数:{mParam1}");
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("暂停指定AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "添加指定AI数据说明:\n 参数1:对象UID";
                    if (long.TryParse(mParam1, out long uid))
                    {
                        TryGetLuaGM();
                        mGM.PauseWorldObjectAI(uid);
                    }
                    else
                    {
                        Debug.LogError($"无效参数:{mParam1}");
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("继续指定AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "添加指定AI数据说明:\n 参数1:对象UID";
                    if (long.TryParse(mParam1, out long uid))
                    {
                        TryGetLuaGM();
                        mGM.ResumeWorldObjectAI(uid);
                    }
                    else
                    {
                        Debug.LogError($"无效参数:{mParam1}");
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("暂停所有AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "暂停所有AI数据说明:\n 参数1:无";
                    TryGetLuaGM();
                    mGM.PauseAll();
                }
                GUILayout.Space(10);
                if (GUILayout.Button("继续所有AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "继续所有AI数据说明:\n 参数1:无";
                    TryGetLuaGM();
                    mGM.ResumeAll();
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("中断并暂停所有AI", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "中断并暂停所有AI数据说明:\n 参数1:无";
                    TryGetLuaGM();
                    mGM.AbortAndPauseAll();
                }
                GUILayout.Space(10);
                if (GUILayout.Button("选中节点UID对象", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "选中节点UID对象数据说明:\n 参数1:节点UID";
                    TryGetLuaGM();
                    if (int.TryParse(mParam1, out int nodeuid))
                    {
                        TryGetLuaGM();
                        SelectNodeUIDGameObject(nodeuid);
                    }
                    else
                    {
                        Debug.LogError($"无效参数:{mParam1}");
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("打印节点绑定信息", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "打印节点绑定信息数据说明:\n 参数1:无";
                    TryGetLuaGM();
                    mGM.PrintAllBindLuaBTNodeInfo();
                }
                EditorGUILayout.EndHorizontal();
                #endregion
            }
            else
            {
                // 非运行时才提供的功能
                DisplayEditorAreaTitle();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("重新保存所有行为树", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    mParamConfigIntroduction = "重新保存所有行为树文件数据说明:\n 参数1:无";
                    ResaveAllBehaviourTreeFiles();
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// 选中指定节点UDI的绑定对象
        /// </summary>
        private bool SelectNodeUIDGameObject(int nodeuid)
        {
            var allbehaviourtrees = GameObject.FindObjectsOfType<TBehaviourTree>();
            foreach (var behaviourtree in allbehaviourtrees)
            {
                if (behaviourtree.BTRunningGraph != null)
                {
                    var validebtnode = behaviourtree.BTRunningGraph.AllNodesList.Find((btnode) =>
                    {
                        return btnode.UID == nodeuid;
                    });
                    if (validebtnode != null)
                    {
                        Debug.Log($"节点UID绑定世界对象名:{behaviourtree.gameObject.name}");
                        Selection.activeObject = behaviourtree.gameObject;
                        return true;
                    }
                }
            }
            Debug.LogError($"找不到节点UID:{nodeuid}的绑定世界对象!");
            return false;
        }

        /// <summary>
        /// 重新保存所有行为树文件
        /// </summary>
        private void ResaveAllBehaviourTreeFiles()
        {
            BTNodeEditor btnodeeditor = BTNodeEditor.ShowEditor();
            var allbehaviourtreassets = Resources.LoadAll<BTGraph>($"{BTData.BTNodeSaveFolderRelativePath}");
            Debug.Log($"行为树文件数量:{allbehaviourtreassets.Length}");
            for (int i = 0, length = allbehaviourtreassets.Length; i < length; i++)
            {
                EditorUtility.DisplayProgressBar("行为树文件保存", $"当前保存行为树文件名:{allbehaviourtreassets[i].name}", (float)i / length);
                btnodeeditor.LoadBTAsset(allbehaviourtreassets[i]);
                btnodeeditor.TrySaveBTAsset();
            }
            EditorUtility.ClearProgressBar();
            this.Focus();
        }
        #endregion

        #region 适配

        private void DisplayUIAdaptation()
        {
            //if (Application.isPlaying)
            {
                // 运行时才提供的功能
                DisplayRuntimeAreaTitle();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUI.color = Launcher.Instance.TestNotchScreen ? Color.green : Color.white;
                if (GUILayout.Button("刘海开关", GUILayout.Width(160), GUILayout.Height(20)))
                {
                    Launcher.Instance.TestNotchScreen = !Launcher.Instance.TestNotchScreen;
                    if (UIModel.Inst)
                        UIModel.Inst.Init();
                }
                GUI.color = Color.white;
                if (GUILayout.Button("应用适配", GUILayout.Width(160), GUILayout.Height(20)))
                {
                    if (UIModel.Inst)
                        UIModel.Inst.Init();
                }

                EditorGUILayout.EndHorizontal();

                InitUIAdaptation();
                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                for (int i = 0; i < gameViewProfilesCount; i++)
                {
                    int index = i;
                    string display = displayTexts[i];
                    if (GUILayout.Button(display, GUILayout.Width(160), GUILayout.Height(20)))
                    {
                        SetSize(index);
                        if (UIModel.Inst)
                            UIModel.Inst.Init();
                    }
                    if ((i + 1) % 2 == 0 || i + 1 == gameViewProfilesCount)
                    {
                        EditorGUILayout.EndHorizontal();
                        if (i + 1 < gameViewProfilesCount)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                        }
                    }
                }
            }
            //else
            //{
            //    // 非运行时才提供的功能
            //    DisplayEditorAreaTitle();
            //}
        }

        private object gameViewSizesInstance;
        private MethodInfo gameViewGetGroup;
        private int gameViewProfilesCount;
        private string[] displayTexts;

        private void InitUIAdaptation()
        {
            if (gameViewSizesInstance == null)
            {
                var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
                var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
                var instanceProp = singleType.GetProperty("instance");
                gameViewGetGroup = sizesType.GetMethod("GetGroup");
                gameViewSizesInstance = instanceProp.GetValue(null, null);
                GameViewSizeGroupType gameViewSizeGroupType = GameViewSizeGroupType.Android;
#if UNITY_ANDROID
                gameViewSizeGroupType = GameViewSizeGroupType.Android;
#elif UNITY_IPHONE
                gameViewSizeGroupType = GameViewSizeGroupType.iOS;
#elif UNITY_STANDALONE_WIN
                gameViewSizeGroupType = GameViewSizeGroupType.Standalone;
#endif
                var group = GetGroup(gameViewSizeGroupType);
                var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
                displayTexts = getDisplayTexts.Invoke(group, null) as string[];
                gameViewProfilesCount = displayTexts.Length;

                for (int i = 0; i < gameViewProfilesCount; i++)
                {
                    int index = i;
                    string display = displayTexts[i];
                    int pren = display.IndexOf('(');
                    if (pren != -1)
                        display = display.Substring(0, pren - 1); // -1 to remove the space that's before the prens. This is very implementation-depdenent
                    displayTexts[i] = display;
                }
            }
        }

        private void SetSize(int index)
        {
            var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var gvWnd = EditorWindow.GetWindow(gvWndType);
            var SizeSelectionCallback = gvWndType.GetMethod("SizeSelectionCallback",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            SizeSelectionCallback.Invoke(gvWnd, new object[] { index, null });
        }

        private object GetGroup(GameViewSizeGroupType type)
        {
            return gameViewGetGroup.Invoke(gameViewSizesInstance, new object[] { (int)type });
        }

        #endregion

        #region 新手引导
        /// <summary>
        /// 缓存的新手引导
        /// </summary>
        private Transform m_guideTrans;
        /// <summary>
        /// 缓存的新手引导遮罩组件
        /// </summary>
        private GuideMask m_guideMask;
        private void DisplayGuideSystem()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("开始新手引导编辑", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null == m_guideTrans)
                    {
                        AssetLoadManager.Instance.LoadPrefabInstance("UI/Guide/GuideView", (_obj) =>
                        {
                            Transform normalUIRoot = UIModel.Inst.NormalUIRoot;
                            m_guideTrans = _obj.transform;
                            m_guideTrans.SetParent(normalUIRoot);
                            m_guideTrans.SetLocalScaleToOne();
                            m_guideTrans.SetLocalPositionToZero();
                            int lastViewSortingOrder = normalUIRoot.GetChild(normalUIRoot.childCount - 2).GetComponent<Canvas>().sortingOrder;
                            m_guideTrans.SetCanvasSortingOrder(lastViewSortingOrder + 10);
                            m_guideMask = m_guideTrans.GetComponentInChildren<GuideMask>();
                            m_guideMask.IsGuideEditorModel = true;
                            m_guideMask.SetGuideTarget(Selection.activeGameObject);
                        });
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("结束新手引导编辑", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null != m_guideTrans)
                    {
                        m_guideTrans.DestroyGameObj();
                        m_guideTrans = null;
                        m_guideMask = null;
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("显示新手引导", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null != m_guideTrans)
                        m_guideTrans.gameObject.SetActive(true);
                }
                GUILayout.Space(10);
                if (GUILayout.Button("隐藏新手引导", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null != m_guideTrans)
                        m_guideTrans.gameObject.SetActive(false);
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(10);
                if (GUILayout.Button("选中新手引导遮罩", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null != m_guideTrans)
                        Selection.activeObject = m_guideMask.gameObject;
                }
                GUILayout.Space(10);
                if (GUILayout.Button("选中新手引导手势", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null != m_guideTrans)
                        Selection.activeObject = m_guideMask.GuideUI.guideGesture.gameObject;
                }
                GUILayout.Space(10);
                if (GUILayout.Button("选中新手引导说明", GUILayout.Width(150), GUILayout.Height(20)))
                {
                    if (null != m_guideTrans)
                        Selection.activeObject = m_guideMask.GuideUI.guideExplain.gameObject;
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                DisplayEditorAreaTitle();
            }
        }
        #endregion

        /// <summary>
        /// Lua层的GM
        /// </summary>
        private GM mGM;

        /// <summary>
        /// 尝试获取Lua的GM工具类 
        /// </summary>
        private void TryGetLuaGM()
        {
            if (mGM != null)
            {
                return;
            }
            else
            {
                mGM = XLuaManager.Instance.GetLuaEnv().Global.Get<GM>("GM");
            }
        }

        private void DisplayTitle(string title)
        {
            EditorGUILayout.BeginHorizontal();
            EditorUtilities.DisplayDIYGUILable(title, Color.yellow, 10.0f, 120, 20);
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 显示战斗中功能区标题
        /// </summary>
        private void DisplayFightingAreaTitle()
        {
            EditorGUILayout.BeginHorizontal();
            EditorUtilities.DisplayDIYGUILable("战斗中功能区:", Color.yellow, 10.0f, 120, 20);
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 显示运行时功能区标题
        /// </summary>
        private void DisplayRuntimeAreaTitle()
        {
            EditorGUILayout.BeginHorizontal();
            EditorUtilities.DisplayDIYGUILable("运行时功能区:", Color.yellow, 10.0f, 120, 20);
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 显示非运行时功能区标题
        /// </summary>
        private void DisplayEditorAreaTitle()
        {
            EditorGUILayout.BeginHorizontal();
            EditorUtilities.DisplayDIYGUILable("非运行时功能区:", Color.yellow, 10.0f, 120, 20);
            EditorGUILayout.EndHorizontal();
        }
    }
}
