using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditorInternal;
using System;

namespace SuperScrollView
{

    [CustomEditor(typeof(LoopListView))]
    public class LoopListViewEditor : Editor
    {

        SerializedProperty mSupportScrollBar;
        SerializedProperty mItemSnapEnable;
        SerializedProperty mArrangeType;
        SerializedProperty mItemPrefabDataList;
        SerializedProperty mItemSnapPivot;
        SerializedProperty mViewPortSnapPivot;

        GUIContent mSupportScrollBarContent = new GUIContent("SupportScrollBar");
        GUIContent mItemSnapEnableContent = new GUIContent("ItemSnapEnable");
        GUIContent mArrangeTypeGuiContent = new GUIContent("ArrangeType");
        GUIContent mItemPrefabListContent = new GUIContent("ItemPrefabList");
        GUIContent mItemSnapPivotContent = new GUIContent("ItemSnapPivot");
        GUIContent mViewPortSnapPivotContent = new GUIContent("ViewPortSnapPivot");

        int count = 0;
        protected virtual void OnEnable()
        {
            mSupportScrollBar = serializedObject.FindProperty("mSupportScrollBar");
            mItemSnapEnable = serializedObject.FindProperty("mItemSnapEnable");
            mArrangeType = serializedObject.FindProperty("mArrangeType");
            mItemPrefabDataList = serializedObject.FindProperty("mItemPrefabDataList");
            mItemSnapPivot = serializedObject.FindProperty("mItemSnapPivot");
            mViewPortSnapPivot = serializedObject.FindProperty("mViewPortSnapPivot");
        }


