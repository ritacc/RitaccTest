using System;
using System.Configuration;
using System.Xml;

namespace App.Framework.Configuration
{
	/// <summary>
	/// 用于读取安全配置信息
	/// </summary>
	/// <remarks>
	/// 从Web.config或App.config中获取值
	/// </remarks>
	/// <version>2.00</version>
	/// <create>2006-12-05 Phoenix</create>
	/// <modify>2008-04-12 Phoenix</modify>
	internal class SecurityConfigHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler

		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
		{
			SecurityConfig config = new SecurityConfig((SecurityConfig)parent);
			config.LoadValuesFromConfigurationXml(section);
			return config;
		}

		#endregion
	}

	/// <summary>
	/// 存放安全配置信息
	/// </summary>
	/// <remarks>
	/// 从Web.config或App.config中获取值
	/// </remarks>
	/// <version>2.00</version>
	/// <create>2006-12-05 Phoenix</create>
	/// <modify>2008-04-12 Phoenix</modify>
	internal class SecurityConfig
	{
		#region Constants
		/// <summary>
		/// 需要读取的config文件中的节
		/// </summary>
		private const string DEF_CONFIG = "App.Framework/Security";
		#endregion

		#region Properties
		/// <summary>
		/// 获取应用程序的名称。
		/// </summary>
		public string ApplicationName { get; private set; }
		/// <summary>
		/// 获取业务单位的名称。
		/// </summary>
		public string BusinessUnit { get; private set; }
		/// <summary>
		/// 获取锁定用户前允许的无效密码尝试次数。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public uint MaxInvalidPasswordAttempts { get; private set; }
		/// <summary>
		/// 密码有效天数
		/// </summary>
		/// /// <remarks>0 = 永远有效</remarks>
		public uint PasswordEffectiveDays { get; private set; }
		/// <summary>
		/// 获取密码所要求的最小长度。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public uint MinRequiredPasswordLength { get; private set; }
		/// <summary>
		/// 获取有效密码中必须包含的最少特殊字符数。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public uint MinNonAlphaChar { get; private set; }
		/// <summary>
		/// 获取有效密码中必须包含的最少字符数。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public uint MinAlphaChar { get; private set; }
		/// <summary>
		/// 获取有效密码中必须包含的最少数字长度。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public uint MinNumChar { get; private set; }
		/// <summary>
		/// 最近修改的几次密码不能重用。
		/// </summary>
		/// <remarks>0 = 无限制</remarks>
		public uint PasswordReusePreventionCount { get; private set; }
		/// <summary>
		/// 初次登录改密码
		/// </summary>
		/// <remarks>false = 不改密码</remarks>
		public bool InitialLogonChangePassword { get; private set; }
		
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
		/// 从XML中读取配置信息
		/// </summary>
		internal void LoadValuesFromConfigurationXml(XmlNode node)
		{
			MaxInvalidPasswordAttempts = 0;
			PasswordEffectiveDays = 0;
			MinRequiredPasswordLength = 0;
			MinNonAlphaChar = 0;
			MinAlphaChar = 0;
			MinNumChar = 0;
			PasswordReusePreventionCount = 0;
			InitialLogonChangePassword = false;

			foreach(XmlNode attribute in node.Attributes)
			{
				if(string.Equals(attribute.Name, "ApplicationName", StringComparison.OrdinalIgnoreCase))
				{
					ApplicationName = (string)attribute.Value;
					continue;
				}
				if(string.Equals(attribute.Name, "BusinessUnit", StringComparison.OrdinalIgnoreCase))
				{
					BusinessUnit = (string)attribute.Value;
					continue;
				}
				if(string.Equals(attribute.Name, "MaxInvalidPasswordAttempts", StringComparison.OrdinalIgnoreCase))
				{
					MaxInvalidPasswordAttempts = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if(string.Equals(attribute.Name, "PasswordEffectiveDays", StringComparison.OrdinalIgnoreCase))
				{
					PasswordEffectiveDays = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if(string.Equals(attribute.Name, "MinRequiredPasswordLength", StringComparison.OrdinalIgnoreCase))
				{
					MinRequiredPasswordLength = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if (string.Equals(attribute.Name, "MinNonAlphaChar", StringComparison.OrdinalIgnoreCase))
				{
					MinNonAlphaChar = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if (string.Equals(attribute.Name, "MinAlphaChar", StringComparison.OrdinalIgnoreCase))
				{
					MinAlphaChar = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if (string.Equals(attribute.Name, "MinNumChar", StringComparison.OrdinalIgnoreCase))
				{
					MinNumChar = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if (string.Equals(attribute.Name, "PasswordReusePreventionCount", StringComparison.OrdinalIgnoreCase))
				{
					PasswordReusePreventionCount = TypeConvertor.ToUInt32(attribute.Value, 0);
					continue;
				}
				if (string.Equals(attribute.Name, "InitialLogonChangePassword", StringComparison.OrdinalIgnoreCase))
				{
					InitialLogonChangePassword = TypeConvertor.ToBool(attribute.Value, false);
					continue;
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// 初始化配置信息
		/// </summary>
		internal SecurityConfig(SecurityConfig parent)
		{
			if(parent != null)
			{
				ApplicationName = parent.ApplicationName;
				BusinessUnit = parent.BusinessUnit;
				MaxInvalidPasswordAttempts = parent.MaxInvalidPasswordAttempts;
				PasswordEffectiveDays = parent.PasswordEffectiveDays;
				MinRequiredPasswordLength = parent.MinRequiredPasswordLength;
				MinNonAlphaChar = parent.MinNonAlphaChar;
				MinAlphaChar = parent.MinAlphaChar;
				MinNumChar = parent.MinNumChar;
				PasswordReusePreventionCount = parent.PasswordReusePreventionCount;
				InitialLogonChangePassword = parent.InitialLogonChangePassword;
			}
		}
		#endregion
	}
}