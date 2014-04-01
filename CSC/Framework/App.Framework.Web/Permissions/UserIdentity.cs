//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：UserIdentity.cs
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
    /// 用户身份标识类
    /// </summary>
    public class UserIdentity : IDisposable
    {
        /// <summary>
        /// 用户身份标识
        /// </summary>
        public string UserIdentityKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User.UserData UserData { get; set; }
        /// <summary>
        /// 用户拥有的权限点
        /// </summary>
        public IList<PermissionsPoint> HasPermissionsPoint { get; internal set; }


        private PermissionsPoint _CurrentPermissionsPoint;
        /// <summary>
        /// 当前权限点
        /// </summary>
        public PermissionsPoint CurrentPermissionsPoint
        {
            get
            {
                if (_CurrentPermissionsPoint == null)
                {
                    if (HasPermissionsPoint.Count > 0)
                        return HasPermissionsPoint[0];
                    return null;
                }
                return _CurrentPermissionsPoint;
            }
            internal set
            {
                if (HasPermissionsPoint == null || !HasPermissionsPoint.Any(p => p.Key == value.Key || p.PermissionsId == value.PermissionsId))
                    return;
                    //throw new InvalidOperationException("该用户的权限表中不存在此权限");
                _CurrentPermissionsPoint = value;
            }
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserIdentity()
        {
            HasPermissionsPoint = new List<PermissionsPoint>();

        }

        /// <summary>
        /// 权限添加
        /// </summary>
        /// <param name="point"></param>
        public void AddPermissions(PermissionsPoint point)
        {
            HasPermissionsPoint.Add(point);
        }

        /// <summary>
        /// 权限点移除
        /// </summary>
        /// <param name="point"></param>
        public void RemovePermissions(PermissionsPoint point)
        {
            HasPermissionsPoint.Remove(point);
        }

        /// <summary>
        /// 清除权限点
        /// </summary>
        public void Clear()
        {
            HasPermissionsPoint.Clear();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            UserIdentityCollection.Instance.Remove(this);
        }



    }
}
