using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Validation
{
	[Serializable]
	public class RequiredAttribute : ValidationAttribute
	{
		public override bool IsValidate(object value)
		{
			if(value == null)
			{
				return false;
			}
			else
			{
				Type valueType = value.GetType();
				switch(valueType.Name)
				{
					case "String":
						return (!string.IsNullOrWhiteSpace((string)value));
					case "Guid":
						return ((Guid)value != Guid.Empty);
					default:
						return true;

				}
			}
		}
	}
}
