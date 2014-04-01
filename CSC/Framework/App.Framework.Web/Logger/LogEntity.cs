//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：LogEntity.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-25
//
//修改标识：
//修改描述：
//**********************************************************
using System;

namespace App.Framework.Web.Logger
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogTime { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Opreater { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 事件级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 权限点
        /// </summary>
        public int PermissionsId { get; set; }
        /// <summary>
        /// 事件状态
        /// </summary>
        public int EventType { get; set; }
    }


}
