using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using YoukiaCore.Utils;

public class SpriteSelectTool : YKEditorWindow
{
    #region 属性变量定义
    /// <summary>
    /// 选中回调
    /// </summary>
    public Action<Sprite> SelectCallback;
    /// <summary>
    /// 选中的图集文件夹名字
    /// </summary>
    public string SelectDirName { get; set; } = "Common";

    /// <summary>
    /// 选中的图集文件夹路径
    /// </summary>
    private string selectDirPath = "";
    /// <summary>
    /// 所有图集文件夹
    /// </summary>
    private DirectoryInfo[] allAtlasDics;
    /// <summary>
    ///  所有图集文件夹路径
    /// </summary>
    private string[] allAtlasDicsPath;
    /// <summary>
    ///  所有图集文件夹名称
    /// </summary>
    private string[] allAtlasDicsName;
    /// <summary>
    ///选中文件夹中所有Sprite
    /// </summary>
    private List<Sprite> allSprites;
    /// <summary>
    /// 当前选中Sprite
    /// </summary>
    private Sprite curSelectedSprite;
    /// <summary>
    /// 搜索名字
    /// </summary>
    private string searchName;
    /// <summary>
    /// 左侧偏移
    /// </summary>
    private readonly int left = 130;
    /// <summary>
    /// 顶部偏移
    /// </summary>
    private readonly int top = 20;
    /// <summary>
    /// 显示图标大小
    /// </summary>
    private readonly int size = 70;
    /// <summary>
    /// 图标横向间隔
    /// </summary>
    private readonly int spaceX = 10;
    /// <summary>
    /// 图标竖向间隔
    /// </summary>
    private readonly int spaceY = 20;
    //Temp
    private int cols, offset;
    private string[] tempGuids;
    private Vector2 rightScrollPos, leftScrollPos;
    #endregion

    public static SpriteSelectTool Open(Sprite sprite = null)
    {
        var rt = AddWindow();
        rt.curSelectedSprite = sprite;
        if (sprite != null)
        {
            string initDir = YKEditorUtils.GetAssetFloderName(sprite);
            if (initDir != "Resources")//可能是使用了原生贴图
            {
                rt.SelectDirName = YKEditorUtils.GetAssetFloderName(sprite);
            }
        }
        return rt;
    }

    private static SpriteSelectTool AddWindow()
    {
        SpriteSelectTool window = GetWindow<SpriteSelectTool>(true, "Sprite选择器");
        window.position = new Rect(100, 50, 1280, 900);
        window.minSize = new Vector2(800, 600);
        return window;
    }

    protected override void Awake()
    {
        base.Awake();
        allSprites = new List<Sprite>();
        InitDirctorys();
    }

    private void InitDirctorys()
    {
        allAtlasDics = FileUtils.GetDirs(SpriteAtlasTool.AtlasSpritePath);
        allAtlasDicsPath = new string[allAtlasDics.Length];
        allAtlasDicsName = new string[allAtlasDics.Length];
        for (int i = 0; i < allAtlasDics.Length; i++)
        {
            allAtlasDicsPath[i] = allAtlasDics[i].FullName.Replace("\\", "/").Replace(Application.dataPath, "Assets");
            allAtlasDicsName[i] = allAtlasDics[i].Name;
        }
    }

    protected override void OnDrawMenus()
    {
        if (curSelectedSprite != null)
        {
            if (GUILayout.Button("查找引用", "toolbarbutton", GUILayout.Width(80)))
            {
                EditorUtility.DisplayDialog("警告", "尚未开发", "OK", "");
            }
            if (GUILayout.Button("编辑", "toolbarbutton", GUILayout.Width(80)))
            {
                EditorSprite(curSelectedSprite);
            }

            EditorGUILayout.Space();
            GUILayout.Label(curSelectedSprite.name, "AssetLabel Partial");
        }
        EditorGUILayout.Space();
        searchName = GUILayout.TextField(searchName, (GUIStyle)"SearchTextField", GUILayout.Width(200));
        if (GUILayout.Button("", "SearchCancelButton"))
        {
            searchName = "";
        }
    }

