/*
*弹出消息
*依赖于jQuery 
*by xh 2012
*/
function msgClickEventHandler(buttons, i, getdialog) {
	this.click = function () {
		buttons[i].onclick(getdialog());
	};
}
/*
var popContentDialog = function (content, width, height, title, buttons, onCloseFun, iconClass) {
    var editObj = $("<div>");
    var dialog = null;
    if (buttons != null) {
        var centraContainer = $("<div>", { noResize: "true" }).width(width).height(height - 36).css("overflow", "auto"); //.css({backgroundColor: "#8db2e3"});
        centraContainer.appendTo(editObj);
        if (iconClass) {
            var icon = $("<div>", { style: 'float:left;margin:5px 5px 5px 5px;' }).width("24px").height("24px").addClass(iconClass);
            centraContainer.append(icon);
        }
        var buttonContainer = $("<div>", { Class: "bottom dialog-buttons" });
        buttonContainer.appendTo(editObj);
        for (var i in buttons) {
            if (typeof (buttons[i]) == "function") continue;
            var btn = $("<input>", { type: "button", value: buttons[i].text, Class: buttons[i].Class });
            btn.css("margin-right", "5px");
            var item = new msgClickEventHandler(buttons, i, function () { return dialog; });
            btn[0].onclick = item.click;
            btn.appendTo(buttonContainer);
        }
    } else {
        centraContainer = editObj;
    }

    //消息内容div
    var _msgDiv = $("<div>");
    if (iconClass) {
        _msgDiv.css({ 'margin-left': '30px',  'width': ((width - 30) + 'px'), 'white-space': 'nowrap' });
    } else {
        _msgDiv.css({  'width': ((width - 6) + 'px'), 'white-space': 'nowrap' });
    }
    //_msgDiv.css({ padding: "10px" });
    _msgDiv.append(content);
    _msgDiv.appendTo(centraContainer);

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
};
*/
function showSuccessMsg(msg, title, okClickFun, wid, hei) {

    if (typeof title == 'undefind' || title == null || title.length == 0)
        title = $g["MessageTitle"];

    if (typeof wid == 'undefind' || wid == null || wid < 1)
        wid = 320;

    if (typeof hei == 'undefind' || hei == null || hei < 1)
        hei = 150;

    popContentDialog(msg, wid, hei, title, [
    {
        Class: 'btnSure', text: $g["BtnOKText"], onclick: function (dialog) {
            dialog.close(function () {
                if (typeof okClickFun != 'undefind' && okClickFun)
                    okClickFun();
            });
        }
    }], null, "success");
}

function showInfoMsg(msg, title, wid, hei) {

    if (typeof title == 'undefind' || title == null || title.length == 0)
        title = $g["MessageTitle"];

    if (typeof wid == 'undefind' || wid == null || wid < 1)
        wid = 400;

    if (typeof hei == 'undefind' || hei == null || hei < 1)
        hei = 200;

    popContentDialog(msg, wid, hei, title, [
    {
        Class: 'btnSure', text: $g["BtnOKText"], onclick: function (dialog) {
            dialog.close();
        }
    }], null, "info");
}

function showMsg(msg, title, okClickFun) {

    if (typeof title == 'undefind' || title == null || title.length == 0)
        title = $g["MessageTitle"];

    popContentDialog(msg, 320, 150, title, [
    {
        Class: 'btnSure', text: $g["BtnOKText"], onclick: function (dialog) {
        	dialog.close(function () {
        		if (typeof okClickFun != 'undefind' && okClickFun)
        			okClickFun();
        	});
        }
    }], null, "failure");
}

function showMessage(title, msg, sucessed, redirectUrl, onCloseFun) {
	var fun = onCloseFun || function () {
		if (window.btnSureClick != undefined) {
			window.btnSureClick(sucessed, redirectUrl);
		}
	};

	//popContentDialog(msg, 320, 150, title || "消息提示", [{ Class: 'btnSure', text: "确定", onclick: function (dialog) { dialog.close(); } }], fun, sucessed ? "success" : "failure");
	popContentDialog(msg, 320, 150, title || $g["MessageTitle"], [{ Class: 'btnSure', text: $g["BtnOKText"], onclick: function (dialog) { dialog.close(); } }], fun, sucessed ? "success" : "failure");
}

function showMessageEx(msg, sucessed, redirectUrl, onCloseFun) {
	var colseFun = onCloseFun || function () {
		if (window.btnSureClick != undefined) {
			window.btnSureClick(sucessed, redirectUrl);
		}
	};
	if (sucessed) {
		colseFun();
	} else {
		$("#Message").html(msg).css("color", "red");
	}
}

function showConfirm(title, msg, onCloseFun, yesClick, noClick) {
	var fun = onCloseFun;

	popContentDialog(msg, 320, 150, title || $g["MessageTitle"], [
    { text: $g["BtnYesText"], onclick: function (dialog) {
    	if (yesClick)
    		yesClick();
    	else if (window.btnYesClick)
    		window.btnYesClick();
    	dialog.close();
    }
    },
    { text: $g["BtnNoText"], onclick: function (dialog) {
    	if (noClick)
    		noClick();
    	else if (window.btnNoClick)
    		window.btnNoClick();
    	dialog.close();
    }
    }], fun, "question");
}
