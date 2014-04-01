using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Validation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class MinLengthAttribute : ValidationAttribute
	{
		public int MinLength { get; private set; }
		public MinLengthAttribute(int minLength)
		{
			MinLength = minLength;
		}

		public override bool IsValidate(object value)
		{
			int valueLength = value.ToString().Length;
			return valueLength >= MinLength;
		}
	}
}