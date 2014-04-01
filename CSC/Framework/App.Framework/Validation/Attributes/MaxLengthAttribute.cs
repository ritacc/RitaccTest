using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class MaxLengthAttribute : ValidationAttribute
	{
		public int MaxLength { get; private set; }
		public MaxLengthAttribute(int maxLength)
		{
			MaxLength = maxLength;
		}

		public override bool IsValidate(object value)
		{
			int valueLength = value.ToString().Length;
			return valueLength <= MaxLength;
		}
	}
}