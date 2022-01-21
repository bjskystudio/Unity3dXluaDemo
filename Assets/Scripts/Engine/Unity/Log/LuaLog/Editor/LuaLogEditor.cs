/*
 * Description:             LuaLogEditor.cs
 * Author:                     zy
 * Create Date:             2021/4/25
 */

public class LuaLogEditor
{
    private static LuaLogEditorHandle handle;
    private static LuaLogEditorHandle Handle
    {
        get
        {
            if (handle == null)
                handle = new LuaLogEditorHandle();
            return handle;
        }
    }

    [UnityEditor.Callbacks.OnOpenAssetAttribute(-1)]
    private static bool OnOpenAsset(int instanceID, int line)
    {
        if (Handle.GetInstanceID() == instanceID)
        {
            string[] contentStrings = Handle.GetConsoleStrings();

            for (int index = 0; index < contentStrings.Length; index++)
            {
                if (Handle.CheckOpen(contentStrings[index], line))
                    return true;
            }
        }
        return false;
    }
}