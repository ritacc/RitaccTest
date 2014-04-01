<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CSC.Business.UserViewModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%=Url.Content("~/Content/Scripts/Shared/CommonDialog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Shared/CommonChoose.js")%>" type="text/javascript"></script>
    <link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=Url.Content("~/Content/Css/Shared/Edit.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Url.Content("~/Content/Scripts/Common/FrameWindow.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .tab_content
        {
            height: 100%;
        }
    </style>
    <script type="text/javascript">

    	var USER_ID_Before = 0;

    	$(function () {
    		/*前端框架改动无需再用此句
    		$(window).resize(function () {
    		$(".tab_container").height($(".centra").height() - 10);

    		}).resize();*/

    		$(".tab_container").tabs({
    			onselect: function (div, i) {
    				var childFrame = $("#tabList").find("iframe");
    				var cbs;
    				if (i > 0) {
    					cbs = $(childFrame[0].contentWindow.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
    				}
    				if (i != 2) {
    					var frame = $("#tabUserRole").find("iframe");
    					if (frame.length) {
    						var win = frame[0].contentWindow;
    						if (!win.checkChanged() && win.isNeedCheckChanged) {
    							if (!window.confirm('<%:GlobalText.SureLeave %>')) {
    								return false;
    							}
    							win.isNeedCheckChanged = false;
    						}
    					}
    				}
    				switch (i) {
    					case 0:
    						return loadFrame(div,null,true);
    						break;
    					case 1:
    						var editType = $(childFrame[0].contentWindow.document).find("#hEditType").val();
    						if (cbs.length !== 1) {
    							//没有勾选时:如果是点击LIST里面的新增则进入新增页面；直接点击TAB PAGE不能进入
    							if (editType == "A") {
    								return loadFrame(div, "Add?userId=" + cbs.val(), true);
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
    								return loadFrame(div, "Edit?userId=" + cbs.val(), true);
    								break;
    							}
    							else {//检视
    								return loadFrame(div, "View?userId=" + cbs.val(), true);
    								break;
    							}
    						}
    						break;
    					case 2:
    						if (USER_ID_Before != 0) {
    							//loadFrame($("#dtlListContainer2"), 'PurchaseOrderDetail?PO_ID=' + PO_ID_Before, true);
    							loadFrame(div, "UserRole?userId=" + USER_ID_Before, true);
    							USER_ID_Before = 0;
    						} else {
    							if (cbs.length !== 1) {
    								alert($g["SelectSingle"]);
    								return false;
    								break;
    							}
    							else {
    								return loadFrame(div, "UserRole?userId=" + cbs.val(), true);
    							}
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
            <li><a href="#">
                <%=UserText.ListTab%></a> </li>
            <li><a href="#">
                <%=UserText.EditTab%></a> </li>
            <li><a href="#">
                <%=UserText.UserRoleTab%></a> </li>
        </ul>
		<%--commented by jason in 20121219 for TSYF02010#06.doc--%>
		<%--<div href="List" id="tabList"></div>--%>
		<%--end commented by jason in 20121219 for TSYF02010#06.doc--%>

		<%--added by jason in 20121219 for TSYF02010#06.doc--%>
        <div href="List?UserSearch.Suspend=False" id="tabList">
        </div>
		<%--end added by jason in 20121219 for TSYF02010#06.doc--%>
        <div href="Edit" id="tabEdit">
        </div>
        <div href="UserRole" id="tabUserRole">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ButtonContent" runat="server">
</asp:Content>
