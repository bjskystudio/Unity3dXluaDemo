using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// IO存储封装类
/// </summary>
public class AorIO
{
    public const string AssetsFolderName = "Assets";
    public static Encoding encoding = new UTF8Encoding(false);


    public static string FormatToUnityPath(string path)
    {
        return path.Replace("\\", "/");
    }

    #region 读写

    public static void WriteAllText(string path, string content)
    {
        CheckFileAndCreateDirWhenNeeded(path, true);
        File.WriteAllText(path, content, encoding);
    }

    public static void WriteAllBytes(string path, byte[] content)
    {
        CheckFileAndCreateDirWhenNeeded(path, true);
        File.WriteAllBytes(path, content);
    }

    public static string ReadAllText(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        if (!File.Exists(path))
            return null;
        //File.SetAttributes(path, FileAttributes.Normal);
        return File.ReadAllText(path, Encoding.UTF8);
    }

    public static byte[] ReadAllBytes(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        if (!File.Exists(path))
            return null;
        //File.SetAttributes(path, FileAttributes.Normal);
        return File.ReadAllBytes(path);
    }

    public static byte[] ReadBytesFormFile(string path)
    {
        if (File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            long size = fs.Length;
            byte[] data = new byte[size];
            fs.Read(data, 0, data.Length);
            fs.Close();
            return data;
        }
        else
        {
            Debug.LogError("AorIO.ReadStringFormFile Error :: 读取数据失败, 没有找到该文件 : " + path);
            return null;
        }
    }

    public static string ReadStringFormFile(string path)
    {
        if (File.Exists(path))
        {
            return File.ReadAllText(path, encoding);
        }
        else
        {
            //            Debug.LogError("AorIO.ReadStringFormFile Error :: 读取数据失败, 没有找到该文件 : " + path);
            return null;
        }
    }

    public static bool SaveBytesToFile(string path, byte[] data)
    {
        if (data != null && data.Length > 0)
        {
            CheckFileAndCreateDirWhenNeeded(path, true);

            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(data, 0, data.Length);
            fs.Close();

            return true;
        }
        else
        {
            Debug.LogError("AorIO.SaveBytesToFile Error :: 保存数据失败,数据不能为空!");
            return false;
        }
    }

    public static bool SaveStringToFile(string path, string fileStr)
    {
        if (fileStr != null && fileStr.Trim() != "")
        {
            CheckFileAndCreateDirWhenNeeded(path, true);

            StreamWriter FileWriter = new StreamWriter(path, false, encoding);
            FileWriter.Write(fileStr);
            FileWriter.Flush();

            FileWriter.Close();
            FileWriter.Dispose();

            return true;
        }
        else
        {
            Debug.LogError("AorIO.SaveStringToFile Error :: 保存数据失败,数据不能为空!");
            return false;
        }
    }

    #endregion

