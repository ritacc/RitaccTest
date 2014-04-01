/// <reference path="../jQuery/jquery-1.4.1-vsdoc.js" />

// 声明一个全局对象Namespace，用来注册命名空间
var Namespace;
if (typeof Namespace == "undefined" || !Namespace) {
    Namespace = new Object();
}
// 全局对象仅仅存在register函数，参数为名称空间全路径，如"Grandsoft.GEA"
Namespace.register = function (fullNS) {
    // 将命名空间切成N部分, 比如Grandsoft、GEA等
    var nsArray = fullNS.split('.');
    var sEval = "";
    var sNS = "";
    for (var i = 0; i < nsArray.length; i++) {
        if (i != 0) sNS += ".";
        sNS += nsArray[i];
        // 依次创建构造命名空间对象（假如不存在的话）的语句
        // 比如先创建Grandsoft，然后创建Grandsoft.GEA，依次下去
        sEval += "if (typeof(" + sNS + ") == 'undefined') " + sNS + " = new Object();"
    }
    if (sEval != "") eval(sEval);
}

// 公共方法
var $t = {
    stringBuilder: function () { // 字符串构造器
        this.buffer = [];
    },
    round: function (value, digits) { // 保留指定小数位
        if (value || value == 0) { return parseFloat(value.toFixed(digits)); }
        return null;
    },
    toJson: function (o) { // 转换成 Json
        var result = [];
        for (var key in o) {
            var value = o[key];
            if (typeof value != 'object' || value == null) {
                if (/\/Date\(-{0,1}\d+\)\//.test(value)) {
                    value = this.changeDateFormat(value);
                }
                if (value == null) {
                    result.push('"' + key + '":' + value);
                }
                else {
                    result.push('"' + key + '":"' + value + '"');
                }
            }
            else { result.push('"' + key + '":' + this.toJson(value)); }
        }
        return '{' + result.join(',') + '}';
    },
    f: function (n) {
        return n < 10 ? '0' + n : n;
    },
    changeDateOfYMD: function (value) {
        var result = '';
        if (/\/Date\(-{0,1}\d+\)\//.test(value)) {
            if (value == "/Date(-62135596800000)/") {
                return '';
            } else {
                value = value.replace("/Date(", "").replace(")/", "");
                if (value.indexOf("+") > 0) {
                    value = value.substring(0, value.indexOf("+"));
                }
                else if (value.indexOf("-") > 0) {
                    value = value.substring(0, value.indexOf("-"));
                }
                var date = new Date(parseInt(value, 10));
                result = isFinite(date.valueOf()) ?
           '' + date.getFullYear() + '-' +
           this.f(date.getMonth() + 1) + '-' +
           this.f(date.getDate()) + ' '
                //+
                //f(date.getHours()) + ':' +
                //f(date.getMinutes()) + ':' +
                //f(date.getSeconds()) + '' 
           : null;
            }
        } else {
            result = value;
        }
        return result;
    },
    changeDateOfYMDHMS: function (value) {
        var result = '';
        if (/\/Date\(-{0,1}\d+\)\//.test(value)) {
            if (value == "/Date(-62135596800000)/") {
                return '0001-01-01';
            } else {
                value = value.replace("/Date(", "").replace(")/", "");
                if (value.indexOf("+") > 0) {
                    value = value.substring(0, value.indexOf("+"));
                }
                else if (value.indexOf("-") > 0) {
                    value = value.substring(0, value.indexOf("-"));
                }
                var date = new Date(parseInt(value, 10));
                result = isFinite(date.valueOf()) ?
           '' + date.getFullYear() + '-' +
           this.f(date.getMonth() + 1) + '-' +
           this.f(date.getDate()) + ' ' +
           this.f(date.getHours()) + ':' +
           this.f(date.getMinutes()) + ':' +
           this.f(date.getSeconds()) + ''
           : null;
            }
        } else {
            result = value;
        }
        return result;
    },
    //在给定对象的范围内,查询 a标签,取消原来的绑定,新绑定. 取消默认行为的方法
    bindAPreventDefault: function (obj) {
        obj.find(" a").attr("disabled", "disabled").unbind("click").bind("click", $t.preventDefault);
    },
    delegate: function (context, handler) { // 绑定事件
        return function (e) {
            handler.apply(context, [e, this]);
        };
    },
    stop: function (handler, context) { // 阻止冒泡
        return function (e) {
            e.stopPropagation();
            handler.apply(context || this, arguments);
        };
    },
    stopAll: function (handler, context) { // 阻止 all
        return function (e) {
            e.preventDefault();
            e.stopPropagation();
            handler.apply(context || this, arguments);
        }
    },
    preventDefault: function (e) { // 阴止默认行为
        e.preventDefault();
    },
    trigger: function (element, eventName, e) { // 事件触发器
        e = $.extend(e || {}, new $.Event(eventName));
        e.stopPropagation();
        $(element).trigger(e);
        return e.isDefaultPrevented();
    },
    //使用 ajax获取 内容.get提交.并刷新 grid..
    ajaxGetInfoOfGet: function (divObj, url, data, callBack) {
        $t._ajaxGetInfo(divObj, url, "get", data, callBack);
    },
    //使用 ajax获取 内容.post提交.并刷新 grid..
    ajaxGetInfoOfPost: function (divObj, url, data, callBack) {
        $t._ajaxGetInfo(divObj, url, "post", data, callBack);
    },
    //给 a 标签 添加事件
    pageAClick: function (divObj, data, callBack) {
        $t._pageAClick(divObj, "get", data, callBack);
    },
    //给 a 标签 添加事件
    pageAClickOfGet: function (divObj, data, callBack) {
        $t._pageAClick(divObj, "get", data, callBack);
    },
    pageAClickOfPost: function (divObj, data, callBack) {
        $t._pageAClick(divObj, "post", data, callBack);
    },
    //使用 ajax获取 内容..并刷新 grid
    _ajaxGetInfo: function (divObj, url, method, data, callBack) {
        var f = function (htmlResult) {
            divObj.html(htmlResult);
            $t.gridListGrid(divObj);
            $t.renderTable(divObj);
            if ($.isFunction(callBack)) {
                callBack(htmlResult);
            }
        };
        if (method == "get") {
            $.get(url, data, f);
        } else {
            $.post(url, data, f);
        }
    },
    //给 a 标签 添加事件
    _pageAClick: function (divObj, method, data, callBack) {
        divObj.find("a").die("click").live("click", function (e) {
            e.preventDefault();
            var f = function (htmlResult) {
                divObj.html(htmlResult);
                $t.gridListGrid(divObj); // Grid  
                $t.renderTable(divObj);
                if ($.isFunction(callBack)) {
                    callBack(htmlResult);
                }
            };
            if (method == "get") {
                $.get($(this).attr("href"), data, f);
            } else {
                $.post($(this).attr("href"), data, f);
            }
        });
        var pager = function () {
            var f = function (htmlResult) {
                divObj.html(htmlResult);
                $t.gridListGrid(divObj); // Grid  
                $t.renderTable(divObj);
                if ($.isFunction(callBack)) {
                    callBack(htmlResult);
                }
            };
            var iv = divObj.find("div.pager input.i").val();
            var hv = divObj.find("div.pager :hidden").val();

            if (method == "get") {
                $.get(hv.replace(/-p-/ig, iv == "" ? 1 : iv), data, f);
            } else {
                $.post(hv.replace(/-p-/ig, iv == "" ? 1 : iv), data, f);
            }
        };
        divObj.find("div.pager :button").die("click").live("click", pager);
        divObj.find("div.pager input.i").die("keypress").live("keypress", function (e) {
            if (e.which == 13) { pager(); }
            else { return e.which >= 48 && e.which <= 57; }
        });


    },
    gridListGrid: function (divObj) {
        if (divObj != undefined) {
            divObj.find(" div.grid_list").each(function () {
                $grids[$(this).attr("id")] = $(this).grid();
            });
        } else {
            $("div.grid_list").each(function () {
                $grids[$(this).attr("id")] = $(this).grid();
            });
        }
    },
    //AJAX 分页中,给 grid_list添加样式
    sortAddClass: function (obj, sortField, sortDirection) {
        if (obj == undefined || obj == null) {
            obj = $(document);
        }
        obj.find("div.grid_list").find("th.sort")
            .eq(sortField).addClass(sortDirection == "-1" ? "sort_desc" : "sort_asc");
    },
    renderTable: function (table) {

        table.find("tbody > tr").each(function () {
            var cbs = $(this).find(":checkbox[name='cbKey'],:radio[name='cbKey1']");
            cbs.click(function () {
                $(this).attr("checked", !$(this).attr("checked"));
            }).end().bind({
                click: $t.delegate(this, tr.click),
                mouseover: $t.delegate(this, tr.mouseOver),
                mouseout: $t.delegate(this, tr.mouseOut)
            });
            if (cbs.attr("checked")) {
                tr.check.call(this);
            }
        }).end().find("tbody > tr:odd").addClass("alter");

        //上下 的移位
        table.find("tbody > tr").parent("tbody").unbind("keydown").bind("keydown", function (e) {
            if (e.keyCode === 38) {
                var tr1 = $(this).find("tr :checkbox:checked:not(:disabled)[name='cbKey']")
                .parent("td").parent("tr");
                if (tr1.length <= 0) {
                    tr1 = $(this).find("tr :radio:checked:not(:disabled)[name='cbKey1']")
                     .parent("td").parent("tr");
                    var prev = tr1.prev("tr");
                    if (prev.length == 1) {
                        prev.click();
                    } else {
                        tr1.parent("tbody").find("tr").last().click();
                    }
                } else {
                    var prev = tr1.prev("tr");
                    if (prev.length > 0) {
                        var firstTr = prev.first();
                        if (firstTr.length == 1) {
                            var cb = firstTr.find(" :checkbox:not(:disabled)[name='cbKey']");
                            cb.attr("checked", true);
                            tr.check.call(firstTr);
                        }

                        //prev.click();
                    } else {
                        tr1.parent("tbody").find("tr").last().click();
                    }
                }
            } else if (e.keyCode === 40) {

                var tr2 = $(this).find("tr :checkbox:checked:not(:disabled)[name='cbKey']")
                 .parent("td").parent("tr");
                if (tr2.length <= 0) {
                    tr2 = $(this).find("tr :radio:checked:not(:disabled)[name='cbKey1']")
                    .parent("td").parent("tr");
                    var next = tr2.next("tr");
                    if (next.length == 1) {
                        next.click();
                    } else {
                        tr2.parent("tbody").find("tr").first().click();
                    }
                } else {
                    var next = tr2.next("tr");
                    if (next.length > 0) {
                        //next.click();
                        var nextTr = next.last();
                        if (nextTr.length == 1) {
                            var cb = nextTr.find(" :checkbox:not(:disabled)[name='cbKey']");
                            cb.attr("checked", true);
                            tr.check.call(nextTr);
                        }
                    } else {
                        tr2.parent("tbody").find("tr").first().click();
                    }
                }

            }
            if (e.keyCode === 38 || e.keyCode === 40) {
                e.preventDefault();
                e.stopPropagation();
            }
            //return false;
        });
    },
    removeBackColor: function (table) { //移除背景颜色
        table.find("tbody > tr").css("background-color", "").each(function () {
            $(this).find(":radio[name='cbKey1']").attr("checked", false);
        });
    },
    changeDateFormat: function (jsondate) {
        jsondate = jsondate.replace("/Date(", "").replace(")/", "");
        if (jsondate.indexOf("+") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("+"));
        }
        else if (jsondate.indexOf("-") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("-"));
        }

        var date = new Date(parseInt(jsondate, 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var year = date.getFullYear();
        if (year == 1) {  //\/Date(-62135596800000)\/ 此种格式的值为 0001/1/1 0:00:00
            return "0001-" + month + "-" + currentDate; ;
        }
        return year + "-" + month + "-" + currentDate;
    }
};
// 构造字符串
$t.stringBuilder.prototype = {
    cat: function (what) {
        this.buffer.push(what);
        return this;
    },
    rep: function (what, howManyTimes) {
        for (var i = 0; i < howManyTimes; i++) { this.cat(what); }
        return this;
    },
    catIf: function (what, condition) {
        if (condition) { this.cat(what); }
        return this;
    },
    string: function () {
        return this.buffer.join('');
    }
};
//将指定的天数加到此日期 
Date.prototype.addDays = function (value) {
    var date = this.getDate();
    this.setDate(date + value);
    return this;
};
Date.prototype.formatForConfig = function (value) {
    var _days = this.getDate();
    var _month = this.getMonth() + 1;
    return (_days > 9 ? _days : '0' + _days) + '-' + (_month > 9 ? _month : '0' + _month) + '-' + this.getFullYear();
}; 

