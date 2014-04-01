using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using App.Framework.Data;
using System.Data;

namespace App.Framework.Reflection
{

	public static class ReflectionHelper
	{
		private static readonly Hashtable creatorCache = Hashtable.Synchronized(new Hashtable());
		private static readonly Hashtable typeCache = Hashtable.Synchronized(new Hashtable());
		private static readonly Hashtable dbFieldPropertyCache = Hashtable.Synchronized(new Hashtable());
		private static readonly Hashtable attributeCache = Hashtable.Synchronized(new Hashtable());

		#region Type
		public static Type GetTypeByName(Type type, string typeName)
		{
			return GetType(type.Assembly.GetName().Name, type.Namespace, typeName);
		}

		public static Type GetType(string assembly, string nameSpace, string typeName)
		{
			string fullName = nameSpace + "." + typeName + "," + assembly;
			return GetType(fullName);
		}

		public static Type GetType(string fullName)
		{
			Type result = typeCache[fullName] as Type;
			if(result == null)
			{
				lock(typeCache.SyncRoot)
				{
					result = typeCache[fullName] as Type;
					if(result != null)
					{
						return result;
					}
					result = Type.GetType(fullName);
					typeCache.Add(fullName, result);
				}
			}
			return result;
		}
		#endregion

		public static PropertyInfo GetProperty(Type objectType, string propertyName)
		{
			PropertyInfo result = objectType.GetProperty(propertyName);
			return result;
		}

		public static PropertyInfo GetDbFieldProperty(Type objectType, string fieldName)
		{
			string keyFormat = "{0}|{1}";
			string key = string.Format(keyFormat, objectType.GetHashCode().ToString(), fieldName);
			PropertyInfo result = dbFieldPropertyCache[key] as PropertyInfo;
			if(result == null)
			{
				lock(dbFieldPropertyCache.SyncRoot)
				{
					result = dbFieldPropertyCache[key] as PropertyInfo;
					if(result != null || dbFieldPropertyCache.Count > 0)
					{
						return result;
					}

					foreach(PropertyInfo property in objectType.GetProperties())
					{
						DbFieldAttribute dbFieldAttr = GetAttribute<DbFieldAttribute>(property);
						if(dbFieldAttr == null)
						{
							continue;
						}
						key = string.Format(keyFormat, objectType.GetHashCode().ToString(), dbFieldAttr.FieldName);
						dbFieldPropertyCache.Add(key, property);
					}
					key = string.Format(keyFormat, objectType.GetHashCode().ToString(), fieldName);
					result = dbFieldPropertyCache[key] as PropertyInfo;
				}
			}
			return result;
		}

		#region Attribute

		public static T GetAttribute<T>(MemberInfo memberInfo) where T : Attribute
		{
			string keyFormat = "{0}|{1}";

			string key = string.Format(keyFormat, memberInfo.GetHashCode().ToString(), typeof(T).GetHashCode().ToString());
			T result = attributeCache[key] as T;

			if(result == null)
			{
				lock(attributeCache.SyncRoot)
				{
					result = attributeCache[key] as T;
					if(result != null)
					{
						return result;
					}
					Type attrType = typeof(T);
					if(Attribute.IsDefined(memberInfo, attrType))
					{
						result = (T)memberInfo.GetCustomAttributes(attrType, true)[0];
						attributeCache.Add(key, result);
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}
		#endregion
	}
}