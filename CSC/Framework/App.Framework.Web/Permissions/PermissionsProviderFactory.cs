//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：PermissionsProviderFactory.cs
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
    /// 权限提供者工厂
    /// </summary>
    public static class PermissionsProviderFactory
    {
        /// <summary>
        /// 权限提供者工厂
        /// </summary>
        /// <returns></returns>
        public static IProvidePermissions ProvidePermissions
        {
            get { return UserIdentityCollection.Instance; }//先简单实现
        }
    }
}
