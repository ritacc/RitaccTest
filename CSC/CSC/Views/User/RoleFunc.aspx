<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.RoleListModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>

	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonDialog.js")%>" type="text/javascript"></script>  
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonChoose.js")%>" type="text/javascript"></script>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" /> 
	<script type="text/javascript">
		$(function () {
			$("#btnCancel").click(function () {
				window.parent.$("#tabUserRole").find("iframe")[0].contentWindow.dlgEdit.close();
				return false;
			});
		});
		</script>
</head>
<body>
    <div class="centra">
		<div class="search">
			<table>
				<tr>
					<td class="w200  t_left">
						<%=Role.RoleCode%>
						<label class="label_readonly" style="width:100px;"><%=Model.RoleModel.RoleCode %></label>
					</td>
					<td class="w350  t_left">
						<%=Role.RoleDSC%>
						<label class="label_readonly" style="width:200px;"><%=Model.RoleModel.RoleDsc %></label>
					</td>
					<%--<td class="w350  t_left">
						<%=Role.RoleType%>
						<label class="label_readonly" style="width:80px;">
							<% if (Model.RoleModel.RoleType == CSC.Business.RoleTypes.SY.ToString())
							{  %>
							<%= Html.Label(Role.RoleTypeSY)%>
							<%  }
							else
							{ %>
							<%= Html.Label(Role.RoleTypeSH)%>
							<%  } %>
						</label>
						<%=Role.ShopCode%>
						<label class="label_readonly" style="width:80px;"><%=Model.RoleModel.ShopName%></label>
					</td>
					<td class="w150  t_left">
						<%=Role.AdminFlag%>
						<%:Html.CheckBox("chkAdminFlag", Model.RoleModel.AdminFlag.ToString(), Model.RoleModel.AdminFlag, new { @onclick = "return false;" })%>
					</td>--%>
				</tr>
			</table>
		</div>
		<div class="grid_list">
			<table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides" id="tRoleFunc">
				<thead>
					<tr>
						<th class="w30">
							<%=Role.Number %>
						</th>
						<th class="w30">
							<%=Role.ActiveFlag %>
						</th>
						<th class="w100 t_left">
							<%=Role.FunctionCode%>
						</th>
						<%--<th class="w80 t_left">
							<%=Role.FunctionType %>
						</th>--%>
						<th class="t_left">
							<%=Role.FunctionName %>
						</th>
						<%--<th class="w30">
							<%=Role.AdminFlag %>
						</th>--%>
						<th class="w100 t_left">
							<%=Role.Model%>
						</th>
						<%--<th class="w80 t_left">
							<%=Role.SystemScope %>
						</th>--%>
						<th class="w80">
							<%=Role.InsertFlag %>
						</th>
						<th class="w80">
							<%=Role.UpdateFlag %>
						</th>
						<th class="w20">
						</th>
					</tr>
				</thead>
				<tbody>
						<%foreach (var item in Model.RoleModel.RoleFuncList)
					{%> 
						<tr>
							<td align="center">
								<%=Html.GridItemIndex()%>
							</td>
							<td align="center">
								<%:Html.CheckBox("chkActiveFlag", item.ActiveFlag, new { @onclick = "return false;" })%>
							</td>
							<td>
								<%=item.FuncCode %>
							</td>
							<%--<td>
								<% if (item.FuncType == CSC.Business.FunctionTypes.BTN.ToString()){  %>
									<%= Html.Label(Role.FunctionTypeBTN)%>
								<%  }else if (item.FuncType == CSC.Business.FunctionTypes.FORM.ToString()){ %>
									<%= Html.Label(Role.FunctionTypeFORM)%>
								<%  }else if (item.FuncType == CSC.Business.FunctionTypes.RPT.ToString()){ %>
									<%= Html.Label(Role.FunctionTypeRPT)%>	
								<%  }else{ %>
									<%= Html.Label(Role.FunctionTypePWD)%>
								<% } %>	
							</td>--%>
							<td>
								<%=item.Dsc %>
							</td>
							<%--<td align="center">
								<%= Html.CheckBox("chkRoleFuncAdmin", item.AdminFlag, new { @onclick = "return false;" })%>
							</td>--%>
							<td>
								<%=item.FuncType %>
							</td>
							<%--<td>
								<%=item.SystemScope %>
							</td>--%>
							<td align="center">
							 <%:Html.CheckBox("chkRoleFuncInsert", item.FuncId.ToString(), item.InsertableFlag, new { @onclick = "return false;" })%>
						 
							</td>
							<td align="center">
								 <%:Html.CheckBox("chkRoleFuncUpdate", item.FuncId.ToString(), item.UpdatableFlag, new { @onclick = "return false;" })%>
							</td>
							<td></td>
						</tr> 
					<%} %>
				</tbody>
			</table>
		</div>
		<div class="buttons" style="text-align: center;">
			<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
		</div> 
    </div>
</body>
</html>