// 行操作
var tr = {
    backColor: "#FFD58D",
    check: function () {
        var s = $(this);
        if (s.find(":checkbox[name='cbKey'],:radio[name='cbKey1']").attr("checked")) {
            s.unbind("mouseover").unbind("mouseout").css("background-color", tr.backColor);
            if (window.trCheck)
                window.trCheck(s);
        } else {

            s.bind({
                mouseover: $t.delegate(this, tr.mouseOver),
                mouseout: $t.delegate(this, tr.mouseOut)
            }).css("background-color", "");
        }
    },
    mouseOver: function () {
        var s = $(this);
        if (!s.find(":checkbox[name='cbKey'],:radio[name='cbKey1']").attr("checked"))
            s.css({ backgroundColor: tr.backColor });
    },
    mouseOut: function () {
        var s = $(this);
        if (!s.find(":checkbox[name='cbKey'],:radio[name='cbKey1']").attr("checked"))
            s.css({ backgroundColor: "" });
    },
    click: function () {
        var cb = $(this).find(":checkbox[name='cbKey'],:radio[name='cbKey1']");
        var isMultipleChoose = $(this).parent().parent().parent().hasClass("isMultipleChoose");
        if (!isMultipleChoose) {
            $(this).siblings("tr").each(function () {
                var cb1 = $(this).find(":checkbox[name='cbKey'],:radio[name='cbKey1']");
                if ($(this)[0].style.backgroundColor != "" || cb1.attr("checked")) {
                    //var cb1 = $(this).find(":checkbox[name='cbKey'],:radio[name='cbKey1']");
                    cb1.attr("checked", cb1.attr("type") == "checkbox" ? !cb1.attr("checked") : true);
                    tr.check.call(this);
                }
            });

            cb.attr("checked", cb.attr("type") == "checkbox" ? !cb.attr("checked") : true);
            tr.check.call(this);
        }
        else {
            if (!cb.attr("disabled")) {
                cb.attr("checked", cb.attr("type") == "checkbox" ? !cb.attr("checked") : true);
                tr.check.call(this);
            }
            if (cb.attr("type") == "radio") {
                $(this).siblings("tr[backgroundColor!='']").each(function () {
                    tr.check.call(this);
                });
            }
        }
    }
};

