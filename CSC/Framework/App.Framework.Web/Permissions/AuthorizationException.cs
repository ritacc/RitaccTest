//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：AuthorizationException.cs
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
    /// 权限认证异常
    /// </summary>
    public class AuthorizationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg"></param>
        public AuthorizationException(string msg)
            : base(msg)
        {
            IsAuthenticated = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isAuthenticated"></param>
        public AuthorizationException(string msg, bool isAuthenticated)
            : base(msg)
        {
            IsAuthenticated = isAuthenticated;
        }
    }
}
