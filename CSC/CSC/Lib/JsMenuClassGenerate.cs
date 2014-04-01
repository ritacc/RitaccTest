using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using App.Framework.Web.Menu;

namespace CSC.Lib
{
    /// <summary>
    /// 生成JS菜单，须配合客户端的menuclass.js完成
    /// </summary>
    public class JsMenuClassGenerate : IGenerateMenu
    {
        #region private field
        private static readonly string _JSSTARTCODE = "function CreateMenuItems(m) { var item0, item1;";
        private static readonly string _JSITEMFORMAT = "item0 = m.Item('{0}');";
        private static readonly string _JSITEMSETPARENTFORMAT = "item{0} = item{1}.Item('{2}');";
        private static readonly string _JSITEMLINKFORMAT = "item{0}.Link('{1}', '{2}', false);";
        private static readonly bool _VISIBLEVALIDATE = true; //启用此项将验证模块和菜单的Visible属性
        #endregion

        private static readonly JsMenuClassGenerate _instance = new JsMenuClassGenerate();
        private JsMenuClassGenerate() { }
        public static JsMenuClassGenerate Instance { get { return _instance; } }

        #region private method
        private void GetMenuString(Menu m, StringBuilder sb, int parent, int current)
        {
            if (m.ChildNodes != null && m.ChildNodes.Count > 0) //如果有子节点
            {
                sb.AppendFormat(_JSITEMSETPARENTFORMAT, current, parent, m.MenuTitle);
                foreach (var item in m.ChildNodes)
                {
                    if (_VISIBLEVALIDATE && !item.Visible)
                        continue;
                    GetMenuString(item, sb, current, current + 1);
                }
            }
            else
                sb.AppendFormat(_JSITEMLINKFORMAT, parent, m.MenuTitle, m.Href);
        }
        #endregion

        #region public method
        /// <summary>
        /// 生成所有模块菜单
        /// </summary>
        /// <param name="moudles"></param>
        /// <returns></returns>
        public string GenerateMoudle(IEnumerable<Module> moudles)
        {
            if (moudles == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_JSSTARTCODE);
            foreach (var item in moudles)
            {
                if (_VISIBLEVALIDATE && !item.Visible)
                    continue;
                string str = GenerateMenu(item);
                sb.AppendLine(str);
            }
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 生成指定模块下的菜单
        /// </summary>
        /// <param name="moudle"></param>
        /// <returns></returns>
        public string GenerateMenu(Module moudle)
        {
            if (moudle == null) throw new ArgumentNullException("moudle");
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(_JSITEMFORMAT, moudle.MoudleCaption);
            IEnumerable<Menu> menus = moudle.Menus;
            if (menus != null)
            {
                foreach (Menu m in menus)
                {
                    if (_VISIBLEVALIDATE && !m.Visible)
                        continue;
                    GetMenuString(m, sb, 0, 1);

                }
            }
            return sb.ToString();
        }
        #endregion
    }
}