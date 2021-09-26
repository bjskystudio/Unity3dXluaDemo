//***********************************
// Name: JY  Time:2019.05.16
// Des: 处理标记节点的方法单例
//***********************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class UINodeSingleton
{
    private UINodeSingleton() { }
    public static readonly UINodeSingleton instance = new UINodeSingleton();

    //以此字符串的节点开头,会向子节点继续遍历查找
    public const string NAMEFLAG1 = "@_";

    //以此字符串的节点开头,不会向子节点继续遍历查找
    public const string NAMEFLAG2 = "$_";

    /// <summary>
    /// 获取所有的标记节点
    /// </summary>
    /// <param name="transform">UI根Trans</param>
    /// <returns></returns>
    public List<Transform> AllFlagTrans(Transform go)
    {
        List<Transform> select_list = new List<Transform>();
        List<Transform> childList = new List<Transform>();
        go.GetComponentsInChildren<Transform>(true, childList);
        //if (go.name.Contains("$_"))//此标记要获取自身的类型
        //{
        //    childList.Add(go.transform);
        //}

        //从根节点遍历所有子节点,将符合要求(即含有标记)的节点存储到 child_list中
        foreach (Transform item in childList)
        {
            //"不符合命名规范的节点不需要再寻找";
            if (!item.name.StartsWith(NAMEFLAG1) && !item.name.StartsWith(NAMEFLAG2))//包含自身, 某些地方在调用自身的操作
            {
                continue;
            }
            bool is_continue = true;
            foreach (Transform parentNode in item.GetComponentsInParent<Transform>(true))
            {
                if (parentNode.name == go.name)//不包含本身节点
                {
                    break;
                }
                if (item != parentNode && parentNode.name.StartsWith(NAMEFLAG2))
                {
                    is_continue = false;
                    break;
                }
            }
            //"$_下的节点不需要再寻找";
            if (!is_continue)
            {
                //Debug.Log("    $_下的节点不需要再寻找  " + item.name);
                continue;
            }

            string keyname = UINodeSingleton.instance.GetUINodeKey(item.name);  //获取Lua使用的节点key
            if (keyname == string.Empty)
            {
                Debug.LogError("[class]UIGoTable 无法获取类型Key [name]" + item.name);
                continue;
            }
            var temp = UINodeSingleton.instance.GetUINodeType(item, keyname); //节点与keyname进行节点和类型的绑定
            if (temp == null)
            {
                Debug.LogError("无法从节点正确获取指定组件:" + string.Join("/", item.GetComponentsInParent<Transform>(true).Reverse().Select((s) => { return s.name; }).ToArray()), item);
                continue;
            }

            select_list.Add(item);
        }
        return select_list;
    }

    /// <summary>
    /// 使用节点名称转换出Lua使用的节点key
    /// </summary>
    /// <param name="name">节点原始名称</param>
    /// <returns></returns>
    public string GetUINodeKey(string name)
    {
        string key = string.Empty;
        if (name.StartsWith(UINodeSingleton.NAMEFLAG1))
        {
            key = name.Replace(UINodeSingleton.NAMEFLAG1, string.Empty);
        }
        if (name.StartsWith(UINodeSingleton.NAMEFLAG2))
        {
            key = name.Replace(UINodeSingleton.NAMEFLAG2, string.Empty);
        }
        return key;
    }
    /// <summary>
    /// 节点类型名称
    /// </summary>
    /// <param name="keyname">Lua使用的节点key</param>
    /// <returns></returns>
    private string GetUINodeTypeNmae(string keyname)
    {
        string typename = string.Empty;
        typename = keyname.Split('_')[0];
        return typename;

    }

    public UnityEngine.Object GetUINodeType(Transform trans, string keyname)
    {
        UnityEngine.Object obj = null;
        string type = GetUINodeTypeNmae(keyname);
        if (type == string.Empty)
        {
            Debug.LogError("[class]UINodeSingleton [fun]GetUINodeType [des]ui node get type is empty");
            return null;
        }
        switch (type)
        {
            case "obj":
                obj = trans.gameObject;
                break;
            case "trans":
                obj = trans.transform;
                break;
            case "rect":
                obj = trans.GetComponent<RectTransform>();
                break;
            //case "hcontainer":
            //    obj = trans.GetComponent<GeneralModule.Cell.HorizontalContainer>();
            //    break;
            //case "vcontainer":
            //    obj = trans.GetComponent<GeneralModule.Cell.VerticalContainer>();
            //    break;
            //case "hgcontainer":
            //    obj = trans.GetComponent<GeneralModule.Cell.HorizontalGridContainer>();
            //    break;
            //case "vgcontainer":
            //    obj = trans.GetComponent<GeneralModule.Cell.VerticalGridContainer>();
            //    break;
            case "text":
                obj = trans.GetComponent<Text>();
                break;
            case "aortext":
                obj = trans.GetComponent<AorText>();
                if (obj == null)
                {
                    obj= trans.GetComponent<AorGraphicText>();
                }
                break;
            case "aortmp":
                obj = trans.GetComponent<AorTMP>();
                break;
            case "aortmp3d":
                obj = trans.GetComponent<AorTMP3D>();
                break;
            case "btn":
                obj = trans.GetComponent<Button>();
                break;
            case "aorlongpressbtn":
                obj = trans.GetComponent<AorLongPressButton>();
                break;
            case "aorbtn":
                obj = trans.GetComponent<AorButton>();
                break;
            case "image":
                obj = trans.GetComponent<Image>();
                break;
            case "aorimage":
                obj = trans.GetComponent<AorImage>();
                break;
            case "rawimage":
                obj = trans.GetComponent<RawImage>();
                break;
            case "aorrawimage":
                obj = trans.GetComponent<AorRawImage>();
                break;
            case "slider":
                obj = trans.GetComponent<Slider>();
                break;
            case "canvs":
                obj = trans.GetComponent<Canvas>();
                break;
            case "aorinputfield":
                obj = trans.GetComponent<AorInputField>();
                break;
            case "inputfield":
                obj = trans.GetComponent<InputField>();
                break;
            case "toggle":
                obj = trans.GetComponent<Toggle>();
                break;
            //case "aorlooplist":
            //    obj = trans.GetComponent<MyScrollList>();
            //    break;
            //case "tswipe":
            //    obj = trans.GetComponent<GeneralModule.UIComponent.TSwipe>();
            //    break;
            case "canvasgroup":
                obj = trans.GetComponent<CanvasGroup>();
                break;
            case "spriter":
                obj = trans.GetComponent<SpriteRenderer>();
                break;
            case "aordrag":
                obj = trans.GetComponent<AorDrag>();
                break;
            case "evetrigger":
                obj = trans.GetComponent<UnityEngine.EventSystems.EventTrigger>();
                break;
            case "audiosource":
                obj = trans.GetComponent<AudioSource>();
                break;
            case "togglegroup":
                obj = trans.GetComponent<ToggleGroup>();
                break;
            case "tmpDropdown":
                obj = trans.GetComponent<TMP_Dropdown>();
                break;
            case "aorDropdown":
                obj = trans.GetComponent<Dropdown>();
                break;
            case "gridView":
                obj = trans.GetComponent<SuperScrollView.LoopGridView>();
                break;
            case "listView":
                obj = trans.GetComponent<SuperScrollView.LoopListView>();
                break;
            case "textmeshpro":
                obj = trans.GetComponent<TextMeshPro>();
                break;
            case "textmeshprougui":
                obj = trans.GetComponent<TextMeshProUGUI>();
                break;
            case "animator":
                obj = trans.GetComponent<Animator>();
                break;
            default:
                break;
        }
        return obj;
    }
}
