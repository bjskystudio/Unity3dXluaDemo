using Cinemachine;
using DG.Tweening;
using EngineCenter.Timeline;
using Framework;
using Framework.TimelineExtend;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using YoukiaCore.Utils;
using UObject = UnityEngine.Object;

[XLua.LuaCallCSharp]
public static class CSGoHelp
{
    #region UObject查询定义
    private static GameObject GetGo(UObject target)
    {
        if (target == null)
            throw new Exception("不能传递null");
        var com = target as Component;
        if (com != null)
            return com.gameObject;
        var go = target as GameObject;
        if (go != null)
            return go;
        throw new Exception("无法转化对象" + target.GetType().FullName);
    }

    private static Transform GetTrans(UObject target)
    {
        if (target == null)
            throw new Exception("不能传递null");
        var com = target as Component;
        if (com != null)
            return com.transform;
        var go = target as GameObject;
        if (go != null)
            return go.transform;
        throw new Exception("无法转化对象" + target.GetType().FullName);
    }

    #endregion

    #region 3D、Transform、位置、距离、位移

    /// <summary>
    /// 分别重置位置、旋转、缩放
    /// </summary>
    /// <param name="target"></param>
    public static void ResetTrans(UObject target)
    {
        var trans = GetTrans(target);
        trans.localPosition = Vector3.zero;
        trans.rotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }
    
    public static void SetPosition(UObject target, float x, float y, float z)
    {
        GetTrans(target).position = new Vector3(x, y, z);
    }

    public static void SetLocalPosition(UObject target, float x, float y, float z)
    {
        GetTrans(target).localPosition = new Vector3(x, y, z);
    }

    public static void SetLocalPositionToZero(UObject target)
    {
        GetTrans(target).localPosition = Vector3.zero;
    }

    public static void AddLocalPosition(UObject target, float x, float y, float z)
    {
        var trans = GetTrans(target);
        trans.localPosition = trans.localPosition + new Vector3(x, y, z);
    }

    public static void SetPositionBySeePosition(UObject target, UObject see, float offsetX, float offsetY, float offsetZ)
    {
        var pos = GetTrans(see).position;
        pos.x += offsetX;
        pos.y += offsetY;
        pos.z += offsetZ;
        GetTrans(target).position = pos;
    }

    public static void SetPositionBySeeLocalPosition(UObject target, UObject see, float offsetX, float offsetY, float offsetZ)
    {
        var pos = GetTrans(see).localPosition;
        pos.x += offsetX;
        pos.y += offsetY;
        pos.z += offsetZ;
        GetTrans(target).position = pos;
    }

    public static void SetLocalPositionBySeeLocalPosition(UObject target, UObject see, float offsetX, float offsetY, float offsetZ)
    {
        var pos = GetTrans(see).localPosition;
        pos.x += offsetX;
        pos.y += offsetY;
        pos.z += offsetZ;
        GetTrans(target).localPosition = pos;
    }

    public static void SetLocalPositionBySeePosition(UObject target, UObject see, float offsetX, float offsetY, float offsetZ)
    {
        var pos = GetTrans(see).position;
        pos.x += offsetX;
        pos.y += offsetY;
        pos.z += offsetZ;
        GetTrans(target).localPosition = pos;
    }

    /// <summary>
    /// 设置物体向前移动(有目标位置)，如有有异常，则返回异常类型(0,无异常，1 遇到了空气墙)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="isUseNavMesh"></param>
    /// <returns></returns>
    public static int SetForwardPositionWithTargetPos(UObject target, float speed, float x, float y, float z, bool isUseNavMesh = false)
    {
        var trans = GetTrans(target);
        Vector3 oldpos = trans.position;
        Vector3 targetpos = new Vector3(x, y, z);
        Vector3 dir = targetpos - oldpos;
        dir.y = 0;
        float distance = dir.x * dir.x + dir.z * dir.z;
        distance = Math.Max(0, distance - 0.001f);
        float moveLen = Math.Min(distance, speed * Time.deltaTime);
        Vector3 pos = dir.normalized * moveLen + oldpos;
        if (isUseNavMesh)
        {
            bool isPass = NavMesh.SamplePosition(pos, out NavMeshHit hit, 1, areaMask);
            if (isPass)
            {
                if (trans.tag == "Player")
                {
                    RaycastHit[] rhit = Physics.RaycastAll(oldpos, trans.forward.normalized, Vector3.Distance(oldpos, pos), 1 << LayerMask.NameToLayer("AirDoor"));
                    if (rhit.Length > 0)
                    {
                        return 1;
                    }
                }
                trans.position = hit.position;
            }
        }
        else
        {
            trans.position = pos;
        }
        return 0;
    }

    /// <summary>
    /// 设置物体向前移动，如有有异常，则返回异常类型(0,无异常，1 遇到了空气墙)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    /// <param name="isUseNavMesh"></param>
    /// <returns></returns>
    public static int SetForwardPosition(UObject target, float speed, bool isUseNavMesh = false)
    {
        var trans = GetTrans(target);
        Vector3 oldpos = trans.position;
        Vector3 pos = trans.position + trans.forward * Time.deltaTime * speed;
        if (isUseNavMesh)
        {
            bool isPass = NavMesh.SamplePosition(pos, out NavMeshHit hit, 1, areaMask);
            if (isPass)
            {
                if (trans.tag == "Player")
                {
                    RaycastHit[] rhit = Physics.RaycastAll(oldpos, trans.forward.normalized, Vector3.Distance(oldpos, pos), 1 << LayerMask.NameToLayer("AirDoor"));
                    if (rhit.Length > 0)
                    {
                        return 1;
                    }
                }
                trans.position = hit.position;
            }
        }
        else
        {
            trans.position = pos;
        }
        return 0;
    }

