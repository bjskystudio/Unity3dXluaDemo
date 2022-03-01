//using System;
//using System.Reflection;
//using System.Text.RegularExpressions;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Shader))]
//public class ShaderDetailInspector : Editor
//{
//    public static GUIStyle CloneGUIStyle(GUIStyle obj)
//    {
//        GUIStyle s = new GUIStyle(obj);
//        s.name = obj.name + "_Clone";
//        return s;
//    }

//    private static GUIStyle _descLabelStyle;
//    protected static GUIStyle DescLabelStyle
//    {
//        get
//        {
//            if (_descLabelStyle == null)
//            {
//                GUIStyle s = new GUIStyle(GUI.skin.GetStyle("Label"));
//                s.name = GUI.skin.GetStyle("Label").name + "_Clone";
//                _descLabelStyle = s;
//                _descLabelStyle.fontSize = 16;
//                _descLabelStyle.wordWrap = true;
//            }
//            return _descLabelStyle;
//        }
//    }

//    private UnityEditor.Editor nativeEditor;
//    private static Regex ShaderInfoRge = new Regex("//@@@DynamicShaderInfoStart\n(.*)\n//@@@DynamicShaderInfoEnd\n");
//    protected string _shaderPath;
//    protected Shader _shaderCahce;
//    protected string _shaderCahceCode;
//    protected bool _shaderDescReadonly;
//    protected string _shderDescription;
//    protected string _shderDescriptionCache;

//    public void OnEnable()
//    {
//        Type t = null;

//        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
//        {
//            foreach (Type type in assembly.GetTypes())
//            {
//                if (type.Name.ToLower().Contains("shaderinspector") && !type.Name.ToLower().Contains("computeshaderinspector"))//不是很保险
//                {
//                    Debug.LogWarning("shader" + type.Name);
//                    t = type;
//                    break;
//                }
//            }
//        }

//        nativeEditor = UnityEditor.Editor.CreateEditor(serializedObject.targetObject, t);
//        _shadercacheInit(target as Shader);
//    }

//    private void _shadercacheInit(Shader shader)
//    {
//        if (!shader) return;
//        _shaderCahce = shader;
//        //获取Shader文本数据
//        _shaderPath = AssetDatabase.GetAssetPath(_shaderCahce);

//        //过滤内置Shader和无法找到其路径的shader
//        if (string.IsNullOrEmpty(_shaderPath)
//            || _shaderPath == "Resources/unity_builtin_extra"
//            || _shaderPath == "Library/unity default resources"
//        ) return;

//        string path = Application.dataPath.Replace("Assets", "") + _shaderPath;
//        _shaderCahceCode = AorIO.ReadStringFormFile(path);

//        //统一结束符 (\r\n -> \n)
//        _shaderCahceCode = _shaderCahceCode.Replace("\r\n", "\n");

//        //获取描述
//        Match InfoMatch = ShaderInfoRge.Match(_shaderCahceCode);
//        if (InfoMatch.Success)
//        {
//            string inner = InfoMatch.Groups[1].Value;
//            _shaderDescReadonly = inner.ToLower().StartsWith("//<readonly>");
//            _shderDescription = _shaderDescReadonly ? inner.Substring(12) : inner.Substring(2);

//            //防止获取的描述中包含\n 或者 \r\n
//            _shderDescription = _shderDescription.Replace("\n", "");

//            _shderDescriptionCache = _shderDescription;
//        }
//    }

//    public override void OnInspectorGUI()
//    {
//        if (nativeEditor != null)
//        {
//            nativeEditor.OnInspectorGUI();
//            GUILayout.Space(50);

//            //描述 (暂时不打算加入新建描述的功能,如果shader确实需要添加描述,请手动为shader添加描述识别字段)
//            if (!string.IsNullOrEmpty(_shderDescriptionCache))
//            {
//                GUILayout.BeginVertical("box");
//                GUILayout.Space(8);
//                GUILayout.Label("Shader Description : ");
//                GUILayout.Space(5);

//                if (_shaderDescReadonly)
//                {
//                    GUILayout.Label(_shderDescriptionCache, DescLabelStyle);
//                }
//                else
//                {

//                    _shderDescriptionCache = EditorGUILayout.TextArea(_shderDescriptionCache, DescLabelStyle);
//                    GUILayout.BeginHorizontal();
//                    GUILayout.FlexibleSpace();
//                    if (_shderDescriptionCache != _shderDescription)
//                    {
//                        if (GUILayout.Button("SaveChanged", GUILayout.Width(Screen.width * 0.3f)))
//                        {
//                            //防止新输入描述中包含\n 或者 \r\n
//                            _shderDescriptionCache = _shderDescriptionCache.Replace("\r\n", "");
//                            _shderDescriptionCache = _shderDescriptionCache.Replace("\n", "");
//                            //重写Shader文件
//                            _ReBuildShaderCode(() =>
//                            {
//                                //重写成功后
//                                _shderDescription = _shderDescriptionCache;
//                            });
//                        }
//                    }
//                    GUILayout.EndHorizontal();
//                }

//                GUILayout.EndVertical();

//                GUILayout.Space(10);
//            }

//        }
//        //SceneView.RepaintAll();
//    }

//    protected void _ReBuildShaderCode(Action sucessDo)
//    {
//        Match infoMatch = ShaderInfoRge.Match(_shaderCahceCode);
//        if (infoMatch.Success)
//        {
//            string rp = infoMatch.Value;
//            rp = rp.Replace(infoMatch.Groups[1].Value, _shaderDescReadonly ? ("//<Readonly>" + _shderDescriptionCache) : ("//" + _shderDescriptionCache) + "\n");
//            _shaderCahceCode = _shaderCahceCode.Replace(infoMatch.Value, rp);

//            string path = Application.dataPath.Replace("Assets", "") + _shaderPath;
//            if (AorIO.SaveStringToFile(path, _shaderCahceCode))
//            {
//                EditorUtility.SetDirty(_shaderCahce);
//                AssetDatabase.SaveAssets();
//                AssetDatabase.Refresh();
//                sucessDo();
//            }
//        }
//    }
//}
