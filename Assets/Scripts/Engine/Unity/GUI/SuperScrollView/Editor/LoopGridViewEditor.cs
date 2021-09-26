using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditorInternal;


namespace SuperScrollView
{

    [CustomEditor(typeof(LoopGridView))]
    public class LoopGridViewEditor : Editor
    {

        SerializedProperty mGridFixedType;
        SerializedProperty mGridFixedRowOrColumn;
        SerializedProperty mItemSnapEnable;
        SerializedProperty mArrangeType;
        SerializedProperty mItemPrefabDataList;
        SerializedProperty mItemSnapPivot;
        SerializedProperty mViewPortSnapPivot;
        SerializedProperty mPadding;
        SerializedProperty mItemSize;
        SerializedProperty mItemPadding;
        SerializedProperty mItemRecycleDistance;

        GUIContent mItemSnapEnableContent = new GUIContent("ItemSnapEnable");
        GUIContent mArrangeTypeGuiContent = new GUIContent("ArrangeType");
        GUIContent mItemPrefabListContent = new GUIContent("ItemPrefabList");
        GUIContent mItemSnapPivotContent = new GUIContent("ItemSnapPivot");
        GUIContent mViewPortSnapPivotContent = new GUIContent("ViewPortSnapPivot");
        GUIContent mGridFixedTypeContent = new GUIContent("GridFixedType");
        GUIContent mPaddingContent = new GUIContent("Padding");
        GUIContent mItemSizeContent = new GUIContent("ItemSize");
        GUIContent mItemPaddingContent = new GUIContent("ItemPadding");
        GUIContent mGridFixedRowContent = new GUIContent("RowCount");
        GUIContent mGridFixedColumnContent = new GUIContent("ColumnCount");
        GUIContent mItemRecycleDistanceContent = new GUIContent("RecycleDistance");

        int count = 0;
        protected virtual void OnEnable()
        {
            mGridFixedType = serializedObject.FindProperty("mGridFixedType");
            mItemSnapEnable = serializedObject.FindProperty("mItemSnapEnable");
            mArrangeType = serializedObject.FindProperty("mArrangeType");
            mItemPrefabDataList = serializedObject.FindProperty("mItemPrefabDataList");
            mItemSnapPivot = serializedObject.FindProperty("mItemSnapPivot");
            mViewPortSnapPivot = serializedObject.FindProperty("mViewPortSnapPivot");
            mGridFixedRowOrColumn = serializedObject.FindProperty("mFixedRowOrColumnCount");
            mItemPadding = serializedObject.FindProperty("mItemPadding");
            mPadding = serializedObject.FindProperty("mPadding");
            mItemSize = serializedObject.FindProperty("mItemSize");
            mItemRecycleDistance = serializedObject.FindProperty("mItemRecycleDistance");
        }


