using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class GlintingAlone : MonoBehaviour
{
    private MaterialPropertyBlock matPropertyBlock;
    public MaterialPropertyBlock matPB {
        get
        {
            if (matPropertyBlock==null)
            {
                matPropertyBlock = new MaterialPropertyBlock();
            }

            return matPropertyBlock;
        }
    }
    public bool glintingColorWork = false;

    public Color color = new Color(1, 1, 1, 0.5f);

    [Range(0.0f, 1.0f)]
    public float minBrightness = 1.0f;

    [Range(0.0f, 2.0f)]
    public float offsetBrightness = 0.1f;

    [Range(0.0001f,0.1f)]
    public float changeSpeed = 0.01f;
    //[Range(0.2f, 30.0f)]
    private float factor = 1;

    private MeshRenderer _renderer;
    private Material _material=null;

    Material renderMat {
        get
        {
            if (_material==null)
            {
                if (_renderer == null)
                {
                    _renderer = gameObject.GetComponent<MeshRenderer>();
                }
                _material = _renderer.sharedMaterial;
            }
            return _material;
        }
    }

    private int factorTemp =1;
//    private void OnRenderObject()
//    {
//#if UNITY_EDITOR
//        if (Application.isPlaying == false)
//        {
//            UpdateMat();
//        }
//#endif
//    }

    private void FixedUpdate()
    {
        UpdateMat();
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

            matPB.SetFloat("_glintingFactor", glintingColorWork ? (1 - factor) : (1 - factor * 10));
            matPB.SetFloat("_glintingColorFactor", glintingColorWork ? 1.0f : 0.0f); //_glintingBlandFactor
            matPB.SetVector("_glintingColor", new Vector4(color.r, color.g, color.b, color.a));
            GetComponent<MeshRenderer>().SetPropertyBlock(matPB);
            //Debug.Log("UpdateMat->" + factor);
//#if UNITY_EDITOR
//            if (Application.isPlaying == false)
//            {
//                SceneView.RepaintAll();
//            }
//#endif
        }
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        EditorApplication.update += UpdateMat;
#endif
    }
    private void OnDisable()
    {
        matPB.SetFloat("_glintingFactor",0);
        matPB.SetFloat("_glintingColorFactor", 0); 
        GetComponent<MeshRenderer>().SetPropertyBlock(null);
#if UNITY_EDITOR
        EditorApplication.update -= UpdateMat;
#endif
    }
}