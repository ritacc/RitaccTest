//**********************************************************
//Copyright(C)2011 By AIS版权所有。
//
//文件名：MenuGenerate.cs
//文件功能：能够根据分页按钮生成HTML
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
using System.Web.Mvc;

namespace App.Framework.Web.Menu
{

    /// <summary>
    /// 菜单生成
    /// </summary>
    public class MenuGenerate : IGenerateMenu
    {
        private const string MoudleFormatString = "<a href='{0}' title='{1}' class='{2}'><s></s>{3}</a>";
        private const string MenuCurrentMoudleFormatString = @"<div class='subhd'>
                        <h3 class='subhdtit'>
                            <span>{0}</span><a href='javascript:void(""{1}"");'  class='pullicon' title='点击收起'>点击收起</a></h3>
                        </div>";
        private const string RootMenuOpenFormatString = "<li><h3 class='open{4}'> <a href='{0}' class='{1}' title='{2}'><s></s>{3}</a></h3></li>";
        private const string RootMenuCloseFormatString = "<li><h3 class='close{4}'> <a href='{0}' class='{1}' title='{2}'><s></s>{3}</a></h3></li>";

        private const string ChildMenuFormatString = @"<li><a href='{0}'>{1}</a></li>";
        private const string CurrentMenuFormatString = @"<li><a href='{0}' class='{1}'>{2}</a></li>";
        private const string WarpChildTag = "ul";
        private const string WarpParentTag = "ul";
        private const string WarpParentCssClass = "cat";
        private const string WarpChildTagCssClass = "catc";
        private const string CurrentMenuCss = "on";
        private const string CurrentModuleCss = "on";
        private const string Identy = "\r\n";

        /// <summary>
        /// 生成模块的HTML代码
        /// </summary>
        /// <param name="moudles"></param>
        /// <returns></returns>
        public string GenerateMoudle(IEnumerable<Module> moudles)
        {
            if (moudles == null) return string.Empty;
            var sb = new StringBuilder();
            foreach (Module m in moudles)
            {
                if (!m.Visible) continue;
                sb.AppendLine(string.Format(MoudleFormatString, m.Href, m.Description, m.CssClass + (m.IsCurrent ? " " + CurrentModuleCss : ""), m.MoudleCaption));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成菜单的HTML代码
        /// </summary>
        /// <param name="moudle"></param>
        /// <returns></returns>
        public string GenerateMenu(Module moudle)
        {
            if (moudle == null) throw new ArgumentNullException("moudle");
            var sb = new StringBuilder();
            IEnumerable<Menu> menus = moudle.Menus;
            sb.AppendLine(string.Format(MenuCurrentMoudleFormatString, moudle.MoudleCaption, moudle.Href));
            var tagBuildertmp = new TagBuilder(WarpParentTag);
            if (!string.IsNullOrEmpty(WarpParentCssClass))
                tagBuildertmp.AddCssClass(WarpParentCssClass);
            if (menus != null)
                foreach (Menu m in menus)
                {
                    if (!m.Visible) continue;
                    if (m.IsOpen && m.ChildNodes.Count > 0)
                        tagBuildertmp.InnerHtml += string.Format(RootMenuOpenFormatString, m.Href, m.CssClass,m.MenuTitle, m.MenuTitle, m.IsCurrent ? " on" : "") + Identy;
                    else
                        tagBuildertmp.InnerHtml += string.Format(RootMenuCloseFormatString, m.Href, m.CssClass, m.MenuTitle, m.MenuTitle, m.IsCurrent ? " on" : "") + Identy;

                    if (m.ChildNodes != null && m.ChildNodes.Count>0)
                    {
                        var tagBuilder = new TagBuilder(WarpChildTag);
                        if (!string.IsNullOrEmpty(WarpChildTagCssClass))
                            tagBuilder.AddCssClass(WarpChildTagCssClass);
                        foreach (Menu n in m.ChildNodes)
                        {
                            if (!n.Visible) continue;
                            if (n == Menu.Current)
                                tagBuilder.InnerHtml += string.Format(CurrentMenuFormatString, n.Href, CurrentMenuCss, n.MenuTitle) + Identy;
                            else
                                tagBuilder.InnerHtml += string.Format(ChildMenuFormatString, n.Href, n.MenuTitle) + Identy;
                        }
                        tagBuildertmp.InnerHtml += new TagBuilder("li") { InnerHtml = tagBuilder.ToString() } + Identy;
                    }
                }
            sb.AppendLine(tagBuildertmp.ToString());
            return sb.ToString();
        }

    }
}
