using System;
using System.Data;

namespace App.Framework.Data
{
	public interface INullableDataReader : IDataReader
	{
		bool? GetNullableBoolean(int i);
		byte? GetNullableByte(int i);
		DateTime? GetNullableDateTime(int i);
		decimal? GetNullableDecimal(int i);
		double? GetNullableDouble(int i);
		float? GetNullableFloat(int i);
		short? GetNullableInt16(int i);
		int? GetNullableInt32(int i);
		long? GetNullableInt64(int i);
	    Guid? GetNullableGuid(int i);
	}
}