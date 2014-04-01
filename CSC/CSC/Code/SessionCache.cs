using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Framework.Caching;

namespace CSC
{
	public class SessionCache : ICacheProvider
	{
		private static readonly SessionCache _Instance = new SessionCache();
		private static readonly object Lock = new object();
		private const int AppcacheCount = 50; //
		private const int UsercacheCount = 5; //每个用户存放的Cache数量
		private SessionCache() { }
		public static SessionCache Instance
		{
			get
			{
				return _Instance;
			}
		}

		private readonly Dictionary<string, Dictionary<string, object>> _dicCache = new Dictionary<string, Dictionary<string, object>>();

		private readonly Dictionary<string, DateTime> _dicAppTimeCache = new Dictionary<string, DateTime>();//针对总的用户的缓存时间

		private readonly Dictionary<string, Dictionary<string, DateTime>> _dicUserTimeCache = new Dictionary<string, Dictionary<string, DateTime>>();//针对每个用户里所存每个对象的时间

		private string SessionID
		{
			get
			{
				return HttpContext.Current.Session.SessionID;
			}
		}

		/// <summary>
		/// 添加
		/// </summary>
		/// <param name="key"></param>
		/// <param name="obj"></param>
		public void Add(string key, object obj)
		{
			lock (Lock)
			{
				//if (_dicCache.Count >= AppcacheCount) //如果超出应用程序所有cache数，则踢掉最前面的
				//{
				//var index0 = _dicCache.First();
				//_dicCache.Remove(index0.Key);

				//string oldKey = GetOldKey(SessionID);
				//_dicCache[SessionID].Remove(oldKey);
				//_dicTimeCache[SessionID].Remove(key);
				//}

				if (!_dicCache.ContainsKey(SessionID))
				{
					string appOldKey = GetOldKeyWithApp();
					if (string.IsNullOrEmpty(appOldKey)) //表示缓存未满
					{
						_dicCache.Add(SessionID, new Dictionary<string, object>());
						_dicAppTimeCache.Add(SessionID, DateTime.Now);
						_dicUserTimeCache.Add(SessionID, new Dictionary<string, DateTime>());
					}
					else
					{
						_dicCache.Remove(appOldKey);
						_dicAppTimeCache.Remove(appOldKey);
						_dicUserTimeCache.Remove(appOldKey);

						_dicCache.Add(SessionID, new Dictionary<string, object>());
						_dicAppTimeCache.Add(SessionID, DateTime.Now);
						_dicUserTimeCache.Add(SessionID, new Dictionary<string, DateTime>());
					}
				}
				//if (_dicCache[SessionID].Count >= UsercacheCount)
				//{					
				//var index0 = _dicCache[SessionID].First();
				//_dicCache[SessionID].Remove(index0.Key);
				//}
				if (!_dicCache[SessionID].ContainsKey(key))
				{
					string userOldKey = GetOldKeyWithUser(SessionID);
					if (string.IsNullOrEmpty(userOldKey)) //表示缓存未满
					{
						_dicCache[SessionID].Add(key, obj);
						_dicUserTimeCache[SessionID].Add(key, DateTime.Now);
					}
					else
					{
						_dicCache[SessionID].Remove(userOldKey);
						_dicUserTimeCache[SessionID].Remove(userOldKey);
						_dicCache[SessionID].Add(key, obj);
						_dicUserTimeCache[SessionID].Add(key, DateTime.Now);
					}
				}
				else
				{
					_dicCache[SessionID][key] = obj;
					_dicUserTimeCache[SessionID][key] = DateTime.Now;
				}
				//_dicCache[SessionID].Add(key, obj);
			}
		}

		/// <summary>
		/// 获取应用程序里所有用户用户的时间
		/// </summary>
		/// <returns>返回空，代表指定数量的缓存还未加满</returns>
		private string GetOldKeyWithApp()
		{
			string key = string.Empty;
			DateTime minDateTime;
			if (_dicAppTimeCache.Count >= AppcacheCount)
			{
				key = _dicAppTimeCache.First().Key;
				minDateTime = _dicAppTimeCache.First().Value;
				foreach (var dic in _dicAppTimeCache)
				{
					if (dic.Value.CompareTo(minDateTime) < 0)
					{
						key = dic.Key;
						minDateTime = dic.Value;
					}
				}
			}
			return key;
		}

		/// <summary>
		/// 获取每个用户的最旧的Key
		/// </summary>
		/// <param name="sessionID"></param>
		/// <returns></returns>
		private string GetOldKeyWithUser(string sessionID)
		{
			DateTime now;
			string key = string.Empty;
			if (_dicUserTimeCache.ContainsKey(sessionID) && _dicUserTimeCache[sessionID].Count >= UsercacheCount)
			{
				now = _dicUserTimeCache[sessionID].First().Value;
				key = _dicUserTimeCache[sessionID].First().Key;
				foreach (var dic in _dicUserTimeCache[sessionID])
				{
					if (dic.Value.CompareTo(now) < 0)
					{
						now = dic.Value;
						key = dic.Key;
					}
				}
			}
			return key;
		}

