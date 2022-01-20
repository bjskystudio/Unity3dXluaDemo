/*
 * Description:             GameHelperEditorWindowGenConfig.cs
 * Author:                  TANGHUAN
 * Create Date:             2020/07/25
 */

using System;
using UnityEngine;
using XLua;

// 暂时没找到排除Editor目录下CSharpCallLua类的方法
// 用于定义GameHelperEditorWindow里需要访问Lua的部分

[CSharpCallLua]
public delegate int PosToUVDelegate(Vector3 a, out int b);

#region GM 映射
/// <summary>
/// 映射Lua层的GM
/// </summary>
[CSharpCallLua]
public interface GM
{
    #region 常规GM    
    /// <summary>
    /// 打印所有已加载脚本
    /// </summary>
    void PrintAllLoadedScrits();
    #endregion

    #region 运行时功能
    #endregion
    
    #region Log部分
    /// <summary>
    /// 切换指定模块Log开关状态
    /// </summary>
    /// <param name="modulename"></param>
    void ChangeSwitchModuleLog(string modulename);
    #endregion

    #region 行为树部分
    /// <summary>
    /// 添加指定AI给指定世界对象
    /// </summary>
    /// <param name="btresourcename"></param>
    /// <param name="uid"></param>
    void AddAIToWorldObject(long uid, string btresourcename);

    /// <summary>
    /// 移除指定世界对象AI
    /// </summary>
    /// <param name="uid"></param>
    void RemoveWorldObjectAI(long uid);

    /// <summary>
    /// 暂停指定世界对象AI
    /// </summary>
    /// <param name="uid"></param>
    void PauseWorldObjectAI(long uid);

    /// <summary>
    /// 继续指定世界对象AI
    /// </summary>
    /// <param name="uid"></param>
    void ResumeWorldObjectAI(long uid);

    /// <summary>
    /// 暂停所有AI
    /// </summary>
    void PauseAll();

    /// <summary>
    /// 继续所有AI
    /// </summary>
    void ResumeAll();

    /// <summary>
    /// 中断并暂停所有AI
    /// </summary>
    void AbortAndPauseAll();

    /// <summary>
    /// 打印当前绑定的行为树节点信息
    /// </summary>
    void PrintAllBindLuaBTNodeInfo();
    #endregion
}
#endregion