//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：UserIdentityCollection.cs
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
    /// 用户集合
    /// </summary>
    public class UserIdentityCollection : ICollection<UserIdentity>, IProvidePermissions
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, UserIdentity> _UserIdentityList = new Dictionary<string, UserIdentity>();

        /// <summary>
        /// 
        /// </summary>
        private static readonly UserIdentityCollection _Instance = new UserIdentityCollection();

        /// <summary>
        /// 
        /// </summary>
        private static object _ThreadLock = new object();

        /// <summary>
        /// 权限获取提供者
        /// </summary>
        public IGetPermissions PermissionsProvider { get; set; }

        /// <summary>
        /// 缓存用户状态数量
        /// </summary>
        public int CacheCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private UserIdentityCollection()
        {
            CacheCount = 1000;//默认缓存
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static UserIdentityCollection Instance
        {
            get { return _Instance; }
        }

        /// <summary>
        /// 根据用户标识获取用户被缓存的权限点,如果没有此用户则从外界获取
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public UserIdentity this[string userIdentity]
        {
            get
            {
                lock (_ThreadLock)
                {
                    if (_UserIdentityList.ContainsKey(userIdentity))
                        return _UserIdentityList[userIdentity];
                    UserIdentity uIdentity = new UserIdentity()
                    {
                        UserIdentityKey = userIdentity
                    };
                    _UserIdentityList.Add(userIdentity, uIdentity);
                    return uIdentity;
                }

            }
        }

        /// <summary>
        /// 添加新的用户身份
        /// </summary>
        /// <param name="item"></param>
        public void Add(UserIdentity item)
        {
            lock (_ThreadLock)
            {
                if (_UserIdentityList.Count > CacheCount)
                    _UserIdentityList.Remove(GetEnumerator().Current.UserIdentityKey);
                if (_UserIdentityList.ContainsKey(item.UserIdentityKey))
                    _UserIdentityList.Remove(item.UserIdentityKey);
                _UserIdentityList.Add(item.UserIdentityKey, item);
            }
        }


        /// <summary>
        /// 包含？
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(UserIdentity item)
        {
            return _UserIdentityList.ContainsKey(item.UserIdentityKey);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(UserIdentity[] array, int arrayIndex)
        {
            lock (_ThreadLock)
            {
                foreach (UserIdentity item in array)
                    _UserIdentityList.Add(item.UserIdentityKey, item);
            }
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        public int Count
        {
            get { return _UserIdentityList.Count; }
        }

        /// <summary>
        /// 是否吟只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(UserIdentity item)
        {
            lock (_ThreadLock)
            {
                return _UserIdentityList.Remove(item.UserIdentityKey);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            lock (_ThreadLock)
            {
                _UserIdentityList.Clear();
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public IEnumerator<UserIdentity> GetEnumerator()
        {
            return _UserIdentityList.Values.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 获取用户拥有的权限
        /// </summary>
        /// <param name="userIdentityKey">用户身份标识</param>
        /// <param name="userData"></param>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public IList<PermissionsPoint> GetUserHasPermissionsPoints(string userIdentity, User.UserData userData)
        {
            if (_UserIdentityList.ContainsKey(userIdentity) && _UserIdentityList[userIdentity].HasPermissionsPoint != null && _UserIdentityList[userIdentity].HasPermissionsPoint.Count >0)
                    return _UserIdentityList[userIdentity].HasPermissionsPoint;
                else
                {
                    if (PermissionsProvider == null)
                        throw new InvalidOperationException("该用户的会话状态还未存储并且没有指定如何从外部获取该用户权限");
                    IList<PermissionsPoint> pointList = PermissionsProvider.GetPermissionsPoint(userIdentity,userData);
                    this[userIdentity].HasPermissionsPoint = pointList;
                    return pointList;
                }
        }

        /// <summary>
        /// 存储用户权限
        /// </summary>
        /// <param name="userIdentityKey">用户身份标识</param>
        /// <param name="points">权限点</param>
        public void SavaPermissionsPoints(string userIdentityKey, IList<PermissionsPoint> points)
        {
            Add(new UserIdentity()
            {
                UserIdentityKey = userIdentityKey,
                HasPermissionsPoint = points
            });
        }

        /// <summary>
        /// 清除特定用户所存储的权限
        /// </summary>
        /// <param name="userIdentityKey">用户身份标识</param>
        public void ClearUserIdentity(string userIdentityKey)
        {
            lock (_ThreadLock)
            {
                _UserIdentityList.Remove(userIdentityKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <returns></returns>
        public PermissionsPoint GetCurrentPermissions(string userIdentityKey)
        {
            return this[userIdentityKey].CurrentPermissionsPoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentityKey"></param>
        /// <param name="currentPermission"></param>
        public void SetCurrentPermissions(string userIdentityKey, PermissionsPoint currentPermission)
        {
            this[userIdentityKey].CurrentPermissionsPoint = currentPermission;
        }

    }
}
