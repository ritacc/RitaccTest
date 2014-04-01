//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：UserIdentityFactory.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-29
//
//修改标识：
//修改描述：
//**********************************************************

namespace App.Framework.Web.User
{
    /// <summary>
    /// 用户身份工厂
    /// </summary>
    public partial class UserIdentityFactory
    {
        /// <summary>
        /// 用户身份默认实例
        /// </summary>
        public static IGetUserIdentity Instance
        {
            get {
                return UserIdentityFormCookies.Instance; 
            }
        }
    }
}
