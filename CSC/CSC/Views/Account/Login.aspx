<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Entity.User.LoginModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>
		<%=Html.MvcSiteMap().SiteMapTitle() %>
	</title>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
	<link href="<%=Url.Content("~/Content/Css/Shared/Login.css")%>" rel="stylesheet" type="text/css" />
	
	<%--<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />--%>
	<script type="text/javascript">
		<%=Html.Message() %>
	

		var href= location.href;
		if(window != window.parent){
			var win = window;
			while(win != win.parent){
				win = win.parent;
			}
			win.location.href=href;
		}
		$(function(){
			$("#UserName").focus();
		});
//		$(document).ready(function(){  
//			$("#login_form").fadeIn(2000); 
//		});
	</script>
	<%Html.EnableClientValidation(); %>
</head>
<body>
	<%--<div class="login_form" style="display:none;" id="login_form">--%>
	<div class="login_form">
		<%using (Html.BeginForm())
	{%>
		<table>
			<tr>
				<td class="login_title" colspan="4" style="height: 100px;">
					&nbsp;
					<img src='<%=Url.Content("~/Content/Css/Images/Logo.png") %>' style="vertical-align: bottom;" />
					<%:Account.LoginToCSCText%>
				</td>
			</tr>
			<tr>
				<td style="width: 160px; text-align: right">
					<%:CSC.Resources.Account.UserNameText%>
				</td>
				<td style="width: 260px;">
					<%:Html.TextBoxFor(m => m.UserName, new { @maxlength = "50", @class = "text_in", style = "TEXT-TRANSFORM: uppercase" })%>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td>
					&nbsp;
				</td>
				<td>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td style="width: 160px; text-align: right">
					<%=CSC.Resources.Account.PasswordText%>
				</td>
				<td style="width: 260px;">
					<%:Html.PasswordFor(m => m.Password, new { @maxlength = "50", @class = "text_in",style = "TEXT-TRANSFORM: uppercase" })%>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<%if (ConfigurationManager.AppSettings["LoginShowShop"] == "true")
	 { %>
			<tr>
				<td style="width: 160px; text-align: right">
					<%=CSC.Resources.Account.ShopText%>
				</td>
				<td style="width: 260px;">
					<%:Html.DropDownListFor(m => m.Shop, Model.ShopList.InnerList.ToSelectList(m => m.Name, m => m.Code), new { @class = "ddl_in" })%>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<%} %>
			<tr style="height: 40px;">
				<td>
				</td>
				<td style="text-align: right;">
					<%--<%=Html.GenerateLaugnageLink()%>
					&nbsp;&nbsp;--%>
					<input type="submit" value="<%=CSC.Resources.Account.BtnLoginText %>" id="btnlogin" />
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr style="height: 52px;">
				<td colspan="2">
					<%=Html.ValidationSummary()%>
				</td>
				<td colspan="2" style="font-size: small;">
					<%=CSC.Resources.Account.Version%>
                     <% var version = System.Reflection.Assembly.Load("CSC").GetName().Version.ToString();
                        var vArr = version.Split('.');
                        var vStr = string.Format("{0}.{1}",vArr[0], vArr[1]);%>
                     <%:vStr%>
					 
				</td>
			</tr>
		</table>
		<%} %>
	</div>
</body>
</html>
