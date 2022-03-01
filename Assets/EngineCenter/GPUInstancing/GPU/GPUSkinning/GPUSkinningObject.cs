using System.Collections.Generic;
using UnityEngine;

namespace YoukiaEngine
{
    /// <summary>
    /// 优先使用GPUSkinningInstanceObject，硬件不支持GPUInstance自动切换GameObject
    /// </summary>
    public class GPUSkinningObject
    {
        private bool _bVisible = true;
        //private string _ModelPath;

        // instance对象
        private GPUSkinningInstanceObject _instanceObj;
        private GameObject _prefabObj;
        public bool IsNewGroup = false; // 新建分组标识，外部好添加属性
        private static Dictionary<GameObject, GeometryInstancingGroup> _geometryInstancingGroups = new Dictionary<GameObject, GeometryInstancingGroup>();

        // GameObject对象
        private GameObject _gameObj;
        private GPUSkinningAnimator _animator;
        private Material _material;
        private static List<string> _propertyName = new List<string>();

        public AnimationClip CurrentClip
        {
            get
            {
                if (_instanceObj != null)
                {
                    return _instanceObj.CurrentClip;
                }

                if (_animator)
                {
                    return _animator.CurrentClip;
                }

                return null;
            }
        }

        public Vector4 GetCtrlVector()
        {
            if(_instanceObj == null)
            {
                return Vector4.zero;
            }
            return _instanceObj.GetCtrlVector();
        }

        public Vector4 GetStatesVector()
        {
            if (_instanceObj == null)
            {
                return Vector4.zero;
            }
            return _instanceObj.GetStatesVector();
        }

        public void SetPropertyVectorCtrl(Vector4 value)
        {
            if (_instanceObj == null)
            {
                return;
            }
            _instanceObj.SetPropertyVectorCtrl(value);
        }

        public void SetPropertyVectorState(Vector4 value)
        {
            if (_instanceObj == null)
            {
                return;
            }
            _instanceObj.SetPropertyVectorState(value);
        }

        public float Speed
        {
            set
            {
                if(_instanceObj != null)
                {
                    _instanceObj.Speed = value;
                }
            }
        }

        public Mesh CurrentMesh
        {
            get
            {
                if(_instanceObj == null)
                {
                    return null;
                }
                return _instanceObj.Group.Mesh;
            }
        }

        public Material CurrentMaterial
        {
            get
            {
                if (_instanceObj == null)
                {
                    return null;
                }
                return _instanceObj.Group.Material;
            }
        }

        public AnimationClip[] AnimClips
        {
            get
            {
                if (_instanceObj != null)
                {
                    return _instanceObj.AnimClips;
                }

                if (_animator)
                {
                    return _animator.AnimClips;
                }

                return null;
            }
        }

        public void Init(GameObject model, Mesh mesh,Material mat, AnimationClip[] clips)
        {
            _prefabObj = model;

            GeometryInstancingGroup group;
            if (!_geometryInstancingGroups.TryGetValue(model, out group))
            {
                group = GPUSkinningInstanceObject.CreateGpuSkinningGroup(mesh, mat, model.layer);
                _geometryInstancingGroups.Add(model, group);
                GeometryInstancingManager.Instance.AddGroup(group);
                IsNewGroup = true;
            }

            _instanceObj = new GPUSkinningInstanceObject(group, clips);
            _instanceObj.LocalBounds = mesh.bounds;

            group.AddObject(_instanceObj);
        }

