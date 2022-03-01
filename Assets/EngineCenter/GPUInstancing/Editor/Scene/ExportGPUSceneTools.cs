using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using ECS;
//using Unity.Rendering;
//using Unity.Transforms;
//using Unity.Mathematics;
using UnityEngine.AI;
using UnityEditor.AI;

namespace ExportGPUScene
{
    /// <summary>
    /// 导出GPU场景信息
    /// </summary>
    public class ExportGPUSceneTools
    {
        /// <summary>
        /// 导出GPU
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="gpuRoot"></param>
        public static void Export(string sceneName, Transform gpuRoot)
        {
            AnalyzeBlock(sceneName, gpuRoot);
        }

        private const string SAVEPATH = "Assets/Export/GPUScene/";
        private const string BLOCK_ROOT_NAME_PREFIX = "block_";
        private const string MIDDLESHOT_ROOT_NAME_PREFIX = "middleShot";
        public const string NAVMESH_ROOT_NAME = "navmeshRoot";
        public const string BLOCK_ROOT_NAME = "blockRoot";
        private const string GPU_MODEL = "mapModel";
        private const string CPU_MODEL = "cpuModel";

        /// <summary>
        /// 解析所有块
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="blockRoot"></param>
        private static void AnalyzeBlock(string sceneName,Transform blockRoot)
        {
            int child = blockRoot.childCount;
            for(int i = 0;i < child;i ++)
            {
                Transform blockTypeRoot = blockRoot.GetChild(i);
                if(blockTypeRoot == null)
                {
                    continue;
                }
                string blockTypeName = blockTypeRoot.name;
                if(blockTypeName.StartsWith(BLOCK_ROOT_NAME_PREFIX))//地块节点
                {
                    Transform mapModel = blockTypeRoot.Find(GPU_MODEL);//GPU
                    if (mapModel != null && mapModel.childCount > 0)
                    {
                        string fileName = sceneName + "_" + blockTypeName;
                        AnalyzeOneBlock(blockTypeRoot.localPosition, mapModel, fileName);
                    }
                    Transform cpuModel = blockTypeRoot.Find(CPU_MODEL);
                    if(cpuModel != null && cpuModel.childCount > 0)//CPU
                    {
                        string fileName = sceneName + "_" + blockTypeName + "_cpu";
                        AnalyzeOneCPUBlock(cpuModel, fileName);
                    }
                }
                if(blockTypeName.StartsWith(MIDDLESHOT_ROOT_NAME_PREFIX))//中景节点
                {
                    string fileName = sceneName + "_" + blockTypeRoot.name;
                    AnalyzeOneBlock(Vector3.zero,blockTypeRoot, fileName);
                }
            }

        }

        private static bool CheckLegitimate(Transform trans)
        {
            Renderer[] renders = trans.GetComponentsInChildren<Renderer>(true);
            if(renders == null || renders.Length == 0)
            {
                return true;
            }
            int len = renders.Length;
            for(int i = 0;i < len;i ++)
            {
                Renderer render = renders[i];
                if (render.sharedMaterial != null && render.sharedMaterial.shader.name.Equals("Standard"))
                {
                    Selection.activeObject = render.gameObject;
                    EditorUtility.DisplayDialog("警告", "有材质球使用了Standard", "确定");
                    return false;
                }
            }
            return true;
        }

        private static void SetActiveChild(Transform trans,bool isShow)
        {
            int len = trans.childCount;
            for(int i = 0;i < len;i ++)
            {
                GameObject go = trans.GetChild(i).gameObject;
                go.SetActive(isShow);
            }
        }

