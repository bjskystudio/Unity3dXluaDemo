using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class Glinting : MonoBehaviour
{
    public bool glintingColorWork = false;
    /// <summary>
    /// 闪烁颜色
    /// </summary>
    public Color color = new Color(1, 1, 1, 0.5f);
    /// <summary>
    /// 最低发光亮度，取值范围[0,1]，需小于最高发光亮度。
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float minBrightness = 1.0f;
    /// <summary>
    /// 最高发光亮度，取值范围[0,1]，需大于最低发光亮度。
    /// </summary>
    [Range(0.0f, 2.0f)]
    public float offsetBrightness = 1.1f;


    /// <summary>
    /// 闪烁频率，取值范围[0.2,30.0]。
    /// </summary>
    [Range(0.0001f,0.01f)]
    public float changeSpeed = 0.001f;
    //[Range(0.2f, 30.0f)]
    private float factor = 1;

    private MeshRenderer _renderer;
    private Material _material;
    Material renderMat {
        get
        {
            if (_material==null)
            {
                _material =_renderer.sharedMaterial;
            }
            return _material;
        }
    }

    private void Start()
    {
        _renderer = gameObject.GetComponent<MeshRenderer>();
        _material = _renderer.sharedMaterial;
    }

    private int factorTemp =1;
    private void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == true)
        {
            UpdateMat();
        }
#else
 UpdateMat();
#endif

    }
    private void OnRenderObject()
    {
#if UNITY_EDITOR
        if (Application.isPlaying==false)
        {
            UpdateMat();
        }
#endif
    }
    void UpdateMat()
    {
        if (renderMat != null)
        {

            factor += factorTemp * changeSpeed;
            if (factor <= minBrightness || factor >= minBrightness + offsetBrightness)
            {
                factorTemp *= -1;
                factor = factor <= minBrightness ? minBrightness : factor;
                factor = factor >= minBrightness + offsetBrightness ? minBrightness + offsetBrightness : factor;
            }
            renderMat.SetFloat("_glintingFactor", glintingColorWork ? (1- factor) : (1- factor * 10));//_glintingBlandFactor
            renderMat.SetFloat("_glintingColorFactor", glintingColorWork ? 1.0f : 0.0f);//_glintingBlandFactor
            renderMat.SetVector("_glintingColor", new Vector4(color.r, color.g, color.b, color.a));
//#if UNITY_EDITOR
//            SceneView.RepaintAll();
//            Debug.Log("更新了材质球->"+gameObject.name);
//#endif
        }
    }
    private void OnDisable()
    {
        if (_material != null)
        {
            _material.SetFloat("_glintingFactor", 0.0f);
            _material.SetVector("_glintingColor", Vector4.zero);
        }
    }
}