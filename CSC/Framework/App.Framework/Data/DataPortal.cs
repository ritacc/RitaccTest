using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using App.Framework.Configuration;
using App.Framework.Reflection;

namespace App.Framework.Data
{
	public delegate object DataReaderDelegate(INullableDataReader data);

	public static class DataPortal
	{
		public static IDbTransaction BeginTransaction()
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.BeginTransaction();
		}

		public static IDbTransaction BeginTransaction(IsolationLevel iso)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.BeginTransaction(iso);
		}

		public static DataResult ExecuteNonQuery(DataCriteria criteria)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.ExecuteNonQuery(criteria);
		}

		public static DataResult ExecuteNonQuery(DataCriteria criteria, IDbTransaction trans)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.ExecuteNonQuery(criteria, trans);
		}

		public static DataSet ExecuteDataSet(DataCriteria criteria)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.ExecuteDataSet(criteria);
		}

		public static DataSet ExecuteDataSet(DataCriteria criteria, IDbTransaction trans)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.ExecuteDataSet(criteria, trans);
		}

		public static object ExecuteReader(DataCriteria criteria, DataReaderDelegate readerDelegate)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.ExecuteReader(criteria, readerDelegate);
		}

		public static object ExecuteReader(DataCriteria criteria, IDbTransaction trans, DataReaderDelegate readerDelegate)
		{
			IDataProvider provider = GetDefaultProvider();
			return provider.ExecuteReader(criteria, trans, readerDelegate);
		}

		public static T ExecuteScalar<T>(DataCriteria criteria)
		{
			IDataProvider provider = GetDefaultProvider();
			T result = provider.ExecuteScalar<T>(criteria);
			return result;
		}

		#region DataProvider

		internal static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());

		public static IDataProvider GetDefaultProvider()
		{
			string connectionString = GetConfigData().GetConnectionString();
			Type providerType = Type.GetType(GetConfigData().Type, true, true);
			return GetProvider(connectionString, providerType);
		}

		public static IDataProvider GetProvider<T>(string connectionString) where T : IDataProvider
		{
			return GetProvider(connectionString, typeof(T));
		}

		public static IDataProvider GetProvider(string connectionString, Type providerType)
		{
			IDataProvider result = cache[connectionString] as IDataProvider;
			if(result == null)
			{
				lock(cache.SyncRoot)
				{
					result = cache[connectionString] as IDataProvider;
					if(result != null)
					{
						return result;
					}
					ObjectBuilder.Builder builder = ObjectBuilder.CreateBuilder(providerType);
					result = builder() as IDataProvider;
					result.ConnectionString = connectionString;
					cache.Add(connectionString, result);
				}
			}
			return result;
		}

		#endregion

		#region Configuration

		/// <summary>
		/// 从缓存中移除指定的数据库事务
		/// </summary>
		private static void EndTransaction(Guid transactionId)
		{
			//_cacheTransactions.Remove(transactionId);
		}


		private static DataConfig config = null;
		/// <summary>
		/// 获取Web.config中的数据引擎配置信息
		/// </summary>
		/// <returns>返回Web.config中的数据引擎配置信息对象，类型：DataConfig</returns>
		internal static DataConfig GetConfigData()
		{
			if(config == null)
			{
				config = (DataConfig)ConfigurationManager.GetSection(DataConfig.GetConfigSection());
			}
			return config;
		}

		#endregion

		#region Helpers

		public static Dictionary<string, int> FindFields(IDataRecord data)
		{
			Dictionary<string, int> result = new Dictionary<string, int>();
			for(int index = 0; index < data.FieldCount; index++)
			{
				var d = data.GetName(index);
				result.Add(d, index);
			}
			return result;
		}

		public static int FindField(Dictionary<string, int> fields, string fieldName)
		{
			int result = -1;
			if(! fields.TryGetValue(fieldName, out result))
			{
				return -1;
			}
			return result;
		}

		#endregion
	}
}