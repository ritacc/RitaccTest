<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CSC.Business.Shop>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
	<link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Form.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
	<script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftAjax.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/MicrosoftMvcValidation.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Form.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdownEx.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.LevelLoader.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Shared/Calendar.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/CommonLov.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
	<script type="text/javascript">
		window.checkFormChanged = true;
		$g = <%:CSC.Controllers.AjaxController.GetResourceText("GlobalText") %>;
        <% =Html.Message() %>
		window.btnSureClick = function (sucessed, redirectUrl) {
			if (sucessed) {
				var container = window.parent.$("div.tab_container");
				container.find("ul>li").eq(0).click();
				loadFrame(window.parent.$("#tabShopList"), "ShopList", true);
			}

		};
		$(document).ready(function() {
			$(document).LevelLoader({
				data: [
				    {
				    	url: '#',
				    	postKey: "CityDDLSearch.ProvCode",
				    	htmlObj: 'Prov'
				    }, {
				    	url: '<%=Url.Content("~/ProvinceCityArea/SearchCityForDDL")%>',
				    	postKey: "AreaDDLSearch.CityCode",
				    	parentKey: "AreaDDLSearch.ProvCode",
				    	parentObj: "Prov",
				    	htmlObj: 'City'
				    }, { url: '<%=Url.Content("~/ProvinceCityArea/SearchAreaForDDL")%>',
				    	postKey: '',
				    	htmlObj: 'Area'
				    }],
				//autoAddDefaultItem: false,
				defaultItem: '<option value=""></option>',
				defaultValue: '',
				getList: function (data) {
					return data.Result;
				},
				getItem: function (item) {
					return '<option value="' + item.Code + '">' + item.FullName + '</option>';
				}
			});
			
			$("#btnCancel").click(function () {
				window.parent.$("div.tab_container").find("ul>li").eq(0).click();
				return false;
			});
            $("#btnSave").click(function(){
                if (!confirm($g["ConfirmSave"])) {
                    return false;
                }else{
                    return true;
                }
            });
		});
	</script>
    <%Html.EnableClientValidation(); %>
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
					<%--<%= Html.TextBoxFor(m => m.Code, new { @readonly="readonly" })%>--%>
					<div class="label_readonly" style="width: 60px;"><%= Model.Code %></div>
					<%=Html.HiddenFor(m=>m.Code) %>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Name%>
					<span style="color: Red;">*</span>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.Name, new { maxlength = 30,style="width:300px;" })%>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.Name)%>
                        </div>
					<%--<span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.Name)%></span>--%>
				</td> 
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.FullName%><span style="color: Red;">*</span>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.FullName, new { maxlength = 100, style = "width:500px;" })%>
                    <div>
                        <%:Html.ValidationMessageFor(m => m.FullName)%>
                        </div>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.AddrLine1%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.AddrLine1, new { maxlength = 300, style = "width:500px;" })%>
				</td>
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.AddrLine2%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.AddrLine2, new { maxlength = 300, style = "width:500px;" })%>
				</td>
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Province%>
				</td>
				<td align="left">
                <%:Html.DropDownListFor(
                                m => m.Prov,
						App.Framework.BusinessPortal.Search<CSC.Business.Province>(new CSC.Business.SearchProvinceCriteriaForDDL()
                                ).ToList().ToSelectList(m => m.Code, m => m.Code, m => m.Code == Model.Prov).AddDefaultItem(GlobalText.DropDownListDefaultItemText))%>
									
					<%--<%= Html.TextBoxFor(m => m.Prov)%> 
					<img src="../../Content/Css/Images/Lov.png" alt="" />--%>
				</td> 
			</tr>
			<%--<tr class="a">
				<td align="right" class="t">
					<%=ShopText.City%>
				</td>
				<td align="left"> 
					<%:Html.DropDownListFor(
										m => m.City,
										BusinessPortal.Search<CSC.Business.City>(new CSC.Business.SearchCityCriteriaForDDL() 
										{
											ProvCode = Model.Prov 
										}).ToList().ToSelectList(m => m.Code, m => m.Code, m => m.Code == Model.City).AddDefaultItem(GlobalText.DropDownListDefaultItemText))%>
									</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Area%>
				</td>
				<td align="left">
					<%:Html.DropDownListFor(
										m => m.Area,
										BusinessPortal.Search<CSC.Business.Area>(new CSC.Business.SearchAreaCriteriaForDDL() 
										{ 
											ProvCode = Model.Prov,
											CityCode =Model.City
										}).ToList().ToSelectList(m => m.Code, m => m.Code, m => m.Code == Model.Area).AddDefaultItem(GlobalText.DropDownListDefaultItemText))%>
						</td> 
			</tr>--%>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.PhoneNo1%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.PhoneNo1, new { maxlength = 16,style="width:160px;" })%>
                     <span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.PhoneNo1)%>
                        </span>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.PhoneNo2%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.PhoneNo2, new { maxlength = 16,style="width:160px;" })%>
                     <span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.PhoneNo2)%>
                        </span>
				</td> 
			</tr>
			<tr class="a">
				<td align="right">
					<%=ShopText.PhoneAreaCode%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.PhoneAreaCode, new { maxlength = 4, style = "width:160px;" })%>
                     <span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.PhoneAreaCode)%>
                        </span>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Fax%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.Fax, new { maxlength = 16, style = "width:160px;" })%>
                     <span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.Fax)%>
                        </span>
				</td> 
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.PostalCode%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.PostalCode, new { maxlength = 10, style = "width:160px;" })%>
                    <span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.PostalCode)%>
                        </span>
				</td> 
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.Email%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.Email, new { maxlength = 60, style="width:300px;" })%>
					<span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.Email)%>
					</span>
				</td>
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.WebUrl%>
				</td>
				<td align="left">
					<%= Html.TextBoxFor(m => m.WebUrl, new { maxlength = 150, style = "width:500px;" })%>
					<span style="color: Red;">
						<%= Html.ValidationMessageFor(m => m.WebUrl)%>
                        </span>
				</td>
			</tr>
			<tr>
				<td align="right" class="t">
					<%=ShopText.BUCode%>
				</td>
				<td align="left">
					<%:Html.DropDownListFor(
										m => m.BU_CODE,
																												BusinessPortal.Search<CSC.Business.BUSINESS_UNIT>(new CSC.Business.SearchBUCriteria() 
										{}).ToList().ToSelectList(m => m.BU_CODE, m => m.BU_NAME, m => m.BU_CODE == Model.BU_CODE))%>
											
										
				</td>
			</tr>
			<tr class="a">
				<td align="right" class="t">
					<%=ShopText.ShopType%>
				</td>
				<td align="left">
					<%=Html.DropDownListFor(m => m.SHOP_TYPE, new List<SelectListItem>() { { new SelectListItem() { Text = CSC.Business.ShopType.CS.ToString(), Value = CSC.Business.ShopType.CS.ToString() } }, { new SelectListItem() { Text = CSC.Business.ShopType.GODOWN.ToString(), Value = CSC.Business.ShopType.GODOWN.ToString() } } })%>
				</td>
			</tr>
		</table> 
	</div>
	    <div class="bottom dialog-buttons">
        <table width="100%">
            <tr>
                <td align="center">
                    <% Html.Submit(GlobalText.BtnSaveText, "btnSave"); %>
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