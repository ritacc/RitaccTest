//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：UserIdentityAuthorization.cs
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
using App.Framework.Web;

namespace App.Framework.Web.Permissions
{
    /// <summary>
    /// 负责权限认证的类
    /// </summary>
    public class UserIdentityAuthorization : IAuthenticate
    {
        private IProvidePermissions _PermissionsProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="providePermissions">存储和获取权限的提供者</param>
        public UserIdentityAuthorization(IProvidePermissions providePermissions)
        {
            _PermissionsProvider = providePermissions;
        }


        /// <summary>
        /// 验证用户是否具有当前操作的权限
        /// </summary>
        /// <param name="userIdentityKey">用户标识</param>
        /// <param name="currentAccessPermissions"></param>
        /// <param name="userData"></param>
        public void Authenticate(string userIdentityKey, User.UserData userData, PermissionsPoint currentAccessPermissions)
        {
            if (!UserHasPermission(userIdentityKey,userData, currentAccessPermissions))
                throw new AuthorizationException(Resources.Messages.NoPermission);
        }


        /// <summary>
        /// 验证用户是否具有当前操作的权限
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <param name="currentAccessPermissions"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool UserHasPermission(string userIdentityKey,User.UserData userData, PermissionsPoint currentAccessPermissions)
        {
            IList<PermissionsPoint> pointList = _PermissionsProvider.GetUserHasPermissionsPoints(userIdentityKey, userData);
            if (pointList == null)
                return false;
            var l = pointList.ToList().ToList(m => m.PermissionsId);
            var s = l.ToString(",");
            var searchResult = pointList.Where(p => 
                (!string.IsNullOrEmpty(currentAccessPermissions.Key) && p.Key == currentAccessPermissions.Key) 
                || (!string.IsNullOrEmpty(currentAccessPermissions.PermissionsId) && p.PermissionsId == currentAccessPermissions.PermissionsId));

            if (searchResult.Count() >0)
                return true;
            return false;
        }
    }
}
