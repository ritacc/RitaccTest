using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework
{
	[Serializable]
	public class BusinessException : ApplicationException
	{
		public int ExceptionCode { get; protected set; }

		public BusinessException()
		{
		}

		public BusinessException(int exceptionCode)
		{
		}

		public BusinessException(int exceptionCode, Exception inner)
		{

		}
	}
}