// ------------- 全局变量定义 -------------
// 模型状态
var ModelStatusEnum = { Normal: 0, New: 1, Edit: 2, Delete: 3 };
var BaseModelItem = { CompanyId: 125, CreatedBy: 102, CreatedDate: "1900-01-01", LastUpdatedBy: 889, LastUpdatedDate: "1900-01-01" };
// 资源文件内容
var $g;
// 所有 combobox 实例集
var $comboboxs = {};
// 所有 grid 实例集
var $grids = {};
// 所有 numeric 实例集
var $numerics = {};
// 所有 lov 实例集
var $lovs = {}
if (window.ImageResources == undefined)
    window.ImageResources = '/content/css/images/';
   $(function () {
   	(function () {  //菜单的隐藏，使其在多个Iframe的情况下也生效
   		var win = window;
   		while (win != win.parent) {
   			$(win.document).mouseup(function () {
   				var doc = $(win.parent.document)[0];
   				if (doc.fireEvent)
   					$(win.parent.document)[0].fireEvent("onmouseup");
   				else
   					$(win.parent.document).mouseup();
   			});
   			win = win.parent;
   		}
   	})();

   	(function () {
   		$("input[type=text]").mouseenter(function () {
   			var $this = $(this);
   			var customTitle = $this.attr("CustomTitle");
   			if (customTitle != null && customTitle != "") {
   				$this.attr("title", customTitle);
   			} else {
   				$this.attr("title", $this.val());
   			}
   		});
   		$("label").mouseenter(function () {
   			var $this = $(this);
   			var customTitle = $this.attr("CustomTitle");
   			if (customTitle != null && customTitle != "") {
   				$this.attr("title", customTitle);
   			} else {
   				$this.attr("title", $this.text());
   			}
   		});
   		$("label").each(function () {
   			var $this = $(this);
   			if ($this.hasClass("label_readonly") && $this.hasClass("replaceto_readonly_text")) {
   				var target = $('<input>', { type: 'text', readonly: 'readonly', Class: "label_readonly " + $this.attr("class"), value: $this.text(), style: "min-width:" + $this.css("width"), id: $this.attr("id") });
   				$this.replaceWith(target);
   			}
   		});
   	})();

   	//需要确认
   	(function () {
   		$(".needConfirm").unbind("click").click(function (e) {
   			//$(this).unbind("click");
   			//e.preventDefault();
   			if (window.checkChanged) {
   				return confirm($g["SureLeave1"] || "确定进行此操作吗 ?");
   			}
   		});
   	})();

   	(function () { // 全局 jQuery 中 ajax 请求参数
   		if (window.noAjaxLoding)
   			return; //先不使用此方法 by xianhong
   		var win = window;
   		var topWin = getTopWindows();
   		//var $ = win.$;
   		$.ajaxSetup({ cache: false });
   		var xiaoImgAjaxLoading = $("<img>", { src: topWin.ImageResources + "xiao001.gif" });
   		$(document).ajaxStart(function () {
   			var img = $("<img>", { src: topWin.ImageResources + "Loading.gif" });
   			var load = $("#ajax_loading");
   			if (load.length >= 1) {
   				load.remove();
   			}
   			load = $("<div>", { id: "ajax_loading" }).appendTo($(document.body));
   			load.html("");
   			load.append(img).css({
   				position: "absolute", //"absolute",
   				zIndex: "9999",
   				textAlign: "center",
   				backgroundColor: "#fff",
   				border: "solid 4px #ebebeb",
   				//padding: "15px",
   				paddingTop: "18px",
   				fontSize: "14px",
   				top: "50%",
   				left: "50%",
   				height: "31px",
   				width: "150px",
   				marginTop: "-38.5px",
   				marginLeft: "-79px"
   			});
   		}).ajaxError(function (event, XMLHttpRequest, ajaxOptions, thrownError) {
   			var ajax_load = $("#ajax_loading");
   			//ajax_load.html(topWin.$g["AjaxFailure"]);

   			win.setTimeout(function () {
   				ajax_load.remove();
   			}, 10);
   		}).ajaxSuccess(function (event, xhr, ajaxOptions) {
   			/*try {
   			var json = eval('(' + xhr.responseText + ')');
   			if (json.Logged == false || json.Logged == "false") {
   			alert('还未登录或登录超时，请重新登录');
   			}
   			} catch (e) { }
   			*/
   			$("#ajax_loading").remove();
   		})
   	})();
   	(function () {
   		$("form").each(function (i) {
   			var action = $(this).attr("action");
   			var lHref = window.location.href;

   			lHref = lHref.replace(/http:\/\//g, "");

   			var tmpFun = function (UseNewType) {
   				$("input[type='text']").each(function () {
   					var $this = $(this);
   					if (!$this.hasClass("textboxDepart")) {
   						if (UseNewType) {
   							$this.replaceWith($('<input>', { type: 'text'
														, readonly: 'readonly'
														, Class: "label_readonly " + $this.attr("class").replace("calendar", "")
														, value: $this.valOrHtml()
														, style: "min-width:" + $this.css("width") + ";width:" + $this.css("width") + ";"
														, id: $this.attr("id")
														, title: $this.attr("CustomTitle") == null ? $this.val() : $this.attr("CustomTitle")
   							}));
   						} else {
   							$this.replaceWith($("<lable>", { Class: "label_readonly"
														, html: $this.valOrHtml()
														, /*style: "width:" + $this.outerWidth()+ "px;"*/width: $this.css("width")
														, height: $this.css("height")
														, id: $this.attr("id")
														, title: $this.attr("CustomTitle") == null ? $this.text() : $this.attr("CustomTitle")
   							}));
   						}
   					}
   				});

   				$("textarea").each(function () {
   					var $this = $(this);
   					if (!$this.hasClass("textboxDepart")) {
   						if (UseNewType) {
   							$this.replaceWith($('<textarea>', { readonly: 'readonly'
														, Class: "label_readonly " + $this.attr("class")
														, value: $this.valOrHtml()
														, style: "min-height:" + $this.css("height") + ";height:" + $this.css("height") + ";min-width:" + $this.css("width") + ";width:" + $this.css("width") + ";"
														, id: $this.attr("id")
														, title: $this.attr("CustomTitle") == null ? $this.val() : $this.attr("CustomTitle")
   							}));
   						} else {
   							$this.replaceWith($("<lable>", { Class: "label_readonly"
														, html: $this.valOrHtml()
														, /*style: "width:" + $this.outerWidth()+ "px;"*/width: $this.css("width")
														, height: $this.css("height")
														, id: $this.attr("id")
														, title: $this.attr("CustomTitle") == null ? $this.text() : $this.attr("CustomTitle")
   							}));
   						}
   					}
   				});


   				$("select").each(function () {
   					var $this = $(this);
   					//commented by jason in 20130426
   					//$this.replaceWith($("<lable>", { Class: "label_readonly", html: $this.find("option:selected").html(), width: $this.css("width"), id: $this.attr("id") }));
   					//added by jason in 20130426
   					$this.replaceWith($("<lable>", { Class: "label_readonly", html: $this.find("option:selected").html(), width: $this.css("width") == "auto" ? "auto" : (parseInt($this.css("width").replace("px", "")) - 5) + "px", id: $this.attr("id") }));
   				});

   				$(":checkbox").die().unbind("click").click(function () {
   					return false;
   				});

   				$(":submit").hide();

   				$(".img_hide_on_view").hide();

   				if ($.fn.formSerialize) {
   					lastHtml = $(window.document.body).find("form").formSerialize();
   					lastFormHtml = $(window.document.body).find("form").formSerialize();
   				}

   				//处理DropdownListEx
   				$(".textboxDepart").each(function (i, o) {
   					var obj = $(o);
   					var id = obj.attr("id");
   					var divObj = $("#div" + id);
   					divObj.addClass("label_readonly");
   					divObj.find(".auto-complete-buttom-click").remove();
   					divObj.unbind("click");
   					obj.attr("disabled", "");
   				});
   			};

   			var locHref = location.href;
   			if (action.indexOf("View") >= 0 || locHref.indexOf("[View]") >= 0) {
   				if (action.indexOf("NewView") >= 0 || locHref.indexOf("[NewView]") >= 0) {
   					tmpFun(true);
   				} else {
   					tmpFun(false);
   				}
   				return;
   			}

   			action = lHref.substring(0, lHref.indexOf("/")) + action;
   			action = "http://" + action;
   			$.getJSON(window.GetCurrentUserHasPermission || '../../Ajax/GetCurrentUserHasPermission', { url: action }, function (data) {
   				if (!data.Result) { //如果没有权限

   					tmpFun();

   				} else {

   				}
   			});
   		});

   	})();
   	(function () { // 读入资源文件内容
   		if (window.parent == window && !$g) {
   			$.getJSON(window.Resources || '../../Ajax/ResourceText', { className: "GlobalText", _: new Date().getTime() }, function (o) {
   				$g = o;
   			});
   		} else {
   			var tmp$g = $g;
   			var win = window;
   			while (win.parent != win) {
   				win = win.parent;
   				tmp$g = win.$g;
   			}
   			$g = tmp$g;
   		}
   	})();
   	(function () { // 菜单操作

   		if (typeof (MenuClass) == "undefined" || location.href.indexOf(".htm") > 0)
   			return;
   		var menu = new MenuClass({ arrowUrl: appRootPath + "Content/css/Images/right.png" }); menu.Apply(); try { window.CreateMenuItems(menu); } catch (e) { }
   	})();
   	(function () { // 调整显示层  

   		var dc = $("div.centra"), dl = $("div.grid_list"), flag = false;
   		var tabContainer = $("div.tab_container");
   		// 改变窗口大小
   		if (typeof (customerResize) != "undefined")
   			return;
   		var cti = $("div.cti");
   		var search = $("div.search");
   		var menu = $(".centra>div.menu");
   		var buttons = $("div.buttons");
   		var message = $("div.message");
   		var pager = $("div.pager");
   		var tabBtn = $("#tabBtn");
   		//added by jason in 20130521 for more than one grid
   		var div_horizontal = $("div.div_horizontal");
   		var div_vertical = $("div.div_vertical");
   		var div_other = $("div.div_other");
   		//end added by jason in 20130521 for more than one grid
   		$(window).resize(function () {
   			var wHeight = $(window).height();
   			if (wHeight <= 0)
   				return;
   			dc.height($(window).height() - $("div.top").outerHeight() - ($("div.bottom").outerHeight()) - ($("div.message").outerHeight()));
   			if (tabContainer.length) {
   				tabContainer.height(dc.outerHeight() - cti.outerHeight() - search.outerHeight() - 35);
   			}

   			window.needReduceSize = window.needReduceSize ? window.needReduceSize : 0;
   			if (dl.length) {
   				var dlH = parseInt(dl.css("height"));
   				if (isNaN(dlH) || dlH === 0 || flag) {
   					flag = true;
   					//commented by jason in 20130521 more than one grid 
   					//dl.height(dc.outerHeight() - pager.outerHeight() - search.outerHeight() - menu.outerHeight() - buttons.outerHeight() - message.outerHeight() - tabBtn.outerHeight() - 2 - window.needReduceSize);
   					//end commented by jason in 20130521 more than one grid

   					//added by jason in 20130521 more than one grid
   					var divOtherHeight = 0;
   					$.each(div_other, function () {
   						if ($(this).parent().attr("class") !== "div_horizontal" && $(this).parent().attr("class") !== "div_vertical") {
   							divOtherHeight = divOtherHeight + $(this).outerHeight();
   						}
   					});
   					var remainHeight = (dc.outerHeight() - pager.outerHeight() - search.outerHeight() - menu.outerHeight() - buttons.outerHeight() - message.outerHeight() - divOtherHeight - tabBtn.outerHeight() - 5 - window.needReduceSize);
   					if (div_horizontal.length > 0) {
   						var beCalcDlLength = 0;
   						$.each(dl, function () {
   							if ($(this).parent().attr("class") !== "div_horizontal" && $(this).parent().attr("class") !== "div_vertical") {
   								beCalcDlLength = beCalcDlLength + 1;
   							}
   						});
   						remainHeight = remainHeight - 8;
   						var eachHeight = (parseInt(remainHeight / (div_horizontal.length + beCalcDlLength)));

   						$.each(div_horizontal, function () {
   							$(this).height(eachHeight);
   						});

   						$.each(div_vertical, function () {
   							$(this).height(remainHeight);
   						});

   						$.each(dl, function () {
   							var needMinusHeight;
   							if ($(this).parent().attr("class") == "div_horizontal") {
   								needMinusHeight = 0;
   								$.each($(this).siblings(), function () {
   									needMinusHeight = needMinusHeight + $(this).outerHeight();
   								});
   								$(this).height(eachHeight - needMinusHeight - 5);
   							} else if ($(this).parent().attr("class") == "div_vertical") {
   								needMinusHeight = 0;
   								$.each($(this).siblings(), function () {
   									needMinusHeight = needMinusHeight + $(this).outerHeight();
   								});
   								$(this).height(remainHeight - needMinusHeight);
   							} else {
   								$(this).height(eachHeight);
   							}
   						});
   					} else {
   						dl.height(remainHeight);
   					}
   					//end added by jason in 20130521 more than one grid
   				}
   			}
   		}).resize();
   		setTimeout(function () { $(window).resize(); }, 1000);
   	})();
   	(function () { // 按钮操作
   		$(":button[url]").click(function () {
   			try {
   				var url = $(this).attr("url");
   				document.location.href = url;
   				return false;
   			} catch (e) { }
   		});
   	})();
   	$t.gridListGrid($(document)); // Grid
   	(function () { // Combobox
   		$("select.combobox").each(function () {
   			var options = {};
   			try { eval("var options = " + $(this).attr("options")); } catch (e) { }
   			$comboboxs[$(this).attr("id")] = $(this).combobox(options);
   		});
   	})();
   	(function () {
   		$("input,textarea").focus(function () {
   			$(this).select();
   		});
   	})();
   	(function () {
   		$(document).keydown(function (e) {
   			if (e.keyCode == 8 && e.srcElement.tagName != "INPUT" && e.srcElement.type != "text" && e.srcElement.tagName != "TEXTAREA") {
   				e.returnValue = false;
   				return false;
   			}
   		});
   	})();
   	(function () { // 日历
   		$("input.calendar").css("padding-left", "23px").focus(function () {
   			$(this).val($(this).val().replace(/[^\d]+/ig, ""));
   			$(this).select();
   			var c = new CalendarClass();
   			c.Element = $(this)[0];
   			c.Lang = window.Language;
   			c.Format = window.DataFormat;
   			//2013-11-05 by zcs 用于修改today 显示交易日期
   			if (window.TranDate)
   				c.TranDate = window.TranDate;
   			//2013-11-05 by zcs 用于修改today 显示交易日期 end
   			try {
   				var options = {};
   				eval("var options = " + $(this).attr("options"));
   				for (var i in options) { c[i] = options[i]; }
   				c.Apply();
   			} catch (e) { alert(e); }
   		})
		.click(function () {
			$(this).val($(this).val().replace(/[^\d]+/ig, ""));
			$(this).select();
			var c = new CalendarClass();
			c.Element = $(this)[0];
			c.Lang = window.Language;
			c.Format = window.DataFormat;
			//2013-11-05 by zcs 用于修改today 显示交易日期
			if (window.TranDate)
				c.TranDate = window.TranDate;
			//2013-11-05 by zcs 用于修改today 显示交易日期 end
			try {
				var options = {};
				eval("var options = " + $(this).attr("options"));
				for (var i in options) { c[i] = options[i]; }
				c.Apply();
			} catch (e) { alert(e); }
		});
   	})();
   	(function () { // 数字输入框
   		$("input.numeric").each(function () {
   			var options = {};
   			try { eval("var options = " + $(this).attr("options")); } catch (e) { }
   			$numerics[$(this).attr("id")] = $(this).numericTextbox(options);
   		});

   		//Added by JLD on 2012-11-27
   		//带小数点的数字输入框
   		$("input.decimal_numeric").each(function () {
   			var options = {};
   			try { eval("var options = " + $(this).attr("options")); } catch (e) { }
   			$numerics[$(this).attr("id")] = $(this).numericTextbox(options);
   		});
   	})();
   	(function () { // LOV
   		$("input.lov").each(function () {
   			var options = {};
   			alert("d");
   			try { eval("var options = " + $(this).attr("options")); } catch (e) { }
   			$lovs[$(this).attr("id")] = $(this).LOV(options);
   		});
   	})();
   	/*(function () { // 分页跳转
   	var pager = function () {
   	var iv = $("div.pager input.i").val();
   	var hv = $("div.pager :hidden").val();
   	window.location.assign(hv.replace(/-p-/ig, iv == "" ? 1 : iv));
   	};
   	$("div.pager :button").click(pager);
   	$("div.pager input.i").keypress(function (e) {
   	if (e.which == 13) { pager(); }
   	else { return e.which >= 48 && e.which <= 57; }
   	});
   	})();*/
   	(function () { // 列表操作
   		$t.renderTable($("div.grid_list").find("table"));
   		//全选
   		$(":checkbox.cball").click(function () {
   			$(":checkbox[name='cbKey'][disabled='false']").attr("checked", $(this).attr("checked")).each(function () {
   				tr.check.call($(this).parents("tr"));
   			});
   		});
   	})();

   	$t.sortAddClass(null, window.SortField, window.SortDirection); // 排序

   	(function () { // 删除搜索
   		$(":button[name='btnresetsearch']").click(function () {
   			document.location.href = window.SearchReset;
   		});
   	})();
   	(function () { // Tabs
   		$("div.tab_container").each(function () {
   			if (!$(this).hasClass("customerInit"))
   				$(this).tabs();
   		});
   	})();

   	(function () {
   		$("body").bind('keyup', function (event) {
   			if (event.keyCode == 13) {
   				$(".btnSure:visible").eq(0).click();
   			}
   		});
   	})();


   	//对备注的长度进行控制
   	function textareaLength() {
   		$("textarea").each(function () {
   			var txtArea = $(this);
   			var length = txtArea.attr("tamaxlength");
   			if (length == undefined || isNaN(length)) {
   				txtArea.attr("tamaxlength", "200");
   			}
   			var _intDo;
   			txtArea.bind('focus', function () {
   				_intDo = setInterval(function () { stat(txtArea); }, 10);
   			});
   			txtArea.bind('blur', function () {
   				clearInterval(_intDo);
   			});
   		});
   	}

   	var _len = 0;
   	var _olen = 0;
   	function stat(e) {
   		var area = e;
   		_len = area.val().length;
   		//$("#SY_UserModel_Name").val(Math.random());
   		if (_len == _olen) return;  // 防止用计时器监听时做无谓的牺牲...
   		var maxlength = area.attr("tamaxlength");
   		if (area.val().length > maxlength) {
   			area.val(area.val().substr(0, maxlength));
   		}
   		_len = _olen = area.val().length;
   	}

   	textareaLength();

   	(function () { // 修改系统日期
   		$("#btnmodifyfmcgcalendar").click(function () {
   			if (confirm($g["ModifySystemDateConfirm"])) {
   				$.get(window.UpdateSDate, { sdate: $(this).siblings("input").eq(0).val() }, function (text) {
   					alert(text);
   				}, "text");
   			}
   		});
   	})();
   });


function renderCalendar() {
    $("input.calendar").css("padding-left", "23px").focus(function () {
        $(this).val($(this).val().replace(/[^\d]+/ig, ""));
        $(this).select();
        var c = new CalendarClass();
        c.Element = $(this)[0];
        c.Lang = window.Language;
        c.Format = window.DataFormat;
        try {
            var options = {};
            eval("var options = " + $(this).attr("options"));
            for (var i in options) { c[i] = options[i]; }
            c.Apply();
        } catch (e) { alert(e); }
    });
}


function getTopWindows() {
    var win = window;
    while (win != win.parent) {
        win = win.parent;
    }
    return win;
}

function getDeleteDiv() {
    var win = getTopWindows();


    //    var divShadeContainer = win.document.body;
    //    this.divShade = this.divShade || $("<div></div>").appendTo(divShadeContainer)
    //                    .addClass("div_shade_dialog").css("z-index", dialogIndex - 1);
    //    winResize = function () {
    //        var windowW = $(win).width(), windowH = $(win).height(),
    //                        bodyW = $(win.document.body).width(), bodyH = $(win.document.body).height();
    //        self.divShade.width(windowW < bodyW ? bodyW : windowW)
    //                    .height(windowH < bodyH ? bodyH : windowH);
    //    };
    //    $(win).resize(winResize);

    //    setTimeout(winResize, 10);


    var div = win.$("#hideForDeleteDiv");
    if (div.length <= 0) {
        div = win.$("<div>", { id: "hideForDeleteDiv" });

        /*
        div.addClass("div_shade_dialog").appendTo($(win.document.body)); //.css("background-color", "#fff");

        winResize = function () {
        var windowW = $(win).width(), windowH = $(win).height(),
        bodyW = $(win.document.body).width(), bodyH = $(win.document.body).height();
        div.width(windowW < bodyW ? bodyW : windowW)
        .height(windowH < bodyH ? bodyH : windowH);
        };
        $(win).resize(winResize);
        setTimeout(winResize, 10);
        */
        div.attr("noResize", "true");
        var win$ = $(win);
        div.css({
            position: "absolute",
            zIndex: "9999",
            width: 0,
            height: 0
        }).css({
            top: (win$.height() - 40) / 2 + win$.scrollTop(),
            left: (win$.width() - 100) / 2 + win$.scrollLeft()
        });
        div.appendTo(win.document.body);
    }

    return div;
}

//关闭弹出窗口
var closeDialogById = function (dlgId) {
    var dlg = getAllWinElementById(dlgId);
    dlg.parents(".topContainter").find(".div_title_close_dialog").attr("noClosing", "true").click();
};

var closeDialogByIdEx = function (dlgId) {
   	var shade = window.parent.$(".div_shade_dialog");
   	var shadeLength = shade.length;
   	$(shade[shadeLength - 1]).remove();

	var dlg = getAllWinElementById(dlgId);
	dlg.parents(".div_container_dialog").remove();
};

//调用jquery的html或val方法
$.fn.valOrHtml = function (str) {
    var ctl = $(this);
    if (!ctl.length) return null;
    var tagName = ctl[0].tagName;
    if (!tagName) return ctl.val(str);
    var cType = tagName.toLowerCase();

    switch (cType) {
        case "input":
        case "select":
            return ctl.val(str);
            break;
        default:
            return ctl.html(str);
            break;
    }
};

//获取一个Url,使用json方式
var getUrl = function (url, pars) {
    for (var item in pars) {
        if (!pars[item]) continue;
        url += url.indexOf("?") < 0 ? "?" : "&";
        url += item + "=" + pars[item];
    }
    return url;
};

//打开window
function xOpenWindow(url, uniqueId, width, height, menubar, resizable, status) {
    var myWin, args;
    var Left, Top;
    if (width == null)
        Left = 0;
    else
        Left = (screen.width - width) / 2;

    if (height == null)
        Top = 0;
    else
        Top = (screen.height - height) / 2 - 20;

    if (resizable == null)
        resizable = 'yes';

    if (status == null)
        status = 'yes';

    args = 'left=' + Left + ',top=' + Top + ',width=' + width + ',height=' + height + ',menubar=' + menubar + ',toolbar=no,location=no,resizable=' + resizable + ',scrollbars=no,status=' + status;
    myWin = window.open(url, uniqueId, args);
    return myWin;
}
//在浏览器中所有Window里获取对象
function getAllWinElement(ex) {
    var pWin = getTopWindows();
    return getWinElement(pWin, ex);
}

//在浏览器中所有Window里获取对象
function getAllWinElementById(idStr) {
    var pWin = getTopWindows();
    //console.log("topWin:" + pWin.location.href);
    return getWinElement(pWin, "#" + idStr);
}


//递归方法
function getWinElement(win, ex) {
    var result = $(win.document).find(ex);
    //console.log("win:" + win.location.href + " resultLength:" + result.length + " ex:" + ex);
    if (result.length)
        return result;
    var ifr = $(win.document).find("iframe");
    if (ifr.length) {
        ifr.each(function (i) {
            result = getWinElement(ifr[i].contentWindow, ex);
            if (result.length) //如果找到了元素，则跳出循环
                return false;
        });
    }
    return result;
}

//显示loading
function showLoading(dlgId) {
    var div = getAllWinElementById(dlgId);
    div.find(".ajaxLoading").show();
}

//获取ajaxLoading
function getAjaxLoading() {
    var topWin = getTopWindows();
    var img = $("<img>", { src: topWin.ImageResources + "Loading.gif" });
    var load = $("#ajax_loading_full");
    if (load.length >= 1) {
        load.remove();
    }
    load = $("<div>", { id: "ajax_loading_full" }).appendTo($(topWin.document.body));
    load.html("");
    var divShadeContainer = $(topWin.document.body);
    var dialogIndex = 1000 || divShadeContainer.data("dialogindex");

    load.append(img).css({
        position: "absolute", //"absolute",
        zIndex: '9999',
        textAlign: "center",
        backgroundColor: "#fff",
        border: "solid 4px #ebebeb",
        //padding: "15px",
        paddingTop: "18px",
        fontSize: "14px",
        top: "50%",
        left: "50%",
        height: "31px",
        width: "150px",
        marginTop: "-38.5px",
        marginLeft: "-79px"
    });

    //取消以下注释，有遮罩效果
    /*var divShade = $("<div></div>", { id: "ajax_loading_full_shade" }).appendTo(divShadeContainer).addClass("div_shade_dialog").css("z-index", dialogIndex - 1);
    var winResize = function () {
    topWin = getTopWindows();
    var windowW = $(topWin.document).width(), 
    windowH = $(topWin.document).height(),
    bodyW = $(topWin.document.body).width(), 
    bodyH = $(topWin.document.body).height();
    //divShade.width(windowW > bodyW ? bodyW : windowW).height(windowH > bodyH ? bodyH : windowH);
    };
    divShade.show();
    $(window).resize(winResize).resize();*/

    return load;
}

function showAjaxLoading() {
    var loading = getAjaxLoading();
    loading.show();
}

function hideAjaxLoading() {
    getAllWinElementById("ajax_loading_full").remove();
    getAllWinElementById("ajax_loading_full_shade").remove();
}

function refreshFrameByContainer(id) {
    var ctl = getAllWinElementById(id);
    if (ctl.length) {
        ctl.find("iframe")[0].contentWindow.location.reload();
    }
}

function refreshFrameByContainerEx(id,url) {
	var ctl = getAllWinElementById(id);
	if (ctl.length) {
		ctl.find("iframe")[0].contentWindow.location.href=url;
	}
}

function reLoadFrameById(dlgId, url) {
    window.parent.loadFrame(getAllWinElementById(dlgId), url, true);
}

var checkIsNullVal = function (val) {
    return val == null || val <= 0 || val == "" || val.length <= 0;
}

//Added by JLD on 2012-11-29
//验证所有带小数点的数字框 输入内容的合法性
function ValidateDecimalNumericTextbox() {
    var isPass = true;
    $(".c_red").remove(); 
    $("input.decimal_numeric").each(function () {
        //$(this).parent().find(".c_red").remove();
        var val = $(this).val();
        var options = eval("(" + $(this).attr("options") + ")");
        if (val != "") {
            var amtArray = val.split('.');
            if (amtArray.length > 1) {
                if (amtArray[1].length > parseInt(options.digits)) {
                    isPass = false;

                    $(this).after("<span class='c_red'>" + $g["Digits"].Format(options.digits) + "</span>");
                    return;
                }
            }
            if (parseFloat(val) > parseFloat(options.maxValue)) {
                isPass = false;

                $(this).after("<span class='c_red'>" + $g["MaxValue"].Format(options.maxValue) + "</span>");
                return;
            }
            if (parseFloat(val) < parseFloat(options.minValue)) {
                isPass = false;

                $(this).after("<span class='c_red'>" + $g["MinValue"].Format(options.minValue) + "</span>");
                return;
            }
            if (val.replace(/\-/g, "") == "" || val.split('-').length > 2 || val.indexOf("-") > 0) {
                isPass = false;

                $(this).after("<span class='c_red'>" + $g["EnterNumber"] + "</span>");
                return;
            }
        }
    });
    return isPass;
}
//解决JS中浮点数的算法bug
//浮点数加法运算
function FloatAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    return (arg1 * m + arg2 * m) / m;
}
//浮点数减法运算
function FloatSub(arg1, arg2) {
    var r1, r2, m, n;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    //动态控制精度长度
    n = (r1 >= r2) ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
}

