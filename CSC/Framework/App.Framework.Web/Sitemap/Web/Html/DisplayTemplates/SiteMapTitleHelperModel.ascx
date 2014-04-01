<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl`1[ [App.Framework.Web.MvcSiteMapProvider.Web.Html.Models.SiteMapTitleHelperModel,App.Framework.Web] ]" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="App.Framework.Web.MvcSiteMapProvider.Web.Html.Models" %>

<%=Model.CurrentNode.Title%>