using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RGBScaleMaterialPropertyDrawer : MaterialPropertyDrawer
{
    private static readonly float[] s_Vector4Floats = new float[3];
    private static readonly GUIContent[] s_XYZWLabels = new GUIContent[3]
    {
        new GUIContent("R"),
        new GUIContent("G"),
        new GUIContent("B")
    };
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor editor)
    {
        // Setup
        Vector4 value = prop.vectorValue;

        EditorGUI.showMixedValue = prop.hasMixedValue;

        EditorGUI.LabelField(position, label);

        s_Vector4Floats[0] = value.x;
        s_Vector4Floats[1] = value.y;
        s_Vector4Floats[2] = value.z;
        position.height = 16f;
        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = position.width / 4;
        position.xMin += EditorGUIUtility.labelWidth;
        EditorGUI.BeginChangeCheck();
        EditorGUI.MultiFloatField(position, s_XYZWLabels, s_Vector4Floats);
        if (EditorGUI.EndChangeCheck())
        {
            value.x = s_Vector4Floats[0];
            value.y = s_Vector4Floats[1];
            value.z = s_Vector4Floats[2];
        }
        EditorGUIUtility.labelWidth = oldLabelWidth;
        EditorGUI.showMixedValue = false;
        prop.vectorValue = value;
    }
}
