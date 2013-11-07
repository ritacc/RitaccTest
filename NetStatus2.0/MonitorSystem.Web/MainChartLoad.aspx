<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainChartLoad.aspx.cs" Inherits="GDK.BCM.Main.MainChartLoad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    

    <style type="text/css">
    *{ padding:0px; margin:0px; border:0px; font-size:12x; background-color:#FAFAFA;}
    .tbcss
    {
        font-size:12px; margin-left:10px;
    }
    body
    {
    	width:100%;
    }
    </style>

    <style type="text/css">
        html, body
        {
            overflow:hidden;
        }
        body
        {
            padding: 0;
            margin: 0;
        }
       #objServerRoom,#silverlightControlHost
        {
            height: 270px;
            width:100%;
            text-align: center;
        }
    </style>

</head>
<body >
    <form id="form1" runat="server">
     <div id="silverlightControlHost">
                <object id="objServerRoom" data="data:application/x-silverlight-2," type="application/x-silverlight-2">
                    <param name="source" value="../ClientBin/MonitorSystem.xap" />
                    <param name="onError" value="onSilverlightError" />
                    <param name="background" value="black" />
                    <param name="minRuntimeVersion" value="4.0.50826.0" />
                    <param name="autoUpgrade" value="true" />
                    <param name="windowless" value="true"/>
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0" style="text-decoration: none">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="获取 Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe id="_sl_historyFrame" style="visibility: hidden; border: 0px"></iframe>
            </div>
    </form>
</body>
</html>
