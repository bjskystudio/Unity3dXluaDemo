using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using YoukiaEngine;

namespace YoukiaEngineEditor
{
	[CustomEditor(typeof(YoukiaEngine.GeometryInstancingManager))]
	class GeomertyInstancingGroupEditor : Editor
	{
		private SerializedProperty m_objCount;
		private SerializedProperty m_visibleCount;

		private void OnEnable()
		{
			m_objCount = serializedObject.FindProperty("ObjectCount");
			m_visibleCount = serializedObject.FindProperty("VisibleObjectCount");
		}

		public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

			EditorGUILayout.PropertyField(m_objCount);
			EditorGUILayout.PropertyField(m_visibleCount);

			EditorGUILayout.Space();

			EditorGUILayout.BeginVertical(GUI.skin.box);

		    GeometryInstancingManager manager = GeometryInstancingManager.Instance;

 
		    int count = GeometryInstancingManager.Instance.GroupCount;

            EditorGUILayout.IntField("Group Count", count);
			for (int i = 0; i < count; i++)
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);

			    GeometryInstancingGroup gi = manager.GetGroup(i);
                if (gi is ImpostorInstancingGroup g)
				{
					EditorGUILayout.TextField("Impostor Group " + i + " #visible obj: " + g.VisibleObjectCount + " #mesh: " + g.Mesh.name);
					EditorGUI.indentLevel++;
					for (int j = 0; j < g.Batchs.Count; j++)
						EditorGUILayout.TextField("Batch " + j + " draw count: " + g.Batchs[j].DrawCount);
					EditorGUILayout.Space();
					for (int j = 0; j < g.BatchsImpostor.Count; j++)
						EditorGUILayout.TextField("BatchImpostor " + j + " draw count: " + g.BatchsImpostor[j].DrawCount);

					EditorGUI.indentLevel--;
				}
				else
				{
					GeometryInstancingGroup geo = gi as GeometryInstancingGroup;
					EditorGUILayout.TextField("Group " + i + " #visible obj: " + geo.VisibleObjectCount + " #mesh: " + geo.Mesh.name);
					EditorGUI.indentLevel++;

					for (int j = 0; j < geo.Batchs.Count; j++)
						EditorGUILayout.TextField("Batch " + j + " draw count: " + geo.Batchs[j].DrawCount);

					EditorGUI.indentLevel--;
				}
				
				EditorGUILayout.Space();
				
				EditorGUILayout.EndVertical();
				EditorGUILayout.Space();
			}

			EditorGUILayout.EndVertical();

			this.serializedObject.ApplyModifiedProperties();
		}
	}
}
