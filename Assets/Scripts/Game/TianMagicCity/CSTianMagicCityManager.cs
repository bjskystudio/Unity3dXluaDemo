using System;
using Framework;
using UnityEngine;
using YoukiaCore.Utils;
using System.Collections;
using System.Collections.Generic;

public class CSTianMagicCityManager : MonoSingleton<CSTianMagicCityManager>
{
    public GameObject TianMagicCityRoot;
    private Transform GameRootTrans;
    private const string qipan1 = "Scene/Qipan/prefabs/Qipan_01";
    private const string qipan2 = "Scene/Qipan/prefabs/Qipan_02";
    private int m_width;
    private int m_height;
    private CallBack LoadFinishCB;
    private float[] degs = { 0f, 180f, 270f, 90f };
    private TianMagicCityPlayer player;
    private TianMagicCityGrid[] gridArr;
    private int m_tempLoadedFinishGridNum;
    public int TempLoadedFinishGridNum
    {
        get { return m_tempLoadedFinishGridNum; }
        set
        {
            m_tempLoadedFinishGridNum = value;
            if (m_tempLoadedFinishGridNum == m_width * m_height)
            {
                m_tempLoadedFinishGridNum = 0;
                willLoadModelPaths.Clear();
                willLoadModelScale.Clear();
                LoadFinishCB?.Invoke();
                LoadFinishCB = null;
            }
        }
    }
    public bool LockClick { get; set; }
    private Dictionary<int, string> willLoadModelPaths = new Dictionary<int, string>();
    private Dictionary<int, float> willLoadModelScale = new Dictionary<int, float>();
    private bool m_lockQiPanRotate = false;

    protected override void Init()
    {
        base.Init();
    }

    public override void Dispose()
    {
        DestroyScene();
    }

