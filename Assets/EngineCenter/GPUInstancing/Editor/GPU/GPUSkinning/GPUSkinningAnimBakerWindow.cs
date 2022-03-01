using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using YoukiaEngine;
using AnimationClip = UnityEngine.AnimationClip;

namespace YoukiaEngineEditor
{
    public class AnimMapBakerWindow : EditorWindow
    {

        private enum SaveMode
        {
            Texture,
//            Config,
        }

        #region 字段

        public static GameObject targetGo;
        private static GPUSkinning_BoneAnimation[] boneAnimations = null;
        private static SaveMode stratege = SaveMode.Texture;

        #endregion


        #region  方法

        private const string PREFAB_PATH = "Assets/Resources/Art/Role/";
        private static string folderPath;
        private const string subFolderPath = "GPUSkinning";
        private const string ASSETS_PATH = "Assets/Resources/Art/LoadPrefab/Role/Monster";


        [MenuItem("YoukiaEngine/GPU Skinning...")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(AnimMapBakerWindow));
        }

        [MenuItem("Assets/导出GPUSkinning")]
        private static void Build()
        {
            targetGo = Selection.activeGameObject;
            if (targetGo == null)
            {
                return;
            }
            string path = AssetDatabase.GetAssetPath(targetGo);
            if (path.StartsWith(PREFAB_PATH) == false)
            {
                //return;
            }
            folderPath = GetFolderPath(path);
            brake();
            Save();

            EditorUtility.DisplayDialog("Info", "导出成功！", "OK");
        }

        private static string GetFolderPath(string path)
        {
            int index = path.LastIndexOf("/");
            if (index == -1)
            {
                return "";
            }
            return path.Substring(0, index);
        }

        static GPUSkinning_Bone[] bones;

        private static int getBoneId(string n)
        {
            string[] narr = n.Split('/');
            string nn = narr[narr.Length - 1];
            //n += "/";
            for (int i = 0; i < bones.Length; i++)
            {
                if (nn == bones[i].name) // && getAllPath(bones[i].transform)==n
                {
                    return i;
                }
            }

            return -1;
        }

        private static string getBoneName(string n)
        {
            string[] narr = n.Split('/');
            string nn = narr[narr.Length - 1];
            return nn;
        }

        private string getAllPath(Transform t)
        {
            string s = "";
            while (t != smr.rootBone)
            {
                s = t.name + "/" + s;
                t = t.parent;
            }

            s = t.name + "/" + s;
            return s;
        }

        private static SkinnedMeshRenderer smr = null;

        private static Mesh mesh = null;

        private static MeshFilter mf = null;

        private static MeshRenderer mr = null;

        private static Material newMtrl = null;

        private static Mesh newMesh = null;

        private static Matrix4x4[] matricesUniformBlock = null;

        private static Dictionary<Transform, GPUSkinning_Bone> boneDic;

        private static void SetMeshUV(Mesh m,int index,Vector2[] uvs)
        {
            List<Vector2> uvList = new List<Vector2>();
            int len = uvs.Length;
            for(int i = 0;i < len;i ++)
            {
                uvList.Add(uvs[i]);
            }
            m.SetUVs(index, uvList);
        }