function onblurChangeAfterSubstract(amt, chpnAmtJId, retJId) {
    var v = $("#" + chpnAmtJId).val();
    v = v == "" ? 0 : v;
    $("#" + retJId).html((parseFloat(amt) - parseFloat(v)).toFixed(2)); //（扣减后）面值余额
}

function compareDate(strDate1, strDate2) {

    if (strDate1.length < 1 || strDate2.length < 1)
        return false;

    var regDateFomart = /^(\d{2})-(\d{2})-(\d{4})$/;  //特定的格式化 形如 dd-mm-yyyy
    strDate1 = strDate1.replace(regDateFomart, "$3/$2/$1");
    strDate2 = strDate2.replace(regDateFomart, "$3/$2/$1");
    var date1 = new Date(strDate1);
    var date2 = new Date(strDate2);
    return date1 > date2;
}

function setDialogScroll(digID) {
    if ($.browser.mozilla) {
        //alert('firefox');
        var _ifrms = window.parent.$(digID + ' iframe');
        if (_ifrms.length > 0)
            _ifrms[0].setAttribute('scrolling', 'no');
    }
}

//大于0的正整数
$.fn.textboxInteger = function () {
    $(this).css("ime-mode", "disabled");
    this.bind("keypress", function (e) {
        var code = (e.keyCode ? e.keyCode : e.which); //兼容火狐 IE 
        if (!$.browser.msie && (e.keyCode == 0x8)) //火狐下不能使用退格键 
        {
            return;
        }
        return code >= 48 && code <= 57;
    });
    this.bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
    this.bind("paste", function () {
        var s = clipboardData.getData('text');
        if (!/\D/.test(s));
        value = s.replace(/^0*/, '');
        return false;
    });
    this.bind("dragenter", function () {
        return false;
    });
    this.bind("keyup", function () {
        if (!/^[1-9][0-9]*$/.test(this.value)) {
            this.value = '';
        }
    });
};

