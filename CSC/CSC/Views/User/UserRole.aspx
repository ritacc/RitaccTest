<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.UserViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
		
		window.checkChanged=function(){
			//如果店-用户正在招揽分派里面已经被使用，必须要勾选
		    if(!isNeedCheckChanged)
		        return true;
			var cbs =  $("#tUserRole").find(":checkbox[name='chkUserRoleActive']");
			var hActiveFlag;
			var flag = true;
			cbs.each(function () {
				hActiveFlag = $(this).parent().find("input:hidden[name='hActiveFlag']").val();
				
				if($(this).attr("checked").toString().toUpperCase() != hActiveFlag.toUpperCase()){
					flag = false;
					return false;
				}
			});
			return flag;
		};

		window.isNeedCheckChanged = true;
		window.onbeforeunload = function(){
			//角色有效性发生变化
			if(checkChanged() == false && isNeedCheckChanged){
				return '<%:GlobalText.SureLeave %>';
			}
		};

		var dlgEdit =null;
		$(function () {
			window.btnSureClick = function (sucessed, redirectUrl) {
//commented by jason in 20121219 for TSYF02010#06.doc				
//				if (sucessed) {
//					var container = window.parent.$("div.tab_container");
//					container.find("ul>li").eq(0).click();
//					loadFrame(window.parent.$("#tabList"), "List", true);
//				}
//end commented by jason in 20121219 for TSYF02010#06.doc
			};

			<% if (App.Framework.Security.User.Current.UserType != CSC.Business.UserTypes.NS.ToString()){  %>
				if($('#ShopRoleSearch_ShopCode option').length == 0)
				{
					$("#btnSave").hide();
				}
			<%} %>

			$("#ShopRoleSearch_ShopCode").change(function (e) {
				//角色有效性发生变化
				if(checkChanged() == false){
					if(window.confirm('<%:GlobalText.SureLeave %>'))
					{
						isNeedCheckChanged = false;
						$("#ShopRoleSearch")[0].submit();
					}
					else
					{
						isNeedCheckChanged = true;
						//将改变之前的数据找回
						$("#ShopRoleSearch_ShopCode").val($("#hShopCode").val());
						return false;
					}
				}
				else
				{
					isNeedCheckChanged = false;
					$("#ShopRoleSearch")[0].submit();
				}
			});

			$("#ShopRoleSearch_ShopCode").click(function (e) {
				//改变店之前先保存DDL里面的值
				$("#hShopCode").val($(this).val());
			});

			$("#btnCancel").click(function (e) {
				isNeedCheckChanged = true;
				e.preventDefault();
				window.parent.$("div.tab_container").find("ul>li").eq(0).click();
			});

			$("#btnViewFunc").click(function (e) {
				e.preventDefault();
				var cbs = $(":checkbox:checked:not(:disabled)[name='cbKey']");
				if (cbs.length !== 1) {
					alert($g["SelectSingle"]);
				
				}
				else {
					//var href= "/User/RoleFunc?roleId="+cbs.val();  
					var href= '<%=Url.Content("~/User/RoleFunc?roleId=")%>'+cbs.val();
					dlgEdit = popDialog(href, "viewEdit", 1000, 600, $(this).attr("value"));
				}
				return false;
			});

			$("#btnSave").click(function(e){
				isNeedCheckChanged = false;
//			    if (!window.confirm($g["ConfirmSave"])) {
//			        return false;
//			    }
			});
			<%:Html.Message() %>
		});
	</script>
