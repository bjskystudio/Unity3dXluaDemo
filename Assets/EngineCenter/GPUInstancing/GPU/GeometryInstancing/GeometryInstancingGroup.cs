using System;
using System.Collections.Generic;
using UnityEngine;

namespace YoukiaEngine
{
    public class GeometryInstancingGroup
    {
        public Mesh Mesh;
        public Material Material;

	    protected DynamicArray<InstanceObject> _objects = new DynamicArray<InstanceObject>();
        public List<string> FloatPropertyName = new List<string>();
        public List<string> VectorPropertyName = new List<string>();
        public Dictionary<string, Texture> mTexMap = new Dictionary<string, Texture>();
	    // shadow
//	    protected List<Matrix4x4[]> _shadowDrawMatrices = new List<Matrix4x4[]>();
//	    protected List<float[][]> _shadowPropertyFloatsBuffer = new List<float[][]>();
//	    protected List<Vector4[][]> _shadowPropertyVectorsBuffer = new List<Vector4[][]>();
//	    protected List<MaterialPropertyBlock> _shadowPropertyBlocks = new List<MaterialPropertyBlock>();
//        protected int _shadowObjectCount;
//	    protected int _shadowVisibleBatchCount;
		// batch list
	    protected DynamicArray<Batch> _batches = new DynamicArray<Batch>();
		// not full batch
		protected Queue<Batch> _batchesAvailable = new Queue<Batch>();
		// dirty arr
		protected DynamicArray<InstanceObject[]> _dirtyData = new DynamicArray<InstanceObject[]>();
	    protected int _dirtyArrCount = 0;
	    protected int _dirtyCount = 0;
		// visible
		protected bool _isVisible = true;
		// 相机裁剪后可见物体总数
		protected int _visibleObjectCount;
        // 记录所属
        private GeometryInstancingManager _Parent = null;

        #region get set
        /// <summary>
        /// Group is visible
        /// </summary>
        public bool IsVisible
		{
			get { return _isVisible; }
		}

		/// <summary>
		/// Layer
		/// </summary>
	    public int Layer { get; set; }

		/// <summary>
		/// Visible object count
		/// </summary>
		public int VisibleObjectCount
		{
			get { return _visibleObjectCount; }
		}

		/// <summary>
		/// Object count
		/// </summary>
		public int ObjectCount
		{
			get { return _objects.Count; }
		}

//		/// <summary>
//		/// Float Property Name
//		/// </summary>
//		List<string> IGeometryInstancingGroup.FloatPropertyName
//		{
//			get { return FloatPropertyName; }
//		}
//
//		/// <summary>
//		/// Vector Property Name
//		/// </summary>
//		List<string> IGeometryInstancingGroup.VectorPropertyName
//		{
//			get { return VectorPropertyName; }
//		}

	    public DynamicArray<Batch> Batchs
	    {
		    get { return _batches; }
	    }
   
        public GeometryInstancingManager Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        #endregion

        /// <summary>
        /// GeometryInstancingGroup
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="material"></param>
        /// <param name="layer"></param>
        public GeometryInstancingGroup(Mesh mesh, Material material, int layer, params GPUObject.TexInfo[] texInfos)
        {
            Mesh = mesh;
            Material = material;
			if (Material != null)
				Material.enableInstancing = true;
            Layer = layer;

            for(int i = 0; i < texInfos.Length; i++)
            {
                mTexMap[texInfos[i].mName] = texInfos[i].mTex;
            }
        }

		/// <summary>
		/// Get object
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public InstanceObject GetObject(int index)
		{
			return _objects[index];
		}

		/// <summary>
		/// Add object
		/// </summary>
		/// <param name="objs"></param>
		public virtual void AddObject(params InstanceObject[] objs)
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

	            objs[i].DistanceSqr = 0;
	            objs[i].GroupID = objCountPre + i;
                _objects.Add(objs[i]);

