using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AnalysisJson
{
	public class BuildHelper
	{
		public static void Save(string path, string fileName)
		{
			fileName = Regex.Replace(fileName, "[^A-Za-z0-9_.]", "_");
			//FileStream stream = null;
			//StreamWriter writer = null;
			try
			{
				//若没有存在指定的目录,则创建之.
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				//若文件已存在,则删除之.
				if (File.Exists(path + @"\" + fileName))
				{
					File.Delete(path + @"\" + fileName);
				}

				 
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{ 
			}
		}
	}
}
