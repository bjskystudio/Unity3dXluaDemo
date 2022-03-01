using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
//using YoukiaCore;

public class NavMeshExport : Editor
{
    static List<Triangle> triangles = new List<Triangle>();

    //[MenuItem("NavMesh/导出Bytes")]
    public static void Export(string savePath, string fileName)
    {
       //_FixData();
       //ByteBuffer byteBuff = new ByteBuffer();
       //byteBuff.writeInt(triangles.Count);
       //foreach (Triangle triangle in triangles)
       //{
       //    byteBuff.writeInt(triangle.index);
       //    byteBuff.writeFloat(ConvertFloat(triangle.a.x));
       //    byteBuff.writeFloat(ConvertFloat(triangle.a.y));
       //    byteBuff.writeFloat(ConvertFloat(triangle.a.z));
       //    byteBuff.writeFloat(ConvertFloat(triangle.b.x));
       //    byteBuff.writeFloat(ConvertFloat(triangle.b.y));
       //    byteBuff.writeFloat(ConvertFloat(triangle.b.z));
       //    byteBuff.writeFloat(ConvertFloat(triangle.c.x));
       //    byteBuff.writeFloat(ConvertFloat(triangle.c.y));
       //    byteBuff.writeFloat(ConvertFloat(triangle.c.z));
       //    byteBuff.writeFloat(ConvertFloat(triangle.center.x));
       //    byteBuff.writeFloat(ConvertFloat(triangle.center.y));
       //    byteBuff.writeFloat(ConvertFloat(triangle.center.z));
       //}
       //
       //string path = savePath + "/" + fileName + ".txt";
       //byte[] data = byteBuff.getArray();
       //FileStream fs = new FileStream(path, FileMode.Create);
       //fs.Write(data, 0, data.Length);
       //fs.Close();
       //
       //Debug.Log("Export Bytes OK");
    }

    static private void _FixData()
    {
        triangles.Clear();
        NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
        Vector3[] vertices = new Vector3[navMeshTriangulation.vertices.Length];
        for (int i = 0; i < navMeshTriangulation.vertices.Length; i++)
        {
            vertices[i] = new Vector3()
            {
                x = navMeshTriangulation.vertices[i].x,
                y = navMeshTriangulation.vertices[i].y,
                z = navMeshTriangulation.vertices[i].z
            };
        }

        int _index = 0;
        for (int i = 0; i < navMeshTriangulation.indices.Length;)
        {
            Vector3 _x = vertices[navMeshTriangulation.indices[i++]];
            Vector3 _y = vertices[navMeshTriangulation.indices[i++]];
            Vector3 _z = vertices[navMeshTriangulation.indices[i++]];
            Triangle temp = new Triangle(_x, _y, _z, _index);
            triangles.Add(temp);
            _index++;
        }
    }

    //[MenuItem("NavMash/导出Text")]
    static void ExportToText()
    {
        _FixData();
        var configCSFiles = Directory.GetFiles(Application.dataPath + "/NavmeshData", "*.txt", SearchOption.AllDirectories);
        List<string> sContent = new List<string>();
        foreach (Triangle triangle in triangles)
        {
            string sPrefix = "index: " + triangle.index + "  " + ConvertFloat(triangle.a.x) + "," + ConvertFloat(triangle.a.y) + "," + ConvertFloat(triangle.a.z) + "    ";
            sPrefix += ConvertFloat(triangle.b.x) + "," + ConvertFloat(triangle.b.y) + "," + ConvertFloat(triangle.b.z) + "   ";
            sPrefix += ConvertFloat(triangle.c.x) + "," + ConvertFloat(triangle.c.y) + "," + ConvertFloat(triangle.c.z) + "   ";
            sContent.Add(sPrefix);
        }

        File.WriteAllLines(configCSFiles[0], sContent.ToArray());

        Debug.Log("Export Text OK");
    }

    static float ConvertFloat(float value)
    {
        return (float)System.Math.Round((decimal)value * 1000);
    }
}

/// <summary>
/// 三角形
/// </summary>
public class Triangle
{
    public int index;
    public Vector3 a;
    public Vector3 b;
    public Vector3 c;
    public Vector3 center;

    public Triangle(Vector3 _a, Vector3 _b, Vector3 _c, int _index)
    {
        a = _a;
        b = _b;
        c = _c;
        center = new Vector3((a.x + b.x + c.x) / 3f, (a.y + b.y + c.y) / 3f, (a.z + b.z + c.z) / 3f);
        index = _index;
    }
}