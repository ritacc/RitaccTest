using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class MinValueAttribute : ValidationAttribute
	{
		public object MinValue { get; private set; }
		public MinValueAttribute(object minValue)
		{
			MinValue = minValue;
		}

		public override bool IsValidate(object value)
		{
			return true;
		}
	}
}
