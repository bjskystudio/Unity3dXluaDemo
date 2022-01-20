/*
 * Description:             GameHelperUtilities.cs
 * Author:                  TANGHUAN
 * Create Date:             2019/08/29
 */

using System.IO;
using System.Text;
using UnityEditor.SceneManagement;
using UnityEngine;
using YoukiaCore.Log;

namespace Game
{
    /// <summary>
    /// 游戏辅助静态工具类
    /// </summary>
    public static class GameHelperUtilities
    {
        /// <summary>
        /// 检查指定场景是否打开
        /// </summary>
        /// <param name="scenepath">场景路径</param>
        /// <returns></returns>
        public static bool IsSceneOpened(string scenepath)
        {
            var activescene = EditorSceneManager.GetActiveScene();
            var scenename = Path.GetFileNameWithoutExtension(scenepath);
            return activescene.name.Equals(scenename);
        }

        /// <summary>
        /// 打开指定场景
        /// </summary>
        /// <returns></returns>
        public static void OpenScene(string scenepath)
        {
            if (!IsSceneOpened(scenepath))
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                    UnityEditor.EditorApplication.delayCall = delegate ()
                    {
                        EditorSceneManager.OpenScene(scenepath);
                    };
                    return;
                }
                else
                    EditorSceneManager.OpenScene(scenepath);
            }
        }
    }
}
