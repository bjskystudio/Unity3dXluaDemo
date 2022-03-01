using UnityEngine.Rendering;
using UnityEditor.Rendering.Universal.Internal;

namespace UnityEditor.Rendering.Universal
{
    internal static class CNewRendererFeatureDropdownItem
    {
        static readonly string defaultNewClassName = "CCustomRendererFeature.cs";

        [MenuItem("Assets/Create/Rendering/NPR Pipeline/Renderer Feature", priority = CoreUtils.assetCreateMenuPriority2)]
        internal static void CreateNewRendererFeature()
        {
            string templatePath = AssetDatabase.GUIDToAssetPath(CResourceGuid.rendererTemplate);
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, defaultNewClassName);
        }
    }
}
