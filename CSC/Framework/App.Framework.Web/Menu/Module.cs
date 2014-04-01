//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：Moudle.cs
//文件功能：
//
//创建标识：鲜红 || 2011-03-31
//
//修改标识：
//修改描述：
//**********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web.Menu
{
    /// <summary>
    /// 模块
    /// </summary>
    public class Module
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Module()
        {
            Href = "#";
            if (Current == null) Current = this;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="moudleName"></param>
        /// <param name="cssClass"></param>
        public Module(string moudleName, string cssClass)
            : this()
        {
            MoudleCaption = moudleName;
            CssClass = cssClass;
        }

        /// <summary>
        /// 当前模块
        /// </summary>
        public static Module Current { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string MoudleCaption { get; set; }       
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        private List<Menu> _Menus = new List<Menu>();
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<Menu> Menus
        {
            get
            {
                return _Menus;
            }
            set
            {
                _Menus = value;
                foreach (Menu m in _Menus)
                    m.Parent = this;
            }
        }
        /// <summary>
        /// Href
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// CSS
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        /// 可见
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是当前项
        /// </summary>
        public bool IsCurrent
        {
            get
            {
                return Current == this;
            }
            set
            {
                if (value)
                    Current = this;
                else if (Current == this)
                    Current = null;
            }
        }
        /// <summary>
        /// 绑定父级
        /// </summary>
        public void BindParent()
        {
            if (_Menus != null)
                foreach (Menu m in _Menus)
                {
                    m.Parent = this;
                    m.BindParent();
                }
        }
    }
}

