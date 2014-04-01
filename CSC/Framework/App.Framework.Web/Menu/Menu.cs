//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：Menu.cs
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
    /// 菜单模型
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Menu()
        {
            IsRoot = false;
            Href = "#";
            Description = "";
            CssClass = "";
            IsOpen = true;
            Visible = false;
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
                {
                    Current = this;
                    Module currentModule = MenuHelper.GetModuleByMenu(this);
                    if(currentModule != null)
                        currentModule.IsCurrent = true;
                }
                else if (Current == this)
                {
                    Current = null;
                }
            }
        }
        /// <summary>
        /// 当前项
        /// </summary>
        public static Menu Current { get; set; }

        /// <summary>
        /// 是否为根
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Href
        /// </summary>
        public string Href { get; set; }

        private List<Menu> _ChildNodes = new List<Menu>();
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Menu> ChildNodes { get { return _ChildNodes; } set { _ChildNodes = value; } }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public Menu ParentMenu { get; set; }

      
        private List<Permission> _Permissions = new List<Permission>();

        /// <summary>
        /// 包含的权限
        /// </summary>
        public List<Permission> Permissions
        {
            get
            {
                return _Permissions;
            }
            set
            {
                _Permissions = value;
                foreach (Permission p in _Permissions)
                    p.Parent = this;
            }
        }

        /// <summary>
        /// css
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MenuTitle { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 父级模块
        /// </summary>
        public Module Parent
        {
            get;
            internal set;
        }

       
        private bool _Visible = false;

        /// <summary>
        /// 可见
        /// </summary>
        public bool Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;
                if (value)
                {
                    if (Parent != null)
                        Parent.Visible = _Visible;
                    if (ParentMenu != null)
                        ParentMenu.Visible = _Visible;
                }
            }
        }

        /// <summary>
        /// 绑定父节点
        /// </summary>
        public void BindParent()
        {
            if (Permissions != null)
                foreach (Permission p in Permissions)
                    p.Parent = this;
            if (ChildNodes != null)
                foreach (Menu m in ChildNodes)
                {
                    m.ParentMenu = this;
                    m.BindParent();
                }
        }
    }
}
