using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSC.Business;
using System.Web.Mvc;
using App.Framework;
using App.Framework.Web;
using App.Framework.Data;
using CSC.Resources;
using System.Configuration;
using App.Framework.Security;


namespace CSC
{
    public static class QuickInvoke
    {

       

       

        public static ActionResult Execute(Func<ActionResult> call, Func<Exception, ActionResult> exceptionCall)
        {
            try
            {
                return call();
            }
            catch (Exception ex)
            {

                return exceptionCall(ex);
            }

        }

        public static void DbOpreation(Func<DataCriteria> action)
        {
            var result = action();
            if (result != null && result.ResultType != 0)
            {
                throw new DbException(result.GetMessage());
            }
        }

        public static void DbTransaction(Func<BusinessResult> action)
        {
            var trans = BusinessPortal.BeginTransaction();
            try
            {
                DbOpreation(action);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }


		public static void InovkeWithTryCatch(Action SuccessAction,Action<string> faultAction) {
			try
			{
				SuccessAction();
			}
			catch (Exception ex) {
				faultAction(ex.Message);			
			}	
		}


        public static void DbOpreation(Func<BusinessResult> action)
        {
            var result = action();
            if (result.ResultType < 0)
            {
                throw new DbException(result.GetMessage());
            }
        }

        public static void DbOpreation2(Func<BusinessResult> action)
        {
            var result = action();
            if (result.ResultType < 0)
            {
                throw new DbException(result.ResultMessage);
            }
        }


        public static List<SelectListItem> GetWeekList()
        {
            return new List<SelectListItem>() { 
							{ new SelectListItem() { Text = "日",Value="SUN" } }, 
							{ new SelectListItem() { Text = "一",Value="MON" } }, 
							{ new SelectListItem() { Text = "二",Value="TUE"} },
							{ new SelectListItem() { Text = "三",Value="WED"} },
							{ new SelectListItem() { Text = "四",Value="THU"} },
							{ new SelectListItem() { Text = "五",Value="FRI"} },
							{ new SelectListItem() { Text = "六",Value="SAT" } }
						};
        }

        /// <summary>
        /// 获取当前用户是否具有某个权限
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool GetCurrentUserHasPermission(EnumPermission p)
        {
            return GetCurrentUserHasPermission(p.ToString());
        }


        public static bool GetCurrentUserHasPermission(string key)
        {
            return UserExtension.GetCurrentUserHasPermissionsPoints().BlockIsNull().Any(m => m.Key == key);
        }

        public static bool GetCurrentUserHasPermission(int id)
        {
            return UserExtension.GetCurrentUserHasPermissionsPoints().BlockIsNull().Any(m => m.PermissionsId == id.ToString());
        }

		/// <summary>
		/// 获取系统参数值
		/// </summary>
		/// <param name="code">参数CODE</param>
		/// <returns></returns>
		public static string GetParameterValue(string code)
        {
            var result = BusinessPortal.Load<ParameterResult>(new GetParameter()
            {
                CODE = code
            });
            return result.Result;
        }
		
		public static DateTime GetTransactionDate()
		{
			var result = BusinessPortal.Load<TransactionDateResult>(new GetTransactionDate()
			{
				
			});
			return result.Result;
		}

		/// <summary>
		/// 获取上传文件夹路径 临时文件，上传后可以删除
		/// </summary>
		/// <returns></returns>
		public static string GetUploadTempPath()
		{
			string mpath = ConfigurationManager.AppSettings["UploadTempPath"];
			if (!mpath.EndsWith("\\"))
				mpath += "\\";
			return mpath;
		}

		/// <summary>
		/// 获取报表文件存放路径
		/// </summary>
		/// <returns></returns>
		public static string GetReportFilePath()
		{
			return ConfigurationManager.AppSettings["ReportFilePath"];
		}

		public static bool EnableScheduleOptions()
		{
			return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableScheduleOptions"]);
		}


		


        /// <summary>
        /// BonusUploadPath
        /// </summary>
        /// <returns></returns>
        public static string GetBonusTemplate()
        {
            return ConfigurationManager.AppSettings["BonusTemplate"];
        }

		public static long? GetDefultGodown()
		{
			ParameterResultLong result = BusinessPortal.Load<ParameterResultLong>(new GetDefaultGodownByWH()
			{
				WH_CODE = App.Framework.Security.User.Current.ShopCode
			});
			return result.Result;
		}

		public static long? GetDefultGodown(string mWH_CODE)
		{
			ParameterResultLong result = BusinessPortal.Load<ParameterResultLong>(new GetDefaultGodownByWH()
			{
				WH_CODE = mWH_CODE
			});
			return result.Result;
		}

		/// <summary>
		/// 加密文本
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string Encrypt(string content) 
		{
			return SecurityPortal.EncryptPwd(UserExtension.Instance, content);
		}

    }


}
