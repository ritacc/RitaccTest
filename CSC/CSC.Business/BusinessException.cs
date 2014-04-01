using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC.Business
{
	[Serializable]
	public class BusinessException : Exception
	{
		public BusinessException(string msg)
			: base(msg)
		{

		}
	}
}
