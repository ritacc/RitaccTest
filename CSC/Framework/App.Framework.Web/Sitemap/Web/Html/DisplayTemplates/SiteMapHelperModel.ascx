<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[ [App.Framework.Web.MvcSiteMapProvider.Web.Html.Models.SiteMapHelperModel,App.Framework.Web] ]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="App.Framework.Web.MvcSiteMapProvider.Web.Html.Models" %>

<ul class="siteMap">
<% foreach (var node in Model.Nodes) { %>
    <li><%=Html.DisplayFor(m => node)%>
    <% if (node.Children.Any()) { %>
        <%=Html.DisplayFor(m => node.Children)%>
    <% } %>
    </li>
<% } %>
</ul>