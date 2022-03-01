
using UnityEngine;

namespace YoukiaEngine
{
    public static class YoukiaEngineUtils
    {
        public static Bounds Transform(this Bounds bounds, Matrix4x4 matrix)
        {
            Vector3 p0 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
            Vector3 p1 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
            Vector3 p2 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            Vector3 p3 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            Vector3 p4 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            Vector3 p5 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            Vector3 p6 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            Vector3 p7 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);

            bounds.center = matrix.MultiplyPoint(p0);
            bounds.size = Vector3.zero;
            bounds.Encapsulate(matrix.MultiplyPoint(p1));
            bounds.Encapsulate(matrix.MultiplyPoint(p2));
            bounds.Encapsulate(matrix.MultiplyPoint(p3));
            bounds.Encapsulate(matrix.MultiplyPoint(p4));
            bounds.Encapsulate(matrix.MultiplyPoint(p5));
            bounds.Encapsulate(matrix.MultiplyPoint(p6));
            bounds.Encapsulate(matrix.MultiplyPoint(p7));

            return bounds;
        }

        public static void FastTransform(ref Bounds src, ref Bounds dst, ref Matrix4x4 matrix)
        {
            Vector3 min = src.min;
            Vector3 max = src.max;
            float minx = 0, miny = 0, minz = 0;
            float maxx = 0, maxy = 0, maxz = 0;

            minx += matrix.m03;                                 //x方向上平移
            maxx += matrix.m03;                                 //x方向上平移
            miny += matrix.m13;                                 //y方向上平移
            maxy += matrix.m13;                                 //y方向上平移
            minz += matrix.m23;                                //z方向上平移
            maxz += matrix.m23;                                //z方向上平移

            if (matrix.m00 > 0.0f)
            {
                minx += matrix.m00 * min.x; maxx += matrix.m00 * max.x;
            }
            else
            {
                minx += matrix.m00 * max.x; maxx += matrix.m00 * min.x;
            }
            if (matrix.m01 > 0.0f)
            {
                minx += matrix.m01 * min.y; maxx += matrix.m01 * max.y;
            }
            else
            {
                minx += matrix.m01 * max.y; maxx += matrix.m01 * min.y;
            }
            if (matrix.m02 > 0.0f)
            {
                minx += matrix.m02 * min.z; maxx += matrix.m02 * max.z;
            }
            else
            {
                minx += matrix.m02 * max.z; maxx += matrix.m02 * min.z;
            }

            if (matrix.m10 > 0.0f)
            {
                miny += matrix.m10 * min.x; maxy += matrix.m10 * max.x;
            }
            else
            {
                miny += matrix.m10 * max.x; maxy += matrix.m10 * min.x;
            }
            if (matrix.m11 > 0.0f)
            {
                miny += matrix.m11 * min.y; maxy += matrix.m11 * max.y;
            }
            else
            {
                miny += matrix.m11 * max.y; maxy += matrix.m11 * min.y;
            }
            if (matrix.m12 > 0.0f)
            {
                miny += matrix.m12 * min.z; maxy += matrix.m12 * max.z;
            }
            else
            {
                miny += matrix.m12 * max.z; maxy += matrix.m12 * min.z;
            }

            if (matrix.m20 > 0.0f)
            {
                minz += matrix.m20 * min.x; maxz += matrix.m20 * max.x;
            }
            else
            {
                minz += matrix.m20 * max.x; maxz += matrix.m20 * min.x;
            }
            if (matrix.m21 > 0.0f)
            {
                minz += matrix.m21 * min.y; maxz += matrix.m21 * max.y;
            }
            else
            {
                minz += matrix.m21 * max.y; maxz += matrix.m21 * min.y;
            }
            if (matrix.m22 > 0.0f)
            {
                minz += matrix.m22 * min.z; maxz += matrix.m22 * max.z;
            }
            else
            {
                minz += matrix.m22 * max.z; maxz += matrix.m22 * min.z;
            }

            min.x = minx; min.y = miny; min.z = minz;    //用新的AABB坐标替换原有坐标
            max.x = maxx; max.y = maxy; max.z = maxz;    //用新的AABB坐标替换原有坐标

            dst.SetMinMax(min, max);
        }

        public static void FastTransformBounds(ref Bounds src, ref Bounds dst, ref Matrix4x4 matrix, ref Vector3 scale)
        {
            dst.center = matrix.MultiplyPoint(src.center);
            dst.size = Vector3.Scale(src.size, scale);
        }
    }
}
