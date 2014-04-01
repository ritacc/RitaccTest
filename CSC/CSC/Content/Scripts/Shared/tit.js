// JavaScript Document
//<!--
//window.onerror=function(){return true};

var pltsPop=null;
var pltsoffsetX = 10;   // 弹出窗口位于鼠标左侧或者右侧的距离；3-12 合适
var pltsoffsetY = 15;  // 弹出窗口位于鼠标下方的距离；3-12 合适
var pltsPopbg="#BBBBBB"; //背景色
var pltsPopfg="#FFFFFF"; //前景色
var pltsTitle = "";
$('<div id=pltsTipLayer style="display: none;position:absolute;z-index:10001;text-align:left;"></div>').appendTo($(document.body));
var pltsTipLayer=document.getElementById("pltsTipLayer");
function pltsinits() {

    var doc = $(document);
    doc.mouseover(plts);
    doc.mousemove(moveToMouseLoc);
    //document.onmouseover = plts;
	//document.onmousemove = moveToMouseLoc;
}
function plts(ev) {
    var Event = window.event || ev;
    var o = Event.srcElement || Event.target;
    if (o == undefined) {
        return;
    }
//    if (o.alt != null && o.alt != "") {
//        o.dypop = o.alt;
//        o.alt = "";
//    };
//    if (o.title != null && o.title != "") {
//        o.dypop = o.title;
//        o.title = "";
//    };
    pltsPop = $(o).attr("dypop");
    if (pltsPop != null && pltsPop != "" && typeof (pltsPop) != "undefined") {
        pltsTipLayer.style.left = -1000;
        pltsTipLayer.style.display = '';
        var Msg = pltsPop.replace(/\n/g, "<br>");
        Msg = Msg.replace(/\0x13/g, "<br>");
        var re = /\{(.[^\{]*)\}/ig;
        if (!re.test(Msg)) pltsTitle = "";
        else {
            re = /\{(.[^\{]*)\}(.*)/ig;
            pltsTitle = Msg.replace(re, "$1") + " ";
            re = /\{(.[^\{]*)\}/ig;
            Msg = Msg.replace(re, "");
            Msg = Msg.replace("<br>", "");
        }
        var content =
             '<table cellspacing="1" cellpadding="0" style="FILTER:alpha(opacity=90);opacity:0.9;background-color:' + pltsPopbg + ';" id=toolTipTalbe border=0><tr><td style="padding-left:10px;padding-right:10px;padding-top: 6px;padding-bottom:6px;line-height:135%;background-color:' + pltsPopfg + ';">' + Msg + '</td></tr></table>';
        pltsTipLayer.innerHTML = content;
        document.getElementById('toolTipTalbe').style.width = Math.min(pltsTipLayer.clientWidth, document.documentElement.clientWidth / 2.2);
        moveToMouseLoc(ev);
        //return true;
    }
    else {
        pltsTipLayer.innerHTML = '';
        pltsTipLayer.style.display = 'none';
        //return true;
    }
}
function moveToMouseLoc(ev) {
    //alert(pltsTipLayer.innerHTML);
    if (pltsTipLayer.innerHTML == '') {
        return true;
    }
    var Event = window.event || ev;
    var MouseX = Event.clientX;
    var MouseY = Event.clientY;
    //var MouseX=event.x;
    //var MouseY=event.y;
    //window.status=event.y;
    var popHeight = pltsTipLayer.clientHeight;
    var popWidth = pltsTipLayer.clientWidth;
    if (MouseY + pltsoffsetY + popHeight > document.documentElement.clientHeight) {
        popTopAdjust = -popHeight - pltsoffsetY * 1.5;
    } else {
        popTopAdjust = 0;
    }
    if (MouseX + pltsoffsetX + popWidth > document.documentElement.clientWidth) {
        popLeftAdjust = -popWidth - pltsoffsetX * 2;
    } else {
        popLeftAdjust = 0;
    }
    pltsTipLayer.style.left = MouseX + pltsoffsetX + document.documentElement.scrollLeft + popLeftAdjust + "px";
    pltsTipLayer.style.top = MouseY + pltsoffsetY + document.documentElement.scrollTop + popTopAdjust + "px";

    //return true;
}

pltsinits();
//-->