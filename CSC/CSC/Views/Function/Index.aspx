<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CSC.Business.FunctionListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
	<script type="text/javascript">
		<%Html.EnableSortScript(); %>

		var dlgEdit=null;
		$(document).ready(function () {
			//新增,编辑,删除
			$("div.btn > a").click((function (e) { 
				//显示弹出框标题
				var title = $(this).attr("title"); 
				var href = $(this).attr("href");
				var toolsId = $(this).attr("id");
				var cbs = $("div.grid_list :checkbox:checked:not(:disabled)[name='cbKey']");
				switch (toolsId) {
					case "tool_edit":
						if (cbs.length !== 1) {
							alert($g["SelectSingle"]);
							break;
						}
						else {
							dlgEdit = popDialog(href.replace(/-FuncId-/ig, cbs.val()),"FunctionEdit",520,240,title);
							break;
						}
				}; 
				e.preventDefault();
			}));
		});
	</script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div class="search">
		<%Html.BeginForm("Index", "Function", FormMethod.Get);%>
		<table>
			<tr>
				<td class="t_left">
					<%=FunctionText.Function_Id %><font color="red">(%)</font>
				</td>
				<td class ="w150 t_left">
					<%=Html.TextBoxFor(m => m.SearchFunction.FunctionCode, new { style = "width:100px;" })%>
				</td>
				<td class="t_left">
					<%=FunctionText.Function_Name %><font color="red">(%)</font>
				</td>
				<td class ="w250 t_left">
					<%=Html.TextBoxFor(m => m.SearchFunction.FunctionDSC,  new { style = "width:200px;" })%>
				</td>
				<td class="t_left">
					<%=FunctionText.Function_Management %>
				</td>
				<td class ="w60 t_left">
					<%=Html.SelectYesOrNoFor(m => m.SearchFunction.AdminFlag, htmlAttribues: new { style = "width:50px;" })%>
				</td>
				<td class="w150 t_right">
					<input type="submit" value="<%:GlobalText.BtnSearchText %>" />
					<%Html.Button(GlobalText.BtnResetText, "reset", Url.Content("~/Function/IndexDefault"));%>
				</td>
			</tr>
		</table>
		<%Html.EndForm();%>
	</div>
	<%if (App.Framework.Security.User.Current.UserType == CSC.Business.UserTypes.SA.ToString()){%>
	<div class="menu">
		<ul>
			<li class="b">
				<div class="btn">
					<%if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Function_Edit)){ %>
						<%=Html.ActionLink(GlobalText.BtnEditText, "Edit", "Function", new { @FuncId = "-FuncId-" }, new { @class = "tool_edit", @id = "tool_edit", @Title = FunctionText.Title_Edit })%>
					<%} %>
				</div>
			</li>
		</ul>
	</div>
	<%} %>
	<div class="grid_list">
		<table>
			<thead>
				<tr>
					<th class= "w20 t_center">
					</th>
					<th class="w40 t_center">
						<%:FunctionText.Function_Number%>
					</th>
					<th class= "w100 t_center sort">
						<%Html.Sort(FunctionText.Function_Id, "Index", 0);%>
					</th>
					<th class= "w700 t_left sort">
						<%Html.Sort(FunctionText.Function_Name, "Index", 1);%>
					</th>
					
					<%--<th class= "w100 t_center sort">
						<%Html.Sort(FunctionText.Function_System, "Index",2);%>
					</th>--%>
					<th class= "w100 t_center">
						<%:FunctionText.Function_Management%>
					</th>
					<th class= "w80 t_left sort">
						<%Html.Sort(FunctionText.Function_Type, "Index", 2);%>
					</th>
					<th class= "w300 t_left">
						<%:FunctionText.Function_Url%>
					</th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<% if(Model.FunctionList != null && Model.FunctionList.Count > 0){ %>
				<%foreach (var item in Model.FunctionList){%>
					<tr>
						<td class= "t_center">
							<input type="checkbox" name="cbKey" value="<%=item.FuncId%>"/>
						</td>
						<td class="t_center">
							<%=Html.GridItemIndex()%>
						</td>
						<td class= "t_center">
							<%=item.FuncCode%>
						</td>
						<td class= "t_left">
							<%=item.Dsc%>
						</td>
						<%--<td class= "t_center">
							<%=item.SystemScope%>
						</td>--%>
						<td class= "t_center">
							<%=Html.CheckBox("chkAdminFlag",item.AdminFlag, new { @OnClick = "return false;" }) %>
						</td>
						<td class= "t_left">
							<%=item.FuncType%>
						</td>
						<td class= "t_left">
							<%=item.Executable%>
						</td>
						<td></td>
					</tr>
				<%}%>
				<%}%>
			</tbody>
		</table>
	</div>
	<div class="pager">
		<%=Html.QuickPager()%>
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>
