using System;
using System.Data;
using System.Data.Common;

namespace App.Framework.Data
{
	public interface IDataProvider
	{
		string ConnectionString{ get; set; }
		IDbTransaction BeginTransaction();
		IDbTransaction BeginTransaction(IsolationLevel iso);
		DataResult ExecuteNonQuery(DataCriteria criteria);
		DataResult ExecuteNonQuery(DataCriteria criteria, IDbTransaction trans);
		DataSet ExecuteDataSet(DataCriteria criteria);
		DataSet ExecuteDataSet(DataCriteria criteria, IDbTransaction trans);
		object ExecuteReader(DataCriteria criteria, DataReaderDelegate readerDelegate);
		object ExecuteReader(DataCriteria criteria, IDbTransaction trans, DataReaderDelegate readerDelegate);
		T ExecuteScalar<T>(DataCriteria criteria);
	}
}