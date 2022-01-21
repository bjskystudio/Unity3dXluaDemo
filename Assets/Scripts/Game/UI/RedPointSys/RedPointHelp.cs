// using UnityEngine;

// [XLua.LuaCallCSharp]
// public static class RedPointHelp
// {
//     //暂时不提供使用
//     public static RedPointNode GetNodeByKey(string key)
//     {
//         return CSRedPointManager.Instance.GetNode(key);
//     }
    
//     public static void AddRedPointNode(string key)
//     {
//         CSRedPointManager.Instance.AddRedPointNode(key);
//     }

//     public static void RemoveChildNode(string key)
//     {
//         CSRedPointManager.Instance.RemoveChildNode(key);
//     }
    
//     //绑定节点数据, 返回成功或失败
//     public static int BindDataNode(GameObject redPointPref, string key)
//     {
//         return BindDataNodeAndType(redPointPref, key, (int)eRedPointType.Dependency, (int)eRedPointType.Dependency);
//     }

//     public static int BindDataNodeAndType(GameObject redPointPref, string key, int childType, int parentType)
//     {
//         var redPoint = redPointPref.GetComponent<RedPointPref>();
//         if (redPoint == null)
//         {
//             YoukiaCore.Log.Log.Error("传入的红点预制, 未发现红点组件<RedPointPref>");
//             return 0;
//         }
//         var redPointNode = CSRedPointManager.Instance.GetNode(key);
//         if (redPointNode == null)
//         {
//             CSRedPointManager.Instance.AddRedPointNode(key);
//             redPointNode = CSRedPointManager.Instance.GetNode(key);
//         }
//         redPoint.DynamicBind(key, new RedType() { ChildType = (eRedPointType)childType, ParentType = (eRedPointType)parentType });

//         return 1;
//     }

// }
