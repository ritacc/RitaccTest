using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class MaxValueAttribute : ValidationAttribute
	{
		public object MaxValue { get; private set; }
		public MaxValueAttribute(int maxValue)
		{
			MaxValue = maxValue;
		}

		public MaxValueAttribute(decimal maxValue)
		{
			MaxValue = maxValue;
		}

		public MaxValueAttribute(DateTime maxValue)
		{
			MaxValue = maxValue;
		}

		public override bool IsValidate(object value)
		{
			return true;
		}
	}
}