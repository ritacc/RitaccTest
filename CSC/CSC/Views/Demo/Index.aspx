<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CSC.Business.GodownListModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>

	<link href="<%=Url.Content("~/Content/Css/jQuery/dropdownEx_autocomplete.css")%>" rel="stylesheet" type="text/css" />
	

	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdown.AutoComplete.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.dropdownEx.js")%>" type="text/javascript"></script>
	<script src="<%=Url.Content("~/Content/Scripts/Common/CommonLov.js")%>" type="text/javascript"></script>
	<script type="text/javascript">
		<%Html.EnableSortScript(); %>

		var dlgEdit=null;
		var isLoad=false;
		$(function () {
			$("#imgSelectSales").click(function () {
				var Code=$("#SearchGodown_GodownCode").val();
				if(Code == "")
					return;
				isLoad=true;
				if(GetCodeDesc())
					return;
			});

			$("#SearchGodown_GodownCode").blur(function () {
				var Code=$("#SearchGodown_GodownCode").val();
				if(Code == "")
					return;
				if(isLoad)
					return;
				if(GetCodeDesc())
					return;
			});

			$("#SearchGodown_GodownCode").keydown(function (e) {
				var code = (e.keyCode ? e.keyCode : e.which);
				isLoad=false;
				if (code == 13) {
					isLoad=true;
					GetCodeDesc();
				}
			}); //end key down

		});

		function LovSet()
		{
			var Code=$("#SearchGodown_GodownCode").val();
			if(Code == "")
				return;

			var murl='../Demo/DemoLov?notIsMultipleChoose=ture&Code='+ Code;
			bindLovClick(null, murl , 650, 450, "SearchGodown_GodownType","SearchGodown_GodownCode", function (values,codesOrDscs) {
					
				var valArr= values.split(";");
				var olderValue="";
				if(valArr.length>0)
				{
					values=valArr[valArr.length-1];
					$("#SearchGodown_GodownType").val(values);
					olderValue=valArr[0];
				}
				else
				{
					$("#SearchGodown_GodownType").val(values);
				}
				codesOrDscs=codesOrDscs.replace(olderValue+";","");

				if(codesOrDscs && codesOrDscs.length>0)
				{
					var Arr = codesOrDscs.split("#");
					if(Arr.length= 2)
					{
						$("#SearchGodown_GodownCode").val(Arr[0]);
						$("#lblPartsDescLov").html(Arr[1]);
					}
				}
			},"Select Parts");
		}

		function GetCodeDesc()
		{
			var PrevCode=$("#SearchGodown_GodownCode").val();
			var result=false;
			$.ajax({
				type: 'GET',
				url: '../Demo/GetAItem',
				dataType: "json",
				data: { Code: PrevCode, _t: Math.random() },
				success: function (res) {
					if (res) {
						$("#lblPartsDescLov").html(res.Result.InterValue);
						$("#SearchGodown_GodownCode").val(res.Result.Text);
						$("#SearchGodown_GodownType").val(res.Result.Value);
						result= true;
					}
					else
					{
						LovSet();
					}
				},
				error: function (xhr) {
				}
			});//end ajax
			return result;
		}
	</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div style=" padding-left:30px;">
	 
	<table>
		<tr>
			<td>
				<%=PartsMaintenanceText.BrandCode%>
			</td>
			<td>
				<% Html.DropdownListEx(m => m.SearchGodown.GodownCode
								, App.Framework.BusinessPortal.Search<CSC.Business.BrandCodeModel>(new CSC.Business.SearchBrandCodeForDLLCriteria() { }).OrderBy(m => m.BrandCode).ToList()
								.ToSelectListWidtType(m => m.BrandCode, m => m.BrandId, m => m.BrandDesc, null).AddDefaultItem()
								, htmlAttributes: new { @dwidth = 200, @width = 140, @height = 250 }, TargetDescID: "lblBrandDesc");  %>
			</td>
			<td>
				<label id="lblBrandDesc" class="label_readonly" style="width: 300px;"></label>
			</td>
		</tr>
		<tr>
				<td>Parts:</td>
				<td>
					<%=Html.HiddenFor(m => m.SearchGodown.GodownType, htmlAttributes: new { maxlength = 8 })%>
					<%=Html.TextBoxFor(m => m.SearchGodown.GodownCode, htmlAttributes: new { maxlength = 20, style = "width:120px;" })%>
					<img class="img_hide_on_view" src="<%=Url.Content("~/Content/Css/Images/Lov.png")%>" id="imgSelectSales" onmouseover="this.style.cursor ='pointer';" />
				</td>
				<td>
					<label id="lblPartsDescLov" class="label_readonly" style="width: 300px;"></label>
				</td>
			</tr>
		<tr>
			<td>Parts:</td>
			<td>
			<% Html.DropdownAutoComplete(m => m.SearchGodown.GodownType,null
					, htmlAttributes: new { @dwidth = 250, @width = 140, @height = 250 }, TargetDescID: "lblPartsDesc");  %>	
			</td>
			<td>
				<label id="lblPartsDesc" class="label_readonly" style="width: 300px;"></label>
			</td>
		</tr>
		
	</table>
	
	</div>
	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBottom" runat="server">
</asp:Content>
