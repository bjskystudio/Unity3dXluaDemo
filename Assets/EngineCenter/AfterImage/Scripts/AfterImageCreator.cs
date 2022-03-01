using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageCreator : MonoBehaviour
{
    [Header("是否生成残影")]
    public bool IsCreate = true;
    [Header("残影生成间隔")]
    public float IntervalTime = 0.2f;

    [Header("残影生命周期")]
    public float LifeTime = 0.5f;

    [Header("残影色")]
    [GradientUsage(true)]
    public Gradient AfterImageColor;

    //网格数据
    private SkinnedMeshRenderer[] meshRender;
    private Shader mAfterImageShader;
    private int mID_AfterImageColor;
    private int mID_MainTex;
    private int mID_StencilRef;
    private int mID_CullMode;

    private float mTimeOut;
    private GameObject mParent;
    private List<AfterImageObj> mAfterImagesPool = new List<AfterImageObj>();
    private List<AfterImageObj> mActivedAfterImages = new List<AfterImageObj>();

    void Start()
    {
        meshRender = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        mAfterImageShader = Shader.Find("Hidden/AfterImage");
        mID_AfterImageColor = Shader.PropertyToID("_AfterImageColor");
        mID_MainTex = Shader.PropertyToID("_AfterImageMainTex");
        mID_StencilRef = Shader.PropertyToID("_StencilRef");
        mID_CullMode = Shader.PropertyToID("_CullMode");
    }

    private void Update()
    {
        if (meshRender == null || meshRender.Length <= 0 || mAfterImageShader == null) return;
        mTimeOut += Time.deltaTime;
        if (mTimeOut >= IntervalTime)
        {
            mTimeOut -= IntervalTime;
            if (IsCreate)
            {
                CreateAfterImage();
            }        
        }
        UpdateAfterImage();
    }

    private void UpdateAfterImage()
    {
        if(mActivedAfterImages != null && mActivedAfterImages.Count > 0)
        {
            for(int i = mActivedAfterImages.Count - 1; i >= 0; i--)
            {
                AfterImageObj afterImage = mActivedAfterImages[i];
                if (afterImage.Rate >= 1)
                {
                    afterImage.Recovery(mAfterImagesPool);
                    mActivedAfterImages.Remove(afterImage);
                }
            }
        }

        if (mActivedAfterImages != null && mActivedAfterImages.Count > 0)
        {
            for (int i = mActivedAfterImages.Count - 1; i >= 0; i--)
            {
                AfterImageObj afterImage = mActivedAfterImages[i];
                afterImage.UpdateMats(Time.deltaTime, mID_AfterImageColor, mID_StencilRef, i, AfterImageColor);
            }
        }
    }


    public void CreateAfterImage()
    {
        if (meshRender == null || meshRender.Length <= 0 || mAfterImageShader == null)
        {
            return;
        }

        AfterImageObj afterImage = GetAfterImageGO();
        if(afterImage != null)
        {
            mActivedAfterImages.Add(afterImage);
            afterImage.LifeTime = LifeTime;
            for (int i = 0; i < meshRender.Length; i++)
            {
                Mesh mesh = new Mesh();
                meshRender[i].BakeMesh(mesh);
                mesh.name = meshRender[i].name;
                afterImage.ChildMesh[i].sharedMesh = mesh;

                for (int j = 0; j < meshRender[i].sharedMaterials.Length; j++)
                {
                    SetMat(afterImage, i, j);
                }
            }
        }
    }

    /// <summary>
    /// 隐藏所有残影，同时后续不再产生残影
    /// </summary>
    public void HideAllAfterImage()
    {
        IsCreate = false;
        if (mActivedAfterImages != null && mActivedAfterImages.Count > 0)
        {
            for (int i = mActivedAfterImages.Count - 1; i >= 0; i--)
            {
                AfterImageObj afterImage = mActivedAfterImages[i];
                afterImage.Recovery(mAfterImagesPool);
                mActivedAfterImages.Remove(afterImage);
            }
        }
    }

    private void SetMat(AfterImageObj afterImage, int renderIndex, int matIndex)
    {
        if (afterImage == null || afterImage.Mats[renderIndex][matIndex] == null || meshRender == null) return;
        Material mat = afterImage.Mats[renderIndex][matIndex];
        mat.SetTexture(mID_MainTex, meshRender[renderIndex].sharedMaterials[matIndex].GetTexture("_MainTex"));
        if(transform.lossyScale.x * transform.lossyScale.y * transform.lossyScale.z < 0)
        {
            mat.SetFloat(mID_CullMode, 1);
        }
        else
        {
            mat.SetFloat(mID_CullMode, 2);
        }
    }

    private Material[][] CreateMats()
    {
        if (meshRender == null || mAfterImageShader == null) return null;
        Material[][] mats = new Material[meshRender.Length][];
        for(int i = 0; i < meshRender.Length; i++)
        {
            Material[] matArr = new Material[meshRender[i].sharedMaterials.Length];
            mats[i] = matArr;
            for(int j = 0; j < matArr.Length; j++)
            {
                Material mat = new Material(mAfterImageShader);
                matArr[j] = mat;
            }
        }
        return mats;
    }

    private AfterImageObj GetAfterImageGO()
    {
        if (meshRender == null || meshRender.Length <= 0 || mAfterImageShader == null)
        {
            return null;
        }
        if(mParent == null)
        {
            mParent = new GameObject(gameObject.name + "_AfterImages");
        }
        AfterImageObj ai;
        if(mAfterImagesPool.Count <= 0)
        {
            ai = new AfterImageObj();
            GameObject parent = new GameObject(gameObject.name + "_AfterImage");
            parent.transform.SetParent(mParent.transform);
            ai.ParentGO = parent;

            MeshFilter[] meshes = new MeshFilter[meshRender.Length];
            Material[][] mats = CreateMats();
            ai.ChildMesh = meshes;
            ai.Mats = mats;

            for (int i = 0; i < meshRender.Length; i++)
            {
                GameObject renderGO;
                MeshFilter filter;
                MeshRenderer render;

                renderGO = new GameObject("AfterImage_" + i);
                renderGO.transform.SetParent(parent.transform);

                filter = renderGO.AddComponent<MeshFilter>();
                render = renderGO.AddComponent<MeshRenderer>();

                render.sharedMaterials = ai.Mats[i];
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                render.receiveShadows = false;

                ai.ChildMesh[i] = filter;
            }

        }
        else
        {
            ai = mAfterImagesPool[mAfterImagesPool.Count - 1];
            mAfterImagesPool.Remove(ai);
            ai.ParentGO.SetActive(true);
            ai.Init();
        }
        SetAfterImagePos(ai);
        return ai;
    }

    private void SetAfterImagePos(AfterImageObj afterImage)
    {
        if (afterImage == null || meshRender == null) return;

        //afterImage.ParentGO.transform.localScale = transform.lossyScale;
        if (afterImage.ChildMesh != null)
        {
            for (int i = 0; i < afterImage.ChildMesh.Length; i++)
            {
                MeshFilter mesh = afterImage.ChildMesh[i];
                if (mesh != null)
                {
                    GameObject renderGO = mesh.gameObject;
                    renderGO.transform.localScale = meshRender[i].transform.localScale;
                    renderGO.transform.position = meshRender[i].transform.position;
                    renderGO.transform.rotation = meshRender[i].transform.rotation;
                }
            }
        }
    }

    private void DisposeData()
    {
        if (mActivedAfterImages != null && mActivedAfterImages.Count > 0)
        {
            for (int i = mActivedAfterImages.Count - 1; i >= 0; i--)
            {
                AfterImageObj afterImage = mActivedAfterImages[i];
                afterImage.DisposeData();
            }
            mActivedAfterImages = null;
        }

        if(mAfterImagesPool != null && mAfterImagesPool.Count > 0)
        {
            for(int i = mAfterImagesPool.Count - 1; i >= 0 ; i--)
            {
                AfterImageObj afterImage = mAfterImagesPool[i];
                afterImage.DisposeData();
            }
            mAfterImagesPool = null;
        }
    }

    private void OnDestroy()
    {
        DisposeData();
        Destroy(mParent);
    }
}
