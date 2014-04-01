using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web
{
    /// <summary>
    /// 可空类型扩展
    /// </summary>
    public static class NullableExtension
    {
        /// <summary>
        /// 如果值为空则返回T类型的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Nullable<T> GetDefaultValueIfNull<T>(this Nullable<T> t) where T : struct
        {
            if (t == null)
                return default(T);
            return t;
        }
    }
}
