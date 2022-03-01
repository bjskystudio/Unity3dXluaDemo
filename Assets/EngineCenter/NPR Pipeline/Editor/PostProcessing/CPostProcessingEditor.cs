using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EngineCenter.NPRPipeline.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEditor.Rendering.Universal;

public class CPostProcessingEditor : EditorWindow
{
    GUIContent[] m_aLayerNames;
    string[] m_asLayerNames;

    GUIStyle m_HeaderLabel;
    GUIStyle m_EffectLabel;
    Vector2 m_vScroll = new Vector2(0.0f, 0.0f);

    [MenuItem("EngineCenter/NPR Pipeline/Post Processing Editor")]
    public static void StartUp()
    {
        EditorWindow.GetWindow(typeof(CPostProcessingEditor), false, "Post Processing Editor");
    }

    void OnEnable()
    {
        CEditorUtils.CreateLayerInfo(out m_asLayerNames);
        m_aLayerNames = new GUIContent[m_asLayerNames.Length];
        for (int i = 0; i < m_asLayerNames.Length; ++i)
            m_aLayerNames[i] = new GUIContent(m_asLayerNames[i]);
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
    void OnGUI()
    {
        bool draw_line = Event.current.type == EventType.Repaint;
        m_HeaderLabel = new GUIStyle("MiniLabel");
        m_HeaderLabel.fontStyle = FontStyle.Italic;
        m_EffectLabel = new GUIStyle("CN CountBadge");
        m_EffectLabel.fontStyle = FontStyle.BoldAndItalic;

        m_vScroll = EditorGUILayout.BeginScrollView(m_vScroll);
        List<AEffect>[] effects = CPostProcessManager.instance.effects;

        const float effect_col1 = 300.0f, effect_col2 = 200.0f;

        AEffect effect;
        List<AEffect> list;

        CAdditionalCameraData data;
        Camera[] objs = FindObjectsOfType<Camera>() as Camera[];

        const float camera_col0 = 250.0f, camera_col1 = 15.0f;

        //Rect rect = GUILayoutUtility.GetRect(1.0f, 17.0f);

        //EditorGUI.DrawRect(rect, Color.white);
        //bool state = false;

        //rect = EditorGUILayout.GetControlRect();
        //EditorGUI.DrawRect(rect, Color.white);

        //state = GUI.Toggle(rect, state, GUIContent.none, EditorStyles.foldout);

        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField(new GUIContent("Camera Objects", "Currently actived camera object list"), m_HeaderLabel, new GUILayoutOption[] { GUILayout.Width(camera_col0) });
        //EditorGUILayout.LabelField(new GUIContent("PP", "Post Processing Enabled"), m_HeaderLabel, new GUILayoutOption[] { GUILayout.Width(camera_col1) });
        //EditorGUILayout.LabelField(new GUIContent("Culling Mask", "Post Processing Culling Mask"), m_HeaderLabel);
        //EditorGUILayout.EndHorizontal();

        Camera camera = null;
        float camera_y_start = 0.0f;
        float camera_y_end = 0.0f;
        float effect_y_start = 0.0f;
        float effect_y_end = 0.0f;
        bool need_draw = false;
        for (int j = 0; j < objs.Length; ++j)
        {
            camera = objs[j];
            if (camera.cameraType == CameraType.SceneView)
                continue;

            data = camera.GetComponent<CAdditionalCameraData>();
            if (data == null)
                continue;

            need_draw = false;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            data.renderPostProcessing = EditorGUILayout.Toggle(data.renderPostProcessing, new GUILayoutOption[] { GUILayout.Width(camera_col1) });
            EditorGUILayout.ObjectField(camera, typeof(Camera), true, new GUILayoutOption[] { GUILayout.Width(camera_col0) });
            data.renderPostProcessingCulling = EditorGUILayout.MaskField(data.renderPostProcessingCulling, m_asLayerNames);
            EditorUtility.SetDirty(data);
            EditorGUILayout.EndHorizontal();

            if (!data.renderPostProcessing)
                continue;

            if (draw_line)
            {
                Rect rect = GUILayoutUtility.GetLastRect();
                camera_y_start = rect.y + 18.0f;
            }

            EditorGUI.indentLevel += 2;
            for (int i = 0; i < effects.Length; ++i)
            {
                list = effects[i];
                if (list == null || list.Count == 0)
                    continue;

                if (!HaveEffect(list, data.renderPostProcessingCulling.value))
                    continue;

                EditorGUILayout.LabelField(((EFFECT_TYPE)i).ToString() + ":", m_EffectLabel);

                if (draw_line)
                {
                    Rect rect = GUILayoutUtility.GetLastRect();
                    rect.y += 8.0f;
                    //Handles.color = Color.red;
                    Handles.DrawLines(new Vector3[] { new Vector3(25.0f, rect.y, 0.0f), new Vector3(32.0f, rect.y, 0.0f) });
                    camera_y_end = rect.y;
                    effect_y_start = rect.y + 8.0f;

                    need_draw = true;
                }

                EditorGUI.indentLevel += 1;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Effect Objects", "Currently actived post-processing effects list"), m_HeaderLabel, new GUILayoutOption[] { GUILayout.Width(effect_col1) });
                EditorGUILayout.LabelField(new GUIContent("Layer", "The layer that this Effect is in."), m_HeaderLabel, new GUILayoutOption[] { GUILayout.Width(effect_col2) });
                EditorGUILayout.LabelField(new GUIContent("Class Priority", "Specifies the priority of effects of the same type."), m_HeaderLabel);
                EditorGUILayout.EndHorizontal();

                int count = 0;
                for (int k = 0; k < list.Count; ++k)
                {
                    effect = list[k];
                    if (effect == null)
                        continue;

                    if (!((data.renderPostProcessingCulling.value & 1 << effect.gameObject.layer) != 0))
                        continue;

                    EditorGUILayout.BeginHorizontal();

                    GUI.color = count == 0 ? Color.white : Color.red;
                    EditorGUILayout.ObjectField(effect, typeof(Object), true, new GUILayoutOption[] { GUILayout.Width(effect_col1) });
                    ++count;

                    effect.gameObject.layer = EditorGUILayout.LayerField(effect.gameObject.layer, new GUILayoutOption[] { GUILayout.Width(effect_col2) });
                    effect.class_priority = EditorGUILayout.IntField(effect.class_priority);
                    //if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
                    //    GUI.FocusControl(null);
                    EditorGUILayout.EndHorizontal();
                    GUI.color = Color.white;

                    if (draw_line)
                    {
                        Rect rect = GUILayoutUtility.GetLastRect();
                        rect.y += 8.0f;
                        Handles.color = Color.green;
                        Handles.DrawLines(new Vector3[] { new Vector3(38.0f, rect.y, 0.0f), new Vector3(46.0f, rect.y, 0.0f) });
                        Handles.color = Color.white;
                        effect_y_end = rect.y;
                    }
                }
                EditorGUI.indentLevel -= 1;

                if (draw_line)
                {
                    Handles.color = Color.green;
                    Handles.DrawLines(new Vector3[] { new Vector3(38.0f, effect_y_start, 0.0f), new Vector3(38.0f, effect_y_end, 0.0f) });
                    Handles.color = Color.white;
                }
            }
            EditorGUI.indentLevel -= 2;

            if (draw_line && need_draw)
                Handles.DrawLines(new Vector3[] { new Vector3(25.0f, camera_y_start, 0.0f), new Vector3(25.0f, camera_y_end, 0.0f) });
        }

        EditorGUILayout.EndScrollView();

        //Handles.DrawAAPolyLine(new Vector3[] { new Vector3(100.0f, 0.0f, 0.0f), new Vector3(100.0f, rect.y, 0.0f) });

/*
        m_vScroll = EditorGUILayout.BeginScrollView(m_vScroll);
        foreach (GUIStyle style in GUI.skin.customStyles)
        {
            //if (style.name.ToLower().Contains())
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(style.name);
            EditorGUILayout.LabelField("AAAAbbbbccc", style);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
*/
    }

    bool HaveEffect(List<AEffect> list, int mask)
    {
        AEffect effect;
        for (int i = 0; i < list.Count; ++i)
        {
            effect = list[i];
            if (effect == null)
                continue;

            if ((mask & 1 << effect.gameObject.layer) != 0)
                return true;
        }
        return false;
    }
}