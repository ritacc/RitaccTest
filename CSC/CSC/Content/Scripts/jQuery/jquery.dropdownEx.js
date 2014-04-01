/* 获取当前页面大小和文档区域大小 */
$.getLayout = function (win) {
	win = win || window.top;
	var oTopBody = $(win.document.body);
	var scrW, scrH;
	if (win.innerHeight && win.scrollMaxY) {
		/* Mozilla     */
		scrW = win.innerWidth + win.scrollMaxX;
		scrH = win.innerHeight + win.scrollMaxY;
	} else if (win.document.body.scrollHeight > win.document.body.offsetHeight) {
		/* all but IE Mac  */
		scrW = win.document.body.scrollWidth;
		scrH = win.document.body.scrollHeight;
	} else if (win.document.body) {
		/* IE Mac    */
		scrW = win.document.body.offsetWidth;
		scrH = win.document.body.offsetHeight;
	}
	var innerWidth, innerHeight;

	if (win.innerHeight) {
		/* all except IE    */
		innerWidth = win.innerWidth;
		innerHeight = win.innerHeight;
	} else if (win.document.documentElement
			&& win.document.documentElement.clientHeight) {
		/* IE 6 Strict Mode    */
		innerWidth = win.document.documentElement.clientWidth;
		innerHeight = win.document.documentElement.clientHeight;
	} else if (win.document.body) {
		/* other    */
		innerWidth = win.document.body.clientWidth;
		innerHeight = win.document.body.clientHeigh;
	}
	var oWin = $(win);
	var scrollTop = oWin.scrollTop();
	var scrollLeft = oWin.scrollLeft();
	/* for small pages with total size less then the viewport */
	var outerWidth = Math.max(scrW, innerWidth);
	var outerHeight = Math.max(scrH, innerHeight);
	outerWidth = Math.max(outerWidth, oTopBody.outerWidth(true));
	outerHeight = Math.max(outerHeight, oTopBody.outerHeight(true));
	return { outerWidth: outerWidth, outerHeight: outerHeight, innerWidth: innerWidth, innerHeight: innerHeight, scrollTop: scrollTop, scrollLeft: scrollLeft };
}

$.tryUnescape = function (text) {
	var oResult = {};
	try {
		try {
			oResult =  jQuery.parseJSON(text);// + ")");
		}
		catch (err) {
			oResult = eval("(" + unescape(text) + ")");
		}
	}
	catch (err) {
	}
	return oResult;
}



var IsClickChange = false;
/************联动处理******************/

$.dpdChange = function (options) {
	var opts = {};
	if (typeof options == "object") {
		opts = $.extend({}, options || {});
	} else {
		throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange([options])\n\r[Element ID]:\t" + id);
		return;
	}

	$("#" + opts.sourceObj).change(function () {
		if (IsClickChange) {
			ClearChage(opts);
		}
		GetJson(opts);
	});
	if (opts.sourceObj1) {
		$("#" + opts.sourceObj1).change(function () {
			if (IsClickChange) {
				ClearChage(opts);
			}
			GetJson(opts);
		});
	}
	if (opts.sourceObj2) {
		$("#" + opts.sourceObj2).change(function () {
			if (IsClickChange) {
				ClearChage(opts);
			}
			GetJson(opts);
		});
	}
	if (opts.sourceObj3) {
		$("#" + opts.sourceObj3).change(function () {
			if (IsClickChange) {
				ClearChage(opts);
			}
			GetJson(opts);
		});
	}
	if (opts.sourceObj4) {
		$("#" + opts.sourceObj4).change(function () {
			if (IsClickChange) {
				ClearChage(opts);
			}
			GetJson(opts);
		});
	}
	if (opts.sourceObj5) {
		$("#" + opts.sourceObj5).change(function () {
			if (IsClickChange) {
				ClearChage(opts);
			}
			GetJson(opts);
		});
	}
	//如果值不为空，初使化时，加载数据项
	if ($("#" + opts.sourceObj)) {
		var txtval = $("#" + opts.sourceObj).val();
		if (txtval != "")
			GetJson(opts);
	}
}  //end $.dpdChange 

