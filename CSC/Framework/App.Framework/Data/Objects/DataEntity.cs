using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using App.Framework.Reflection;
using App.Framework.Export;

namespace App.Framework.Data
{
	[Serializable]
	public abstract class DataEntity : IDataObject
	{
		internal static readonly Hashtable exportCache = Hashtable.Synchronized(new Hashtable());

		public abstract DataCriteria CreateSaveCriteria();
		public abstract DataCriteria CreateDeleteCriteria();

		#region IDataObject

		public BusinessResult Save()
		{
			BusinessResult result = new BusinessResult();
			DataCriteria criteria = this.CreateSaveCriteria();
			return (BusinessResult)DataPortal.ExecuteNonQuery(criteria);
		}

		public BusinessResult Save(IDbTransaction trans)
		{
			BusinessResult result = new BusinessResult();
			DataCriteria criteria = this.CreateSaveCriteria();
			return (BusinessResult)DataPortal.ExecuteNonQuery(criteria, trans);
		}

		public BusinessResult Delete()
		{
			BusinessResult result = new BusinessResult();
			DataCriteria criteria = this.CreateDeleteCriteria();
			return (BusinessResult)DataPortal.ExecuteNonQuery(criteria);
		}

		public BusinessResult Delete(IDbTransaction trans)
		{
			BusinessResult result = new BusinessResult();
			DataCriteria criteria = this.CreateDeleteCriteria();
			return (BusinessResult)DataPortal.ExecuteNonQuery(criteria, trans);
		}

		#endregion

		//public virtual string JsonData
		//{
		//    get
		//    {
		//        DataContractJsonSerializer ds = new DataContractJsonSerializer(this.GetType());
		//        using (MemoryStream ms = new MemoryStream())
		//        {
		//            ds.WriteObject(ms, this);
		//            return Encoding.UTF8.GetString(ms.ToArray());
		//        }
		//    }
		//    set
		//    {
		//        //将Json字符串转化成对象
		//        DataContractJsonSerializer outDs = new DataContractJsonSerializer(this.GetType());
		//        DataEntity outEntity = null;
		//        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
		//        {
		//            outEntity = outDs.ReadObject(ms) as DataEntity;
		//        }

		//        if (outEntity != null)
		//        {
		//            foreach (PropertyInfo property in this.GetType().GetProperties())
		//            {
		//                if (property.Name != "JsonData")
		//                {
		//                    property.SetValue(this, outEntity.GetType().GetProperty(property.Name).GetValue(outEntity, null), null);
		//                }
		//            }
		//        }
		//    }
		//}

