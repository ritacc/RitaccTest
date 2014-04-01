//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：IProvidePermissions.cs
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
    /// 存储和获取权限的提供者
    /// </summary>
    public interface IProvidePermissions
    {
        /// <summary>
        /// 从数据库或其他来源获取用户权限点的提供者
        /// </summary>
        IGetPermissions PermissionsProvider { set; get; }

        /// <summary>
        /// 获取用户的权限点
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <returns></returns>
        IList<PermissionsPoint> GetUserHasPermissionsPoints(string userIdentityKey,User.UserData userData);

        /// <summary>
        /// 存储用户的权限点，如存入缓存或者存入Cookies
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <param name="points"></param>
        void SavaPermissionsPoints(string userIdentityKey, IList<PermissionsPoint> points);

        /// <summary>
        /// 清除用户权限点
        /// </summary>
        /// <param name="userIdentityKey"></param>
        void ClearUserIdentity(string userIdentityKey);

        /// <summary>
        /// 获取当前权限点
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <returns></returns>
        PermissionsPoint GetCurrentPermissions(string userIdentityKey);

        /// <summary>
        /// 设置当前权限点
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <param name="currentPermission"></param>
        void SetCurrentPermissions(string userIdentityKey, PermissionsPoint currentPermission);
        
    }
}
