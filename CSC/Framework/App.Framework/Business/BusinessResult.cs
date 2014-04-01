using System;
using System.Collections.Generic;
using System.Data;
using App.Framework.Data;

namespace App.Framework
{
	[Serializable]
	public class BusinessResult
	{
		public int ResultType { get; set; }
		public string ResultMessage { get; set; }

		public static implicit operator BusinessResult(DataResult value)
		{
			return new BusinessResult(value.ResultType, value.ResultMessage);
		}

		public BusinessResult()
		{
			ResultType = 0;
			ResultMessage = "Succeed";
		}

		public BusinessResult(int resultType, string resultMessage)
		{
			ResultType = resultType;
			ResultMessage = resultMessage;
		}
	}
}