<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.UserViewModel>" %>

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
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
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
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							else {
								$("#hEditType").val("E");
								goToPage(1);
								$("#hEditType").val("");
								break;
							}
						case "tool_delete":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							if (window.confirm($g["DeleteConfirm"])) {
								href = "<%=Url.Content("~/User/Delete?id=-id-")%>";
								var deleteDiv = getDeleteDiv();
								return loadFrame(deleteDiv, href.replace(/-id-/ig, cbs.val()), true, true);
							}
							break;
						case "tool_changePassword":
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							else {
								window.parent.location.assign(href.replace(/-id-/ig, cbs.val()));
								break;
							}
						case "tool_cancelFreezeGroup":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							var isCancelFrozenFlag = cbs.parent().parent().find(":checkbox[name='chkFrozenFlag']").val();
							if (isCancelFrozenFlag == "False") {
								alert($g["NoLock"]);
							}
							else {
								if (window.confirm($g["CancelLockConfirm"])) {
									document.location.assign(href.replace(/-id-/ig, cbs.val()));
								}
							}
							break;
						case "tool_suspend":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							var isSuspendFlag = cbs.parent().parent().find(":checkbox[name='chkSuspendFlag']").val();
							if (isSuspendFlag == "True") {
								alert($g["HasSuspend"]);
							}
							else {
								if (window.confirm($g["SuspendConfirm"])) {
									document.location.assign(href.replace(/-id-/ig, cbs.val()));
								}
							}
							break;
						case "tool_cancelSuspend":
							if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
							var isCancelSuspendFlag = cbs.parent().parent().find(":checkbox[name='chkSuspendFlag']").val();
							if (isCancelSuspendFlag == "False") {
								alert($g["NoSuspend"]);
							}
							else {
								if (window.confirm($g["CancelSuspendConfirm"])) {
									document.location.assign(href.replace(/-id-/ig, cbs.val()));
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
			<%Html.BeginForm("List", "User", FormMethod.Get);%>
			<table>
				<tr>
					<td class="w240  t_left">
						<%=UserText.UserCode%><font color="red">(%)</font>
						<%--<%=Html.DropDownListFor(m => m.UserSearch.UserId, Model.UserForDropDownList.ToSelectList(m => m.UserCode, m => m.UserId).AddDefaultItem(), htmlAttributes: new { style = "width:150px" })%>--%>
						<%=Html.TextBoxFor(m => m.UserSearch.USER_CODE, new { maxlength = 20, @style = "width:150px;;TEXT-TRANSFORM: uppercase" })%>	
					</td>
					<td class="w240 t_left">
						<%=UserText.UserName%><font color="red">(%)</font>
						<%:Html.TextBoxFor(m => m.UserSearch.UserName, htmlAttributes: new { maxlength = 20, style = "width:150px" })%>
					</td>
					<td class="w220 t_left">
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<%=UserText.RoleCode%>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<%=UserText.SystemRole%>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
						<%=Html.DropDownListFor(m => m.UserSearch.RoleID, Model.RoleForDropDownList.ToSelectList(m => m.RoleDsc, m => m.RoleID).AddDefaultItem(), htmlAttributes: new { style = "width:150px" })%>
					</td>
					<%--added by jason in 20121219 for TSYF02010#06.doc--%>
					<td class="w130 t_left">
						<%=UserText.SuspendFlag %>
                        <%= Html.DropDownListFor(m => m.UserSearch.Suspend, new SelectListItem[]
                        { 
						    new SelectListItem { Text = "All", Value = "A" },
						    new SelectListItem { Text = "Noraml", Value = "N" },
						    new SelectListItem { Text = "Suspend", Value = "S" } 
                        }, new  { @style="width:80px;"})%>  
						<%--<%=Html.CheckBoxFor(m => m.UserSearch.Suspend, htmlAttributes: new { @id = "chkSalesCntc" })%>--%>
						<%--<%:Html.CheckBox("chkSalesCntc", Model.UserSearch.SuspendFlag != null && Model.UserSearch.SuspendFlag == "Y")%>--%>
					</td>
					<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
					<td class="t_left">
						<input type="submit" value="<%:GlobalText.BtnSearchText %>" />
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<%Html.Button(GlobalText.BtnResetText, "reset", Url.Content("~/User/List"));%>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<%Html.Button(GlobalText.BtnResetText, "reset", Url.Content("~/User/List") + "?UserSearch.SuspendFlag=True");%>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
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
						<%if (App.Framework.Security.User.Current.UserType == CSC.Business.UserTypes.SA.ToString()){%>
							<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.User_Add)){ %>
								<%=Html.ActionLink(GlobalText.BtnNewText, "Edit", "User", new { @userId = "-id-" }, new { @class = "tool_new" })%>
							<%} %>
							<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.User_Edit)){ %>
								<%=Html.ActionLink(GlobalText.BtnEditText, "Edit", "User", new { @userId = "-id-" }, new { @class = "tool_edit" })%>
								<a id="tool_delete" class="tool_delete" title="<%=GlobalText.BtnDeleteText %>" href="#"><%=GlobalText.BtnDeleteText %></a>
								<%=Html.ActionLink(Account.Password, "PasswordChange", "Account", new { @userId = "-id-" }, new { @class = "tool_changePassword" })%>
								<%=Html.ActionLink(GlobalText.CancelLock, "CancelFrozen", "User", new { @userId = "-id-" }, new { @class = "tool_cancelFreezeGroup" })%>
								<%=Html.ActionLink(GlobalText.Suspend, "Suspend", "User", new { @userId = "-id-", @suspendFlag = true }, new { @class = "tool_suspend" })%>
								<%=Html.ActionLink(GlobalText.CancelSuspend, "Suspend", "User", new { @userId = "-id-", @suspendFlag = false }, new { @class = "tool_cancelSuspend" })%>
							<%} %>
						<% } %>
						<%if(App.Framework.Security.User.Current.UserType == CSC.Business.UserTypes.NS.ToString()){%>
							<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.User_Edit)){ %>
								<%=Html.ActionLink(GlobalText.BtnEditText, "Edit", "User", new { @userId = "-id-" }, new { @class = "tool_edit" })%>
								<%=Html.ActionLink(GlobalText.CancelLock, "CancelFrozen", "User", new { @userId = "-id-" }, new { @class = "tool_cancelFreezeGroup" })%>
							<%} %>
						<%} %>					
					</div>
				</li>
			</ul>
		</div>
		<div class="grid_list">
			<table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides">
				<thead>
					<tr>
						<th class="w30">
						</th>
						<th class="w30">
							<%=UserText.Number %>
						</th>
						<th class="w150 t_left sort">
							<%Html.Sort(UserText.UserCode, "List", 0);%>
						</th>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<th class="t_left sort">
							<%Html.Sort(UserText.UserName, "List", 1);%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.UserType, "List", 2);%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.CreationDate, "List", 3);%>
						</th>
						<th class="w60 sort">
							<%Html.Sort(UserText.ShopCount, "List", 4);%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.SystemScope, "List", 5);%>
						</th>
						<th class="w30 sort">
							<%Html.Sort(UserText.SuspendFlag, "List", 6);%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.SuspendDate, "List", 7);%>
						</th>
						<th class="w30 sort">
							<%Html.Sort(UserText.FrozenFlag, "List", 8);%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.FrozenDate, "List", 9);%>
						</th>--%>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						
						
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<th class=" t_left">
							<%=UserText.UserName%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.UserType, "List", 1);%>
						</th>
						<th class="w80 t_left sort">
							<%Html.Sort(UserText.CreationDate, "List", 2);%>
						</th>
						<%--<th class="w60 sort">
							<%Html.Sort(UserText.ShopCount, "List", 3);%>
						</th>--%>
						<th class="w60">
							<%=UserText.SuspendFlag%>
						</th>
						<th class="w80 t_left">
							<%=UserText.SuspendDate%>
						</th>
						<th class="w60">
							<%=UserText.FrozenFlag%>
						</th>
						<th class="w80 t_left">
							<%=UserText.FrozenDate%>
						</th>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w20"></th>
					</tr>
				</thead>
				<tbody>
					<%foreach (var item in Model.UserList)
					{%>
					<tr>
						<td style="text-align: center;">
							<input type="checkbox" name="cbKey" value="<%=item.UserId %>" />
						</td>
						<td style="width: 40px;" align="center">
							<%=Html.GridItemIndex()%>
						</td>
						<td align="left">
							<%=item.UserCode %>
						</td>
						<td align="left">
							<%=item.UserName %>
						</td>
						<td align="left">
							<% if (item.UserType == CSC.Business.UserTypes.SA.ToString()){  %>
								<%= Html.Label(UserText.UserTypeSA)%>
							<%  }else{ %>
								<%= Html.Label(UserText.UserTypeNS)%>
							<%  } %>
						</td>
						<td>
							<%= item.CreationDate.Format()%>
						</td>
						<%--<td align="center">
							<%=item.ShopCount %>
						</td>--%>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
						<%--<td align="left">
							<%=item.SystemScope %>
						</td>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						<td align="center">
							<%:Html.CheckBox("chkSuspendFlag", item.SuspendFlag.ToString(), item.SuspendFlag, new { @onclick = "return false;" })%>
						</td>
						<td>
							<%=item.SuspendDate.HasValue ? item.SuspendDate.Value.Format() : ""%>
						</td>
						<td align="center">
							<%:Html.CheckBox("chkFrozenFlag", item.FrozenFlag.ToString(), item.FrozenFlag, new { @onclick = "return false;" })%>
						</td>
						<td>
							<%=item.FrozenDate.HasValue ? item.FrozenDate.Value.Format() : ""%>
						</td>
						<td></td>
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