</head>
<body>
	<%=Html.Hidden("hShopCode","")%>
	<div class="centra">
		<div class="search">
			<%Html.BeginForm("UserRole", "User", FormMethod.Get, new { @id = "ShopRoleSearch" });%>
			<div>
				<%=Html.Hidden("userId",Request["userId"])%>
				<table>
					<tr>
						<td class="w190  t_left">
							<%=UserText.UserCode%>
							<label class="label_readonly" style="width: 120px;">
								<%=Model.UserModel.UserCode %></label>
						</td>
						<td class="w300  t_left">
							<%=UserText.UserName%>
							<label class="label_readonly" style="width: 200px;">
								<%=Model.UserModel.UserName%></label>
						</td>
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<td class="w180 t_left">
							<%:UserText.UserType%>
							<% if (Model.UserModel.UserType == CSC.Business.UserTypes.NS.ToString()){%>
								<label id="lblUserTypeNS" class="label_readonly" style="width: 80px;">
									<%= Html.Label(UserText.UserTypeNS)%>
								</label>
							<% }else{ %>
								<label id="lblUserTypeSA" class="label_readonly" style="width: 80px;">
									<%= Html.Label(UserText.UserTypeSA)%>
								</label>
							<% } %>
						</td>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
						<td class="w300  t_left">
							<%=UserText.ShopCode%>
							<% if (App.Framework.Security.User.Current.UserType == CSC.Business.UserTypes.NS.ToString()){  %>
								<label id="lblShopName" class="label_readonly" style="width: 200px;">
									<%=App.Framework.Security.User.Current.ShopName%>
								</label>
							<%  }else{ %>
								<%=Html.DropDownListFor(m => m.ShopRoleSearch.ShopCode, Model.ShopForDropDownList.ToSelectList(m => string.Format("{0}-{1}",m.Code, m.Name), m => m.Code).AddDefaultItem(new SelectListItem{Text="",Value = ""}), htmlAttributes: new { style = "width:200px" })%>
							<%  } %>
						</td>
					</tr>
				</table>
			</div>
			<%Html.EndForm(); %>
		</div>
		<%Html.BeginForm(); %>
		<%=Html.Hidden("hUserId",Request["userId"])%>
		<%=Html.Hidden("hShopCode", Request["ShopRoleSearch.ShopCode"])%>
		<div class="grid_list">
			<table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides" id="tUserRole">
				<thead>
					<tr>
						<th class="w30">
							<% =Role.ActiveFlag %>
						</th>
						<th class="w30">
							<%=UserText.Number%>
						</th>
						<th class="w120 t_left">
							<%=ShopText.BUCode%>
						</th>
						<th class="w100 t_left">
							<%=Role.RoleCode%>
						</th>
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w200 t_left">
							<%=Role.RoleSDSC%>
						</th>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="w300 t_left">
							<%=Role.RoleDSC%>
						</th>
						<th class="w80 t_left">
							<%=Role.RoleType%>
						</th>
						<th class="w120 t_left">
							<%=Role.ShopCode%>
						</th>
						<th class="w60">
							<%=Role.AdminFlag %>
						</th>
						<th class="w80">
							<%=UserText.ViewRoleFunc%>
						</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					<%if (Model.UserRoleShopList != null){%>
					<%foreach (var item in Model.UserRoleShopList){%>
					<tr>
						<td align="center">
							<%:Html.CheckBox("chkUserRoleActive", item.RoleID.ToString(), item.ActiveFlag)%>
							<input type="hidden" name="hActiveFlag" value="<%=item.ActiveFlag %>" />
						</td>
						<td align="center">
							<%=Html.GridItemIndex()%>
						</td>
						 <td>
                            <%=item.BU_CODE%>
                        </td>
						<td>
							<%=item.RoleCode%>
						</td>
						<td>
							<%=item.RoleSDsc%>
						</td>
						<td>
							<%=item.RoleDsc%>
						</td>
						<td>
							<% if (item.RoleType == CSC.Business.RoleTypes.SY.ToString()){  %>
								<%= Html.Label(Role.RoleTypeSY)%>
							<%  }else{ %>
								<%= Html.Label(Role.RoleTypeSH)%>
							<%  } %>
						</td>
						<td align="left">
							<% if (item.RoleType == CSC.Business.RoleTypes.SH.ToString()){  %>
								<%=item.ShopCode + "-" +item.ShopName%>							
							<%  } %>
						</td>
						<td align="center">
							<%:Html.CheckBox("chkIsAdminFlag", item.AdminFlag.ToString(), item.AdminFlag, new { @onclick = "return false;" })%>
						</td>
						<td style="text-align: center;">
							<input type="checkbox" name="cbKey" value="<%=item.RoleID %>" />
						</td>
						<td>
						</td>
					</tr>
					<%} %>
					<%} %>
				</tbody>
			</table>
		</div>
		<div class="pager">
			<%=Html.QuickPager()%>
		</div>
		<div class="buttons" style="text-align: center;">
			<% Html.Submit(GlobalText.BtnSaveText, "btnSave");%>
			&nbsp;
			<% Html.Button(UserText.ViewRoleFunc, "btnViewFunc"); %>
			&nbsp;
			<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
		</div>
		<%Html.EndForm(); %>
	</div>
</body>
</html>
