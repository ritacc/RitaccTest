<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Entity.User.ModifyPasswordModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftMvcValidation.js")%>"
        type="text/javascript"></script>
    <%Html.EnableClientValidation(); %>
    <style type="text/css">
        table.wizTitle
        {
            padding-top: 19px;
            margin-bottom: 10px;
        }
        td.wizTitle
        {
            color: #4A6DA5;
            border-bottom: solid 1px #5275A5;
            padding-bottom: 4px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        window.checkChanged = true;
        var isCurrentUser = "<%=Model.IsCurrentUser %>";

        window.btnSureClick = function (sucessed, redirectUrl) {
        	if (isCurrentUser.toUpperCase() == "TRUE") {
        		//不做任何操作
        	}
        	else {
        		if (sucessed) {
        			document.location.assign('<%=Url.Content("~/User/Index")%>');
        		}
        		else {
        			var user_id = $("#UserID").val();
        			if (user_id != 0) {
        				document.location.assign('<%=Url.Content("~/Account/PasswordChange?userId=")%>' + user_id);
        			}
        			else {
        				document.location.assign('<%=Url.Content("~/Account/PasswordChange")%>');
					}
        			
        		}
        	}
        };

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

//            if (!confirm($g["ConfirmSave"])) {
//                return false;
//            }

            return true;
        }

        function ValidateAuthPas() {
            var authPwd = $("#NewAuthPwd").val().toUpperCase();
            var authCPwd = $("#NewConfirmAuthPwd").val().toUpperCase();
            if ((authPwd == null || authPwd == "") && (authCPwd == null || authCPwd == "")) {
                
                $("#NewAuthPwd_validationMessage").text($g["EnterPassword"]);
                return false;
            } else if (authPwd != authCPwd) {
                alert("两次输入的密码不一致");
                $("#NewAuthPwd_validationMessage").text("");
                $("#NewConfirmAuthPwd_validationMessage").text($g["PassNoEqualsConfirmPass"]);
                return false;
            }

//            if (!confirm($g["ConfirmSave"])) {
//                return false;
//            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
    <div>
        <% using (Html.BeginForm(Request.QueryString["UserId"] == null ? "ModifyLoginPwd" : "ModifyUserLoginPwd", "Account", FormMethod.Post, new { @onsubmit = "return ValidateLoginPas();" }))
           { %>
        <%=Html.Hidden("UserID", Request.QueryString["UserId"])%>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" class="wizTitle">
            <tr>
                <td class="wizTitle">
                    <%= CSC.Resources.Account.LoginPwd%>
                </td>
            </tr>
        </table>
        <table class="edit">
            <tr>
                <td style="width: 100px;">
                </td>
                <td style="width: 160px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    
                    <%:Html.HiddenFor(m=>m.UserCode) %>
                    <%:Html.HiddenFor(m=>m.UserName) %>
                    <%:CSC.Resources.Account.UserCode%>
                </td>
                <td align="left">
                    <%:Model.UserCode %>&nbsp;
                </td>
                <td colspan="3" style="text-align: left;">
                    <%:CSC.Resources.Account.UserNameDsc%><%:Model.UserName%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%= CSC.Resources.Account.NewPassword%>
                </td>
                <td align="left">
                    <%=Html.PasswordFor(m => m.NewLoginPwd, new { maxlength = "10", style = "TEXT-TRANSFORM: uppercase" })%>
                    <span style="color: Red;">
                        <%=Html.ValidationMessageFor(m => m.NewLoginPwd)%></span>
                </td>
                <td>
                   <%-- <%:CSC.Resources.Account.PasswordValidate %>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%= CSC.Resources.Account.NewConfirmPassword%>
                </td>
                <td align="left">
                    <%=Html.PasswordFor(m => m.NewConfirmLoginPwd, new { maxlength = "10", style = "TEXT-TRANSFORM: uppercase" })%>
                    <span style="color: Red;">
                        <%=Html.ValidationMessageFor(m => m.NewConfirmLoginPwd)%></span>
                </td>
                <td>
                    <% if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Own_ChangePassword) ||
                           QuickInvoke.GetCurrentUserHasPermission(EnumPermission.User_ChangePassword)
                          )
                       {
                    %>
                    <% Html.Submit(CSC.Resources.Account.ModifyPassword); %>
                    <% 
                        } 
                    %>
                </td>
            </tr>
        </table>
        <%} %>
        <%--<% using (Html.BeginForm(Request.QueryString["UserId"] == null ? "ModifyAuthPwd" : "ModifyUserAuthPwd", "Account", FormMethod.Post, new { @onsubmit = "return ValidateAuthPas();" }))
	  { %> 
		
		<%=Html.Hidden("UserID", Request.QueryString["UserId"])%>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" class="wizTitle">
            <tr>
                <td class="wizTitle">
                    <%= CSC.Resources.Account.AuthPwd%>
                </td>
            </tr>
        </table>
        <table class="edit">
            <tr>
                <td style="width:100px;"></td>
                <td style="width:160px;"></td>
				<td></td>
            </tr>
            <tr>
                <td align="right">
                    <%= CSC.Resources.Account.NewPassword%>
                </td>
                <td align="left">
                    <%=Html.PasswordFor(m => m.NewAuthPwd, new { maxlength = "10" })%>
                    <span style="color:Red;"><%=Html.ValidationMessageFor(m => m.NewAuthPwd)%></span>
                </td> 
				<td></td>
            </tr>
            <tr>
                <td align="right">
                    <%= CSC.Resources.Account.NewConfirmPassword%>
                </td>
                <td align="left">
                    <%=Html.PasswordFor(m => m.NewConfirmAuthPwd, new { maxlength = "10" })%>
                    <span style="color:Red;"><%=Html.ValidationMessageFor(m => m.NewConfirmAuthPwd)%></span>
                </td>
				<td><% Html.Submit(CSC.Resources.Account.ModifyPassword); %></td>
            </tr>
        </table>

		<% } %>--%>
    </div>
    <%--	<div class="edit_btn" style="margin-top:15px;">
		<table width="100%">
			<tr>
				<td align="center">
					<% Html.Submit(GlobalText.BtnSaveText, url: Url.Action("ModifyUserLoginPwdAndAuthPwd")); %>
					&nbsp;
					<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", Url.Action("ModifyUserLoginPwdAndAuthPwd")); %>         
				</td>
			</tr>
		</table>
	</div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>
