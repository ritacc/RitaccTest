using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace App.Framework.Configuration
{
	/// <summary>
	/// 用于读取数据访问对象配置信息
	/// </summary>
	/// <remarks>
	/// 从Web.config或App.config中获取值
	/// </remarks>
	/// <version>2.00</version>
	/// <create>2006-12-05 Phoenix</create>
	/// <modify>2008-04-12 Phoenix</modify>
	internal class DataConfigHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler 成员

		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
		{
			DataConfig config = new DataConfig((DataConfig)parent);
			config.LoadValuesFromConfigurationXml(section);
			return config;
		}

		#endregion
	}

	/// <summary>
	/// 存放数据访问对象的配置信息
	/// </summary>
	/// <remarks>
	/// 从Web.config或App.config中获取值
	/// </remarks>
	/// <version>2.00</version>
	/// <create>2006-12-05 Phoenix</create>
	/// <modify>2008-04-12 Phoenix</modify>
	internal class DataConfig
	{
		#region Constants
		/// <summary>
		/// 需要读取的config文件中的节
		/// </summary>
		private const string DEF_CONFIG = "App.Framework/Data";
		#endregion

		#region Properties

		private string _type = "App.Framework.Data.SqlProvider, App.Framework.Data";
		/// <summary>
		/// 数据访问对象的类型
		/// </summary>
		public string Type
		{
			get { return _type; }
		}

		string _connectionStringName;
		/// <summary>
		/// 数据访问对象使用的数据库连接字符串的名称
		/// </summary>
		/// <remarks>
		/// 存放的是数据库连接字符串的名称，而不是数据库连接字符串本身
		/// </remarks>
		public string ConnectionStringName
		{
			get { return _connectionStringName; }
		}

		private int _timeout = 0;
		/// <summary>
		/// 数据访问命令的默认超时时间，为 0 时为数据库默认时间(以秒为单位)
		/// </summary>
		public int Timeout
		{
			get { return _timeout; }
		}
		#endregion

		#region Methods
		/// <summary>
		/// 获取要读取的config文件中的节
		/// </summary>
		public static string GetConfigSection()
		{
			return DEF_CONFIG;
		}
		/// <summary>
		/// 数据访问对象使用的数据库连接字符串
		/// </summary>
		public string GetConnectionString()
		{
			return GetConnectionString(_connectionStringName);
		}
		/// <summary>
		/// 数据访问对象使用的数据库连接字符串
		/// </summary>
		public static string GetConnectionString(string connectionStringName)
		{
			return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
		}
		/// <summary>
		/// 从XML中读取配置信息
		/// </summary>
		internal void LoadValuesFromConfigurationXml(XmlNode node)
		{
			foreach(XmlNode attribute in node.Attributes)
			{
				if(string.Equals(attribute.Name, "connectionStringName", StringComparison.OrdinalIgnoreCase))
				{
					_connectionStringName = attribute.Value;
					continue;
				}
				if(string.Equals(attribute.Name, "type", StringComparison.OrdinalIgnoreCase))
				{
					_type = attribute.Value;
					continue;
				}
				if (string.Equals(attribute.Name, "timeout", StringComparison.OrdinalIgnoreCase))
				{
					if (!int.TryParse(attribute.Value, out _timeout))
					{
						_timeout = 0;
					}
					continue;
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// 初始化配置信息
		/// </summary>
		internal DataConfig(DataConfig parent)
		{
			if(parent != null)
			{
				_type = parent.Type;
				_connectionStringName = parent.ConnectionStringName;
				_timeout = parent.Timeout;
			}
		}
		#endregion
	}
}
