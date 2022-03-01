using System.Collections.Generic;
using UnityEngine;

namespace YoukiaEngine
{
    /// <summary>
    /// 优先使用DynamicInstanceObject，硬件不支持GPUInstance自动切换GameObject
    /// </summary>
    public class GPUObject
    {
        private bool _bVisible = true;
        //private string _ModelPath;

        public class TexInfo
        {
            public string mName;
            public Texture mTex;
        }

        public class GroupInfo
        {
            public GameObject mObj;
            public Dictionary<string, TexInfo> mTexInfoMap = new Dictionary<string, TexInfo>();
        }
        
        // instance对象
        private DynamicInstanceObject _instanceObj;
        private GameObject _prefabObj;
        public bool IsNewGroup = false; // 新建分组标识，外部好添加属性
        private static Dictionary<GroupInfo, GeometryInstancingGroup> _geometryInstancingGroups = new Dictionary<GroupInfo, GeometryInstancingGroup>();

        // GameObject对象
        private GameObject _gameObj;
        private Material _material;
        private static List<string> _propertyName = new List<string>();

        public void Init(GameObject model, Transform trans = null, params TexInfo[] texInfos)
        {
            if (GPUDeviceInfo.SupportsInstancing())
            {
                _prefabObj = model;
                

                GeometryInstancingGroup group = GetGroup(model, texInfos);
                //group = null;
                if (group == null)
                {
                    group = GeometryInstancingUtils.CreateGroupByObj(model, texInfos);
                    GroupInfo groupInfo = new GroupInfo();
                    groupInfo.mObj = model;
                    for(int i = 0; i < texInfos.Length; i++)
                    {
                        groupInfo.mTexInfoMap.Add(texInfos[i].mName, texInfos[i]);
                    }
                    _geometryInstancingGroups.Add(groupInfo, group);
                    GeometryInstancingManager.Instance.AddGroup(group);
                    IsNewGroup = true;
                }

                _instanceObj = new DynamicInstanceObject(group);

                MeshFilter filter = model.GetComponent<MeshFilter>();
                if (filter)
                {
                    Mesh mesh = filter.sharedMesh;
                    _instanceObj.LocalBounds = mesh.bounds;
                }

                if (trans)
                {
                    _instanceObj.Position = trans.position;
                    _instanceObj.Rotation = trans.rotation;
                    _instanceObj.Scale = trans.localScale;
                    _instanceObj.UpdateTransform();
                }

                group.AddObject(_instanceObj);
            }
            else
            {
                _gameObj = GameObject.Instantiate(model);
                _gameObj.SetActive(true);
                if (trans)
                {
                    _gameObj.transform.position = trans.position;
                    _gameObj.transform.rotation = trans.rotation;
                    _gameObj.transform.localScale = trans.localScale;
                }

                _material = _gameObj.GetComponentInChildren<Renderer>().material;
                _material.enableInstancing = false;// 腾讯模拟器默认设置不支持Instancing，需要关闭才能正确显示
            }

            model.SetActive(false);
        }

        public void Clear()
        {
            if (_instanceObj != null)
            {
                GeometryInstancingGroup group = _instanceObj.Group;
                group.RemoveObject(_instanceObj);
                if (group.ObjectCount == 0)
                {
                    //if (GeometryInstancingManager.IsOK())
                    //    GeometryInstancingManager.Instance.RemoveGroup(group);
                    //_geometryInstancingGroups.Remove(_prefabObj); todo
                    _prefabObj = null;
                }

                _instanceObj = null;
            }

            if (_gameObj != null)
            {
                GameObject.Destroy(_gameObj);
                _gameObj = null;
            }
        }

        public GeometryInstancingGroup GetGroup(GameObject obj, params TexInfo[] texInfos)
        {
            foreach(var item in _geometryInstancingGroups)
            {
                if(item.Key.mObj == obj)
                {
                    if(texInfos.Length == item.Key.mTexInfoMap.Count)
                    {
                        if(texInfos.Length > 0)
                        {
                            bool isFind = false;
                            for (int i = 0; i < texInfos.Length; i++)
                            {
                                TexInfo texInfo = null;
                                if (item.Key.mTexInfoMap.TryGetValue(texInfos[i].mName, out texInfo))
                                {
                                    if (texInfo.mTex != texInfos[i].mTex)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }

                                if (i == texInfos.Length - 1)
                                {
                                    isFind = true;
                                }
                            }

                            if (isFind)
                            {
                                return item.Value;
                            }
                        }
                        else
                        {
                            return item.Value;
                        }
                        
                    }
                }
            }

            return null;
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
            // 绑定旗帜等是使用的此接口
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

        public void SetRotation(ref Quaternion rotation)
        {
            if (_instanceObj != null)
                _instanceObj.Rotation = rotation;
            if (_gameObj)
                _gameObj.transform.rotation = rotation;
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