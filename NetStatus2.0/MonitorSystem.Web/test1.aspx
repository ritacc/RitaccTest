<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="MonitorSystem.Web.test1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <script type="text/javascript">

        function ShowDoubleCurve() {
            alert("通过这个函数，打开多曲线窗口。");
        }

        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Silverlight 应用程序中未处理的错误 " + appSource + "\n";

            errMsg += "代码: " + iErrorCode + "    \n";
            errMsg += "类别: " + errorType + "       \n";
            errMsg += "消息: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "文件: " + args.xamlFile + "     \n";
                errMsg += "行: " + args.lineNumber + "     \n";
                errMsg += "位置: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "行: " + args.lineNumber + "     \n";
                    errMsg += "位置: " + args.charPosition + "     \n";
                }
                errMsg += "方法名称: " + args.methodName + "     \n";
            }

            alert(errMsg);
        }

        function fullScreen() {
            setFullScreen(document.getElementById("silverlightObject"));
            return 0;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="divfloat">
        <table class="gridheader_table" cellpadding="0" cellspacing="0" style=" width:100%;">
            <tr>
                <td width="6">
                    <img src="../images/gridview/gridheader_03.gif" alt="" />
                </td>
                <td>
                    实时曲线
                </td>
                <td  style=" width:20px;">
                    <img id="imgSetRealTime" src="../images/icon/options.png" alt="设置实时曲线参数"/>
                </td>
                <td width="6">
                    <img src="../images/gridview/gridheader_06.gif" alt="" />
                </td>
            </tr>
        </table>
        <div class="divgrid">
            <div class="divMSChart" id="divMSChart">
                <iframe id="iframLoad" frameborder="0" src="MainChartLoad.aspx?toWhere=RealtimeCurve" style="width: 100%; height: 100%;"></iframe>
                <div id="divloading">
                    <img src="../images/popWin/loading.gif" alt="数据加载中" title="" />数据加载中.....
                </div>
            </div>
        </div>

        <table class="gridheader_table" cellpadding="0" cellspacing="0">
            <tr>
                <td width="6">
                    <img src="../images/gridview/gridheader_03.gif" alt="" />
                </td>
                <td>
                    告警列表
                </td>
                <td width="40">
                    <a href="../SerMonitor/AlarmLog.aspx">
                    <img style=" border:0px;" src="../images/Common/more.png" alt="查看更多告警记录" /> </a>
                </td>
                <td width="6">
                    <img src="../images/gridview/gridheader_06.gif" alt="" />
                </td>
                
            </tr>
        </table>
        <div class="divgrid divGridViwe" id="div2">
               
        </div>
    </div>
    <div class="divfloat">
        <div class="divright_padding">
            <table class="gridheader_table" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="6">
                        <img src="../images/gridview/gridheader_03.gif" alt="" />
                    </td>
                    <td>
                        服务器资产统计
                    </td>
                    <td width="6">
                        <img src="../images/gridview/gridheader_06.gif" alt="" />
                    </td>
                </tr>
            </table>
            <div class="divgrid">
                <div class="divMSChart" id="div1">
                    
                </div>
            </div>

            <table class="gridheader_table" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="6">
                        <img src="../images/gridview/gridheader_03.gif" alt="" />
                    </td>
                    <td>
                        服务器资产列表
                    </td>
                    <td width="6">
                        <img src="../images/gridview/gridheader_06.gif" alt="" />
                    </td>
                </tr>
            </table>
            <div class="divgrid divGridViwe" id="div4">
             

            
            </div>
        </div>
    </div>
    </form>
</body>
</html>
