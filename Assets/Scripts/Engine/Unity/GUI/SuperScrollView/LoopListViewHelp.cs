using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[XLua.LuaCallCSharp]
public static class LoopListViewHelp
{
    [XLua.CSharpCallLua]
    public delegate Transform OnRefreshAction(int comId, int index);
    [XLua.CSharpCallLua]
    public delegate void OnDestroyAction(int comId);

    public static OnRefreshAction onRefreshEvent = null;

    public static OnDestroyAction onDestroyEvent = null;

    public static OnRefreshAction onEndDragEvent = null;



    static LoopListViewHelp()
    {
        LoopListView.onLoopListViewDestroyEvent += (loopListView) =>
        {
            if (onDestroyEvent != null)
            {
                onDestroyEvent(loopListView.GetInstanceID());
            }
        };
    }

    public static void Release()
    {
        onRefreshEvent = null;
        onDestroyEvent = null;
    }

    //暂时不提供使用
    public static void InitListView(LoopListView loopListView, int count, int initJumpIndex)
    {
        var objId = loopListView.GetInstanceID();
        loopListView.InitListView(count, (LoopListView arg1, int index) =>
        {
            if (index < 0 || index >= arg1.ItemTotalCount)
            {
                return null;
            }
            if (onRefreshEvent != null)
            {
                var r = onRefreshEvent(objId, index);
                if (r == null)
                {
                    return null;
                }
                return r.GetComponent<LoopListViewItem>();
            }
            return null;
        }, null, initJumpIndex);
    }

    public static Transform NewListViewItem(LoopListView arg1, string name)
    {
        var item = arg1.NewListViewItem(name);
        return item.transform;
    }

    public static void RefreshListViewItemAll(LoopListView loopListView, int itemTotalCount, int resetPos)
    {
        if (itemTotalCount < 0 || loopListView.ItemTotalCount == itemTotalCount)
        {
            if (resetPos != 0)
            {
                loopListView.MovePanelToItemIndex(0, 0);
            }
            else
            {
                loopListView.RefreshAllShownItem();
            }
        }
        else
        {
            //这里面可能存在bug, 设置长度不一定会调用到刷新, ?需要测试
            loopListView.SetListItemCount(itemTotalCount, resetPos != 0 ? 0 : -1);
            //loopListView.ResetListView
        }
    }

    public static Transform GetListViewItem(LoopListView loopListView, int itemIndex)
    {
        var item = loopListView.GetShownItemByItemIndex(itemIndex);
        if (item == null)
            return null;
        return item.transform;
    }

    public static Transform GetShowListViewItem(LoopListView loopListView,int showIndex)
    {
        var item = loopListView.GetShownItemByIndex(showIndex);
        if (item == null)
            return null;
        return item.transform;
    }

    public static void SetViewItemSize(LoopListView loopListView, int itemIndex, int Size)
    {
        var item = loopListView.GetShownItemByItemIndex(itemIndex);
        if (item == null)
            return;
        RectTransform rt = item.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Size);
        //rt.sizeDelta = new Vector2(rt.sizeDelta.x, Size);
    }

    public static void OnItemSizeChanged(LoopListView loopListView, int itemIndex)
    {
        loopListView.OnItemSizeChanged(itemIndex);
    }

    public static void SetViewItemWidth(Transform trans, float itemWidth)
    {
        LoopListViewItem listViewItem = trans.GetComponent<LoopListViewItem>();
        listViewItem.SetItemWidth(itemWidth);
    }

    public static void SetViewItemHeight(Transform trans, float itemHeight)
    {
        LoopListViewItem listViewItem = trans.GetComponent<LoopListViewItem>();
        listViewItem.SetItemHeight(itemHeight);
    }

    public static int GetItemIndex(Transform trans)
    {
        LoopListViewItem listViewItem = trans.GetComponent<LoopListViewItem>();
        return listViewItem.ItemIndex;
    }

    public static void RegisterEndDragEvent(LoopListView loopListView, int objId)
    {
        loopListView.mOnEndDragAction = () =>
        {
            onEndDragEvent(objId, 0);
        };
    }

    public static void UnRegisterEndDragEvent(LoopListView loopListView)
    {
        loopListView.mOnEndDragAction = null;
    }


    public static float GetDistanceWithViewPortSnapCenter(Transform trans)
    {
        LoopListViewItem listViewItem = trans.GetComponent<LoopListViewItem>();
        return listViewItem.DistanceWithViewPortSnapCenter;
    }
}