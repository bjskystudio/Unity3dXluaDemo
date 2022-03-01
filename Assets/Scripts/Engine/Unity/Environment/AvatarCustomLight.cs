using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using YoukiaCore;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class AvatarCustomLight : MonoBehaviour
{
    public bool IFChangeMainLightRotation = false;
    public bool IFUseSceneLightColor = false;
    public CustomLightType LightType = CustomLightType.Direction;
    int CircleLineCount = 200;
    #region 平行光绘制参数
    int lineCount = 13;
    float circleRange = 0.03f;
    float lineLength = 0.17f;
    #endregion

    #region 聚光灯绘制参数

    #endregion
    public float Range = 1.0f;
    [Range(0.01f, 1.0f)]
    public float SpotAngle = 1.0f;
    [Range(0.01f, 10.0f)]
    public float LightIntensity = 0.01f;
    public Color lightColor;
    Color lineColor;
    //距离衰减
    public float distanceReduction;
    public float angleReduction;
    [HideInInspector]
    public bool ISUseable = true;
    Action DrawFunc;

    private void OnEnable()
    {
        CustomLightManager.Instance.Add(this);
    }
    private void OnDisable()
    {
        CustomLightManager.Instance.Remove(this);
    }
    private void OnDestroy()
    {
        CustomLightManager.Instance.Remove(this);
    }
    public void Update()
    {
        CustomLightManager.Instance.UpdateLightInfo();
        if (ISUseable==false)
        {
            return;
        }

        if (this.IFChangeMainLightRotation && CustomLightManager.Instance.MainLight!=null)
        {

            CustomLightManager.Instance.ChangeMainLightRotation(this);
        }

        //Debug.Log(lightColor);

        if (IFUseSceneLightColor)
        {
            //GetOriginalColor
            Shader.SetGlobalColor("_CustomAvatarLightColor", CustomLightManager.Instance.GetOriginalColor());
        }
        else
        {
            Shader.SetGlobalColor("_CustomAvatarLightColor", lightColor * LightIntensity);
        }

        Shader.SetGlobalVector("_CustomAvatarLightVec", new Vector4(transform.forward.x, transform.forward.y, transform.forward.z, 0));
    }



    float CalculatorSpotLightColorFactor()
    {
        //float dis = (render.transform.position - transform.position).magnitude;
        //if (dis > Range)
        //{
        //    return 0;
        //}

        //float angle = Vector3.Dot((render.transform.position-transform.position).normalized,transform.forward);
        //if (Math.Acos(angle) > SpotAngle)
        //{
        //    return 0;
        //}

        //double factor = 1 - Math.Log10((double)dis * 10 / Range + 1);
        //factor *= angle;
        //return (float)factor;
        return 1;
    }
    float CalculatorPointLightColorFactor()
    {
        return 1;
       // float dis = (render.transform.position - transform.position).magnitude;
       // if (dis>Range)
       // {
       //     return 0;
       // }

       //double factor= 1- Math.Log10((double)dis*10/Range+1);
       // return (float)factor;
    }
    private float scaleFactor;
#if UNITY_EDITOR
    //public void OnValidate()
    //{
    //    Update();
    //    SceneView.RepaintAll();
    //}
    public void UpdateInfo()
    {
        OnDrawGizmos();
    }


    private void OnDrawGizmos()
    {
        lineColor = lightColor;
        lineColor.a = 1;
        Range = Range <= 0 ? 0 : Range;
        float lightFactor = 1.0f;
        switch (LightType)
        {
            case CustomLightType.Direction:
                DrawFunc = DrawDirection;
                lightFactor = 1.0f;
                break;
            case CustomLightType.Point:
                DrawFunc = DrawPointLight;
                lightFactor = CalculatorPointLightColorFactor();
                break;
            case CustomLightType.Spot:
                DrawFunc = DrawSpotLight;
                lightFactor = CalculatorSpotLightColorFactor();
                break;
        }


        scaleFactor = Vector3.Magnitude(SceneView.lastActiveSceneView.camera.transform.position - transform.position);
        scaleFactor = scaleFactor <= 0 ? 0.5f : scaleFactor;
        if (DrawFunc != null)
        {
            DrawFunc();
        }
    }

    private void DrawDirection()
    {
        //Debug.Log(SceneView.lastActiveSceneView.camera.transform.position);
        Vector3 fromPos = Vector3.zero;
        Vector3 toPos = Vector3.zero;
        Gizmos.color = lineColor;
        for (int i = 0; i < lineCount; i++)
        {
            fromPos = transform.position + scaleFactor * circleRange * transform.up * Mathf.Cos(i) + scaleFactor * circleRange * transform.right * Mathf.Sin(i);
            Gizmos.DrawLine(fromPos, fromPos + scaleFactor * lineLength * transform.forward);
        }
        Gizmos.DrawWireSphere(transform.position, scaleFactor * circleRange);
    }

    private void DrawSpotLight()
    {
        Matrix4x4 object2world = transform.localToWorldMatrix;
        Vector3 fromPos = transform.position;
        Vector3 toPos0 =new Vector3(0,0,1.0f)* Range;
        Gizmos.DrawLine(fromPos, fromPos + transform.forward* Range);
        Matrix4x4 max1 = new Matrix4x4(
            new Vector4((float)Math.Cos(SpotAngle),0,(float)Math.Sin(SpotAngle),0),
            new Vector4(0,1,0,0),
            new Vector4((float)Math.Sin(SpotAngle)*-1,0,(float)Math.Cos(SpotAngle),0),
            new Vector4(0,0,0,0));
        Matrix4x4 max2 = new Matrix4x4(
            new Vector4((float)Math.Cos(-1*SpotAngle), 0, (float)Math.Sin(-1 * SpotAngle), 0),
            new Vector4(0, 1, 0, 0),
            new Vector4((float)Math.Sin(-1 * SpotAngle) * -1, 0, (float)Math.Cos(-1 * SpotAngle), 0),
            new Vector4(0, 0, 0, 0));
        Matrix4x4 max3 = new Matrix4x4(
            new Vector4(1,0,0,0),
            new Vector4(0, (float)Math.Cos(SpotAngle), (float)Math.Sin(SpotAngle), 0),
            new Vector4(0, (float)Math.Sin(SpotAngle) * -1, (float)Math.Cos(SpotAngle), 0),
            new Vector4(0, 0, 0,0));
        Matrix4x4 max4 = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, (float)Math.Cos(-1 * SpotAngle), (float)Math.Sin(-1 * SpotAngle), 0),
            new Vector4(0, (float)Math.Sin(-1 * SpotAngle) * -1, (float)Math.Cos(-1 * SpotAngle), 0),
            new Vector4(0, 0, 0, 0));
        Vector3 edgeTemp = toPos0/(float)Math.Cos(SpotAngle);
        Vector3 localPos1 = max1.MultiplyVector(edgeTemp);
        Vector3 localPos2 = max2.MultiplyVector(edgeTemp);
        Vector3 localPos3 = max3.MultiplyVector(edgeTemp);
        Vector3 localPos4 = max4.MultiplyVector(edgeTemp);
        //在世界坐标系中四个线段终点的坐标
        Vector3 toPos1 =  object2world.MultiplyVector(localPos1) ;
        Vector3 toPos2 =object2world.MultiplyVector(localPos2);
        Vector3 toPos3 =object2world.MultiplyVector(localPos3);
        Vector3 toPos4 = object2world.MultiplyVector(localPos4);
        Gizmos.color = lineColor;
        Gizmos.DrawLine(fromPos, transform.position+ toPos1);
        Gizmos.DrawLine(fromPos, transform.position + toPos2);
        Gizmos.DrawLine(fromPos, transform.position + toPos3);
        Gizmos.DrawLine(fromPos, transform.position + toPos4);

        Vector3 tempFromPos = localPos3;
        Vector3 tempToPos = localPos1;

        Matrix4x4 local2Circle = new Matrix4x4();
        local2Circle = Matrix4x4.zero;
        local2Circle.m00 = 1;
        local2Circle.m11 = 1;
        local2Circle.m22 = 1;
        local2Circle.m33 = 1;
        local2Circle.m23 = localPos1.z*-1;
        //Matrix4x4 circle2local= new Matrix4x4();
        //circle2local = Matrix4x4.zero;
        //circle2local.m00 = 1;
        //circle2local.m11 = 1;
        //circle2local.m22 =1;
        //circle2local.m33 = 1;
        //circle2local.m23 = localPos1.z;
        tempFromPos = local2Circle.inverse.MultiplyVector(tempFromPos);
        for (int i=0;i< CircleLineCount*4;i++)
        {
            
            int index =(i+1)%(CircleLineCount);
            float factor = ((float) index) / ((float)CircleLineCount);
            float oldz = localPos1.z;
            
            localPos1 = local2Circle.MultiplyPoint(localPos1);
            localPos2 = local2Circle.MultiplyPoint(localPos2);
            localPos3 = local2Circle.MultiplyPoint(localPos3);
            localPos4 = local2Circle.MultiplyPoint(localPos4);
            //localPos1.z = 0;
            //localPos2.z = 0;
            //localPos3.z = 0;
            //localPos4.z = 0;
            if (i< CircleLineCount)
            {
                tempToPos = Vector3.Slerp(localPos1, localPos3, factor); //localPos1 * factor + (1 - factor) * localPos3;
            }
            else if (i < CircleLineCount * 2)
            {
                tempToPos = Vector3.Slerp(localPos3, localPos2, factor); // localPos3 * factor + (1 - factor) * localPos2;
            }
            else if (i < CircleLineCount * 3)
            {
                tempToPos = Vector3.Slerp(localPos2, localPos4, factor); // localPos2 * factor + (1 - factor) * localPos4;
            }
            else if (i < CircleLineCount * 4)
            {
                tempToPos = Vector3.Slerp(localPos4, localPos1, factor); //localPos4 * factor + (1 - factor) * localPos1;
            }
            //tempFromPos = circle2local.MultiplyVector(tempFromPos);
            tempToPos = local2Circle.inverse.MultiplyPoint(tempToPos);
            localPos1 = local2Circle.inverse.MultiplyPoint(localPos1);
            localPos2 = local2Circle.inverse.MultiplyPoint(localPos2);
            localPos3 = local2Circle.inverse.MultiplyPoint(localPos3);
            localPos4 = local2Circle.inverse.MultiplyPoint(localPos4);
            //tempToPos.z = oldz;
            //localPos1.z = oldz;
            //localPos2.z =oldz;
            //localPos3.z =oldz;
            //localPos4.z = oldz;
            Gizmos.DrawLine(object2world.MultiplyVector(tempFromPos)+ fromPos, object2world.MultiplyVector(tempToPos) + fromPos);
            tempFromPos = tempToPos;
        }

        //Gizmos.DrawLine(toPos1, toPos4);
        //Gizmos.DrawLine(toPos1, toPos3);
        //Gizmos.DrawLine(toPos1, toPos2);
        //Gizmos.DrawLine(toPos2, toPos3);
        //Gizmos.DrawLine(toPos2, toPos4);
        //Gizmos.DrawLine(toPos3, toPos4);
    }
    private void DrawPointLight()
    {
        Gizmos.color = lineColor;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
#endif
}
public enum CustomLightType
{
    Direction,
    Spot,
    Point,
}

