using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace YoukiaEngine
{
    public class GeometryInstancingManager : MonoBehaviour
    {
        private static GeometryInstancingManager _instance;

        public static GeometryInstancingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject root = new GameObject("GeometryInstancingManager");
                    DontDestroyOnLoad(root);
                    _instance = root.AddComponent<GeometryInstancingManager>();
                }
                return _instance;
            }
        }

        public static bool IsOK()
        {
            return (_instance != null);
        }

        public int ObjectCount;
        public int VisibleObjectCount;

        private Camera _renderCamera;

        public Camera RenderCamera
        {
            get { return _renderCamera ?? Camera.main; }
            //get { return null; }
            set { _renderCamera = value; }
        }

        public Plane[] CameraPlanes = new Plane[6];

        //public Camera ShadowVsmCamera;
        private DynamicArray<GeometryInstancingGroup> _groups = new DynamicArray<GeometryInstancingGroup>();

        [HideInInspector]
        public uint BoundCheckCode = 0;

        public void ForceUpdateAllGeometryInstancingGroup()
        {
            int count = GroupCount;
            for (int i = 0; i < count; i++)
            {
                GeometryInstancingGroup group = GetGroup(i);
                if (group is ImpostorInstancingGroup)
                {
                    ImpostorInstancingGroup ig = (ImpostorInstancingGroup)@group;
                    ig.RefreshImpostor();
                }
            }
        }

        private void UpdateBoundCheckCode()
        {
            BoundCheckCode++;
            if (BoundCheckCode == uint.MaxValue)
            {
                BoundCheckCode = 1;
            }
        }

        protected void LateUpdate()
        {
            if (RenderCamera == null) return;
            ObjectCount = 0;
            VisibleObjectCount = 0;

            UpdateBoundCheckCode();

            Matrix4x4 worldToPorjMatrix = RenderCamera.projectionMatrix * RenderCamera.worldToCameraMatrix;
            GPUSkinningUtils.CalculateFrustumPlanes(CameraPlanes, ref worldToPorjMatrix);

            //            CameraPlanes = GeometryUtility.CalculateFrustumPlanes(RenderCamera);

            int groupCount = _groups.Count;
            for (int i = 0; i < groupCount; i++)
            {
                _groups[i].Update();
                ObjectCount += _groups[i].ObjectCount;
                VisibleObjectCount += _groups[i].VisibleObjectCount;
            }

            for (int i = 0; i < groupCount; ++i)
            {
                _groups[i].Render();
            }
        }

        public void AddGroup(GeometryInstancingGroup group)
        {
            if (group != null)
            {
                if (group.Parent == null)
                {
                    group.Parent = this;
                    _groups.Add(group);
                }
            }
            else
            {
                Debug.LogError("<GeometryInstancingManager:AddGroup>group is null!");
            }
        }

        public void RemoveGroup(GeometryInstancingGroup group)
        {
            if (group != null && group.Parent == this)
            {
                group.Parent = null;
                _groups.QuickRemove(group);
            }
        }

        public void ClearGroups()
        {
            int groupCount = _groups.Count;
            for (int i = 0; i < groupCount; i++)
            {
                _groups[i].Parent = null;
            }
            _groups.Clear();
        }

        public int GroupCount
        {
            get { return _groups.Count; }
        }

        public GeometryInstancingGroup GetGroup(int index)
        {
            return _groups[index];
        }

        /// <summary>
        /// 判断group是否已经添加到当前管理器
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool CheckGroup(GeometryInstancingGroup group)
        {
            if (group != null && group.Parent == this)
            {
                return true;
            }
            return false;
        }

        public void OnDestroy()
        {
            //            Destroy(gameObject);
            _instance = null;
        }

        public void SetVisible(bool bVisible)
        {
            int groupCount = _groups.Count;
            for (int i = 0; i < groupCount; i++)
            {
                _groups[i].SetVisible(bVisible);
            }
        }

        // manual render
        public void ManualRender(Camera camera)
        {
            _renderCamera = camera;

            if (RenderCamera == null) return;
            ObjectCount = 0;
            VisibleObjectCount = 0;

            UpdateBoundCheckCode();
            Matrix4x4 worldToPorjMatrix = RenderCamera.projectionMatrix * RenderCamera.worldToCameraMatrix;
            GPUSkinningUtils.CalculateFrustumPlanes(CameraPlanes, ref worldToPorjMatrix);

            //            CameraPlanes = GeometryUtility.CalculateFrustumPlanes(RenderCamera);

            int groupCount = _groups.Count;
            for (int i = 0; i < groupCount; i++)
            {
                _groups[i].ManualRender(_renderCamera);
                ObjectCount += _groups[i].ObjectCount;
                VisibleObjectCount += _groups[i].VisibleObjectCount;
            }

            _renderCamera = Camera.main;
        }
    }
}