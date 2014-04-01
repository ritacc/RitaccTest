<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.ShopListModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/jQuery/dropdownEx.css")%>" rel="stylesheet" type="text/css" />

	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdownEx.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>


	<%=Html.SetCalendarFormat() %>
	<script type="text/javascript">
        window.checkFormChanged = true;
		var dlgEdit = null;
		<%Html.EnableSortScript(); %>
			function goToPage(index) {
				window.parent.$("div.tab_container").find("ul>li").eq(index).click();		
				}
			$(document).ready(function ()  { // tool button click
				$("div.btn > a").click(function (e) {
					var ids = $(this).attr("id");
					var cbs = $(":checkbox:checked:not(:disabled)[name='cbKey']");				
					switch (ids) {
						case "tool_edit":
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							else {
								window.parent.editClickedFalg =true;
								goToPage(1);
								break;
							}
						case "tool_SetDefult":
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							href = "<%=Url.Content("~/Shop/SetDefaultGodown?CODE=-WHCode-")%>";
							dlgEdit = popDialog(href.replace(/-WHCode-/ig, cbs.val()),"SetDefaultGodown",450,350,"Set Default Godown");
							break;
						case "tool_setpwd":
							if (cbs.length !== 1) {
								alert($g["SelectSingle"]);
								break;
							}
							href = '<%=Url.Content("~/Shop/SetPassWord?CODE=-WHCode-")%>';
							dlgEdit = popDialog(href.replace(/-WHCode-/ig, cbs.val()),"SetPassWord",400,200,"Set Password");
						break;
					};
					e.preventDefault();
					return false;
				});
			});		
	</script>
<//head>
<body>
<div class="centra">
	<div class="search">
			<%Html.BeginForm("ShopList", "Shop", FormMethod.Get);%>
				<table>
					<tr>
						<td class="t_left">
							<%=ShopText.Code%>
                            <font color="red">(%)</font>
						</td>
						<td class="w100 t_left">
							<%=Html.TextBoxFor(m => m.Search.Code, htmlAttributes: new { maxlength = 10, style = "width:80px" })%>
						</td>                        
						<td class="t_left">
							<input type="submit" value="<%:GlobalText.BtnSearchText %>" />
							<%Html.Button(GlobalText.BtnResetText, "reset", Url.Content("~/Shop/ShopList"));%>                           
						</td>
					</tr>
				</table>
		<%Html.EndForm(); %>
	</div>
	<div class="menu">
			<ul>
				<li class="b">
					<div class="btn">
					 <%
						 if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Shop_Edit))
						 {%>
						   <a class="tool_edit" href="#" id="tool_edit"><%:GlobalText.BtnEditText%></a>
						 <% }%>

						 <% if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Shop_Edit)){%>
							<a class="tool_edit" href="#" id="tool_SetDefult"><%=ShopText.BtnSetDefult %></a>
						 <% }%>

						 <% if (QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Shop_Edit)){%>
							<a class="tool_edit" href="#" id="tool_setpwd"><%=ShopText.Password %></a>
						 <% }%>
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
					<th class="w120 t_left">
						<%=ShopText.BUCode%>
					</th>
					<th class="w120 t_left sort">
						<%Html.Sort(ShopText.Code, "ShopList", 0);%>
					</th>
					<th class="w200 t_left sort">
						<%Html.Sort(ShopText.Name, "ShopList", 1);%>
					</th>
					<th class="w300 t_left">
						<%=ShopText.FullName%>
					</th>
					<th class="w120 t_left">
						<%=ShopText.ShopType%>
					</th>
					<th class="w120 t_left">
						<%= ShopText.DefultGodownSHOW %>
					</th>
					<th></th>
				</tr>            
			</thead>
			<tbody>  
		   <%if (Model != null && Model.List != null) { %>            
				<%foreach (var item in Model.List) {%>
				<tr>
					<td>
						<input type="checkbox" name="cbKey" value="<%=item.Code %>" />
					</td>
					<td>
						<%=item.BU_CODE%>
					</td>
					<td>
						<%=item.Code%>
					</td>
					<td>
						<%=item.Name%>
					</td>
					<td>
						<%=item.FullName%>                                
					</td>
					<td>
						<%=item.SHOP_TYPE%>                                
					</td>
					<td>
						<%=item.DEFUALT_GODOWN %>
					</td>
					<td></td>
				</tr>
				<%}
			 }%>
			</tbody>
		</table>
	
	</div>
	
	<div class="pager">
		<%=Html.QuickPager()%>
	</div>
</div>

</body>
</html>