		/// <summary>
		/// 移除
		/// </summary>
		/// <param name="key"></param>
		public void Remove(string key)
		{
			lock (Lock)
			{
				if (_dicCache.ContainsKey(SessionID) && _dicCache[SessionID].ContainsKey(key))
				{
					_dicCache[SessionID].Remove(key);
					_dicUserTimeCache[SessionID].Remove(key);
				}
			}
		}

		/// <summary>
		/// 清除用户的所有缓存
		/// </summary>
		public void Clear()
		{
			lock (Lock)
			{
				if (_dicCache.ContainsKey(SessionID))
				{
					_dicCache[SessionID].Clear();
					_dicUserTimeCache[SessionID].Clear();
				}
			}
		}

		/// <summary>
		/// 获得指定key的Cache
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public T GetCache<T>(string key)
		{
			lock (Lock)
			{
				if (!_dicCache.ContainsKey(SessionID))
					return default(T);
				if (!_dicCache[SessionID].ContainsKey(key))
					return default(T);
				var result = _dicCache[SessionID][key];
				if (result == null || result.GetType() != typeof(T))
					return default(T);
				return (T)result;
			}
		}

		/// <summary>
		/// 如果有Cache则获取，没有则设置并返回
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="func"></param>
		/// <param name="noFromCache"></param>
		/// <returns></returns>
		public T GetOrSetCache<T>(string key, Func<T> func, bool noFromCache = false)
		{
			var result = GetCache<T>(key);
			if (result == null || noFromCache)
			{
				result = func();
				Add(key, result);
			}
			return result;
		}
	}



	//public class SessionCache : ICacheProvider
	//{
	//    private static readonly SessionCache _Instance = new SessionCache();
	//    private static readonly object Lock = new object();
	//    private const int AppcacheCount = 50; //
	//    private const int UsercacheCount = 5; //每个用户存放的Cache数量
	//    private SessionCache() { }
	//    public static SessionCache Instance
	//    {
	//        get
	//        {
	//            return _Instance;
	//        }
	//    }

	//    private readonly Dictionary<string, Dictionary<string, object>> _dicCache = new Dictionary<string, Dictionary<string, object>>();

	//    private string SessionID
	//    {
	//        get
	//        {
	//            return HttpContext.Current.Session.SessionID;
	//        }
	//    }

	//    /// <summary>
	//    /// 添加
	//    /// </summary>
	//    /// <param name="key"></param>
	//    /// <param name="obj"></param>
	//    public void Add(string key, object obj)
	//    {
	//        lock (Lock)
	//        {
	//            if (_dicCache.Count >= AppcacheCount) //如果超出应用程序所有cache数，则踢掉最前面的
	//            {
	//                var index0 = _dicCache.First();
	//                _dicCache.Remove(index0.Key);
	//            }
	//            if (!_dicCache.ContainsKey(SessionID))
	//                _dicCache.Add(SessionID, new Dictionary<string, object>());
	//            if (_dicCache[SessionID].Count >= UsercacheCount)
	//            {
	//                var index0 = _dicCache[SessionID].First();
	//                _dicCache[SessionID].Remove(index0.Key);
	//            }
	//            if (_dicCache[SessionID].ContainsKey(key))
	//                _dicCache[SessionID].Remove(key);
	//            _dicCache[SessionID].Add(key, obj);
	//        }
	//    }

	//    /// <summary>
	//    /// 移除
	//    /// </summary>
	//    /// <param name="key"></param>
	//    public void Remove(string key)
	//    {
	//        lock (Lock)
	//        {
	//            if (_dicCache.ContainsKey(SessionID) && _dicCache[SessionID].ContainsKey(key))
	//                _dicCache[SessionID].Remove(key);
	//        }
	//    }

	//    /// <summary>
	//    /// 清除用户的所有缓存
	//    /// </summary>
	//    public void Clear()
	//    {
	//        lock (Lock)
	//        {
	//            _dicCache[SessionID].Clear();
	//        }
	//    }

	//    /// <summary>
	//    /// 获得指定key的Cache
	//    /// </summary>
	//    /// <typeparam name="T"></typeparam>
	//    /// <param name="key"></param>
	//    /// <returns></returns>
	//    public T GetCache<T>(string key)
	//    {
	//        lock (Lock)
	//        {
	//            if (!_dicCache.ContainsKey(SessionID))
	//                return default(T);
	//            if (!_dicCache[SessionID].ContainsKey(key))
	//                return default(T);
	//            var result = _dicCache[SessionID][key];
	//            if (result == null || result.GetType() != typeof(T))
	//                return default(T);
	//            return (T)result;
	//        }
	//    }

	//    /// <summary>
	//    /// 如果有Cache则获取，没有则设置并返回
	//    /// </summary>
	//    /// <typeparam name="T"></typeparam>
	//    /// <param name="key"></param>
	//    /// <param name="func"></param>
	//    /// <param name="noFromCache"></param>
	//    /// <returns></returns>
	//    public T GetOrSetCache<T>(string key, Func<T> func, bool noFromCache = false)
	//    {
	//        var result = GetCache<T>(key);
	//        if (result == null || noFromCache)
	//        {
	//            result = func();
	//            Add(key, result);
	//        }
	//        return result;
	//    }
	//}


}
