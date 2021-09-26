using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AddAorComponentEditor : Editor
{
    private const string UILayerKey = "UI";

    #region Add AorComponent
    private static T AddAorComponent<T>(GameObject target) where T : Component
    {
        GameObject newObj = new GameObject();
        if (target)
            newObj.transform.SetParent(target.transform);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localScale = Vector3.one;

        T com = newObj.AddComponent<T>();

        if (com.GetComponent<MaskableGraphic>() != null)
        {
            com.GetComponent<MaskableGraphic>().raycastTarget = false;
        }

        if (com.GetComponent<Graphic>() != null)
        {
            com.GetComponent<Graphic>().color = Color.white;
        }

        Selection.activeTransform = com.transform;
        return com;
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorImage", priority = 10)]
    private static void AddAorImage(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        AddAorComponent<AorImage>(go).name = "image";
        go.transform.SetLayer(UILayerKey);
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorRawImage", priority = 11)]
    private static void AddAorRawImage(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        AddAorComponent<AorRawImage>(go).name = "rawImage";
        go.transform.SetLayer(UILayerKey);
    }

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/AorTMP", priority = 22)]
    // private static void AddAorTMP(MenuCommand command)
    // {
    //     GameObject go = command.context as GameObject;
    //     var tmp = AddAorComponent<AorTMP>(go);
    //     tmp.name = "@_aortmp_";
    //     //tmp.font = Resources.Load<TMP_FontAsset>("Fonts/MainFont/Main SDF");
    //     TMP_FontAsset tmpFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Res/Fonts/MainFont/Main SDF.asset");
    //     AddAorComponentEditor.DoAorTmpDefultSetting(tmp, tmpFont);
    // }

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/AorTMP-3D", priority = 21)]
    // private static void AddAorTMP3D(MenuCommand command)
    // {
    //     GameObject go = command.context as GameObject;
    //     var tmp = AddAorComponent<AorTMP3D>(go);
    //     tmp.name = "@_aortmp3d_";
    //     //tmp.font = Resources.Load<TMP_FontAsset>("Fonts/MainFont/Main SDF");
    //     //tmp.alignment = TextAlignmentOptions.Center;
    //     TMP_FontAsset tmpFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Res/Fonts/MainFont/Main SDF.asset");
    //     AddAorComponentEditor.DoAorTmpDefultSetting(tmp, tmpFont);
    //     tmp.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 5);
    // }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorText", priority = 20)]
    private static void AddAorText(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        var aorText = AddAorComponent<AorText>(go);
        aorText.name = "text";
        Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Res/Fonts/Main.ttf");
        aorText.font = font;
        aorText.fontSize = 22;
        aorText.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
        aorText.alignment = TextAnchor.MiddleCenter;
        go.transform.SetLayer(UILayerKey);
    }
    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorGraphicText", priority = 20)]
    private static void AddAorGraphicText(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        var aorText = AddAorComponent<AorGraphicText>(go);
        aorText.name = "text";
        Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Res/Fonts/Main.ttf");
        aorText.font = font;
        aorText.fontSize = 22;
        aorText.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
        aorText.alignment = TextAnchor.MiddleCenter;
        go.transform.SetLayer(UILayerKey);
    }
    // [UnityEditor.MenuItem("GameObject/添加Aor组件/AorText转AorTmp", priority = 100)]
    // public static void AortextToTmp()
    // {
    //     TMP_FontAsset tmpFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Res/Fonts/MainFont/Main SDF.asset");
    //     Transform trans = Selection.activeTransform;
    //     AorText[] all = trans.gameObject.GetComponentsInChildren<AorText>(true);
    //     foreach (var o in all)
    //     {
    //         string now = o.text;
    //         GameObject obj = o.gameObject;
    //         string a = obj.name.Replace("aortext", "aortmp");
    //         obj.name = a;
    //         Outline ol = obj.GetComponent<Outline>();
    //         if (null != ol)
    //         {
    //             DestroyImmediate(ol);
    //         }
    //         DestroyImmediate(o);
    //         AorTMP tmp = obj.AddComponent<AorTMP>();
    //         AddAorComponentEditor.DoAorTmpDefultSetting(tmp, tmpFont);
    //         tmp.text = now;
    //     }
    // }

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/Text转AorTmp", priority = 100)]
    // public static void TextToTmp()
    // {
    //     TMP_FontAsset tmpFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Res/Fonts/MainFont/Main SDF.asset");
    //     Transform trans = Selection.activeTransform;
    //     Text[] all = trans.gameObject.GetComponentsInChildren<Text>(true);
    //     foreach (var o in all)
    //     {
    //         string now = o.text;
    //         GameObject obj = o.gameObject;
    //         Outline ol = obj.GetComponent<Outline>();
    //         if (null != ol)
    //         {
    //             DestroyImmediate(ol);
    //         }
    //         DestroyImmediate(o);
    //         AorTMP tmp = obj.AddComponent<AorTMP>();
    //         AddAorComponentEditor.DoAorTmpDefultSetting(tmp, tmpFont);
    //         tmp.text = now;
    //     }
    // }

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/AorTmp默认设置", priority = 100)]
    // public static void AorTmpDefultSetting()
    // {
    //     TMP_FontAsset tmpFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Res/Fonts/MainFont/Main SDF.asset");
    //     Transform trans = Selection.activeTransform;
    //     AorTMP[] all = trans.gameObject.GetComponentsInChildren<AorTMP>(true);
    //     foreach (var tmp in all)
    //     {
    //         AddAorComponentEditor.DoAorTmpDefultSetting(tmp, tmpFont);
    //     }
    // }

    //aortmp的默认设置走这
    private static void DoAorTmpDefultSetting(AorTMP tmp, TMP_FontAsset tmpFont)
    {
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.font = tmpFont;
    }

    //aortmp3d的默认设置走这
    private static void DoAorTmpDefultSetting(AorTMP3D tmp, TMP_FontAsset tmpFont)
    {
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.font = tmpFont;
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/输入框", priority = 25)]
    private static void AddAorInput(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/@_aorinputfield_.prefab");
        if (orgin != null)
        {
            GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
            target.name = "@_aorinputfield_";
            target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            target.transform.localScale = Vector3.one;
            Selection.activeTransform = target.transform;
        }
        go.transform.SetLayer(UILayerKey);
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/添加循环列表", priority = 51)]
    private static void AddLoopList(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/LoopList.prefab");
        if (orgin != null)
        {
            GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
            target.name = "LoopList";
            target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            target.transform.localScale = Vector3.one;
            Selection.activeTransform = target.transform.Find("@_listView_");
        }
        go.transform.SetLayer(UILayerKey);
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/添加循环网格列表", priority = 52)]
    private static void AddGridView(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/GridView.prefab");
        if (orgin != null)
        {
            GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
            target.name = "GridView";
            target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            target.transform.localScale = Vector3.one;
            Selection.activeTransform = target.transform.Find("@_gridView_");
        }
        go.transform.SetLayer(UILayerKey);
    }

    //[UnityEditor.MenuItem("GameObject/添加Aor组件/添加循环列表", priority = 50)]
    //private static void AddAorScrollList(MenuCommand command)
    //{
    //    GameObject go = command.context as GameObject;
    //    GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/LoopListGroup.prefab");
    //    if (orgin != null)
    //    {
    //        GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
    //        target.name = "LoopListGroup";
    //        target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    //        target.transform.localScale = Vector3.one;
    //        Selection.activeTransform = target.transform.Find("@_aorlooplist_");
    //    }
    //}

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/AorToggle", priority = 51)]
    // private static void AddAorToggle(MenuCommand command)
    // {
    //     GameObject go = command.context as GameObject;
    //     GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/$_toggle_.prefab");
    //     if (orgin != null)
    //     {
    //         GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
    //         target.name = "$_toggle_";
    //         target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    //         target.transform.localScale = Vector3.one;
    //         Selection.activeTransform = target.transform;
    //     }
    // }

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/AorSlider", priority = 52)]
    // private static void AorSlider(MenuCommand command)
    // {
    //     GameObject go = command.context as GameObject;
    //     GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/$_slider_.prefab");
    //     if (orgin != null)
    //     {
    //         GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
    //         target.name = "$_slider_";
    //         target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    //         target.transform.localScale = Vector3.one;
    //         Selection.activeTransform = target.transform;
    //     }
    // }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorButton", priority = 0)]
    private static void AddAorButton(MenuCommand command)
    {
        // GameObject go = command.context as GameObject;
        // var com = AddAorComponent<AorImage>(go);
        // com.raycastTarget = true;
        // com.name = "@_aorbtn_";
        // com.gameObject.AddComponent<AorButton>().targetGraphic = com;

        GameObject go = command.context as GameObject;
        GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/@_aorbtn_.prefab");
        if (orgin != null)
        {
            GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
            target.name = "@_aorbtn_";
            target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            target.transform.localScale = Vector3.one;
            Selection.activeTransform = target.transform.Find("@_aorbtn_");
        }
        go.transform.SetLayer(UILayerKey);
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/长按按钮AorLongPressButton", priority = 0)]
    private static void AddAorLongPressButton(MenuCommand command)
    {
        // GameObject go = command.context as GameObject;
        // var com = AddAorComponent<AorImage>(go);
        // com.raycastTarget = true;
        // com.name = "@_aorbtn_";
        // com.gameObject.AddComponent<AorButton>().targetGraphic = com;

        GameObject go = command.context as GameObject;
        GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TempRes/AorPrefab/@_aorlongpressbtn_.prefab");
        if (orgin != null)
        {
            GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
            target.name = "@_aorlongpressbtn_";
            target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            target.transform.localScale = Vector3.one;
            Selection.activeTransform = target.transform.Find("@_aorlongpressbtn_");
        }
        go.transform.SetLayer(UILayerKey);
    }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorObj", priority = 1)]
    private static void AddAorObj(MenuCommand command)
    {
        GameObject go = Selection.activeTransform.gameObject;
        GameObject addObj = new GameObject();
        addObj.transform.SetParent(go.transform);
        var rect = addObj.AddComponent<RectTransform>();
        rect.anchoredPosition3D = Vector3.zero;
        addObj.name = "@_obj_";
        go.transform.SetLayer(UILayerKey);
    }

    // [UnityEditor.MenuItem("GameObject/添加Aor组件/TmpDropdown", priority = 9)]
    // private static void AddTmpDropdown(MenuCommand command)
    // {
    //     GameObject go = command.context as GameObject;
    //     string path = "Assets/TestPrefab/@_tmpDropdown_.prefab";
    //     GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    //     if (orgin != null)
    //     {
    //         GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
    //         target.name = "@_tmpDropdown_";
    //         target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    //         target.transform.localScale = Vector3.one;
    //         Selection.activeTransform = target.transform;
    //     }
    //     else
    //     {
    //         Debug.LogError("can not load " + path);
    //     }
    // }

    [UnityEditor.MenuItem("GameObject/添加Aor组件/AorDropdown", priority = 53)]
    private static void AddAorDropdown(MenuCommand command)
    {
        GameObject go = command.context as GameObject;
        string path = "Assets/TestPrefab/@_aorDropdown_.prefab";
        GameObject orgin = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (orgin != null)
        {
            GameObject target = GameObject.Instantiate(orgin, Vector3.zero, Quaternion.identity, go.transform);
            target.name = "@_aorDropdown_";
            target.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            target.transform.localScale = Vector3.one;
            Selection.activeTransform = target.transform;
        }
        else
        {
            Debug.LogError("can not load " + path);
        }
        go.transform.SetLayer(UILayerKey);
    }
    #endregion


    #region 扩展
    [MenuItem("Tools/UI/选中的预制将AorTmp转AorText")]
    public static void AorTmpToAorText()
    {
        Font mainFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/Res/Fonts/Main.ttf");
        Font artFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/Res/Fonts/Art.ttf");
        GameObject[] selectGos = Selection.gameObjects;
        foreach (var gameObj in selectGos)
        {
            AorTMP[] allTmp = gameObj.GetComponentsInChildren<AorTMP>(true);
            if (allTmp.Length == 0)
            {
                EditorUtility.DisplayDialog("提示", gameObj.name + "预制件下没有AorTmp", "确定");
                return;
            }
            foreach (var tmp in allTmp)
            {
                GameObject tmpObj = tmp.gameObject;
                string tmpObjName = tmpObj.name;
                if (tmpObjName.Contains("aortmp"))
                {
                    string replaceName = tmpObjName.Replace("aortmp", "aortext");
                    tmpObj.name = replaceName;
                }
                else
                {
                    tmpObj.name = tmpObjName;
                }

                TMP_FontAsset tmpFontAsset = tmp.font;
                string tmpText = tmp.text;
                float tmpFontSize = tmp.fontSize;
                string tmpLangKey = tmp.languageKey;
                Color tmpColor = tmp.color;
                TextAlignmentOptions tmpAlignment = tmp.alignment;

                TextAnchor textAnchor = TextAnchor.MiddleCenter;
                switch (tmpAlignment)
                {
                    case TextAlignmentOptions.Bottom:
                        textAnchor = TextAnchor.MiddleRight;
                        break;
                    case TextAlignmentOptions.BottomLeft:
                        textAnchor = TextAnchor.LowerLeft;
                        break;
                    case TextAlignmentOptions.BottomRight:
                        textAnchor = TextAnchor.LowerRight;
                        break;
                    case TextAlignmentOptions.Top:
                        textAnchor = TextAnchor.MiddleLeft;
                        break;
                    case TextAlignmentOptions.TopLeft:
                        textAnchor = TextAnchor.UpperLeft;
                        break;
                    case TextAlignmentOptions.TopRight:
                        textAnchor = TextAnchor.UpperRight;
                        break;
                }

                // //如果有SoftMaskable那么要先移除掉，AorTmp会依赖它
                // SoftMaskable softMaskable = tmpObj.GetComponent<SoftMaskable>();
                // if (softMaskable != null)
                // {
                //     DestroyImmediate(softMaskable, true);
                // }
                DestroyImmediate(tmp, true);

                AorText aorText = tmpObj.AddComponent<AorText>();
                if (tmpFontAsset.name == "Main SDF")
                    aorText.font = mainFont;
                else if (tmpFontAsset.name == "Art SDF")
                    aorText.font = artFont;
                aorText.text = tmpText;
                aorText.fontSize = Convert.ToInt32(tmpFontSize);
                aorText.languageKey = tmpLangKey;
                aorText.color = tmpColor;
                aorText.alignment = textAnchor;
                aorText.raycastTarget = false;
            }
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("提示", "转换成功", "确定");
        }
    }
    #endregion
}
