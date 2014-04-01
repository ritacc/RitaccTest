//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：PermissionsPoint.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-26
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.Permissions
{
    /// <summary>
    /// 用户权限点实体
    /// </summary>
    public class PermissionsPoint
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionsId">权限编号</param>
        public PermissionsPoint(string permissionsId)
        {
            PermissionsId = permissionsId;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionsId">权限编号</param>
        /// <param name="description">权限描述</param>
        /// <param name="key"></param>
        public PermissionsPoint(string permissionsId,string key, string description)
        {
            PermissionsId = permissionsId;
            Description = description;
            Key = key;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="description"></param>
        public PermissionsPoint(string key, string description)
        {
            Key = key;
            Description = description;
        }

        /// <summary>
        /// 权限编号
        /// </summary>
        public string PermissionsId { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 唯一键
        /// </summary>
        public string Key { get; set; }
    }
}