        private static void brake()
        {
            GameObject go = (GameObject)Instantiate(targetGo);
            smr = go.GetComponentInChildren<SkinnedMeshRenderer>();
            Transform rootBone = go.transform.Find("rotate");
            smr.rootBone = rootBone;
            mesh = smr.sharedMesh;

            //********************** 创建新的材质球替换老的 **************************       
            mf = go.AddComponent<MeshFilter>();
            mr = go.AddComponent<MeshRenderer>();

            

            Mesh newMesh = GameObject.Instantiate(mesh);


            //**********************设置每个顶点的骨骼信息 * *************************

            BoneWeight[] boneWs = newMesh.boneWeights;
            Vector4[] tangents = new Vector4[newMesh.vertexCount];
            for (int i = 0; i < newMesh.vertexCount; i++)
            {
                BoneWeight boneWeight = boneWs[i];
                tangents[i].x = boneWeight.boneIndex0; //1顶点id
                tangents[i].y = boneWeight.weight0; //1顶点权重
                tangents[i].z = boneWeight.boneIndex1; //2顶点i0d
                tangents[i].w = boneWeight.weight1; //2顶点权重    
                //if(boneWeight.boneIndex0==29)
                //Debug.Log(boneWeight.boneIndex0 + " : " + boneWeight.weight0 + " / "+boneWeight.boneIndex1+" : " + boneWeight.weight1 + " / " + boneWeight.boneIndex2 + " : " + boneWeight.weight2 + " / " + boneWeight.boneIndex3 + " : " + boneWeight.weight3);
            }

            newMesh.tangents = tangents;

            Material material = smr.sharedMaterial;
            bool twoBoneWeight = material.IsKeywordEnabled("_TwoBoneWeight");
            GPUSkinningUtils.MeshPrepareForGPUSkinning(newMesh, !twoBoneWeight);


            mf.sharedMesh = newMesh;
            //********************************* 获取全部骨骼和骨骼的蒙皮信息 保存在bones里 ************************      

            boneDic = new Dictionary<Transform, GPUSkinning_Bone>();

            bones = new GPUSkinning_Bone[smr.bones.Length];
            for (int i = 0; i < smr.bones.Length; ++i)
            {
                GPUSkinning_Bone bone = new GPUSkinning_Bone();
                bones[i] = bone;
                bone.id = i;
                bone.transform = smr.bones[i];
                bone.bindpose = mesh.bindposes[i] /*smr to bone*/;

                boneDic[smr.bones[i]] = bone;
            }

            //System.Action<GPUSkinning_Bone> CollectChildren = null;
            //****************  设置骨骼的父子关系  ********************
            //************主要的变化 将断开的骨骼重新连接***********
            Transform[] transforms = smr.rootBone.gameObject.GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < transforms.Length; i++)
            {
                if (!boneDic.ContainsKey(transforms[i]) || boneDic[transforms[i]] == null)
                {
                    boneDic[transforms[i]] = new GPUSkinning_Bone();
                    boneDic[transforms[i]].transform = transforms[i];
                }

                if (transforms[i] != smr.rootBone)
                    boneDic[transforms[i]].parent = boneDic[transforms[i].transform.parent];
            }

            foreach (var item in boneDic)
            {
                List<GPUSkinning_Bone> childList = new List<GPUSkinning_Bone>();

                foreach (var bone in boneDic.Values)
                {
                    if (bone.parent == item.Value)
                        childList.Add(bone);
                }

                item.Value.children = childList.ToArray();
            }