function ClearChage(opts) {
	$("#" + opts.changeObj).val("");
	$("#" + opts.changeObj).change();
	$("#caption" + opts.changeObj).find("a").html("");
	ClearDescValue(opts.Target);
}

function GetJson(opts) {
	var mTarget = opts.Target;
	if (!mTarget)
		mTarget = '';

	var extArr = {}
	var newData = { ID: $("#" + opts.sourceObj).val() }
	if (opts.sourceObj1) {
		if ($("#" + opts.sourceObj1)) {
			newData = { ID: $("#" + opts.sourceObj).val(), ID1: $("#" + opts.sourceObj1).val() }
		}
	}
	$.extend(extArr, opts.data || {}, newData);

	if (opts.sourceObj2) {
		if ($("#" + opts.sourceObj2)) {
			$.extend(extArr, extArr, { ID2: $("#" + opts.sourceObj2).val() });
		}
	}
	if (opts.sourceObj3) {
		if ($("#" + opts.sourceObj3)) {
			$.extend(extArr, extArr, { ID3: $("#" + opts.sourceObj3).val() });
		}
	}
	if (opts.sourceObj4) {
		if ($("#" + opts.sourceObj4)) {
			$.extend(extArr, extArr, { ID4: $("#" + opts.sourceObj4).val() });
		}
	}
	if (opts.sourceObj5) {
		if ($("#" + opts.sourceObj5)) {
			$.extend(extArr, extArr, { ID5: $("#" + opts.sourceObj5).val() });
		}
	}

	$.ajax({
		url: opts.url,
		data: extArr,
		success: function (ajaxContext) {
			var objArr = $.tryUnescape(ajaxContext);
			var child = $("#ul" + opts.changeObj);
			var initVal = $("#" + opts.changeObj).val();
			child.html("");
			var isAddDesc = true;
			var descInfo = "";
			if (!opts.Field.Desc) isAddDesc = false;

			var len = 0;
			if (objArr) {
				$.each(objArr, function (i, o) {
					var desc = ",\"objDesc\":\"\"";
					if (isAddDesc) {
						desc = ",\"objDesc\":\"" + o[opts.Field.Desc] + "\"";
						descInfo = "-" + o[opts.Field.Desc];
					}
					if (!desc)
						desc = "";

					var value = o[opts.Field.Value];
					if (value ==null)
						value = "";
					if (descInfo == "-")
						descInfo = "";

					var displayName = o[opts.Field.DisplayName];

					var varli = $("<li><a obj='{\"DisplayName\":\""
						+ displayName + "\",\"objValue\":\"" + value + "\"" + desc + ",\"objTarget\":\"" + mTarget + "\"}'>" + displayName + descInfo.replace("{:#}", "\"") + "</a></li>");
					len++;
					LiClick(varli);
					child.append(varli);
					if (initVal == value && varli) {
						liSetValue(varli, opts.changeObj);
					}
				});
			}
			//处理高度
			var dHeight = 20;
			if (len > 0)
				dHeight = len * 19;
			$("#popup" + opts.changeObj).css("height", dHeight + "px");

			InitDpdValue($("#" + opts.changeObj));
		},
		dataType: "text",
		cache: false,
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			if (typeof error == "function") {
				error();
			} else {
				//				var errorArray = [];
				//				errorArray.push("出错了!\r\n\r\n\t错误信息：\t");
				//				errorArray.push(textStatus);
				//alert(errorArray.join(""));
			}
		},
		complete: function () {

		}
	});

} //end function
var Tag = "";
function LiClick(obj) {
	obj.click(function (event) {
		IsClickChange = true;
		var id = obj.parent().attr("guid");
		var ObjItem = $.tryUnescape(obj.find("a").attr("obj"));
		SetDpdValue(ObjItem, id, obj);		
	});
}

