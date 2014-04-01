using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Security
{
    public interface IUserProfile
    {
        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool UserIsExists(string userName, string shopCode);
        /// <summary>
        /// 获取用户密码
        /// </summary>
        /// <param name="userIndentify"></param>
        /// <returns></returns>
        string GetUserPassword(string userIndentify, string shopCode);
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userIndentify"></param>
        /// <param name="password"></param>
        void ModifyUserPassword(string userIndentify, string password, uint pwdExpiryDate);
        /// <summary>
        /// 修改用户登录密码和特批密码
        /// </summary>
        /// <param name="userIndentity"></param>
        /// <param name="newLoginPwd"></param>
        /// <param name="newAuthPwd"></param>
		void ModifyUserLoginPwdAndAuthPwd(long userID, string newLoginPwd, string newAuthPwd);
		/// <summary>
		/// 修改用户登录密码
		/// </summary>
		/// <param name="userIndentity"></param>
		/// <param name="newLoginPwd"></param> 
		void ModifyUserLoginPwd(long userID, string newLoginPwd);
		/// <summary>
		/// 修改用户特批密码
		/// </summary>
		/// <param name="userIndentity"></param> 
		/// <param name="newAuthPwd"></param>
		void ModifyUserAuthPwd(long userID,string newAuthPwd);
        /// <summary>
        /// 保存用户（登录）状态
        /// </summary>
        /// <param name="user"></param>
        void SaveUserState(UserBase user);
        /// <summary>
        /// 获取用户密码输入错误次数
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
		int GetErrorPasswordTimes(long userId);
        /// <summary>
        /// 存储用户密码输入错误次数
        /// </summary>
        /// <param name="userName"></param>
        void SaveErrorPasswordTimes(long userId);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserBase GetUserInfo(string userName,string shopCode);
        /// <summary>
        /// 获取用户密码过期时间
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        DateTime? GetPasswordEffectiveDays(string userName, string shopCode);
		/// <summary>
		/// 锁定用户
		/// </summary>
		/// <param name="userId"></param>
		void Freeze(long userId);

		void ClearUserErrorPasswordTimes(long userId);

		/// <summary>
		/// 修改密码时，最近几次使用的密码不能被使用
		/// </summary>
		/// <param name="userId">用户</param>
		/// <param name="newPwd">新密码</param>
		/// <param name="lastUseCount">最近使用次数</param>
		/// <returns>false-新密码不能被使用</returns>
		bool CheckLastPwd(long userId,string newPwd,uint lastUseCount);
    }
}
