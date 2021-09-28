#if UNITY_EDITOR
using System.Collections;

namespace UnityEditor.Rendering.Universal
{
    internal static class CSceneViewDrawMode
    {
#if !HOTFIX_ENABLE
        static bool RejectDrawMode(SceneView.CameraMode cameraMode)
        {
            if (cameraMode.drawMode == DrawCameraMode.TexturedWire ||
                cameraMode.drawMode == DrawCameraMode.ShadowCascades ||
                cameraMode.drawMode == DrawCameraMode.RenderPaths ||
                cameraMode.drawMode == DrawCameraMode.AlphaChannel ||
                cameraMode.drawMode == DrawCameraMode.Overdraw ||
                cameraMode.drawMode == DrawCameraMode.Mipmaps ||
                cameraMode.drawMode == DrawCameraMode.SpriteMask ||
                cameraMode.drawMode == DrawCameraMode.DeferredDiffuse ||
                cameraMode.drawMode == DrawCameraMode.DeferredSpecular ||
                cameraMode.drawMode == DrawCameraMode.DeferredSmoothness ||
                cameraMode.drawMode == DrawCameraMode.DeferredNormal ||
                cameraMode.drawMode == DrawCameraMode.ValidateAlbedo ||
                cameraMode.drawMode == DrawCameraMode.ValidateMetalSpecular ||
                cameraMode.drawMode == DrawCameraMode.ShadowMasks ||
                cameraMode.drawMode == DrawCameraMode.LightOverlap
            )
                return false;

            return true;
        }
#endif

        public static void SetupDrawMode()
        {
#if !HOTFIX_ENABLE
            ArrayList sceneViewArray = SceneView.sceneViews;
            foreach (SceneView sceneView in sceneViewArray)
                sceneView.onValidateCameraMode += RejectDrawMode;
#endif
        }

        public static void ResetDrawMode()
        {
#if !HOTFIX_ENABLE
            ArrayList sceneViewArray = SceneView.sceneViews;
            foreach (SceneView sceneView in sceneViewArray)
                sceneView.onValidateCameraMode -= RejectDrawMode;
#endif
        }

    }
}
#endif
