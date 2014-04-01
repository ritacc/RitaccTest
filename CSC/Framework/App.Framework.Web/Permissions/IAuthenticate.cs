//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：IAuthenticate.cs
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
    /// 权限认识接口
    /// </summary>
    public interface IAuthenticate
    {
        /// <summary>
        /// 验证用户是否有访问传入功能点的权限
        /// </summary>
        /// <exception cref="AuthorizationException"></exception>
        /// <param name="userIdentityKey">用户身份标识</param>
        /// <param name="currentAccessPermissions">当前访问权限点</param>
        void Authenticate(string userIdentityKey, User.UserData userData, PermissionsPoint currentAccessPermissions);

        /// <summary>
        /// 验证用户是否有访问传入功能点的权限
        /// </summary>
        /// <param name="userIdentityKey">用户身份标识</param>
        /// <param name="currentAccessPermissions">当前访问权限点</param>
        /// <returns></returns>
        bool UserHasPermission(string userIdentityKey, User.UserData userData, PermissionsPoint currentAccessPermissions);
    }

   
}
