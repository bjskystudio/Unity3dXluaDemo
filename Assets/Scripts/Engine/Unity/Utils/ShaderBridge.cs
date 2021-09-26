using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class ShaderBridge
    {
        /// <summary>
        /// 查找Shader的桥接方法
        /// </summary>
        /// <param name="shaderName"></param>
        /// <param name="callback"></param>
        public static Shader Find(string shaderName)
        {
            return AssetLoadManager.Instance.LoadShader(shaderName);
        }
        /// <summary>
        /// 创建指定名字和Shader的材质球
        /// </summary>
        /// <param name="matName"></param>
        /// <param name="shaderName"></param>
        /// <returns></returns>
        public static Material CreateMaterial(string matName, string shaderName)
        {
            var shader = Find(shaderName);
            Material material = new Material(shader)
            {
                name = matName
            };
            return material;
        }
    }
}
