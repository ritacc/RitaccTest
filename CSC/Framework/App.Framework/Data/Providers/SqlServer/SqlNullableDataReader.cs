using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace App.Framework.Data
{
	public class SqlNullableDataReader : INullableDataReader
	{
		protected SqlDataReader m_reader;

		public SqlNullableDataReader(SqlDataReader reader)
		{
			m_reader = reader;

		}

		#region INullableDataReader

		public bool? GetNullableBoolean(int i)
		{
			bool? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetBoolean(i);
			}
			return result;
		}

		public byte? GetNullableByte(int i)
		{
			byte? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetByte(i);
			}
			return result;
		}

		public DateTime? GetNullableDateTime(int i)
		{
			DateTime? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetDateTime(i);
			}
			return result;
		}

		public decimal? GetNullableDecimal(int i)
		{
			decimal? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetDecimal(i);
			}
			return result;
		}

		public double? GetNullableDouble(int i)
		{
			double? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetDouble(i);
			}
			return result;
		}

		public float? GetNullableFloat(int i)
		{
			float? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetFloat(i);
			}
			return result;
		}

		public short? GetNullableInt16(int i)
		{
			short? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetInt16(i);
			}
			return result;
		}

		public int? GetNullableInt32(int i)
		{
			try
			{
				int? result = null;
				if (!m_reader.IsDBNull(i))
				{
					result = m_reader.GetInt32(i);
				}
				return result;
			}
			catch (Exception ex)
			{
				throw new Exception("i=" + i.ToString() + "|" + ex.Message);
			}
		}

		public long? GetNullableInt64(int i)
		{
			long? result = null;
			if (!m_reader.IsDBNull(i))
			{
				result = m_reader.GetInt64(i);
			}
			return result;
		}

		public Guid? GetNullableGuid(int i)
		{
			Guid? result = null;
			if (!m_reader.IsDBNull(i))
			{
				return m_reader.GetGuid(i);
			}
			return result;
		}


		#endregion

		#region IDataReader

		public void Close()
		{
			m_reader.Close();
		}

		public int Depth
		{
			get { return m_reader.Depth; }
		}

		public DataTable GetSchemaTable()
		{
			return m_reader.GetSchemaTable();
		}

		public bool IsClosed
		{
			get { return m_reader.IsClosed; }
		}

		public bool NextResult()
		{
			return m_reader.NextResult();
		}

		public bool Read()
		{
			return m_reader.Read();
		}

		public int RecordsAffected
		{
			get { return m_reader.RecordsAffected; }
		}

		#endregion

		#region IDisposable

		public void Dispose()
		{
			m_reader.Dispose();
		}

		#endregion

		#region IDataRecord

		public int FieldCount
		{
			get { return m_reader.FieldCount; }
		}

		public bool GetBoolean(int i)
		{
			return m_reader.GetBoolean(i);
		}

		public byte GetByte(int i)
		{
			return m_reader.GetByte(i);
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return m_reader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
		}

		public char GetChar(int i)
		{
			return m_reader.GetChar(i);
		}

		public long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return m_reader.GetChars(i, fieldOffset, buffer, bufferOffset, length);
		}

		public IDataReader GetData(int i)
		{
			return m_reader.GetData(i);
		}

		public string GetDataTypeName(int i)
		{
			return m_reader.GetDataTypeName(i);
		}

		public DateTime GetDateTime(int i)
		{
			return m_reader.GetDateTime(i);
		}

		public decimal GetDecimal(int i)
		{
			return m_reader.GetDecimal(i);
		}

		public double GetDouble(int i)
		{
			return m_reader.GetDouble(i);
		}

		public Type GetFieldType(int i)
		{
			return m_reader.GetFieldType(i);
		}

		public float GetFloat(int i)
		{
			return m_reader.GetFloat(i);
		}

		public Guid GetGuid(int i)
		{
			if (IsDBNull(i))
			{
				return Guid.Empty;
			}
			return m_reader.GetGuid(i);
		}

		public short GetInt16(int i)
		{
			return m_reader.GetInt16(i);
		}

		public int GetInt32(int i)
		{
			return m_reader.GetInt32(i);
		}

		public long GetInt64(int i)
		{
			return m_reader.GetInt64(i);
		}

		public string GetName(int i)
		{
			return m_reader.GetName(i);
		}

		public int GetOrdinal(string name)
		{
			return m_reader.GetOrdinal(name);
		}

		public string GetString(int i)
		{
			if (IsDBNull(i))
			{
				return string.Empty;
			}
			return m_reader.GetString(i);
		}

		public object GetValue(int i)
		{
			return m_reader.GetValue(i);
		}

		public int GetValues(object[] values)
		{
			return m_reader.GetValues(values);
		}

		public bool IsDBNull(int i)
		{
			return m_reader.IsDBNull(i);
		}

		public object this[string name]
		{
			get { return m_reader[name]; }
		}

		public object this[int i]
		{
			get { return m_reader[i]; }
		}

		#endregion
	}
}