    protected override void OnDrawContent()
    {
        EditorGUILayout.BeginHorizontal();
        //左侧绘制
        leftScrollPos = EditorGUILayout.BeginScrollView(leftScrollPos, GUILayout.Width(125));
        GUILayout.Space(20);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(100));
        selectDirPath = null;
        if (allAtlasDicsPath != null)
        {
            for (int i = 0; i < allAtlasDicsPath.Length; i++)
            {
                if (SelectDirName == allAtlasDicsName[i])
                {
                    selectDirPath = allAtlasDicsPath[i];
                    GUI.color = Color.green;
                }
                if (GUILayout.Button(allAtlasDicsName[i]))
                {
                    SelectDirName = allAtlasDicsName[i];
                    selectDirPath = allAtlasDicsPath[i];
                }
                GUI.color = Color.white;
            }
            if (!string.IsNullOrEmpty(selectDirPath))
            {
                allSprites.Clear();
                tempGuids = AssetDatabase.FindAssets("t:Texture", new string[] { selectDirPath });
                for (int i = 0; i < tempGuids.Length; i++)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(tempGuids[i]);
                    var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                    if (sprite != null)
                        allSprites.Add(sprite);
                    //后续可以用以下加载方式继续操作
                    //var textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                }
            }
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

        //EditorGUILayout.BeginVertical();

        DrawImages();

        //EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
        EditorUtility.ClearProgressBar();
    }

    private void DrawImages()
    {
        cols = (Screen.width - left) / (size + spaceX);
        rightScrollPos = GUI.BeginScrollView(new Rect(left, top, Screen.width - left, Screen.height - top * 2), rightScrollPos,
            new Rect(-spaceX, top - spaceY, Screen.width - 20 - left, Mathf.CeilToInt(allSprites.Count / (float)cols) * (spaceY + size) + spaceY));

        offset = 0;
        for (int i = 0; i < allSprites.Count; i++)
        {
            var sprite = allSprites[i];
            if (!string.IsNullOrEmpty(searchName) && !sprite.name.ToLower().Contains(searchName.ToLower()))
            {
                offset++;
                continue;
            }
            int ii = i - offset;

            var rect = new Rect((ii % cols) * (size + spaceX), (ii / cols) * (size + spaceY) + top, size, size);
            string style = curSelectedSprite == sprite ? "LightmapEditorSelectedHighlight" : "box";
            if (YKEditorUtils.SpriteButton(rect, allSprites[i], style, GUILayout.Width(50), GUILayout.Height(50)))
            {
                if (curSelectedSprite == sprite)
                {
                    if (SelectCallback != null)
                    {
                        SelectCallback.Invoke(sprite);
                        SelectCallback = null;
                        Close();
                    }
                }
                else
                {
                    curSelectedSprite = sprite;
                }
            }

            if (curSelectedSprite == sprite)
            {
                rect.y += size;
                GUI.Label(rect, sprite.name, "sv_label_3");
            }
            else
            {
                rect.y += size / 1.7f;
                GUI.Label(rect, sprite.name, "MiniLabel");
            }
        }
        GUI.EndScrollView();
    }

    /// <summary>
    /// 编辑Sprite
    /// </summary>
    /// <param name="sprite">Sprite对象</param>
    /// <returns>编辑后的Sprite</returns>
    public static void EditorSprite(Sprite sprite)
    {
        //临时屏蔽错误by xin.liu
//#if UNITY_2019
//        UnityEngine.Object tempOld = Selection.activeObject;
//        Selection.activeObject = sprite;
//        var uniSpriteEditorType = typeof(UnityEditor.U2D.Sprites.ISpriteEditor).Assembly.GetType("UnityEditor.U2D.Sprites.SpriteEditorWindow");
//        var uniSpriteEditor = GetWindow(uniSpriteEditorType);
//        var temp = uniSpriteEditorType.GetMethod("TrySelect", BindingFlags.NonPublic | BindingFlags.Instance)
//            .Invoke(uniSpriteEditor, new object[] { sprite.rect.position });
//        uniSpriteEditorType.GetProperty("selectedSpriteRect").SetValue(uniSpriteEditor, temp, null);
//        Selection.activeObject = tempOld;
//#else
//        UnityEngine.Object tempOld = Selection.activeObject;
//        Selection.activeObject = sprite;
//        var uniSpriteEditorType = typeof(Editor).Assembly.GetType("UnityEditor.SpriteEditorWindow");
//        var uniSpriteEditor = GetWindow(uniSpriteEditorType);
//        var temp = uniSpriteEditorType.GetMethod("TrySelect", BindingFlags.NonPublic | BindingFlags.Instance)
//            .Invoke(uniSpriteEditor, new object[] { sprite.rect.position });
//        uniSpriteEditorType.GetProperty("selectedSpriteRect").SetValue(uniSpriteEditor, temp, null);
//        Selection.activeObject = tempOld;
//#endif
    }
}
