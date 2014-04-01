#region Using directives

using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using App.Framework.Web.MvcSiteMapProvider.Web.Html.Models;
using System.Collections.Specialized;

#endregion

namespace App.Framework.Web.MvcSiteMapProvider.Web.Html
{
    /// <summary>
    /// MvcSiteMapHtmlHelper extension methods
    /// </summary>
    public static class SiteMapPathHelper
    {
        /// <summary>
        /// Source metadata
        /// </summary>
        private static Dictionary<string, object> SourceMetadata = new Dictionary<string, object> { { "HtmlHelper", typeof(SiteMapPathHelper).FullName } };

        /// <summary>
        /// Gets SiteMap path for the current request
        /// </summary>
        /// <param name="helper">MvcSiteMapHtmlHelper instance</param>
        /// <returns>SiteMap path for the current request</returns>
        public static MvcHtmlString SiteMapPath(this MvcSiteMapHtmlHelper helper)
        {
            return SiteMapPath(helper, null);
        }

        public static MvcHtmlString HelpPath(this MvcSiteMapHtmlHelper helper)
        {
            string description = string.Empty;

            try
            {
                description = helper.Provider.CurrentNode.Description;
            }
            catch
            {
                description = "NO_SITEMAP";
            }

            string path = System.Configuration.ConfigurationManager.AppSettings["Help"];
            string text = "帮助";
            bool exists = System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format(path + "{0}.csv", description)));
            StringBuilder builder = new StringBuilder();
            if (exists)
            {
                builder.AppendFormat(System.String.Format("<a href=\"{0}\">{1}</a>", string.Format(path + "{0}.csv", description), text));
                return MvcHtmlString.Create(builder.ToString());
            }

            builder.AppendFormat("<a onclick=\"alert('没有帮助文件');return false;\" href=\"{0}\">{1}</a>", "#", text);
            return MvcHtmlString.Create(builder.ToString());
        }

        /// <summary>
        /// Gets SiteMap path for the current request
        /// </summary>
        /// <param name="helper">MvcSiteMapHtmlHelper instance</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns>SiteMap path for the current request</returns>
        public static MvcHtmlString SiteMapPath(this MvcSiteMapHtmlHelper helper, string templateName)
        {
            var model = BuildModel(helper, helper.Provider.CurrentNode);
            var hl = helper.CreateHtmlHelperForModel(model);
            var str = hl.DisplayFor(m => model, templateName);
            return str;
        }

        /// <summary>
        /// Builds the model.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="startingNode">The starting node.</param>
        /// <returns>The model.</returns>
        private static SiteMapPathHelperModel BuildModel(MvcSiteMapHtmlHelper helper, SiteMapNode startingNode)
        {
            // Build model
            var model = new SiteMapPathHelperModel();
            var node = startingNode;
            while (node != null)
            {
                var mvcNode = node as MvcSiteMapNode;

                // Check visibility
                bool nodeVisible = true;
                if (mvcNode != null)
                {
                    nodeVisible = mvcNode.VisibilityProvider.IsVisible(
                        node, HttpContext.Current, SourceMetadata);
                }

                // Check ACL
                if (nodeVisible && node.IsAccessibleToUser(HttpContext.Current))
                {
                    // Add node
                    var nodeToAdd = SiteMapNodeModelMapper.MapToSiteMapNodeModel(node, mvcNode, SourceMetadata);
                    model.Nodes.Add(nodeToAdd);
                }
                node = node.ParentNode;
            }
            model.Nodes.Reverse();

            return model;
        }
    }
}