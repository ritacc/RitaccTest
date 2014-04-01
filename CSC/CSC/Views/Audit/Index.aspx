<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Audit>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Audit</title>
	
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />

	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
	<script type="text/javascript">
		$(function () {
			$("#btnOK").click(function () {
				getAllWinElementById("Audit").parents(".topContainter").find(".div_title_close_dialog").attr("noClosing", "true").click();
				return false;
			});
            setDialogScroll('#Audit');
		});
		
	</script>
</head>
<body>
    <div class="centra">
		<table style="margin-left: 20px;margin-top: 10px;">
			<tr>
				<td>
					<%=GlobalText.CreatedBy %>
				</td>
				<td>
					<label id="lblCreatedBy" class="label_readonly" style="width: 200px;">
						<%=Model.CreatedBy%>
					</label>
				</td>
			</tr>
			<tr>
				<td>
					<%=GlobalText.CreationDate %>
				</td>
				<td>
					<label id="lblCreationDate" class="label_readonly" style="width: 200px;">
						<%=Model.CreationDate.Format(true) %>
					</label>
				</td>
			</tr>
			<tr>
				<td>
					<%=GlobalText.LastUpdatedBy %>
				</td>
				<td>
					<label id="lblLastUpdatedBy" class="label_readonly" style="width: 200px;">
						<%=Model.LastUpdatedBy %>
					</label>
				</td>
			</tr>
			<tr>
				<td>
					<%=GlobalText.LastUpdateDate %>
				</td>
				<td>
					<label id="lblLastUpdateDate" class="label_readonly" style="width: 200px;">
						<%=Model.LastUpdateDate.Format(true) %>
					</label>
				</td>
			</tr>
		</table>
    </div>
	<div class="bottom dialog-buttons">
		<table width="100%">
			<tr>
				<td align="center">
					<% Html.ReturnButton(GlobalText.BtnOKText, "btnOK", null); %>
				</td>
			</tr>
		</table>
	</div>
</body>
</html>
