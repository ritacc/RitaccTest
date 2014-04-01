<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.RoleListModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script> 
	<script type="text/javascript">
		<%Html.EnableSortScript(); %>
		function goToPage(index) {
			window.parent.$("div.tab_container").find("ul>li").eq(index).click();

		}

		$(function () {
			
			(function () { // tool button click
				$("div.btn > a").click(function (e) {
					var href = $(this).attr("href");
					var cls = $(this).attr("class");
					var cbs = $(":checkbox:checked:not(:disabled)[name='cbKey']");
					var userType = cbs.parent().find("input:hidden[name='hUserType']").val();
					var roleType = cbs.parent().find("input:hidden[name='hRoleType']").val();
					switch (cls) {
						case "tool_view":
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							else {
								goToPage(1);
								break;
							}
						case "tool_new":
							//没有勾选
							if (cbs.length >= 1) {
								//勾选后点新增，进入新增页面，同时清除勾选，不让进入rolefunc,userRole页面。
								tr.click.call(cbs.parent().parent());
							}
							$("#hEditType").val("A");
							goToPage(1);
							$("#hEditType").val("");
							break;
						case "tool_edit":
							var isFrozenFlag = cbs.parent().parent().find(":checkbox[name='chkIsFrozenFlag']").val();
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							else {
								if (isFrozenFlag == "True") { alert($g["FrozenEditError"]); break; }

								//公司用户不能编辑系统层面的角色
								if(userType == "NS" && roleType == "SY"){
									alert($g["NotEditRoleByNS"]);
									break;
								}
								else{
									$("#hEditType").val("E");
									goToPage(1);
									$("#hEditType").val("");
									break;
								}
							}
						case "tool_delete":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							if (window.confirm($g["DeleteConfirm"])) {
								href = "<%=Url.Content("~/Role/Delete?id=-id-")%>";
								var deleteDiv = getDeleteDiv();
								return loadFrame(deleteDiv, href.replace(/-id-/ig, cbs.val()), true, true);
							}
							break;
						case "tool_freezeGroup":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							var isFrozenFlag = cbs.parent().parent().find(":checkbox[name='chkIsFrozenFlag']").val();
							if (isFrozenFlag == "True") {
								alert($g["HasFrozen"]);
							}
							else {
								//公司用户不能冻结系统层面的角色
								if(userType == "NS" && roleType == "SY"){
									alert($g["NotFrozenRoleByNS"]);
									break;
								}
								else{
									if (window.confirm($g["FrozenConfirm"])) {
										document.location.assign(href.replace(/-id-/ig, cbs.val()));
									}
								}
							}
							break;
						case "tool_cancelFreezeGroup":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							var isFrozenFlag = cbs.parent().parent().find(":checkbox[name='chkIsFrozenFlag']").val();
							if (isFrozenFlag == "False") {
								alert($g["NoFrozen"]);
							}
							else {
								//公司用户不能取消冻结系统层面的角色
								if(userType == "NS" && roleType == "SY"){
									alert($g["NotCancelFrozenRoleByNS"]);
									break;
								}
								else{
									if (window.confirm($g["CancelFrozenConfirm"])) {
										document.location.assign(href.replace(/-id-/ig, cbs.val()));
									}
								}
							}
							break;
					};

					e.preventDefault();
				});
			})();
		});
	</script>
