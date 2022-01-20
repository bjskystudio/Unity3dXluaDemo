using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using YoukiaCore;
using System.Collections.Generic;
using Cinemachine;

public class WorldSetting : MonoBehaviour
{
    public Vector3 Offset = Vector3.zero;
    public Transform ItemRoot;
    private NavMeshDataInstance NavDataInstance;
    public NavMeshData NavMesh;
    public Transform RootTran;
    public Transform MainRoleTransform;
    public CinemachineVirtualCamera MainVirtualCamera;
    public Collider2D[] Collider2DList;
    public Collider[] ColliderList;

    private void Awake()
    {
        if (Launcher.IsInstance())
        {
            Camera[] cameras = gameObject.GetComponentsInChildren<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].enabled = false;
            }
        }
        if (MainVirtualCamera == null)
        {
            MainVirtualCamera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        }
        Init();
    }
    public void SetFollowTransform(Transform trans)
    {
        MainVirtualCamera.Follow = trans;
    }

    /// <summary>
    /// 设定摄像机的bounding volume （区域）
    /// </summary>
    /// <param name="idx">设定需要使用第几个区域，正数减1即为索引，负数为从最后开始倒数，0为最后一个</param>
    /// <returns>是否切换成功</returns>
    public bool SetBoundingVolume(int idx)
    {
        if (this.MainVirtualCamera == null)
        {
            Debug.LogError(" 不存在Cinemachine.CinemachineVirtualCamera类型的相机");
            return false;
        }

        CinemachineConfiner tCinemachineConfiner = this.MainVirtualCamera.GetComponent<CinemachineConfiner>();
        switch (tCinemachineConfiner.m_ConfineMode)
        {
            case CinemachineConfiner.Mode.Confine2D:
                if (this.Collider2DList.Length <= 0)        // 没有设定对应的区域
                {
                    return true;
                }

                Collider2D tCollider2D = this.GetColliderFromList<Collider2D>(this.Collider2DList, idx);
                if (tCollider2D != null && tCollider2D != tCinemachineConfiner.m_BoundingShape2D)
                {
                    tCinemachineConfiner.m_BoundingShape2D = tCollider2D;
                    return true;
                }
                return false;
            case CinemachineConfiner.Mode.Confine3D:
                if (this.ColliderList.Length <= 0)        // 没有设定对应的区域
                {
                    return true;
                }
                Collider tCollider = this.GetColliderFromList<Collider>(this.ColliderList, idx);
                if (tCollider != null && tCollider != tCinemachineConfiner.m_BoundingVolume)
                {
                    tCinemachineConfiner.m_BoundingVolume = tCollider;
                    return true;
                }
                return false;
        }
        return false;
    }
    private T GetColliderFromList<T>(T[] list, int idx) where T : Component
    {
        T tResult = null;
        if (idx == 0)
        {
            tResult = list[list.Length - 1];
            return tResult;
        }

        if (idx < 0)
        {
            idx = list.Length + idx;
        }
        else if (idx > 0)
        {
            idx = idx - 1;
        }

        if (idx >= list.Length)
        {
            idx = list.Length - 1;
        }
        else if (idx < 0)
        {
            idx = 0;
        }

        if (list != null && list.Length > idx)
        {
            tResult = list[idx];
        }

        if (tResult == null)
        {
            Debug.LogError("错误的相机Bounding 索引" + idx);
        }
        return tResult;
    }
    public void OnEnable()
    {
        if (!NavDataInstance.valid)
        {
            NavDataInstance = UnityEngine.AI.NavMesh.AddNavMeshData(NavMesh);
        }
    }
    public void OnDisable()
    {
        if (NavDataInstance.valid)
        {
            UnityEngine.AI.NavMesh.RemoveNavMeshData(NavDataInstance);
        }
    }
    public void Init()
    {
        NavMesh.position = transform.localPosition;
        NavDataInstance = UnityEngine.AI.NavMesh.AddNavMeshData(NavMesh);
    }

    public float GetY(float x, float z)
    {
        if (UnityEngine.AI.NavMesh.SamplePosition(new Vector3(x, 0, z), out var hit, 50, UnityEngine.AI.NavMesh.AllAreas))
        {
            if (hit.hit)
            {
                return hit.position.y;
            }
        }
        return 0;
    }
#if UNITY_EDITOR
    [XLua.BlackList]
    public void SetNavMesh()
    {
        UnityEngine.AI.NavMesh.RemoveAllNavMeshData();
        UnityEngine.AI.NavMesh.AddNavMeshData(NavMesh);
    }
#endif
}
