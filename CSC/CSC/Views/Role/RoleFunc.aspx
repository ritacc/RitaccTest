<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.RoleListModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js")%>" type="text/javascript"></script>
	<%--<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Form.js")%>" type="text/javascript"></script>--%>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonDialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonChoose.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
		window.checkFormChanged = true;
		
		<%Html.EnableSortScript(); %>

		$(function () {
			window.btnSureClick = function (sucessed, redirectUrl) {
				if (sucessed) {
					var container = window.parent.$("div.tab_container");
					container.find("ul>li").eq(0).click();
					loadFrame(window.parent.$("#tabList"), "List", true);
				}

			};

			$("#tRoleFunc").find("input[name='chkRoleFuncActive']").click(function () {
				$(this).parent().parent().find("input[name='chkRoleFuncUpdate'],input[name='chkRoleFuncInsert']").attr("checked", $(this).attr("checked"));
			});

			$("#tRoleFunc").find("input[name='chkRoleFuncUpdate']").click(function () {
				if($(this).parent().parent().find("input[name='chkRoleFuncActive']").attr("checked") == false){
					return false;
				}
			});

			$("#tRoleFunc").find("input[name='chkRoleFuncInsert']").click(function () {
				if($(this).parent().parent().find("input[name='chkRoleFuncActive']").attr("checked") == false){
					return false;
				}
			});

            $("#btnSave").click(function () {
//				if (!window.confirm($g["ConfirmSave"])) {
//					return false;
//				}
			});

			$("#btnCancel").click(function (e) {
				e.preventDefault();
				window.parent.$("div.tab_container").find("ul>li").eq(0).click();
			});

			if($("#hUserType").attr("value") == "NS" && $("#hRoleType").attr("value") == "SY"){
				//$("#btnSave").attr("disabled","disabled");
				$("#btnSave").hide();
				//Added by jason in 20120329
				$("input[name='chkRoleFuncInsert']").unbind('click').click(function(){
					return false;
				});
				$("input[name='chkRoleFuncUpdate']").unbind('click').click(function(){
					return false;
				});
				$("input[name='chkRoleFuncActive']").unbind('click').click(function(){
					return false;
				});
				//Added by jason in 20120329
			}

			<%:Html.Message() %>
		});
	</script>
</head>
<body>
	<div class="centra">
		<%Html.BeginForm(); %>
		<%=Html.Hidden("hRoleId", Model.RoleModel.RoleID)%>
		<%=Html.Hidden("hUserType", App.Framework.Security.User.Current.UserType)%>
		<%=Html.Hidden("hRoleType", Model.RoleModel.RoleType)%>
		<div class="search">
			<table>
				<tr>
					<td class="w30  t_left">
						<%=Role.RoleText%>
					</td>
					<td class="w160 t_left">
						<label class="label_readonly" style="width:150px;"><%=Model.RoleModel.RoleCode %></label>
					</td>
					<td class="w60  t_center">
						<%=Role.RoleDSC%>
					</td>
					<td class="w310 t_left">
						<label class="label_readonly" style="width:300px;"><%=Model.RoleModel.RoleDsc %></label>
					</td>
					<%--<td class="w80 t_center">
						<%=Role.RoleType%>
					</td>
					<td class="w90 t_left">
						<label class="label_readonly" style="width:80px;">
							<% if (Model.RoleModel.RoleType == CSC.Business.RoleTypes.SY.ToString()){  %>
								<%= Html.Label(Role.RoleTypeSY)%>
							<%  }else{ %>
								<%= Html.Label(Role.RoleTypeSH)%>
							<%  } %>
						</label>
					</td>
					<td class="w30 t_center">
						<%=Role.ShopCode%>
					</td>
					<td class="w180 t_left">
						<label class="label_readonly" style="width:150px;"><%=Model.RoleModel.ShopName%></label>
					</td>--%>
					<td class="w120 t_center">
						<%=ShopText.BUCode%>
					</td>
					<td class="w180 t_left">
						<label class="label_readonly" style="width:150px;"><%=Model.RoleModel.BU_CODE%></label>
					</td>
						
					<%--<td class="w150  t_left">
						<%=Role.SystemScope%>
						<label class="label_readonly" style="width:80px;"><%=Model.RoleModel.SystemScope%></label>
					</td>--%>
					<%--<td class="w100 t_left">
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
						<th class="w40">
							<%=Role.Number %>
						</th>
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w100 t_left ">
							<%=Role.FunctionCode %>
						</th>
						<th class="w300 t_left">
							<%=Role.FunctionName%>
						</th>
						<%--<th class="w50 t_center">
							<%=Role.AdminFlag%>
						</th>--%>
						<th class="w100 t_left">
							<%=FunctionText.Function_Type%>
						</th>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
						
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="w100 t_left">
							<%=Role.FunctionCode%>
						</th>
						<th class="w80 t_left">
							<%=Role.FunctionType %>
						</th>
						<th class="w300 t_left">
							<%=Role.FunctionName%>
						</th>
						<th class="w50 t_center">
							<%=Role.AdminFlag %>
						</th>
						<th class="w100 t_left">
							<%=Role.SystemScope %>
						</th>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w50 t_center">
							<%=Role.InsertFlag %>
						</th>
						<th class="w40 t_center">
							<%=Role.UpdateFlag %>
						</th>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="w40 t_center">
							<%=Role.ActiveFlag %>
						</th>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w40 t_center">
							<%=Role.ActiveFlag %>
						</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
						<%foreach (var item in Model.RoleModel.RoleFuncList){%> 
						<tr>
							<td align="center">
								<%=Html.GridItemIndex()%>
							</td>
							<td>
								<%=item.FuncCode %>
							</td>
							<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
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
							<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
							<td>
								<%=item.Dsc %>
							</td>
							<%--<td align="center">
								<%= Html.CheckBox("chkRoleFuncAdmin", item.AdminFlag, new { @onclick = "return false;" })%>
							</td>--%>
							<td>
								<%=item.FuncType %>
							</td>
							<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
							<%--<td>
								<%=item.SystemScope %>
							</td>--%>
							<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
							<td align="center">
								<%:Html.CheckBox("chkRoleFuncInsert", item.FuncId.ToString(), item.InsertableFlag)%>
							</td>
							<td align="center">
								<%:Html.CheckBox("chkRoleFuncUpdate", item.FuncId.ToString(),item.UpdatableFlag)%>
							</td>
							<td align="center">
								<%:Html.CheckBox("chkRoleFuncActive", item.FuncId.ToString(), item.ActiveFlag)%>
							</td>
							<td></td>
						</tr> 
					<%} %>
				</tbody>
			</table>
		</div> 
		<div class="buttons" style="text-align:center;">
			<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Role_Edit))
						{ %>
			<% Html.Submit(GlobalText.BtnSaveText,"btnSave");%>
			<%} %>
			&nbsp;
			<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
		</div> 
		<%Html.EndForm(); %>
	</div>
</body>
</html>
