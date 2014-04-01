<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.DefualtGodownModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>SetDefaultGodown</title>

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
    <%Html.BeginForm("SetDefaultGodown", "Shop", FormMethod.Post);%>
    <div class="centra">
       <table style="margin-left: 20px; margin-top: 10px;">
			<tr>
                <td>
                    <%=ShopText.SetDefultGodown%>
                    <span class="asterisk">＊</span>
                </td>
                <td>
						<% Html.DropdownListEx(m => m.GodownID
							 , App.Framework.BusinessPortal.Search<CSC.Business.GodownForDllEntity>(new CSC.Business.GodownSelectForDefualtGodown() { WAREHOUSE_CODE = Model.Code }).OrderBy(m => m.GODOWN_CODE)
						 .ToList().ToSelectListWidtType(m => m.GODOWN_CODE, m => m.GODOWN_ID)
						   , htmlAttributes: new { @dwidth = 200, @width = 200, @height = 250, @id = "FmGodownId" });  %>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.GodownID)%>
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
