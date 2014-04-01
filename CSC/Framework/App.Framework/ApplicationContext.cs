using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace App.Framework
{
	[Serializable]
	public static class ApplicationContext
	{
		#region Constants
		private const string DEF_GLOBAL_CONTEXT = "App.Framework.GlobalContext";
		private const string DEF_CLIENT_CONTEXT = "App.Framework.ClientContext";
		#endregion

		#region User
		/// <summary>
		/// 设置和获取当前用户的身份
		/// </summary>
		public static IPrincipal User
		{
			get
			{
				return (HttpContext.Current != null) ? HttpContext.Current.User : Thread.CurrentPrincipal;
			}
			set
			{
				if(HttpContext.Current != null)
				{
					HttpContext.Current.User = value;
				}
				Thread.CurrentPrincipal = value;
			}
		}
		#endregion

		#region Client/Global Context

		public static HybridDictionary ClientContext
		{
			get
			{
				HybridDictionary context = GetClientContext();
				if(context == null)
				{
					context = new HybridDictionary();
					SetClientContext(context);
				}
				return context;
			}
		}

		public static HybridDictionary GlobalContext
		{
			get
			{
				HybridDictionary context = GetGlobalContext();
				if(context == null)
				{
					context = new HybridDictionary();
					SetGlobalContext(context);
				}
				return context;
			}
		}

		public static HybridDictionary GetClientContext()
		{
			if(HttpContext.Current == null)
			{
				LocalDataStoreSlot slot = Thread.GetNamedDataSlot(DEF_CLIENT_CONTEXT);
				return (HybridDictionary)Thread.GetData(slot);
			}
			else
			{
				return (HybridDictionary)HttpContext.Current.Items[DEF_CLIENT_CONTEXT];
			}
		}

		public static HybridDictionary GetGlobalContext()
		{
			if(HttpContext.Current == null)
			{
				LocalDataStoreSlot slot = Thread.GetNamedDataSlot(DEF_GLOBAL_CONTEXT);
				return (HybridDictionary)Thread.GetData(slot);
			}
			else
			{
				return (HybridDictionary)HttpContext.Current.Items[DEF_GLOBAL_CONTEXT];
			}
		}

		public static void SetClientContext(HybridDictionary clientContext)
		{
			if(HttpContext.Current == null)
			{
				LocalDataStoreSlot slot = Thread.GetNamedDataSlot(DEF_CLIENT_CONTEXT);
				Thread.SetData(slot, clientContext);
			}
			else
			{
				HttpContext.Current.Items[DEF_CLIENT_CONTEXT] = clientContext;
			}
		}

		public static void SetGlobalContext(HybridDictionary globalContext)
		{
			if(HttpContext.Current == null)
			{
				LocalDataStoreSlot slot = Thread.GetNamedDataSlot(DEF_GLOBAL_CONTEXT);
				Thread.SetData(slot, globalContext);
			}
			else
			{
				HttpContext.Current.Items[DEF_GLOBAL_CONTEXT] = globalContext;
			}
		}

		public static void SetContext(HybridDictionary clientContext, HybridDictionary globalContext)
		{
			SetClientContext(clientContext);
			SetGlobalContext(globalContext);
		}

		public static void Clear()
		{
			SetContext(null, null);
		}

		#endregion

		#region Config Settings


		#endregion
	}
}
