//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：DictionaryExtension.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-25
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace App.Framework.Web
{
    /// <summary>
    /// Dictionary扩展类
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="setSelected"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList<TValue>(this Dictionary<Enum, TValue> dic,Predicate<Enum> setSelected)
        {
            return dic.ToSelectList(e => (int)e.GetValue(),setSelected);
        }

        /// <summary>
        /// 转换为List
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList<TValue>(this Dictionary<Enum, TValue> dic)
        {
            return dic.ToSelectList(e => (int)e.GetValue());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="changeValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList<TKey, TValue>(this Dictionary<TKey, TValue> dic, Func<TKey, object> changeValue)
        {
            return dic.ToSelectList(changeValue, null);
        }

        /// <summary>
        ///  转换为IEnumerable
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="dic">Dictionary</param>
        /// <param name="changeValue">改变值的委托</param>
        /// <param name="setSelected"></param>
        /// <returns>SelectListItem集合</returns>
        public static List<SelectListItem> ToSelectList<TKey, TValue>(this Dictionary<TKey, TValue> dic, Func<TKey, object> changeValue, Predicate<TKey> setSelected)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var item in dic)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = item.Value.ToString(),
                    Value = changeValue != null ? changeValue(item.Key).ToString() : item.Key.ToString(),
                    
                };
                selectListItem.Selected = setSelected != null ? setSelected(item.Key) : false;
                result.Add(selectListItem);
            }
            return result;
        }

        /// <summary>
        /// 获取字典中的值，如果不字典不含有值，则返回默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue)
        {
            if (dic.ContainsKey(key))
                return dic[key];
            return defaultValue;
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dic,TKey key)
        {
            if (dic.ContainsKey(key))
                return dic[key];
            return default(TValue);
        }
    }
}
