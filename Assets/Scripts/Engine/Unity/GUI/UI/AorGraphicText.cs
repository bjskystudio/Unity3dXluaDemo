using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AorGraphicText : AorText
{
    public struct Info
    {
        public int index;
        public Vector2 size;
        public string path;
        public Info(int _index, string _path, float _width, float _height)
        {
            index = _index;
            size = new Vector2(_width, _height);
            path = _path;
        }
    }

    private static readonly Regex m_spriteTagRegex =
new Regex(@"<quad path=(.+?) size=(\d*\.?\d+%?) width=(\d*\.?\d+%?)/(>)", RegexOptions.Singleline);
    private static readonly string miscstr = "##fgh";
    private static readonly Regex miscRegex = new Regex(@miscstr, RegexOptions.Singleline);
    //    private static readonly Regex m_spriteTagRegex =
    //new Regex(@"<quad name=(.*) size=(.*)width=(.*)/(>)", RegexOptions.Singleline);
    List<int> indexs = new List<int>();
    List<Info> infos = new List<Info>();
    List<UIVertex> vertexlist = new List<UIVertex>();
    private List<AorRawImage> images = null;
    public string oldtext = "-12123890138902";
    string temptext;
    public bool isRefresh;
    public int step;
    public List<AorRawImage> Images
    {
        get
        {
            if (Application.isPlaying)
            {
                if (images != null)
                {
                    return images;
                }
            }
            images = new List<AorRawImage>();
            GetComponentsInChildren<AorRawImage>(true, images);
            return images;
        }

    }
    public void Update()
    {
        if (isRefresh)
        {
            DealImage();
            isRefresh = false;
        }
    }
    public void DealImage()
    {
        if (Application.isPlaying)
        {
            for (int i = Images.Count; i < step; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(transform, false);
                AorRawImage imag = obj.AddComponent<AorRawImage>();
                imag.color = Color.white;
                imag.raycastTarget = false;
                Images.Add(imag);
            }
        }
        for (int i = 0; i < Images.Count; i++)
        {
            if (i < step)
            {
                if (Application.isPlaying)
                {
                    Images[i].LoadTexture(infos[i].path);
                    images[i].GetComponent<RectTransform>().sizeDelta = infos[i].size;
                }
            }
            else
            {
                Images[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
            }
        }
        for (int z = 0; z < infos.Count; z++)
        {
            var info = infos[z];
            int index = info.index;
            if (Images.Count > z)
            {
                Vector2 pos;
                if (index * 6 + 4 > vertexlist.Count)
                {
                    Images[z].GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
                    continue;
                }
                pos = vertexlist[index * 6 + 4].position;
                pos.y += (info.size.y / 4);
                pos.x += (info.size.x / 2);
                Images[z].GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        vertexlist.Clear();
        toFill.GetUIVertexStream(vertexlist);
        Deal();
        for (int z = 0; z < infos.Count; z++)
        {
            var info = infos[z];
            int index = info.index;
            if (Images.Count > z)
            {
                Vector2 pos;
                if (index * 6 + 4 > vertexlist.Count)
                {
                    Images[z].GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
                    continue;
                }
                pos = vertexlist[index * 6 + 4].position;
                pos.y += (info.size.y / 4);
                pos.x += (info.size.x / 2);
                Images[z].GetComponent<RectTransform>().anchoredPosition = pos;
            }
            for (int i = index * 6; i < index * 6 + 6; i++)
            {
                if (i >= vertexlist.Count)
                {
                    break;
                }
                UIVertex tempVertex = vertexlist[i];
                tempVertex.uv0 = Vector2.zero;
                vertexlist[i] = tempVertex;
            }
        }
        if (!Application.isPlaying)
        {
            DealImage();
        }
        toFill.Clear();
        toFill.AddUIVertexTriangleStream(vertexlist);
    }
    public void Deal()
    {
        if (oldtext != text || !Application.isPlaying)
        {
            int len = 0;
            indexs.Clear();
            infos.Clear();
            step = 0;
            temptext = text;
            foreach (Match match in m_spriteTagRegex.Matches(text))
            {
                temptext = temptext.Replace(match.Value.ToString(), miscstr);
                infos.Add(new Info(match.Index, match.Groups[1].Value.ToString(), float.Parse(match.Groups[2].Value) * float.Parse(match.Groups[3].Value), float.Parse(match.Groups[2].Value)));
                len += (match.Groups[4].Index - match.Index);
                step++;
            }
            temptext = Regex.Replace(temptext, @"( )|(\n)|(</color>)|(<color=(.+?)>)|(<size=(\d+?)>)|(</size>)", "", RegexOptions.Singleline);
            int index = 0;
            string str = temptext.Replace(miscstr, "#");
            if (str.Length == (vertexlist.Count / 6))//区别于是否有被Truncate false 则被裁剪了
            {
                foreach (Match match in miscRegex.Matches(temptext))
                {
                    var v = infos[index];
                    v.index = match.Index - (index * (miscstr.Length - 1));
                    infos[index] = v;
                    index++;
                }
            }
            for (int i = 0; i < Images.Count; i++)
            {
                if (i < step)
                {
                    if (Application.isPlaying)
                    {
                        Images[i].LoadTexture(infos[i].path);
                        images[i].GetComponent<RectTransform>().sizeDelta = infos[i].size;
                    }
                }
                else
                {
                    Images[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(10000, 10000);
                }
            }
            oldtext = text;
            isRefresh = true;
        }
    }
}
