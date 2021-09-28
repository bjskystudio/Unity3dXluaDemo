using UnityEngine.Rendering.Universal;

namespace UnityEditor.Rendering.Universal
{
    [CanEditMultipleObjects]
    // Disable the GUI for additional camera data
    [CustomEditor(typeof(CAdditionalCameraData))]
    class CAdditionalCameraDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
        }
        [MenuItem("CONTEXT/CAdditionalCameraData/Remove Component")]
        static void RemoveComponent(MenuCommand command)
        {
            EditorUtility.DisplayDialog("Component Info", "You can not delete this component, you will have to remove the camera.", "OK");
        }
    }
}
