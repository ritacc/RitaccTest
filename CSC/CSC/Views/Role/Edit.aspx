<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.RoleListModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
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

		window.btnSureClick = function (sucessed, redirectUrl) {
			if (sucessed) {
				var container = window.parent.$("div.tab_container");
				container.find("ul>li").eq(0).click();
				loadFrame(window.parent.$("#tabList"), "List", true);
			}
		};
              
		window.ShopList = '<%=Url.Action("ShopList", "Role")%>';

		$(function () {
            <%:Html.Message() %>
            
               $("#btnSave").click(function () {
//					if (!window.confirm($g["ConfirmSave"])) {
//						return false;
//					}
				});
		    
			if ($("#RoleModel_RoleID").attr("value") == 0) {
				$("#RoleModel_RoleCode").removeAttr("readonly").removeClass("in_readonly");
				//公司用户新建角色时，层面=公司，店=当前的店
				if($("#hUserType").attr("value") == "NS"){
					$("#RoleModel_RoleType").hide();
					$("#RoleModel_RoleType").attr("readonly", true).addClass("in_readonly");
					$("#RoleModel_ShopName").attr("readonly", true).addClass("in_readonly");
				
					$("#lblRoleTypeSY").hide();
					$("#lblRoleTypeSH").show();
					$("#lblShopCode").show();
					$("#RoleModel_ShopName").show();
					$("#imgShopLov").hide();
					$("#RoleModel_ShopCode").val($("#hShopCode").attr("value"));
					$("#RoleModel_ShopName").val($("#hShopName").attr("value"));
				}
				else{
					$("#lblRoleTypeSY").hide();
					$("#lblRoleTypeSH").hide();
					$("#RoleModel_RoleType").show();
					$("#RoleModel_RoleType").removeAttr("readonly").removeClass("in_readonly");
					$("#RoleModel_RoleType").val("SY");
					$("#RoleModel_ShopName").attr("readonly", true).addClass("in_readonly");
					$("#lblShopCode").hide();
					$("#RoleModel_ShopName").hide();
					$("#imgShopLov").hide();

					//Added by Jason in 20120308 for SYSTEM05#13.doc
					$("#RoleModel_ShopCode").val($("#hShopCode").attr("value"));
					$("#RoleModel_ShopName").val($("#hShopName").attr("value"));
				}
			}
			else {
				$("#RoleModel_RoleCode").attr("readonly", true).addClass("in_readonly");
				$("#RoleModel_RoleType").hide();
				$("#RoleModel_RoleType").attr("readonly", true).addClass("in_readonly");
				$("#RoleModel_ShopName").attr("readonly", true).addClass("in_readonly");
				
				if ($("#RoleModel_RoleType").val() == "SY" || $("#hRoleModel_RoleType").val() == "SY") {
					$("#lblRoleTypeSY").show();
					$("#lblRoleTypeSH").hide();

					$("#lblShopCode").hide();
					$("#RoleModel_ShopName").hide();
				}
				else{
					$("#lblRoleTypeSY").hide();
					$("#lblRoleTypeSH").show();
					$("#lblShopCode").show();
					$("#RoleModel_ShopName").show();
				}
				$("#imgShopLov").hide();
			}

//			$("#RoleModel_RoleType").change(function () {
//				if ($("#RoleModel_RoleType").val() == "SY") {
//					$("#RoleModel_ShopName").val("");
//					$("#RoleModel_ShopCode").val("");
//					$("#RoleModel_ShopName").attr("readonly", true).addClass("in_readonly");
//					$("#lblShopCode").hide();
//					$("#RoleModel_ShopName").hide();
//					$("#imgShopLov").hide();
//				}
//				else {
//					$("#RoleModel_ShopName").attr("readonly", true).removeClass("in_readonly");
//					$("#lblShopCode").show();
//					$("#RoleModel_ShopName").show();
//					$("#imgShopLov").show();
//					//Added by Jason in 20120308 for SYSTEM05#13.doc
//					$("#RoleModel_ShopCode").val($("#hShopCode").attr("value"));
//					$("#RoleModel_ShopName").val($("#hShopName").attr("value"));
//				}
//			});

            var shopCodeControl = $("#RoleModel_ShopCode");
			var shopnameControl = $("#RoleModel_ShopName");
			$("#imgShopLov").click(function (e) {
				e.preventDefault();
				var ShopCode = shopCodeControl.val(); 
				ChooseDialogDiv(window.ShopList, { ShopCode:ShopCode  }, "Shop", "1000px", "545px", $g["ShopLovTitle"], false, function (trsArry, cbsArry, currentObj) {
					//多选时，应该将isMultipleChoose参数设置成true
					//			for (var a in cbsArry) {
					//				//如果是多选，此处应该将cbsArry[a].value以及$(cbsArry[a]).parent().find("input:hidden[name='hName']").val()使用逗号拼接起来，然后返回父页面
					//				alert(cbsArry[a].value);
					//				alert($(cbsArry[a]).parent().find("input:hidden[name='hName']").val());
					//			}

          
					//单选，应该将isMultipleChoose参数设置成false.给父页面赋值
					shopCodeControl.val(cbsArry.val());
					shopnameControl.val(cbsArry.parent().find("input:hidden[name='hName']").val());

				});
			});

			$("#btnCancel").click(function (e) {
				e.preventDefault();

				//window.parent.$("div.tab_container").tabs({ index: 0 });
				window.parent.$("div.tab_container").find("ul>li").eq(0).click();
			});

//			$("#btnSave").click(function () {
//				if ($("#RoleModel_RoleType").val() == "SH" && $("#RoleModel_ShopName").val() == "") {
//					$("#RoleModel_ShopName_validationMessage").text($g["SHOP_MUST_SELECT"]); 
//					$("#RoleModel_ShopName_validationMessage").show();
//					return false;
//				}

//				return true;
//			});
		});

	</script>
