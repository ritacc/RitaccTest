using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data;

namespace App.Framework.Data
{
	[Serializable]
	public abstract class DataCriteria
	{
		/// <summary>
		/// 获取或设置在终止执行命令的尝试并生成错误之前的等待时间。
		/// </summary>
		/// <result>
		/// 等待命令执行的时间（以秒为单位）。小于或等于0时，为数据库默认值。
		/// </result>
		public virtual int Timeout { get{ return DataPortal.GetConfigData().Timeout; }}

		[DbParameter("ResultType", ParameterDirection.Output)]
		public int ResultType{ get; set; }

		[DbParameter("ResultMessage", ParameterDirection.Output)]
		public string ResultMessage { get; set; }
	}
}