            //****************************************************
            int boneAnimationsCount = 0;
            Animator at = go.GetComponent<Animator>();
            boneAnimations = new GPUSkinning_BoneAnimation[at.runtimeAnimatorController.animationClips.Length];
            foreach (AnimationClip animClip in at.runtimeAnimatorController.animationClips)
            {
                GPUSkinning_BoneAnimation boneAnimation = ScriptableObject.CreateInstance<GPUSkinning_BoneAnimation>();
                boneAnimation.fps = (int)animClip.frameRate;
                boneAnimation.animName = animClip.name;
                boneAnimation.frames =
                    new GPUSkinning_BoneAnimationFrame[(int)(animClip.length * boneAnimation.fps) + 1];
                boneAnimation.length = animClip.length;
                boneAnimations[boneAnimationsCount++] = boneAnimation;

                for (int frameIndex = 0; frameIndex < boneAnimation.frames.Length; ++frameIndex)
                {
                    GPUSkinning_BoneAnimationFrame frame = new GPUSkinning_BoneAnimationFrame();
                    boneAnimation.frames[frameIndex] = frame;
                    float second = (float)(frameIndex) / (float)boneAnimation.fps;

                    List<string> bones2 = new List<string>();
                    List<Matrix4x4> matrices = new List<Matrix4x4>();
                    List<int> bonesHierarchyIds = new List<int>();
                    List<string> bonesHierarchyNames = new List<string>();
                    List<Matrix4x4> matrices2 = new List<Matrix4x4>();
                    EditorCurveBinding[] curvesBinding = AnimationUtility.GetCurveBindings(animClip);
                    foreach (EditorCurveBinding curveBinding in curvesBinding)
                    {
                        string path = curveBinding.path;
                        if (bones2.Contains(path))
                        {
                            continue;
                        }

                        bones2.Add(path);

                        Transform ts = go.transform.Find(path);
                        if (ts == null)
                            continue;

                        AnimationCurve curveRX =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalRotation.x");
                        AnimationCurve curveRY =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalRotation.y");
                        AnimationCurve curveRZ =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalRotation.z");
                        AnimationCurve curveRW =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalRotation.w");

                        AnimationCurve curvePX =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalPosition.x");
                        AnimationCurve curvePY =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalPosition.y");
                        AnimationCurve curvePZ =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalPosition.z");

                        AnimationCurve curveSX =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalScale.x");
                        AnimationCurve curveSY =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalScale.y");
                        AnimationCurve curveSZ =
                            AnimationUtility.GetEditorCurve(animClip, path, curveBinding.type, "m_LocalScale.z");

                        float curveRX_v = 0;
                        if (curveRX != null)
                            curveRX_v = curveRX.Evaluate(second);
                        float curveRY_v = 0;
                        if (curveRY != null)
                            curveRY_v = curveRY.Evaluate(second);
                        float curveRZ_v = 0;
                        if (curveRZ != null)
                            curveRZ_v = curveRZ.Evaluate(second);
                        float curveRW_v = 0;
                        if (curveRW != null)
                            curveRW_v = curveRW.Evaluate(second);

                        #region 缩放

                        Vector3 scale = ts.localScale;

                        float curveSX_v = 0;
                        if (curveSX != null)
                        {
                            curveSX_v = curveSX.Evaluate(second);
                        }

                        float curveSY_v = 0;
                        if (curveSY != null)
                            curveSY_v = curveSY.Evaluate(second);

                        float curveSZ_v = 0;
                        if (curveSZ != null)
                            curveSZ_v = curveSZ.Evaluate(second);

                        if (curveSX != null || curveSY != null || curveSZ != null)
                            scale = new Vector3(curveSX_v, curveSY_v, curveSZ_v);

                        #endregion

                        #region 位置

                        Vector3 translation = ts.localPosition;

                        float curvePX_v = 0;
                        if (curvePX != null)
                        {
                            curvePX_v = curvePX.Evaluate(second);
                        }

                        float curvePY_v = 0;
                        if (curvePY != null)
                        {
                            curvePY_v = curvePY.Evaluate(second);
                        }

                        float curvePZ_v = 0;
                        if (curvePZ != null)
                        {
                            curvePZ_v = curvePZ.Evaluate(second);
                        }

                        if (curvePX != null && curvePY != null && curvePZ != null)
                            translation = new Vector3(curvePX_v, curvePY_v, curvePZ_v);

                        #endregion

                        Quaternion rotation;
                        if (curveRX_v == 0 && curveRY_v == 0 && curveRZ_v == 0 && curveRW_v == 0)
                        {
                            rotation = ts.localRotation; // new Quaternion(0, 0, 0, 1);
                        }
                        else
                        {
                            rotation = new Quaternion(curveRX_v, curveRY_v, curveRZ_v, curveRW_v);
                        }

                        NormalizeQuaternion(ref rotation);

                        int boneId = getBoneId(path);
                        if (boneId != -1)
                        {
                            bonesHierarchyIds.Add(boneId);
                            matrices.Add(Matrix4x4.TRS(translation, rotation, scale));
                        }
                        else
                        {
                            bonesHierarchyNames.Add(getBoneName(path));
                            matrices2.Add(Matrix4x4.TRS(translation, rotation, scale));
                        }
                    }

                    //**********************************  记录位置   ****************************
                    frame.matrices = matrices.ToArray();
                    //**********************************  记录对应骨骼id  **************************
                    frame.bonesId = bonesHierarchyIds.ToArray();
                    frame.bonesName = bonesHierarchyNames.ToArray();
                    frame.matrices2 = matrices2.ToArray();
                }
            }


            CreateFolder(folderPath, subFolderPath);
            string resPath = folderPath + "/" + subFolderPath + "/";

            textureWidth = 0;
            textureHeight = smr.bones.Length * 4;

            GPUSkinningAnimator gsa = go.AddComponent<GPUSkinningAnimator>();
            gsa.AnimClips = new YoukiaEngine.AnimationClip[boneAnimations.Length];
            gsa.AnimClips = new YoukiaEngine.AnimationClip[boneAnimations.Length];
            for (int i = 0; i < boneAnimations.Length; i++)
            {

                YoukiaEngine.AnimationClip clip = new YoukiaEngine.AnimationClip();
                clip.Name = boneAnimations[i].animName;
                clip.Start = textureWidth + 1;
                clip.Offset = boneAnimations[i].frames.Length - 2;
                textureWidth += boneAnimations[i].frames.Length;
                gsa.AnimClips[i] = clip;
            }

