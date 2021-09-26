using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.AorUI
{
    [AddComponentMenu("UI/Effects/Gradient")]
    public class Gradient : Outline, IGrayMember
    {
        [SerializeField]
        Type _gradientType = Type.Vertical;

        [SerializeField]
        Blend _blendMode = Blend.Multiply;

        [SerializeField]
        [Range(-1, 1)]
        float _offset = 0f;

        [SerializeField]
        private UnityEngine.Gradient GradientColor;

        private GradientColorKey[] OldGradientColorKeys;
        private GradientAlphaKey[] OldGradientAlphaKeys;
        private Color OldEffectColor;

        #region Properties
        public Blend BlendMode
        {
            get { return _blendMode; }
            set { _blendMode = value; }
        }

        public Type GradientType
        {
            get { return _gradientType; }
            set { _gradientType = value; }
        }

        public float Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        private bool _IsGray;
        public bool IsGray
        {
            get { return _IsGray; }
        }
        #endregion
        protected override void Awake()
        {
            base.Awake();

        }
        public UnityEngine.Gradient GetGradient()
        {
            if (GradientColor == null)
            {
                GradientColor = new UnityEngine.Gradient();
            }
            return GradientColor;
        }
        public override void ModifyMesh(VertexHelper helper)
        {
            if (!IsActive() || helper.currentVertCount == 0)
                return;

            List<UIVertex> _vertexList = new List<UIVertex>();

            helper.GetUIVertexStream(_vertexList);

            int nCount = _vertexList.Count;
            switch (GradientType)
            {
                case Type.Horizontal:
                    {
                        float left = _vertexList[0].position.x;
                        float right = _vertexList[0].position.x;
                        float x = 0f;

                        for (int i = nCount - 1; i >= 1; --i)
                        {
                            x = _vertexList[i].position.x;

                            if (x > right) right = x;
                            else if (x < left) left = x;
                        }

                        float width = 1f / (right - left);
                        UIVertex vertex = new UIVertex();

                        for (int i = 0; i < helper.currentVertCount; i++)
                        {
                            helper.PopulateUIVertex(ref vertex, i);
                            if (GradientColor == null)
                                GradientColor = new UnityEngine.Gradient();

                            vertex.color = BlendColor(vertex.color, GradientColor.Evaluate((vertex.position.x - left) * width - Offset));

                            helper.SetUIVertex(vertex, i);
                        }
                    }
                    break;

                case Type.Vertical:
                    {
                        float bottom = _vertexList[0].position.y;
                        float top = _vertexList[0].position.y;
                        float y = 0f;

                        for (int i = nCount - 1; i >= 1; --i)
                        {
                            y = _vertexList[i].position.y;

                            if (y > top) top = y;
                            else if (y < bottom) bottom = y;
                        }

                        float height = 1f / (top - bottom);
                        UIVertex vertex = new UIVertex();

                        for (int i = 0; i < helper.currentVertCount; i++)
                        {
                            helper.PopulateUIVertex(ref vertex, i);
                            if (GradientColor == null)
                                GradientColor = new UnityEngine.Gradient();
                            vertex.color = BlendColor(vertex.color, GradientColor.Evaluate((vertex.position.y - bottom) * height - Offset));

                            helper.SetUIVertex(vertex, i);
                        }
                    }
                    break;
            }

            base.ModifyMesh(helper);

        }

        Color BlendColor(Color colorA, Color colorB)
        {
            switch (BlendMode)
            {
                default: return colorB;
                case Blend.Add: return colorA + colorB;
                case Blend.Multiply: return colorA * colorB;
            }
        }

        public enum Type
        {
            Horizontal,
            Vertical
        }

        public enum Blend
        {
            Override,
            Add,
            Multiply
        }
        public void SetGrayEffect(bool isGray)
        {
            if (_IsGray == isGray)
                return;

            _IsGray = isGray;
            if (_IsGray)
            {
                OldEffectColor = effectColor;
                OldGradientColorKeys = GradientColor.colorKeys;
                OldGradientAlphaKeys = GradientColor.alphaKeys;

                GradientColorKey key = new GradientColorKey(new Color(0.5f, 0.5f, 0.5f, 1), 0);
                GradientColor.colorKeys = new GradientColorKey[] { key };
                effectColor = new Color(0.02f, 0.02f, 0.02f, effectColor.a);
            }
            else
            {
                effectColor = OldEffectColor;
                GradientColor.SetKeys(OldGradientColorKeys, OldGradientAlphaKeys);

            }
        }
    }
}