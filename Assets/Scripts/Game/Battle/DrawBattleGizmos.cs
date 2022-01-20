
using System;
using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate byte[] EncodeLogBattleCallback(string path);

public class DrawBattleGizmos : MonoBehaviour
{
    public static Func<int, LuaTable> LuaCallBattleState;
    public static Func<string, LuaTable> LuaCallFightState;
    public static Action<byte[]> LuaCallStartLogBattle;
    public static Action<byte[], string, string> LuaCallSaveLogBattle;
    public static EncodeLogBattleCallback LuaCallEncodeLogBattle;
#if UNITY_EDITOR
    public static bool IsDrawGizmos = false;
    public static Vector3 AABBMin = Vector3.zero;
    public static Vector3 AABBMax = Vector3.zero;
    void OnDrawGizmos()
    {
        if (IsDrawGizmos)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Vector3 mid = new Vector3(AABBMin.x + AABBMax.x, AABBMin.y + AABBMax.y, AABBMin.z + AABBMax.z) * 0.5f;
            Gizmos.DrawCube(mid, new Vector3(AABBMax.x - AABBMin.x, AABBMax.y - AABBMin.y, AABBMax.z - AABBMin.z));
        }
    }
#endif
}
