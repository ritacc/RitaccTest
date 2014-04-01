<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Function>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftMvcValidation.js")%>"
        type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Form.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Form.js")%>" type="text/javascript"></script> 
    <script src="<%=Url.Content("~/Content/Scripts/Common/formLeaveCheck.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
   
     <script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
    

    <%Html.EnableClientValidation(); %>
    <script type="text/javascript">
			<%:Html.Message() %>

			window.btnSureClick = function (sucessed, redirectUrl) {
				if (sucessed) {
					window.parent.location.reload();
				}
			};

			$(document).ready(function () {
				$("#btnCancel").click(function (e) {
					window.parent.dlgEdit.close();
				});
			    
                $(":submit").click(function () {
                    if (!window.confirm($g["ConfirmSave"])) {
                        return false;
                    }
                });
			});
    </script>
</head>
<body>
    <%Html.BeginForm("Edit", "Function", FormMethod.Post);%>
    <div class="centra">
        <table style="margin-top: 10px;">
            <tr>
                <td class="w150 t_right">
                    <%:FunctionText.Function_Id %>
                </td>
                <td class="w300 t_left">
                    <label class="label_readonly " style="width: 155px;">
                        <%=Model.FuncCode%></label>
                </td>
            </tr>
            <tr>
                <td class="w150 t_right">
                    <%:FunctionText.Function_Name%>
                    <%--<span class="asterisk">＊</span>--%>
                </td>
                <td class="w300 t_left">
                    <label class="label_readonly " style="width: 250px;">
                        <%=Model.Dsc%></label>
                    <%=Html.HiddenFor(m => m.Dsc) %>
                    <%--<%=Html.TextBoxFor(m => m.Dsc, htmlAttributes: new { maxlength = 50 ,style = "width:250px;"})%>
						<div>
							<%:Html.ValidationMessageFor(m => m.Dsc)%>
						</div>--%>
                </td>
            </tr>
            <%--<tr>
					<td class = "w150 t_right">
						<%:FunctionText.Function_Url%>
					</td>
					<td class = "w300 t_left">
						<label class="label_readonly" style="width:205px;"><%=Model.Executable%></label>
					</td>
				</tr>--%>
            <tr>
                <td class="w150 t_right">
                    <%:FunctionText.Function_Type%>
                </td>
                <td class="w300 t_left">
                    <label class="label_readonly" style="width: 45px;">
                        <%=Model.FuncType%>
					</label>
                </td>
            </tr>
            <tr>
                <td class="w150 t_right">
                    <%:FunctionText.Function_Management%>
                </td>
                <td class="w300 t_left">
                    <%=Html.CheckBoxFor(m => m.AdminFlag, new { })%>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.AdminFlag)%>
                    </div>
                </td>
            </tr>
            <%--<tr>
					<td class = "w150 t_right">
						<%:FunctionText.Function_Type%>
					</td>
					<td class = "w300 t_left">
						<%=Html.DropDownListFor(m => m.FuncType, (List<SelectListItem>)(ViewData["FuncTypeItems"]), htmlAttributes: new { style = "width:80px;" })%>
						<div>
							<%:Html.ValidationMessageFor(m => m.FuncType)%>
						</div>
					</td>
				</tr>--%>
        </table>
    </div>
    <div class="bottom dialog-buttons">
        <% Html.Submit(GlobalText.BtnSaveText, "btnSave"); %>
        &nbsp;
        <% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
    </div>
    <%=Html.HiddenFor(m => m.FuncType)%>
    <%=Html.HiddenFor(m => m.FuncCode)%>
    <%=Html.HiddenFor(m => m.Executable)%>
    <%=Html.HiddenFor(m => m.SystemScope)%>
    <%=Html.HiddenFor(m => m.FuncId) %>
    <%=Html.HiddenFor(m => m.LastUpdateDate) %>
    <%=Html.HiddenFor(m => m.LastUpdatedBy) %>
    <%Html.EndForm(); %>
</body>
</html>
