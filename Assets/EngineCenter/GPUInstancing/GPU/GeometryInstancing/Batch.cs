using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace YoukiaEngine
{
	// GPU Instance batch
	public class Batch
	{
		// id
		private int _index;

	    public int Index => _index;

	    // visible
		private bool _isVisible;
		//available
		internal bool _isAvailable;

		//-------------------------------------------------
		// batch
		// visible item count
		internal int _drawCount;
		internal int[] _arrObjects;

		//-------------------------------------------------
		// imposter
		public bool IsImposter;

		//-------------------------------------------------
		// matrix
		internal Matrix4x4[] _drawMatrices;
		// property
		internal float[][] _propertyFloatsBuffer;
		internal Vector4[][] _propertyVectorsBuffer;
		public MaterialPropertyBlock _propertyBlocks;

		// mesh & material & layer
		private Mesh _mesh;
		private Material _material;
		private int _layer;

		#region get set
		public bool Visible
		{
			get => _isVisible; 
			set { _isVisible = value; }
		}

		public int DrawCount
		{
			get { return _drawCount; }
		}

		#endregion

		public bool IsBatchVisible()
		{
			if (_drawCount == 0 || !_isVisible)
				return false;

			return true;
		}

		// init
		public void Init(int index, int floatProCount, int vecProCount, Mesh mesh, Material material, int layer, bool bImpostor = false)
		{
			_drawCount = 0;
			_index = index;
			_mesh = mesh;
			_material = material;
			_layer = layer;
			_isVisible = true;
			_isAvailable = true;
			IsImposter = bImpostor;

			_arrObjects = new int[GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT];

			// matrix
			_drawMatrices = new Matrix4x4[GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT];
			for (int i = 0; i < GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT; i++)
			{
				if (bImpostor)
				{
					_drawMatrices[i].m00 = 1;
					_drawMatrices[i].m11 = 1;
					_drawMatrices[i].m22 = 1;
					_drawMatrices[i].m33 = 1;
				}
				else
					_drawMatrices[i].m33 = 1;
			}
				
			// property
			_propertyFloatsBuffer = new float[floatProCount][];
			for (int i = 0; i < floatProCount; i++)
			{
				float[] buffer = new float[GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT];
				_propertyFloatsBuffer[i] = buffer;
			}
			_propertyVectorsBuffer = new Vector4[vecProCount][];
			for (int i = 0; i < vecProCount; i++)
			{
				Vector4[] buffer = new Vector4[GeometryInstancingUtils.MAX_GEOMETRY_INSTANCE_DRAW_COUNT];
				_propertyVectorsBuffer[i] = buffer;
			}
			_propertyBlocks = new MaterialPropertyBlock();


            //if (_material.shader.name.Contains("YoukiaPlantsDiffuse"))
            //{
            //    if (matPreZ == null)
            //    {
            //        matPreZ = new Material(EngineIO.Instance.ShaderFind("YoukiaEngine/Plant/YoukiaPlantsPreZ"));
            //    }

            //    if (matPlantAfterPreZ == null)
            //    {
            //        matPlantAfterPreZ = new Material(EngineIO.Instance.ShaderFind("YoukiaEngine/Plant/YoukiaPlantsDiffuseAfterPreZ"));
            //    }
            //    preZ = true;
            //    matPreZ.CopyPropertiesFromMaterial(_material);
            //    matPreZ.renderQueue = (int)RenderQueue.Geometry - 100;//_material.renderQueue - 1;
            //    matPlantAfterPreZ.CopyPropertiesFromMaterial(_material);

            //}


        }

        internal int AddDrawObject(InstanceObject obj)
		{
			_arrObjects[_drawCount] = obj.GroupID;

		    obj._batch = this;
            obj._batchHandle = _drawCount;
			_drawCount++;

			obj._batchUpdateFlag = true;

            return obj.GroupID;
		}

		internal int RemoveDrawObject(InstanceObject obj, ref DynamicArray<InstanceObject> objs)
		{
			int batchHandle = obj._batchHandle;
			if (batchHandle == -1 || _drawCount == 0)
			{
				obj._batch = null;
				return -1;
			}
				
			int endIndex = _drawCount - 1;

			if (batchHandle == endIndex)
			{
				obj._batchHandle = -1;
				obj._batch = null;
				_drawCount--;
				return -1;
			}

			int groupIdSwitch = _arrObjects[endIndex];
			InstanceObject objSwitch = objs._array[groupIdSwitch];

		    obj._batch = null;
            obj._batchHandle = -1;
            objSwitch._batchHandle = batchHandle;
			objSwitch._batchUpdateFlag = true;
			_arrObjects[batchHandle] = groupIdSwitch;
			_drawCount--;
			return objSwitch.GroupID;
		}

        private Material matPreZ;
        private Material matPlantAfterPreZ;
        private bool preZ = false;

        public void DrawBatch(int floatProCount, int vecProCount, ref List<string> listFloatProName, ref List<string> listVecProName,Camera camera = null)
		{
			for (int i = 0; i < floatProCount; i++)
				_propertyBlocks.SetFloatArray(listFloatProName[i], _propertyFloatsBuffer[i]);
			for (int i = 0; i < vecProCount; i++)
				_propertyBlocks.SetVectorArray(listVecProName[i], _propertyVectorsBuffer[i]);

            if (preZ)
            {
                Graphics.DrawMeshInstanced(_mesh, 0, matPreZ, _drawMatrices, _drawCount, _propertyBlocks, ShadowCastingMode.Off, false, _layer);
                Graphics.DrawMeshInstanced(_mesh, 0, matPlantAfterPreZ, _drawMatrices, _drawCount, _propertyBlocks, ShadowCastingMode.Off, false, _layer);
            }
            else
            {
                Graphics.DrawMeshInstanced(_mesh, 0, _material, _drawMatrices, _drawCount, _propertyBlocks, ShadowCastingMode.Off, true, _layer);
            }
            
		}
	}
}