<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Shop>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title></title>

	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css") %>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Form.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/jQuery/dropdownEx.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />

	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js") %>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js") %>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js") %>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dialog.js") %>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdownEx.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js") %>" type="text/javascript"></script>
	<script src="<%:Url.Content("~/Content/Scripts/jquery/jquery-SimpleValidForm.js") %>" type="text/javascript"></script>
	<script src="<%:Url.Content("~/Content/Scripts/jquery/jquery-SimpleValidMethod.js") %>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
		
	<%Html.EnableClientValidation(); %>
	<script type="text/javascript">
		window.checkFormChanged = true;
		window.btnSureClick = function (sucessed, redirectUrl) {
			if (sucessed) {
				window.parent.$("#tabShopList").find("iframe")[0].contentWindow.dlgEdit.close(function () {
					window.parent.loadFrame(window.parent.$("#tabShopList"), null, true);
				});
			}
		};

		function ValidateLoginPas() {
            var loginPwd = $("#AUTH_PWD").val().toUpperCase();
            var loginCPwd = $("#NewPwd").val().toUpperCase();

            if ((loginPwd == null || loginPwd == "") && (loginCPwd == null || loginCPwd == "")) {
                alert($g["EnterPassword"]);
                $("#AUTH_PWD_validationMessage").text($g["EnterPassword"]);
                return false;
            } else if (loginPwd != loginCPwd) {
                alert($g["PassNoEqualsConfirmPass"]);
                $("#AUTH_PWD_validationMessage").text("");
                $("#NewPwd_validationMessage").text($g["PassNoEqualsConfirmPass"]);
                return false;
            }
//            if (!confirm($g["ConfirmSave"])) {
//                return false;
//            }

            return true;
        }


		$(function () {
			<%=Html.Message() %>

		

		$("#btnCancel").click(function () {
				window.parent.$("#tabShopList").find("iframe")[0].contentWindow.dlgEdit.close();
				return false;
		});

		});
	</script>
</head>
<body>
    <%Html.BeginForm("SetPassword", "Shop", FormMethod.Post, new { @onsubmit = "return ValidateLoginPas();" });%>
    <div class="centra">
       <table style="margin-left: 20px; margin-top: 10px;">
			<tr>
                <td>
                    <%= CSC.Resources.Account.NewPassword%>
                    <span class="asterisk">＊</span>
                </td>
                <td>
						<%=Html.PasswordFor(m => m.AUTH_PWD, new { maxlength = "10", style = "TEXT-TRANSFORM: uppercase" })%>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.AUTH_PWD)%>
                    </div>
                </td> 
            </tr>
			<tr>
                <td>
                    <%= CSC.Resources.Account.NewConfirmPassword%>
                    <span class="asterisk">＊</span>
                </td>
                <td>
						<%=Html.Password("NewPwd","", new { maxlength = "10", style = "TEXT-TRANSFORM: uppercase" })%>
                    <div>
                        
                    </div>
                </td> 
            </tr>
	</table>
    </div>
	<div id="Message" class="message">
	</div>
	<div class="bottom dialog-buttons">
		<table width="100%">
			<tr>
				<td align="center">
					<% Html.Submit(GlobalText.BtnSaveText,"btnSave"); %>
					&nbsp;
					<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
				</td>
			</tr>
		</table>
	</div>
	<%=Html.HiddenFor(m=> m.Code) %>
	<%Html.EndForm();%>
</body>
</html>
