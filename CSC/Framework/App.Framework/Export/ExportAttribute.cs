using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace App.Framework.Export
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ExportAttribute : Attribute
	{
		#region Properties
		public string Name { get; set; }
		public string ResourceName { get; set; }
		public string FormatString { get; set; }
		public PropertyInfo BoundProperty { get; set; }
		#endregion

		#region Constructors
		public ExportAttribute(string name)
		{
			Name = name;
		}

		public ExportAttribute(string name, string resourceName)
		{
			Name = name;
			ResourceName = resourceName;
		}

		public ExportAttribute(string name, string resourceName, string formatString)
		{
			Name = name;
			ResourceName = resourceName;
			FormatString = formatString;
		}
		#endregion
	}
}