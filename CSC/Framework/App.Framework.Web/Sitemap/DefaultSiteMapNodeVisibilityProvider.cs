#region Using directives

using System.Collections.Generic;
using System.Web;
using App.Framework.Web.MvcSiteMapProvider.Extensibility;

#endregion

namespace App.Framework.Web.MvcSiteMapProvider
{
    /// <summary>
    /// Default SiteMapNode Visibility Provider.
    /// </summary>
    public class DefaultSiteMapNodeVisibilityProvider
        : ISiteMapNodeVisibilityProvider
    {
        #region ISiteMapNodeVisibilityProvider Members

        /// <summary>
        /// Determines whether the node is visible.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="context">The context.</param>
        /// <param name="sourceMetadata">The source metadata.</param>
        /// <returns>
        /// 	<c>true</c> if the specified node is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsVisible(SiteMapNode node, HttpContext context, IDictionary<string, object> sourceMetadata)
        {
            return true;
        }
    
        #endregion
    }
}