    #region 创建
    /// <summary>
    /// 检查文件路径，没有则创建路径上的文件夹
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="needDelFile">是否删除已存在文件</param>
    public static void CheckFileAndCreateDirWhenNeeded(string filePath, bool needDelFile = false)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }
        if (needDelFile)
        {
            DelFile(filePath);
        }
        FileInfo file_info = new FileInfo(filePath);
        DirectoryInfo dir_info = file_info.Directory;
        if (!dir_info.Exists)
        {
            Directory.CreateDirectory(dir_info.FullName);
        }
    }
    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="path"></param>
    public static void CreateDir(string path)
    {
        if (string.IsNullOrEmpty(path))
            return;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除指定文件夹
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="all">是否删除目标文件夹中的子文件夹及子文件</param>
    /// <returns></returns>
    public static bool DelDirectory(string path, bool all = true)
    {
        if (!Directory.Exists(path))
            return false;
        Directory.Delete(path, all);
        return true;
    }
    /// <summary>
    /// 清理指定文件夹里面所有东西，如果文件夹不存在，则创建
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="excludeFolds">排除文件夹名字集合</param>
    /// <param name="excludeFiles">排除文件名字集合</param>
    public static void ClearDir(string path, List<string> excludeFolds = null, List<string> excludeFiles = null)
    {
        DirectoryInfo di = new DirectoryInfo(path);
        if (di.Exists)
        {
            foreach (FileInfo file in di.GetFiles())
            {
                if (excludeFiles != null && excludeFiles.Contains(file.Name))
                {
                    continue;
                }
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                if (excludeFolds != null && excludeFolds.Contains(dir.Name))
                {
                    continue;
                }
                dir.Delete(true);
            }
        }
        else
        {
            di.Create();
        }
    }
    /// <summary>
    /// 删除指定文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static bool DelFile(string path)
    {
        if (!File.Exists(path))
            return false;
        File.Delete(path);
        return true;
    }
    /// <summary>
    /// 删除创建日期最久的文件
    /// </summary>
    /// <param name="path">路径</param>
    public static void DelOldestFile(string path)
    {
        if (!File.Exists(path))
            return;
        FileInfo delTarget = null;
        foreach (var item in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly))
        {
            FileInfo dirInfo = new FileInfo(item);
            if (delTarget == null || dirInfo.CreationTime < delTarget.CreationTime)
            {
                delTarget = dirInfo;
            }
        }
        if (delTarget != null)
        {
            delTarget.Delete();
        }
    }
    #endregion

    #region 拷贝
    /// <summary>
    /// 拷贝目录
    /// </summary>
    /// <param name="srcdir">原目录</param>
    /// <param name="desdir">目标目录</param>
    public static void CopyDirectory(string srcdir, string desdir)
    {
        if (string.IsNullOrEmpty(srcdir) || string.IsNullOrEmpty(desdir))
        {
            return;
        }

        if (!Directory.Exists(desdir))
        {
            Directory.CreateDirectory(desdir);
        }

        string[] filePaths = Directory.GetFileSystemEntries(srcdir);

        foreach (string filePath in filePaths)
        {

            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            string srcPath = FormatToUnityPath(filePath);
            string subDesPath = FormatToUnityPath(desdir + "\\" + fileName);

            if (Directory.Exists(filePath))
            {
                CopyDirectory(filePath, subDesPath);
            }
            else
            {
                Debug.Log("DU> copy " + srcPath + " -> " + subDesPath);
                if (File.Exists(subDesPath))
                    File.Delete(subDesPath);
                File.Copy(srcPath, subDesPath, true);
            }
        }

    }
    /// <summary>
    /// 拷贝目录
    /// </summary>
    /// <param name="srcdir">原目录</param>
    /// <param name="desdir">目标目录</param>
    /// <param name="excludeList">例外</param>
    /// <param name="isIgnoreSame">跳过相同文件</param>
    public static void CopyDirectory(string srcdir, string desdir, List<string> excludeList, bool isIgnoreSame = false)
    {

        if (string.IsNullOrEmpty(srcdir) || string.IsNullOrEmpty(desdir))
        {
            return;
        }

        if (!Directory.Exists(desdir))
        {
            Directory.CreateDirectory(desdir);
        }

        string[] filePaths = Directory.GetFileSystemEntries(srcdir);

        foreach (string filePath in filePaths)
        {

            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            string srcPath = FormatToUnityPath(filePath);
            string subDesPath = FormatToUnityPath(desdir + "\\" + fileName);

            if (Directory.Exists(filePath))
            {
                CopyDirectory(filePath, subDesPath, excludeList);
            }
            else
            {
                if (!excludeList.Contains(srcPath))
                {
                    if (isIgnoreSame)
                    {
                        if (!File.Exists(subDesPath) || (File.GetLastWriteTime(srcPath) != File.GetLastWriteTime(subDesPath)))
                        {
                            Debug.Log("DU> copy " + srcPath + " -> " + subDesPath);
                            if (File.Exists(subDesPath))
                                File.Delete(subDesPath);
                            File.Copy(srcPath, subDesPath, true);
                        }
                        else
                        {
                            Debug.Log(string.Format("跳过相同文件:{0}", fileName));
                        }
                    }
                    else
                    {
                        Debug.Log("DU> copy " + srcPath + " -> " + subDesPath);
                        if (File.Exists(subDesPath))
                            File.Delete(subDesPath);
                        File.Copy(srcPath, subDesPath, true);
                    }
                }
                else
                {
                    Debug.Log("DU> exclude " + subDesPath);
                }
            }
        }
    }

    public static void CopyToFile(string srcdir, string desdir)
    {
        if (string.IsNullOrEmpty(srcdir) || string.IsNullOrEmpty(desdir))
        {
            return;
        }


        desdir = FormatToUnityPath(desdir);
        string[] _array = desdir.Split('/');

        string _folderPath = string.Empty;
        int _length = _array.Length - 1;
        for (int i = 0; i < _length; ++i)
        {
            _folderPath += _array[i];
            if (i != _length - 1)
            {
                _folderPath += "/";
            }
        }

        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }
        File.Copy(srcdir, desdir, true);
    }
    #endregion


  

}
