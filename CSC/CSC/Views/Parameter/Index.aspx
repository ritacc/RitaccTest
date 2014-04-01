<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CSC.Business.ParameterListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">  
	<script type="text/javascript"> 
		<%Html.EnableSortScript(); %>
	</script>
	<link  href="<%=Url.Content("~/Content/Css/Shared/Form.css")%>" rel="stylesheet" type="text/css" /> 
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.NumericTextbox.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script> 

	<%Html.EnableClientValidation(); %>
	<script type="text/javascript" language="javascript">
	    window.checkChanged = true;
		window.btnSureClick = function (sucessed, redirectUrl) {
		    if (sucessed) { 
		        document.location.assign('<%=Url.Content("~/Parameter/Index")%>');
			}
		};

		$(document).ready(function () {
		    $("#butSavePara").click(function () {
		        if (!window.confirm($g["ConfirmSave"])) {
		            return false;
		        }
		    });

		    $("#btnCancel").click(function () {
		        if (!window.confirm($g["SureLeave1"])) {
		            return false;
		        }
		        location.reload();
		        return false;
		    });

		    var lastValue = "";

		    $(".tmp").focus(function () {
		        lastValue = $(this).attr("accesskey");
		    });

		    $(".tmp").blur(function () {
		        var tmpID = this.name;
		        var arr = tmpID.split(",");
		        var type = arr[1];
		        var id = arr[0];
		        var code = arr[2];
		        var lastUpdateDate = arr[3];
		        var v = /^[0-9]*$/;

		        if (type == "N" && !this.value.match(v)) {
		            alert($g["EnterNumber"]);
		            this.style.color = "red";
		            this.focus();
		            //$("#butSavePara").attr({ "disabled": "disabled" });
		            this.value = lastValue;  //如果输入值不是数值型 则将原来的值赋予文本框
		            return false;
		        } else {
		            this.style.color = "";
		            if (lastValue != this.value) {
		            	$.ajax({
		                	url: '<%=Url.Content("~/Parameter/GetValue")%>?paraID=' + encodeURI(id) + '&code=' + encodeURI(code) + '&value=' + encodeURI(this.value) + '&lastUpdateDate=' + encodeURI(lastUpdateDate),
		                    type: "POST",
		                    dataType: "text",
		                    success: function (data) {

		                    },
		                    error: function (oXmlHttpReq, textStatus, errorThrown) {
		                        alert("failed to load data!");
		                    }
		                });
		            }
		        }
		    });
		}); 
		 
	</script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centra"> 
        <%using (Html.BeginForm("Edit", "Parameter")) { %> 

        <div class="grid_list">
            <table width="100%" border="1" cellspacing="1" cellpadding="1" frame="hsides">
                <thead>
                    <tr> 
						<th style="text-align:center;width:40px;">
							<%= CSC.Resources.Parameter.Number %>
						</th>
                        <th align="left" style="width:80px;"> 
							<%= CSC.Resources.Parameter.UserType%> 
                        </th>
                        <th align="left" style="width:60px;"> 
							<%= CSC.Resources.Parameter.Module%> 
                        </th>
						<th align="left" style="width:160px;"> 
							<%= CSC.Resources.Parameter.Code%>
							<%--<%Html.Sort(CSC.Resources.Parameter.Code, "Index", 0); %>--%>
                        </th>
                        <th align="left" style="width:60px;"> 
							<%= CSC.Resources.Parameter.ParaType%>
							<%--<%Html.Sort(CSC.Resources.Parameter.ParaType, "Index", 1); %>--%>
                        </th> 
                        <th align="left" style="width:210px;">
                            <%= CSC.Resources.Parameter.Value%>
                        </th> 
                        <th  align="left" style="width:600px;">
                            <%= CSC.Resources.Parameter.DetailDSC%>
                        </th>
                        <th  align="left" style="width:350px;">
                            <%= CSC.Resources.Parameter.Dsc%>
                        </th> 
                        <th></th>
                    </tr>
                </thead>
                <tbody> 
                    <%  
                        //是否有编辑权限
                        bool isPermissioins = QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Parameter_Edit);
              
                        //当前用户类型(SA-管理员  NS-店用户)
                        string CurrentUserType = App.Framework.Security.User.Current.UserType;
              
						if (Model.List != null && Model.List.Count > 0)
						{
                            //是否是车主会 模块参数
                            bool isMC = false;
							foreach (var item in Model.List)
							{
                                if (isMC)
                                {
                                    continue;
                                }
                                
                                if (item.Module.Trim() == "车主会" && item.Code.Trim().ToUpper() == "MC_ENABLED" && item.Value.Trim().ToUpper() != "Y")
                                {
                                    isMC = true;
                                } 
                                
                                %>
								<tr> 
									<td style="text-align:center;width:40px;">
										<%=Html.GridItemIndex()%> 
									</td>
                                    <td align="left">
										<%: item.UserType.ToUpper().Trim() == "SA" ? CSC.Resources.Parameter.SA : CSC.Resources.Parameter.NS%>
									</td>
                                    <td align="left">
										<%: item.Module%>
									</td>
									<td align="left">
										<%: item.Code%>
									</td>
									<td align="left">
										<% if (item.ParaType == "N") {  %>
										<%= Html.Label("Numeric")%>
										<%  }else if(item.ParaType == "D"){ %>
											<%= Html.Label("Date")%> 
										<%  }else{ %>
											<%= Html.Label("Character")%>
										<%  } %>
									</td>
									<td align="left">
                                        <% if (!isPermissioins) { %>
                                            <div class="label_readonly" style="width:204px;"><%= item.Value%></div>
                                        <%} else{%>
										    <% if (string.IsNullOrEmpty(item.ShopCode) && item.ParaID == 0){  %>
											    <div class="label_readonly" style="width:204px;"><%= item.Value%></div>
										    <% } else { %>  

                                                <% if (CurrentUserType == CSC.Business.UserTypes.SA.ToString()) { %>
                                                    <% if (item.ParaType == "N") {  %>
												        <%Html.NumericTextbox(item.ParaID.ToString() + "," + item.ParaType + "," + item.Code + "," + item.LastUpdateDate, item.Value, htmlAttributes: new { @class = "tmp", @style = "width:200px;", @accesskey = item.Value, maxlength = 30 }, numericParams: new NumericParams() { hideUpDownBtn = true }); %>
											        <%  }else if(item.ParaType == "D"){ %>
												            <%Html.Calendar(item.ParaID.ToString() + "," + item.ParaType + "," + item.Code + "," + item.LastUpdateDate, item.Value, htmlAttributes: new { @class = "tmp", @style = "width:180px;", @accesskey = item.Value, maxlength = 30 }, calendarParams: new CalendarParams() { }); %><br />
											        <%  }else{ %>
												            <%= Html.TextBox(item.ParaID.ToString() + "," + item.ParaType + "," + item.Code + "," + item.LastUpdateDate, item.Value, new { @class = "tmp", style = "width:200px;", @accesskey = item.Value, maxlength = 30 })%>
											        <%  } %>
                                                <% } else if (CurrentUserType == CSC.Business.UserTypes.NS.ToString() && item.UserType == "NS") { %>
                                                    <% if (item.ParaType == "N") {  %>
												        <%Html.NumericTextbox(item.ParaID.ToString() + "," + item.ParaType + "," + item.Code + "," + item.LastUpdateDate, item.Value, htmlAttributes: new { @class = "tmp", @style = "width:200px;", @accesskey = item.Value, maxlength = 30 }, numericParams: new NumericParams() { hideUpDownBtn = true }); %>
											        <%  } else if (item.ParaType == "D") { %>
												        <%Html.Calendar(item.ParaID.ToString() + "," + item.ParaType + "," + item.Code + "," + item.LastUpdateDate, item.Value, htmlAttributes: new { @class = "tmp", @style = "width:180px;", @accesskey = item.Value, maxlength = 30 }, calendarParams: new CalendarParams() { }); %><br />
											        <%  } else { %>
												        <%= Html.TextBox(item.ParaID.ToString() + "," + item.ParaType + "," + item.Code + "," + item.LastUpdateDate, item.Value, new { @class = "tmp", style = "width:200px;", @accesskey = item.Value, maxlength = 30 })%>
											        <%  } %>
                                                <% } else {%>
                                                    <div class="label_readonly" style="width:204px;"><%= item.Value%></div>
                                                <% } %> 

										    <% } %> 
                                        <% } %>  
									</td> 
                                    <td align="left"> 
										<%: item.Remarks%>
									</td>
									<td align="left"> 
										<%: item.Dsc%>
									</td>
                                    <td></td>
								</tr>
							<% 
							} 
					}%>
                </tbody>
            </table>
        </div>  

		<div class="pager">
            <% 
              if(QuickInvoke.GetCurrentUserHasPermission(EnumPermission.Parameter_Edit))
              { 
            %>
                <% Html.Submit(CSC.Resources.GlobalText.BtnSaveText,id:"butSavePara", url: Url.Action("Edit")); %>
			    &nbsp;
			    <%:Html.ButtonX(CSC.Resources.GlobalText.BtnResetText, "btnCancel")%>
            <% 
              } 
            %>
        </div> 
		
        <%} %>  
    </div> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ButtonContent" runat="server">
 
</asp:Content>
