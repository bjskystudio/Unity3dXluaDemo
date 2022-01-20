/*
 * Description:             BTNodeData.cs
 * Author:                  TONYTANG
 * Create Date:             2020/08/16
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuaBehaviourTree
{
    /// <summary>
    /// BTNodeData.cs
    /// 行为树常量数据
    /// </summary>
    public static class BTNodeData
    {
        /// Note:
        /// 条件节点,行为节点以及相关参数提示的定义写在这里，
        /// 然后去 BehaviourTreeDefine.lua去添加同步定义并编写相关逻辑脚本

        #region CS部分
        /// <summary>
        /// 组合节点名数据
        /// </summary>
        public static string[] BTCompositeNodeNameArray = BTData.BTCompositeNodeNameArray;

        /// <summary>
        /// 组合节点描述数据(和BTCompositeNodeNameArray一一对应)
        /// </summary>
        public static string[] BTCompositeNodeIntroArray =
        {
            "顺序执行，第一个成功的节点就算成功，后面不再执行",     // 顺序选择
            "顺序执行，有一个失败就算失败，后面不再执行",           // 顺序执行
            "并发，所有节点全部成功才算成功，任何一个失败都算失败", // 并发节点(所有成功算成功)
            "并发，任何一个成功就算成功，其它行为同时停止",         // 并发节点(一个成功算成功)
            "随机选择一个子节点来执行",                             // 随机节点
        };
        
        /// <summary>
        /// 修饰节点名数据
        /// </summary>
        public static string[] BTDecorationNodeNameArray = BTData.BTDecorationNodeNameArray;

        /// <summary>
        /// 修饰节点描述数据(和BTDecorationNodeNameArray一一对应)
        /// </summary>
        public static string[] BTDecorationNodeIntroArray =
        {
            "翻转结果修饰节点",                                  // 翻转结果修饰节点
            "重复执行修饰节点",                                  // 重复执行修饰节点
        };

        /// <summary>
        /// 修饰节点描述数据(和BTDecorationNodeNameArray一一对应)
        /// </summary>
        public static string[] BTDecorationNodeParamsIntroArray =
        {
            "无",                                                // 翻转修饰节点
            "重复次数",                                          // 重复次数
        };

        /// <summary>
        /// CS行为节点名数据
        /// </summary>
        public static string[] BTCSActionNodeNameArray = BTData.BTActionNodeNameArray;

        /// <summary>
        /// CS行为节点描述数据(和BTCSActionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTCSActionNodeIntroArray =
        {
            "设置自定义Bool变量值",                             // 设置自定义Bool变量值节点描述数据
            "设置自定义Int变量值",                              // 设置自定义Int变量值节点描述数据
            "设置自定义Float变量值",                            // 设置自定义Float变量值节点描述数据
            "设置自定义String变量值",                           // 设置自定义String变量值节点描述数据
        };

        /// <summary>
        /// CS行为节点参数介绍(和BTCSActionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTCSActionNodeParamsIntroArray =
        {
            "无",                                                // 设置自定义Bool变量值节点参数介绍
            "无",                                                // 设置自定义Int变量值节点参数介绍
            "无",                                                // 设置自定义Float变量值节参数点介绍
            "无",                                                // 设置自定义String变量值节点参数介绍
        };

        /// <summary>
        /// CS条件节点名数据
        /// </summary>
        public static string[] BTCSConditionNodeNameArray = BTData.BTConditionNodeNameArray;

        /// <summary>
        /// CS条件节点描述数据(和BTConditionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTCSConditionNodeIntroArray =
        {
            "比较自定义Bool变量值",                                 // 比较自定义Bool变量值
            "比较自定义Int变量值",                                  // 比较自定义Int变量值
            "比较自定义Float变量值",                                // 比较自定义Float变量值
            "比较自定义String变量值",                               // 比较自定义String变量值
        };

        /// <summary>
        /// CS条件节点参数介绍(和BTCSConditionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTCSConditionNodeParamsIntroArray =
        {
            "无",                                                    // 比较自定义Bool变量值参数点介绍
            "无",                                                    // 比较自定义Int变量值参数点介绍
            "无",                                                    // 比较自定义Float变量值参数点介绍
            "无",                                                    // 比较自定义String变量值参数点介绍
        };
        #endregion

        #region Lua部分

        #region 行为节点相关：BehaviourTreeDefine.lua
        /// <summary>
        /// 行为节点名数据
        /// </summary>
        public static string[] BTLuaActionNodeNameArray = {
            "LogActionAct",                                        // 打印Log
            "MoveToPostionAct",                                    // 移动到指定位置
            "PlayAnimationAct",                                    // 播放指定动画
            "ActiveSelfAct",                                       // 激活自身
            "HideSelfAct",                                         // 隐藏自身
            "LookAtTargetAct",                                     // 看向指定目标对象
            "LookAtDirectionAct",                                  // 看向指定方向
            "PlayBubbleAct",                                       // 播放指定气泡
            "PlayAudioAct",                                        // 播放指定音频
            "FollowTargetAct",                                     // 跟随指定玩家
            //"FollowRandomPathAct",                                 // 选择随机路线移动
            //"FollowPathAct",                                       // 沿固定路线移动
            "WaitForSecondsAct",                                   // 等待执行一段时间
            "RunAwayAct",                                          // 逃离
            "FadeSelfAct",                                         // 淡入或淡出自身
            "BackToBornStateAct",                                  // 回归出生状态(位置和旋转)
            "MoveAreaAct",                                         // 某区域自由行走
            "RoundMoveAct",                                        // 动态圆形半径随机移动(中心点动态确认)
        };

        /// <summary>
        /// 行为节点描述数据(和BTLuaActionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTLuaActionNodeIntroArray =
        {
            "打印log",                                              // 打印Log
            "移动到指定位置",                                       // 移动到指定位置
            "播放指定动画",                                         // 播放指定动画
            "激活自身",                                             // 激活自身
            "隐藏自身",                                             // 隐藏自身
            "朝向指定对象",                                         // 看向指定目标对象
            "朝向指定方位",                                         // 看向指定方向
            "播放气泡对话",                                         // 播放指定气泡
            "播放音频文件",                                         // 播放指定音频
            "跟随玩家",                                             // 跟随指定玩家
            //"选择随机路线巡逻",                                     // 选择随机路线移动
            //"沿指定路线巡逻",                                       // 沿固定路线移动
            "等待执行一段时间",                                     // 等待执行一段时间
            "排斥",                                                 // 排斥
            "指定时长内淡入或淡出自身",                             // 指定时长内淡入或淡出自身
            "回归出生状态(位置和旋转)",                             // 回归出生状态(位置和旋转)
            "某区域自由行走(区域中心点和半径)",                     // 某区域自由行走
            "动态圆形半径随机移动(中心点动态确认)",                 // 动态圆形半径随机移动(中心点动态确认)
        };

        /// <summary>
        /// 行为节点描述数据(和BTLuaActionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTLuaActionNodeParamsIntroArray =
        {
            "Log内容",                                                         // 打印Log
            "坐标X,坐标Y,坐标Z,移动动作名,移动速度",                           // 移动到指定位置
            "动画名",                                                          // 播放指定动画
            "无",                                                              // 激活自身
            "无",                                                              // 隐藏自身
            "目标朝向对象UID(0表示主玩家)",                                    // 看向指定目标对象
            "目标朝向X,Y,Z",                                                   // 看向指定方向
            "气泡对话ID",                                                      // 播放指定气泡
            "音频ID",                                                          // 播放指定音频
            "跟随玩家UID(0表示主玩家),跟随玩家动作名,跟随速度(0用跟随目标的速度),跟随停止距离",   // 跟随指定玩家
            //"移动动作名,移动速度",                                             // 选择随机路线移动
            //"移动动作名,移动速度",                                             // 沿固定路线移动
            "等待时长(s)",                                                     // 等待执行一段时间
            "警戒距离(只支持逃离主玩家),逃跑速度",                             // 逃离
            "淡入淡出类型(1淡入2淡出),时常",                                   // 淡入淡出类型(1淡入2淡出),时常
            "无",                                                              // 回归出生状态(位置和旋转)
            "坐标X,坐标Y,坐标Z,半径",                                          // 某区域自由行走
            "圆形半径,移动动作,移动速度",                                      // 动态圆形半径随机移动(中心点动态确认)
        };
        #endregion

        #region  条件节点相关：BehaviourTreeDefine.lua
        /// <summary>
        /// 条件节点名数据
        /// </summary>
        public static string[] BTLuaConditionNodeNameArray = {
            "ActiveSelf",                                       // 指定对象是否激活
            "CompleteTask",                                     // 是否已完成指定任务
            "HasTask",                                          // 是否拥有指定任务
            "TargetInSight",                                    // 指定目标对象是否在视野内
            "PlayerWithinScope",                                // 玩家是否在指定范围内
        };

        /// <summary>
        /// 条件节点描述数据(和BTLuaConditionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTLuaConditionNodeIntroArray =
        {
            "自身显示出来",                                      // 自身显示出来
            "完成指定任务",                                      // 完成指定任务
            "正在执行指定任务",                                  // 正在执行指定任务
            "指定目标对象是否在视野内",                          // 指定目标对象是否在视野内
            "玩家是否在指定范围内",                              // 玩家是否在指定范围内
        };

        /// <summary>
        /// 条件节点描述数据(和BTLuaConditionNodeNameArray一一对应)
        /// </summary>
        public static string[] BTLuaConditionNodeParamsIntroArray =
        {
            "目标对象UID(0表示玩家)",                            //自身显示出来
            "需要完成任务ID",                                    // 完成指定任务
            "需要拥有的任务ID",                                  // 正在执行指定任务
            "判定对象UID(0表示主玩家),半径",                     // 指定目标对象是否在视野内
            "中心点X,中心点Y,中心点Z,半径",                      // 玩家是否在指定范围内
        };
        #endregion


        #endregion
    }
}