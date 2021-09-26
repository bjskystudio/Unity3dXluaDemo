using UnityEngine;
using UnityEngine.UI;

public class AlphaMask : BaseMeshEffect
{
    private static Vector2[] uvList= new Vector2[] {
        new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1),  new Vector2(1, 0)
    };

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
        {
            return;
        }

        var count = vh.currentVertCount;
        if (count != 4)
            return;
        UIVertex tempVertex = new UIVertex();
        for (int i = 0; i < 4; i++)
        {
            vh.PopulateUIVertex(ref tempVertex, i);
            tempVertex.uv1 = uvList[i];
            vh.SetUIVertex(tempVertex, i);
        }
    }
}
