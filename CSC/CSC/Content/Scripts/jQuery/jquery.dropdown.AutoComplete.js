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
			oResult = jQuery.parseJSON(text); // + ")");
		}
		catch (err) {
			oResult = eval("(" + unescape(text) + ")");
		}
	}
	catch (err) {
	}
	return oResult;
}
/*************选中处理***************/
$.fn.setCursorPosition = function (position) {
	if (this.lengh == 0) return this;
	return $(this).setSelection(position, position);
}

$.fn.setSelection = function (selectionStart, selectionEnd) {
	if (this.lengh == 0) return this;
	input = this[0];

	if (input.createTextRange) {
		var range = input.createTextRange();
		range.collapse(true);
		range.moveEnd('character', selectionEnd);
		range.moveStart('character', selectionStart);
		range.select();
	} else if (input.setSelectionRange) {
		input.focus();
		input.setSelectionRange(selectionStart, selectionEnd);
	}

	return this;
}

$.fn.focusEnd = function () {
	this.setCursorPosition(this.val().length);
}
/************* End选中处理 ***************/


/************联动处理******************/
function AutoCompleteClearChage(opts) {
	$("#" + opts.changeObj).val("");
	$("#" + opts.changeObj).change();
	$("#caption" + opts.changeObj).find("a").html("");
	AutoCompleteClearDescValue(opts.Target);
}


var Tag = "";
function AutoCompleteLiClick(obj) {
	obj.click(function (event) {
		IsClickChange = true;
		var id = obj.parent().attr("guid");
		var ObjItem = $.tryUnescape(obj.find("a").attr("obj"));
		 
		AutoCompleteSetDpdValue(ObjItem, id, obj);
	});
}

