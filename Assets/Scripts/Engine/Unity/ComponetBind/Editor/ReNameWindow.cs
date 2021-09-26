using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YoukiaCore;

public class ReNameWindow : EditorWindow
{
    #region Dictionary
    private static Dictionary<string, string> nameDesc = new Dictionary<string, string>()
    {
        { "$_aortext_ConfrimBtnText", "$_aortext_ConfrimBtnText"},
        { "$_aortext_CancelBtnText", "$_aortext_CancelBtnText"},
        { "$_aortext_FreeTips", "$_aortext_FreeTips"},
        { "$_aortext_CostNum", "$_aortext_CostNum"},


        { "$_aorimage_CostIcon", "$_aorimage_CostIcon"},


        { "$_aorbtn_CloseBtn", "$_aorbtn_CloseBtn"},
        { "$_aorbtn_ConfrimBtn", "$_aorbtn_ConfrimBtn"},
        { "$_aorbtn_CancelBtn", "$_aorbtn_CancelBtn"},

        { "$_aorbtn_AddBtn", "$_aorbtn_AddBtn"},
        { "$_aorbtn_SubBtn", "$_aorbtn_SubBtn"},
        { "$_aorbtn_MaxBtn", "$_aorbtn_MaxBtn"},
        { "$_aorbtn_SliderAddBtn", "$_aorbtn_SliderAddBtn"},
        { "$_aorbtn_SliderSubBtn", "$_aorbtn_SliderSubBtn"},

        // { "$_aorbtn_SliderSubBtn", "$_aorbtn_SliderSubBtn"},
        // { "$_aorbtn_SliderSubBtn", "$_aorbtn_SliderSubBtn"},

    };
    private static Dictionary<string, Type> widgetDics = new Dictionary<string, Type>()
    {
        {"obj", typeof(Transform)},
        {"aortext", typeof(AorText)},
        {"aortmp", typeof(AorTMP)},
        {"aortmp3d", typeof(AorTMP3D)},
        {"aorlongpressbtn", typeof(AorLongPressButton)},
        {"aorbtn", typeof(AorButton)},
        {"aorimage", typeof(AorImage)},
        {"aorrawimage",typeof(AorRawImage)},
        {"aorinputfield", typeof(AorInputField)},
        {"inputfield", typeof(InputField)},
        {"aordrag", typeof(AorDrag)},
        {"slider", typeof(Slider)},
        {"toggle", typeof(Toggle)},
        {"canvs", typeof(Canvas)},
        {"rect", typeof(RectTransform)},
        {"evetrigger",typeof(UnityEngine.EventSystems.EventTrigger)},
        {"input", typeof(InputField)},
        { "text", typeof(Text)},
        { "btn", typeof(Button)},
        { "image", typeof(Image)},
        {"rawimage",typeof(RawImage)},
        //{"myscroll", typeof(MyScrollList)},
    };

    #endregion


    #region Enum
    private enum OPTIONS
    {
        Origin,

        obj,
        rect,
        aortext,
        aortext2,
        aorbtn,
        aorimage,
        aorrawimage,
        aorinput,
        aordrag,

        slider,
        canvs,
        toggle,
        myscroll,
        evetrigger,
        textmeshpro,
        //text,
        //btn,
        //image,
        //rawimage,
        //input,
        //richgrid,
        //listview,
    }

    /// <summary>
    /// 是否为终点节点
    /// </summary>
    private enum ENDNODE
    {
        IsNotNode,
        NormalNode,
        EndNode,
    }
    #endregion

    private string oldName;
    private static OPTIONS comType_Cur = OPTIONS.Origin;
    private static ENDNODE nodeType_Cur = ENDNODE.IsNotNode;
    private static OPTIONS comType_Orgin = OPTIONS.Origin;
    private static ENDNODE nodeType_Orgin = ENDNODE.IsNotNode;

    //Dictionary<OPTIONS, Action> FuncDic;

    private Editor _editor;
    private bool fold = false;
    private static EditorWindow thisWindow;
    private Vector2 sp;

    public static Transform target;
    private static List<string> typeName;
    private static Dictionary<string, Type> highWidgetDics = new Dictionary<string, Type>();
    private static Dictionary<string, Type> lowWidgetDics = new Dictionary<string, Type>();

