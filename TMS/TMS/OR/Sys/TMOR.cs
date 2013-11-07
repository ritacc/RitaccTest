using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TM.OR.Sys
{
	public class TMOR
	{
		public int ID { get; set; }

		public string NO { get; set; }
		public string Name { get; set; }
		public string ISUse { get; set; }

		public TMOR()
		{
		}
		public TMOR(DataRow dr)
		{

		}
	}
}