    /// <summary>
    /// 冲刺到目标位置distance处
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <param name="distance">攻击距离+战斗者size</param>
    /// <param name="speedRate">速度s/m</param>
    /// <returns>跑步时间（0为不跑步）</returns>
    public static float RushToTarget(UObject self, UObject target, float distance, float speedRate)
    {
        var selfTran = GetTrans(self);
        var targetTran = GetTrans(target);
        float dis = Vector3.Distance(selfTran.position, targetTran.position);
        if (dis > distance)
        {
            Vector3 pos = targetTran.position + targetTran.forward * distance;
            float time = speedRate * (dis - distance);
            selfTran.DOMove(pos, time);
            return time;
        }
        return 0;
    }

    /// <summary>
    /// 获取物体朝向距离distance的位置
    /// </summary>
    /// <param name="target">目标物体</param>
    /// <param name="distance">距离</param>
    /// <param name="limitObj">限制物体（不超过）</param>
    /// <returns></returns>
    public static float[] GetPosByTargetForwardDistance(UObject target, float distance, float size = 0, UObject limitObj = null)
    {        
        var trans = GetTrans(target);
        Vector3 targetPos = trans.forward * distance + trans.position;
        if (limitObj != null)
        {
            var limitTran = GetTrans(limitObj);
            Vector3 limmitTransPos = limitTran.position;
            limmitTransPos.y = 0;
            Vector3 limitPos = limitTran.forward * size + limmitTransPos;
            if (Vector3.Dot(trans.forward, limitPos - targetPos) <= 0)
            {
                targetPos = limitPos;
            }
        }
        float[] pos = new float[3] { targetPos.x, targetPos.y, targetPos.z};
        return pos;
    }
    /// <summary>
    /// 设置物体朝向
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetForward(UObject target, float x, float y, float z)
    {
        var trans = GetTrans(target);
        trans.forward = new Vector3(x, y, z);
    }

    #endregion

    #region 2D、RectTransform

    /// <summary>
    /// 设置Rect的AnchorPosition
    /// </summary>
    /// <param name="target"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetAnchorPosition(UObject target, float x, float y)
    {
        var targetRect = GetTrans(target) as RectTransform;
        if (targetRect == null)
            return;
        targetRect.anchoredPosition = new Vector2(x, y);
    }

    public static void SetRectTransformZero(UObject target)
    {
        var targetRect = GetTrans(target) as RectTransform;
        if (targetRect == null)
            return;
        targetRect.offsetMin = Vector2.zero;
        targetRect.offsetMax = Vector2.zero;
    }

    public static void SetRectTransform(UObject target, float minX, float minY, float maxX, float maxY)
    {
        var targetRect = GetTrans(target) as RectTransform;
        if (targetRect == null)
            return;
        targetRect.offsetMin = new Vector2(minX, minY);
        targetRect.offsetMax = new Vector2(maxX, maxY);
    }

    public static void SetSizeDelta(UObject target, float width, float height)
    {
        var targetRect = GetTrans(target) as RectTransform;
        if (targetRect == null)
            return;
        targetRect.sizeDelta = new Vector2(width, height);
    }

    public static void SetSizeDeltaMatchTarget(UObject target, UObject parent)
    {
        var targetRect = GetTrans(target) as RectTransform;
        var parentRect = GetTrans(parent) as RectTransform;
        if (targetRect && parentRect)
        {
            targetRect.sizeDelta = parentRect.sizeDelta;
        }
    }

    #endregion

    #region 缩放

    public static void SetLocalScale(UObject target, float x, float y, float z)
    {
        GetTrans(target).localScale = new Vector3(x, y, z);
    }

    public static void SetLocalScaleXYZ(UObject target, float s)
    {
        GetTrans(target).localScale = new Vector3(s, s, s);
    }

    public static void SetLocalScaleToOne(UObject target)
    {
        GetTrans(target).localScale = Vector3.one;
    }

    #endregion

    #region 旋转、弧度

    public static Quaternion GetLookCameraRotation(UObject target)
    {
        var trans = GetTrans(target);
        var forward = trans.forward;
        var up = trans.up;
        return Quaternion.LookRotation(forward, up);
    }

    public static void SetRotationLookCamera(UObject target, UObject viewCamera)
    {
        GetTrans(target).rotation = GetLookCameraRotation(viewCamera);
    }

    public static void SetRotationToIdentity(UObject target)
    {
        GetTrans(target).rotation = Quaternion.identity;
    }

    public static void SetLocalRotationToIdentity(UObject target)
    {
        GetTrans(target).localRotation = Quaternion.identity;
    }

    public static void SetRotation(UObject target, float x, float y, float z, float w)
    {
        GetTrans(target).rotation = new Quaternion(x, y, z, w);
    }

    public static void SetLocalRotation(UObject target, float x, float y, float z, float w)
    {
        GetTrans(target).localRotation = new Quaternion(x, y, z, w);
    }

    public static void SetEulerAngles(UObject target, float x, float y, float z)
    {
        GetTrans(target).eulerAngles = new Vector3(x, y, z);
    }

