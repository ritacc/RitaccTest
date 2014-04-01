<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CSC.Business.ShopViewModel>" %>
<% var model = Model as CSC.Business.ShopViewModel; %>
<div class="grid_list dialog_grid_list" style="height: 500px">
    <table>
        <thead>
            <tr class="t">
                <th class="w20"></th>
                <th class="w120 t_left">
                    <%=ShopText.Code%>
                </th>
                <th class="t_left">
                    <%=ShopText.Name%>
                </th>
                <th class="w20"></th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in model.ShopList) { %>
				<tr>
					<% var a = Request.Form["ShopCode"].ToString();
					   bool radiochecked = (a==item.Code);%>
					<td>
						<%if (radiochecked){ %>
							<%--<input checked="checked" type="radio" name="cbKey1" value="<%=item.Code %>" />--%>
							<input checked="checked" type="checkbox" name="cbKey" value="<%=item.Code %>" />
						<%}else { %>
							<%--<input type="radio" name="cbKey1" value="<%=item.Code %>" />--%>
							<input  type="checkbox" name="cbKey" value="<%=item.Code %>" />
						<%} %>
						<input type="hidden" name="hName" value="<%=item.Name %>" />
					</td>
					<td>
						<%=item.Code%>
					</td>
					<td>
						<%=item.Name%>
					</td>
					<td></td>
				</tr>
            <% } %>
        </tbody>
    </table>
</div>
<div class="pager">
	<%=Html.QuickPager()%>
</div>
<div class="bottom dialog-buttons">
    <% Html.Button(GlobalText.PagerBtnJumpText, id: "btnChoose"); %>
    &nbsp;
    <% Html.Button(GlobalText.btnCancelText, id: "btnClose"); %>
</div>
<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonDialog.js")%>" type="text/javascript"></script>



