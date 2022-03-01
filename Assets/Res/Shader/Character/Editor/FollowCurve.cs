using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FollowCurve : EditorWindow
{
    AnimationCurve curveX = AnimationCurve.Linear(0, 0, 0, 0);
  //  AnimationCurve curveY = AnimationCurve.Linear(0, 0, 10, 10);
  //  public Material mat;
    public int SampleRate = 128;

    [MenuItem("Examples/Create Curve For Object")]
    static void Init()
    {
        FollowCurve window = (FollowCurve)EditorWindow.GetWindow(typeof(FollowCurve));
        window.Show();
    }

    [System.Obsolete]
    void OnGUI()
    {
        curveX = EditorGUILayout.CurveField("Start", curveX);
   //     curveY = EditorGUILayout.CurveField("End", curveY);
        SampleRate = EditorGUILayout.IntField(SampleRate);
    //    mat = EditorGUILayout.ObjectField(mat, typeof(Material))as Material;
        if (GUILayout.Button("Generate Curve"))
            AddCurveToSelectedGameObject();
    }

    void AddCurveToSelectedGameObject()
    {

        ExportCurveTexture(curveX, "CurveMap");

    }

    void ExportCurveTexture(AnimationCurve curve,string name)
    {
        var tex = new Texture2D(SampleRate, 1, TextureFormat.ARGB32, false);

        float invSampleRate = 1.0f / SampleRate;

        for(int i = 0; i < SampleRate; i++)
        {
            var c = Color.white;
            c.b = c.g = c.r = curve.Evaluate(i * invSampleRate);
            Debug.Log(c);
            tex.SetPixel(i, 0, c);
        }

        File.WriteAllBytes(Application.dataPath + $"/{name}.png", tex.EncodeToPNG());
        AssetDatabase.Refresh();
    }
}