        void ShowItemPrefabDataList(LoopListView listView)
        {
            EditorGUILayout.PropertyField(mItemPrefabDataList, mItemPrefabListContent);
            if (mItemPrefabDataList.isExpanded == false)
            {
                return;
            }
            EditorGUI.indentLevel += 1;
            if (GUILayout.Button("Add New"))
            {
                mItemPrefabDataList.InsertArrayElementAtIndex(mItemPrefabDataList.arraySize);
                if(mItemPrefabDataList.arraySize > 0)
                {
                    SerializedProperty itemData = mItemPrefabDataList.GetArrayElementAtIndex(mItemPrefabDataList.arraySize - 1);
                    SerializedProperty mItemPrefab = itemData.FindPropertyRelative("mItemPrefab");
                    mItemPrefab.objectReferenceValue = null;
                }
            }
            int removeIndex = -1;
            EditorGUILayout.PropertyField(mItemPrefabDataList.FindPropertyRelative("Array.size"));
            for (int i = 0; i < mItemPrefabDataList.arraySize; i++)
            {
                SerializedProperty itemData = mItemPrefabDataList.GetArrayElementAtIndex(i);
                SerializedProperty mInitCreateCount = itemData.FindPropertyRelative("mInitCreateCount");
                SerializedProperty mItemPrefab = itemData.FindPropertyRelative("mItemPrefab");
                SerializedProperty mItemPrefabPadding = itemData.FindPropertyRelative("mPadding");
                SerializedProperty mItemStartPosOffset = itemData.FindPropertyRelative("mStartPosOffset");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(itemData);
                if (GUILayout.Button("Remove"))
                {
                    removeIndex = i;
                }
                EditorGUILayout.EndHorizontal();
                if (itemData.isExpanded == false)
                {
                    continue;
                }
                mItemPrefab.objectReferenceValue = EditorGUILayout.ObjectField("ItemPrefab", mItemPrefab.objectReferenceValue, typeof(GameObject), true);
                mItemPrefabPadding.floatValue = EditorGUILayout.FloatField("ItemPadding", mItemPrefabPadding.floatValue);
                if(listView.ArrangeType == ListItemArrangeType.TopToBottom || listView.ArrangeType == ListItemArrangeType.BottomToTop)
                {
                    mItemStartPosOffset.floatValue = EditorGUILayout.FloatField("XPosOffset", mItemStartPosOffset.floatValue);
                }
                else
                {
                    mItemStartPosOffset.floatValue = EditorGUILayout.FloatField("YPosOffset", mItemStartPosOffset.floatValue);
                }
                mInitCreateCount.intValue = EditorGUILayout.IntField("InitCreateCount", mInitCreateCount.intValue);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            if (removeIndex >= 0)
            {
                mItemPrefabDataList.DeleteArrayElementAtIndex(removeIndex);
            }
            EditorGUI.indentLevel -= 1;
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="listView"></param>
        void ShowPreviewList(LoopListView listView)
        {
#if UNITY_EDITOR
            if (listView.ScrollRect == null)
                return;
            Transform parent = listView.ScrollRect.viewport == null ? listView.ScrollRect.transform : listView.ScrollRect.viewport;
            Transform preview = parent.Find("Preview");
            string label = preview == null ? "Preview" : "RemovePrefive";

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUI.indentLevel += 1;
            count = EditorGUILayout.IntField("PreviewNum",count);
            if (GUILayout.Button(label))
            {
                if (preview == null)
                {
                    if (listView.ItemPrefabDataList.Count <= 0)
                    {
                        Debug.LogWarning("没有预制信息");
                        return;
                    }
                    ItemPrefabConfData data = listView.ItemPrefabDataList[listView.ItemPrefabDataList.Count - 1];
                    GameObject obj = data.mItemPrefab;
                    if (obj == null)
                    {
                        Debug.LogWarning("没有设置预制体对象");
                        return;
                    }

                    //隐藏遮罩
                    RectMask2D mask = listView.ScrollRect.viewport.GetComponent<RectMask2D>();
                    mask.enabled = false;

                    //创建显示的面板
                    GameObject go = Instantiate(listView.ScrollRect.content.gameObject, listView.ScrollRect.viewport);
                    go.name = "Preview";
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;

                    //设置锚点信息
                    RectTransform trans = go.GetComponent<RectTransform>();
                    Vector2 vector = new Vector2(0,1);
                    if (listView.ArrangeType == ListItemArrangeType.BottomToTop)
                    {
                        vector = Vector2.zero;
                    }
                    else if(listView.ArrangeType == ListItemArrangeType.RightToLeft) 
                    {
                        vector = Vector2.one;
                    }
                    trans.anchorMin = vector;
                    trans.anchorMax = vector;
                    trans.pivot = vector;
                    trans.anchoredPosition = Vector2.zero;

                    //添加排列组件
                    GridLayoutGroup layout = go.AddComponent<GridLayoutGroup>();
                    int startOffset = (int)data.mStartPosOffset;
                    float padding = data.mPadding;
                    Vector2 size = obj.GetComponent<RectTransform>().sizeDelta;
                    layout.cellSize = size;
                    layout.constraintCount = 1;
                    switch (listView.ArrangeType)
                    {
                        case ListItemArrangeType.LeftToRight:
                            layout.padding.top = startOffset;
                            layout.spacing = new Vector2(padding, 0);
                            layout.startCorner = GridLayoutGroup.Corner.UpperLeft;
                            layout.startAxis = GridLayoutGroup.Axis.Horizontal;
                            layout.childAlignment = TextAnchor.UpperLeft;
                            layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                            break;
                        case ListItemArrangeType.TopToBottom:
                            layout.padding.left = startOffset;
                            layout.spacing = new Vector2(0, padding);
                            layout.startCorner = GridLayoutGroup.Corner.UpperLeft;
                            layout.startAxis = GridLayoutGroup.Axis.Vertical;
                            layout.childAlignment = TextAnchor.UpperLeft;
                            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                            break;
                        case ListItemArrangeType.RightToLeft:
                            layout.padding.top = startOffset;
                            layout.spacing = new Vector2(padding, 0);
                            layout.startCorner = GridLayoutGroup.Corner.UpperRight;
                            layout.startAxis = GridLayoutGroup.Axis.Horizontal;
                            layout.childAlignment = TextAnchor.UpperRight;
                            layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                            break;
                        case ListItemArrangeType.BottomToTop:
                            layout.padding.left = startOffset;
                            layout.spacing = new Vector2(0, padding);
                            layout.startCorner = GridLayoutGroup.Corner.LowerLeft;
                            layout.startAxis = GridLayoutGroup.Axis.Vertical;
                            layout.childAlignment = TextAnchor.LowerLeft;
                            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                            break;
                    }

                    //添加自适应组件
                    ContentSizeFitter fitter = go.AddComponent<ContentSizeFitter>();
                    fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                    //创建对应数量子对象
                    for(int i = 0; i < count; i++)
                    {
                        GameObject child = Instantiate(data.mItemPrefab, trans);
                        child.SetActive(true);
                        RectTransform childTrans = child.GetComponent<RectTransform>();
                        childTrans.anchorMin = vector;
                        childTrans.anchorMax = vector;
                        childTrans.pivot = vector;
                    }

                }
                else
                {
                    RectMask2D mask = listView.ScrollRect.viewport.GetComponent<RectMask2D>();
                    mask.enabled = true;
                    DestroyImmediate(preview.gameObject);
                }
            }
            EditorGUI.indentLevel -= 1;
#endif
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            LoopListView tListView = serializedObject.targetObject as LoopListView;
            if (tListView == null)
            {
                return;
            }
            ShowItemPrefabDataList(tListView);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(mSupportScrollBar, mSupportScrollBarContent);
            EditorGUILayout.PropertyField(mItemSnapEnable, mItemSnapEnableContent);
            if(mItemSnapEnable.boolValue == true)
            {
                EditorGUILayout.PropertyField(mItemSnapPivot, mItemSnapPivotContent);
                EditorGUILayout.PropertyField(mViewPortSnapPivot, mViewPortSnapPivotContent);
            }
            EditorGUILayout.PropertyField(mArrangeType, mArrangeTypeGuiContent);
            ShowPreviewList(tListView);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
