//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：IGetUserIdentity.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-29
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.User
{
    /// <summary>
    /// 获取用户身份接口
    /// </summary>
    public interface IGetUserIdentity
    {
        /// <summary>
        /// 用户身份标识
        /// </summary>
        string UserIdentity { get; }

        /// <summary>
        /// 是否认证通过
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 基它信息
        /// </summary>
        string OtherInfo { get; }

        /// <summary>
        /// 用户访问服务的校验字符串
        /// </summary>
        string Token { get; }

        /// <summary>
        /// 角色ID
        /// </summary>
        string RoleId { get; }

        /// <summary>
        /// 角色名
        /// </summary>
        string RoleName { get; }


        string UserDescription { get; }

        /// <summary>
        /// 设置登录成功
        /// </summary>
        /// <param name="userIndentityKey"></param>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="otherInfo"></param>
        void SetAuth(string userIndentityKey, string userName, string token, string roleId, string roleName, string otherInfo);

        /// <summary>
        /// 转转自登录页面
        /// </summary>
        void RedirectToLoginPage();

        /// <summary>
        /// 判断用户是否登录，若未登录则自动跳转,如果已经登录则执行action
        /// </summary>
        void AutoRedirectToLoginPage(Action successAction, Action failureAction);
    }
}
