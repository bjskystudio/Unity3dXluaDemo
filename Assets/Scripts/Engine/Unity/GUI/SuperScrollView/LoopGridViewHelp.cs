using SuperScrollView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static LoopListViewHelp;

[XLua.LuaCallCSharp]
public static class LoopGridViewHelp
{

    // item刷新时
    public static OnRefreshAction onRefreshEvent = null;
    // 拖拽结束
    public static OnRefreshAction onEndDragEvent = null;
    // 销毁时
    public static OnDestroyAction onDestroyEvent = null;

    static LoopGridViewHelp()
    {
        LoopGridView.onViewDestroyEvent += (loopGridView) =>
        {
            if (onDestroyEvent != null)
            {
                onDestroyEvent(loopGridView.GetInstanceID());
            }
        };
    }

    public static void Release()
    {
        onRefreshEvent = null;
        onEndDragEvent = null;
        onDestroyEvent = null;
    }

    // 初始化视图
    public static void InitGridView(LoopGridView loopGridView, int count)
    {
        var objId = loopGridView.GetInstanceID();
        loopGridView.InitGridView(count, (LoopGridView gridView, int itemIndex, int row, int column) =>
        {
            if (itemIndex < 0 || itemIndex >= gridView.ItemTotalCount)
            {
                return null;
            }
            if (onRefreshEvent != null)
            {
                var r = onRefreshEvent(objId, itemIndex);
                if (r == null)
                {
                    return null;
                }
                return r.GetComponent<LoopGridViewItem>();
            }
            return null;
        });
    }

    // 构建一个视图item
    public static Transform NewViewItem(LoopGridView gridView, string name)
    {
        var item = gridView.NewListViewItem(name);
        return item.transform;
    }

    // 获取指定索引下的视图item
    public static Transform GetItemByItemIndex(LoopGridView view, int itemIndex)
    {
        var itemview = view.GetShownItemByItemIndex(itemIndex);
        if (itemview == null)
        {
            return null;
        }
        return itemview.transform;
    }

    // 设置item长度
    public static void SetItemCount(LoopGridView gridView, int itemCount, int iresetPos)
    {
        var resetPos = iresetPos != 0;
        gridView.SetListItemCount(itemCount, resetPos);
    }

    // 注册拖拽结束事件
    public static void RegisterEndDragEvent(LoopGridView view, int objId)
    {
        view.mOnEndDragAction = (data) =>
        {
            onEndDragEvent(objId, 0);
        };
    }

    // 取消拖拽结束事件
    public static void UnRegisterEndDragEvent(LoopGridView view)
    {
        view.mOnEndDragAction = null;
    }

}