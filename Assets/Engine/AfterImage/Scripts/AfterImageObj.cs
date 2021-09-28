using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageObj
{
    public float LifeTime;
    public GameObject ParentGO;
    public MeshFilter[] ChildMesh;
    public Material[][] Mats;

    public float TimeOut;
    public float Rate;

    public void Init()
    {
        TimeOut = 0;
        Rate = 0;
    }

    public void UpdateMats(float deltaTime, int colorID, int stencilRefID, int index, Gradient col)
    {
        Rate = LifeTime <= 0 ? 1 : TimeOut / LifeTime;
        Rate = Mathf.Clamp01(Rate);
        Color afterCol = Color.white;
        if(col != null)
        {
            afterCol = col.Evaluate(Rate);
        }
        if (Mats != null)
        {
            for (int i = 0; i < Mats.Length; i++)
            {
                for (int j = 0; j < Mats[i].Length; j++)
                {
                    Material mat = Mats[i][j];
                    if (mat != null)
                    {
                        mat.SetColor(colorID, afterCol);
                        mat.SetFloat(stencilRefID, index + 1);
                        mat.renderQueue = 3500 - index;
                    }
                }
            }
        }

        TimeOut += deltaTime;
    }

    public void Recovery(List<AfterImageObj> pool)
    {
        DisposeMeshData();
        ParentGO.SetActive(false);
        if(pool != null)
        {
            pool.Add(this);
        }
    }

    private void DisposeMeshData()
    {
        if (ChildMesh != null)
        {
            for (int i = 0; i < ChildMesh.Length; i++)
            {
                MeshFilter meshFilter = ChildMesh[i];
                Mesh mesh = meshFilter != null ? meshFilter.sharedMesh : null;
                if (mesh != null)
                {
                    meshFilter.sharedMesh = null;
                    mesh.Clear();
                    Object.Destroy(mesh);
                }
            }
        }
    }

    public void DisposeData()
    {
        DisposeMeshData();
        //destory mat
        if(Mats != null)
        {
            for (int i = 0; i < Mats.Length; i++)
            {
                for (int j = 0; j < Mats[i].Length; j++)
                {
                    Material mat = Mats[i][j];
                    if (mat != null)
                    {
                        Object.Destroy(mat);
                    }
                }
            }
            Mats = null;
        }

        //destory GO
        if(ParentGO != null)
        {
            Object.Destroy(ParentGO);
            ParentGO = null;
        }
    }
}
