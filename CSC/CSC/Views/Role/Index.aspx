<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CSC.Business.RoleListModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server"> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonDialog.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonChoose.js")%>" type="text/javascript"></script> 
	

	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<style type="text/css">
		.tab_content
		{
			height: 100%;
		}
	</style>
	<script type="text/javascript">
		$(function () {
			/*前端框架改动无需再用此句
			$(window).resize(function () {
				$(".tab_container").height($(".centra").height() - 10);

			}).resize();*/

			$(".tab_container").tabs({ onselect: function (div, i) {
				var childFrame = $("#tabList").find("iframe");
				var cbs;
				if (i > 0) {
					cbs = $(childFrame[0].contentWindow.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
				}
				switch (i) {
					case 0:
						return loadFrame(div);
						break;
					case 1:
						var editType = $(childFrame[0].contentWindow.document).find("#hEditType").val();
						if (cbs.length !== 1) {//新增
							//没有勾选时:如果是点击LIST里面的新增则进入新增页面；直接点击TAB PAGE不能进入
							if (editType == "A") {
								return loadFrame(div, "Add?roleId=" + cbs.val(), true);
								break;
							}
							else {
								alert($g["SelectSingle"]);
								return false;
								break;
							}
						}
						else {
							//编辑
							if (editType == "E") {
								var userType = cbs.parent().find("input:hidden[name='hUserType']").val();
								var roleType = cbs.parent().find("input:hidden[name='hRoleType']").val();
								var isFrozenFlag = cbs.parent().parent().find(":checkbox[name='chkIsFrozenFlag']").val();

								if (isFrozenFlag == "True") {
									alert($g["FrozenEditError"]);
									return false;
									break;
								}

								//公司用户不能编辑系统层面的角色
								if (userType == "NS" && roleType == "SY") {
									alert($g["NotEditRoleByNS"]);
									return false;
									break;
								}
								else {
									return loadFrame(div, "Edit?roleId=" + cbs.val(), true);
									break;
								}
							}
							else {//检视
								return loadFrame(div, "View?roleId=" + cbs.val(), true);
								break;
							}
						}
					case 2:
						var adminFlag = cbs.parent().parent().find(":checkbox[name='chkIsAdminFlag']").val();
						if (cbs.length !== 1) {
							alert($g["SelectSingle"]);
							return false;
							break;
						}
						else {
							return loadFrame(div, "RoleFunc?roleId=" + cbs.val() + "&adminFlag=" + adminFlag, true);
						}
						break;
					case 3:
						if (cbs.length !== 1) {
							alert($g["SelectSingle"]);
							return false;
							break;
						}
						else {
							return loadFrame(div, "UserRole?roleId=" + cbs.val(), true);
						}
						break;
				}
			}
			});
		});

	</script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

	<div class="tab_container customerInit">
		<ul>
			<li>
				<%--<%:Html.ActionLink(Role.ListTab, "List", "Role", null, new { @id = "tabList", @class="tab"})%>--%>
				<a href="#"><%=Role.ListTab%></a>
			</li>
			<li>
				<%--<%:Html.ActionLink(Role.EditTab, "Edit", "Role", new { @roleId = "-id-" }, new { @id = "tabEdit", @class = "tab" })%>--%>
				<a href="#"><%=Role.EditTab%></a>
			</li>
			<li>
				<%--<%:Html.ActionLink(Role.RoleFuncTab, "RoleFunc", "Role", new { @roleId = "-id-" }, new { @id = "tabRoleFunc", @class = "tab" })%>--%>
				<a href="#"><%=Role.RoleFuncTab%></a>
			</li>
			<li>
				<%--<%:Html.ActionLink(Role.UserRoleTab, "UserRole", "Role", new { @roleId = "-id-" }, new { @id = "tabUserRole", @class = "tab" })%>--%>
				<a href="#"><%=Role.UserRoleTab%></a>
			</li>
		</ul>
		<div href="List" id="tabList">
		</div>
		<div href="Edit" id="tabEdit">
		</div>
		<div href="RoleFunc" id="tabRoleFunc">
		</div>
		<div href="UserRole" id="tabUserRole">
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>
