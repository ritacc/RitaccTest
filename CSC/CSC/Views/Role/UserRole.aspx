<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.RoleListModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Grid.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Tabs.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	
	<script type="text/javascript">
		<%Html.EnableSortScript(); %>
	</script>
</head>
<body>
	<div class="centra">
		<div class="search">
			<table style="margin-top:10px; margin-bottom:10px;">
				<tr>
					<td class="w30  t_left">
						<%=Role.RoleText%>
					</td>
					<td class="w160 t_left">
						<label class="label_readonly" style="width:150px;"><%=Model.RoleModel.RoleCode %></label>
					</td>
					<td class="w60 t_center">
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
							<% if (Model.RoleModel.RoleType == CSC.Business.RoleTypes.SY.ToString())
							{  %>
							<%= Html.Label(Role.RoleTypeSY)%>
							<%  }
							else
							{ %>
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
					<%--<td class="w150  t_left">
						<%=Role.SystemScope%>
						<label class="label_readonly" style="width:80px;"><%=Model.RoleModel.SystemScope%></label>
					</td>--%>

					<td class="w120 t_center">
						<%=ShopText.BUCode%>
					</td>
					<td class="w180 t_left">
						<label class="label_readonly" style="width:150px;"><%=Model.RoleModel.BU_CODE%></label>
					</td>

					<%--<td class="w100  t_left">
						<%=Role.AdminFlag%>
						<%= Html.CheckBox("chkAdminFlag", Model.RoleModel.AdminFlag, new { @onclick = "return false;" })%>
					</td>--%>
				</tr>
			</table>
		</div>
		<div class="grid_list">
			<table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides">
				<thead>
					<tr>
						<th class="w40 t_center">
							<%=Role.Number %>
						</th>
						<%--<th class="w120 t_left">
							<%=ShopText.BUCode%>
						</th>--%>
						<th class="w100 t_left sort">
							<%Html.Sort(ShopText.Code, "UserRole", 0); %>
						</th>
						<th class="w150  t_left">
							<%=ShopText.Name%>
						</th>
						<th class="w150 t_left sort">
							<%Html.Sort(Role.UserType, "UserRole", 1); %>
						</th>
						<th class="w150 t_left sort">
							<%Html.Sort(Role.UserCode, "UserRole", 2); %>
						</th>
						<th class="w400 t_left">
							<%=Role.UserName %>
						</th>
						<th>
						</th>
					</tr>
				</thead>
				<tbody>
						<%foreach (var item in Model.RoleModel.UserRoleList)
					{%> 
						<tr>
							<td>
								<%=Html.GridItemIndex()%>
							</td>
							<%--<td>
								<%=item.BU_CODE%>
							</td>--%>
							<td>
								<%=item.ShopCode %>
							</td>
							<td>
								<%=item.ShopName %>
							</td>
							<td>
								<% if (item.UserType == CSC.Business.UserTypes.SA.ToString()){  %>
									<%= Html.Label(Role.UserTypeSA)%>
								<%  }else{ %>
									<%= Html.Label(Role.UserTypeNS)%>
								<%  } %>
							</td>
							<td>
								<%=item.UserCode %>
							</td>
							<td>
								<%=item.UserName %>
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
