<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<CSC.Business.ShopListModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="tab_container customerInit">
		<ul>
			<li>
				<a href="#"><%:ShopText.ShopList%></a>
			</li>
			<li>
				<a href="#"><%:ShopText.ShopEdit%></a>
			</li>					 
		</ul>
		<div id="tabShopList">
		</div>
		<div id="tabShopEdit">
		</div>       
	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonDialog.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Shared/CommonChoose.js")%>" type="text/javascript"></script> 
	<script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet" type="text/css" />
	<link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />

	<style type="text/css">
		.tab_content
		{
			height: 100%;
		}
	</style>

	<script type="text/javascript">
	    var editClickedFalg = false;
	    $(function () {

	        /*前端框架改动无需再用此句
	        $(window).resize(function () {
	        $(".tab_container").height($(".centra").height() - 10);
	        }).resize();*/

	        $(".tab_container").tabs({
	            onselect: function (div, i) {
	                var childFrame = $("#tabShopList").find("iframe");
	                var cbs;
	                if (i > 0) {
	                    cbs = $(childFrame[0].contentWindow.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
	                }
	                switch (i) {
	                    case 0:
	                        return loadFrame(div, "ShopList?rand=" + Math.random());
	                        break;
	                    case 1:
	                        if (cbs.length !== 1) {
	                            alert($g["SelectSingle"]);
	                            return false;
	                        }
	                        else {
	                            if (editClickedFalg) {
	                                return loadFrame(div, "Edit?Code=" + cbs.val() + "&rand=" + Math.random(), true);
	                            } else {
	                                return loadFrame(div, "detail?Code=" + cbs.val() + "&rand=" + Math.random(), true);
	                            }
	                            editClickedFalg = false;
	                        }
	                        break;
	                }
	            }
	        });

	    });
		</script>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>

