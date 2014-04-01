//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：Permissions.cs
//文件功能：
//
//创建标识：鲜红 || 2011-04-06
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
    /// 权限
    /// </summary>
    public class Permission
    {
       
        private bool _Visible = false;

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 低级菜单
        /// </summary>
        public Menu Parent { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        
        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                _Visible = value;
                if (Parent != null && _Visible)
                    Parent.Visible = _Visible;
            }
        }
    }
}