</head>
<body>
	<div class="centra">
		<div class="search">
			<%Html.BeginForm("List", "Role", FormMethod.Get);%>
			<table>
				<tr>
					<td class="w80 t_left">
						<%=Role.RoleCode%><font color="red">(%)</font>
					</td>
					<td class="w150 t_left">
						<%:Html.TextBoxFor(m => m.RoleSearch.ROLE_CODE, htmlAttributes: new { maxlength = 20, style = "width:150px;TEXT-TRANSFORM: uppercase" })%>
					</td>
					<%--<td class="w150 t_left">
						<%=Html.DropDownListFor(m => m.RoleSearch.RoleDescFrom, Model.RoleForDropDownList.ToSelectList(m => m.RoleDsc, m => m.RoleDsc).AddDefaultItem(), htmlAttributes: new { style = "width:150px" })%>
					</td>
					<td class="w30 t_center">
						<%=Role.To%>
					</td>
					<td class="w150 t_left">
						<%=Html.DropDownListFor(m => m.RoleSearch.RoleDescTo, Model.RoleForDropDownList.ToSelectList(m => m.RoleDsc, m => m.RoleDsc).AddDefaultItem(), htmlAttributes: new { style = "width:150px" })%>
					</td>--%>
					<%--<td class="w60 t_center">
						<%=Role.RoleType%>
					</td>
					<td class="w100 t_left">
						<%=Html.DropDownListFor(m => m.RoleSearch.RoleType, new List<SelectListItem>() { { new SelectListItem() { Text = Role.RoleTypeSY, Value = CSC.Business.RoleTypes.SY.ToString() } }, { new SelectListItem() { Text = Role.RoleTypeSH, Value = CSC.Business.RoleTypes.SH.ToString() } } }.AddDefaultItem(), htmlAttributes: new { style = "width:100px" })%>
					</td>--%>
					<%--<td class="w50 t_center">
						<%=Role.AdminFlag%>
					</td>
					<td class="w100 t_left">
						<%=Html.SelectYesOrNoFor(m => m.RoleSearch.AdminFlag,new { style = "width:60px" })%>
					</td>--%>
					<td class="t_left">
						<input type="submit" value="<%:GlobalText.BtnSearchText %>" />
						<%Html.Button(GlobalText.BtnResetText, "reset", Url.Content("~/Role/List"));%>
					</td>
				</tr>
			</table>
			<%Html.EndForm(); %>
		</div>
		<div class="menu">
			<ul>
				<li class="b">
					<div class="btn">
						<%=Html.Hidden("hEditType") %>
						<a class="tool_view" href="#" id="tool_view"><%:GlobalText.BtnViewText%></a>
						<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Role_Add))
						{ %>
							<%=Html.ActionLink(GlobalText.BtnNewText, "Add", "Role", new { @roleId = "-id-" }, new { @class = "tool_new" })%>
						<%} %>
						<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Role_Edit))
						{ %>
							<%=Html.ActionLink(GlobalText.BtnEditText, "Edit", "Role", new { @roleId = "-id-" }, new { @class = "tool_edit" })%>
							<a id="tool_delete" class="tool_delete" title="<%=GlobalText.BtnDeleteText %>" href="#"><%=GlobalText.BtnDeleteText %></a>
							<%=Html.ActionLink(GlobalText.Frozen, "Frozen", "Role", new { @roleId = "-id-", @frozenFlag = true }, new { @class = "tool_freezeGroup" })%>
							<%=Html.ActionLink(GlobalText.CancelFrozen, "Frozen", "Role", new { @roleId = "-id-", @frozenFlag = false }, new { @class = "tool_cancelFreezeGroup" })%>
						<%} %>
					</div>
				</li>
			</ul>
		</div>
		<div class="grid_list">
			<table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides">
				<thead>
					<tr>
						<th class="w40">
						</th>
						<th class="w40">
							<%=Role.Number %>
						</th>
						<th class="w120 t_left">
							<%=ShopText.BUCode%>
						</th>
						<th class="w150 t_left sort">
							<%Html.Sort(Role.RoleCode, "List", 0);%>
						</th>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="w200 t_left sort">
							<%Html.Sort(Role.RoleDSC, "List", 1);%>
						</th>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w200 t_left">
							<%=Role.RoleDSC%>
						</th>
						<%--<th class="w70 t_left sort">
							<%Html.Sort(Role.RoleType, "List", 1);%>
						</th>
						<th class="w150 t_left">
							<%=Role.ShopCode%>
						</th>--%>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="w80 t_left sort">
							<%Html.Sort(Role.SystemScope, "List", 3);%>
						</th>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="w50 sort">
							<%Html.Sort(Role.AdminFlag, "List", 2);%>
						</th>--%>
						<th class="w90 t_left sort">
							<%Html.Sort(Role.CreationDate, "List", 1);%>
						</th>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="w50 sort">
							<%Html.Sort(Role.FrozenFlag, "List", 4);%>
						</th>
						<th class="w90 t_left sort">
							<%Html.Sort(Role.FrozenDate, "List", 5);%>
						</th>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w50">
							<%=Role.FrozenFlag%>
						</th>
						<th class="w90 t_left">
							<%=Role.FrozenDate%>
						</th>
						<th class="t_left">
						</th>
					</tr>
				</thead>
				<tbody>
					<%foreach (var item in Model.RoleList){%>
					<tr>
						<td style="text-align: center;">
							<input type="checkbox" name="cbKey" value="<%=item.RoleID %>" />
							<input type="hidden" name="hRoleType" value="<%=item.RoleType %>" />
							<input type="hidden" name="hUserType" value="<%=item.UserType %>" />
						</td>
						<td style="width: 40px;" align="center">
							<%=Html.GridItemIndex()%>
						</td>
						<td>
                            <%=item.BU_CODE%>
                        </td>
						<td align="left">
							<%=item.RoleCode %>
						</td>
						<td align="left">
							<%=item.RoleDsc %>
						</td>
						<%--<td align="left">
							<% if (item.RoleType == CSC.Business.RoleTypes.SY.ToString()){  %>
								<%= Html.Label(Role.RoleTypeSY)%>
							<%  }else{ %>
								<%= Html.Label(Role.RoleTypeSH)%>
							<%  } %>
						</td>
						<td align="left">
							<% if (item.RoleType == CSC.Business.RoleTypes.SH.ToString()){%>
								<%= item.ShopCode + "-" + item.ShopName %>
							<% } %>
						</td>--%>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<td align="left">
							<%=item.SystemScope %>
						</td>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<td align="center">
							<%:Html.CheckBox("chkIsAdminFlag", item.AdminFlag.ToString(), item.AdminFlag, new { @onclick = "return false;" })%>
						</td>--%>
						<td>
							<%= item.CreationDate.Format()%>
						</td>
						<td align="center">
							<%--<%= Html.CheckBox("chkIsFrozenFlag", item.FrozenFlag, new { @onclick = "return false;" })%>--%>
							<%:Html.CheckBox("chkIsFrozenFlag", item.FrozenFlag.ToString(), item.FrozenFlag, new { @onclick = "return false;" })%>
						</td>
						<td>
							<%=item.FrozenDate.HasValue ? item.FrozenDate.Value.Format():""%>
						</td>
						<td>
						</td>
					</tr>
					<%} %>
				</tbody>
			</table>
		</div>
		<div class="pager">
			<%=Html.QuickPager()%>
		</div>
	</div>
	
</body>
</html>