function AutoCompleteSetDpdValue(ObjItem, id, obj) {
	var objTarget = $("#div" + id).attr("TargetDescID");
	 
	if (objTarget) {
		var objDesc = $("#" + objTarget);
		if (objDesc) {
			 
			if (!objDesc.attr("type")) {
				objDesc.html(ObjItem.objDesc);
			} else {
				objDesc.val(ObjItem.objDesc);
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
		if (ObjItem.objValue) {
			$(lblMsg).hide();
		}
		else {
			$(lblMsg).show();
		}
	}

	if (txtItem.hasClass("showdesc") && ObjItem.objDesc) {
		$("#caption" + id).find("a").html(ObjItem.objDesc);
	}
	else {
		$("#caption" + id).find("a").html(ObjItem.DisplayName);
	}

	obj.parent().find(".auto-complete-itme-select").removeClass("auto-complete-itme-select");
	obj.addClass("auto-complete-itme-select");

	txtItem.val(ObjItem.objValue);
	txtItem.change();
}

function AutoCompleteClearDescValue(descid) {
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

function AutoCompleteInitDpdValue(obj) {
	if (!obj)
		return;
	var txtVal = obj.val();

	if ((txtVal && txtVal.length > 0) || txtVal == "") {

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

function AutoCompleteTableKey(mid, code) {
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
		pNext.parent().find(".auto-complete-itme-select").removeClass("auto-complete-itme-select");
		pNext.addClass("auto-complete-itme-select");
	}
}

var IsClickChange = false;
$(document).ready(function () {
	$(".auto-completeBox-button").each(function (i, o) {
		var obj = $(o);
		var id = obj.attr("id").substring(3);
		if (obj.attr("disabled") || (obj.attr("view") && obj.attr("view") == "view")) {

		}
		else {
			obj.keydown(function (e) {
				var code = (e.keyCode ? e.keyCode : e.which);
				if (code >= 37 && code <= 40) {
					AutoCompleteTableKey(id, code);
				}
			}); //end key down

			var valueObj = obj.find(".auto-complete-button_value");
			var chA = valueObj.find("a");
			var chInput = valueObj.find("input");
			chInput.hide();
			valueObj.click(function (event) {
				chA.hide();
				chInput.show();
				chInput.focus();

				chInput.focusEnd();
				//chInput.val("");
				//AutoCompleteAutoItems("", id);
				AutoCompleteTogeter(event, obj);
			});
			chInput.blur(function () {
				chA.show();
				chInput.hide();
			});
			chInput.keydown(function (e) {
				var code = (e.keyCode ? e.keyCode : e.which);
				if (code == 13) {
					//auto-complete-itme-select
					var item = $("#ul" + id).find(".auto-complete-itme-select");
					if (item) {
						var mstr = item.find("a").attr("obj");
						if (mstr && mstr.length > 0) {
							var itemObj = $.tryUnescape(mstr);
							if (itemObj) {
								chA.show();
								chInput.hide();
								AutoCompleteSetDpdValue(itemObj, id, item);
								$(".auto-complete-popup").hide();
							}
						}
					}
				}
			}); //end key down
			chInput.keyup(function () {
				AutoCompleteAutoItems(chInput.val(), id);
			});
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
		AutoCompleteInitDpdValue(obj);
	});

	//bind Click Event
	$(".auto-complete-popup li").each(function (i, o) {
		var obj = $(o);
		AutoCompleteLiClick(obj);
	});

	$(document).click(hide);
	function hide() {
		$(".auto-complete-popup").css("visibility", "hidden");
	}
});

var PrevCode = "";
var mChildID = "";
function AutoCompleteAutoItems(mCode, mid) {
	//$("#divTest").html(mCode, mid);
	
	if (PrevCode == mCode)
		return;
	PrevCode = mCode;

	var child = $("#ul" + mid);
	if (!mCode) {
		child.html("");
		$("#popup" + mid).css("height",  "20px");
		return;
	}
	mChildID = mid;
	setTimeout(AutoCompleteAjaxLoadItems, 600);
}

function AutoCompleteAjaxLoadItems() {
	var child = $("#ul" + mChildID);
	$.ajax({
		type: 'GET',
		url: '../Demo/GetItems',
		dataType: "json",
		data: { Code: PrevCode, _t: Math.random() },
		success: function (res) {
			if (res) {
				if (PrevCode == "")
					return;

				child.html("");
				var isAddDesc = true;
				var descInfo = "";
				var len = 0;
				var itemIndex = 0;
				var SelectClass = "class='auto-complete-itme-select' ";
				$.each(res.Result, function (i, o) {
					var desc = ",\"objDesc\":\"\"";
					if (isAddDesc) {
						if (o.InterValue != "null") {
							desc = ",\"objDesc\":\"" + o.InterValue + "\"";
							descInfo = "-" + o.InterValue;
						}
					}
					if (descInfo == "-null")
						descInfo = "";
					var value = o.Value;
					var displayName = o.Text;

					if (displayName) {
						itemIndex++;
					}
					if (itemIndex == 1) {
						SelectClass = " <li class='auto-complete-itme-select'>  ";
					}
					else {
						SelectClass = "<li>";
					}

					var varli = $(SelectClass +"<a obj='{\"DisplayName\":\""
						+ displayName + "\",\"objValue\":\"" + value + "\"" + desc + "}'>" + displayName + descInfo + "</a></li>");
					len++;
					AutoCompleteLiClick(varli);
					child.append(varli);
				}); //end each
				//处理高度
				var dHeight = 20;
				if (len > 0)
					dHeight = len * 19;
				$("#popup" + mChildID).css("height", dHeight + "px");
			}
		},
		error: function (xhr) {
		}
	});        //end ajax
}

function AutoCompletehideButThis(id) {
	var mid = "popup" + id;
	$(".auto-complete-popup").each(function (i, o) {
		if ($(o).attr("id") != mid)
			$(o).css("visibility", "hidden");
	});
}

function AutoCompleteTogeter(event, opts) {
	var id = opts.attr("id").substring(3);
	var item = $("#popup" + id);
	AutoCompletehideButThis(id);
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

$.AutoComplete = {	
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
					AutoCompleteTogeter(event, popdiv);
				});
			}
			if (opts.isClearValue) {
				if ($("#" + opts.id).val() != "") {
					$("#" + opts.id).val("");
					$("#" + opts.id).change();
					$("#caption" + opts.id).find("a").html("");
					if (opts.Target)
						AutoCompleteClearDescValue(opts.Target);
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
		AutoCompleteInitDpdValue(obj);
		if (opts.val == "") {
			AutoCompleteClearDescValue(opts.id);
		}
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


