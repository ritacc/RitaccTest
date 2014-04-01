<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.UserViewModel>" %>

<%
	var isAdd = RouteData.Values["action"].ToString().Equals("Add", StringComparison.InvariantCultureIgnoreCase);
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftAjax.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftMvcValidation.js")%>" type="text/javascript"></script> 
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
    <%Html.EnableClientValidation(); %>
    <script type="text/javascript">
		window.checkFormChanged = true;
		var add = '<%:isAdd %>';
		window.btnSureClick = function (sucessed, redirectUrl) {
			if (sucessed) {
				if(add){
					window.parent.USER_ID_Before = '<%:Model.UserModel!=null?Model.UserModel.UserId:0 %>';
					window.parent.$(".tab_container").find("ul>li").eq(2).click();
					return;
				}
				var container = window.parent.$("div.tab_container");
				container.find("ul>li").eq(2).click();
//				loadFrame(window.parent.$("#tabList"), "List?UserSearch.Suspend=False", true);
			}
		};

		$(function () {
            <%:Html.Message() %>
			
			//added by Jason in 20120913 for remove user role list in add/edit page
			$("input[name='chkUserShopActive']").click(function () {
				var $this = $(this);
				$this.siblings("input[name='chkUserShopActive-Anti']").attr("checked", !$this.attr("checked"));
			});
			//end added by Jason in 20120913 for remove user role list in add/edit page
			
		    $("#btnSave").click(function () {
//				if (!window.confirm($g["ConfirmSave"])) {
//					return false;
//				}
			});


			if ($("#UserModel_UserId").attr("value") == 0) {
				$("#UserModel_UserCode").removeAttr("readonly").removeClass("in_readonly");
			}
			else{
				$("#UserModel_UserCode").attr("readonly", true).addClass("in_readonly");
			}

			if ($("#hUserType").attr("value") == "NS") {
				$("#UserModel_UserType").hide();
				$("#lblUserTypeNS").show();
			}
			else{
				$("#UserModel_UserType").show();
				$("#lblUserTypeNS").hide();
			}

			$("#btnCancel").click(function (e) {
				e.preventDefault();
				window.parent.$("div.tab_container").find("ul>li").eq(0).click();
			});

			$("#btnSave").click(function () {
				//added by jason in 20120308 for SYSTEM05#13.doc NO.1
				if($("#UserModel_UserId").val() == null || $("#UserModel_UserId").val() == ""){
					var chks =  $("#tUserShop").find(":checkbox:checked[name='chkUserShopActive']");
					if (chks.length <= 0) {
						alert($g["MustSelectShop"]);
						return false;
					}
				}

				//如果店-用户正在招揽分派里面已经被使用，必须要勾选
				var cbs =  $("#tUserShop").find(":checkbox[name='chkUserShopActive']");
				var ASSIGNMENT_FLAG;
				var flag = true;
				cbs.each(function () {
					ASSIGNMENT_FLAG = $(this).parent().find("input:hidden[name='hASSIGNMENT_FLAG']").val();
					if(ASSIGNMENT_FLAG == "Y" && $(this).attr("checked") == false)
					{
						flag = false;
						return false;
					}
				});
				if(flag == false){
					alert('<%:UserText.USER_USING %>');
					return false;
				}

				return true;
			});
		});

    </script>