        public static void AnalyzeOneNav(Transform blockTypeRoot,string fileName)
        {
            Transform navmeshObj = blockTypeRoot;
            bool show = navmeshObj.gameObject.activeSelf;
            if(show == false)
            {
                navmeshObj.gameObject.SetActive(true);
            }
            SetActiveChild(navmeshObj,true);
            //Transform navmeshObj = blockTypeRoot.Find(NAVMESH_ROOT_NAME);
            //if (navmeshObj == null)
            //{
            //    EditorUtility.DisplayDialog("警告", "找不到" + NAVMESH_ROOT_NAME + "节点", "确定");
            //    return;
            //}
            //Selection.activeObject = navmeshObj;
            LayerUtil.SetLayer(navmeshObj.gameObject, LayerUtil.LayerNavMesh);
            NavMeshSurface nmSurface = navmeshObj.GetComponent<NavMeshSurface>();
            if (nmSurface == null)
            {
                nmSurface = navmeshObj.gameObject.AddComponent<NavMeshSurface>();
            }
            nmSurface.collectObjects = CollectObjects.All;
            nmSurface.layerMask = LayerUtil.NavMesh;
            UnityEngine.Object[] surfaces = new UnityEngine.Object[] { nmSurface };
            NavMeshAssetManager.instance.StartBakingSurfaces(surfaces,(savePath)=> {
                NavMeshExport.Export(savePath, fileName);

                navmeshObj.gameObject.SetActive(show);

                AssetDatabase.Refresh();
            });
        }

        private static float SaveFloat(float f)
        {
            return float.Parse(f.ToString("F2"));
        }

        /// <summary>
        /// 解析单个CPU块
        /// </summary>
        /// <param name="modelRoot"></param>
        /// <param name="fileName"></param>
        private static void AnalyzeOneCPUBlock(Transform modelRoot, string fileName)
        {
            if(CheckLegitimate(modelRoot) == false)
            {
                return;
            }
            Animation[] anims = modelRoot.GetComponentsInChildren<Animation>(true);
            if(anims != null && anims.Length > 0)
            {
                foreach(Animation anim in anims)
                {
                    GameObject go = anim.gameObject;
                    ScenePlayTriggerAnimation triggerAnim = go.GetComponent<ScenePlayTriggerAnimation>();
                    if (triggerAnim == null)
                    {
                        triggerAnim = go.AddComponent<ScenePlayTriggerAnimation>();
                    }
                    triggerAnim.Anim = anim;
                    BoxCollider boxc = go.GetComponent<BoxCollider>();
                    if(boxc == null)
                    {
                        boxc = go.AddComponent<BoxCollider>();
                    }
                    boxc.isTrigger = true;

                    Rigidbody rg = go.GetComponent<Rigidbody>();
                    if(rg == null)
                    {
                        rg = go.AddComponent<Rigidbody>();
                    }
                    rg.isKinematic = false;
                    rg.useGravity = false;
                }
                
            }

            string path = SAVEPATH + fileName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(modelRoot.gameObject, path);
        }

        private static void AnalyzeOneBlock(Vector3 rootPos, Transform modelRoot, string fileName)
        {
            GPUSceneAsset asset = ScriptableObject.CreateInstance<GPUSceneAsset>();
            int len = modelRoot.childCount;
            asset.mObjs = new GPUSceneObjectData[len];
            for (int i = 0; i < len; i++)
            {
                Transform tf = modelRoot.GetChild(i);
                if (tf.gameObject.activeInHierarchy == false)
                {
                    continue;
                }

                MeshRenderer mr = tf.GetComponent<MeshRenderer>();
                Material mat = mr.sharedMaterial;
                if (mat != null && mat.shader.name.Equals("Standard"))
                {
                    Selection.activeObject = tf.gameObject;
                    EditorUtility.DisplayDialog("警告", "有材质球使用了Standard", "确定");
                    return;
                }

                if (mat.enableInstancing == false)
                {
                    mat.enableInstancing = true;
                    EditorUtility.SetDirty(mat);
                }

                GPUSceneObjectData data = new GPUSceneObjectData();
                data.mPrefab = PrefabUtility.GetCorrespondingObjectFromSource(tf.gameObject) as GameObject;
                data.mPos = tf.position;
                data.mScale = tf.lossyScale;
                data.mRotation = tf.rotation;
                data.mBounds = mr.bounds;
                data.mLightScaleOffset = mr.lightmapScaleOffset;
                data.mLightMapIndex = mr.lightmapIndex;

                asset.mObjs[i] = data;
            }

            string path = SAVEPATH + fileName + ".asset";
            AssetDatabase.CreateAsset(asset, path);
        }

