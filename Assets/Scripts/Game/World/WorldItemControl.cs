using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate void WorldItemControlCallBack(int goID, bool isEnter, int tarInstID, GameObject tarObj);

[LuaCallCSharp]
public class WorldItemControl : MonoBehaviour
{
	/// <summary>
	/// 为了减少lua 创建委托的开销，增加的全局委托方式
	/// </summary>
	public static WorldItemControlCallBack ConstLuaCallBack;
	/// <summary>
	/// 实例化的ID
	/// </summary>
	public int InstID;

	public void Awake()
	{
		InstID = gameObject.GetInstanceID();
	}

	public virtual void OnTriggerEnter(Collider other)
	{
        if (ConstLuaCallBack == null)
        {
            return;
        }

        var control = other.gameObject.GetComponent<WorldItemControl>();
        if (control != null)
        {
            var tarGOID = control.InstID;
		    ConstLuaCallBack(InstID, true, tarGOID, other.gameObject);
        }
    }

    public void Test(LuaFunction lua)
    {
        lua.Call();
    }
	public virtual void OnTriggerExit(Collider other)
	{
        if (ConstLuaCallBack == null)
        {
            return;
        }
        var control = other.gameObject.GetComponent<WorldItemControl>();
        if (control != null)
        {
            var tarGOID = control.InstID;
            ConstLuaCallBack(InstID, false, tarGOID, other.gameObject);
        }
    }
}