$.fn.numeral_int = function () {
    $(this).css("ime-mode", "disabled");
    this.bind("keypress", function (e) {
        var code = (e.keyCode ? e.keyCode : e.which); //兼容火狐 IE 
        if (!$.browser.msie && (e.keyCode == 0x8)) //火狐下不能使用退格键 
        {
            return;
        }
        return code >= 48 && code <= 57;
    });
    this.bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
    this.bind("paste", function () {
        var s = clipboardData.getData('text');
        if (!/\D/.test(s));
        value = s.replace(/^0*/, '');
        return false;
    });
    this.bind("dragenter", function () {
        return false;
    });
    this.bind("keyup", function () {
        if (/(^0+)/.test(this.value)) {
            this.value = this.value.replace(/^0*/, '0');
        }
    });
};


$.fn.numeral_decimal = function () {
    $(this).css("ime-mode", "disabled");
    this.bind("keypress", function (e) {
        var code = (e.keyCode ? e.keyCode : e.which); //兼容火狐 IE 
        if (!$.browser.msie && (e.keyCode == 0x8)) //火狐下不能使用退格键 
        {
            return;
        }
        if (code == 46) {
            if (this.value.indexOf(".") != -1) {
                return false;
            }
        } else {
            return code >= 46 && code <= 57;
        }
    });
    this.bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
    this.bind("paste", function () {
        var s = clipboardData.getData('text');
        if (!/\D/.test(s));
        value = s.replace(/^0*/, '');
        return false;
    });
    this.bind("dragenter", function () {
        return false;
    });
    this.bind("keyup", function () {
        if (/(^0+)/.test(this.value)) {
            this.value = this.value.replace(/^0*/, '0');
        }
    });
};

    String.prototype.trimStart = function (trimStr) {
		if (!trimStr) { return this; }
		var temp = this;
		while (true) {
			if (temp.substr(0, trimStr.length) != trimStr) {
         		break;
			}
			temp = temp.substr(trimStr.length);
		}
		return temp;
    }; 


    String.prototype.trimEnd = function (trimStr) {
		if (!trimStr) { return this; }
		var temp = this;
		while (true) {
			if (temp.substr(temp.length - trimStr.length, trimStr.length) != trimStr) {
         		break;
			}
			temp = temp.substr(0, temp.length - trimStr.length);
		}
		return temp;
	};

	//为每个submit按钮都添加防重复提交的功能
	$(function () {
		$("form").submit(function () {
			var btn = $(":submit", this);
			btn.attr("disabled", "disabled");
			setTimeout(function () { btn.removeAttr("disabled"); }, 1500);
		});
	});

	function ConverStrToDecimal(str, precision) {
		if (!precision)
			precision = 2;
		if (str == 0 ||(str != null && str != "" && !isNaN(str))) {
			str = new Number(str);
			return parseFloat(Math.round(str * 100) / 100).toFixed(precision);
		}
		return "";
	}
