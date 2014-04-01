using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web
{
    /// <summary>
    /// int类型扩展
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// 将可空类型转换为不可空，并且去掉为null的元素
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this int?[] array)
        {
            var result = array.ToList().Where(i => i !=null && i.HasValue);
            return result.ToList().ToList(m => m.Value).ToArray();
        }
    }
}