    public static void SetLocalEulerAngles(UObject target, float x, float y, float z)
    {
        GetTrans(target).localEulerAngles = new Vector3(x, y, z);
    }

    /// <summary>
    /// 转身朝向对象
    /// </summary>
    /// <param name="self"> 自身 </param>
    /// <param name="target"> 朝向的对象 </param>
    public static void SetTowardsToTarget(UObject self, UObject target)
    {
        var selfTrans = GetTrans(self);
        var targetTrans = GetTrans(target);
        var tmp = selfTrans.eulerAngles;
        tmp.y = MathEx.AngleXZ(selfTrans.position, targetTrans.position);
        selfTrans.eulerAngles = tmp;
    }

    /// <summary>
    /// 旋传一个f的度数值算坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="cx"></param>
    /// <param name="cz"></param>
    /// <param name="f"></param>
    /// <param name="rx"></param>
    /// <param name="rz"></param>
    public static void GetOffValue(float x, float z, float cx, float cz, int f, out float rx, out float rz)
    {
        double fd = Math.PI / 180 * f;
        Vector2 rv = RotatePoint(new Vector2(x, z), new Vector2(cx, cz), fd, true);

        rx = rv.x;
        rz = rv.y;
    }

    /// <summary>
    /// 旋转一个固定比例的弧度得到一个坐标值，一般用于不动的坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="cx"></param>
    /// <param name="cz"></param>
    /// <param name="f"></param>
    /// <param name="rx"></param>
    /// <param name="rz"></param>
    public static void GetRotatePoint(float x, float z, float cx, float cz, int f, out float rx, out float rz)
    {
        Vector2 rv = RotatePoint(new Vector2(x, z), new Vector2(cx, cz), Math.PI / 4 * f);
        rx = rv.x;
        rz = rv.y;
    }

    /// <summary>
    /// 返回当前点围绕中心点旋转弧度rad后的坐标
    /// </summary>
    /// <param name="nowPos">当前坐标点</param>
    /// <param name="roCenterPos">中心坐标点，但一般都是传0</param>
    /// <param name="rad">旋转弧度, 默认45度</param>
    /// <param name="isClockwise">true:顺时针/false:逆时针</param>
    /// <returns>旋转后坐标</returns>
    private static Vector2 RotatePoint(Vector2 nowPos, Vector2 roCenterPos, double rad = Math.PI / 4 * 1, bool isClockwise = false)
    {
        //当前点值
        Vector2 nowP = new Vector2(nowPos.x - roCenterPos.x, nowPos.y - roCenterPos.y);
        //当前点和中心点的距离
        double len = DistanceTo(nowP, new Vector2(0, 0));
        //当前值弧度
        double nowR = RadPOX(nowP.x, nowP.y);
        //得到旋转弧度rad后的弧度
        double endR = nowR - (isClockwise ? 1 : -1) * rad;
        //旋转后的点
        Vector2 endPos = new Vector2(
            (float)(len * Math.Cos(endR)),
            (float)(len * Math.Sin(endR))
            );
        //根据传入中心点算最终点g
        return new Vector2(endPos.x + roCenterPos.x, endPos.y + roCenterPos.y);
    }

    /// <summary>
    /// 计算点P(x,y)与X轴正方向的夹角
    /// </summary>
    /// <param name="x">横坐标</param>
    /// <param name="y">纵坐标</param>
    /// <returns>夹角弧度</returns>
    private static double RadPOX(double x, double y)
    {
        //P在(0,0)的情况
        if (x == 0 && y == 0) return 0;

        //P在四个坐标轴上的情况：x正、x负、y正、y负
        if (y == 0 && x > 0) return 0;
        if (y == 0 && x < 0) return Math.PI;
        if (x == 0 && y > 0) return Math.PI / 2;
        if (x == 0 && y < 0) return Math.PI / 2 * 3;

        //点在第一、二、三、四象限时的情况
        if (x > 0 && y > 0) return Math.Atan(y / x);
        if (x < 0 && y > 0) return Math.PI - Math.Atan(y / -x);
        if (x < 0 && y < 0) return Math.PI + Math.Atan(-y / -x);
        if (x > 0 && y < 0) return Math.PI * 2 - Math.Atan(-y / x);

        return 0;
    }

    /// <summary>
    /// 两点距离(Vector2)
    /// </summary>
    /// <param name="t">当前点</param>
    /// <param name="p">中心点</param>
    /// <returns></returns>
    private static double DistanceTo(Vector2 t, Vector2 p)
    {
        return Math.Sqrt((p.x - t.x) * (p.x - t.x) + (p.y - t.y) * (p.y - t.y));
    }

    #endregion

    #region NavMesh

    private static readonly int areaMask = NavMesh.AllAreas;
    /// <summary>
    /// 计算目标和目的地之间是否可达
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="x">目的地x</param>
    /// <param name="y">目的地y</param>
    /// <param name="z">目的地z</param>
    /// <param name="navNodes">最终路径</param>
    /// <param name="istoclose">是否过于靠近</param>
    /// <returns></returns>
    public static bool CalculatePath(UObject target, float x, float y, float z, NavMeshPath navNodes, out bool istoclose)
    {
        var trans = GetTrans(target);
        return CalculatePathByPos(trans.position.x, trans.position.y, trans.position.z, x, y, z, navNodes, out istoclose);
    }

