//name:列名, labelVal:label的标签值,title:弹窗的标题,btnSureTitle:确认按钮的名称, divContain:容器的ID , editFun:回调函数 ， callFun 是回调函数
function GenerateLov(name, val, labelVal,url, width, height, title, btnSureTitle, btnCancelTitle, divContain,editFun,callFun,rootPath) {
	var contain, idHidden, imgId,idLabel
		idHidden = name + "_hiddent";
		imgId = name + "_img";
		idLabel = name + "_label";
		//url = (val == "") ? url : (url.indexOf('?') != -1 ? url + "&id=" + val : url + "?id=" + val);
		if(rootPath!="/")
			rootPath = rootPath.length > 1 && rootPath.substr(rootPath.length - 1, 1) == "/" ? rootPath : rootPath + "/";

	contain = "<input type=\"hidden\" name=\"" + name + "\" id=\"" + idHidden + "\" value='" + val + "' />";	
	if (!isFunction(callFun))
		contain += "<label id=\"" + idLabel + "\" class=\"label_readonly\" style=\"width: 145px;\"></label><img src=\"" + rootPath + "Content/Css/Images/Lov.png\" id=\"" + imgId + "\" onclick='javascript:SelectItemLov(\"" + name + "\",\"" + val + "\",\"" + url + "\"," + width + "," + height + ",\"" + title + "\",\"" + btnSureTitle + "\",\"" + btnCancelTitle + "\");' onmouseover=\"this.style.cursor ='pointer';\" />";
	else
		contain += "<label id=\"" + idLabel + "\" class=\"label_readonly\" style=\"width: 145px;\"></label><img src=\"" + rootPath + "Content/Css/Images/Lov.png\" id=\"" + imgId + "\" onclick='javascript:SelectItemLov(\"" + name + "\",\"" + val + "\",\"" + url + "\"," + width + "," + height + ",\"" + title + "\",\"" + btnSureTitle + "\",\"" + btnCancelTitle + "\"," + callFun + ");' onmouseover=\"this.style.cursor ='pointer';\" />";
	$("#" + divContain).append(contain);

	if (val != "") {
		$("#" + idLabel).html(labelVal);
		if (isFunction(editFun))
			editFun();
	}
	

}

function SelectItemLov(name, val, url, width, height, title, btnSureTitle, btnCancelTitle,callFun) {	
	var idHidden, idLabel
	idHidden = name + "_hiddent";
	idLabel = name + "_label";

	var idval = $("#" + idHidden).val();
	url = (idval == "") ? url : (url.indexOf('?') != -1 ? url + "&id=" + idval : url + "?id=" + idval);//根据hidden里的值去重新设置

	var pd = popDialog(url, title, width, height, title, [{ text: btnSureTitle, onclick: function (div, win, dlg) {
		var cbk = $(win.document).find("input[name='cbKey']:checked");
		if (cbk.length != 1) {
			alert($g["SelectSingle"]);
			return false;
		}
		
		$("#" + idHidden).val(cbk.val());
		$("#" + idLabel).html(cbk.attr("code"));
		
		if (isFunction(callFun)) {
			callFun(cbk);
		}
		dlg.close();
	}
	}, { text: btnCancelTitle, onclick: function (div, win, dlg) {
		dlg.close();
	}
	}], function (win) {
		$(win.document).find("#btnCancel").click(function () {
			pd.close();
		})
	});
}

function isFunction(obj) {
	return !!(obj && obj.constructor && obj.call && obj.apply);
}

//在lov的页面里，勾选传递过来的id所对应的checkbox
function SetValById() {
	var id = GetParam(location.href, "id");
	if (id != "") {
		$("input[name='cbKey']").each(function (index) {
			if ($(this).val() == id) {
				$(this).attr("checked", "true");
				$(this).parent().parent().css("background-color", "#FFD58D");
			}
		});
	}
}

//在url里获取参数值
function GetParam(url, param) {
	if (url.indexOf("?") != -1) {
		var qstr = url.substring(url.indexOf("?") + 1);
		if (qstr) {
			var arr = qstr.split("&");
			var tmp;
			if (arr.length > 0) {
				for (var i in arr) {
					tmp = arr[i].split('=');
					if (tmp.length > 1 && tmp[0] == param) {
						return tmp[1];
					}
				}
			}
		}
	}
	return "";
}
//获取相对于网站的跟目录
function GetUrlRelativeRoot() {
	var root;
	var href = window.document.location.href;
	var path = window.document.location.pathname;
	var pos = href.indexOf(path);
	var domain = href.substring(0, pos);
	var proName = path.substring(0, path.indexOf("/") + 1); //  eg: /csc/
	return proName; //return relative root
}