        /// <summary>
        /// 解析单个GPU块
        /// </summary>
        /// <param name="modelRoot"></param>
        /// <param name="fileName"></param>
        //private static void AnalyzeOneBlockECS(Vector3 rootPos, Transform modelRoot,string fileName)
        //{
        //    GpuStaticObjectBlockAsset asset = ScriptableObject.CreateInstance<GpuStaticObjectBlockAsset>();
        //    MeshRenderer[] meshRenderers = modelRoot.GetComponentsInChildren<MeshRenderer>();
        //    MeshFilter[] meshFilters = modelRoot.GetComponentsInChildren<MeshFilter>();
        //    int len = meshRenderers.Length;
        //    asset.EntityInfos = new GpuStatiObjectGroupData[len];
        //    for (int i = 0; i < len; i++)
        //    {
        //        MeshRenderer mr = meshRenderers[i];
        //        Transform tf = mr.transform;
        //        if(tf.gameObject.activeInHierarchy == false)
        //        {
        //            continue;
        //        }
        //        Material mat = mr.sharedMaterial;
        //        if (mat != null && mat.shader.name.Equals("Standard"))
        //        {
        //            Selection.activeObject = tf.gameObject;
        //            EditorUtility.DisplayDialog("警告", "有材质球使用了Standard", "确定");
        //            return;
        //        }
        //        Mesh mesh = meshFilters[i].sharedMesh;
        //
        //        if (mat.enableInstancing == false)
        //        {
        //            mat.enableInstancing = true;
        //            EditorUtility.SetDirty(mat);
        //        }
        //
        //        GpuStatiObjectGroupData data = new GpuStatiObjectGroupData();
        //        data.InstanceRender = new RenderMesh();
        //        data.InstanceRender.mesh = mesh;
        //        data.InstanceRender.material = mat;
        //        data.InstanceRender.subMesh = 0;
        //        data.InstanceRender.layer = mr.gameObject.layer;
        //        data.InstanceRender.castShadows = mr.shadowCastingMode;
        //        data.InstanceRender.receiveShadows = mr.receiveShadows;
        //
        //        Vector3 tfPos = tf.position;
        //        Position pos = new Position();
        //        pos.Value = new float3(SaveFloat(tfPos.x - rootPos.x), SaveFloat(tfPos.y - rootPos.y), SaveFloat(tfPos.z - rootPos.z));
        //        data.Pos = pos;
        //
        //        Scale scale = new Scale();
        //        scale.Value = new float3(tf.lossyScale.x, tf.lossyScale.y, tf.lossyScale.z);
        //        data.Scale = scale;
        //
        //        Rotation rotation = new Rotation();
        //        rotation.Value = new quaternion(SaveFloat(tf.rotation.x), SaveFloat(tf.rotation.y), SaveFloat(tf.rotation.z), SaveFloat(tf.rotation.w));
        //        data.Rotation = rotation;
        //
        //        MeshBound bounds = new MeshBound();
        //        Vector3 endCenter = mr.bounds.center - rootPos;
        //        Vector3 center = new Vector3(SaveFloat(endCenter.x), SaveFloat(endCenter.y), SaveFloat(endCenter.z));
        //        bounds.Center = center;
        //        Vector3 EndBounds = mr.bounds.max - Vector3.zero - rootPos;
        //        Vector3 Bounds = new Vector3(SaveFloat(EndBounds.x), SaveFloat(EndBounds.y), SaveFloat(EndBounds.z));
        //        bounds.Radius = SaveFloat(EndBounds.magnitude);
        //        data.Bounds = bounds;
        //
        //
        //        asset.EntityInfos[i] = data;
        //    }
        //
        //    string path = SAVEPATH + fileName + ".asset";
        //    AssetDatabase.CreateAsset(asset, path);
        //}
    }
}

