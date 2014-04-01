using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Data
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class DbFieldAttribute : Attribute
	{
		#region Properties
		public string FieldName { get; set; }
		#endregion

		#region Constructors
		public DbFieldAttribute(string fieldName)
		{
			FieldName = fieldName;
		}
		#endregion
	}
}