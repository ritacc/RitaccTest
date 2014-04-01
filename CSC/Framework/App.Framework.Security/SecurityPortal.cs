using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using App.Framework.Configuration;

namespace App.Framework.Security
{
    public static class SecurityPortal
    {


        #region Configuration

        /// <summary>
        /// 获取应用程序的名称。
        /// </summary>
        public static string ApplicationName { get { return GetConfigData().ApplicationName; } }
        /// <summary>
        /// 获取业务单位的名称。
        /// </summary>
        public static string BusinessUnit { get { return GetConfigData().BusinessUnit; } }
        /// <summary>
        /// 获取锁定用户前允许的无效密码尝试次数。
        /// </summary>
        /// <remarks>0 = 无限制</remarks>
        public static uint MaxInvalidPasswordAttempts { get { return GetConfigData().MaxInvalidPasswordAttempts; } }
        /// <summary>
        /// 密码有效天数
        /// </summary>
        /// /// <remarks>0 = 永远有效</remarks>
        public static uint PasswordEffectiveDays { get { return GetConfigData().PasswordEffectiveDays; } }
        /// <summary>
        /// 获取密码所要求的最小长度。
        /// </summary>
        /// <remarks>0 = 无限制</remarks>
        public static uint MinRequiredPasswordLength { get { return GetConfigData().MinRequiredPasswordLength; } }
        /// <summary>
        /// 获取有效密码中必须包含的最少特殊字符数。
        /// </summary>
        /// <remarks>0 = 无限制</remarks>
		public static uint MinNonAlphaChar { get { return GetConfigData().MinNonAlphaChar; } }
		/// <summary>
		/// 获取有效密码中必须包含的最少字符数。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public static uint MinAlphaChar { get { return GetConfigData().MinAlphaChar; } }
		/// <summary>
		/// 获取有效密码中必须包含的最少数字长度。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public static uint MinNumChar { get { return GetConfigData().MinNumChar; } }

		/// <summary>
		/// 最近修改的几次密码不能重用。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public static uint PasswordReusePreventionCount { get { return GetConfigData().PasswordReusePreventionCount; } }

		/// <summary>
		/// 初次登录改密码。
		/// </summary>
		/// <remarks>false = 不改密码</remarks>
		public static bool InitialLogonChangePassword { get { return GetConfigData().InitialLogonChangePassword; } }



        private static SecurityConfig config = null;
        /// <summary>
        /// 获取Web.config中的数据引擎配置信息
        /// </summary>
        /// <returns>返回Web.config中的数据引擎配置信息对象，类型：DataConfig</returns>
        internal static SecurityConfig GetConfigData()
        {
            if (config == null)
            {
                config = (SecurityConfig)ConfigurationManager.GetSection(SecurityConfig.GetConfigSection());
            }
            return config;
        }

        #endregion

        public static bool ValidateUser(string username,string userdescription, string password, string shopCode, IUserProfile profile,bool autoSaveState = true)
        {
            return new DefaultSecurityProvider(DefaultEncrypt.Instance, profile).ValidateUser(username,userdescription, password, shopCode, autoSaveState);
        }


        public static void ModifyUserLoginPwdAndAuthPwd(long userIndentity, string newLoginPwd, string newAuthPwd, IUserProfile profile)
        {
            new DefaultSecurityProvider(DefaultEncrypt.Instance, profile).ModifyUserLoginPwdAndAuthPwd(userIndentity, newLoginPwd, newAuthPwd);
        }

        public static void ModifyUserAuthPwd(long userIndentity, string newAuthPwd, IUserProfile profile)
        {
            new DefaultSecurityProvider(DefaultEncrypt.Instance, profile).ModifyUserAuthPwd(userIndentity, newAuthPwd);
        }

        public static void ModifyUserLoginPwd(long userIndentity, string newLoginPwd, IUserProfile profile)
        {
            new DefaultSecurityProvider(DefaultEncrypt.Instance, profile).ModifyUserLoginPwd(userIndentity, newLoginPwd);
        }

        public static string GetCurrentUserLoginPwd(IUserProfile profile)
        {
            return new DefaultSecurityProvider(DefaultEncrypt.Instance, profile).GetUserPassword(App.Framework.Security.User.Current.UserName, App.Framework.Security.User.Current.ShopCode);
        }

		public static string EncryptPwd(IUserProfile profile,string pwd)
		{
			return new DefaultSecurityProvider(DefaultEncrypt.Instance, profile).EncryptPwd(pwd);
		}
    }
}