function SetDpdValue(ObjItem, id, obj) {
	if (ObjItem.objTarget) {
		var objDesc = $("#" + ObjItem.objTarget);
		if (objDesc) {
			if (!objDesc.attr("type")) {
				objDesc.html(ObjItem.objDesc.replace("{:#}","\""));
			} else {
				objDesc.val(ObjItem.objDesc.replace("{:#}", "\""));
			}
		}
	}
	
	var txtItem = $("#" + id);
	txtItem.val(ObjItem.objValue);
	if (ObjItem.Tag) {
		Tag = ObjItem.Tag;
	}
	//txtItem.change();		20130904
	var lblMsg = $("#" + txtItem.attr("name") + "_validationMessage");
	if (lblMsg) {
		if (lblMsg.hasClass("field-validation-error")) {
			$(lblMsg).show();
		}
		else {
			$(lblMsg).hide();
		}
	}

	if (txtItem.hasClass("showdesc") && ObjItem.objDesc) {
		$("#caption" + id).find("a").html(ObjItem.objDesc.replace("{:#}", "\""));
	}
	else {
		$("#caption" + id).find("a").html(ObjItem.DisplayName);
	}
	
	obj.parent().find(".auto-complete-itme-select").removeClass("auto-complete-itme-select");
	obj.addClass("auto-complete-itme-select");

	txtItem.val(ObjItem.objValue);
	txtItem.change();
}

function ClearDescValue(descid) {
	if (!descid)
		return;

	var objDesc = $("#" + descid);
	if (objDesc) {
		if (!objDesc.attr("type")) {
			objDesc.html("");
		} else {
			objDesc.val("");
		}
	}
}
/************End联动处理******************/

function InitDpdValue(obj) {
	if (!obj)
		return;
	var txtVal = obj.val();

	if ((txtVal && txtVal.length > 0) || txtVal=="") {

		var mid = obj.attr("id");
		var ulID = "#ul" + mid + " li";
		var isHaveValue = false;
		$(ulID).each(function (i, o) {
			var item = $(o);
			var itemObj = $.tryUnescape(item.find("a").attr("obj"));
			if (itemObj.objValue == txtVal) {
				SetDpdValue(itemObj, mid, item);
				isHaveValue = true;
				obj.change();
			}
		});
		//Clear Data
		if (!isHaveValue && txtVal == "0")
			obj.val("");
	}
}

function TableKey(mid, code) {
	var pNext;
	if (code == 37 || code == 38) {
		var selectLi = $("#ul" + mid).find(".auto-complete-itme-select");
		if (!selectLi)
			return;
		if (selectLi.length == 0)
			return;
		pNext = selectLi.prev();
	}
	else if (code == 39 || code == 40) {
		var selectLi = $("#ul" + mid).find(".auto-complete-itme-select");

		if (selectLi && selectLi.length > 0) {
			pNext = selectLi.next();
		}
		else {
			var arr = $("#ul" + mid).find("li");
			if (arr && arr.length > 0)
				pNext = $(arr[0]);
		}
	}

	if (pNext) {		
		var item = pNext;
		var mstr = item.find("a").attr("obj");
		if (mstr && mstr.length > 0) {
			var itemObj = $.tryUnescape(mstr);
			if (itemObj) {
				SetDpdValue(itemObj, mid, item);
			}
		}
	}
}
function liSetValue(liItem, mid) {
	var item = liItem;
	var mstr = item.find("a").attr("obj");
	if (mstr && mstr.length > 0) {
		var itemObj = $.tryUnescape(mstr);
		if (itemObj) {
			SetDpdValue(itemObj, mid, item);
		}
	}
}

