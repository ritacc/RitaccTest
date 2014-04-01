//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：AuthorizationFilter.cs
//文件功能：页面验证 拦截器
//
//修改标识：鲜红
//修改描述：
//**********************************************************
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using System;
using App.Framework.Web.Permissions;
using App.Framework.Web.User;
using System.Web;
using App.Framework.Web.Menu;


namespace App.Framework.Web.Filters
{

    /// <summary>
    /// 页面验证 拦截器
    /// </summary>
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter, IExceptionFilter, IActionFilter
    {
        private readonly IProvidePermissions _permissionsProvider = UserIdentityCollection.Instance;
        private readonly ExceptionFilter _exceptionFilter = new ExceptionFilter();
        private readonly IActionFilter _executionTimingFilter = new ExecutionTimingFilterAttribute();
        

        /// <summary>
        /// 
        /// </summary>
        public PermissionsPoint CurrentPermissionsPoint { get; private set; }

        /// <summary>
        /// 权限枚举
        /// </summary>
        public static Type PowerEnumType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">当前的权限点编号</param>
        /// <param name="description">权限点的描述信息</param>
        [Obsolete("请使用AuthorizationFilter(int id)构造函数", false)]
        public AuthorizationFilter(string key, string description)
            : this(new PermissionsPoint(key, description))
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">权限点标识</param>
        [Obsolete("请使用AuthorizationFilter(int id)构造函数", false)]
        public AuthorizationFilter(string key)
            : this(new PermissionsPoint(null, key, null))
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">权限数值</param>
        public AuthorizationFilter(int id)   
        {
            string description = "";
            string key = ConvertToEnumName(id,out description);
            var per = new PermissionsPoint(id.ToString(), key, description);

            CurrentPermissionsPoint = per;
            _permissionsProvider.SetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity, per);
            _permissionsProvider = UserIdentityCollection.Instance;
        }

        /// <summary>
        /// 将int转换为枚举名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ConvertToEnumName(int id,out string description)
        {
            
            description = null;
            return string.Empty;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionsId">权限点编号</param>
        /// <param name="key">权限点标识</param>
        /// <param name="description">描述</param>
        public AuthorizationFilter(string permissionsId, string key, string description)
            : this(new PermissionsPoint(permissionsId, key, description))
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentPermissionsPoint">当前权限点实体</param>
        public AuthorizationFilter(PermissionsPoint currentPermissionsPoint)
        {
            CurrentPermissionsPoint = currentPermissionsPoint;
            _permissionsProvider.SetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity, currentPermissionsPoint);
            _permissionsProvider = UserIdentityCollection.Instance;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentPermissionsPoint">当前权限点实体</param>
        /// <param name="permissionsProvider">存储和获取权限的提供者</param>
        public AuthorizationFilter(PermissionsPoint currentPermissionsPoint, IProvidePermissions permissionsProvider)
        {
            CurrentPermissionsPoint = currentPermissionsPoint;
            _permissionsProvider = permissionsProvider;
        }

        /// <summary>
        /// 认证逻辑方法
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            UserIdentityFactory.Instance.AutoRedirectToLoginPage(() =>
            {
                //验证权限
                _permissionsProvider.SetCurrentPermissions(UserIdentityFactory.Instance.UserIdentity, CurrentPermissionsPoint);
                IAuthenticate auth = new UserIdentityAuthorization(_permissionsProvider);
                auth.Authenticate(UserIdentityFactory.Instance.UserIdentity,
                    new UserData() { Token = UserIdentityFactory.Instance.Token, RoleId = UserIdentityFactory.Instance.RoleId, RoleName = UserIdentityFactory.Instance.RoleName }
                    , CurrentPermissionsPoint);
            }, () => {
                filterContext.Result = new HttpUnauthorizedResult();
            });
            
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            _exceptionFilter.OnException(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _executionTimingFilter.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _executionTimingFilter.OnActionExecuting(filterContext);
        }
    }
}