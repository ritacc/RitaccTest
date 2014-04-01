using System;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Threading;

namespace App.Framework
{
	[Serializable]
	public class BusinessContext
	{
		private IPrincipal m_Principal;
		internal IPrincipal Principal
		{
			get { return m_Principal; }
		}

		private string m_Culture;
		internal string Culture
		{
			get { return m_Culture; }
		}

		private string m_UICulture;
		internal string UICulture
		{
			get { return m_UICulture; }
		}

		private HybridDictionary m_ClientContext;
		internal HybridDictionary ClientContext
		{
			get { return m_ClientContext; }
		}

		private HybridDictionary m_GlobalContext;
		internal HybridDictionary GlobalContext
		{
			get { return m_GlobalContext; }
		}

		//public BusinessContext Current()
		//{
		//    BusinessContext context = new BusinessContext();
		//}

		protected BusinessContext(IPrincipal principal)
		{
			m_Principal = principal;
			m_Culture = Thread.CurrentThread.CurrentCulture.Name;
			m_UICulture = Thread.CurrentThread.CurrentUICulture.Name;
			m_ClientContext = ApplicationContext.GetClientContext();
			m_GlobalContext = ApplicationContext.GetGlobalContext();
		}
	}
}