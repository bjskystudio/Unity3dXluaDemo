using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Framework.AorUI.effects
{
    /// <summary>
    /// 配合CustomFont Shader 使用
    /// </summary>
    public class OutLinePlus : BaseMeshEffect
    {
        [Range(1, 5)]
        public float Size = 1;

        public Color TextColorTop;
        public Color TextColorBottom;

        public Color LineColorTop;
        public Color LineColorBottom;

        private Text target;

        protected override void Start()
        {
            base.Start();
            target = gameObject.GetComponent<Text>();
            if (target != null && target.material == null)
            {
               UnityEngine. Debug.LogWarning("Text组件必须使用配套的CustomFont Shader材质球");
            }
        }


        void getMat()
        {
            if (target == null)
                target = gameObject.GetComponent<Text>();

            if (target != null && target.font != null)
            {
                Material fontMat = target.font.material;
                target.material.SetVector("_size", new Vector4(1f / fontMat.mainTexture.width, 1f / fontMat.mainTexture.height, 0, 0));
                target.material.SetFloat("_Offset", Size);

                target.material.SetColor("_ColorT", TextColorTop);
                target.material.SetColor("_ColorB", TextColorBottom);

                target.material.SetColor("_OutLineColorT", LineColorTop);
                target.material.SetColor("_OutLineColorB", LineColorBottom);
            }
        }

        public override void ModifyMesh(VertexHelper vh) {

            if (!IsActive())
            {
                return;
            }

            int count = vh.currentVertCount;

            getMat();

            UIVertex vtb = new UIVertex();
            vh.PopulateUIVertex(ref vtb, 0);

            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (vtb.uv0 == Vector2.zero)
                    continue;

                if (i % 4 == 3)
                {
                    Vector4 UVrect;


                    float UVScale = 1.5f;

                    /**
                    w,m等字符为了压缩贴图空间,旋转了放置,uv需要根据情况计算

                    y相等,字符在贴图中没颠倒

                     0 -- 1
                     |    |        
                     3 __ 2

                    x相等就是旋转过的

                    2 -- 1
                    |    |
                    3 -- 0
                    **/

                    UIVertex vt0 = new UIVertex();
                    vh.PopulateUIVertex(ref vt0, 0 + index * 4);
                    UIVertex vt1 = new UIVertex();
                    vh.PopulateUIVertex(ref vt1, 1 + index * 4);
                    UIVertex vt2 = new UIVertex();
                    vh.PopulateUIVertex(ref vt2, 2 + index * 4);
                    UIVertex vt3 = new UIVertex();
                    vh.PopulateUIVertex(ref vt3, 3 + index * 4);

                    bool isRot = false;
                    if (vt0.uv0.y != vt1.uv0.y)
                    {
                        UVrect = new Vector4(vt2.uv0.x, vt2.uv0.y, vt0.uv0.x, vt0.uv0.y);
                        isRot = true;
                    }
                    else
                    {
                        UVrect = new Vector4(vt0.uv0.x, vt0.uv0.y, vt2.uv0.x, vt2.uv0.y);
                    }


                    Vector3 center = new Vector2((vt1.position.x - vt0.position.x) * 0.5f, (vt2.position.y - vt0.position.y) * 0.5f);
                    center = new Vector2(center.x + vt0.position.x, center.y + vt0.position.y);
                    //put to org
                    Vector3 pos1 = vt0.position - center;
                    Vector3 pos2 = vt1.position - center;
                    Vector3 pos3 = vt2.position - center;
                    Vector3 pos4 = vt3.position - center;

                    pos1 *= UVScale;
                    pos2 *= UVScale;
                    pos3 *= UVScale;
                    pos4 *= UVScale;

                    vt0.position = pos1 + center;
                    vt1.position = pos2 + center;
                    vt2.position = pos3 + center;
                    vt3.position = pos4 + center;

                    float width;
                    float height;
                    Vector2 UVcenter;

                    if (!isRot)
                    {
                        width = (vt1.uv0.x - vt0.uv0.x) * 0.5f;
                        height = (vt2.uv0.y - vt0.uv0.y) * 0.5f;
                        UVcenter = new Vector2(Mathf.Abs(width) + vt0.uv0.x, Mathf.Abs(height) + vt0.uv0.y);
                    }
                    else
                    {
                        width = (vt1.uv0.x - vt2.uv0.x) * 0.5f;
                        height = (vt3.uv0.y - vt2.uv0.y) * 0.5f;
                        UVcenter = new Vector2(Mathf.Abs(width) + vt2.uv0.x, Mathf.Abs(height) + vt2.uv0.y);

                    }

                    //put to org
                    Vector2 UVpos1 = vt0.uv0 - UVcenter;
                    Vector2 UVpos2 = vt1.uv0 - UVcenter;
                    Vector2 UVpos3 = vt2.uv0 - UVcenter;
                    Vector2 UVpos4 = vt3.uv0 - UVcenter;


                    UVpos1 *= UVScale;
                    UVpos2 *= UVScale;
                    UVpos3 *= UVScale;
                    UVpos4 *= UVScale;
                    /*
                    2-- 1
                    |   |
                    3-- 0
                   */

                    vt0.uv0 = UVpos1 + UVcenter;
                    vt0.uv1 = new Vector2(1, 1);
                    vt0.tangent = UVrect;

                    vt1.uv0 = UVpos2 + UVcenter;
                    vt1.uv1 = new Vector2(0, 1);
                    vt1.tangent = UVrect;

                    vt2.uv0 = UVpos3 + UVcenter;
                    vt2.uv1 = new Vector2(0, 0);
                    vt2.tangent = UVrect;

                    vt3.uv0 = UVpos4 + UVcenter;
                    vt3.uv1 = new Vector2(1, 0);
                    vt3.tangent = UVrect;
                        
                    vh.SetUIVertex(vt0, 0 + index * 4);
                    vh.SetUIVertex(vt1, 1 + index * 4);
                    vh.SetUIVertex(vt2, 2 + index * 4);
                    vh.SetUIVertex(vt3, 3 + index * 4);

                    index += 1;
                }


            }
        }
    }
}