        void ShowItemPrefabDataList(LoopGridView listView)
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
        /// <param name="gridView"></param>
        void ShowPreviewList(LoopGridView gridView)
        {
#if UNITY_EDITOR
            if (gridView.ScrollRect == null)
                return;
            Transform parent = gridView.ScrollRect.viewport == null ? gridView.ScrollRect.transform : gridView.ScrollRect.viewport;
            Transform preview = parent.Find("Preview");
            string label = preview == null ? "Preview" : "RemovePrefive";

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUI.indentLevel += 1;
            count = EditorGUILayout.IntField("PreviewNum", count);
            if (GUILayout.Button(label))
            {
                if (preview == null)
                {
                    if (gridView.ItemPrefabDataList.Count <= 0)
                    {
                        return;
                    }
                    GridViewItemPrefabConfData data = gridView.ItemPrefabDataList[gridView.ItemPrefabDataList.Count - 1];
                    GameObject obj = data.mItemPrefab;
                    if (obj == null)
                    {
                        return;
                    }

                    RectMask2D mask = gridView.ScrollRect.viewport.GetComponent<RectMask2D>();
                    mask.enabled = false;

                    GameObject go = Instantiate(gridView.ScrollRect.content.gameObject, gridView.ScrollRect.viewport);
                    go.name = "Preview";
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;


                    RectTransform trans = go.GetComponent<RectTransform>();
                    Vector2 vector = new Vector2(0, 1);
                    if (gridView.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
                    {
                        vector = Vector2.zero;
                    }
                    else if (gridView.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
                    {
                        vector = Vector2.one;
                    }
                    else if (gridView.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
                    {
                        vector = new Vector2(1, 0);
                    }

                    trans.anchorMin = vector;
                    trans.anchorMax = vector;
                    trans.pivot = vector;
                    trans.anchoredPosition = Vector2.zero;

                    GridLayoutGroup layout = go.AddComponent<GridLayoutGroup>();
                    RectOffset padding = gridView.Padding;
                    layout.padding.left = padding.left;
                    layout.padding.right = padding.right;
                    layout.padding.top = padding.top;
                    layout.padding.bottom = padding.bottom;
                    Vector2 size = obj.GetComponent<RectTransform>().sizeDelta;
                    layout.cellSize = size;
                    layout.constraintCount = gridView.FixedRowOrColumnCount;
                    layout.spacing = gridView.ItemPadding;
                    switch (gridView.GridFixedType)
                    {
                        case GridFixedType.RowCountFixed:
                            layout.startAxis = GridLayoutGroup.Axis.Horizontal;
                            layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                            break;
                        case GridFixedType.ColumnCountFixed:
                            layout.startAxis = GridLayoutGroup.Axis.Vertical;
                            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                            break;
                    }
                    switch (gridView.ArrangeType)
                    {
                        case GridItemArrangeType.TopLeftToBottomRight:
                            layout.startCorner = GridLayoutGroup.Corner.UpperLeft;
                            layout.childAlignment = TextAnchor.UpperLeft;
                            break;
                        case GridItemArrangeType.BottomLeftToTopRight:
                            layout.startCorner = GridLayoutGroup.Corner.LowerLeft;
                            layout.childAlignment = TextAnchor.LowerLeft;
                            break;
                        case GridItemArrangeType.TopRightToBottomLeft:
                            layout.startCorner = GridLayoutGroup.Corner.UpperRight;
                            layout.childAlignment = TextAnchor.UpperRight;
                            break;
                        case GridItemArrangeType.BottomRightToTopLeft:
                            layout.startCorner = GridLayoutGroup.Corner.LowerRight;
                            layout.childAlignment = TextAnchor.LowerRight;
                            break;
                    }

                    ContentSizeFitter fitter = go.AddComponent<ContentSizeFitter>();
                    fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                    for (int i = 0; i < count; i++)
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
                    RectMask2D mask = gridView.ScrollRect.viewport.GetComponent<RectMask2D>();
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
            LoopGridView tListView = serializedObject.targetObject as LoopGridView;
            if (tListView == null)
            {
                return;
            }
            ShowItemPrefabDataList(tListView);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(mGridFixedType, mGridFixedTypeContent);
            if(mGridFixedType.enumValueIndex == (int)GridFixedType.ColumnCountFixed)
            {
                EditorGUILayout.PropertyField(mGridFixedRowOrColumn, mGridFixedColumnContent);
            }
            else
            {
                EditorGUILayout.PropertyField(mGridFixedRowOrColumn, mGridFixedRowContent);
            }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(mPadding, mPaddingContent,true);
            EditorGUILayout.PropertyField(mItemSize, mItemSizeContent);
            EditorGUILayout.PropertyField(mItemPadding, mItemPaddingContent);
            EditorGUILayout.PropertyField(mItemRecycleDistance, mItemRecycleDistanceContent);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(mItemSnapEnable, mItemSnapEnableContent);
            if(mItemSnapEnable.boolValue == true)
            {
                EditorGUILayout.PropertyField(mItemSnapPivot, mItemSnapPivotContent);
                EditorGUILayout.PropertyField(mViewPortSnapPivot, mViewPortSnapPivotContent);
            }
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(mArrangeType, mArrangeTypeGuiContent);

            ShowPreviewList(tListView);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
