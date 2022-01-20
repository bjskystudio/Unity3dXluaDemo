using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using XLua;
using System.Threading.Tasks;

public class GuideUI
{
    public RectTransform guideExplain;
    public RectTransform guideGesture;
    public RectTransform guideGestureEffect;
    public RectTransform guideButton;
}

public class GuideMask : MaskableGraphic
{
    private GuideUI guideUI;
    public GuideUI GuideUI { get { return guideUI; } }

    private Canvas useCanvas;

    [SerializeField]
    private RectTransform guideTarget;

    private Vector3 guideTargetMin = Vector3.zero;

    private Vector3 guideTargetMax = Vector3.zero;

    private Transform selfCacheTrans = null;

#if UNITY_EDITOR
    [BlackList]
    [HideInInspector]
    public bool IsGuideEditorModel = false;
    private RectTransform lastGuideTarget;
    private Dictionary<RectTransform, Vector2> cacheRTPos;
    private string pasteGuideGestureRelativePos = "";
    private string pasteGuideExplainRelativePos = "";
    // 以下为配置表字段
    [SerializeField]
    [HideInInspector]
    private string guideSid = "";
    [SerializeField]
    [HideInInspector]
    private string guideTipsRelativePath = "";
    [SerializeField]
    [HideInInspector]
    private string cloneIndex = "";
    [SerializeField]
    [HideInInspector]
    [ReadOnlySerializedProperty]
    private string guideGestureRelativePos = "";
    [SerializeField]
    [HideInInspector]
    private string guideGestureRotate = "";
    [SerializeField]
    [HideInInspector]
    private string isFlipHorizontal = "";
    [SerializeField]
    [HideInInspector]
    [ReadOnlySerializedProperty]
    private string guideExplainRelativePos = "";
    [SerializeField]
    [HideInInspector]
    private string guideExplainStyle = "";
    [SerializeField]
    [HideInInspector]
    private string guideExplainDesc = "";
    [SerializeField]
    [HideInInspector]
    private string guideEffectPath = "";
#endif

    protected override void Awake()
    {
        base.Awake();
        selfCacheTrans = GetComponent<RectTransform>();
        if (Application.isPlaying)
            useCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        else
            useCanvas = GameObject.Find("UIRoot").GetComponent<Canvas>();
        guideUI = new GuideUI();
        guideUI.guideExplain = transform.parent.Find("@_obj_guideExplain").GetComponent<RectTransform>();
        guideUI.guideGesture = transform.parent.Find("@_obj_guideGesture").GetComponent<RectTransform>();
        guideUI.guideGestureEffect = transform.parent.Find("@_aorimage_guideGestureEffect").GetComponent<RectTransform>();
        guideUI.guideButton = transform.parent.Find("@_aorbtn_guideButton").GetComponent<RectTransform>();
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying || IsGuideEditorModel)
        {
            if (null == lastGuideTarget)
            {
                lastGuideTarget = guideTarget;
                RefreshView();
            }
            else
            {
                if (lastGuideTarget != guideTarget)
                {
                    lastGuideTarget = guideTarget;
                    RefreshView();
                }
            }
        }
    }

    [BlackList]
    public void SetGuideTarget(GameObject target)
    {
        if (target != null)
            guideTarget = target.GetComponent<RectTransform>();
    }

    [BlackList]
    public async void CheckAndSetTarget()
    {
        if (guideTarget == null && !string.IsNullOrEmpty(guideTipsRelativePath))
        {
            int childIndex;
            int.TryParse(cloneIndex, out childIndex);
            SetGuideTargetPath(guideTipsRelativePath, childIndex, 0);
            await Task.Delay(100);
            string[] pasteGuideGestureRelativePosStrs = pasteGuideGestureRelativePos.Split('#');
            string[] pasteGuideExplainRelativePosStrs = pasteGuideExplainRelativePos.Split('#');
            SetGuideInfo(float.Parse(pasteGuideGestureRelativePosStrs[0]), float.Parse(pasteGuideGestureRelativePosStrs[1]), float.Parse(pasteGuideExplainRelativePosStrs[0]), float.Parse(pasteGuideExplainRelativePosStrs[1]), isFlipHorizontal.ToLower().Equals("true") ? 180 : 0, float.Parse(guideGestureRotate));
        }
    }

    [BlackList]
    public void SetCacheValue(string str1, string str2)
    {
        pasteGuideGestureRelativePos = str1;
        pasteGuideExplainRelativePos = str2;
    }

    /// <summary>
    /// 获取预制件相对路径及坐标增量描述，主要是为了方便配置
    /// </summary>
    [BlackList]
    public void RefreshProperty()
    {
        if (cacheRTPos == null)
            return;

        GameObject rootgo = UIModel.Inst.NormalUIRoot.gameObject;
        GameObject currentgo = guideTarget.gameObject;
        guideTipsRelativePath = CSGoHelp.GetNodeRelativeParentPath(rootgo, currentgo);

        guideGestureRotate = guideUI.guideGesture.localRotation.eulerAngles.z.ToString();

        Vector2 v1 = guideUI.guideExplain.anchoredPosition - cacheRTPos[guideUI.guideExplain];
        Vector2 v2 = guideUI.guideGesture.anchoredPosition - cacheRTPos[guideUI.guideGesture];
        guideExplainRelativePos = string.Format("{0}#{1}", v1.x, v1.y);
        guideGestureRelativePos = string.Format("{0}#{1}", v2.x, v2.y);
    }
