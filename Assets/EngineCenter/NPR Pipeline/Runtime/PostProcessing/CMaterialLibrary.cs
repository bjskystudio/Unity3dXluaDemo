using UnityEngine;
using UnityEngine.Rendering;

namespace EngineCenter.NPRPipeline.PostProcessing
{
    public class CMaterialLibrary
    {
        public readonly Material invertColor;
        public readonly Material grayScale;
        public readonly Material radialBlur;
        public readonly Material sunShafts;

        public CMaterialLibrary()
        {
            invertColor = Load(Shader.Find("Hidden/PostProcessing/InvertColor"));
            grayScale = Load(Shader.Find("Hidden/PostProcessing/Grayscale"));
            radialBlur = Load(Shader.Find("Hidden/PostProcessing/RadialBlur"));
            sunShafts = Load(Shader.Find("Hidden/PostProcessing/SunShafts"));
        }

        Material Load(Shader shader)
        {
            if (shader == null)
            {
                Debug.LogErrorFormat($"Missing shader. {GetType().DeclaringType.Name} render pass will not execute. Check for missing reference in the renderer resources.");
                return null;
            }
            else if (!shader.isSupported)
            {
                return null;
            }

            return CoreUtils.CreateEngineMaterial(shader);
        }

        internal void Cleanup()
        {
            CoreUtils.Destroy(invertColor);
            CoreUtils.Destroy(grayScale);
            CoreUtils.Destroy(radialBlur);
            CoreUtils.Destroy(sunShafts);
        }
    }
}