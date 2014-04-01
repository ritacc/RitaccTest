<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.PartsListModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head id="Head1" runat="server">
	   <link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/jQuery/dropdownEx.css")%>" rel="stylesheet" type="text/css" />

	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdownEx.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>

		<script type="text/javascript">
		<%Html.EnableSortScript(); %>
		$(function () {
			$("#reset").click(function()
			{
				window.location.href="../Demo/DemoLov?Code= ";
				return false;
			});
		});
	</script>
	</head>
	<body>
		<div class="centra">
			<div class="search">
				<%Html.BeginForm("DemoLov", "Demo", FormMethod.Get);%>
					<table><tr>
						<td>
							Parts:
						</td>
						<td>
							<%=Html.TextBox("Code", new { maxlength = 30, style = "width:140px;" })%>
						</td>
						<%--<td>
							Wildcat
							<%:Html.CheckBox("WildcatFlag")%>
						</td>--%>
						<td>
							<input type="submit" tabindex="0" value="<%:GlobalText.BtnSearchText %>" />
							<%Html.Button(GlobalText.BtnResetText, "reset");%>
						</td>	
					</tr></table>
				<%Html.EndForm();%>
			</div>
			<div class="grid_list">
				<table>
					<thead>
						<tr>
							<th class="w20 t_center">
								<%if (String.IsNullOrEmpty(Request["notIsMultipleChoose"])){%>
									<input type="checkbox" name="cball" class="cball"/>
								<% } %>
							</th>
							<th class="w180 t_left">
								<%:PartsMaintenanceText.PartsNo%>
							</th>
							<th class="w200 t_left">
								<%:PartsMaintenanceText.Description%>
							</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
					<% if (Model.PartsList != null)
					{ %>
						<%foreach (var item in Model.PartsList)
					{%>
							<tr>
								<td class="t_center">
									<input type="checkbox" name="cbKey" value="<%= item.PartsId %>"  
									SearchGodown_GodownCode = "<%=item.PartsNo %>#<%=item.PartsDesc %>"  />
								</td>
								<td class="t_left">
									<%=item.PartsNo%>
								</td>
								<td class="t_left">
									<%=item.PartsDesc%>
								</td>
								<td></td>
							</tr>
						<% } %>						
					<% } %>
					</tbody>
				</table>
			</div>
			<div class="pager">
				<%=Html.QuickPager()%>
			</div>
		</div>
	</body>
</html>