public class CustomLightManager
{
    private static CustomLightManager instance;

    public static CustomLightManager Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new CustomLightManager();
            }

            return instance;
        }
    }
    CommandBuffer cb = new CommandBuffer();
    private AvatarCustomLight currentAvatarCustomLight;

    //1.自定义组件更新时如果没有缓存过主光源方向，且需要修改主光源方向，则需要缓存一次方向
    //2.当没有组件有修改主光源方向方向时，且有缓存光源方向，则需要覆盖一次主光源方向
    public void UpdateLightInfo()
    {
        if (currentAvatarCustomLight!=null && currentAvatarCustomLight.IFChangeMainLightRotation && originalSunLightRotation== Quaternion.identity)
        {
            if (MainLight!=null)
            {
                originalSunLightRotation = MainLight.transform.rotation;
            }
            else
            {
                //Debug.Log("当前场景有自定义光照组件生效，但是没有原生光照组件生效，这种情况可能导致自定义光照组件无法还原原生光照组件的方向");
            }
        }else if ((currentAvatarCustomLight==null || currentAvatarCustomLight.IFChangeMainLightRotation==false)&& originalSunLightRotation!= Quaternion.identity)
        {
            if (MainLight != null)
            {
                MainLight.transform.rotation=originalSunLightRotation;
                originalSunLightRotation = Quaternion.identity;
            }
            else
            {
                //Debug.Log("当前场景有自定义光照组件生效，但是没有原生光照组件生效，这种情况可能导致自定义光照组件无法还原原生光照组件的方向");
            }
        }

        //for (int i = 0; i < customLightList.Count; i++)
        //{
        //    if (i == customLightList.Count - 1)
        //    {
        //        customLightList[i].ISUseable = true;
        //    }
        //    else
        //    {
        //        customLightList[i].ISUseable = false;
        //    }
        //}
    }

    public Light MainLight
    {
        get
        {
            Light[] lightList = Light.GetLights(LightType.Directional, 0);
            if (lightList.Length>0)
            {
                return lightList[0];
            }
            else
            {
                Debug.Log("当前场景没有主光源");
                return null;
            }
        }
    }
    private static Quaternion playingOriginalRotation = Quaternion.identity;
    private static Quaternion notPlayingOriginalRotation = Quaternion.identity;

    private Quaternion originalSunLightRotation
    {
        get
        {
            if (Application.isPlaying)
            {
                return playingOriginalRotation;
            }
            else
            {
                return notPlayingOriginalRotation;
            }
        }
        set
        {
            if (Application.isPlaying)
            {
                 playingOriginalRotation=value;
            }
            else
            {
                 notPlayingOriginalRotation = value;
            }
        }
    }

    public AvatarCustomLight CLight
    {
        get { return currentAvatarCustomLight; }
    }

    public void ChangeMainLightRotation(AvatarCustomLight clight)
    {
        MainLight.transform.rotation = clight.transform.rotation;
    }

    private List<AvatarCustomLight> customLightList = new List<AvatarCustomLight>();
    public Color GetOriginalColor()
    {
        Color result = currentAvatarCustomLight.lightColor * currentAvatarCustomLight.LightIntensity;
        bool originalIsCLight = false;
        for (int i= customLightList.Count-1; i>=0;i--)
        {
            if (customLightList[i].IFUseSceneLightColor==false && customLightList[i]!=currentAvatarCustomLight)
            {
                result = customLightList[i].lightColor * customLightList[i].LightIntensity;
                originalIsCLight = true;
                break;
            }
        }

        if (originalIsCLight==false)
        {
            Light[] lightList = Light.GetLights(LightType.Directional,0);
            if (lightList.Length>0)
            {
                result = lightList[0].color * lightList[0].intensity;
            }
        }

        return result;
    }
    public void ResetMainLightState()
    {
        if (MainLight!=null)
        {
            MainLight.transform.rotation = originalSunLightRotation;
        }
    }

    public void Add(AvatarCustomLight newLight)
    {
        if (customLightList.Contains(newLight)==false)
        {
            customLightList.Add(newLight);
            UpdateLightListState();
        }
        UpdateLightInfo();
        //else
        //{
        //    Debug.Log("有试图重复生效自定义光照组件");
        //}
    }
    public void Remove(AvatarCustomLight newLight)
    {
        if (customLightList.Contains(newLight) == true)
        {
            customLightList.Remove(newLight);
            UpdateLightListState();
            //ResetMainLightState();
        }
        UpdateLightInfo();
        //else
        //{
        //    Debug.Log("有试图删除一个未储存的自定义光照组件");
        //}
    }
    private void UpdateLightListState()
    {
        for(int i=0;i< customLightList.Count;i++)
        {
            if (i== customLightList.Count-1)
            {
                customLightList[i].ISUseable = true;
            }
            else
            {
                customLightList[i].ISUseable = false;
            }
        }

        if (customLightList.Count>0)
        {
            currentAvatarCustomLight = customLightList[customLightList.Count - 1];
            Shader.EnableKeyword("CUSTOMLIGHT_ON");
            //Debug.Log("自定义光照组件："+ currentAvatarCustomLight.gameObject.name+"  生效");
        }
        else
        {
            currentAvatarCustomLight = null;
            //Debug.Log("自定义光照组件都失效了");
            Shader.DisableKeyword("CUSTOMLIGHT_ON");
        }
    }

    public bool CheckIfOnly(AvatarCustomLight ac)
    {
        if (currentAvatarCustomLight==null)
        {
            currentAvatarCustomLight = ac;
            //cb.EnableShaderKeyword("CUSTOM_LIGHT");
            //cb.DisableShaderKeyword("CUSTOMLIGHT_OFF");
            Shader.EnableKeyword("CUSTOMLIGHT_ON");
            return true;
        }else if (currentAvatarCustomLight!=null && currentAvatarCustomLight!=ac)
        {
            Debug.LogError(currentAvatarCustomLight.gameObject.name + "已拥有AvatarCustomLight组件");
            return false;
        }
        return true;
    }

    public void OnCustomLightDestroy(AvatarCustomLight ac)
    {
        if (currentAvatarCustomLight == ac)
        {
            currentAvatarCustomLight = null;

            //cb.DisableShaderKeyword("CUSTOM_LIGHT");
            //cb.EnableShaderKeyword("CUSTOMLIGHT_OFF");
            Shader.DisableKeyword("CUSTOMLIGHT_ON");
        }
    }
}
