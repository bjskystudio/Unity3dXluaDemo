using UnityEngine;
public class HUDItemController
{
    private RectTransform ownrecttrans;
    private Transform targettrans;
    private Vector3 uioffset = Vector2.zero;
    private RectTransform parentrect;
    public  int uid;
    private Camera mCamera;
    private Transform mCamPos;
    private bool IsSet = false;
    /// <summary>
    /// 摄像机距离
    /// </summary>
    private const int ValideCameraDistance = 30;

    public HUDItemController(int _uid,RectTransform rect,Transform target,float offsetx=0,float offsety=0)
    {
        mCamera = Camera.main;
        mCamPos = mCamera.transform;
        uid =_uid;
        ownrecttrans=rect;
        targettrans=target;
        uioffset=new Vector2(offsetx,offsety);
        parentrect= rect.parent.GetComponent<RectTransform>();
        ownrecttrans.transform.position = targettrans.transform.position + uioffset;
    }
    public void Update()
    {
        if (ownrecttrans!=null && targettrans!=null)
        {
            ownrecttrans.transform.position = Vector3.Lerp(ownrecttrans.transform.position, targettrans.transform.position + uioffset, Time.deltaTime * 30);
        }
    }


    //剧情timeline专用
    public void DestroyFoStory()
    {
        if (ownrecttrans != null)
        {
            if (Application.isPlaying)
                GameObject.Destroy(ownrecttrans.gameObject);
            else
                GameObject.DestroyImmediate(ownrecttrans.gameObject);
        }
    }
}