		public virtual string ToCSV(List<string> fields)
		{
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();

			foreach(string fieldName in fields)
			{
				ExportAttribute exportInfo = GetExportFields()[fieldName] as ExportAttribute;
				if(exportInfo == null)
				{
					throw new NotSupportedException(string.Format("{0} 上未发现 ExportAttribute 定义", fieldName));
				}

				object propertyValue = exportInfo.BoundProperty.GetValue(this, null);
				switch(exportInfo.BoundProperty.PropertyType.Name)
				{
					case "String":
						if(propertyValue != null)
						{
							stringBuilder.Append("=\"" + ((string)propertyValue).Trim() + "\"\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
					case "DateTime":
						if(propertyValue != null)
						{
							stringBuilder.Append(((DateTime)propertyValue).ToString(exportInfo.FormatString) + "\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
					case "Boolean":
						if(propertyValue != null)
						{
							stringBuilder.Append(((bool)propertyValue) ? "YES\t" : "NO\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
					//Added by Jason in 20120320 for nullable data type
					case "Nullable`1":
						Type underlyingType = exportInfo.BoundProperty.PropertyType.GetGenericArguments()[0];
						switch (underlyingType.Name)
						{
							case "String":
								if (propertyValue != null)
								{
									stringBuilder.Append("=\"" + (string)propertyValue + "\"\t");
								}
								else
								{
									stringBuilder.Append("\t");
								}
								break;
							case "DateTime":
								if (propertyValue != null)
								{
									stringBuilder.Append(((DateTime)propertyValue).ToString(exportInfo.FormatString) + "\t");
								}
								else
								{
									stringBuilder.Append("\t");
								}
								break;
							case "Boolean":
								if (propertyValue != null)
								{
									stringBuilder.Append(((bool)propertyValue) ? "YES\t" : "NO\t");
								}
								else
								{
									stringBuilder.Append("\t");
								}
								break;
							default:
								if(propertyValue != null)
								{
									stringBuilder.Append(propertyValue.ToString() + "\t");
								}
								else
								{
									stringBuilder.Append("\t");
								}
								break;
						}
						break;
					//End added by Jason in 20120320 for nullable data type
					default:
						if(propertyValue != null)
						{
							stringBuilder.Append(propertyValue.ToString() + "\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
				}
			}

			result = stringBuilder.ToString();

			if(!string.IsNullOrWhiteSpace(result))
			{
				result = result.Substring(0, result.Length - 1);
			}
			return result;
		}

		public virtual string ToCSV(List<string> fields, Type resourceType)
		{
			return ToCSV(fields);
		}

		public virtual string TitleToCSV(List<string> fields, Type resourceType)
		{
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			foreach(string fieldName in fields)
			{
				ExportAttribute exportInfo = GetExportFields()[fieldName] as ExportAttribute;
				if(exportInfo == null)
				{
					throw new NotSupportedException(string.Format("{0} 上未发现 ExportAttribute 定义", fieldName));
				}

				string titleName = string.Empty;
				try
				{
					titleName = (string)resourceType.GetProperty(exportInfo.ResourceName).GetValue(null, null);
				}
				catch
				{
					titleName = string.Empty;
				}
				if(string.IsNullOrWhiteSpace(titleName))
				{
					stringBuilder.Append(exportInfo.Name + "\t");
					continue;
				}
				stringBuilder.Append(titleName + "\t");
			}
			result = stringBuilder.ToString();

			if(!string.IsNullOrWhiteSpace(result))
			{
				result = result.Substring(0, result.Length - 1);
			}
			return result;
		}

		#region 使用逗号分割（add by xj）
		public virtual string ToCSVWithTab(List<string> fields)
		{
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();

			foreach (string fieldName in fields)
			{
				ExportAttribute exportInfo = GetExportFields()[fieldName] as ExportAttribute;
				if (exportInfo == null)
				{
					throw new NotSupportedException(string.Format("{0} 上未发现 ExportAttribute 定义", fieldName));
				}

				object propertyValue = exportInfo.BoundProperty.GetValue(this, null);
				switch (exportInfo.BoundProperty.PropertyType.Name)
				{
					case "String":
						if (propertyValue != null)
						{
							stringBuilder.Append((string)propertyValue + "\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
					case "DateTime":
						if (propertyValue != null)
						{
							stringBuilder.Append(((DateTime)propertyValue).ToString(exportInfo.FormatString) + "\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
					case "Boolean":
						if (propertyValue != null)
						{
							stringBuilder.Append(((bool)propertyValue) ? "Yes\t" : "No\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
					default:
						if (propertyValue != null)
						{
							stringBuilder.Append(propertyValue.ToString() + "\t");
						}
						else
						{
							stringBuilder.Append("\t");
						}
						break;
				}
			}

			result = stringBuilder.ToString();

			if (!string.IsNullOrWhiteSpace(result))
			{
				result = result.Substring(0, result.Length - 1);
			}
			return result;
		}

		public virtual string TitleToCSVWithTab(List<string> fields, Type resourceType)
		{
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string fieldName in fields)
			{
				ExportAttribute exportInfo = GetExportFields()[fieldName] as ExportAttribute;
				if (exportInfo == null)
				{
					throw new NotSupportedException(string.Format("{0} 上未发现 ExportAttribute 定义", fieldName));
				}

				string titleName = string.Empty;
				try
				{
					titleName = (string)resourceType.GetProperty(exportInfo.ResourceName).GetValue(null, null);
				}
				catch
				{
					titleName = string.Empty;
				}
				if (string.IsNullOrWhiteSpace(titleName))
				{
					stringBuilder.Append(exportInfo.Name + "\t");
					continue;
				}
				stringBuilder.Append(titleName + "\t");
			}
			result = stringBuilder.ToString();

			if (!string.IsNullOrWhiteSpace(result))
			{
				result = result.Substring(0, result.Length - 1);
			}
			return result;
		}
		#endregion

		public ExportAttribute GetExportField(string fieldName)
		{
			ExportAttribute result = GetExportFields()[fieldName] as ExportAttribute;
			foreach(ExportAttribute field in GetExportFields())
			{
				if(field.Name == fieldName)
				{
					return field;
				}
			}
			return null;
		}

		public Hashtable GetExportFields()
		{
			Type entityType = this.GetType();
			Hashtable result = exportCache[entityType] as Hashtable;
			if(result == null)
			{
				lock(exportCache.SyncRoot)
				{
					result = exportCache[this.GetType()] as Hashtable;
					if(result != null)
					{
						return result;
					}

					result = new Hashtable();
					foreach(PropertyInfo property in this.GetType().GetProperties())
					{
						ExportAttribute exportInfo = ReflectionHelper.GetAttribute<ExportAttribute>(property);
						if(exportInfo == null)
						{
							continue;
						}
						exportInfo.BoundProperty = property;
						result.Add(exportInfo.Name, exportInfo);
					}
					exportCache.Add(entityType, result);
				}
			}
			return result;
		}
	}

	public static class Json
	{
		private const string DEF_STRING = "\"{1}\"";
		private const string DEF_VALUE_TYPE = "{1}";
		private const string DEF_DATETIME = "{1}";

		public static string ToJson(string value)
		{
			string template = "\"{1}\"";
			return string.Format(template, value);
		}

		public static string ToString(string json)
		{
			return json.Replace("\"", "");
		}
	}
}