$(document).ready(function () {
	$(".auto-complete-button").each(function (i, o) {
		var obj = $(o);
		var id = obj.attr("id").substring(3);
		if (obj.attr("disabled") || (obj.attr("view") && obj.attr("view") == "view")) {

		}
		else {
			obj.click(function (event) {
				togeter(event, obj);
			}).keydown(function (e) {
				var code = (e.keyCode ? e.keyCode : e.which);
				if (code >= 37 && code <= 40) {
					TableKey(id, code);
				}
			}); //end key down
		}

		if (obj.attr("view") && obj.attr("view") == "view") {
			obj.addClass("label_readonly");
			obj.attr("disabled", "");
			obj.find(".auto-complete-buttom-click").remove();
			obj.unbind("click");
			obj.unbind("keydown");
		}
	});

	// Init Value
	$(".textboxDepart").each(function (i, o) {
		var obj = $(o);
		InitDpdValue(obj);
	});

	//bind Click Event
	$(".auto-complete-popup li").each(function (i, o) {
		var obj = $(o);
		LiClick(obj);
	});

	$(document).click(hide);
	function hide() {
		$(".auto-complete-popup").css("visibility", "hidden");
	}
});

function hideButThis(id) {
	var mid = "popup" + id;
	$(".auto-complete-popup").each(function (i, o) {
		if ($(o).attr("id") != mid)
			$(o).css("visibility", "hidden");
	});
}

function togeter(event, opts) {
	var id = opts.attr("id").substring(3);
	var item = $("#popup" + id);
	hideButThis(id);
	var offset, height, left, top;
	var width = opts.width();
	var layout = $.getLayout();


	offset = opts.offset();
	height = opts.outerHeight(true);
	left = offset.left;

	top = offset.top;
	if (left + width > layout.innerWidth) {
		left = layout.innerWidth - width;
	}

	var itemTop = top + height;
	var itemLeft = left;

	//Set Top Position
	if ((itemTop + item.height()) > layout.innerHeight) {
		if (top > item.height())
			itemTop = top - item.height() - 1;
	}
	//set left Position
	if ((left + width) > layout.innerWidth) {
		if (((left + width) - item.width()) > 0) {
			left = left + width - item.width();
		}
	}

	item.css("top", itemTop);
	item.css("left", itemLeft);

	item.css("visibility", "visible");
	item.show();
	event.stopPropagation();
}

$.dropdown = {
	disabled: function (options) {
		var opts = {};
		if (typeof options == "object") {
			opts = $.extend({}, options || {});
		} else {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}
		if (!opts.id) {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}
		var popdiv = $("#div" + opts.id);
		if (popdiv) {
			if (opts.isDisabled) {
				popdiv.attr("disabled", "disabled");
				popdiv.unbind("click");
			}
			else {
				popdiv.attr("disabled", "");
				popdiv.unbind("click");
				popdiv.click(function (event) {
					togeter(event, popdiv);
				});
			}
			if (opts.isClearValue) {
				if ($("#" + opts.id).val() != "") {
					$("#" + opts.id).val("");
					$("#" + opts.id).change();
					$("#caption" + opts.id).find("a").html("");
					if (opts.Target)
						ClearDescValue(opts.Target);
				}
			}
		}

	}, //end disable
	SetValue: function (options) {// js Set Value   参数：$.dropdown.SetValue({id: "",val :""});
		var opts = {};
		if (typeof options == "object") {
			opts = $.extend({}, options || {});
		} else {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}

		if (!opts.id) {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}
		var obj = $("#" + opts.id);
		obj.val(opts.val);
		InitDpdValue(obj);
		if (opts.val == "") {
			ClearDescValue(opts.id);
		}
	}, //end disable
	ClearValue: function (options) {// js Set Value   参数：$.dropdown.SetValue({id: "",val :""});
		var opts = {};
		if (typeof options == "object") {
			opts = $.extend({}, options || {});
		} else {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}

		if (!opts.id) {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}
		var obj = $("#" + opts.id);
		obj.val("");
		$("#caption" + opts.id).find("a").html("");
		 
	}, //end SetValue
	GetCode: function (options) {
		var opts = {};
		if (typeof options == "object") {
			opts = $.extend({}, options || {});
		} else {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}

		if (!opts.id) {
			throw new Error("[Message]\t\t\t:\t缺少参数必要的参数$.dpdChange.disable([options])\n\r[Element ID]:\t");
			return;
		}
		return $("#caption" + opts.id).find("a").html();
	}

}


