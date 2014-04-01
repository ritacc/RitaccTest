using System;
using System.Data;

namespace App.Framework.Data
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DbCommandAttribute : Attribute
	{
		public string CommandText { get; private set; }
		public CommandType CommandType { get; private set; }

		public DbCommandAttribute(string commandText)
		{
			CommandText = commandText;
			CommandType = CommandType.StoredProcedure;
		}

		public DbCommandAttribute(string commandText, CommandType commandType)
		{
			CommandText = commandText;
			CommandType = commandType;
		}
	}
}