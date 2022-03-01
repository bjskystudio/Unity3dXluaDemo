using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace YoukiaEngine
{
    //注意:出于效率优化考量,并不使用多态等方式实现与GeometryInstancingGroup的代码复用
    //对于Impostor会强制的加入InstaningShader参数'_Size',物件被正常渲染时会跳过此参数,因此参数循环会由'1'开始
    public class ImpostorInstancingGroup : GeometryInstancingGroup
    {
        private static float DEFAULT_IMPOSTOR_DISTANCE = 600;
        private static float _distanceScale = 1.0f;

        private Mesh ImpostorMesh;

        public Material ImpostorMateral;
        public Material SnapshotMateral;

        //实体物体绘制时跳过index_0,即Impostor参数'_Size'以优化效率
        private float _impostorDistance = DEFAULT_IMPOSTOR_DISTANCE;

        private ImpostorSnapshotAtlas.Snapshot _snapshotRT;
        private bool _isSnapshotRefresh = true;

        // batch list
        private DynamicArray<Batch> _batchesImpostor = new DynamicArray<Batch>();
        // not full batch
        private Queue<Batch> _batchesImpostorAvailable = new Queue<Batch>();

        public float ImpostorDistance
        {
            set { _impostorDistance = value; }
            get { return _impostorDistance * _distanceScale; }
        }

	    public DynamicArray<Batch> BatchsImpostor
		{
		    get { return _batchesImpostor; }
	    }

        /// <summary>
        /// ImpostorInstancingGroup
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="material"></param>
        /// <param name="impostorMateral"></param>
        /// <param name="snapshotMateral"></param>
        /// <param name="layer"></param>
        /// <param name="impostorMesh"></param>
        public ImpostorInstancingGroup(Mesh mesh, Material material, Material impostorMateral, Material snapshotMateral, int layer, Mesh impostorMesh = null) : base(mesh, material, layer)
        {
            ImpostorMateral = impostorMateral;
            ImpostorMateral.CopyPropertiesFromMaterial(Material);
            ImpostorMateral.enableInstancing = true;

            SnapshotMateral = snapshotMateral;
            SnapshotMateral.CopyPropertiesFromMaterial(Material);
    

            _snapshotRT = ImpostorSnapshotAtlas.Instance.GetSnapshot();
            ImpostorMateral.SetTexture("_MainTex", _snapshotRT.atlas.Target);

            RegisterFloatProperty("_Size");
            if (!ImpostorMesh)
            {
                if (impostorMesh == null)
                    ImpostorMesh = CreateImpostorMesh(mesh.bounds);
                else
                    ImpostorMesh = impostorMesh;
            }
        }

        // 这里没法获取unity球谐光照数据，手动获取
        public void SphericalHarmonicsL(Renderer render)
        {
            SphericalHarmonicsL2 sh2;
            LightProbes.GetInterpolatedProbe(Vector3.zero, render, out sh2);

            //Vector4 unity_SHAr = new Vector4(sh2[0, 0], sh2[0, 1], sh2[0, 2], sh2[0, 3]);
            //Vector4 unity_SHAg = new Vector4(sh2[1, 0], sh2[1, 1], sh2[1, 2], sh2[1, 3]);
            //Vector4 unity_SHAb = new Vector4(sh2[2, 0], sh2[2, 1], sh2[2, 2], sh2[2, 3]);
            //Vector4 unity_SHBr = new Vector4(sh2[0, 4], sh2[0, 5], sh2[0, 6], sh2[0, 7]);
            //Vector4 unity_SHBg = new Vector4(sh2[1, 4], sh2[1, 5], sh2[1, 6], sh2[1, 7]);
            //Vector4 unity_SHBb = new Vector4(sh2[2, 4], sh2[2, 5], sh2[2, 6], sh2[2, 7]);
            //Vector4 unity_SHC = new Vector4(sh2[0, 8], sh2[1, 8], sh2[2, 8], 1);

            Vector4 unity_SHAr = new Vector4(sh2[0, 3], sh2[0, 1], sh2[0, 2], sh2[0, 0]);
            Vector4 unity_SHAg = new Vector4(sh2[1, 3], sh2[1, 1], sh2[1, 2], sh2[1, 0]);
            Vector4 unity_SHAb = new Vector4(sh2[2, 3], sh2[2, 1], sh2[2, 2], sh2[2, 0]);
            Vector4 unity_SHBr = new Vector4(sh2[0, 7], sh2[0, 5], sh2[0, 6], sh2[0, 4]);
            Vector4 unity_SHBg = new Vector4(sh2[1, 7], sh2[1, 5], sh2[1, 6], sh2[1, 4]);
            Vector4 unity_SHBb = new Vector4(sh2[2, 7], sh2[2, 5], sh2[2, 6], sh2[2, 4]);
            Vector4 unity_SHC = new Vector4(sh2[0, 8], sh2[1, 8], sh2[2, 8], 1);

            SnapshotMateral.SetVector("yk_SHAr", unity_SHAr);
            SnapshotMateral.SetVector("yk_SHAg", unity_SHAg);
            SnapshotMateral.SetVector("yk_SHAb", unity_SHAb);
            SnapshotMateral.SetVector("yk_SHBr", unity_SHBr);
            SnapshotMateral.SetVector("yk_SHBg", unity_SHBg);
            SnapshotMateral.SetVector("yk_SHBb", unity_SHBb);
            SnapshotMateral.SetVector("yk_SHC", unity_SHC);

            //Shader.SetGlobalVector("unity_SHAr", unity_SHAr);
            //Shader.SetGlobalVector("unity_SHAg", unity_SHAg);
            //Shader.SetGlobalVector("unity_SHAb", unity_SHAb);
            //Shader.SetGlobalVector("unity_SHBr", unity_SHBr);
            //Shader.SetGlobalVector("unity_SHBg", unity_SHBg);
            //Shader.SetGlobalVector("unity_SHBb", unity_SHBb);
            //Shader.SetGlobalVector("unity_SHC", unity_SHC);
        }

        /// <summary>
        /// Add objects
        /// </summary>
        /// <param name="objs"></param>
        public override void AddObject(InstanceObject[] objs)
        {
            int objCountPre = _objects.Count;
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].Group != this)
                {
                    throw new NotSupportedException("InstancingObject not match the group.");
                }
                if (objs[i].GroupID != -1)
                {
                    continue;
                }

                InstanceBounds boundses = objs[i].Bounds;
                if (boundses != null)
                {
                    Bounds srcBounds = boundses.BoundingBox;
                    //float size = Mathf.Max(Mathf.Max(srcBounds.extents.x, srcBounds.extents.y), srcBounds.extents.z) * 0.7f;
                    float size = Mathf.Max(srcBounds.extents.x, srcBounds.extents.y);
                    objs[i].SetPropertyFloat("_Size", size);
                }
                objs[i].ImpostorDistanceSqr = ImpostorDistance * (Random.value * 0.2f + 0.9f);
                objs[i].ImpostorDistanceSqr = objs[i].ImpostorDistanceSqr * objs[i].ImpostorDistanceSqr;
                objs[i].GroupID = objCountPre + i;
                _objects.Add(objs[i]);

                // 共享包围盒计数
                if (objs[i].SharedBounds != null)
                    objs[i].SharedBounds.SharedCount++;
            }
        }

        /// <summary>
        /// Remove objects
        /// </summary>
        /// <param name="objs"></param>
        public override void RemoveObject(params InstanceObject[] objs)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                InstanceObject objDirty = null;
                InstanceObject obj = objs[i];
                if (obj.GroupID == -1)
                    continue;

                if (obj.Group == this)
                {
                    int dirtyId = -1;
                    // remove from batch
                    if (obj._batchHandle != -1)
                    {
                        bool bImpostor = obj.DistanceSqr > obj.ImpostorDistanceSqr;
                        if (bImpostor)
                            dirtyId = RemoveObjectFromBatchImpostor(obj);
                        else
                            dirtyId = RemoveObjectFromBatch(obj);
                    }

                    if (dirtyId != -1)
                        objDirty = _objects._array[dirtyId];

                    // remove from objects
                    _objects.QuickRemoveAt(obj.GroupID);
                    if (obj.GroupID < _objects.Count)
                    {
                        InstanceObject objSwitch = _objects._array[obj.GroupID];
                        objSwitch.GroupID = obj.GroupID;

                        if (objSwitch._batch != null)
                        {
//                            bool bImpostor = objSwitch.DistanceSqr > objSwitch.ImpostorDistanceSqr;
                            objSwitch._batch._arrObjects[objSwitch._batchHandle] = obj.GroupID;
                        }
                    }

                    if (dirtyId != -1)
                    {
                        if (!objDirty._dirtyFlag)
                        {
                            AddDirtyObject(objDirty);
                        }
                    }

                    // 共享包围盒
                    if (obj.SharedBounds != null)
                    {
                        obj.SharedBounds.SharedCount--;
                        if (obj.SharedBounds.SharedCount == 0)
                            obj.SharedBounds.ResetSharedBounds();
                        obj._isSharedBoundsVisible = false;
                    }

                    obj.Clear();
                    obj = null;
                }
            }
        }

        /// <summary>
        /// Clear Impostor 
        /// </summary>
        public override void ClearObject()
        {
            base.ClearObject();

            _batchesImpostor.Clear();
            _batchesImpostorAvailable.Clear();
        }

        public override void SetVisible(bool bVisible)
        {
            base.SetVisible(bVisible);

            for (int i = 0; i < _batchesImpostor.Count; i++)
                _batchesImpostor[i].Visible = bVisible;
        }

        public void ReleaseRT()
        {
            if (ImpostorMesh != null)
            {
                GameObject.Destroy(ImpostorMesh);
                ImpostorMesh = null;
            }
            if (_snapshotRT != null)
            {
                ImpostorSnapshotAtlas.Instance.Release(_snapshotRT);
                _snapshotRT = null;
            }
        }

        /// <summary>
        /// Refresh Impostor
        /// </summary>
        public void RefreshImpostor()
        {
            _isSnapshotRefresh = true;
        }

        /// <summary>
        /// Update impostor
        /// </summary>
        public override void Update()
        {
            if (!_isVisible)
                return;
            GeometryInstancingManager mgr = GeometryInstancingManager.Instance;

            if (mgr.RenderCamera == null)
                return;
            Vector3 cameraPos = mgr.RenderCamera.transform.position;

            int objectCount = _objects.Count;
            _visibleObjectCount = objectCount;

            if (_isSnapshotRefresh)
            {
                ImpostorSnapshotUtils.Instance.Process(_snapshotRT, Mesh, SnapshotMateral);
                _isSnapshotRefresh = false;
            }

            // dirty data
            InstanceObject[] arrObjs = _objects._array;
            for (int i = 0; i < objectCount; i++)
            {
                InstanceObject obj = arrObjs[i];
                if (obj is DynamicInstanceObject o)
                {
                    o.UpdateTransform();
                }

                bool bBoundVisible = true;
                InstanceBounds boundses = null;
                // first check shared bounds
                if (obj.SharedBounds != null)
                {
                    boundses = obj.SharedBounds;

                    if (mgr.BoundCheckCode != boundses.CheckCode)
                    {
                        bBoundVisible = boundses.IsVisible;
                        boundses.IsVisible = GeometryUtility.TestPlanesAABB(mgr.CameraPlanes, boundses.BoundingBox);
                        boundses.ImpostorStatus = CheckImpostorShared(ref boundses.BoundingBox, ref cameraPos);

                        boundses.CheckCode = mgr.BoundCheckCode;
                        obj._isSharedBoundsVisible = boundses.IsVisible;
                    }
                    else
                    {
                        bBoundVisible = obj._isSharedBoundsVisible;
                        obj._isSharedBoundsVisible = boundses.IsVisible;
                    }
                }
                else if (obj.Bounds != null)
                {
                    boundses = obj.Bounds;

                    if (mgr.BoundCheckCode != boundses.CheckCode)
                    {
                        bBoundVisible = boundses.IsVisible;
                        boundses.IsVisible = GeometryUtility.TestPlanesAABB(mgr.CameraPlanes, boundses.BoundingBox);
                        boundses.CheckCode = mgr.BoundCheckCode;
                    }
                }

                if (boundses != null)
                {
                    int dirtyId0 = -1;
                    int dirtyId1 = -1;

                    if (bBoundVisible != boundses.IsVisible || obj._isVisible != obj._isVisiblePre || (boundses.IsVisible && obj._batch == null))
                    {
                        if (!boundses.IsVisible || !obj._isVisible)
                        {
                            if (obj._batchHandle != -1)
                            {
                                bool bImpostor = obj.DistanceSqr > obj.ImpostorDistanceSqr;
                                if (bImpostor)
                                    dirtyId0 = RemoveObjectFromBatchImpostor(obj);
                                else
                                    dirtyId0 = RemoveObjectFromBatch(obj);
                            }
                        }
                        else
                        {
                            obj.DistanceSqr = GeometryInstancingUtils.DistanceSquareXZ(ref obj.Transform, ref cameraPos);
                            bool bImpostor = obj.DistanceSqr > obj.ImpostorDistanceSqr;
                            if (bImpostor)
                                dirtyId0 = AddObjectToBatchImpostor(obj);
                            else
                                dirtyId0 = AddObjectToBatch(obj);
                        }
                    }
                    else if (boundses.IsVisible && obj._isVisible && obj._batchHandle != -1)
                    {
                        // impostor change
                        bool bUpdateImpostor = !boundses.ImpostorStatus && CheckImpostor(ref obj, ref cameraPos);
                        if (bUpdateImpostor)
                        {
                            bool bImpostor = obj.DistanceSqr > obj.ImpostorDistanceSqr;
                            if (bImpostor)
                            {
                                dirtyId1 = RemoveObjectFromBatch(obj);
                                dirtyId0 = AddObjectToBatchImpostor(obj);
                            }
                            else
                            {
                                dirtyId1 = RemoveObjectFromBatchImpostor(obj);
                                dirtyId0 = AddObjectToBatch(obj);
                            }
                        }
                    }

                    if (dirtyId0 != -1)
                    {
                        InstanceObject objDirty = arrObjs[dirtyId0];
                        if (!objDirty._dirtyFlag)
                        {
                            AddDirtyObject(objDirty);
                        }
                    }

                    if (dirtyId1 != -1)
                    {
                        InstanceObject objDirty = arrObjs[dirtyId1];
                        if (!objDirty._dirtyFlag)
                        {
                            AddDirtyObject(objDirty);
                        }
                    }

                    if (boundses.IsVisible && obj._isVisible && obj.IsNeedUpdateData() && !obj._dirtyFlag)
                        AddDirtyObject(obj);

                    if (!boundses.IsVisible || !obj._isVisible)
                        _visibleObjectCount--;
                }
                else
                {
                    // 无包围盒
                    if (obj._batchHandle == -1)
                    {
                        if (obj.DistanceSqr > obj.ImpostorDistanceSqr)
                            AddObjectToBatchImpostor(obj);
                        else
                            AddObjectToBatch(obj);

                        AddDirtyObject(obj);
                    }
                    else
                    {
                        int dirtyId0 = -1;
                        int dirtyId1 = -1;

                        bool bUpdateImpostor = CheckImpostor(ref obj, ref cameraPos);
                        if (bUpdateImpostor)
                        {
                            if (obj.DistanceSqr > obj.ImpostorDistanceSqr)
                            {
                                if (obj._batchHandle != -1)
                                    RemoveObjectFromBatch(obj);
                                AddObjectToBatchImpostor(obj);
                            }
                            else
                            {
                                if (obj._batchHandle != -1)
                                    RemoveObjectFromBatchImpostor(obj);
                                AddObjectToBatch(obj);
                            }
                            if (dirtyId0 != -1)
                            {
                                InstanceObject objDirty = arrObjs[dirtyId0];
                                if (!objDirty._dirtyFlag)
                                {
                                    AddDirtyObject(objDirty);
                                }
                            }
                            if (dirtyId1 != -1)
                            {
                                InstanceObject objDirty = arrObjs[dirtyId1];
                                if (!objDirty._dirtyFlag)
                                {
                                    AddDirtyObject(objDirty);
                                }
                            }
                            else if (obj.IsNeedUpdateData())
                            {
                                AddDirtyObject(obj);
                            }
                        }
                    }
                }

                obj._isVisiblePre = obj._isVisible;
            }

            // update dirty array
            UpdateDirtyData();
        }

        /// <summary>
        /// Render impostor instance
        /// </summary>
        public override void Render()
        {
            int floatPropertyCount = FloatPropertyName.Count;
            int vectorPropertyCount = VectorPropertyName.Count;
            int batchCount = _batches.Count;

            if (Mesh != null && Material != null)
            {
                for (int i = 0; i < batchCount; i++)
                {
                    Batch batch = _batches[i];
                    if (!batch.IsBatchVisible())
                        continue;

                    batch.DrawBatch(floatPropertyCount, vectorPropertyCount, ref FloatPropertyName, ref VectorPropertyName);
                }
            }

            batchCount = _batchesImpostor.Count;
            if (ImpostorMateral != null && ImpostorMesh != null)
            {
                for (int i = 0; i < batchCount; i++)
                {
                    Batch batchImpostor = _batchesImpostor[i];
                    if (!batchImpostor.IsBatchVisible())
                        continue;

                    batchImpostor.DrawBatch(floatPropertyCount, vectorPropertyCount, ref FloatPropertyName, ref VectorPropertyName);
                }
            }
        }

        private Batch AddBatchImpostor()
        {
            int floatPropCount = FloatPropertyName.Count;
            int vectorPropCount = VectorPropertyName.Count;

            Batch batch = new Batch();
            batch.Init(_batchesImpostor.Count, floatPropCount, vectorPropCount, ImpostorMesh, ImpostorMateral, Layer, true);
            _batchesImpostor.Add(batch);
            _batchesImpostorAvailable.Enqueue(batch);
            batch._isAvailable = true;

            return batch;
        }

        private Batch GetAvailableBatchImpostor()
        {
            Batch batch;
            if (_batchesImpostorAvailable.Count == 0)
                batch = AddBatchImpostor();
            else
                batch = _batchesImpostorAvailable.Peek();

            return batch;
        }

        private int AddObjectToBatchImpostor(InstanceObject obj)
        {
            // check need new batch
            Batch batch = GetAvailableBatchImpostor();

            int dirtyId = batch.AddDrawObject(obj);

            if (batch._drawCount >= GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT)
            {
                _batchesImpostorAvailable.Dequeue();
                batch._isAvailable = false;
            }

            return dirtyId;
        }

        private int RemoveObjectFromBatchImpostor(InstanceObject obj)
        {
            if (obj._batch == null)
                return -1;

            Batch batch = obj._batch;
            int dirtyId = batch.RemoveDrawObject(obj, ref _objects);
            if (!batch._isAvailable)
            {
                _batchesImpostorAvailable.Enqueue(batch);
                batch._isAvailable = true;
            }

            return dirtyId;
        }

        private Mesh CreateImpostorMesh(Bounds bounds)
        {
            Vector3 extents = bounds.extents;

            // 顶点
            Vector3[] vertices = new Vector3[4];
            float x = 1;
            float y = 1;
            float scale = 1;
            if (extents.x < extents.y)
            {// 宽高比，为了减少面板面积
                x = extents.x / extents.y;
            }
            if (extents.x > extents.y)
            {// 宽高比，为了减少面板面积
                //y = extents.y / extents.x;
                // 由于正交投影和从斜上方投影，造成矮宽的物体Impostor大一点，这里特殊修正
                y = extents.y * 1.414f / extents.x;
                if (y > 1) y = 1;
                scale = y;
            }

            vertices[0] = new Vector3(-x * scale, -y * scale, 0);
            vertices[1] = new Vector3(x * scale, -y * scale, 0);
            vertices[2] = new Vector3(x * scale, +y * scale, 0);
            vertices[3] = new Vector3(-x * scale, +y * scale, 0);

            //Vector3 center = bounds.center;
            //float size = Mathf.Max(extents.x, extents.y);
            //vertices[0] = new Vector3(center.x - extents.x, center.y - extents.y, 0);
            //vertices[1] = new Vector3(center.x + extents.x, center.y - extents.y, 0);
            //vertices[2] = new Vector3(center.x + extents.x, center.y + extents.y, 0);
            //vertices[3] = new Vector3(center.x - extents.x, center.y + extents.y, 0);
            //vertices[0] /= size;
            //vertices[1] /= size;
            //vertices[2] /= size;
            //vertices[3] /= size;

            // UV
            Vector2[] uvs = new Vector2[4];
            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(1, 0);
            uvs[2] = new Vector2(1, 1);
            uvs[3] = new Vector2(0, 1);

            if (extents.x < extents.y)
            {
                uvs[0] = new Vector2(0.5f - x * 0.5f, 0);
                uvs[1] = new Vector2(0.5f + x * 0.5f, 0);
                uvs[2] = new Vector2(0.5f + x * 0.5f, 1);
                uvs[3] = new Vector2(0.5f - x * 0.5f, 1);
            }
            if (extents.x > extents.y)
            {
                uvs[0] = new Vector2(0, 0.5f - y * 0.5f);
                uvs[1] = new Vector2(1, 0.5f - y * 0.5f);
                uvs[2] = new Vector2(1, 0.5f + y * 0.5f);
                uvs[3] = new Vector2(0, 0.5f + y * 0.5f);
            }

            // uv在图集中的位置
            for (int i = 0; i < 4; i++)
            {
                uvs[i].x = _snapshotRT.u + uvs[i].x * _snapshotRT.size;
                uvs[i].y = _snapshotRT.v + uvs[i].y * _snapshotRT.size;
            }


            // 顶点索引
            int[] indices = new int[6];
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;
            indices[3] = 0;
            indices[4] = 3;
            indices[5] = 2;

            Mesh mesh = new Mesh();
            mesh.name = "ImpostorMesh";
            mesh.vertices = vertices;
            mesh.uv = uvs;
            // 某些老机型使用uv2有问题，比如红米Note3的uv2无效
            //mesh.uv2 = uvs2;
            mesh.SetIndices(indices, MeshTopology.Triangles, 0, false);

            return mesh;
        }

        /// <summary>
        /// check impostor has changed
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cameraPos"></param>
        /// <returns></returns>
        private bool CheckImpostor(ref InstanceObject obj, ref Vector3 cameraPos)
        {
            bool bImpostor;
            float distanceSqrPre = obj.DistanceSqr;
            obj.DistanceSqr = GeometryInstancingUtils.DistanceSquareXZ(ref obj.Transform, ref cameraPos);
            bImpostor = (distanceSqrPre - obj.ImpostorDistanceSqr) * (obj.DistanceSqr - obj.ImpostorDistanceSqr) < 0;

            return bImpostor;
        }

        private bool CheckImpostorShared(ref Bounds bounds, ref Vector3 cameraPos)
        {
            //float distanceSqrMin = bounds.SqrDistance(cameraPos);
            float distanceSqrMin = GeometryInstancingUtils.DistanceSquareXZ(ref bounds, ref cameraPos);
            float dis = ImpostorDistance * 1.1f;

            return distanceSqrMin > dis * dis;
        }


        private Batch batchManual = null;
        public override void ManualRender(Camera camera = null)
        {
            //Update();
            //Render();

            // 阴影相机使用后会改变缓存的值，导致主相机错误绘制，这里需要手动处理
            if (!_isVisible)
                return;

            int floatPropCount = FloatPropertyName.Count;
            int vectorPropCount = VectorPropertyName.Count;

            if (batchManual == null)
            {
                int nLayer = LayerMask.NameToLayer("InstancingManualShadow");
                if (nLayer == -1)
                {
                    Debug.LogError("InstancingManualShadow Layer is undefine!");
                }
                
                batchManual = new Batch();
                batchManual.Init(_batches.Count, floatPropCount, vectorPropCount, Mesh, Material, nLayer);
            }

            int objectCount = _objects.Count;
            GeometryInstancingManager mgr = GeometryInstancingManager.Instance;
            InstanceObject[] arrObjs = _objects._array;
            for (int i = 0; i < objectCount; i++)
            {
                InstanceObject obj = arrObjs[i];
                if (obj._isVisible)
                {

                    InstanceBounds boundses = null;
                    // first check shared bounds
                    if (obj.SharedBounds != null)
                    {
                        boundses = obj.SharedBounds;
                    }
                    else if (obj.Bounds != null)
                    {
                        boundses = obj.Bounds;
                    }
                    bool bImpostor = obj.DistanceSqr > obj.ImpostorDistanceSqr;
                    // 主相机没有提交过，这里才提交，并且是Impostor的需要重新提交原模型
                    if (!boundses.IsVisible || bImpostor)
                    {
                        if(GeometryUtility.TestPlanesAABB(mgr.CameraPlanes, obj.Bounds.BoundingBox))
                        {
                            //batchManual.AddDrawObject(obj);
                            batchManual._arrObjects[batchManual._drawCount] = obj.GroupID;

                            Matrix4x4[] batchMatrices = batchManual._drawMatrices;
                            GeometryInstancingUtils.WorldMatrixCopy(ref batchMatrices[batchManual._drawCount], ref obj.Transform);

                            batchManual._drawCount++;

                            if (batchManual._drawCount >= GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT)
                            {
                                batchManual.DrawBatch(floatPropCount, vectorPropCount, ref FloatPropertyName, ref VectorPropertyName,camera);
                                batchManual._drawCount = 0;
                            }
                        }
                    }
                }
            }
            // 缓存没满的，这里补画
            if (batchManual.IsBatchVisible())
            {
                batchManual.DrawBatch(floatPropCount, vectorPropCount, ref FloatPropertyName, ref VectorPropertyName, camera);
                batchManual._drawCount = 0;
            }
        }

        private static bool _LoadChangeState = false;
        public static float GetImpostorDistanceScale()
        {
            return _distanceScale;
        }

        /// <summary>
        /// 设置Impostor距离比例(配合客户端低中高配置)
        /// </summary>
        /// <param name="scale"></param>
        public static void SetImpostorDistanceScale(float scale, bool bChangeNow = true)
        {
            if (_distanceScale == scale)
                return;
            _distanceScale = scale;
            if (bChangeNow)
            {
                //if (GeometryInstancingManager.IsOK())
                //{
                //    int count = GeometryInstancingManager.Instance.GroupCount;
                //    for (int i = 0; i < count; i++)
                //    {
                //        GeometryInstancingGroup group = GeometryInstancingManager.Instance.GetGroup(i);
                //        if (group is ImpostorInstancingGroup)
                //        {
                //            ImpostorInstancingGroup ig = (ImpostorInstancingGroup)@group;
                //            ig.RefreshImpostor();

                //            int objectCount = ig._objects.Count;
                //            InstanceObject[] arrObjs = ig._objects._array;
                //            for (int j = 0; j < objectCount; j++)
                //            {
                //                InstanceObject obj = arrObjs[i];
                //                if (obj != null)
                //                {
                //                    obj.ImpostorDistanceSqr = ig.ImpostorDistance * (Random.value * 0.2f + 0.9f);
                //                    obj.ImpostorDistanceSqr = obj.ImpostorDistanceSqr * obj.ImpostorDistanceSqr;

                //                    obj._isVisiblePre = !obj._isVisible;// 刷新
                //                }
                //            }
                //        }
                //    }
                //}

                // 上面代码由于缓存机制刷新不了，这里直接重载
                //EntityManager mgr = EntityManager.current;
                //if (mgr != null && !_LoadChangeState)
                //{
                //    _LoadChangeState = true;
                //    bool bThread = mgr.IsThread();
                //    mgr.SetThread(false, () => {
                //        mgr.Reload();
                //        mgr.SetThread(bThread);
                //        _LoadChangeState = false;
                //    });
                //}
            }
        }

    }
}