            //get2(textureHeight, textureWidth);
            Texture2D animMap = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBAHalf, false);
            animMap.name = string.Format("{0}.animMap", targetGo.name);
            int x = 0;
            int y = 0;
            for (int i = 0; i < boneAnimations.Length; i++)
            {
                saveTexture(boneAnimations[i], animMap, ref x, ref y);
            }

            animMap.Apply();
            AssetDatabase.CreateAsset(animMap, Path.Combine(resPath, animMap.name + ".asset"));
            newMtrl = new Material(Shader.Find("YoukiaEngine/Lighting/YoukiaMonsterPBR"));
            newMtrl.CopyPropertiesFromMaterial(smr.sharedMaterial);
            newMtrl.EnableKeyword("_GPUAnimation");
            mr.sharedMaterial = newMtrl;
            newMtrl.enableInstancing = true;
            EditorApplication.update -= UpdateAsyncBuildOperations;
            EditorApplication.update += UpdateAsyncBuildOperations;
            newMtrl.SetTexture("_AnimMap", animMap);
            EditorUtility.SetDirty(newMtrl);

            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(go.transform.GetChild(i).gameObject);
            }

            GameObject.DestroyImmediate(at);

            string matPath = resPath + "/" + targetGo.name + "_mat.mat";
            matPath = matPath.Replace("//", "/");
            AssetDatabase.CreateAsset(newMtrl, matPath);


            AssetDatabase.CreateAsset(newMesh, Path.Combine(resPath, targetGo.name + "_mesh.mesh"));

            PrefabUtility.CreatePrefab(ASSETS_PATH + "/" + targetGo.name + "_GPU" + ".prefab", go);
            GameObject.DestroyImmediate(go);

        }

        static void UpdateAsyncBuildOperations()
        {
            if(newMtrl.GetTexture("_AnimMap") != null)
            {
                EditorApplication.update -= UpdateAsyncBuildOperations;
                return;
            }
            ImportMaterial(newMtrl);
            EditorApplication.update -= UpdateAsyncBuildOperations;
        }


        private static void ImportMaterial(Material targetMat)
        {
            string path = AssetDatabase.GetAssetPath(targetMat);
            string name = targetMat.name;
            string animMapName = name + ".animMap.asset";
            string directName = System.IO.Path.GetDirectoryName(path).Replace("\\","/");
            string allPath = directName + "/" + animMapName;
            Texture animMap = AssetDatabase.LoadAssetAtPath(allPath, typeof(Texture)) as Texture;
            newMtrl.SetTexture("_AnimMap", animMap);
            EditorUtility.SetDirty(newMtrl);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void saveTexture(GPUSkinning_BoneAnimation ba, Texture2D animMap, ref int x, ref int y)
        {
            for (int frameIndex = 0; frameIndex < ba.frames.Length; ++frameIndex)
            {
                UpdateBoneAnimationMatrix(ba.frames[frameIndex]);
                for (int i = 0; i < smr.bones.Length; ++i)
                {
                    Matrix4x4 mx = bones[i].animationMatrix;
                    int ii = i * 4 + y * textureHeight;
                    animMap.SetPixel(x, ii++, new Color(mx.m00, mx.m01, mx.m02, mx.m03));
                    animMap.SetPixel(x, ii++, new Color(mx.m10, mx.m11, mx.m12, mx.m13));
                    animMap.SetPixel(x, ii++, new Color(mx.m20, mx.m21, mx.m22, mx.m23));
                    animMap.SetPixel(x, ii++, new Color(mx.m30, mx.m31, mx.m32, mx.m33));
                }

                x++;
                //if (x > textureWidth)
                //{
                //    y++;
                //}
            }
        }

        static int textureWidth;

        static int textureHeight;

        //最优的图片尺寸
        private static void get2(int a, int b)
        {
            float c = b / a / 2;
            if (c == 0)
            {
                Debug.Log("?" + a + " / " + b);
                return;
            }

            Debug.Log("c = " + c + "/" + c * a + " / " + Mathf.Ceil(b / c));
        }

        private static GPUSkinning_BoneAnimation_config saveMx(GPUSkinning_BoneAnimation ba, string folderPath)
        {
            GPUSkinning_BoneAnimation_config gba = new GPUSkinning_BoneAnimation_config();
            gba.fps = ba.fps;
            gba.animName = ba.animName;
            gba.frames = new GPUSkinning_BoneAnimationFrame_config[ba.frames.Length];
            gba.length = ba.length;
            for (int frameIndex = 0; frameIndex < ba.frames.Length; ++frameIndex)
            {
                UpdateBoneAnimationMatrix(ba.frames[frameIndex]);

                GPUSkinning_BoneAnimationFrame_config gbaf = new GPUSkinning_BoneAnimationFrame_config();

                gbaf.matrices = new Matrix4x4[smr.bones.Length];
                for (int i = 0; i < smr.bones.Length; ++i)
                {
                    Matrix4x4 mx = bones[i].animationMatrix;
                    gbaf.matrices[i] = mx;
                    int ii = i * 4;
                }

                gba.frames[frameIndex] = gbaf;
            }

            AssetDatabase.CreateAsset(gba,
                Path.Combine(folderPath, gba.animName + "_" + targetGo.name + "_anim.asset"));
            return gba;
        }

        //************* 更新骨骼的动画信息 *******************
        private static void UpdateBoneAnimationMatrix(GPUSkinning_BoneAnimationFrame frame)
        {
            UpdateBoneTransformMatrix(boneDic[smr.rootBone], Matrix4x4.identity, frame);
        }

        //************* 通过骨骼去帧动画里对应的骨骼mx4信息，返回bone.animationMatrix信息
        private static void UpdateBoneTransformMatrix(GPUSkinning_Bone bone, Matrix4x4 parentMatrix,
            GPUSkinning_BoneAnimationFrame frame)
        {
            int index = BoneAnimationFrameIndexOf(frame, bone);
            Matrix4x4 mat;

            if (index == -1)
            {
                index = BoneAnimationFrameIndexOf2(frame, bone);
                if (index == -1)
                {
                    //如果骨骼不存在动画中，则返回骨骼当前位置
                    mat = parentMatrix * Matrix4x4.TRS(bone.transform.localPosition, bone.transform.localRotation,
                              bone.transform.localScale);
                }
                else
                {
                    mat = parentMatrix * frame.matrices2[index];
                }

            }
            else
            {
                mat = parentMatrix * frame.matrices[index];
            }

            bone.animationMatrix = mat * bone.bindpose; //   骨骼的当前位置
            GPUSkinning_Bone[] children = bone.children;
            int numChildren = children.Length;
            for (int i = 0; i < numChildren; ++i)
            {
                UpdateBoneTransformMatrix(children[i], mat, frame);
            }
        }

        private static void NormalizeQuaternion(ref Quaternion q)
        {
            float sum = 0;
            for (int i = 0; i < 4; ++i)
                sum += q[i] * q[i];
            float magnitudeInverse = 1 / Mathf.Sqrt(sum);
            for (int i = 0; i < 4; ++i)
                q[i] *= magnitudeInverse;
        }

        private static void Save()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string CreateFolder(string p1, string p2)
        {
            string folderPath = Path.Combine(p1, p2);
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder(p1, p2);
            }

            return folderPath;
        }

        #endregion

        private GPUSkinning_Bone GetBoneByTransform(Transform transform)
        {
            foreach (GPUSkinning_Bone bone in bones)
            {
                if (bone.transform == transform)
                {
                    return bone;
                }
            }

            return null;
        }

        private static int BoneAnimationFrameIndexOf2(GPUSkinning_BoneAnimationFrame frame, GPUSkinning_Bone bone)
        {
            string[] bs = frame.bonesName;
            for (int i = 0; i < bs.Length; i++)
            {
                if (bs[i] == bone.name)
                {
                    return i;
                }
            }

            return -1;
        }

        private static int BoneAnimationFrameIndexOf(GPUSkinning_BoneAnimationFrame frame, GPUSkinning_Bone bone)
        {
            int[] bs = frame.bonesId;
            for (int i = 0; i < bs.Length; i++)
            {
                if (bs[i] == bone.id)
                {
                    return i;
                }
            }

            return -1;
        }

    }
}