				// 共享包围盒计数
	            if (objs[i].SharedBounds != null)
		            objs[i].SharedBounds.SharedCount++;
            }
        }

		/// <summary>
		/// Remove object
		/// </summary>
		/// <param name="objs"></param>
        public virtual void RemoveObject(params InstanceObject[] objs)
        {
            if(_objects.Count == 0)
            {
                return;
            }

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
					if (obj._batch != null)
						dirtyId = RemoveObjectFromBatch(obj);

			        if (dirtyId != -1)
						objDirty = _objects._array[dirtyId];

					// remove from objects
			        _objects.QuickRemoveAt(obj.GroupID);
			        if (obj.GroupID < _objects.Count)
			        {
				        InstanceObject objSwitch = _objects._array[obj.GroupID];
				        objSwitch.GroupID = obj.GroupID;
						if (objSwitch._batch != null)
							objSwitch._batch._arrObjects[objSwitch._batchHandle] = obj.GroupID;
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
		/// Clear instance objects
		/// </summary>
        public virtual void ClearObject()
        {
            _objects.Clear();

			// clear dirty data
			_dirtyData.Clear();
	        _dirtyCount = 0;

			_batches.Clear();
			_batchesAvailable.Clear();
        }

		/// <summary>
		/// Set current group visible
		/// </summary>
		/// <param name="bVisible"></param>
		public virtual void SetVisible(bool bVisible)
		{
			for (int i = 0; i < _batches.Count; i++)
				_batches[i].Visible = bVisible;

			_isVisible = bVisible;
		}

        /// <summary>
        /// Register float property
        /// </summary>
        /// <param name="name"></param>
        public void RegisterFloatProperty(string name)
        {
            if (FloatPropertyName.Count >= InstanceObject.MAX_PROPERTY_COUNT)
            {
                throw new NotSupportedException("Number of Property is [0, 32]");
            }
            if (!FloatPropertyName.Contains(name))
            {
                FloatPropertyName.Add(name);
            }
        }

		/// <summary>
		/// Register vector property
		/// </summary>
		/// <param name="name"></param>
        public void RegisterVectorProperty(string name)
        {
            if (VectorPropertyName.Count >= InstanceObject.MAX_PROPERTY_COUNT)
            {
                throw new NotSupportedException("Number of Property is [0, 32]");
            }
            if (!VectorPropertyName.Contains(name))
            {
                VectorPropertyName.Add(name);
            }
        }

		/// <summary>
		/// Deregister float property
		/// </summary>
		/// <param name="name"></param>
		public void DeregisterFloatProperty(string name)
        {
            FloatPropertyName.Remove(name);
        }

		/// <summary>
		/// Deregister vector property
		/// </summary>
		/// <param name="name"></param>
		public void DeregisterVectorProperty(string name)
        {
            VectorPropertyName.Remove(name);
        }

		/// <summary>
		/// Update
		/// </summary>
		public virtual void Update()
        {
			if (!_isVisible)
				return;
			
			int objectCount = _objects.Count;
	        _visibleObjectCount = objectCount;
			GeometryInstancingManager mgr = GeometryInstancingManager.Instance;
	        InstanceObject[] arrObjs = _objects._array;
			for (int i = 0; i < objectCount; i++)
	        {
		        bool bBoundVisible = true;
				InstanceObject obj = arrObjs[i];
		        if (obj is DynamicInstanceObject o)
		        {
			        o.UpdateTransform();
		        }

				InstanceBounds boundses = null;
				// first check shared bounds
				if (obj.SharedBounds != null)
		        {
					// shared bounds
			        boundses = obj.SharedBounds;

			        if (mgr.BoundCheckCode != boundses.CheckCode)
			        {
						bBoundVisible = boundses.IsVisible;
						boundses.IsVisible = GeometryUtility.TestPlanesAABB(mgr.CameraPlanes, boundses.BoundingBox);
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
			        // bounds
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
			        if (!boundses.IsVisible || !obj._isVisible)
				        _visibleObjectCount--;

					// dirty data
					if (bBoundVisible != boundses.IsVisible || obj._isVisible != obj._isVisiblePre)
					{
						int dirtyId = -1;
						if (boundses.IsVisible && obj._isVisible)
							dirtyId = AddObjectToBatch(obj);
						else
							dirtyId = RemoveObjectFromBatch(obj);
							
						if (dirtyId != -1)
						{
							InstanceObject objDirty = arrObjs[dirtyId];
							if (!objDirty._dirtyFlag)
							{
                                AddDirtyObject(objDirty);
							}
						}
					}
					else if (boundses.IsVisible && obj._isVisible && obj.IsNeedUpdateData() && !obj._dirtyFlag)
					{
                        AddDirtyObject(obj);
					}
				}
		        else
		        {
			        if (obj._batchHandle == -1)
			        {
                        AddObjectToBatch(obj);
			            AddDirtyObject(obj);
                    }
					else if (obj.IsNeedUpdateData())
						AddDirtyObject(obj);
				}

				obj._isVisiblePre = obj._isVisible;
			}

			// update dirty data to batch
	        UpdateDirtyData();
            for (int i = 0; i < _batches.Count; i++)
            {
                foreach(var item in mTexMap)
                {
                    _batches[i]._propertyBlocks.SetTexture(item.Key, item.Value);
                }
            }

        }

		/// <summary>
		/// Update shadow
		/// </summary>
//		public virtual void ShadowUpdate()
//        {
//            int objectCount = _objects.Count;
//            GeometryInstancingManager mgr = GeometryInstancingManager.Instance;
//            _visibleObjectCount = 0;
//            _shadowObjectCount = 0;
//            UpdateShadowBuffer();
//            int floatPropCount = FloatPropertyName.Count;
//            int vectorPropCount = VectorPropertyName.Count;
//            int batchIndex = -1;
//            Matrix4x4[] batchMatrices = null;
//            float[][] batchFloats = null;
//            Vector4[][] batchVectors = null;
//
//            for (int j = 0; j < objectCount; j++)
//            {
//                //Camera Culling
//                InstanceObject obj = _objects[j];
//                InstanceBounds boundses = obj.Bounds;
//                if (mgr.BoundCheckCode != boundses.CheckCode)
//                {
//                    //TODO:prevent struct copy may improve performance
//                    boundses.IsVisibleShadow = GeometryUtility.TestPlanesAABB(mgr.CameraPlanes, boundses.BoundingBox);
//                    boundses.CheckCode = mgr.BoundCheckCode;
//                }
//
//                if (!boundses.IsVisibleShadow)
//                {
//                    int batchIndexCurrent = _shadowObjectCount /
//                                                GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT;
//                    if (batchIndexCurrent != batchIndex)
//                    {
//                        batchIndex = batchIndexCurrent;
//                        batchMatrices = _shadowDrawMatrices[batchIndex];
//                        batchFloats = _shadowPropertyFloatsBuffer[batchIndex];
//                        batchVectors = _shadowPropertyVectorsBuffer[batchIndex];
//                    }
//
//                    int batchCount = _shadowObjectCount -
//                                     batchIndex * GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT;
//                    GeometryInstancingUtils.WorldMatrixCopy(ref batchMatrices[batchCount], ref obj.Transform);
//
//                    for (int k = 1; k < floatPropCount; k++)
//                    {
//                        batchFloats[k][batchCount] = obj.FloatPropertyValue[k];
//                    }
//                    for (int k = 0; k < vectorPropCount; k++)
//                    {
//                        GeometryInstancingUtils.VectorCopy(ref batchVectors[k][batchCount],
//                            ref obj.VectorPropertyValue[k]);
//                    }
//
//                    _shadowObjectCount++;
//                }
//            }
//
//            _shadowVisibleBatchCount = _shadowObjectCount == 0
//                   ? 0
//                   : _shadowObjectCount / GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT + 1;
//        }

		/// <summary>
		/// Render
		/// </summary>
        public virtual void Render()
        {
	        if (Mesh == null || Material == null)
		        return;

			int floatPropertyCount = FloatPropertyName.Count;
            int vectorPropertyCount = VectorPropertyName.Count;
            int batchCount = _batches.Count;

            for (int i = 0; i < batchCount; ++i)
            {
	            Batch batch = _batches[i];
				if (!batch.IsBatchVisible())
					continue;

				batch.DrawBatch(floatPropertyCount, vectorPropertyCount, ref FloatPropertyName, ref VectorPropertyName);
			}
        }

		/// <summary>
		/// Render shadow
		/// </summary>
//        public virtual void ShadowRender()
//        {
//            int floatPropertyCount = FloatPropertyName.Count;
//            int vectorPropertyCount = VectorPropertyName.Count;
//            int batchCount = _shadowVisibleBatchCount;
//            for (int i = 0; i < batchCount; i++)
//            {
//                for (int j = 1; j < floatPropertyCount; j++)
//                {
//                    _shadowPropertyBlocks[i].SetFloatArray(FloatPropertyName[j], _shadowPropertyFloatsBuffer[i][j]);
//                }
//                for (int j = 0; j < vectorPropertyCount; j++)
//                {
//                    _shadowPropertyBlocks[i].SetVectorArray(VectorPropertyName[j], _shadowPropertyVectorsBuffer[i][j]);
//                }
//                Graphics.DrawMeshInstanced(
//                    Mesh,
//                    0,
//                    Material,
//                    _shadowDrawMatrices[i],
//                    GetShadowDrawBatchCount(i),
//                    _shadowPropertyBlocks[i],
//                    ShadowCastingMode.Off,
//                    false,
//                    Layer, GeometryInstancingManager.Instance.ShadowVsmCamera);
//            }
//        }

	    protected virtual Batch AddBatch()
	    {
		    int floatPropCount = FloatPropertyName.Count;
		    int vectorPropCount = VectorPropertyName.Count;

		    Batch batch = new Batch();
		    batch.Init(_batches.Count, floatPropCount, vectorPropCount, Mesh, Material, Layer);
		    _batches.Add(batch);
		    _batchesAvailable.Enqueue(batch);
		    batch._isAvailable = true;

			return batch;
	    }

	    protected virtual Batch GetAvailableBatch()
	    {
		    Batch batch;
		    if (_batchesAvailable.Count == 0)
			    batch = AddBatch();
		    else
			    batch = _batchesAvailable.Peek();

		    return batch;
	    }

	    protected virtual int AddObjectToBatch(InstanceObject obj)
	    {
		    // check need new batch
		    Batch batch = GetAvailableBatch();

		    int dirtyId = batch.AddDrawObject(obj);

		    if (batch._drawCount >= GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT)
		    {
			    _batchesAvailable.Dequeue();
			    batch._isAvailable = false;
		    }

		    return dirtyId;
	    }

	    protected virtual int RemoveObjectFromBatch(InstanceObject obj)
	    {
		    if (obj._batch == null)
			    return -1;

			Batch batch = obj._batch;
		    int dirtyId = batch.RemoveDrawObject(obj, ref _objects);

            if (!batch._isAvailable)
		    {
			    _batchesAvailable.Enqueue(batch);
			    batch._isAvailable = true;
		    }

		    return dirtyId;
	    }

        protected void AddDirtyObject(InstanceObject obj)
        {
            int dirtyIndex = _dirtyCount / GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_ARRAY_COUNT;
            int dirtyCount = _dirtyCount - dirtyIndex * GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_ARRAY_COUNT;
            if (dirtyIndex + 1 > _dirtyArrCount)
            {
                InstanceObject[] arr = new InstanceObject[GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_ARRAY_COUNT];
                _dirtyData.Add(arr);
                _dirtyArrCount++;
            }

            _dirtyData._array[dirtyIndex][dirtyCount] = obj;
            obj._dirtyFlag = true;
            _dirtyCount++;
        }

        protected virtual void UpdateDirtyData()
        {
            InstanceObject obj;
            if (_dirtyCount != 0)
            {
                for (int i = 0; i < _dirtyData.Count; i++)
                {
                    InstanceObject[] arrObj = _dirtyData._array[i];
                    for (int j = 0; j < GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_ARRAY_COUNT; j++)
                    {
                        if (j + i * GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_ARRAY_COUNT >= _dirtyCount)
                        {
                            _dirtyCount = 0;
                            return;
                        }


                        obj = arrObj[j];
                        if (obj._batchHandle == -1 || obj._batch == null)
                        {
                            obj._batchUpdateFlag = false;
                            obj._dirtyFlag = false;
                            continue;
                        }

                        if (obj._batchUpdateFlag)
                            obj.UpdateAll();
                        obj.RefreshBatchBuffer(obj._batch);
                        obj._batchUpdateFlag = false;
                        obj._dirtyFlag = false;
                    }
                }
            }
        }

        public virtual void ManualRender(Camera camera = null)
	    {
			Update();
			Render();
	    }
    }
}