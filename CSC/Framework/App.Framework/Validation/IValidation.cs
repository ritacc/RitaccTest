using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace App.Framework.Validation
{
	public delegate bool CustomValidateDelegate();

	public interface IValidation
	{
		bool Validate();
	}
}