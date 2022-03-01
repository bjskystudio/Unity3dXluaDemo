using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class CameraExtension
{
    /// <summary>
    /// 获取指定Camera对象上的RendererFeature
    /// 由于unity没有提供方法，所以这里使用了反射的方式获取，后续unity支持后，这里可以取消
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static T GetRendererFeature<T>(this Camera camera) where T : AScriptableRendererFeature
    {
        CAdditionalCameraData cameraData = camera.GetComponent<CAdditionalCameraData>();
        if(cameraData == null)
        {
            return null;
        }

        PropertyInfo fi = cameraData.scriptableRenderer.GetType().GetProperty("rendererFeatures", BindingFlags.Instance | BindingFlags.NonPublic);
        if(fi == null)
        {
            return null;
        }
        List<AScriptableRendererFeature> featureList = (List<AScriptableRendererFeature>)fi.GetValue(cameraData.scriptableRenderer);
        for(int i = 0; i < featureList.Count; i++)
        {
            if(featureList[i] is T)
            {
                return featureList[i] as T;
            }
        }

        return null;
    }

    public static AScriptableRendererFeature GetRendererFeature(this Camera camera, Type type)
    {
        CAdditionalCameraData cameraData = camera.GetComponent<CAdditionalCameraData>();
        if (cameraData == null)
        {
            return null;
        }

        PropertyInfo fi = cameraData.scriptableRenderer.GetType().GetProperty("rendererFeatures", BindingFlags.Instance | BindingFlags.NonPublic);
        if (fi == null)
        {
            return null;
        }
        List<AScriptableRendererFeature> featureList = (List<AScriptableRendererFeature>)fi.GetValue(cameraData.scriptableRenderer);
        for (int i = 0; i < featureList.Count; i++)
        {
            if (featureList[i].GetType() == type)
            {
                return featureList[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 获取摄像机得4条射线，一般配合CametaDepthTexture反推世界坐标
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="topLeftDir"></param>
    /// <param name="topRightDir"></param>
    /// <param name="bottomLeftDir"></param>
    /// <param name="bottomRightDir"></param>
    /// <param name="ZDepth">代表了距离摄像机的Z距离</param>
    public static void GetViewDir(this Camera camera, out Vector3 topLeftDir, out Vector3 topRightDir, out Vector3 bottomLeftDir, out Vector3 bottomRightDir, float ZDepth = 1.0f)
    {
        float halfTheta = Mathf.Deg2Rad * camera.fieldOfView / 2;
        float halfHeight = camera.nearClipPlane * Mathf.Tan(halfTheta);
        float halfWidth = halfHeight * camera.aspect;
        Vector3 top = camera.transform.up * halfHeight;
        Vector3 right = camera.transform.right * halfWidth;

        topLeftDir = (camera.transform.forward * camera.nearClipPlane - right + top) / camera.nearClipPlane * ZDepth;
        topRightDir = (camera.transform.forward * camera.nearClipPlane + right + top) / camera.nearClipPlane * ZDepth;
        bottomLeftDir = (camera.transform.forward * camera.nearClipPlane - right - top) / camera.nearClipPlane * ZDepth;
        bottomRightDir = (camera.transform.forward * camera.nearClipPlane + right - top) / camera.nearClipPlane * ZDepth;
    }

    public static Vector4[] GetViewDir(this Camera camera, float ZDepth = 1.0f)
    {
        float halfTheta = Mathf.Deg2Rad * camera.fieldOfView / 2;
        float halfHeight = camera.nearClipPlane * Mathf.Tan(halfTheta);
        float halfWidth = halfHeight * camera.aspect;
        Vector3 top = camera.transform.up * halfHeight;
        Vector3 right = camera.transform.right * halfWidth;

        Vector3 topLeftDir = (camera.transform.forward * camera.nearClipPlane - right + top) / camera.nearClipPlane * ZDepth;
        Vector3 topRightDir = (camera.transform.forward * camera.nearClipPlane + right + top) / camera.nearClipPlane * ZDepth;
        Vector3 bottomLeftDir = (camera.transform.forward * camera.nearClipPlane - right - top) / camera.nearClipPlane * ZDepth;
        Vector3 bottomRightDir = (camera.transform.forward * camera.nearClipPlane + right - top) / camera.nearClipPlane * ZDepth;

        return new Vector4[4] { bottomLeftDir, bottomRightDir, topLeftDir, topRightDir};
    }
}