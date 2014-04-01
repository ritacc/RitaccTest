<%--<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<BusinessList<CSC.Business.Shop>>" %>--%>
<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.LoginToShopModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=Html.MvcSiteMap().SiteMapTitle() %>
	</title>
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
	<link href="<%=Url.Content("~/Content/Css/Shared/Login.css")%>" rel="Stylesheet" />
	<%--<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />--%>
	<script type="text/javascript">
		$(function () {
			$(document).keyup(function (e) {
				if (e.keyCode == 13) {
					$("form").submit();
				}
			});

			$(":button[url]").click(function () {
				try {
					var url = $(this).attr("url");
					document.location.href = url;

					return false;
				} catch (e) { }
			});
			if ($("#warningMessage").val() != "") {
				alert($("#warningMessage").val());
				$("#warningMessage").val("");
			}

			$("#shop").focus();
		});

//	    $(document).ready(function () {
//			$("#login_form").fadeIn(1);
//	    });
    </script>
</head>
<body>
    <%--<div class="login_form" style="display:none;" id="login_form">--%>
	<div class="login_form">
		<%using (Html.BeginForm()){%>
			<table>
				<tr>
					<td class="login_title" colspan="4" style="height:100px;">
						&nbsp;
						<img src='<%=Url.Content("~/Content/Css/Images/Logo.png") %>' style="vertical-align:bottom;"  alt=""/>
						<%:Account.ComanyName%>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td style="width:160px; text-align:right">
						<%=CSC.Resources.Account.SelectShopText%>
					</td>
					<td style="width:260px;">
						<%:Html.DropDownList("shop", Model.ShopList.ToSelectList(m => (m.Name + "(" + m.Code + ")"), m => m.Code), new { @class = "ddl_in" })%>
					</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr style="height:40px;">
					<td></td>
					<td style="text-align:right;">
						<%--<%=Html.GenerateLaugnageLink()%> 
						&nbsp;&nbsp;--%>
						<input type="submit" value="<%=CSC.Resources.Account.BtnEnterText %>" id="btnlogin" />
						<%Html.Button(CSC.Resources.GlobalText.btnCancelText, url: "SignOut");%>
					</td>
					<td></td>
					<td></td>
				</tr>
				<tr style="height:52px;">
					<td></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
			</table>
            <%= Html.Hidden("warningMessage", Model.PwdWarningMesssage) %>
		<%} %>
    </div>
</body>
</html>
