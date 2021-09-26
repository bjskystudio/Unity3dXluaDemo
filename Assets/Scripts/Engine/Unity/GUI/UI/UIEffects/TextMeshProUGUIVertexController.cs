using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[ExecuteInEditMode]
public class TextMeshProUGUIVertexController : MonoBehaviour
{
    public float XOffsetFactor = 0;
    public float OffsetBeginY = 0;
    float lastFrameXOffsetFactor = 0;
    float lastFrameOffsetBeginY = 0;
    public Material mat2;
    public Material matE2;
    void Start()
    {

    }
    public void SetVertexXOffset(Mesh m_mesh)
    {
        if (m_mesh.vertices != null)
        {
            Vector3[] verticesList = m_mesh.vertices;
            for (int i = 0; i < verticesList.Length; i++)
            {
                verticesList[i].x += XOffsetFactor * (verticesList[i].y - OffsetBeginY);
            }
            m_mesh.vertices = verticesList;
        }
    }
    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI targerUGUI = transform.GetComponent<TextMeshProUGUI>();
        RectTransform rt = transform.GetComponent<RectTransform>();
        if (targerUGUI != null)
        {
            if (lastFrameOffsetBeginY != OffsetBeginY || lastFrameXOffsetFactor != XOffsetFactor)
            {
                targerUGUI.material = mat2;
                TMP_SubMeshUI[] subTextObjects = GetComponentsInChildren<TMP_SubMeshUI>();
                if (subTextObjects.Length > 0)
                {
                    for (int i = 0; i < subTextObjects.Length; i++)
                    {
                        subTextObjects[i].sharedMaterial = matE2;
                        subTextObjects[i].sharedMaterial.SetFloat("_XOffsetFactor", XOffsetFactor);
                        subTextObjects[i].sharedMaterial.SetFloat("_OffsetBeginY", OffsetBeginY + rt.localPosition.y - targerUGUI.fontSize / 2);
                    }

                    
                }
                targerUGUI.material.SetFloat("_XOffsetFactor", XOffsetFactor);
                targerUGUI.material.SetFloat("_OffsetBeginY", OffsetBeginY + rt.localPosition.y - targerUGUI.fontSize / 2);
                //targerUGUI.MeshOperaFunc = SetVertexXOffset;
                //lastFrameOffsetBeginY = OffsetBeginY;
                //lastFrameXOffsetFactor = XOffsetFactor;
                //targerUGUI.PropertiesChanged();
                //targerUGUI.Rebuild(CanvasUpdate.PreRender);
            }
        }
    }
}

