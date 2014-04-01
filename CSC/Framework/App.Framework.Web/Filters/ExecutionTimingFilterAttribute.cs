using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace App.Framework.Web.Filters
{
    /// <summary>
    /// Action执行时间统计
    /// </summary>
    public class ExecutionTimingFilterAttribute:ActionFilterAttribute 
    {
        private bool timingEnabled = bool.Parse(ConfigurationManager.AppSettings["EnabledStatisticalTime"]);
        private Stopwatch timer;

        /// <summary>
        /// Action开始执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (this.timingEnabled)
            {
                this.timer = new Stopwatch();
                this.timer.Start();
            }
        }

        /// <summary>
        /// Action执行完成
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (this.timingEnabled)
            {
                this.timer.Stop();
                Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Action execution time: {0}ms", this.timer.ElapsedMilliseconds));
                if (filterContext.Result is ViewResult)
                {
                    ((ViewResult)filterContext.Result).ViewData["ExecTime"] = this.timer.ElapsedMilliseconds.ToString("N0") + "(ms)";
                }
            }
        }
    } 
  
}
