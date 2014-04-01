using System.Collections.Generic;
using System.Web.Mvc;

namespace App.Framework.Web
{
    /// <summary>
    /// List SelectListItem 的扩展
    /// </summary>
    public static class SelectListExtension
    {

        /// <summary>
        /// 填加默认项
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static List<SelectListItem> AddDefaultItem(this List<SelectListItem> selectList)
        {
            return selectList.AddDefaultItem(Resources.Messages.DropdownListDefaultItem);
        }

        /// <summary>
        /// 填加默认项
        /// </summary>
        /// <param name="selectList"></param>
        /// <param name="defaultTag">默认的标签</param>
        /// <returns></returns>
        public static List<SelectListItem> AddDefaultItem(this List<SelectListItem> selectList, string defaultTag)
        {
            selectList.Insert(0, new SelectListItem()
            {
                Text = defaultTag,
                Value = null
            });
            return selectList;
        }

        /// <summary>
        /// 填加默认项
        /// </summary>
        /// <param name="selectList"></param>
        /// <param name="defaultItem">默认选择项</param>
        /// <returns></returns>
        public static List<SelectListItem> AddDefaultItem(this List<SelectListItem> selectList, SelectListItem defaultItem)
        {
            selectList.Insert(0, defaultItem);
            defaultItem.Selected = true;
            return selectList;
        }


        #region selectlistwithtype

        /// <summary>
        /// 填加默认项
        /// </summary>
        /// <param name="selectList"></param>
        /// <returns></returns>
        public static List<ListItemWidthType> AddDefaultItem(this List<ListItemWidthType> selectList)
        {
            return selectList.AddDefaultItem(Resources.Messages.DropdownListDefaultItem);
        }

        /// <summary>
        /// 填加默认项
        /// </summary>
        /// <param name="selectList"></param>
        /// <param name="defaultTag">默认的标签</param>
        /// <returns></returns>
        public static List<ListItemWidthType> AddDefaultItem(this List<ListItemWidthType> selectList, string defaultTag)
        {
            selectList.Insert(0, new ListItemWidthType()
            {
                Text = defaultTag,
                Value = string.Empty,
				InterValue = string.Empty
            });
            return selectList;
        }

        /// <summary>
        /// 填加默认项
        /// </summary>
        /// <param name="selectList"></param>
        /// <param name="defaultItem">默认选择项</param>
        /// <returns></returns>
        public static List<ListItemWidthType> AddDefaultItem(this List<ListItemWidthType> selectList, ListItemWidthType defaultItem)
        {
            selectList.Insert(0, defaultItem);
            defaultItem.Selected = true;
            return selectList;
        }
        #endregion

		#region tag
		/// <summary>
		/// 填加默认项
		/// </summary>
		/// <param name="selectList"></param>
		/// <returns></returns>
		public static List<ListItemWidthTypeTag> AddDefaultItem(this List<ListItemWidthTypeTag> selectList)
		{
			return selectList.AddDefaultItem(Resources.Messages.DropdownListDefaultItem);
		}

		/// <summary>
		/// 填加默认项
		/// </summary>
		/// <param name="selectList"></param>
		/// <param name="defaultTag">默认的标签</param>
		/// <returns></returns>
		public static List<ListItemWidthTypeTag> AddDefaultItem(this List<ListItemWidthTypeTag> selectList, string defaultTag)
		{
			selectList.Insert(0, new ListItemWidthTypeTag()
			{
				Text = defaultTag,
				Value = null
			});
			return selectList;
		}

		/// <summary>
		/// 填加默认项
		/// </summary>
		/// <param name="selectList"></param>
		/// <param name="defaultItem">默认选择项</param>
		/// <returns></returns>
		public static List<ListItemWidthTypeTag> AddDefaultItem(this List<ListItemWidthTypeTag> selectList, ListItemWidthTypeTag defaultItem)
		{
			selectList.Insert(0, defaultItem);
			defaultItem.Selected = true;
			return selectList;
		}
		#endregion
	}
}