        public void Init(GameObject model, Transform trans = null,bool Instantiate = false)
        {
            GPUSkinningAnimator animator = model.GetComponentInChildren<GPUSkinningAnimator>(true);
            if (!animator)
            {
                return;
            }
            
            GameObject animModel = animator.gameObject;

            if (GPUDeviceInfo.SupportsInstancing())
            {
                _prefabObj = model;
                if(Instantiate == true)
                {
                    _gameObj = model;
                }
                AnimationClip[] clips = animator.AnimClips;
                Mesh mesh = animModel.GetComponent<MeshFilter>().sharedMesh;

                GeometryInstancingGroup group;
                if (!_geometryInstancingGroups.TryGetValue(model, out group))
                {
                    group = GPUSkinningInstanceObject.CreateGpuSkinningGroup(animModel);
                    _geometryInstancingGroups.Add(model, group);
                    GeometryInstancingManager.Instance.AddGroup(group);
                    IsNewGroup = true;
                }

                _instanceObj = new GPUSkinningInstanceObject(group, clips);
                _instanceObj.LocalBounds = mesh.bounds;
                if (trans)
                {
                    _instanceObj.Position = trans.position;
                    _instanceObj.Rotation = trans.rotation;
                    _instanceObj.Scale = trans.localScale;
                }

                group.AddObject(_instanceObj);
            }
            else
            {
                _gameObj = GameObject.Instantiate(animModel);
                _gameObj.SetActive(true);
                if (trans)
                {
                    Transform tf = _gameObj.transform;
                    tf.SetParent(trans);
                    tf.position = trans.position;
                    tf.localEulerAngles = Vector3.zero;
                    tf.localScale = trans.localScale;
                }

                _animator = _gameObj.GetComponentInChildren<GPUSkinningAnimator>(true);
                _material = _gameObj.GetComponentInChildren<Renderer>().material;
                _material.enableInstancing = false;// 腾讯模拟器默认设置不支持Instancing，需要关闭才能正确显示
            }

            animModel.SetActive(false);
        }

        public void Clear()
        {
            if (_instanceObj != null)
            {
                GeometryInstancingGroup group = _instanceObj.Group;
                group.RemoveObject(_instanceObj);
                if (group.ObjectCount == 0)
                {
                    if(GeometryInstancingManager.IsOK())
                        GeometryInstancingManager.Instance.RemoveGroup(group);
                    _geometryInstancingGroups.Remove(_prefabObj);
                    _prefabObj = null;
                }

                _instanceObj = null;
            }

            // 摧毁蒙皮模型对象
            _animator = null;
            if (_gameObj != null)
            {
                GameObject.Destroy(_gameObj);
                _gameObj = null;
            }
        }

        private string _nowState;
        private WrapMode _nowMode;
        private float _nowSpeed;
        private float _nowStarTime;

        public void RePlay()
        {
            if (_nowState != null)
            {
                Play(_nowState, _nowMode, _nowSpeed, _nowStarTime);
            }
        }

        public void Play(string anim, WrapMode wrap, float speed, float startTime)
        {
            if (_instanceObj != null)
            {
                _instanceObj.WarpMode = wrap;
                _instanceObj.Speed = speed;
                _instanceObj.Play(anim, startTime);
            }
            else if (_animator != null)
            {
                _animator.WarpMode = wrap;
                _animator.Speed = speed;
                _animator.Play(anim, startTime);
            }
            else
            {
                _nowState = anim;
                _nowMode = wrap;
                _nowSpeed = speed;
                _nowStarTime = startTime;
            }
        }

        /// <summary>
        /// 暂停定帧
        /// </summary>
        public void Pause()
        {
            if (_instanceObj != null)
            {
                _instanceObj.Pause();
            }
            else if (_animator != null)
            {
                _animator.Pause();
            }
        }

        public void Play(string anim, float startTime = 0)
        {
            if (_instanceObj != null)
            {
                _instanceObj.Play(anim, startTime);
            }
            else if (_animator != null)
            {
                _animator.Play(anim, startTime);
            }
            else
            {
                _nowState = anim;
                _nowStarTime = startTime;
            }
        }

        public void SetLayer(int layer)
        {
            if (_instanceObj != null)
            {
                _instanceObj.Group.Layer = layer;
            }

            if (_gameObj)
            {
                _gameObj.layer = layer;
            }
        }

        public void SetLocalBounds(Bounds bounds)
        {
            if (_instanceObj != null)
            {
                _instanceObj.LocalBounds = bounds;
            }
        }

