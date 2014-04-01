<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Entity.User.ModifyPasswordModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
	<title>Reset Password</title>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Form.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
	<script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftAjax.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftMvcValidation.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Form.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdownEx.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.LevelLoader.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/CommonLov.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>

<%--	<link href="<%=Url.Content("~/Content/Css/Shared/Login.css")%>" rel="stylesheet" type="text/css" />--%>
	
	<%Html.EnableClientValidation(); %>
	
	<script type="text/javascript">
		window.checkChanged = true;
		$g = <%:CSC.Controllers.AjaxController.GetResourceText("GlobalText") %>;
		window.btnSureClick = function (sucessed, redirectUrl) {
			if (sucessed) {
				document.location.assign('<%=Url.Content("~/Account/SignOut")%>');
			}
		};

		$(function () {
			<%=Html.Message() %>

			$("#btnCancel").click(function () {
				document.location.assign('<%=Url.Content("~/Account/SignOut")%>');
				return false;
			});
		});

		function ValidateLoginPas() {
			var loginPwd = $("#NewLoginPwd").val().toUpperCase();
			var loginCPwd = $("#NewConfirmLoginPwd").val().toUpperCase();

			if ((loginPwd == null || loginPwd == "") && (loginCPwd == null || loginCPwd == "")) {
				alert($g["EnterPassword"]);
				$("#NewLoginPwd_validationMessage").text($g["EnterPassword"]);
				return false;
			} else if (loginPwd != loginCPwd) {
				alert($g["PassNoEqualsConfirmPass"]);
				$("#NewLoginPwd_validationMessage").text("");
				$("#NewConfirmLoginPwd_validationMessage").text($g["PassNoEqualsConfirmPass"]);
				return false;
			}

			return true;
		}
    </script>
</head>
<body>
        <% using (Html.BeginForm("InitialPwdChange", "Account", FormMethod.Post, new { @onsubmit = "return ValidateLoginPas();" }))
           { %>
		<div style="position:absolute;left:50%;top:50%;margin-left:-284px;margin-top:-182px;width:567px;height:283px;">
			<table>
				<tr>
					<td style="font-size:xx-large;font-family:@微软雅黑;font-style:oblique;height:80px;" colspan="4">
						&nbsp;
						<img src='<%=Url.Content("~/Content/Css/Images/Logo.png") %>' style="vertical-align:bottom;"  alt=""/>
						<%:Account.ModifyPassword%>
					</td>
				</tr>
				<%--<tr>
					<td colspan="4" style="width: 260px; text-align:left">
						<%:Html.HiddenFor(m=>m.UserCode) %>
						<%:Html.HiddenFor(m=>m.UserName) %>
						<%:CSC.Resources.Account.UserCode%>
						<%:Model.UserCode %> &nbsp;&nbsp;
						<%:CSC.Resources.Account.UserNameDsc%>
						<%:Model.UserName%>
					</td>
					
				</tr>--%>
				
				<tr>
					<td style="width: 160px; text-align: right;height:40px;">
						<%= CSC.Resources.Account.NewPassword%>
					</td>
					<td style="width: 160px;">
						<%--<%=Html.PasswordFor(m => m.NewLoginPwd, new { maxlength = "10", style = "TEXT-TRANSFORM: uppercase" })%>--%>
						<%:Html.PasswordFor(m => m.NewLoginPwd, new { @maxlength = "10", style = "TEXT-TRANSFORM: uppercase;width: 160px;font-family: @微软雅黑;font-size: 18px;padding: 0px 2px;" })%>
						<span style="color: Red;">
							<%=Html.ValidationMessageFor(m => m.NewLoginPwd)%></span>
					</td>
					<td>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td style="width: 160px; text-align: right;height:40px;">
						<%= CSC.Resources.Account.NewConfirmPassword%>
					</td>
					<td style="width: 160px;">
						<%--<%=Html.PasswordFor(m => m.NewConfirmLoginPwd, new { maxlength = "10", style = "TEXT-TRANSFORM: uppercase" })%>--%>
						<%:Html.PasswordFor(m => m.NewConfirmLoginPwd, new { @maxlength = "10", style = "TEXT-TRANSFORM: uppercase;width: 160px;font-family: @微软雅黑;font-size: 18px;padding: 0px 2px;" })%>
						<span style="color: Red;">
							<%=Html.ValidationMessageFor(m => m.NewConfirmLoginPwd)%></span>
					</td>
					<td>
					</td>
					<td>
					</td>
				</tr>
				<tr style="height: 40px;">
					<td>
					</td>
					<td style="text-align: right;">
						<% Html.Submit(CSC.Resources.Account.ModifyPassword); %>
						&nbsp;
						<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
					</td>
					<td>
					</td>
					<td>
					</td>
				</tr>
				
			</table>
		</div>
		
        <%} %>
 
</body>
</html>

