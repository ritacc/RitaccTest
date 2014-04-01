using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Business
{
	public class ExportInfo
	{
		public string FileName
		{
			get;
			set;
		}

		public string FileNameDsc
		{
			get;
			set;
		}

		public string ResourceName
		{
			get;
			set;
		}

		public string FormatString
		{
			get;
			set;
		}

		public ExportInfo(string fileName, string fileNameDsc)
		{ 
			this.FileName = fileName;
			this.FileNameDsc = fileNameDsc;
			this.ResourceName = string.Empty;
			this.FormatString = string.Empty;
		}

		public ExportInfo(string fileName, string fileNameDsc, string resourceName)
		{
			this.FileName = fileName;
			this.FileNameDsc = fileNameDsc;
			this.ResourceName = resourceName;
			this.FormatString = string.Empty;
		}

		public ExportInfo(string fileName, string fileNameDsc, string resourceName, string formatString)
		{
			this.FileName = fileName;
			this.FileNameDsc = fileNameDsc;
			this.ResourceName = resourceName;
			this.FormatString = formatString;
		}
	}
}
