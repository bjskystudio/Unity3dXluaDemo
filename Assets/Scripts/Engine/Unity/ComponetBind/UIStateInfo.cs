using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;
using LuaAPI = XLua.LuaDLL.Lua;

public class UIStateInfo : MonoBehaviour
{
    public string stateName;
    public int objCount;

    [HideInInspector]
    [SerializeField]
    private RectTransform[] list;
    public RectTransform[] List
    {
        get => list;
        set
        {
            list = value;
            objCount = value.Length;
        }
    }

    [HideInInspector]
    [SerializeField]
    private bool[] active;

    /// <summary>
    /// 同步坐标 旋转
    /// </summary>
    public bool syncPos = true;
    public bool syncSelfPos = true;
    [HideInInspector]
    [SerializeField]
    private Vector3[] posList;

    public bool syncRotation = true;
    [HideInInspector]
    [SerializeField]
    private Quaternion[] rotList;

    /// <summary>
    /// 同步缩放
    /// </summary>
    public bool syncScale = true;
    [HideInInspector]
    [SerializeField]
    private Vector3[] scaleList;

    /// <summary>
    /// 同步宽高
    /// </summary>
    public bool syncSize = true;
    [HideInInspector]
    [SerializeField]
    private Rect[] tfRectList;


    // private Dictionary<int, RectTransform> RectTFDict = new Dictionary<int, RectTransform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void InitValueList(int length)
    {
        this.active = new bool[length];
        this.posList = new Vector3[length];
        this.scaleList = new Vector3[length];
        this.rotList = new Quaternion[length];
        this.tfRectList = new Rect[length];
    }

    public void SetAllTF(RectTransform[] tTFList)
    {
        this.List = tTFList;
        this.InitValueList(tTFList.Length);
        for (int i = 0; i < tTFList.Length; i++)
        {
            RectTransform tTransform = tTFList[i] as RectTransform;
            this.GetUIState(i, active, posList, rotList, scaleList, tfRectList, tTransform);
        }
    }

    private void GetUIState(int idx, bool[] active, Vector3[] posList, Quaternion[] rotList, Vector3[] scaleList, Rect[] tfRectList, RectTransform rectTF)
    {
        active[idx] = rectTF.gameObject.activeSelf;
        tfRectList[idx] = rectTF.rect;
        posList[idx] = rectTF.anchoredPosition;
        rotList[idx] = rectTF.rotation;
        scaleList[idx] = rectTF.localScale;
    }

    public void SyncAllStateInfo(RectTransform[] list)
    {
        if (this.IsSameList(this.List, list)) { return; }

        if (this.List == null)
        {
            this.List = new RectTransform[0];
        }
        bool[] tNewActiveList = new bool[list.Length];
        Vector3[] posList = new Vector3[list.Length];
        Vector3[] scaleList = new Vector3[list.Length];
        Quaternion[] rotList = new Quaternion[list.Length];
        Rect[] tfRectList = new Rect[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            int tIdx = Array.IndexOf(this.List, list[i]);
            if (tIdx == -1)
            {
                this.GetUIState(i, tNewActiveList, posList, rotList, scaleList, tfRectList, list[i]);
                tNewActiveList[i] = list[i].gameObject.activeSelf;
            }
            else
            {
                tNewActiveList[i] = this.active[tIdx];
                posList[i] = this.posList[tIdx];
                rotList[i] = this.rotList[tIdx];
                scaleList[i] = this.scaleList[tIdx];
                tfRectList[i] = this.tfRectList[tIdx];
            }

        }
        this.List = list;
        this.active = tNewActiveList;
        this.posList = posList;
        this.scaleList = scaleList;
        this.rotList = rotList;
        this.tfRectList = tfRectList;
    }

    public bool IsSameList(RectTransform[] list1, RectTransform[] list2)
    {
        if (list1 == null || list2 == null) { return false; }
        if (list1.Length != list2.Length) { return false; }
        for (int j = 0; j < list1.Length; j++)
        {
            if (list1[j] != list2[j]) { return false; }
        }

        return true;
    }

    public void ChangeTo()
    {
        if (this.List == null) { return; }
        for (int i = 0; i < this.List.Length; i++)
        {
            RectTransform tRectTransform = this.List[i];
            GameObject tGameObject = tRectTransform.gameObject;
            if (this.active != null && this.active.Length > i)
            {
                tGameObject.SetActive(this.active[i]);
            }

            if (
                this.syncPos
                &&
                (this.posList != null && this.posList.Length > i)
                &&
                 (tGameObject != this.gameObject || this.syncSelfPos)
                )
            {
                tRectTransform.anchoredPosition = new Vector2(this.posList[i].x, this.posList[i].y);
            }

            if (this.syncRotation && (this.rotList != null && this.rotList.Length > i))
            {
                tRectTransform.rotation = new Quaternion(this.rotList[i].x, this.rotList[i].y, this.rotList[i].z, this.rotList[i].w);
            }

            if (this.syncScale && this.scaleList != null && this.scaleList.Length > i)
            {
                tRectTransform.localScale = new Vector3(this.scaleList[i].x, this.scaleList[i].y, this.scaleList[i].z);
            }

            if (this.syncSize && this.tfRectList != null && this.tfRectList.Length > i)
            {
                tRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.tfRectList[i].width);
                tRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.tfRectList[i].height);
            }
        }
    }
}