    [MenuItem("Tools/UI节点命名工具/快速命名 #F4")]
    public static void ReNameWindowShow()
    {
        typeName = new List<string>();
        foreach (var item in widgetDics.Keys)
        {
            typeName.Add(item + "_");
        }

        if (Selection.transforms.Length > 0)
        {
            target = Selection.transforms[0];
        }
        SelectionObjChange();
        highWidgetDics.Clear();
        lowWidgetDics.Clear();

        foreach (var item in widgetDics)
        {
            if (item.Value == typeof(Transform))
                continue;

            if (item.Key.Contains("aor") || item.Key == "input" || item.Key == "toggle" || item.Key.Contains("btn") || item.Key.Contains("evetrigger") || item.Key.Contains("listview"))
            {
                highWidgetDics.Add(item.Key, item.Value);
            }
            else
            {
                lowWidgetDics.Add(item.Key, item.Value);
            }

        }

        thisWindow = EditorWindow.GetWindow<ReNameWindow>(true, "快速命名");
        thisWindow.Show();
    }

    private bool CheckHasComponent(Type targetCom, bool needShowMsg = true)
    {
        //var obj = target as Component;

        if (target.GetComponent(targetCom) == null)
        {
            if (needShowMsg)
            {
                widgetDics.TryGetValue(comType_Cur.ToString().ToLower(), out targetCom);
                Debug.LogError("缺少组件:      " + targetCom.Name);
            }
            return false;
        }
        return true;
    }


    private bool AutoSetAttribute(out OPTIONS comType, out ENDNODE endNodeType)
    {
        comType = OPTIONS.obj;
        endNodeType = ENDNODE.NormalNode;
        foreach (var item in highWidgetDics)
        {
            if (item.Value == typeof(Button) ||
                //item.Value == typeof(AorButton) ||
                item.Value == typeof(UnityEngine.EventSystems.EventTrigger))
            {
                comType = OPTIONS.obj;
                endNodeType = ENDNODE.NormalNode;
                return true;
            }
            else if (CheckHasComponent(item.Value, false))
            {
                comType = (OPTIONS)Enum.Parse(typeof(OPTIONS), item.Key);
                endNodeType = ENDNODE.NormalNode;
                return true;
            }
        }

        foreach (var item in lowWidgetDics)
        {
            if (CheckHasComponent(item.Value, false))
            {
                comType = (OPTIONS)Enum.Parse(typeof(OPTIONS), item.Key);
                endNodeType = ENDNODE.NormalNode;
                return true;
            }
        }

        return false;
    }

    [MenuItem("Tools/UI节点命名工具/快速命名(无弹窗) &F1", priority = 0)]
    public static void QuickReName()
    {
        //GameObject selectGameObject = Selection.activeGameObject;
        //if (selectGameObject == null)
        //{
        //    Debug.LogError("请选择要命名的对象");
        //    return;
        //}
        Transform[] selecttransforms = Selection.transforms;
        if (selecttransforms.Length == 0)
        {
            Debug.LogError("请选择要命名的对象");
            return;
        }

        foreach(var selection in selecttransforms)
        {
            ModifyName(selection);
        }
    }

