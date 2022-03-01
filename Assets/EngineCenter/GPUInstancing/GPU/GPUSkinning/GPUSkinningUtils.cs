using System.Collections.Generic;
using UnityEngine;

namespace YoukiaEngine
{
    public static class GPUSkinningUtils
    {
        public const float FRAME_DELTA = 1 / 30f;

        public static void MeshPrepareForGPUSkinning(Mesh mesh, bool fourBonesWeight = false)
        {
            BoneWeight[] boneWs = mesh.boneWeights;
            if (boneWs == null || boneWs.Length == 0)
            {
                return;
            }
            Vector4[] uv2 = new Vector4[mesh.vertexCount];
            for (int i = 0; i < uv2.Length; i++)
            {
                BoneWeight boneWeight = boneWs[i];
                uv2[i].x = boneWeight.boneIndex0;//1顶点id
                uv2[i].y = boneWeight.weight0;//1顶点权重
                uv2[i].z = boneWeight.boneIndex1;//2顶点id
                uv2[i].w = boneWeight.weight1;//2顶点权重
            }
            mesh.SetUVs(1, new List<Vector4>(uv2));

            if (fourBonesWeight)
            {
                Vector4[] uv3 = new Vector4[mesh.vertexCount];
                for (int i = 0; i < uv3.Length; i++)
                {
                    BoneWeight boneWeight = boneWs[i];
                    uv3[i].x = boneWeight.boneIndex2;//3顶点id
                    uv3[i].y = boneWeight.weight2;//3顶点权重
                    uv3[i].z = boneWeight.boneIndex3;//4顶点id
                    uv3[i].w = boneWeight.weight3;//4顶点权重
                }
                mesh.SetUVs(2, new List<Vector4>(uv3));
            }

            //mesh.boneWeights = null;
        }

        private static System.Action<Plane[], Matrix4x4> _calculateFrustumPlanes_Imp;
        //https://forum.unity.com/threads/calculatefrustumplanes-without-allocations.371636/
        public static void CalculateFrustumPlanes(Plane[] planes, ref Matrix4x4 worldToProjectMatrix)
        {
            if (planes == null) 
                return;
            if (planes.Length < 6) 
                return;
            if (_calculateFrustumPlanes_Imp == null)
            {
                var meth = typeof(GeometryUtility).GetMethod("Internal_ExtractPlanes", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic, null, new System.Type[] { typeof(Plane[]), typeof(Matrix4x4) }, null);
                if (meth == null) throw new System.Exception("Failed to reflect internal method. Your Unity version may not contain the presumed named method in GeometryUtility.");

                _calculateFrustumPlanes_Imp = System.Delegate.CreateDelegate(typeof(System.Action<Plane[], Matrix4x4>), meth) as System.Action<Plane[], Matrix4x4>;
                if (_calculateFrustumPlanes_Imp == null) throw new System.Exception("Failed to reflect internal method. Your Unity version may not contain the presumed named method in GeometryUtility.");
            }

            _calculateFrustumPlanes_Imp(planes, worldToProjectMatrix);
        }
    }
}
