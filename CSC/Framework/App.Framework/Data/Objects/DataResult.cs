using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Data
{
	public class DataResult
	{
		public int ResultType { get; set; }
		public string ResultMessage { get; set; }

		public DataResult(int resultType, string resultMessage)
		{
			ResultType = resultType;
			ResultMessage = resultMessage;
		}
	}
}