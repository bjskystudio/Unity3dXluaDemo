using Framework.Graphic;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Graphic
{
    /// <summary>
    /// 图形绘制阻挠器,场景中只要有任何一个阻挠器,图形管理器就不再更新游戏画面(排除UI)
    /// </summary>
    [DisallowMultipleComponent]
    public class GraphicDrawObstruction : MonoBehaviour
    {
        private static Dictionary<int, GraphicDrawObstruction> ObstructionDic = new Dictionary<int, GraphicDrawObstruction>();
        private int HashCode;

        private void Check()
        {
            bool isStopRender = ObstructionDic.Count > 0 ? true : false;
            //TODO 新的实现
            //if (GraphicsManager.Instance)
            //    GraphicsManager.Instance.StopRender = isStopRender;
            //YKApplication.Instance.GlobalEvent.DispatchEvent(FrameDef.OnGraphicDrawObstructionChange, isStopRender);
        }

        private void Awake()
        {
            HashCode = gameObject.GetHashCode();
        }

        private void OnDisable()
        {
            ObstructionDic.Remove(HashCode);
            Check();
        }

        private void OnEnable()
        {
            ObstructionDic.Add(HashCode, this);
            Check();
        }
    }
}