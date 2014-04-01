using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 如果对象不为空则返回对象的ToString方法，为空则返回string.Empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStringAllowNull(this object obj)
        {
            if (obj == null)
                return string.Empty;
            return obj.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T BlockIsNull<T>(this T t) where T:class
        {
            if (t == null)
                return Activator.CreateInstance<T>();
            return t;
        }
 
    }
}
