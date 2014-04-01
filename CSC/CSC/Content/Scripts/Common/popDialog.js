/*
*弹出Dialog
*依赖于jQuery 
*by xh 2012
*/
function clickEventHandler(buttons,i, div, dlg) {
	this.click = function () {
		buttons[i].onclick(div, div.find("iframe")[0].contentWindow, dlg());
	};
}
function msgClickEventHandler(buttons, i, getdialog) {
    this.click = function () {
        buttons[i].onclick(getdialog());
    };
}


function popContentDialog(content, width, height, title, buttons, onCloseFun, iconClass) {
    var editObj = $("<div>");
    var dialog = null;
    if (buttons != null) {
        var centraContainer = $("<div>", { noResize: "true" }).width(width).height(height - 36).css("overflow", "auto"); //.css({backgroundColor: "#8db2e3"});
        centraContainer.appendTo(editObj)
        if (iconClass) {
            var icon = $("<div>", { style: 'float:left;margin:5px 5px 5px 5px;' }).width("24px").height("24px").addClass(iconClass);
            centraContainer.append(icon);
        }
        var buttonContainer = $("<div>", { Class: "bottom dialog-buttons" });
        buttonContainer.appendTo(editObj);
        for (var i in buttons) {
            var btn = $("<input>", { type: "button", value: buttons[i].text });
            btn.css("margin-right", "5px");
            var item = new msgClickEventHandler(buttons, i, function () { return dialog; });
            btn[0].onclick = item.click;
            btn.appendTo(buttonContainer);
        }
    }
    else
        centraContainer = editObj;
    $("<div>", { style: iconClass ? 'margin-left:30px;' : '' }).append(content).css({ "padding": "10px", "width": (width - 50) + "px", "word-wrap": "break-word"/*, "word-break": "break-all"*/ }).appendTo(centraContainer);

    editObj.appendTo($(document.body));

    dialog = editObj.dialog({
        width: width,
        height: height,
        modal: true,
        title: title,
        onclose: onCloseFun,
        hideCloseBtn: true
    });
    dialog.open();
    return dialog;
}

//依赖于FrameWindow.js
function popDialog(url, viewEditId, width, height, title, buttons /*[{text:'ok',onclick=function(){alert('v');}}]*/, loadCall, onCloseFun,onCloseingFun) {    //编辑
    // 创建结果显示层
    var editObj = $("#" + viewEditId + "");
    if (editObj.length == 0) {
        editObj = $("<div>", { id: viewEditId });
    }

    editObj.html("");
  
    var dlg = null;
    if (buttons != null) {
        var centraContainer = $("<div>", { Class: "centra", noResize: "true" }).width(width).height(height - 36);
        centraContainer.appendTo(editObj);
        var buttonContainer = $("<div>", { Class: "bottom dialog-buttons" });
        buttonContainer.appendTo(editObj);
        for (var i in buttons) {
            if (typeof (buttons[i]) == "function") continue;
            
            var btn = $("<input>", { type: "button", value: buttons[i].text });
            btn.css("margin-right","5px");
            var item = new clickEventHandler(buttons, i, centraContainer, function () { return dlg; });
            btn[0].onclick = item.click;
            btn.appendTo(buttonContainer);
        }
    }
    else
        centraContainer = editObj;

    editObj.css("height", "100%").appendTo($(document.body));

    dlg = editObj.dialog({
        width: width,
        height: height,
        modal: true,
        title: title,
        onopen: function () {
            loadFrame(centraContainer, url, null,null, loadCall);
        },
		onclose: onCloseFun,
		oncloseing: onCloseingFun
    });
    dlg.open();
    return dlg;

}