    public static bool CalculatePathByPos(float targetx, float targety, float targetz, float x, float y, float z, NavMeshPath navNodes, out bool istoclose)
    {
        Vector3 targetPos = new Vector3(targetx, targety, targetz);
        istoclose = false;
        Vector3 point = new Vector3(x, y, z);
        var result = NavMesh.SamplePosition(point, out NavMeshHit hit, 1, areaMask);
        if (result == false)
        {
            return false;
        }
        point = new Vector3(x, hit.position.y, z);
        bool state = NavMesh.CalculatePath(targetPos, point, areaMask, navNodes);
        if (state)
        {
            if ((targetPos - navNodes.corners[navNodes.corners.Length - 1]).sqrMagnitude > 0.5)
            {
                return true;
            }
            else
            {
                istoclose = true;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判断是否处于某个寻路层上
    /// </summary>
    /// <param name="worldPos"></param>
    /// <param name="areaName">寻路层名称</param>
    /// <returns></returns>
    public static bool IsOnArea(float x, float y, float z, string areaName)
    {
        Vector3 fix = GetSamplePosition(x, y, z, 4);
        int layer = NavMesh.GetAreaFromName(areaName);
        bool isOn = NavMesh.SamplePosition(fix, out NavMeshHit hit, 1f, 1 << layer);
        return isOn;
    }

    /// <summary>
    /// 得到指定位置在哪个层
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static int GetPositionAreaLayer(float x, float y, float z)
    {
        bool isOn = NavMesh.SamplePosition(new Vector3(x, y, z), out NavMeshHit hit, 1f, NavMesh.AllAreas);
        return isOn ? hit.mask : 0;
    }

    /// <summary>
    /// 通过采样位置，发现最近的点时矫正位置（适用于地图有位置变化的情况）
    /// </summary>
    /// <param name="target"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static int SamplePosition(UObject target, float x, float y, float z)
    {
        var trans = GetTrans(target);
        Vector3 point = new Vector3(x, y, z);
        var result = NavMesh.SamplePosition(point, out NavMeshHit hit, 2, areaMask);
        if (!result)
        {
            return -1;
        }
        point = new Vector3(x, hit.position.y, z);
        trans.position = point;
        return hit.mask;
    }

    /// <summary>
    /// 采样位置,是否发现最近的点
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static bool BeSamplePosition(float x, float y, float z)
    {
        Vector3 point = new Vector3(x, y, z);
        var result = NavMesh.SamplePosition(point, out NavMeshHit hit, 1, areaMask);
        return result;
    }

    /// <summary>
    /// 取得修正位置
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="dis"></param>
    /// <returns></returns>
    public static Vector3 GetSamplePosition(float x, float y, float z, float dis = 1)
    {
        Vector3 point = new Vector3(x, y, z);
        var result = NavMesh.SamplePosition(point, out NavMeshHit hit, dis, areaMask);
        if (!result)
        {
            Debug.LogError("位置矫正失败x" + x + "y" + y + "z" + z);
            return point;
        }
        point = new Vector3(x, hit.position.y, z);
        return point;
    }
    public static Vector3 GetSamplePosition(Vector3 point)
    {
        NavMeshHit hit;
        var result = NavMesh.SamplePosition(point, out hit, 4, areaMask);
        if (!result)
        {
            return point;
        }
        return hit.position;
    }

    /// <summary>
    /// 修正目标位置
    /// </summary>
    /// <param name="target">目标</param>
    public static void AdjustPosition(UObject target)
    {
        var trans = GetTrans(target);
        Vector3 point = trans.position;
        var result = NavMesh.SamplePosition(point, out NavMeshHit hit, 30, areaMask);
        if (!result)
        {
            return;
        }
        trans.position = hit.position;
    }
    #endregion

    #region DOTween

    public static void PlayStep(UObject target, DOTweenPath path, float time, Vector3 offset, Action endCall)
    {
        var trans = GetTrans(target);
        trans.position = path.transform.position;
        var point = path.wps.ToArray();
        var start = point[0];
        var end = point[point.Length - 1];

        for (int i = 0; i < point.Length; i++)
        {
            point[i] += offset;
        }

        var tweener = trans.DOPath(point, time, PathType.CatmullRom);
        if (endCall != null)
        {
            tweener.onComplete = (() =>
            {
                endCall();
            });
        }
    }

    /// <summary>
    /// dotween path 贝塞尔曲线移动
    /// </summary>
    /// <param name="target">目标</param>
    /// <param name="aabb">空气墙:minx,miny,minz,maxx,maxy,maxz</param>
    /// <param name="points">点位:{endx,endy,endz}</param>
    /// <param name="offsetY">中间点偏移</param>
    /// <param name="segmentNum">采样点数量</param>
    /// <param name="time">时间</param>
    /// <param name="endCall">回调</param>
    public static Tweener PlayCurvePath(UObject target, float[] points, float offsetY, int segmentNum, float time, Action endCall, float[] aabb = null, int easeIndex = 0)
    {
        var trans = GetTrans(target);
        Vector3 start = trans.position;
        Vector3 end = new Vector3(points[0], points[1], points[2]);
        Vector3 center = (start + end) * 0.5f;
        center.y += offsetY;
        Vector3[] vecPoints = GetBeizerList(start, center, end, segmentNum);
        if (aabb != null)
        {
            Vector3 aabbMin = new Vector3(aabb[0], aabb[1], aabb[2]);
            Vector3 aabbMax = new Vector3(aabb[3], aabb[4], aabb[5]);
            for (int i = 0; i < vecPoints.Length; i++)
            {
                VectorClamp(ref vecPoints[i], ref aabbMin, ref aabbMax);
            }
        }    
        var tweener = trans.DOPath(vecPoints, time, PathType.CatmullRom);
        if (endCall != null)
        {
            tweener.onComplete = (() =>
            {
                endCall();
            });
        }
        if (easeIndex > 0)
        {
            tweener.SetEase((Ease)easeIndex);
        }
        return tweener;
    }

    /// <summary>
    /// dotween path 移动
    /// </summary>
    /// <param name="target"></param>
    /// <param name="points"></param>
    /// <param name="time"></param>
    /// <param name="endCall"></param>
    /// <returns></returns>
    public static Tweener DoPath(UObject target, float[] points, float time, Action endCall)
    {
        var trans = GetTrans(target);
        int count = points.Length / 3;
        Vector3[] vecPoints = new Vector3[count];
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            index = i * 3;
            vecPoints[i] = new Vector3(points[index], points[index + 1], points[index + 2]);
        }
        var tweener = trans.DOPath(vecPoints, time, PathType.CatmullRom);
        if (endCall != null)
        {
            tweener.onComplete = (() =>
            {
                endCall();
            });
        }
        return tweener;
    }

    /// <summary>
    /// 执行DoTween移动
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetx"></param>
    /// <param name="targety"></param>
    /// <param name="targetz"></param>
    /// <param name="endcall"></param>
    /// <param name="duration"></param>
    /// <param name="snapping"></param>
    /// <returns></returns>
    public static Tweener DoMove(UObject target, float targetx, float targety, float targetz, TweenCallback endcall = null, float duration = 0, bool snapping = false, int easeIndex = 0)
    {
        var endvalue = new Vector3(targetx, targety, targetz);
        var tweener = GetTrans(target).DOMove(endvalue, duration, snapping);
        if (endcall != null)
        {
            tweener.onComplete = endcall;
        }
        if (easeIndex > 0)
        {
            tweener.SetEase((Ease)easeIndex);
        }      
        return tweener;
    }

    /// <summary>
    /// 执行DoTween局部坐标移动
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetx"></param>
    /// <param name="targety"></param>
    /// <param name="targetz"></param>
    /// <param name="endcall"></param>
    /// <param name="duration"></param>
    /// <param name="snapping"></param>
    /// <returns></returns>
    public static Tweener DOLocalMove(UObject target, float targetx, float targety, float targetz, TweenCallback endcall, float duration = 0, bool snapping = false)
    {
        var endvalue = new Vector3(targetx, targety, targetz);
        var tweener = GetTrans(target).DOLocalMove(endvalue, duration, snapping);
        tweener.onComplete = endcall;
        return tweener;
    }

    /// <summary>
    /// 执行DOScale缩放
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetScale"></param>
    /// <param name="duration"></param>
    /// <param name="endcall"></param>
    /// <returns></returns>
    public static Tweener DOScale(UObject target, float targetScale, float duration, TweenCallback endcall)
    {
        var tweener = GetTrans(target).DOScale(targetScale, duration);
        tweener.onComplete = endcall;
        return tweener;
    }

    /// <summary>
    /// 执行DoTween旋转
    /// </summary>
    /// <param name="target"></param>
    /// <param name="quaternionx"></param>
    /// <param name="quaterniony"></param>
    /// <param name="quaternionz"></param>
    /// <param name="quaternionw"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Tweener DORotateQuaternion(UObject target, float quaternionx, float quaterniony, float quaternionz, float quaternionw, float duration, TweenCallback endcall)
    {
        var targetquaternion = new Quaternion(quaternionx, quaterniony, quaternionz, quaternionw);
        var tweener = GetTrans(target).DORotateQuaternion(targetquaternion, duration);
        tweener.onComplete = endcall;
        return tweener;
    }
    /// <summary>
    /// 执行DoTween摄像机FOV
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetFov"></param>
    /// <param name="duration"></param>
    /// <param name="endcall"></param>
    /// <returns></returns>
    public static Tweener DOFieldOfView(UObject target, float targetFov, float duration, TweenCallback endcall)
    {
        var camera = GetTrans(target).GetComponent<Camera>();
        if (camera == null)
        {
            Debug.LogError("找不到Camera");
            return null;
        }
        var tweener = camera.DOFieldOfView(targetFov, duration);
        tweener.onComplete = endcall;
        return tweener;
    }
    /// <summary>
    /// 目标节点改变Alpha动画
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetAlpha"></param>
    /// <param name="duration"></param>
    /// <param name="endcall"></param>
    /// <returns></returns>
    public static Tweener DOFade(UObject target, float targetAlpha, float duration, TweenCallback endcall)
    {
        var tweener = GetTrans(target).GetOrAddComponent<CanvasGroup>().DOFade(targetAlpha, duration);
        tweener.onComplete = endcall;
        return tweener;
    }

    #endregion

    #region Animator

    public static void SetAnimatorTrigger(UObject model, string key)
    {
        Animator animator = GetGo(model).GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(key);
        }
    }

    /// <summary>
    /// 更改模型的裁剪模式
    /// </summary>
    /// <param name="model"></param>
    /// <param name="mode">AnimatorCullingMode</param>
    public static void ChangeModelCullingMode(UObject model, int mode)
    {
        Animator modelAt = GetGo(model).GetComponent<Animator>();
        if (modelAt == null)
        {
            Debug.LogError("没有找到Animator:" + model.name);
            return;
        }
        modelAt.cullingMode = (AnimatorCullingMode)mode;
    }

    #endregion

    #region 实例化、销毁

    public static GameObject Instantiate(UObject target, UObject parent)
    {
        var go = GetGo(target);
        var parentTrans = GetTrans(parent);
        if (parentTrans != null)
        {
            return UObject.Instantiate(go, Vector3.zero, Quaternion.identity, parentTrans);
        }
        return UObject.Instantiate(go, Vector3.zero, Quaternion.identity);
    }

    public static void Destroy(UObject target, float time = 0)
    {
        GetGo(target).DestroyGameObj();
    }

    public static void DestroyAllChildren(UObject target)
    {
        GetTrans(target).ClearChildren();
    }

    #endregion

    #region UGUI、Aor

    /// <summary>
    /// 设置节点下所有的可置灰的状态
    /// </summary>
    /// <param name="target">节点</param>
    /// <param name="value">是否置灰:非0置灰</param>
    public static void SetGaryWithAllChildren(UObject target, int value)
    {
        GrayManager.SetGaryWithAllChildren(GetTrans(target), value != 0);
    }

    #endregion

    #region Cinemachine相机
    /// <summary>
    /// 设置相机跟随组
    /// </summary>
    /// <param name="group"></param>
    /// <param name="targetList"></param>
    public static void SetTargetGroup(UObject group, UObject[] targetList)
    {
        CinemachineTargetGroup targetGroup = GetTrans(group).GetComponent<CinemachineTargetGroup>();
        if (targetGroup == null)
        {
            Debug.LogError("没有找到CinemachineTargetGroup:" + group.name);
            return;
        }
        if (targetGroup.m_Targets == null || targetGroup.m_Targets.Length != targetList.Length)
        {
            targetGroup.m_Targets = new CinemachineTargetGroup.Target[targetList.Length];
        }
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            targetGroup.m_Targets[i].target = GetTrans(targetList[i]);
            targetGroup.m_Targets[i].weight = 1;
            targetGroup.m_Targets[i].radius = 0;
        }
    }

