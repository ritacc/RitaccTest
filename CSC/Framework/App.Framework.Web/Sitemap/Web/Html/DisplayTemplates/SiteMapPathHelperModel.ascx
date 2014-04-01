<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[ [App.Framework.Web.MvcSiteMapProvider.Web.Html.Models.SiteMapPathHelperModel,App.Framework.Web] ]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="App.Framework.Web.MvcSiteMapProvider.Web.Html.Models" %>

<% foreach (var node in Model) { %>
    <%=Html.DisplayFor(m => node)%>
    <% if (node != Model.Last()) { %>
        &gt;
    <% } %>
<% } %>