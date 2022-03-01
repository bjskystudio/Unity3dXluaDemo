using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YoukiaEngine
{
    public class InstanceObject
    {
        public const int MAX_PROPERTY_COUNT = 32;

        public readonly GeometryInstancingGroup Group;
        //GeometryInstancingGroup中_objects列表的下标
        public int GroupID = -1;

        public InstanceBounds Bounds;
        public InstanceBounds SharedBounds;

        public Vector3 Center;

        public Matrix4x4 Transform = Matrix4x4.identity;

        public float DistanceSqr;
        public float ImpostorDistanceSqr;

        public float[] FloatPropertyValue = new float[MAX_PROPERTY_COUNT];
        public Vector4[] VectorPropertyValue = new Vector4[MAX_PROPERTY_COUNT];

		// shared bounds
	    internal bool _isSharedBoundsVisible = false;
        // --------------------------------------------------
        // batch

        // batch index
        //        internal int _batchIndex = -1;

        public int BatchIndex => _batch == null ? -1 : _batch.Index;


        internal Batch _batch;
        //本对象在对应batch对象列表的下标
        internal int _batchHandle = -1;
        public int BatchHandle => _batchHandle;

        // dirty flag
        internal bool _dirtyFlag = false;
        internal bool _batchUpdateFlag = false;
        // update flag
        protected bool _isNeedUpdateFloat = true;
        protected bool _isNeedUpdateVector = true;
        protected bool _isNeedUpdateTrans = true;

        internal bool _isVisible = true;
	    internal bool _isVisiblePre = true;

        /// <summary>
        /// Is visible
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
				_isVisible = value;
            }
        }

        /// <summary>
        /// 是否需要更新数据
        /// </summary>
        /// <returns></returns>
        public bool IsNeedUpdateData()
        {
            return _isNeedUpdateFloat || _isNeedUpdateVector || _isNeedUpdateTrans;
        }

        /// <summary>
        /// InstanceObject
        /// </summary>
        /// <param name="group"></param>
        public InstanceObject(GeometryInstancingGroup @group)
        {
            Group = @group;
        }

        /// <summary>
        /// Set float property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetPropertyFloat(string name, float value)
        {
            int index = Group.FloatPropertyName.IndexOf(name);
            if (index == -1)
            {
                throw new NotSupportedException("Unregistered property, Regist for group first.");
            }
            else
            {
                FloatPropertyValue[index] = value;
                _isNeedUpdateFloat = true;
            }
        }

		/// <summary>
		/// Set vector property
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
        public void SetPropertyVector(string name, Vector4 value)
        {
            int index = Group.VectorPropertyName.IndexOf(name);
            if (index == -1)
            {
                throw new NotSupportedException("Unregistered property, Regist for group first.");
            }
            else
            {
                VectorPropertyValue[index] = value;
                _isNeedUpdateVector = true;
            }
        }

		/// <summary>
		/// Get float property handle
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
        public int GetFloatPropertyHandle(string name)
        {
            return Group.FloatPropertyName.IndexOf(name);
        }

		/// <summary>
		/// Set float property
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
        public void SetPropertyFloat(int index, float value)
        {
            FloatPropertyValue[index] = value;
            _isNeedUpdateFloat = true;
        }

		/// <summary>
		/// Get vector property handle
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
        public int GetVectorPropertyHandle(string name)
        {
            return Group.VectorPropertyName.IndexOf(name);
        }

		/// <summary>
		/// Set vector property
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		public void SetPropertyVector(int index, Vector4 value)
        {
            if (VectorPropertyValue[index] != value)
            {
                VectorPropertyValue[index] = value;
                _isNeedUpdateVector = true;
            }
        }

        public Vector4 GetPropertyVector(string name)
        {
            int index = Group.VectorPropertyName.IndexOf(name);
            if (index == -1)
            {
                throw new NotSupportedException("Unregistered property, Regist for group first.");
                return Vector4.zero;
            }

            return GetPropertyVector(index);
        }

        public Vector4 GetPropertyVector(int index)
        {
            return VectorPropertyValue[index];
        }

        /// <summary>
        /// Manual update transform
        /// </summary>
        public void ManualUpdateTransform()
        {
            _isNeedUpdateTrans = true;
        }

		/// <summary>
		/// Manual update float property
		/// </summary>
		public void ManualUpdateFloatProperty()
		{
			_isNeedUpdateFloat = true;
		}

		/// <summary>
		/// Manual update vector property
		/// </summary>
		public void ManualUpdateVectorProperty()
		{
			_isNeedUpdateVector = true;
		}

		/// <summary>
		/// Update all
		/// </summary>
		public void UpdateAll()
	    {
			_isNeedUpdateTrans = true;
			_isNeedUpdateFloat = true;
			_isNeedUpdateVector = true;
		}

        protected void RefreshTransformBuffer(ref Batch batch)
        {
            _isNeedUpdateTrans = false;
	        bool bImpostor = batch.IsImposter;
	        if (_batchHandle == -1)
		        return;

			// update to batch
			if (!bImpostor)
            {
                Matrix4x4[] batchMatrices = batch._drawMatrices;
                GeometryInstancingUtils.WorldMatrixCopy(ref batchMatrices[_batchHandle], ref Transform);
            }
            else
            {
                Matrix4x4[] batchMatrices = batch._drawMatrices;
                GeometryInstancingUtils.ImpostorWorldMatrixCopy(ref batchMatrices[_batchHandle], ref Center);
            }
        }

        protected void RefreshFloatPropertyBuffer(ref Batch batch)
        {
            _isNeedUpdateFloat = false;
	        bool bImpostor = batch.IsImposter;
	        if (_batchHandle == -1)
		        return;

			// update to batch
			if (!bImpostor)
            {
                float[][] batchFloats = batch._propertyFloatsBuffer;
                int floatPropCount = Group.FloatPropertyName.Count;
                for (int i = 0; i < floatPropCount; i++)
                {
                    batchFloats[i][_batchHandle] = FloatPropertyValue[i];
                }
            }
            else
            {
                float[][] batchFloats = batch._propertyFloatsBuffer;
                int floatPropCount = Group.FloatPropertyName.Count;
                for (int i = 0; i < floatPropCount; i++)
                {
                    batchFloats[i][_batchHandle] = FloatPropertyValue[i];
                }
            }
        }

        protected void RefreshVectorPropertyBuffer(ref Batch batch)
        {
            _isNeedUpdateVector = false;
	        bool bImpostor = batch.IsImposter;
			if (_batchHandle == -1)
				return;

			// update to batch
			if (!bImpostor)
            {
                Vector4[][] batchVectors = batch._propertyVectorsBuffer;
                int vectorPropCount = Group.VectorPropertyName.Count;
                for (int i = 0; i < vectorPropCount; i++)
                {
                    GeometryInstancingUtils.VectorCopy(ref batchVectors[i][_batchHandle], ref VectorPropertyValue[i]);
                }
            }
            else
            {
                Vector4[][] batchVectors = batch._propertyVectorsBuffer;
                int vectorPropCount = Group.VectorPropertyName.Count;
                for (int i = 0; i < vectorPropCount; i++)
                {
                    GeometryInstancingUtils.VectorCopy(ref batchVectors[i][_batchHandle], ref VectorPropertyValue[i]);
                }
            }
        }

		/// <summary>
		/// Refresh batch buffer
		/// </summary>
		/// <param name="batch"></param>
        public void RefreshBatchBuffer(Batch batch)
        {
            if (_isNeedUpdateTrans)
                RefreshTransformBuffer(ref batch);
            if (_isNeedUpdateFloat)
                RefreshFloatPropertyBuffer(ref batch);
            if (_isNeedUpdateVector)
                RefreshVectorPropertyBuffer(ref batch);
        }

		/// <summary>
		/// Clear
		/// </summary>
	    public void Clear()
	    {
		    GroupID = -1;
//		    _batchIndex = -1;
	        _batch = null;
		    _batchHandle = -1;
		    _dirtyFlag = false;
		    _batchUpdateFlag = false;
			Bounds.IsVisible = false;
		    Bounds.CheckCode = 0;
	    }
    }
}