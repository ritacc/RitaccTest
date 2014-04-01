using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics;

namespace App.Framework.Data
{
	public class SqlProvider : DbProviderFactory, IDataProvider
	{
		#region DbProviderFactory

		public override bool CanCreateDataSourceEnumerator
		{
			get { return SqlClientFactory.Instance.CanCreateDataSourceEnumerator; }
		}

		public override DbCommand CreateCommand()
		{
			return SqlClientFactory.Instance.CreateCommand();
		}

		public override DbCommandBuilder CreateCommandBuilder()
		{
			return SqlClientFactory.Instance.CreateCommandBuilder();
		}

		public override DbConnection CreateConnection()
		{
			return SqlClientFactory.Instance.CreateConnection();
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return SqlClientFactory.Instance.CreateConnectionStringBuilder();
		}

		public override DbDataAdapter CreateDataAdapter()
		{
			return SqlClientFactory.Instance.CreateDataAdapter();
		}

		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlClientFactory.Instance.CreateDataSourceEnumerator();
		}

		public override DbParameter CreateParameter()
		{
			return SqlClientFactory.Instance.CreateParameter();
		}

		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return SqlClientFactory.Instance.CreatePermission(state);
		}

		#endregion

		#region IDataProvider

		private string connectionString = DataPortal.GetConfigData().GetConnectionString();

		public string ConnectionString
		{
			get { return connectionString; }
			set { connectionString = value; }
		}

		public IDbTransaction BeginTransaction()
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			IDbTransaction result = connection.BeginTransaction();
			return result;
		}

		public IDbTransaction BeginTransaction(IsolationLevel iso)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			IDbTransaction result = connection.BeginTransaction(iso);
			return result;
		}

		public DataResult ExecuteNonQuery(DataCriteria criteria)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = CreateCommand(criteria))
				{
					command.Connection = connection;
					connection.Open();
					command.ExecuteNonQuery();
					RetrieveCriteria(command, criteria);
				}
			}
			return new DataResult(criteria.ResultType, criteria.ResultMessage);
		}

		public DataResult ExecuteNonQuery(DataCriteria criteria, IDbTransaction trans)
		{
			SqlConnection connection = (SqlConnection)trans.Connection;
			using (SqlCommand command = CreateCommand(criteria))
			{
				command.Connection = connection;
				command.Transaction = (SqlTransaction)trans;
				command.ExecuteNonQuery();
				RetrieveCriteria(command, criteria);
			}
			return new DataResult(criteria.ResultType, criteria.ResultMessage);
		}

		public DataSet ExecuteDataSet(DataCriteria criteria)
		{
			DataSet result = new DataSet();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = CreateCommand(criteria))
				{
					command.Connection = connection;
					SqlDataAdapter adapter = new SqlDataAdapter(command);
					connection.Open();
					adapter.Fill(result);
					RetrieveCriteria(command, criteria);
				}
			}

			return result;
		}

		public DataSet ExecuteDataSet(DataCriteria criteria, IDbTransaction trans)
		{
			DataSet result = null;
			SqlConnection connection = (SqlConnection)trans.Connection;
			SqlCommand command = CreateCommand(criteria);
			command.Connection = connection;
			command.Transaction = (SqlTransaction)trans;
			SqlDataAdapter adapter = new SqlDataAdapter(command);
			connection.Open();
			adapter.Fill(result);
			RetrieveCriteria(command, criteria);
			return result;
		}

		public object ExecuteReader(DataCriteria criteria, DataReaderDelegate readerDelegate)
		{

			object result = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = CreateCommand(criteria))
				{
					command.Connection = connection;
					connection.Open();
					
					SqlDataReader sqlReader = command.ExecuteReader();

					using (SqlNullableDataReader reader = new SqlNullableDataReader(sqlReader))
					{
						result = readerDelegate(reader);
					}
					RetrieveCriteria(command, criteria);
				}
			}

			return result;
		}

		public object ExecuteReader(DataCriteria criteria, IDbTransaction trans, DataReaderDelegate readerDelegate)
		{
			object result = null;
			SqlConnection connection = (SqlConnection)trans.Connection;
			SqlCommand command = CreateCommand(criteria);
			command.Connection = connection;
			command.Transaction = (SqlTransaction)trans;
			using (SqlNullableDataReader reader = new SqlNullableDataReader(command.ExecuteReader()))
			{
				result = readerDelegate(reader);
			}
			RetrieveCriteria(command, criteria);
			return result;
		}

		public T ExecuteScalar<T>(DataCriteria criteria)
		{
			T result;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = CreateCommand(criteria))
				{
					command.Connection = connection;
					connection.Open();
					result = (T)command.ExecuteScalar();
					RetrieveCriteria(command, criteria);
				}
			}
			return result;
		}

		public T ExecuteScalar<T>(DataCriteria criteria, IDbTransaction trans)
		{
			T result = default(T);
			SqlConnection connection = (SqlConnection)trans.Connection;
			using (SqlCommand command = CreateCommand(criteria))
			{
				command.Connection = connection;
				command.Transaction = (SqlTransaction)trans;
				result = (T)command.ExecuteScalar();
				RetrieveCriteria(command, criteria);
			}
			return result;
		}

		#endregion

		#region Helper

		protected SqlCommand CreateCommand(DataCriteria criteria)
		{
			SqlCommandBuilder.Builder builder = SqlCommandBuilder.CreateBuilder(criteria.GetType());
			SqlCommand result = builder(criteria);
			if (criteria.Timeout > 0) { result.CommandTimeout = criteria.Timeout; }
			return result;
		}

		protected void RetrieveCriteria(SqlCommand command, DataCriteria criteria)
		{
			DataResult result = new DataResult(command.Parameters["@ResultType"].Value == null ? 0 : (int)command.Parameters["@ResultType"].Value, command.Parameters["@ResultMessage"].Value == null ? "" : command.Parameters["@ResultMessage"].Value.ToString());

			criteria.ResultType = command.Parameters["@ResultType"].Value == null ? 0 : (int)command.Parameters["@ResultType"].Value;
			criteria.ResultMessage = command.Parameters["@ResultMessage"].Value == null ? "" : command.Parameters["@ResultMessage"].Value.ToString();
			if (criteria.ResultType == 0)
			{
				SqlCriteriaSync.Sync sync = SqlCriteriaSync.CreateSync(criteria.GetType());
				sync(command, criteria);
			}
		}

		public static SqlDbType GetDbType(Type type)
		{
			SqlDbType result = SqlDbType.NVarChar;
			switch (type.Name)
			{
				case "Byte":
					result = SqlDbType.TinyInt;
					break;
				case "Int16":
					result = SqlDbType.SmallInt;
					break;
				case "Int32":
					result = SqlDbType.Int;
					break;
				case "Int64":
					result = SqlDbType.BigInt;
					break;
				case "Boolean":
					result = SqlDbType.Bit;
					break;
				case "Decimal":
					result = SqlDbType.Decimal;
					break;
				case "Double":
					result = SqlDbType.Real;
					break;
				case "Float":
					result = SqlDbType.Float;
					break;
				case "DateTime":
					result = SqlDbType.DateTime;
					break;
			}
			return result;
		}

		public static object GetOutputParameterValue(SqlParameter param)
		{
			if (param.Value == DBNull.Value)
			{
				return null;
			}
			return param.Value;
		}

		#endregion
	}
}