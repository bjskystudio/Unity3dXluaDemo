using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YoukiaEngine
{
    public static class GeometryInstancingUtils
    {
        public const int MAX_GEOMETRY_INSTANCE_DRAW_COUNT = 200;
	    public const int MAX_GEOMETRY_INSTANCE_ARRAY_COUNT = 1024;

		//Fast matrix copy
		public static void WorldMatrixCopy(ref Matrix4x4 dst, ref Matrix4x4 src)
        {
            dst.m00 = src.m00;
            dst.m01 = src.m01;
            dst.m02 = src.m02;
            dst.m03 = src.m03;
            dst.m10 = src.m10;
            dst.m11 = src.m11;
            dst.m12 = src.m12;
            dst.m13 = src.m13;
            dst.m20 = src.m20;
            dst.m21 = src.m21;
            dst.m22 = src.m22;
            dst.m23 = src.m23;

            //this elment has been initialized when matrix be created
            //dst.m33 = src.m33;
        }

		public static void SwitchMatrix(ref Matrix4x4 m1, ref Matrix4x4 m2)
		{
			float tmp;
			tmp = m2.m00;
			m2.m00 = m1.m00;
			m1.m00 = tmp;

			tmp = m2.m01;
			m2.m01 = m1.m01;
			m1.m01 = tmp;

			tmp = m2.m02;
			m2.m02 = m1.m02;
			m1.m02 = tmp;

			tmp = m2.m03;
			m2.m03 = m1.m03;
			m1.m03 = tmp;

			tmp = m2.m10;
			m2.m10 = m1.m10;
			m1.m10 = tmp;

			tmp = m2.m11;
			m2.m11 = m1.m11;
			m1.m11 = tmp;

			tmp = m2.m12;
			m2.m12 = m1.m12;
			m1.m12 = tmp;

			tmp = m2.m13;
			m2.m13 = m1.m13;
			m1.m13 = tmp;

			tmp = m2.m20;
			m2.m20 = m1.m20;
			m1.m20 = tmp;

			tmp = m2.m21;
			m2.m21 = m1.m21;
			m1.m21 = tmp;

			tmp = m2.m22;
			m2.m22 = m1.m22;
			m1.m22 = tmp;

			tmp = m2.m23;
			m2.m23 = m1.m23;
			m1.m23 = tmp;

// 			m1.m01 = m2.m01;
// 			m1.m02 = m2.m02;
// 			m1.m03 = m2.m03;
// 			m1.m10 = m2.m10;
// 			m1.m11 = m2.m11;
// 			m1.m12 = m2.m12;
// 			m1.m13 = m2.m13;
// 			m1.m20 = m2.m20;
// 			m1.m21 = m2.m21;
// 			m1.m22 = m2.m22;
// 			m1.m23 = m2.m23;

			//this elment has been initialized when matrix be created
			//dst.m33 = src.m33;
		}

		public static void SwitchVector(ref Vector4 v1, ref Vector4 v2)
		{
			float tmp;
			tmp = v2.x;
			v2.x = v1.x;
			v1.x = tmp;

			tmp = v2.y;
			v2.y = v1.y;
			v1.y = tmp;

			tmp = v2.z;
			v2.z = v1.z;
			v1.z = tmp;

			tmp = v2.w;
			v2.w = v1.w;
			v1.w = tmp;
		}

		public static void ImpostorWorldMatrixCopy(ref Matrix4x4 dst, ref Vector3 pos)
        {
            dst.m03 = pos.x;
            dst.m13 = pos.y;
            dst.m23 = pos.z;
        }

        public static void VectorCopy(ref Vector4 dst, ref Vector4 src)
        {
            dst.x = src.x;
            dst.y = src.y;
            dst.z = src.z;
            dst.w = src.w;
        }

        public static float DistanceSquare(ref Vector3 a, ref Vector3 b)
        {
            float tX = a.x - b.x;
            float tY = a.y - b.y;
            float tZ = a.z - b.z;
            return tX*tX + tY*tY + tZ*tZ;
        }

		public static float DistanceSquare(ref Matrix4x4 m, ref Vector3 b)
		{
			float tX = m.m03 - b.x;
			float tY = m.m13 - b.y;
			float tZ = m.m23 - b.z;
			return tX * tX + tY * tY + tZ * tZ;
		}

        public static float DistanceSquareXZ(ref Vector3 a, ref Vector3 b)
        {
            float tX = a.x - b.x;
            float tZ = a.z - b.z;
            return tX * tX + tZ * tZ;
        }

        public static float DistanceSquareXZ(ref Matrix4x4 m, ref Vector3 b)
        {
            float tX = m.m03 - b.x;
            float tZ = m.m23 - b.z;
            return tX * tX + tZ * tZ;
        }

        public static float DistanceSquareXZ(ref Bounds bounds, ref Vector3 b)
        {
            Vector3 pos = bounds.min;
            float tX = pos.x - b.x;
            float tZ = pos.z - b.z;
            float disMin = tX * tX + tZ * tZ;

            pos = bounds.max;
            tX = pos.x - b.x;
            tZ = pos.z - b.z;
            float disMax = tX * tX + tZ * tZ;

            return (disMin < disMax) ? disMin : disMax;
        }

        public static GeometryInstancingGroup CreateGroupByObj(GameObject obj, params GPUObject.TexInfo[] texInfos)
        {
            GeometryInstancingGroup group = null;

            MeshRenderer mr = obj.GetComponent<MeshRenderer>();
            if (mr == null)
            {
                mr = obj.GetComponentInChildren<MeshRenderer>();
            }
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            if (mf == null)
            {
                mf = obj.GetComponentInChildren<MeshFilter>();
            }

            if (mr && mf)
            {
                Mesh mesh = mf.sharedMesh;
                Material mat = mr.sharedMaterial;
                group = new GeometryInstancingGroup(mesh, mat, obj.layer, texInfos);
            }
            return group;
        }

	    public static void  SetSharedBounds(ref InstanceObject[] objs)
	    {
			bool isCombine = objs.Length > 1;
			InstanceBounds bounds = null;
			if (isCombine)
			{
				bounds = new InstanceBounds(new Bounds());
			}

			for (int i = 0; i < objs.Length; i++)
			{
				Bounds srcBounds = objs[i].Bounds.BoundingBox;
				if (isCombine)
				{
					if (i == 0)
					{
						bounds.BoundingBox = srcBounds;
					}
					else
					{
						bounds.BoundingBox.Encapsulate(srcBounds);
					}
					objs[i].SharedBounds = bounds;
				}
			}
		}

		public static void SetSharedBounds(ref DynamicInstanceObject[] objs)
		{
			bool isCombine = objs.Length > 1;
			InstanceBounds bounds = null;
			if (isCombine)
			{
				bounds = new InstanceBounds(new Bounds());
			}

			for (int i = 0; i < objs.Length; i++)
			{
				Bounds srcBounds = objs[i].Bounds.BoundingBox;
				if (isCombine)
				{
					if (i == 0)
					{
						bounds.BoundingBox = srcBounds;
					}
					else
					{
						bounds.BoundingBox.Encapsulate(srcBounds);
					}
					objs[i].SharedBounds = bounds;
				}
			}
		}
	}
}
