﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Collections;

namespace CSC
{
	public static class CacheHelper2
    {
        private static Cache _cache;
        public static double SaveTime 
        { 
            get; 
            set;
        }
        static CacheHelper2()
        {
            _cache = HttpRuntime.Cache;
            SaveTime = 15.0;
        }
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return _cache.Get(key);
        }
		public static T Get<T>(string key)
		{
			object obj = Get(key);
			return obj == null ? default(T) : (T)obj;
		}
		public static void Insert(string key, object value, CacheDependency dependency,double time,  CacheItemPriority priority, CacheItemRemovedCallback callback)
		{
			_cache.Insert(key, value, dependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(time), priority, callback);
		}
		
		public static void Insert(string key, object value, CacheDependency dependency, CacheItemPriority priority, CacheItemRemovedCallback callback)
        {
            _cache.Insert(key, value, dependency, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(SaveTime), priority, callback);
        }
        public static void Insert(string key, object value, CacheDependency dependency, CacheItemRemovedCallback callback)
        {
            Insert(key, value, dependency, CacheItemPriority.Default, callback);
        }
        public static void Insert(string key, object value, CacheDependency dependency)
        {
            Insert(key, value, dependency, CacheItemPriority.Default, null);
        }
        public static void Insert(string key, object value)
        {
            Insert(key, value, null, CacheItemPriority.Default, null);
        }


        public static void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            _cache.Remove(key);
        }
        public static IList<string> GetKeys()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator enumerator = _cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            return keys.AsReadOnly();
        }
        public static void RemoveAll()
        {
            IList<string> keys = GetKeys();
            foreach (string key in keys)
            {
                _cache.Remove(key);
            }
        }
    }
}