    /// <summary>
    /// 设置相机跟随和看向目标
    /// </summary>
    /// <param name="target"></param>
    /// <param name="look">看向目标</param>
    /// <param name="follow">跟随目标</param>
    public static void SetCameraLookAndFollow(UObject target, UObject look = null, UObject follow = null)
    {
        CinemachineVirtualCamera virtualCamera = GetTrans(target).GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("没有找到CinemachineVirtualCamera:" + target.name);
            return;
        }
        if (look == null)
        {
            virtualCamera.LookAt = null;
        }
        else
        {
            virtualCamera.LookAt = GetTrans(look);
        }
        if (follow == null)
        {
            virtualCamera.Follow = null;
        }
        else
        {
            virtualCamera.Follow = GetTrans(follow);
        }

    }

    /// <summary>
    /// 设置相机默认移动融合
    /// </summary>
    public static void SetCameraDefaultBlend(UObject target, int style, float time)
    {
        CinemachineBrain brain = GetTrans(target).GetComponent<CinemachineBrain>();
        if (brain == null)
        {
            Debug.LogError("没有找到CinemachineBrain:" + target.name);
            return;
        }
        brain.m_DefaultBlend.m_Style = (CinemachineBlendDefinition.Style)style;
        brain.m_DefaultBlend.m_Time = time;
    }

    /// <summary>
    /// 添加栈相机渲染
    /// </summary>
    /// <param name="mainCamObj"></param>
    /// <param name="addCamObj"></param>
    public static void CameraAddStack(UObject mainCamObj, UObject addCamObj)
    {
        if (CNPRPipeline.asset != null)
        {
            CAdditionalCameraData mainCam = GetGo(mainCamObj).GetOrAddComponent<CAdditionalCameraData>();
            if (mainCam == null)
            {
                Debug.LogError("没有找到CAdditionalCameraData:" + mainCamObj.name);
                return;
            }
            Camera addCam = GetTrans(addCamObj).GetComponent<Camera>();
            if (addCam == null)
            {
                Debug.LogError("没有找到Camera:" + addCamObj.name);
                return;
            }
            mainCam.cameraStack.Add(addCam);
        }else if (UniversalRenderPipeline.asset != null)
        {
            UniversalAdditionalCameraData mainCam = GetGo(mainCamObj).GetOrAddComponent<UniversalAdditionalCameraData>();
            if (mainCam == null)
            {
                Debug.LogError("没有找到UniversalAdditionalCameraData:" + mainCamObj.name);
                return;
            }
            Camera addCam = GetTrans(addCamObj).GetComponent<Camera>();
            if (addCam == null)
            {
                Debug.LogError("没有找到Camera:" + addCamObj.name);
                return;
            }
            mainCam.cameraStack.Add(addCam);
        }
    }

    /// <summary>
    /// 设置相机震动效果
    /// </summary>
    /// <param name="target">目标相机</param>
    /// <param name="RawSignal">震动资源</param>
    /// <param name="Amplitude">振幅</param>
    /// <param name="Frequency">频率</param>
    /// <param name="Time">时间</param>
    /// <param name="DecayTime">衰减时间</param>
    public static void SetCameraImpulse(UObject target, string RawSignalPath, float Amplitude, float Frequency, float Time, float DecayTime)
    {
        CinemachineImpulseSource impulseSource = GetTrans(target).GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
        {
            Debug.LogError("没有找到CinemachineImpulseSource:" + target.name);
            return;
        }
        AssetLoadManager.Instance.LoadObj(RawSignalPath, AssetLoadType.eScriptableObject, false, (tran, res) =>
        {
            SignalSourceAsset RawSignal = tran as SignalSourceAsset;
            impulseSource.m_ImpulseDefinition.m_RawSignal = RawSignal;
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain = Amplitude;
            impulseSource.m_ImpulseDefinition.m_FrequencyGain = Frequency;
            impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = Time;
            impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = DecayTime;
            impulseSource.GenerateImpulse();
        });      
    }

    #endregion

    #region 未归类、控制、设置、层级、组合用法

    public static void SetActive(UObject target, int value)
    {
        GetGo(target).SetActive(value != 0);
    }

    public static void SetParent(UObject target, UObject parent, int worldPositionStays = 0)
    {
        GetTrans(target).SetParent(GetTrans(parent), worldPositionStays != 0);
    }

    public static void SetLayer(UObject target, string layerName, string ignoreLayerName)
    {
        GetTrans(target).SetLayer(layerName, ignoreLayerName);
    }

    /// <summary>
    /// 添加指定的组件
    /// </summary>
    /// <param name="target"> 自身 </param>
    /// <param name="type"> 组件类型 </param>
    public static Component AddComponent(UObject target, Type type)
    {
        return GetGo(target).GetOrAddComponent(type);
    }

    public static Transform GetChild(UObject target, int index)
    {
        return GetTrans(target).GetChild(index);
    }

    public static void SetChildrenActiveNumber(UObject target, int showCount)
    {
        if (showCount < 0)
        {
            Debug.LogError("显示数量不能小于0");
            return;
        }
        var trans = GetTrans(target);
        int childCount = trans.childCount;
        if (0 == childCount)
        {
            return;
        }

        Transform tranChild = trans.GetChild(0);
        GameObject instanceObj = tranChild.gameObject, tempGo;
        string objName = instanceObj.name;
        Vector3 localScale = tranChild.localScale;

        var maxCount = Mathf.Max(showCount, childCount);
        for (int i = 0; i < maxCount; i++)
        {
            if (i < childCount)
            {
                trans.GetChild(i).gameObject.SetActive(i < showCount);
            }
            else
            {
                tempGo = GameObject.Instantiate(instanceObj, trans, false);
                tempGo.name = objName;
                tempGo.transform.localScale = localScale;
                tempGo.SetActive(true);
            }
        }
    }
    #endregion

    #region Spine控制器
    /// <summary>
    /// 设置Spine的Alpha值
    /// </summary>
    /// <param name="target"></param>
    /// <param name="alpha"></param>
    public static void SetSpineAlpha(UObject target,float alpha)
    {
        SpineCtrl ctrl = GetTrans(target).GetComponent<SpineCtrl>();
        if (ctrl == null)
        {
            Debug.LogError("没有找到SpineCtrl:" + target.name);
            return;
        }
        ctrl.SetAlpha(alpha);
    }

    /// <summary>
    /// 设置Spine的Alpha值
    /// </summary>
    /// <param name="target"></param>
    /// <param name="alpha"></param>
    /// <param name="time"></param>
    public static void SetSpineAlphaWithTime(UObject target, float alpha,float time)
    {
        SpineCtrl ctrl = GetTrans(target).GetComponent<SpineCtrl>();
        if (ctrl == null)
        {
            Debug.LogError("没有找到SpineCtrl:" + target.name);
            return;
        }
        ctrl.SetAlphaWithTime(alpha,time);
    }
    #endregion

    #region TimeLine
    /// <summary>
    /// 设置timeline钩子
    /// </summary>
    /// <param name="call"></param>
    public static void SetTimeLineHook(Func<string, GameObject> call = null)
    {
        TimelineExtend.LoadDynamicCharacterHook = call;
    }
    #endregion

    #region 战斗
    /// <summary>
    /// 模型半透明消失
    /// </summary>
    /// <param name="target"></param>
    /// <param name="endAlpha"></param>
    /// <param name="time"></param>
    /// <param name="callBack"></param>
    public static void HideModel(UObject target, CallBack callBack = null, float endAlpha = 0, float time = 0.5f)
    {
        var trans = GetTrans(target);
        var t = trans.GetComponentInChildren<AfterImageCreator>();
        if (t != null) t.HideAllAfterImage();
        Renderer[] renderers = trans.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            if (renderer.material.HasProperty("_srcBlend"))
            {
                renderer.material.SetFloat("_srcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
            }
            if (renderer.material.HasProperty("_dstBlend"))
            {
                renderer.material.SetFloat("_dstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            }
            if (renderer.material.HasProperty("_srcAlphaBlend"))
            {
                renderer.material.SetFloat("_srcAlphaBlend", (float)UnityEngine.Rendering.BlendMode.One);
            }
            if (renderer.material.HasProperty("_dstAlphaBlend"))
            {
                renderer.material.SetFloat("_dstAlphaBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            }
            //TA要求 这里需要调整到3000 否则表现会出问题
            renderer.material.renderQueue = 3000;
            if (renderer.material.HasProperty("_AlphaCtrl"))
            {
                if (time > 0)
                {
                    DOTween.To(() => renderer.material.GetFloat("_AlphaCtrl"), curr => { renderer.material.SetFloat("_AlphaCtrl", curr); }, endAlpha, time).onComplete = () =>
                    {
                        if (renderer.material.HasProperty("_StencilOp"))
                        {
                            renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Keep);
                        }
                        if (renderer.material.HasProperty("_ZWriteCtrl"))
                        {
                            renderer.material.SetFloat("_ZWriteCtrl", 0f);
                        }
                        if (callBack != null)
                        {
                            callBack.Invoke();
                        }
                    };
                }
                else
                {
                    renderer.material.SetFloat("_AlphaCtrl", endAlpha);
                    if (renderer.material.HasProperty("_StencilOp"))
                    {
                        renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Keep);
                    }
                    if (renderer.material.HasProperty("_ZWriteCtrl"))
                    {
                        renderer.material.SetFloat("_ZWriteCtrl", 0f);
                    }
                    if (callBack != null)
                    {
                        callBack.Invoke();
                    }
                }

            }
        }
    }

    /// <summary>
    /// 模型显示
    /// </summary>
    /// <param name="target"></param>
    /// <param name="showShadow">是否显示残影</param>
    public static void ShowModel(UObject target, bool showShadow = false)
    {
        var trans = GetTrans(target);
        if (showShadow)
        {
            var t = trans.GetComponentInChildren<AfterImageCreator>();
            if (t != null) t.IsCreate = true;
        }
        Renderer[] renderers = trans.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            if (renderer.material.HasProperty("_srcBlend"))
            {
                renderer.material.SetFloat("_srcBlend", (float)UnityEngine.Rendering.BlendMode.One);
            }
            if (renderer.material.HasProperty("_dstBlend"))
            {
                renderer.material.SetFloat("_dstBlend", (float)UnityEngine.Rendering.BlendMode.Zero);
            }
            if (renderer.material.HasProperty("_srcAlphaBlend"))
            {
                renderer.material.SetFloat("_srcAlphaBlend", (float)UnityEngine.Rendering.BlendMode.One);
            }
            if (renderer.material.HasProperty("_dstAlphaBlend"))
            {
                renderer.material.SetFloat("_dstAlphaBlend", (float)UnityEngine.Rendering.BlendMode.Zero);
            }
            renderer.material.renderQueue = 2200;
            if (renderer.material.HasProperty("_AlphaCtrl"))
            {
                renderer.material.SetFloat("_AlphaCtrl", 1);
            }
            if (renderer.material.HasProperty("_StencilOp"))
            {
                renderer.material.SetFloat("_StencilOp", (float)UnityEngine.Rendering.StencilOp.Replace);
            }
            if (renderer.material.HasProperty("_ZWriteCtrl"))
            {
                renderer.material.SetFloat("_ZWriteCtrl", 1f);
            }
        }
    }

    /// <summary>
    /// 显示残影
    /// </summary>
    /// <param name="show">是否显示</param>
    /// <param name="target">目标</param>
    public static void ShowShadow(UObject target, bool show)
    {
        var go = GetGo(target);
        AfterImageCreator afterImage = go.GetComponent<AfterImageCreator>();
        if (afterImage != null)
        {
            afterImage.IsCreate = show;
        }
        else
        {
            Debug.LogError("没有找到AfterImageCreator:" + target.name);
        }
    }

    /// <summary>
    /// 设置剪辑镜像
    /// </summary>
    public static void SetTimeLineMirror(UObject target, bool IsMirror)
    {
        TimelineMirror mirror = GetTrans(target).GetComponent<TimelineMirror>();
        if (mirror == null)
        {
            Debug.LogError("没有找到TimelineMirror:" + target.name);
            return;
        }
        mirror.mIsMirror = IsMirror;
    }

    /// <summary>
    /// 设置角色镜像
    /// </summary>
    public static void SetRoleMirror(UObject target, bool IsMirror)
    {
        MirrorUtility.SwitchMirror(MirrorUtility.AddMirrorComponent(GetGo(target)), IsMirror);
    }

    #endregion

    #region 内部方法
    /// <summary>
    /// 获取存储贝塞尔曲线点的数组二次贝塞尔
    /// </summary>
    /// <param name="startPoint"></param>起始点
    /// <param name="controlPoint"></param>控制点
    /// <param name="endPoint"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量
    /// <returns></returns>存储贝塞尔曲线点的数组
    private static Vector3[] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum + 1];
        path[0] = startPoint;
        for (int i = 1; i <= segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculateCubicBezierPoint(t, startPoint,
                controlPoint, endPoint);
            path[i] = pixel;
        }
        return path;
    }

    /// <summary>
    /// 根据T值，计算贝塞尔曲线上面相对应的点
    /// </summary>
    /// <param name="t"></param>T值
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点
    /// <param name="p2"></param>目标点
    /// <returns></returns>根据T值计算出来的贝赛尔曲线点
    private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    private static void VectorClamp(ref Vector3 value, ref Vector3 min, ref Vector3 max)
    {
        value.x = Mathf.Clamp(value.x, min.x, max.x);
        value.y = Mathf.Clamp(value.y, min.y, max.y);
        value.z = Mathf.Clamp(value.z, min.z, max.z);
    }
    #endregion
}