    private static void ModifyName(Transform selection)
    {
        if(selection != null)
        {
            var selectGameObject = selection.gameObject;
            Transform target = selection;

            if (target.name.Contains("@_"))
            {
                return;
            }

            if (target.name.Contains("$_"))
            {
                return;
            }

            if (selectGameObject.GetComponent<AorLongPressButton>())
            {
                target.name = "@_aorlongpressbtn_" + target.name;
            }
            else if(selectGameObject.GetComponent<AorButton>())
            {
               target.name = "@_aorbtn_" + target.name;
            }
            else if (selectGameObject.GetComponent<Button>())
            {
               target.name = "@_btn_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorImage>())
            {
               target.name = "@_aorimage_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorRawImage>())
            {
               target.name = "@_aorrawimage_" + target.name;
            }
            // else if (selectGameObject.GetComponent<GeneralModule.Cell.HorizontalContainer>())
            // {
            //    target.name = "@_hcontainer_" + target.name;
            // }
            // else if (selectGameObject.GetComponent<GeneralModule.Cell.VerticalContainer>())
            // {
            //    target.name = "@_vcontainer_" + target.name;
            // }
            // else if (selectGameObject.GetComponent<GeneralModule.Cell.HorizontalGridContainer>())
            // {
            //    target.name = "@_hgcontainer_" + target.name;
            // }
            // else if (selectGameObject.GetComponent<GeneralModule.Cell.VerticalGridContainer>())
            // {
            //    target.name = "@_vgcontainer_" + target.name;
            // }
            else if (selectGameObject.GetComponent<Slider>())
            {
                target.name = "@_slider_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorInputField>())
            {
               target.name = "@_aorinputfield_" + target.name;
            }
            else if (selectGameObject.GetComponent<InputField>())
            {
                target.name = "@_inputfield_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorDrag>())
            {
               target.name = "@_aordrag_" + target.name;
            }
            else if (selectGameObject.GetComponent<Image>())
            {
                target.name = "@_image_" + target.name;
            }
             else if (selectGameObject.GetComponent<RawImage>())
            {
                target.name = "@_rawimage_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorTMP>())
            {
               target.name = "@_aortmp_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorTMP3D>())
            {
               target.name = "@_aortmp3d_" + target.name;
            }
            else if (selectGameObject.GetComponent<AorText>())
            {
               target.name = "@_aortext_" + target.name;
            }
            else if (selectGameObject.GetComponent<Text>())
            {
               target.name = "@_text_" + target.name;
            }
            //else if (selectGameObject.GetComponent<MyScrollList>())
            //{
            //    target.name = "@_myscroll_" + target.name;
            //}
            else if (selectGameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>())
            {
                target.name = "@_evetrigger_" + target.name;
            }
            //else if (selectGameObject.GetComponent<GeneralModule.UIComponent.TSwipe>())
            //{
            //    target.name = "@_tswipe_" + target.name;
            //}
            else if (selectGameObject.GetComponent<CanvasGroup>())
            {
                target.name = "@_canvasgroup_" + target.name;
            }
            else if (selectGameObject.GetComponent<AudioSource>())
            {
                target.name = "@_audiosource_" + target.name;
            }
            else if (selectGameObject.GetComponent<ToggleGroup>())
            {
                target.name = "@_togglegroup_" + target.name;
            }
            else if (selectGameObject.GetComponent<Toggle>())
            {
                target.name = "@_toggle_" + target.name;
            }
            else if (selectGameObject.GetComponent<TMP_Dropdown>())
            {
                target.name = "@_tmpDropdown_" + target.name;
            }
            else if (selectGameObject.GetComponent<Dropdown>())
            {
                target.name = "@_aorDropdown_" + target.name;
            }
            else if (selectGameObject.GetComponent<TextMeshProUGUI>())
            {
                target.name = "@_textmeshprougui_" + target.name;
            }
            else if (selectGameObject.GetComponent<TextMeshPro>())
            {
                target.name = "@_textmeshpro_" + target.name;
            }
            else if (selectGameObject.GetComponent<Animator>())
            {
                target.name = "@_animator_" + target.name;
            }
            else
            {
                target.name = "@_obj_" + target.name;
            }
        }
    }


    private void OnGUI()
    {
        if (Selection.transforms.Length > 0)
        {
            target = Selection.transforms[0];
        }

        if (target == null)
        {
            return;
        }

        EditorGUILayout.BeginVertical();

        GetOrginName();
        EditorGUILayout.LabelField("原始名称", oldName);
        comType_Orgin = comType_Cur;
        nodeType_Orgin = nodeType_Cur;
        EditorGUILayout.Space();
        if (GUILayout.Button("快捷命名"))
        {
            if (AutoSetAttribute(out comType_Cur, out nodeType_Cur))
            {
                target.name = oldName;
                string fName = comType_Cur.ToString().ToLower() + "_" + oldName;
                target.name = fName;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (oldName.Contains(" "))
        {
            EditorGUILayout.HelpBox("节点命名中包含空格", MessageType.Error);
        }

        comType_Cur = (OPTIONS)EditorGUILayout.EnumPopup("设置节点的类型", comType_Cur);
        if (comType_Cur != OPTIONS.Origin)
        {
            Type t = widgetDics[comType_Cur.ToString().ToLower()];
            if (comType_Cur != comType_Orgin)
            {
                if (CheckHasComponent(t))
                {
                    target.name = oldName;
                    string fName = comType_Cur.ToString().ToLower() + "_" + oldName;
                    target.name = fName;
                }
                else
                {
                    comType_Cur = comType_Orgin;
                }
            }

        }

        EditorGUILayout.Space();
        nodeType_Cur = (ENDNODE)EditorGUILayout.EnumPopup("是否为终点节点", nodeType_Cur);
        if (comType_Cur == OPTIONS.Origin)
        {
            nodeType_Cur = ENDNODE.IsNotNode;
        }

        if (nodeType_Cur != ENDNODE.IsNotNode)
        {
            if (target.name.Contains("@_"))
            {
                target.name = target.name.Replace("@_", string.Empty);
            }
            if (target.name.Contains("$_"))
            {
                target.name = target.name.Replace("$_", string.Empty);
            }
        }

        switch (nodeType_Cur)
        {
            case ENDNODE.EndNode:

                if (!target.name.Contains("$_"))
                {
                    target.name = "$_" + target.name;
                }
                break;
            case ENDNODE.NormalNode:
                if (!target.name.Contains("@_"))
                {
                    target.name = "@_" + target.name;
                }
                break;
            case ENDNODE.IsNotNode:
                break;
            default:
                break;
        }

        if (comType_Cur == OPTIONS.Origin)
        {
            GetOrginName();
            target.name = oldName;
        }


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("设置的名称:    " + target.name, MessageType.Info);

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        fold = EditorGUILayout.Foldout(fold, "显示已有的命名");
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // 控制折叠
        if (fold)
        {
            sp = GUI.BeginScrollView(new Rect(0, 0, Screen.width, Screen.height), sp, new Rect(0, 0, Screen.width, 400));
            foreach (var item in nameDesc)
            {
                if (GUILayout.Button(item.Key))
                {
                    oldName = target.name;
                    target.name = item.Value;
                }
            }
            GUI.EndScrollView();
        }
    }

    private void OnSelectionChange()
    {
        if (Selection.transforms.Length > 0 && Selection.transforms[0] != target)
        {
            target = Selection.transforms[0];
            SelectionObjChange();
            GetOrginName();
        }
        thisWindow.Repaint();
    }

    private void GetOrginName()
    {
        oldName = GetOrginName(target.name);
    }

    private string GetOrginName(string name)
    {
        if (name.Contains("@_"))
        {
            name = name.Replace("@_", string.Empty);
            name = GetOrginName(name);
        }
        if (name.Contains("$_"))
        {
            name = name.Replace("$_", string.Empty);
            name = GetOrginName(name);
        }

        foreach (var item in typeName)
        {
            if (name.Contains(item))
            {
                string comTypeName = name.Split('_')[0] + "_";
                name = name.Replace(comTypeName, string.Empty);
                name = GetOrginName(name);
            }
        }

        return name;
    }

    private static void SelectionObjChange()
    {
        if (target == null)
        {
            return;
        }

        string name = target.name;
        ENDNODE temp_nodeType = ENDNODE.IsNotNode;
        OPTIONS temp_comType = OPTIONS.Origin;

        foreach (var item in typeName)
        {
            if (name.Contains(item))
            {
                if (name.Contains("@_"))
                {
                    temp_nodeType = ENDNODE.NormalNode;
                    name = name.Replace("@_", string.Empty);
                }
                if (name.Contains("$_"))
                {
                    temp_nodeType = ENDNODE.EndNode;
                    name = name.Replace("$_", string.Empty);
                }

                string comTypeName = name.Split('_')[0];
                temp_comType = (OPTIONS)Enum.Parse(typeof(OPTIONS), comTypeName);
            }
        }
        if (temp_nodeType != nodeType_Cur)
        {
            nodeType_Cur = temp_nodeType;
            nodeType_Orgin = temp_nodeType;
        }

        if (temp_comType != comType_Cur)
        {
            comType_Cur = temp_comType;
            comType_Orgin = temp_comType;
        }
    }

    #region MyRegion
    /*
    [MenuItem("Tools/SetRectSize &R")]
    public static void AutoFindUi()
    {
        Transform target = Selection.activeTransform;
        Transform sizeNode = target.GetChild(target.childCount - 1);

        List<Transform> t = new List<Transform>();

        for (int i = 0; i < target.childCount; i++)
        {
            if (target.GetChild(i) != sizeNode)
            {
                t.Add(target.GetChild(i));
            }
        }

        foreach (var item in t)
        {
            item.SetParent(sizeNode, true);
        }

        target.GetComponent<RectTransform>().sizeDelta = sizeNode.GetComponent<RectTransform>().sizeDelta;
        sizeNode.SetParent(target.parent, true);
        target.position = sizeNode.position;
        sizeNode.SetParent(target, true);

        foreach (var item in t)
        {
            item.SetParent(target, true);
        }
        sizeNode.transform.SetAsLastSibling();
    }
    */
    #endregion
}