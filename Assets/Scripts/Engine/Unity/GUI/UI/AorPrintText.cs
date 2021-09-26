using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AorPrintText : AorText
{
    List<UIVertex> vertexlist = new List<UIVertex>();
    public int showCount;
    private int textCount;

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        vertexlist.Clear();
        toFill.GetUIVertexStream(vertexlist);
        textCount = vertexlist.Count;
        for (int i = 0; i < vertexlist.Count; i++)
        {
            if (i >= (showCount * 6))
            {
                UIVertex tempVertex = vertexlist[i];
                tempVertex.uv0 = Vector2.zero;
                vertexlist[i] = tempVertex;
            }
        }
        toFill.Clear();
        toFill.AddUIVertexTriangleStream(vertexlist);
    }
    public int GetTextCount()
    {
        return textCount / 6;
    }

    public void SetShowCount(int num)
    {
        showCount = num;
        SetVerticesDirty();
    }
}