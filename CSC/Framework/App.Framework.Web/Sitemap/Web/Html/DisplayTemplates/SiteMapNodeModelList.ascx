<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[ [App.Framework.Web.MvcSiteMapProvider.Web.Html.Models.SiteMapNodeModelList,App.Framework.Web] ]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="App.Framework.Web.MvcSiteMapProvider.Web.Html.Models" %>

<ul>
<% foreach (var node in Model) { %>
    <li><%=Html.DisplayFor(m => node)%>
    <% if (node.Children.Any()) { %>
        <%=Html.DisplayFor(m => node.Children)%>
    <% } %>
    </li>
<% } %>
</ul>