#endif

    /// <summary>
    /// 设置引导目标路径
    /// </summary>
    /// <param name="path">引导目标路径，例：CardView/Root/@_obj_content/btns/@_aorbtn_foster</param>
    /// <param name="childIndex">引导目标为Clone的情况下使用</param>
    /// <param name="isNeedDrawMask">是否需要绘制遮罩</param>
    public void SetGuideTargetPath(string path, int childIndex, int isNeedDrawMask)
    {
        RectTransform target;
        GameObject findGO = GetGameObject(path);
        if (findGO == null)
        {
            Debug.LogWarningFormat("找不到该路径映射的GameObject:{0}", path);
            return;
        }

        if (childIndex == 0)
            target = findGO.GetComponent<RectTransform>();
        else
        {
            if (findGO.GetChildCount() == 0)
                return;
            else
                target = findGO.GetChild(childIndex - 1).GetComponent<RectTransform>();
        }
        guideTarget = target;
        SetGuideAnchorPosition();
        if (isNeedDrawMask == 1)
        {
            RefreshView();
        }
    }

    /// <summary>
    /// 设置引导手势信息
    /// </summary>
    /// <param name="x1">引导手势相对位置x</param>
    /// <param name="y1">引导手势相对位置y</param>
    /// <param name="x2">引导说明相对位置x</param>
    /// <param name="y2">引导说明相对位置y</param>
    /// <param name="rotateY">引导手势Y轴旋转量</param>
    /// <param name="rotateZ">引导手势Z轴旋转量</param>
    public void SetGuideInfo(float x1, float y1, float x2, float y2, float rotateY, float rotateZ)
    {
        // 增量锚点位置
        Vector2 guideGestureAnchorPosition = guideUI.guideGesture.anchoredPosition;
        guideUI.guideGesture.SetAnchorPosition(guideGestureAnchorPosition.x + x1, guideGestureAnchorPosition.y + y1);
        Vector2 guideButtonAnchorPosition = guideUI.guideButton.anchoredPosition;
        guideUI.guideButton.SetAnchorPosition(guideButtonAnchorPosition.x + x1, guideButtonAnchorPosition.y + y1);
        Vector2 guideExplainAnchorPosition = guideUI.guideExplain.anchoredPosition;
        guideUI.guideExplain.SetAnchorPosition(guideExplainAnchorPosition.x + x2, guideExplainAnchorPosition.y + y2);
        guideUI.guideGesture.SetEulerAngles(0, rotateY, rotateZ);
        Vector2 guideGestureEffectAnchorPosition = guideUI.guideGestureEffect.anchoredPosition;
        guideUI.guideGestureEffect.SetAnchorPosition(guideGestureEffectAnchorPosition.x + x1, guideGestureEffectAnchorPosition.y + y1);
    }

    /// <summary>
    /// 目标是否创建出来了
    /// </summary>
    /// <param name="path">目标路径</param>
    /// <param name="childIndex">子物体Index</param>
    /// <returns>0,1/false,true</returns>
    public int IsTargetCreated(string path, int childIndex = 0)
    {
        GameObject findGO = GetGameObject(path);
        if (findGO)
        {
            if (childIndex == 0)
                return 1;
            else
            {
                if (findGO.GetChildCount() == 0)
                    return 0;
                else
                    return 1;
            }
        }
        else
            return 0;
    }

    /// <summary>
    /// 目标是否显示
    /// </summary>
    /// <param name="path">目标路径</param>
    /// <returns>0,1/false,true</returns>
    public int IsTargetActive(string path)
    {
        return GetGameObject(path).activeInHierarchy ? 1 : 0;
    }

    public bool IsGuideTargetNil()
    {
        return guideTarget == null;
    }

    private GameObject GetGameObject(string path)
    {
        return GameObject.Find(path);
    }

    private void SetGuideTargetSize(Vector3 targetMin, Vector3 targetMax)
    {
        if (targetMin == guideTargetMin && targetMax == guideTargetMax)
            return;
        guideTargetMin = targetMin;
        guideTargetMax = targetMax;
        SetAllDirty();
    }

    private void RefreshView()
    {
        SetGuideAnchorPosition();
        if (guideTarget)
        {
            // 计算得到内盒的顶点位置
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(selfCacheTrans, guideTarget);
            SetGuideTargetSize(bounds.min, bounds.max);
        }
        else
        {
            SetGuideTargetSize(Vector3.zero, Vector3.zero);
            SetAllDirty();
        }
    }

    private void SetGuideAnchorPosition()
    {
        if (guideTarget)
        {
            Vector2 anchorPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(useCanvas.transform as RectTransform, guideTarget.position, null, out anchorPosition);
            guideUI.guideExplain.SetAnchorPosition(anchorPosition.x, anchorPosition.y);
            guideUI.guideGesture.SetAnchorPosition(anchorPosition.x, anchorPosition.y);
            guideUI.guideGestureEffect.SetAnchorPosition(anchorPosition.x, anchorPosition.y);
            guideUI.guideGestureEffect.SetSizeDelta(guideTarget.rect.width + 15, guideTarget.rect.height + 16);
            guideUI.guideButton.SetAnchorPosition(anchorPosition.x, anchorPosition.y);
            guideUI.guideButton.SetSizeDelta(guideTarget.rect.width, guideTarget.rect.height);
#if UNITY_EDITOR
            cacheRTPos = new Dictionary<RectTransform, Vector2>();
            cacheRTPos.Add(guideUI.guideExplain, guideUI.guideExplain.anchoredPosition);
            cacheRTPos.Add(guideUI.guideGesture, guideUI.guideGesture.anchoredPosition);
#endif
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        // TODO 目前只支持了绘制矩形包围盒，如果内部包围盒是个圆形，得绘制大量三角形避免抗锯齿，但是绘制的三角形多了叠加RGBA后色值会不对，得采取其他方案
        if (guideTargetMin == Vector3.zero && guideTargetMax == Vector3.zero)
        {
            base.OnPopulateMesh(vh);
            return;
        }

        vh.Clear();

        // 填充顶点
        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;

        // 计算得到外盒的4个顶点
        Vector2 selfPiovt = rectTransform.pivot;// 中心点位：0.5, 0.5
        Rect selfRect = rectTransform.rect;// 矩形的宽高：4000, 4000
        float maxX = (1 - selfPiovt.x) * selfRect.width;// 2000
        float minX = -selfPiovt.x * selfRect.width;// -2000
        float maxY = (1 - selfPiovt.y) * selfRect.height;// 2000
        float minY = -selfPiovt.y * selfRect.height;// -2000

        // 0 - 外盒:upper left corner
        vert.position = new Vector3(minX, maxY);
        vh.AddVert(vert);
        // 1 - 外盒:upper right corner
        vert.position = new Vector3(maxX, maxY);
        vh.AddVert(vert);
        // 2 - 外盒:lower right corner
        vert.position = new Vector3(maxX, minY);
        vh.AddVert(vert);
        // 3 - 外盒:lower left corner
        vert.position = new Vector3(minX, minY);
        vh.AddVert(vert);

        // 4 - 内盒:upper left corner
        vert.position = new Vector3(guideTargetMin.x, guideTargetMax.y);
        vh.AddVert(vert);
        // 5 - 内盒:upper right corner
        vert.position = new Vector3(guideTargetMax.x, guideTargetMax.y);
        vh.AddVert(vert);
        // 6 - 内盒:lower right corner
        vert.position = new Vector3(guideTargetMax.x, guideTargetMin.y);
        vh.AddVert(vert);
        // 7 - 内盒:lower left corner
        vert.position = new Vector3(guideTargetMin.x, guideTargetMin.y);
        vh.AddVert(vert);

        // 连接各个顶点绘制三角形
        vh.AddTriangle(4, 0, 1);
        vh.AddTriangle(4, 1, 5);
        vh.AddTriangle(5, 1, 2);
        vh.AddTriangle(5, 2, 6);
        vh.AddTriangle(6, 2, 3);
        vh.AddTriangle(6, 3, 7);
        vh.AddTriangle(7, 3, 0);
        vh.AddTriangle(7, 0, 4);
    }
}
