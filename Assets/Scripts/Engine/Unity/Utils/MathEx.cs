//-----------------------------------------------------------------------
//| Autor:Adam                                                             |
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public static class MathEx
    {

        public static int Clamp(int v, int min, int max)
        {
            return v < min ? min : (v > max ? max : v);
        }

        public static float Clamp01(float v)
        {
            return v < 0 ? 0 : (v > 1 ? 1 : v);
        }
        public static double Clamp01(double v)
        {
            return v < 0 ? 0 : (v > 1 ? 1 : v);
        }

        public static float Clamp(float v, float min, float max)
        {
            return v < min ? min : (v > max ? max : v);
        }

        public static double Clamp(double v, double min, double max)
        {
            return v < min ? min : (v > max ? max : v);
        }

        public static float Lerp(float from, float to, float t)
        {
            return (from + ((to - from) * Clamp01(t)));
        }

        public static double Lerp(double from, double to, double t)
        {
            return (from + ((to - from) * Clamp01(t)));
        }

        public static float Repeat(float t, float length)
        {
            return (t - ((float)(Math.Floor(t / length)) * length));
        }
        public static double Repeat(double t, double length)
        {
            return (t - ((Math.Floor(t / length)) * length));
        }

        public static float LerpAngle(float a, float b, float t)
        {
            float num;
            num = Repeat(b - a, 360f);
            if (num <= 180f)
            {
                goto Label_0021;
            }
            num -= 360f;
        Label_0021:
            return (a + (num * Clamp01(t)));
        }
        public static double LerpAngle(double a, double b, double t)
        {
            double num;
            num = Repeat(b - a, 360);
            if (num <= 180)
            {
                goto Label_0021;
            }
            num -= 360;
        Label_0021:
            return (a + (num * Clamp01(t)));
        }

        public static float Distance(float a, float b)
        {
            return Math.Abs(a - b);
        }
        public static double Distance(double a, double b)
        {
            return Math.Abs(a - b);
        }

        /// <summary>
        /// 返回
        /// 0:在范围内;
        /// 1:过大了;
        /// -1:过小了;
        /// </summary>
        public static int IsRange(float value, float min, float max)
        {
            return value < min ? -1 : (value > max ? 1 : 0);
        }
        public static int IsRange(double value, double min, double max)
        {
            return value < min ? -1 : (value > max ? 1 : 0);
        }

        /// <summary>
        /// 返回两个或更多值中最大的值。
        /// </summary>
        public static float Max(float a, float b)
        {
            return ((a <= b) ? b : a);
        }
        public static double Max(double a, double b)
        {
            return ((a <= b) ? b : a);
        }


        /// <summary>
        /// 返回两个或更多值中最小的值。
        /// </summary>
        public static float Min(float a, float b)
        {
            return ((a <= b) ? a : b);
        }
        public static double Min(double a, double b)
        {
            return ((a <= b) ? a : b);
        }


        /// <summary>
        /// 比较两个浮点数值，看它们是否非常接近。
        /// </summary>
        public static bool Approximately(float a, float b)
        {
            return (Math.Abs(b - a) < Max(1E-06f * Max(Math.Abs(a), Math.Abs(b)), 1.121039E-44f));
        }
        public static bool Approximately(double a, double b)
        {
            return (Math.Abs(b - a) < Max(1E-06 * Max(Math.Abs(a), Math.Abs(b)), 1.121039E-44));
        }

        public static float Distance(Vector3 a, Vector3 b) =>
            Vector3.Distance(a, b);

        public static float RadianXZ(Vector3 a, Vector3 b) =>
            Mathf.Atan2(b.x - a.x, b.z - a.z);

        public static float RadianXY(Vector2 a, Vector2 b) =>
            Mathf.Atan2(b.x - a.x, b.y - a.y);

        public static float RadianUni(Vector2 a, Vector2 b) =>
            1.5707963f - Mathf.Atan2(b.x - a.x, b.y - a.y);

        /// <summary>
        /// -180 ~ 180
        /// </summary>
        public static float AngleXY(Vector2 a, Vector2 b) =>
            RadianXY(a, b) * (180f / Mathf.PI);

        /// <summary>
        /// -180 ~ 180
        /// </summary>
        public static float AngleXZ(Vector3 a, Vector3 b) =>
            RadianXZ(a, b) * (180f / Mathf.PI);


        public static Vector2 ForwardXY(float dis, float radian) =>
            new Vector2(Mathf.Cos(radian) * dis, Mathf.Sin(radian) * dis);
        public static Vector3 ForwardXZ(float dis, float radian) =>
            new Vector3(Mathf.Sin(radian) * dis, 0, Mathf.Cos(radian) * dis);
    }
}