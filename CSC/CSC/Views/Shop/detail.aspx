<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Shop>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
		<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
		<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
		<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
        <link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
		
		$(document).ready(function() {
			
			$("#btnCancel").click(function () {
				window.parent.$("div.tab_container").find("ul>li").eq(0).click();
				return false;
			});
		});
	</script>
  
	<style type="text/css">
		.asterisk
		{
			color: Red;
			font-weight: bold;
			padding: 0px 5px;
		}
	</style>
</head>
<body>
    <%Html.BeginForm(); %>
	
	
	<div class="centra">
		<table class="edit"> 
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.Code %>
				</td>
				<td align="left">					
					<label class="label_readonly" style="width: 200px;"><%= Model.Code %></label>					
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Name%>					
				</td>
				<td align="left">
                <label class="label_readonly" style="width:200px;"><%:Model.Name %></label>					 
				</td> 
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.FullName%>
				</td>
				<td align="left">
                   <label class="label_readonly" style="width:300px;"><%:Model.FullName%></label>			
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.AddrLine1%>
				</td>
				<td align="left">
                  <label class="label_readonly" style="width:300px;"><%:Model.AddrLine1%></label>			
				</td>
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.AddrLine2%>
				</td>
				<td align="left">
                 <label class="label_readonly" style="width:300px;"><%:Model.AddrLine2%></label>			
				</td>
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Province%>
				</td>
				<td align="left">
                 <label class="label_readonly" style="width:150px;"><%:Model.ProvName%></label>
				</td> 
			</tr>
			<%--<tr class="a">
				<td align="right" class="t">
					<%=ShopText.City%>
				</td>
				<td align="left"> 
                  <label class="label_readonly" style="width:150px;"><%:Model.CityName%></label>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Area%>
				</td>
				<td align="left">
                 <label class="label_readonly" style="width:150px;"><%:Model.AreaName%></label>
                </td> 
			</tr>--%>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.PhoneNo1%>
				</td>
				<td align="left">
                   <label class="label_readonly" style="width:150px;"><%:Model.PhoneNo1%></label>
					 
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.PhoneNo2%>
				</td>
				<td align="left">
                   <label class="label_readonly" style="width:150px;"><%:Model.PhoneNo2%></label>
				 
				</td> 
			</tr>
			<tr class="a">
				<td align="right">
					<%=ShopText.PhoneAreaCode%>
				</td>
				<td align="left">
                   <label class="label_readonly" style="width:150px;"><%:Model.PhoneAreaCode%></label>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Fax%>
				</td>
				<td align="left">
                <label class="label_readonly" style="width:150px;"><%:Model.Fax%></label>
				</td> 
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.PostalCode%>
				</td>
				<td align="left">
                  <label class="label_readonly" style="width:150px;"><%:Model.PostalCode%></label>
				</td> 
			</tr>
			<tr>
				<td align="right" valign="top">
					<%=ShopText.Email%>
				</td>
				<td align="left"> 
                    <%--<%:Html.TextArea("Email", Model.Email, 3, 100, new { @class = "in_readonly w400" })%>--%>	
                    <label class="label_readonly" style="width:300px;"><%:Model.Email%></label>				 
				</td>
			</tr>
			<tr class="a">
				<td align="right" valign="top" >
					<%=ShopText.WebUrl%>
				</td>
				<td align="left"> 
				    <%--<%:Html.TextArea("WebUrl", Model.WebUrl,5,100, new { @class = "in_readonly w400" }) %>--%>
                    <label class="label_readonly" style="width:500px;"><%:Model.WebUrl%></label>	
				</td>
			</tr>
		</table> 
	</div>
	    <div class="bottom dialog-buttons">
        <table width="100%">
            <tr>
                <td align="center">                   
					&nbsp;
					<% Html.ReturnButton(GlobalText.btnCancelText, "btnCancel", null); %>
                </td>
            </tr>
        </table>
    </div>
	<%=Html.HiddenFor(m => m.LastUpdateDate)%>
	<%=Html.HiddenFor(m => m.LastUpdatedBy)%>
    <%Html.EndForm(); %>
</body>
</html>