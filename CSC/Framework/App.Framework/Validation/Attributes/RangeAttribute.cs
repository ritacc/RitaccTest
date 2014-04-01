using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Framework.Validation
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public sealed class RangeAttribute : ValidationAttribute
	{
		public object Min { get; private set; }
		public object Max { get; private set; }

		public RangeAttribute(object min, object max)
		{
			Min = min;
			Max = max;
		}

		public override bool IsValidate(object value)
		{
			throw new NotImplementedException();
		}
	}
}