using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace App.Framework.Data
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class DbParameterAttribute : Attribute
	{
		#region Properties
		public string ParameterName { get; set; }
		public ParameterDirection Direction { get; set; }
		public int Size { get; set; }
		#endregion

		#region Constructors
		public DbParameterAttribute(string parameterName)
		{
			ParameterName = parameterName;
			Direction = ParameterDirection.Input;
			Size = -1;
		}

		public DbParameterAttribute(string parameterName, ParameterDirection direction)
		{
			ParameterName = parameterName;
			Direction = direction;
			Size = -1;
		}

		public DbParameterAttribute(string parameterName, ParameterDirection direction, int size)
		{
			ParameterName = parameterName;
			Direction = direction;
			Size = size;
		}
		#endregion
	}
}