</head>
<body>
    <div>
		<%Html.BeginForm(); %>
		<%=Html.HiddenFor(m => m.RoleModel.RoleID)%>
		<%=Html.HiddenFor(m => m.RoleModel.LastUpdateDate)%>
		<%=Html.HiddenFor(m => m.RoleModel.LastUpdatedBy)%>
		<%=Html.Hidden("hUserType", App.Framework.Security.User.Current.UserType)%>
		<%=Html.Hidden("hShopCode", App.Framework.Security.User.Current.ShopCode)%>
		<%=Html.Hidden("hShopName", App.Framework.Security.User.Current.ShopName)%>
		<table class="edit">
			<tr>
				<td class="t">
					<%:Role.RoleCode%><span class="asterisk">＊</span>
				</td>
				<td>
					<%:Html.TextBoxFor(m => m.RoleModel.RoleCode, htmlAttributes: new { maxlength = 20, style = "width:150px;TEXT-TRANSFORM: uppercase"})%>
					<div><%:Html.ValidationMessageFor(m => m.RoleModel.RoleCode)%></div>
				</td>
			</tr>
			<tr class="a">
				<td class="t">
					<%:Role.RoleSDSC%><span class="asterisk">＊</span>
				</td>
				<td>
					<%:Html.TextBoxFor(m => m.RoleModel.RoleSdsc, htmlAttributes: new { maxlength = 20, style = "width:150px" })%>
					<div><%:Html.ValidationMessageFor(m => m.RoleModel.RoleSdsc)%></div>
				</td>
			</tr>
			<tr>
				<td class="t">
					<%:Role.RoleDSC%><span class="asterisk">＊</span>
				</td>
				<td>
					<%:Html.TextBoxFor(m => m.RoleModel.RoleDsc, htmlAttributes: new { maxlength = 50, style = "width:300px" })%>
					<div><%:Html.ValidationMessageFor(m => m.RoleModel.RoleDsc)%></div>
				</td>
			</tr>
			<%--<tr class="a">
				<td class="t">
					<%:Role.RoleType%><span class="asterisk">＊</span>
				</td>
				<td>
					<label id="lblRoleTypeSY" class="label_readonly" style="width:80px;">
						<%= Html.Label(Role.RoleTypeSY)%>
					</label>
					<label id="lblRoleTypeSH" class="label_readonly" style="width:80px;">
						<%= Html.Label(Role.RoleTypeSH)%>
					</label>
					<%:Html.DropDownListFor(m => m.RoleModel.RoleType
											, new List<SelectListItem>() {{ new SelectListItem() { Text = Role.RoleTypeSY, Value = CSC.Business.RoleTypes.SY.ToString() } }
																		, { new SelectListItem() { Text = Role.RoleTypeSH, Value = CSC.Business.RoleTypes.SH.ToString() } } }
											, htmlAttributes: new { @id = "RoleModel_RoleType" })%>
					<%if (Model != null && Model.RoleModel != null){ %>
						<%=Html.Hidden("hRoleModel_RoleType", Model.RoleModel.RoleType)%>
					<% }else{%>
						<%=Html.Hidden("hRoleModel_RoleType", "")%>
					<%} %>
					<label id="lblShopCode"><%=Role.ShopCode%></label>
					<%:Html.TextBoxFor(m => m.RoleModel.ShopName, htmlAttributes: new { maxlength = 20, style = "width:150px" })%>
					<% =Html.HiddenFor(m => m.RoleModel.ShopCode)%>
					<img src='<%=Url.Content("~/Content/Css/Images/Lov.png")%>' id="imgShopLov" onmouseover="this.style.cursor ='pointer';" />
					<div><span style="color:Red;"><%:Html.ValidationMessageFor(m => m.RoleModel.ShopName)%></span></div>
				</td>
			</tr>
			<tr>
				<td class="t">
					<%:Role.AdminFlag%>
				</td>
				<td>
					<% if (Model != null && Model.RoleModel != null && Model.RoleModel.AdminFlag)
					{  %>
						<%=Html.CheckBoxFor(m => m.RoleModel.AdminFlag, new { @onclick = "return false;" })%>
					<%  }
					 else
					 { %>
						<%=Html.CheckBoxFor(m => m.RoleModel.AdminFlag)%>
					<%  } %>
				</td>
			</tr>--%>
		</table>
		<div class="bottom" style="text-align:center;">
				<% Html.Submit(GlobalText.BtnSaveText,"btnSave");%>
				&nbsp;
				<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel",null); %>
		</div>
		<%Html.EndForm(); %>
	</div>
</body>
</html>