</head>
<body>



	<%--added by Jason in 20120913 for remove user role list in add/edit page--%>
	<%Html.BeginForm(); %>
    <div class="centra">
        <%=Html.HiddenFor(m => m.UserModel.UserId)%>
        <%=Html.HiddenFor(m => m.UserModel.LastUpdateDate)%>
        <%=Html.HiddenFor(m => m.UserModel.LastUpdatedBy)%>
        <%=Html.Hidden("hUserType", App.Framework.Security.User.Current.UserType)%>
        <div class="search">
			<table class="edit">
				<tr>
					<td class="t">
						<%:UserText.UserCode%><span class="asterisk">＊</span>
					</td>
					<td>
						<%:Html.TextBoxFor(m => m.UserModel.UserCode, htmlAttributes: new { maxlength = 20, style = "width:100px;TEXT-TRANSFORM: uppercase" })%>
						<div>
							<%:Html.ValidationMessageFor(m => m.UserModel.UserCode)%></div>
					</td>
				</tr>
				<tr class="a">
					<td class="t">
						<%:UserText.UserName%><span class="asterisk">＊</span>
					</td>
					<td>
						<%:Html.TextBoxFor(m => m.UserModel.UserName, htmlAttributes: new { maxlength = 50, style = "width:300px;TEXT-TRANSFORM: uppercase" })%>
						<div>
							<%:Html.ValidationMessageFor(m => m.UserModel.UserName)%></div>
					</td>
				</tr>
				<tr>
					<td class="t">
						<%:UserText.UserType%><span class="asterisk">＊</span>
					</td>
					<td>
						<%=Html.DropDownListFor(m => m.UserModel.UserType, new List<SelectListItem>() { { new SelectListItem() { Text = UserText.UserTypeSA, Value = CSC.Business.UserTypes.SA.ToString() } }, { new SelectListItem() { Text = UserText.UserTypeNS, Value = CSC.Business.UserTypes.NS.ToString() } } })%>
						<label id="lblUserTypeNS" class="label_readonly" style="width: 80px;">
							<%= Html.Label(UserText.UserTypeNS)%>
						</label>
					</td>
				</tr>
			</table>
			<div style="height: 40px;">
				<table style="width: 100%;" class="wizTitle">
					<tr>
						<td class="wizTitle">
							<%:UserText.ShopCode %>
						</td>
					</tr>
				</table>
			</div>
		</div>
		<div class="grid_list">
			<table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides" id="tUserShop">
                <thead>
                    <tr>
                        <th class="w30">
                            <%=Role.ActiveFlag %>
                        </th>
                        <th class="w30">
                            <%=UserText.Number%>
                        </th>
						<th class="w120 t_left">
							<%=ShopText.BUCode%>
						</th>
                        <th class="w100 t_left">
                            <%=ShopText.Code%>
                        </th>
                        <th class="t_left">
                            <%=ShopText.Name%>
                        </th>
						<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
                        <%--<th class="t_left">
                            <%=ShopText.Area%>
                        </th>--%>
						<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>
						
						<%--added by jason in 20121219 for TSYF02010#06.doc--%>
						<th class="t_left">
                            <%=ShopText.FullName%>
                        </th>
						<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
                        <th class="w20">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% var index = 1; %>
                    <%foreach (var item in Model.UserShopList)
                      {%>
                    <tr>
                        <td align="center">
                            <%:Html.CheckBox("chkUserShopActive", item.Code, item.ActiveFlag)%>
							<%:Html.CheckBox("chkUserShopActive-Anti", item.Code, !item.ActiveFlag, new { style = "display:none;" })%>
                            <input type="hidden" name="hASSIGNMENT_FLAG" value="<%=item.ASSIGNMENT_FLAG %>" />
                        </td>
                        <td align="center">
                            <%=index%>
                        </td>
						 <td>
                            <%=item.BU_CODE%>
                        </td>
                        <td>
                            <%=item.Code %>
                        </td>
                        <td>
                            <%=item.Name %>
                        </td>
                        <td>
                            <%=item.FullName %>
                        </td>
                        <td></td>
                    </tr>
                    <% index++; ; %>
                    <%} %>
                </tbody>
            </table>
        </div>    
    </div>
	<div class="bottom dialog-buttons">
		<table width="100%">
			<tr>
				<td align="center">
					<% Html.Submit(GlobalText.BtnSaveText, "btnSave");%>
					&nbsp;
					<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
				</td>
			</tr>
		</table>
	</div>
	<%Html.EndForm(); %>
	<%--end added by Jason in 20120913 for remove user role list in add/edit page--%>

	<%--commented by Jason in 20120913 for remove user role list in add/edit page--%>
	<%--<div class="centra">
        <%Html.BeginForm(); %>
        <%=Html.HiddenFor(m => m.UserModel.UserId)%>
        <%=Html.HiddenFor(m => m.UserModel.LastUpdateDate)%>
        <%=Html.HiddenFor(m => m.UserModel.LastUpdatedBy)%>
        <%=Html.Hidden("hUserType", App.Framework.Security.User.Current.UserType)%>
        <table class="edit">
            <tr>
                <td class="t">
                    <%:UserText.UserCode%><span class="asterisk">＊</span>
                </td>
                <td>
                    <%:Html.TextBoxFor(m => m.UserModel.UserCode, htmlAttributes: new { maxlength = 20, style = "width:100px" })%>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.UserModel.UserCode)%></div>
                </td>
            </tr>
            <tr class="a">
                <td class="t">
                    <%:UserText.UserName%><span class="asterisk">＊</span>
                </td>
                <td>
                    <%:Html.TextBoxFor(m => m.UserModel.UserName, htmlAttributes: new { maxlength = 20, style = "width:300px" })%>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.UserModel.UserName)%></div>
                </td>
            </tr>
            <tr>
                <td class="t">
                    <%:UserText.UserType%><span class="asterisk">＊</span>
                </td>
                <td>
                    <%=Html.DropDownListFor(m => m.UserModel.UserType, new List<SelectListItem>() { { new SelectListItem() { Text = UserText.UserTypeSA, Value = CSC.Business.UserTypes.SA.ToString() } }, { new SelectListItem() { Text = UserText.UserTypeNS, Value = CSC.Business.UserTypes.NS.ToString() } } })%>
                    <label id="lblUserTypeNS" class="label_readonly" style="width: 80px;">
                        <%= Html.Label(UserText.UserTypeNS)%>
                    </label>
                </td>
            </tr>
        </table>
        <div>
            <table style="width: 100%;" class="wizTitle">
                <tr>
                    <td class="wizTitle">
                        <%:UserText.ShopCode %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="grid_list" style="height: 190px;">
            <table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides" id="tUserShop">
                <thead>
                    <tr>
                        <th class="w30">
                            <%=Role.ActiveFlag %>
                        </th>
                        <th class="w30">
                            <%=UserText.Number%>
                        </th>
                        <th class="w100 t_left">
                            <%=ShopText.Code%>
                        </th>
                        <th class="t_left">
                            <%=ShopText.Name%>
                        </th>
                        <th class="t_left">
                            <%=ShopText.Area%>
                        </th>
                        <th class="w20">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% var index = 1; %>
                    <%foreach (var item in Model.UserShopList)
                      {%>
                    <tr>
                        <td align="center">
                            <%:Html.CheckBox("chkUserShopActive", item.Code, item.ActiveFlag)%>
                            <input type="hidden" name="hASSIGNMENT_FLAG" value="<%=item.ASSIGNMENT_FLAG %>" />
                        </td>
                        <td align="center">
                            <%=index%>
                        </td>
                        <td>
                            <%=item.Code %>
                        </td>
                        <td>
                            <%=item.Name %>
                        </td>
                        <td>
                            <%=item.FullName %>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <% index++; ; %>
                    <%} %>
                </tbody>
            </table>
        </div>
        <div>
            <table style="width: 100%;" class="wizTitle">
                <tr>
                    <td class="wizTitle">
                        <%:UserText.RoleCode %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="grid_list" style="height: 190px;">
            <table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides" id="tUserRole">
                <thead>
                    <tr>
                        <th class="w30">
                            <%=Role.ActiveFlag %>
                        </th>
                        <th class="w30">
                            <%=UserText.Number%>
                        </th>
                        <th class="w100 t_left">
                            <%=Role.RoleCode%>
                        </th>
                        <th class="t_left">
                            <%=Role.RoleDSC%>
                        </th>
                        <th class="w80 t_left">
                            <%=Role.RoleType%>
                        </th>
                        <th class="w80 t_left">
                            <%=Role.ShopCode%>
                        </th>
                        <th class="w30">
                            <%=Role.AdminFlag %>
                        </th>
                        <th class="w20">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%foreach (var item in Model.UserRoleList)
                      {%>
                    <tr>
                        <td align="center">
                            <%:Html.CheckBox("chkUserRoleActive", item.RoleID.ToString(), item.ActiveFlag)%>
                        </td>
                        <td align="center">
                            <%=Html.GridItemIndex()%>
                        </td>
                        <td>
                            <%=item.RoleCode %>
                        </td>
                        <td>
                            <%=item.RoleDsc %>
                        </td>
                        <td>
                            <% if (item.RoleType == CSC.Business.RoleTypes.SY.ToString())
                               {  %>
                            <%= Html.Label(Role.RoleTypeSY)%>
                            <%  }
                               else
                               { %>
                            <%= Html.Label(Role.RoleTypeSH)%>
                            <%  } %>
                        </td>
                        <td align="left">
                            <%=item.ShopName %>
                        </td>
                        <td align="center">
                            <%:Html.CheckBox("chkIsAdminFlag", item.AdminFlag.ToString(), item.AdminFlag, new { @onclick = "return false;" })%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
        <div style="text-align: center;">
            <% Html.Submit(GlobalText.BtnSaveText, "btnSave");%>
            &nbsp;
            <% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
        </div>
        <%Html.EndForm(); %>
    </div>--%>
	<%--end commented by Jason in 20120913 for remove user role list in add/edit page--%>
</body>
</html>
