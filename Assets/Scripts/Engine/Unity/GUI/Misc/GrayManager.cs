using Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Gradient = Framework.AorUI.Gradient;
using Object = UnityEngine.Object;

public static class GrayManager
{
    public static Dictionary<Object, Color> OldColorsForGray;

    public static Material _DefFontMaterial;
    public static Material _DefImageMaterial;
    public static Color ColorGray = new Color(0, 0, 0, 1);

    public static void Init()
    {
        if (OldColorsForGray == null)
        {
            OldColorsForGray = new Dictionary<Object, Color>();
        }
        else
        {
            OldColorsForGray.Clear();
        }
        //填充后处理用Shader&Material
        if (_DefFontMaterial == null)
        {
            _DefFontMaterial = ShaderBridge.CreateMaterial("TempFontMat", "Custom/Fonts/Default Font");
        }
        //填充后处理用Shader&Material
        if (_DefImageMaterial == null)
        {
            _DefImageMaterial = ShaderBridge.CreateMaterial("TempImageMat", "Custom/Sprites/SpriteUI");
        }
    }

    public static Material DefFontMaterial
    {
        get
        {
            return _DefFontMaterial;
        }
    }

    public static Material DefImageMaterial
    {
        get
        {
            return _DefImageMaterial;
        }
    }

    private static void ClearDictionary()
    {
        Object[] ops = OldColorsForGray.Keys.ToArray();
        for (int i = 0; i < ops.Length; i++)
        {
            if (!ops[i])
            {
                OldColorsForGray.Remove(ops[i]);
            }
        }
    }

    /// <summary>
    /// 处理Text的Gray
    /// </summary>
    public static void SetGray(this RawImage image, bool isGray)
    {
        ClearDictionary();
        bool _isGray = false;

        if (OldColorsForGray.ContainsKey(image) || image.color == Color.black)
        {
            _isGray = true;

        }
        if (_isGray == isGray)
            return;
        switch (image.material.shader.name)
        {
            case "UI/Default":
                if (DefImageMaterial == null)
                    return;
                image.material = DefImageMaterial;
                image.SetMaterialDirty();
                break;
        }
        if (isGray)
        {
            OldColorsForGray.Add(image, image.color);
            image.color = new Color(0, 0, 0, image.color.a);
        }
        else if (OldColorsForGray.ContainsKey(image))
        {
            image.color = OldColorsForGray[image];
            OldColorsForGray.Remove(image);
        }
    }

    /// <summary>
    /// 处理Text的Gray
    /// </summary>
    public static void SetGray(this Image image, bool isGray)
    {
        ClearDictionary();
        bool _isGray = false;

        if (OldColorsForGray.ContainsKey(image) || image.color == Color.black)
        {
            _isGray = true;
        }
        if (_isGray == isGray)
            return;
        switch (image.material.shader.name)
        {
            case "UI/Default":
                if (DefImageMaterial == null)
                    return;
                image.material = DefImageMaterial;
                image.SetMaterialDirty();
                break;
        }
        if (isGray)
        {
            OldColorsForGray.Add(image, image.color);
            image.color = new Color(0, 0, 0, image.color.a);
        }
        else if (OldColorsForGray.ContainsKey(image))
        {
            image.color = OldColorsForGray[image];
            OldColorsForGray.Remove(image);
        }
    }

    /// <summary>
    /// 处理Text的Gray
    /// </summary>
    public static void ProcessTextSetGray(Text text, bool isGray)
    {
        ClearDictionary();
        if (isGray)
        {
            if (OldColorsForGray == null)
            {
                OldColorsForGray = new Dictionary<Object, Color>();
            }

            switch (text.material.shader.name)
            {
                case "UI/Default Font":
                    if (DefFontMaterial == null)
                    {
                        Debug.LogWarning("AorButtom.setGray :: can not find the Shader<Custom/Fonts/Default Font>");
                        return;
                    }
                    text.material = DefFontMaterial;
                    break;
                case "Custom/Fonts/SpriteUI CustomFont":
                case "Custom/Fonts/SpriteUI CustomFont Gradient":
                    break;
                default:
                    if (Application.platform == RuntimePlatform.WindowsEditor ||
                        Application.platform == RuntimePlatform.OSXEditor)
                    {
                        //Debug.Log("AorButtom.setGray :: subChild : Text[" + text.transform.getHierarchyPath() +
                        //            "]Not use supported Shader. setGray Faild.");
                    }
                    break;
            }


            Color oldcol = text.color;

            if (!OldColorsForGray.ContainsKey(text))
                OldColorsForGray.Add(text, oldcol);

            text.color = new Color(1, 1, 1, text.color.a);
        }
        else if (OldColorsForGray.ContainsKey(text))
        {

            text.color = OldColorsForGray[text];
            OldColorsForGray.Remove(text);
        }
    }
    /// <summary>
    /// 处理outline的Gray
    /// </summary>
    public static void ProcessOutlineSetGray(Outline line, bool isGray)
    {
        ClearDictionary();
        if (isGray)
        {
            if (OldColorsForGray == null)
            {
                OldColorsForGray = new Dictionary<Object, Color>();
            }
            Color oldcol = line.effectColor;
            if (!OldColorsForGray.ContainsKey(line))
                OldColorsForGray.Add(line, oldcol);

            line.effectColor = new Color(0.02f, 0.02f, 0.02f, oldcol.a);
        }
        else if (OldColorsForGray.ContainsKey(line))
        {
            line.effectColor = OldColorsForGray[line];
            OldColorsForGray.Remove(line);
        }
    }

    public static void SetGaryWithAllChildren(Transform trans, bool bGray)
    {
        SetChildGrayLoop(trans, bGray);
    }

    private static void SetChildGrayLoop(Transform t, bool isGray)
    {
        IGrayMember[] ims = t.GetInterfaces<IGrayMember>();
        if (ims != null && ims.Length > 0)
        {
            for (int n = 0; n < ims.Length; n++)
            //foreach (IGrayMember im in ims)
            {
                if (!(ims[n] is AorButton))
                    ims[n].SetGrayEffect(isGray);
            }
        }
        //单独处理Text
        Text text = t.GetComponent<Text>();
        if (text != null)
        {
            ProcessTextSetGray(text, isGray);
        }

        //Gradient 的逻辑在上面的循环中完成 这里不再走继承类的逻辑
        Gradient gradient = t.GetComponent<Gradient>();
        if (gradient == null)
        {
            Outline line = t.GetComponent<Outline>();
            if (line != null)
            {
                ProcessOutlineSetGray(line, isGray);
            }
        }

        int i, len = t.childCount;

        for (i = 0; i < len; i++)
        {
            SetChildGrayLoop(t.GetChild(i), isGray);
        }
    }

    public static void Dispose()
    {
        OldColorsForGray.Clear();
        OldColorsForGray = null;
        if (_DefFontMaterial != null)
        {
            GameObject.Destroy(_DefFontMaterial);
        }
        _DefFontMaterial = null;
        if (_DefImageMaterial != null)
        {
            GameObject.Destroy(_DefImageMaterial);
        }
        _DefImageMaterial = null;
    }
}

