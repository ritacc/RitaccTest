<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="<%=Url.Content("~/Content/Css/Shared/Common.css")%>" rel="stylesheet"
        type="text/css" />
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery-1.4.1.min.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/jQuery/jquery.Dialog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Shared/Common.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Common/popDialog.js")%>" type="text/javascript"></script>
    <script src="<%=Url.Content("~/Content/Scripts/Common/Message.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            showMessage(null, '<%=ViewData["ErrMsg"].ToJSON2()%>', false, null, function () {
                var win = window;
                while (win != win.parent) {
                    win = win.parent;
                }
                if (win.location.href.indexOf("/Home/Index") < 0 && win.location.href.indexOf("/Workplace") < 0) {
                    win.location.href = '<%:Url.Content("~/Home/Index") %>';
                }
            });
        });
    </script>
</head>
<body>
</body>
</html>
