using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Framework.Data;
using System.Threading;

namespace CSC.Business
{
	[DbCommand("spWriteLog")]
	public class WirteLog:DataCriteria
	{
		[DbParameter("LOG_TYPE")]
		public int LOG_TYPE { get; set; }
		[DbParameter("LOG_LEVEL")]
		public string LOG_LEVEL { get; set; }
		[DbParameter("LOGGER")]
		public string LOGGER { get; set; }
		[DbParameter("LOG_DATA")]
		public string LOG_DATA { get; set; }
		[DbParameter("EVENT_TYPE")]
		public int EVENT_TYPE { get; set; }
		[DbParameter("THREAD_ID")]
		public string THREAD_ID { get; set; }
		[DbParameter("USER_ID")]
		public long USER_ID { get {
			return App.Framework.Security.User.Current.UserId;
		} }
		[DbParameter("SYS_CODE")]
		public string SYS_CODE { get {
			return App.Framework.Security.SecurityPortal.ApplicationName;
		} }
		[DbParameter("SHOP_CODE")]
		public string SHOP_CODE { get {
			return App.Framework.Security.User.Current.ShopCode;
		} }
		[DbParameter("PROGRAM")]
		public string PROGRAM { get; set; }

		public void Save() {
			this.THREAD_ID = Thread.CurrentThread.ManagedThreadId.ToString();
			App.Framework.BusinessPortal.Execute(this);
		}
	}
}
