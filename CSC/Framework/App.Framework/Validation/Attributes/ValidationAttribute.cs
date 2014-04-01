using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Validation
{
	public abstract class ValidationAttribute : Attribute
	{
		public abstract bool IsValidate(object value);
	}
}