    /// <summary>
    /// 设置地图宽高
    /// </summary>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    public void SetMapInfo(int width, int height)
    {
        this.m_width = width;
        this.m_height = height;
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="callBack">lua端加载完成后的回调</param>
    public void LoadScene(CallBack callBack)
    {
        this.LoadFinishCB = callBack;
        AssetLoadManager.Instance.LoadPrefabInstance("UI/TianMagicCity/3DRoot/TianMagicCityRoot", (_rootobj) =>
        {
            TianMagicCityRoot = _rootobj;
            GameRootTrans = TianMagicCityRoot.transform.Find("GameRoot");
            Transform sceneRoot = TianMagicCityRoot.transform.Find("SceneRoot");
            Transform playerRoot = TianMagicCityRoot.transform.Find("GameRoot/PlayerRoot");
            player = playerRoot.gameObject.AddComponent<TianMagicCityPlayer>();
            AssetLoadManager.Instance.LoadPrefabInstance("Scene/Qipan/prefabs/Qipan", (_sceneobj) =>
            {
                _sceneobj.transform.SetParent(sceneRoot);
                _sceneobj.transform.localPosition = Vector3.zero;
            });
            InstQiPan();
            AssetLoadManager.Instance.LoadPrefabInstance("Actor/Role/caotijing_0/prefab/caotijing_0", (_roleobj) =>
            {
                _roleobj.transform.SetParent(playerRoot);
                _roleobj.transform.localScale = Vector3.one;
                _roleobj.transform.localPosition = Vector3.zero;
            });
        });
    }

    /// <summary>
    /// 销毁场景
    /// </summary>
    public void DestroyScene()
    {
        gridArr = null;
        LoadFinishCB = null;
        GameObject.Destroy(TianMagicCityRoot);
        GC.Collect();
    }

    /// <summary>
    /// 实例化棋盘
    /// </summary>
    void InstQiPan()
    {
        Transform gameRoot = TianMagicCityRoot.transform.Find("GameRoot/GridRoot");
        gridArr = new TianMagicCityGrid[m_width * m_height];
        int instGridNum = 0;
        float fx = 0f;
        float fz = 0f;
        //棋盘宽小于高？
        bool isHBW = m_height > m_width;
        bool exchange = false;
        //从第三象限开始创建
        for (int z = -m_height; z < 0; z++)
        {
            fx = 0f;
            fz += 0.5f;
            exchange = z % 2 != 0;
            for (int x = -m_width; x < 0; x++)
            {
                fx += 0.5f;
                instGridNum++;
                bool isOddNum = instGridNum % 2 != 0;
                string qipanPath = isOddNum ? exchange ? isHBW ? qipan2 : qipan1 : qipan2 : exchange ? isHBW ? qipan1 : qipan2 : qipan1;
                GameObject gridGo = new GameObject(string.Format("Grid{0}", instGridNum));
                gridGo.SetParent(gameRoot);
                gridGo.SetLocalPosition(x / 2.0f + fx, 0, z / 2.0f + fz);
                GameObject qipanModelRoot = new GameObject("qipanModelRoot");
                qipanModelRoot.SetParent(gridGo);
                qipanModelRoot.transform.localPosition = Vector3.zero;
                GameObject qiziModelRoot = new GameObject("qiziModelRoot");
                qiziModelRoot.SetParent(gridGo);
                qiziModelRoot.transform.SetLocalPosition(0, 0.1f, 0);
                BoxCollider boxCollider = gridGo.AddComponent<BoxCollider>();
                boxCollider.center = new Vector3(0f, 0.05f, 0f);
                boxCollider.size = new Vector3(1f, 0.11f, 1f);
                TianMagicCityGrid grid = gridGo.AddComponent<TianMagicCityGrid>();

                float qiziModelScale;
                willLoadModelScale.TryGetValue(instGridNum, out qiziModelScale);

                string qiziPath;
                if (willLoadModelPaths.TryGetValue(instGridNum, out qiziPath))
                    grid.LoadFBX(qipanPath, qiziPath, qiziModelScale);
                else
                    grid.LoadFBX(qipanPath);

                gridArr[instGridNum - 1] = grid;
            }
        }
    }

    /// <summary>
    /// 隐藏场景
    /// </summary>
    /// <param name="active">是否隐藏</param>
    public void ActiveScene(bool active)
    {
        TianMagicCityRoot.SetActive(active);
    }

    /// <summary>
    /// 旋转玩家
    /// </summary>
    /// <param name="direction">朝向</param>
    public void RotatePlayer(int direction)
    {
        player.RotateFBX(degs[direction]);
    }

    /// <summary>
    /// 添加格子上的棋子模型
    /// </summary>
    /// <param name="index">第几个格子</param>
    /// <param name="modelPath">模型路径</param>
    /// <param name="scale">模型尺寸</param>
    public void AddGridModelPaths(int index, string modelPath, float scale)
    {
        willLoadModelPaths.Add(index, modelPath);
        willLoadModelScale.Add(index, scale);
    }

    /// <summary>
    /// 旋转格子中的模型
    /// </summary>
    /// <param name="index">第几个格子</param>
    /// <param name="direction">朝向</param>
    public void RotateGrid(int index, int direction)
    {
        gridArr[index].RotateFBX(degs[direction]);
    }

    /// <summary>
    /// 替换格子中的模型
    /// </summary>
    /// <param name="index">第几个格子</param>
    /// <param name="fbxPath">模型资源路径</param>
    /// <param name="fbxScale">模型尺寸</param>
    public void ReplaceGridFBX(int index, string fbxPath, float fbxScale)
    {
        gridArr[index].ReplaceFBX(fbxPath, fbxScale);
    }

    /// <summary>
    /// 销毁格子中的模型
    /// </summary>
    /// <param name="index">第几个格子</param>
    public void DestroyGridFBX(int index)
    {
        gridArr[index].DestroyFBX();
    }

    /// <summary>
    ///  设置玩家的位置
    /// </summary>
    /// <param name="index">设置到第几个格子的位置</param>
    public void SetPlayerPos(int index)
    {
        Vector3 pos = gridArr[index].transform.position;
        player.SetPosition(pos.x, pos.y + 0.1f, pos.z);
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    /// <param name="index">移动到第几个格子的位置</param>
    /// <param name="direction">朝向</param>
    public void PlayerMove(int index, int direction)
    {
        RotatePlayer(direction);
        player.TargetTrans = gridArr[index].transform;
    }

    public void EnemyMove(int index, int direction)
    {
        TianMagicCityGrid enemy = gridArr[index];
        enemy.TargetTrans = gridArr[index].transform;
    }

    /// <summary>
    /// 旋转棋盘
    /// </summary>
    public void RotateQiPan()
    {
        if (!m_lockQiPanRotate)
        {
            m_lockQiPanRotate = true;
            GameRootTrans.DORotate(0, GameRootTrans.localEulerAngles.y + 90, 0, 1, () =>
            {
                m_lockQiPanRotate = false;
            });
        }
    }
}