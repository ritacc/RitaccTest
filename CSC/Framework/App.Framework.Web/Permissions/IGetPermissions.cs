//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：IGetPermissions.cs
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
using App.Framework.Web.User;

namespace App.Framework.Web.Permissions
{
    /// <summary>
    /// 从数据库或其他来源获取用户权限点
    /// </summary>
    public interface IGetPermissions
    {
        /// <summary>
        /// 从数据库或其他来源获取用户权限点（利用缓存存储时，如果从缓存中找不到，会调用此接口获取）
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <returns></returns>
        IList<PermissionsPoint> GetPermissionsPoint(string userIdentity,UserData user);
    }
}
