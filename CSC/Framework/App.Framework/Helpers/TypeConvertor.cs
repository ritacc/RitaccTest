using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework
{
	public static class TypeConvertor
	{
		public static Int32 ToInt32(Object value, Int32 defaultValue)
		{
			return ToInt32((string)value, defaultValue);
		}

		public static Int32 ToInt32(String value, Int32 defaultValue)
		{
			Int32 result;
			if(!Int32.TryParse(value, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		public static UInt32 ToUInt32(Object value, UInt32 defaultValue)
		{
			return ToUInt32((string)value, defaultValue);
		}

		public static UInt32 ToUInt32(String value, UInt32 defaultValue)
		{
			UInt32 result;
			if(!UInt32.TryParse(value, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		public static bool ToBool(Object value, bool defaultValue)
		{
			return ToBool((string)value, defaultValue);
		}

		public static bool ToBool(String value, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(value, out result))
			{
				result = defaultValue;
			}
			return result;
		}
	}
}