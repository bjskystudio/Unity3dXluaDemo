using UnityEngine;
using System.Collections;

namespace YoukiaEngine
{
    /// <summary>
    /// For dynamic usage objects with attributes: Posititon/Rotation/Scale.
    /// </summary>
	public class DynamicInstanceObject : InstanceObject
	{
        private Vector3 _position = Vector3.zero;
        private Quaternion _rotation = Quaternion.identity;
        private Vector3 _scale = Vector3.one;
	    private Bounds _localBounds;

        private bool _isTransformRefresh = false;
        private bool _isBoundsRefresh = false;

		/// <summary>
		/// Posotion
		/// </summary>
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                _isTransformRefresh = true;
            }
        }

		/// <summary>
		/// Rotation
		/// </summary>
	    public Quaternion Rotation
	    {
	        get => _rotation;
	        set
	        {
	            _rotation = value;
	            _isTransformRefresh = true;
            }
	    }

		/// <summary>
		/// Scale
		/// </summary>
	    public Vector3 Scale
	    {
	        get => _scale;
	        set
	        {
	            _scale = value;
	            _isTransformRefresh = true;
            }
	    }

		/// <summary>
		/// Forward
		/// </summary>
	    public Vector3 Forward
        {
	        get => Rotation.eulerAngles;
	        set
	        {
	            _rotation = Quaternion.LookRotation(value);
	            _isTransformRefresh = true;
	        }
	    }

		/// <summary>
		/// Local bounds
		/// </summary>
        public Bounds LocalBounds
        {
            get => _localBounds;
            set
            {
                _localBounds = value;
                if (Bounds == null)
                {
                    Bounds = new InstanceBounds(_localBounds);
                }
                _isBoundsRefresh = true;
            }
        }

		/// <summary>
		/// DynamicInstanceObject
		/// </summary>
		/// <param name="group"></param>
		public DynamicInstanceObject(GeometryInstancingGroup @group):base(@group)
		{
			
		}

		/// <summary>
		/// Update transform
		/// </summary>
		public void UpdateTransform()
		{
			if (_isTransformRefresh)
			{
				Transform.SetTRS(_position, _rotation, _scale);

			    _isTransformRefresh = false;
			    _isBoundsRefresh = true;
			    _isNeedUpdateTrans = true;
			}

		    if (_isBoundsRefresh && Bounds != null)
		    {
		        YoukiaEngineUtils.FastTransformBounds(ref _localBounds, ref Bounds.BoundingBox, ref Transform, ref _scale);
		        Center = Bounds.BoundingBox.center;

		        _isBoundsRefresh = false;
		    }
		}
	}
}