        /// <summary>
        /// 更新Transform（做为子节点时，比如绑定旗帜等是使用的此接口）
        /// </summary>
        /// <param name="mRootTransform"></param>
        public void UpdateTransform(Transform mRootTransform)
        {
            if (_instanceObj != null)
            {
                if (_instanceObj.Transform != mRootTransform.localToWorldMatrix)
                {
                    _instanceObj.Transform = mRootTransform.localToWorldMatrix;
                    Vector3 scale = mRootTransform.localScale;
                    Bounds localbounds = _instanceObj.LocalBounds;
                    YoukiaEngineUtils.FastTransformBounds(ref localbounds, ref _instanceObj.Bounds.BoundingBox,
                        ref _instanceObj.Transform, ref scale);
                    _instanceObj.ManualUpdateTransform();
                }
            }

            if (_gameObj)
            {
                if (_gameObj.transform.parent != mRootTransform)
                {
                    _gameObj.transform.SetParent(mRootTransform);
                    _gameObj.transform.localPosition = Vector3.zero;
                    _gameObj.transform.localEulerAngles = Vector3.zero;
                    _gameObj.transform.localScale = Vector3.one;
                }
            }
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        public void SetPosition(ref Vector3 position)
        {
            if (_instanceObj != null)
                _instanceObj.Position = position;
            if (_gameObj)
                _gameObj.transform.position = position;
        }

        public void SetForward(ref Vector3 forward)
        {
            if (_instanceObj != null)
                _instanceObj.Forward = forward;
            if (_gameObj)
                _gameObj.transform.forward = forward;
        }

        public void SetScale(ref Vector3 scale)
        {
            if (_instanceObj != null)
                _instanceObj.Scale = scale;
            if (_gameObj)
                _gameObj.transform.localScale = scale;
        }

        public void SetVisible(bool b)
        {
            _bVisible = b;
            if (_instanceObj != null)
            {
                _instanceObj.IsVisible = b;
            }
            if (_gameObj)
            {
                _gameObj.SetActive(b);
            }
        }

        public bool GetVisible()
        {
            return _bVisible;
        }

        /// <summary>
        /// 添加Vector属性
        /// </summary>
        /// <param name="name"></param>
        public void RegisterVectorProperty(string name)
        {
            if (_instanceObj != null)
            {
                _instanceObj.Group.RegisterVectorProperty(name);
            }
            else if (_material)
            {
                if (!_propertyName.Contains(name))
                {
                    _propertyName.Add(name);
                }
            }
        }

        public void SetPropertyVector(string name, Vector4 value)
        {
            if (_instanceObj != null)
            {
                _instanceObj.SetPropertyVector(name, value);
            }
            else if (_material)
            {
                _material.SetVector(name, value);
            }
        }

        public void SetPropertyVector(int index, Vector4 value)
        {
            if (_instanceObj != null)
            {
                _instanceObj.SetPropertyVector(index, value);
            }
            else if (_material)
            {
                if (index >= 0 && index < _propertyName.Count)
                {
                    _material.SetVector(_propertyName[index], value);
                }
            }
        }

        public int GetVectorPropertyHandle(string name)
        {
            if (_instanceObj != null)
            {
                return _instanceObj.GetVectorPropertyHandle(name);
            }
            else if (_material)
            {
                int idx = _propertyName.IndexOf(name);
                if (idx == -1)
                {
                    _propertyName.Add(name);
                    idx = _propertyName.IndexOf(name);
                }
                return idx;
            }
            return -1;
        }

        /// <summary>
        /// 添加Float属性
        /// </summary>
        /// <param name="name"></param>
        public void RegisterFloatProperty(string name)
        {
            if (_instanceObj != null)
            {
                _instanceObj.Group.RegisterFloatProperty(name);
            }
            else if (_material)
            {
                if (!_propertyName.Contains(name))
                {
                    _propertyName.Add(name);
                }
            }
        }

        public void SetPropertyFloat(string name, float value)
        {
            if (_instanceObj != null)
            {
                _instanceObj.SetPropertyFloat(name, value);
            }
            else if (_material)
            {
                _material.SetFloat(name, value);
            }
        }

        public void SetPropertyFloat(int index, float value)
        {
            if (_instanceObj != null)
            {
                _instanceObj.SetPropertyFloat(index, value);
            }
            else if (_material)
            {
                if (index >= 0 && index < _propertyName.Count)
                {
                    _material.SetFloat(_propertyName[index], value);
                }
            }
        }

        public int GetFloatPropertyHandle(string name)
        {
            if (_instanceObj != null)
            {
                return _instanceObj.GetFloatPropertyHandle(name);
            }
            else if (_material)
            {
                int idx = _propertyName.IndexOf(name);
                if (idx == -1)
                {
                    _propertyName.Add(name);
                    idx = _propertyName.IndexOf(name);
                }
                return idx;
            }
            return -1;
        }

    }

}