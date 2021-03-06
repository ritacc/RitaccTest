/*
-- 日历
-- 下载软件 on 2008-9-12 于深圳
*/
function CalendarClass() {
    // 加入日期控件的对象 id, 或者直接传入元素, 二选一
    this.Id = "";
    this.Element = null;
    // 皮肤编号（程序提供 1，2，3 种选择，可自行添加）
    this.Css = 5;
    // 语言（cn - 简体中文，hk - 繁体中文，en - 英文）
    this.Lang = "cn";
    // 选择年/月/日/时/分/日期("Year","Month","Day","Hour","Minute","All")
    this.Type = "All";
    // 格式化日历输出，支持 3 种分隔符 [.,-,/]
    this.Format = "yyyy-MM-dd";
    // 日历起始时间，如果是当前时间，请留空（固定格式[年-月-日]，年份在 1900 - 2099 年之间）
    this.Date = "";
    // 起始日期/终止日期（固定格式[年-月-日]，年份在 1900 - 2099 年之间）
    this.StartDate = "1900-01-01";
    this.EndDate = "2050-12-31";
    this.TranDate = ""; //2013-11-05 by zcs 用于修改today 显示交易日期
    // 是否显示底端的时/分/秒
    this.IsAttach = false;
    // 是否在文档其它地方单击隐藏
    this.IsClick = true;
    // 是否鼠标移出隐藏
    this.IsOver = true;
    // 清空操作后是否隐藏
    this.IsClear = true;
    // 日历显示位置(true 表示显示在触发事件的对象下面)
    this.IsPosition = false;
    // 是否输出不是本月的日期数据和每年的周数
    this.IsOutCurrentMonth = true;
    // 当为只读或禁用时是否仍显示日历
    this.IsShowOften = true;
    // 当点击日历中数据时，触发指定对象的指定事件
    this.FireEventObjectId = "";
    this.FireEventName = "";
    this.FunCall = null;
};

CalendarClass.prototype.Apply = function () {
	// 加入日期控件的元素
	this.__Object__ = this.Id == "" ? this.Element : __C__.G(this.Id);
	// 只读，当对象的属性有 readonly, disabled 时
	var r = this.__Object__.readOnly, d = this.__Object__.disabled;
	if (r || d) {
		if (r) { if (r != "false" && this.IsShowOften) return; }
		if (d) { if (d != "false" && this.IsShowOften) return; }
	};

	this.__Lang__ = __C__.Lang(this.Lang);
	this.__Type__ = this.Type.toLowerCase();
	this.__Format__ = this.__InitFormat__();
	this.__CSS__ = this.__InitCSS__(this.Css);
	this.__Element__ = this.__InitElement__();
	this.__OriginDate__ = this.__InitDate__(this.Date); //保留最初设置的原始值
	this.__DateS__ = this.__InitDate__(this.StartDate);
	this.__DateE__ = this.__InitDate__(this.EndDate);
	
	//2013-11-05 by zcs 用于修改today 显示交易日期
	if (this.TranDate) {
		this.__TranDate__ = this.__InitDate__(this.TranDate);
	}
	//2013-11-05 by zcs 用于修改today 显示交易日期 end

	this.__MaxLength__ = 10;
	//this.__ConvertReg__ = __DateRgExp__.YMDCONVERT;
	switch (this.__Type__) {
		case "all": break;
		case "year": this.__MaxLength__ = 4; this.__ConvertReg__ = __DateRgExp__.Y; break;
		case "month": this.__MaxLength__ = 2; this.__ConvertReg__ = __DateRgExp__.M; break;
		case "day": this.__MaxLength__ = 2; this.__ConvertReg__ = __DateRgExp__.D; break;
		case "hour": this.__MaxLength__ = 2; this.__ConvertReg__ = __DateRgExp__.H; break;
		case "minute": this.__MaxLength__ = 2; this.__ConvertReg__ = __DateRgExp__.m; break;
	};
	this.__Date__ = this.__Object__.value ? this.__StrToDate__(this.__Object__.value) : this.__OriginDate__;
	var evt = __C__.SearchEvent();
	this.__srcElement__ = evt.srcElement || evt.target;

	this.__Year__ = this.__Date__.getFullYear();
	this.__Month__ = this.__Date__.getMonth();
	this.__Day__ = this.__Date__.getDate();
	this.__Hour__ = this.__Date__.getHours();
	this.__Minute__ = this.__Date__.getMinutes();
	this.__WeekDay__ = this.__Date__.getDay();
	this.__SelectYear__ = this.__InitSelectYear__();

	this.__Hidden__();
	switch (this.__Type__) {
		case "all": this.__TableTop__(); this.__CalendarDay__(); break;
		case "year": this.__CalendarYear__(); break;
		case "month": this.__CalendarMonth__(); break;
		case "day": this.__CalendarDay__(); break;
		case "hour": this.__CalendarHour__(); break;
		case "minute": this.__CalendarMinute__(); break;
	};
	this.__TableBottom__();
	this.__ShowAttach__();
	this.__InitIsOver__();
	this.__InitIsClick__();
	this.__Location__(true);
	this.__Event__();
};

CalendarClass.prototype.__Event__ = function () {
	var s = this, o = s.__Object__, f = s.Format, c = s.__ConvertReg__, t = s.__Type__;
	var oldevent = o.onfocus;
	if (o.tagName == "INPUT") {

		var keyCodes = [8, 9, 35, 36, 37, 39, 45, 189, 109];
		for (var i = 48; i <= 57; i++) { keyCodes.push(i); }
		for (var i = 96; i <= 105; i++) { keyCodes.push(i); }
		var _event = function (e) {
			var flag = false;
			var evt = __C__.SearchEvent();
			for (var i in keyCodes) {
				if (keyCodes[i] == (evt.keyCode || evt.charCode)) {
					flag = true;
					break;
				}
			}
			if (!flag) { __C__.StopDefault(); }
		};
		var _update = function (e) {
			var y, m, d, v, val = o.value;
			if (val != "") {
				val = val.replace(/[^\d]+/ig, "");
				if (c.exec(val) != null) {
					switch (c) {
						case __DateRgExp__.YMDCONVERT: y = RegExp.$1; m = RegExp.$3; d = RegExp.$4; break;
						case __DateRgExp__.MDYCONVERT: y = RegExp.$3; m = RegExp.$1; d = RegExp.$2; break;
						case __DateRgExp__.DMYCONVERT: y = RegExp.$3; m = RegExp.$2; d = RegExp.$1; break;
						default: v = RegExp.$1; break;
					}

					//					var selectedDate = s.__InitDate__(s.__FormatDateOut__(y, m - 1, d));
					//					var largerDate = selectedDate; //s.__DateS__ > selectedDate ? s.__DateS__ : selectedDate;  
					//					var largerDateStr = s.__FormatDateOut__(largerDate.getYear(), largerDate.getMonth(), largerDate.getDate());
					//					var val = t == "all" ? largerDateStr : v;
					//o.value = val;
					o.value = s.__FormatDateOut__(y, m - 1, d);
					if ($(o)) {
						$(o).change();
					}
					// 目前只找到 ie 下的方法, firefox 暂不支持
					if (__C__.IsIE()) {
						var to = document.activeElement;
						if (to != o && (!__C__.Contains(s.__Element__.DivBody, to))) {
							s.__Hidden__();
						}
					}
				} else {
					//alert(s.__Lang__.DateFormatError);
					//o.value = "2013-04-15";
					o.value = "";
					_focus();
				}
			}
		};
		var _focus = function () {
			if (oldevent) { oldevent.call(o); }
			o.value = o.value.replace(/[^\d]+/ig, "");
			//o.value = "20130909";
			o.select();
		};
		// o.style.width = (s.__MaxLength__ * 10) + "px";
		o.maxLength = s.__MaxLength__;
		o.onkeydown = o.onkeyup = _event;
		o.onblur = _update;
		o.onfocus = _focus;
	}
	return;
};

CalendarClass.prototype.__InitDate__ = function (s) {
	var reg = /^(19|20)[0-9]{2}-(0?[1-9]|1[012])-(0?[1-9]|[12][0-9]|3[01])$/;
	var isDMY = false;
	if (!reg.test(s)) {
		reg = /^(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[012])-(19|20)[0-9]{2}$/;
		isDMY = true;
	}
	if (s == "") return new Date();
	if (reg.test(s)) {
		var vs = s.split("-");
		if (isDMY) {
			return new Date(parseInt(vs[2]), vs[1] - 1, vs[0]);
		} else {
			return new Date(parseInt(vs[0]), vs[1] - 1, vs[2]);
		}
	} else {
		alert(this.__Lang__.DateFormatError);
		return;
	}
};

CalendarClass.prototype.__InitCSS__ = function (c) {
    var css;

    if (this.__CSS__) {
        css = this.__CSS__;
    } else {
        css = {
            // 最外层div
            DivMaster: "matchDivMaster_{0}".Format(c),
            // table
            TableMiddle: "matchTableData_{0}".Format(c),
            // 日期数据
            TdData: "matchDataTd_{0}".Format(c),
            // 日期数据(非本月)
            TdDataDisabled: "matchDataTdDisabled_{0}".Format(c),
            // 日期数据(选择天)
            TdDataSelect: "matchDataTdSelect_{0}".Format(c),
            // 日期数据(当前天)
            TdDataCurrent: "matchDataTdCurrent_{0}".Format(c),
            // 日期数据(双休日)
            TdDataRest: "matchDataTdRest_{0}".Format(c),
            // 日期数据(鼠标划过)
            TdDataOver: "matchDataTdOver_{0}".Format(c),
            // 标题(星期)
            TrTitleWeek: "matchTitleWeek_{0}".Format(c),
            // 标题(链接,年月翻动行)
            TdTitle: "matchTitle_{0}".Format(c),
            TdTitleLink: "matchTitleLink_{0}".Format(c),
            TdTitleLinkSymbol: "matchTitleLinkSymbol_{0}".Format(c),
            TdTitleLinkOver: "matchTitleLinkOver_{0}".Format(c),
            TdTitleLinkOverSymbol: "matchTitleLinkOverSymbol_{0}".Format(c),
            TdTitleDisabled: "matchTitleDisabled_{0}".Format(c),
            // 底部
            TdBottom: "matchBottom_{0}".Format(c),
            TdBottomLink: "matchBottomLink_{0}".Format(c),
            TdBottomLinkOver: "matchBottomLinkOver_{0}".Format(c),
            // 年份月份选择
            TdYMLink: "matchYMLink_{0}".Format(c),
            TdYMOver: "matchYMOver_{0}".Format(c),
            // 禁用
            TdDisabled: "matchDiabled_{0}".Format(c),
            // 一年中的第几周
            TdWeek: "matchTdWeek_{0}".Format(c)
        };
    }
    return css;
};

// 加入元素存放，避免重复添加对象
document.Calendar = null;
CalendarClass.prototype.__InitElement__ = function () {
	var element;

	if (document.Calendar) {
		element = document.Calendar;
	} else {
		var divBody, divMaster;
		var tableMiddle, tableTop, tableBottom, tableAttach;
		divBody = __C__.C("div");
		divBody.className = "matchDivBody";
		// 使用 iframe 可以让 div 覆盖住 select 对象
		divBody.innerHTML = '<iframe id="frameCalendarEstop" frameborder="0" style="position:absolute;z-index=-1;" src="about:blank"></iframe>';
		divMaster = __C__.C("div");
		tableMiddle = __C__.C("table");
		tableTop = __C__.C("table");
		tableBottom = __C__.C("table");
		tableAttach = __C__.C("table");
		__C__.B().appendChild(divBody);
		divBody.appendChild(divMaster);
		divMaster.appendChild(tableTop);
		divMaster.appendChild(tableMiddle);
		divMaster.appendChild(tableBottom);
		divMaster.appendChild(tableAttach);

		element =
        {
        	DivBody: divBody,
        	DivMaster: divMaster,
        	TableTop: tableTop,
        	TableMiddle: tableMiddle,
        	TableBottom: tableBottom,
        	TableAttach: tableAttach
        };
		document.Calendar = element;
	}

	element.DivMaster.className = this.__CSS__.DivMaster;
	element.TableTop.className = this.__CSS__.TdTitle;
	element.TableMiddle.className = this.__CSS__.TableMiddle;
	element.TableBottom.className = this.__CSS__.TdBottom;
	element.TableAttach.className = this.__CSS__.TdBottom;

	return element;
};

CalendarClass.prototype.__InitFormat__ = function () {
    var f = this.Format;
    var flag = false;
    var r = __DateRgExp__;
    if (r.YMDTYPE.test(f)) {
        flag = true;
        this.__ConvertReg__ = r.YMDCONVERT;
    } else if (r.DMYTYPE.test(f)) {
        flag = true;
        this.__ConvertReg__ = r.DMYCONVERT;
    } else if (r.MDYTYPE.test(f)) {
        flag = true;
        this.__ConvertReg__ = r.MDYCONVERT;
    }
    return flag ? f : "yyyy-MM-dd";
};

CalendarClass.prototype.__InitSelectYear__ = function () {
    var interZone = 15;
    var b = this, r = b.__OriginDate__.getFullYear() - 7;
    if (b.__Object__.value) {
        var tmp = Math.abs(r - b.__Year__);
        // 余数
        var mod = tmp % 15;
        // 除数
        var divisor = Math.floor(tmp / interZone);
        if (r > b.__Year__ && r >= b.__DateS__.getFullYear()) {
            // 往后翻时当余数大于 0 就要加 1 页
            mod > 0 ? divisor++ : divisor;
            r -= interZone * divisor;
        } else if (r < b.__Year__ && (r + interZone) <= b.__DateE__.getFullYear()) {
            // 往前翻时不需要考虑余数，只需要判断有没有达到指定的年份
            r += interZone * divisor;
        }
    }
    return r;
};

// top
CalendarClass.prototype.__TableTop__ = function () {
    var tr, td;
    var b = this, table = b.__Element__.TableTop, l = b.__Lang__, cs = b.__CSS__;
    var bS = new Date(b.__Year__, b.__Month__, b.__DateS__.getDate()) > b.__DateS__;
    var bE = new Date(b.__Year__, b.__Month__, b.__DateE__.getDate()) < b.__DateE__;

    __C__.Display(table, "");
    __C__.Del(table);

    tr = table.insertRow(-1);
    // 前翻一年
    td = tr.insertCell(-1);
    td.innerHTML = __C__.IsIE() ? "7" : "<<";
    td.title = l.PrevYear;
    TdClassName(td, false);
    if (bS) {
        td.onclick = function () {
            b.__Year__--;
            b.__TableTop__();
            b.__CalendarDay__();
            __C__.StopDefault();
        };
    } else {
        DisabledTd(td, true);
    }
    // 前翻一月
    td = tr.insertCell(-1);
    td.innerHTML = __C__.IsIE() ? "3" : "<";
    td.title = l.PrevMonth;
    TdClassName(td, false);
    if (bS) {
        td.onclick = function () {
            b.__Month__--;
            if (b.__Month__ < 0) {
                b.__Month__ = 11;
                b.__Year__--;
            }
            b.__TableTop__();
            b.__CalendarDay__();
            __C__.StopDefault();
        };
    } else {
        DisabledTd(td, true);
    }
    // 单击选择年份
    td = tr.insertCell(-1);
    td.innerHTML = b.__Year__;
    td.title = l.SelectYear;
    TdClassName(td, true);
    td.onclick = function () {
        b.__CalendarYear__();
        b.__Location__();
        __C__.StopDefault();
    };
    // 单击选择月份
    td = tr.insertCell(-1);
    td.innerHTML = b.__Month__ + 1;
    td.title = l.SelectMonth;
    TdClassName(td, true);
    td.onclick = function () {
        b.__CalendarMonth__();
        b.__Location__();
        __C__.StopDefault();
    };
    // 后翻一月
    td = tr.insertCell(-1);
    td.innerHTML = __C__.IsIE() ? "4" : ">";
    td.title = l.NextMonth;
    TdClassName(td, false);
    if (bE) {
        td.onclick = function () {
            b.__Month__++;
            if (b.__Month__ > 11) {
                b.__Month__ = 0;
                b.__Year__++;
            }
            b.__TableTop__();
            b.__CalendarDay__();
            __C__.StopDefault();
        };
    } else {
        DisabledTd(td, false);
    }
    // 后翻一年
    td = tr.insertCell(-1);
    td.innerHTML = __C__.IsIE() ? "8" : ">>";
    td.title = l.NextYear;
    TdClassName(td, false);
    if (bE) {
        td.onclick = function () {
            b.__Year__++;
            b.__TableTop__();
            b.__CalendarDay__();
            __C__.StopDefault();
        };
    } else {
        DisabledTd(td, false);
    }
    // 方法私有
    function DisabledTd(o, flag) {
        // true 表示最小日期, false 表示最大日期
        var date = flag ? b.__DateS__ : b.__DateE__;
        b.__Year__ = date.getFullYear();
        b.__Month__ = date.getMonth();
        b.__Day__ = date.getDate();
        __C__.ClearEvent(o);
        o.className = cs.TdTitleDisabled;
    };
    function TdClassName(td, flag) {
        var curClassName;
        td.className = flag ? cs.TdTitleLink : cs.TdTitleLinkSymbol;
        td.onmouseover = function () {
            curClassName = this.className;
            this.className = flag ? cs.TdTitleLinkOver : cs.TdTitleLinkOverSymbol;
        };
        td.onmouseout = function () {
            this.className = curClassName;
        };
    };
};

// bottom
CalendarClass.prototype.__TableBottom__ = function () {
	var td, tr, b = this, n = __C__.N(), c = b.__CSS__;
	var table = this.__Element__.TableBottom;
	var y = n.Y, M = n.M, d = n.D, h = n.H, m = n.m, s = n.S;

	__C__.Display(table, "");
	__C__.Del(table);

	table.cellPadding = table.cellSpacing = 0;
	tr = table.insertRow(-1);
	// 清空
	td = tr.insertCell(-1);
	td.innerHTML = b.__Lang__.Clear;
	td.onclick = function () {
		b.__Set__("");
		if (b.IsClear) b.__Hidden__();
		__C__.StopDefault();
	};
	TdEvent(td);
	// 当前的选择
	td = tr.insertCell(-1);
	switch (b.__Type__) {
		case "all": td.innerHTML = b.__Lang__.Today; break;
		case "year": td.innerHTML = b.__Lang__.Year; break;
		case "month": td.innerHTML = b.__Lang__.Month; break;
		case "day": td.innerHTML = b.__Lang__.Day; break;
		case "hour": td.innerHTML = b.__Lang__.Hour; break;
		case "minute": td.innerHTML = b.__Lang__.Minute; break;
	}
	TdEvent(td);
	td.onclick = function () {
		switch (b.__Type__) {
			case "all":	
				//2013-11-05 by zcs 用于修改today 显示交易日期
				if (b.__TranDate__) {
					y = b.__TranDate__.getFullYear();
					M = b.__TranDate__.getMonth();
					d = b.__TranDate__.getDate();
				}
				//2013-11-05 by zcs 用于修改today 显示交易日期	end
				b.__Set__(b.__FormatDateOut__(y, M, d));
				break;
			case "year": b.__Set__(y); break;
			case "month": b.__Set__(M + 1); break;
			case "day": b.__Set__(d); break;
			case "hour": b.__Set__(h); break;
			case "minute": b.__Set__(m); break;
		}
		b.__Hidden__();
		__C__.StopDefault();
	};
	// 关闭
	td = tr.insertCell(-1);
	td.innerHTML = b.__Lang__.Close;
	TdEvent(td);
	td.onclick = function () {
		b.__Hidden__();
		__C__.StopDefault();
	};

	function TdEvent(o) {
		o.className = c.TdBottomLink;
		o.onmouseover = function () {
			this.className = c.TdBottomLinkOver;
		};
		o.onmouseout = function () {
			this.className = c.TdBottomLink;
		};
	};
};

// 年
CalendarClass.prototype.__CalendarYear__ = function () {
    var tr, td;
    var b = this, c = b.__CSS__, yearNow = __C__.N().Y;
    var bSY = b.__DateS__.getFullYear(), bEY = b.__DateE__.getFullYear();
    var year = b.__SelectYear__, table = b.__Element__.TableMiddle;

    __C__.Display(table, "");
    __C__.Del(table);

    for (var i = 0; i < 5; i++) {
        tr = table.insertRow(-1);
        for (var ii = 0; ii < 3; ii++) {
            // 日期范围
            var flag = year > bEY || year < bSY;
            td = tr.insertCell(-1);
            this.__TdYM__(td, !flag);
            td.innerHTML = year;
            if (year == b.__Year__ && b.__Object__.value != "") td.className = c.TdDataSelect;
            if (year == yearNow) td.className = c.TdDataCurrent;
            year += 1;
        }
    }

    tr = table.insertRow(-1);
    td = tr.insertCell(-1);
    td.style.fontFamily = "Webdings";
    td.innerHTML = __C__.IsIE() ? "7" : "<";
    this.__TdYM__(td, true);
    if (b.__SelectYear__ > bSY) {
        td.onclick = function () {
            b.__SelectYear__ -= 15;
            b.__CalendarYear__();
            __C__.StopDefault();
        };
    }
    else {
        td.className = c.TdDisabled;
        __C__.ClearEvent(td);
    }

    td = tr.insertCell(-1);
    if (b.__Type__ == "all") {
        td.innerHTML = b.__Lang__.Year;
        this.__TdYM__(td, true);
        td.onclick = function () {
            b.__SelectYear__ = yearNow - 7;
            b.__CalendarYear__();
            __C__.StopDefault();
        };
    }
    else {
        td.className = c.TdYMLink;
    }

    td = tr.insertCell(-1);
    td.style.fontFamily = "Webdings";
    td.innerHTML = __C__.IsIE() ? "8" : ">";
    this.__TdYM__(td, true);
    if (b.__SelectYear__ < bEY - 15) {
        td.onclick = function () {
            b.__SelectYear__ += 15;
            b.__CalendarYear__();
            __C__.StopDefault();
        };
    }
    else {
        td.className = c.TdDisabled;
        __C__.ClearEvent(td);
    }
};

// 月
CalendarClass.prototype.__CalendarMonth__ = function () {
    var tr, td;
    var b = this, month = 1, c = b.__CSS__;
    var table = b.__Element__.TableMiddle;

    __C__.Display(table, "");
    __C__.Del(table);

    for (var i = 0; i < 6; i++) {
        tr = table.insertRow(-1);
        for (var ii = 0; ii < 2; ii++) {
            // 日期范围
            var curDate = new Date(b.__Year__, month - 1, 1);
            var flag = curDate < b.__DateE__ && curDate >= b.__DateS__;
            td = tr.insertCell(-1);
            td.id = month;
            td.innerHTML = month < 10 ? "0" + month : month;
            b.__TdYM__(td, flag);
            if (flag) {
                td.onclick = function () {
                    if (b.__Type__ == "all") {
                        b.__Month__ = parseInt(this.id) - 1;
                        b.__TableTop__();
                        b.__CalendarDay__();
                    }
                    else {
                        b.__Set__(this.id);
                        b.__Hidden__();
                    }
                    __C__.StopDefault();
                };
            }
            if (month == b.__Month__ + 1 && b.__Object__.value != "") td.className = c.TdDataSelect;
            if (month == new Date().getMonth() + 1) td.className = c.TdDataCurrent;
            month += 1;
        }
    }
};

// 日
CalendarClass.prototype.__CalendarDay__ = function () {
    var b = this, c = __C__, cs = this.__CSS__, e = this.__Element__;
    // 当前选择年/月/日/星期
    var cY = b.__Year__, cM = b.__Month__, cD = b.__Day__, cW = b.__WeekDay__;
    // 当前年/月/日/
    var nY = b.__Date__.getFullYear(), nM = b.__Date__.getMonth(), nD = b.__Date__.getDate();
    // 月份对应天数组，闰年2月为28天
    var febDays = (cY % 4 == 0) && (cY % 100 != 0) || (cY % 400 == 0) ? 29 : 28;
    var dayNums = [31, febDays, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    // 行，列，当前月
    var dMs = dayNums[cM], fWD = new Date(cY, cM, 1).getDay();
    var rowNums = (fWD > 4 && dMs == 31) || fWD > 5 ? 6 : 5, colNums = 7;
    // 闰年 2 月的时候，当第 1 天是星期天时，只输出 4 行
    rowNums = (fWD == 0 && dMs <= 28) ? 4 : rowNums;

    var tr, td, tdWeek, divideCols;
    var i, ii, j = 0, jj = 1;
    var cn = cs.TdData;
    var table = e.TableMiddle;

    c.Display(table, "");
    c.Del(table);
    // 头行创建
    tr = table.insertRow(-1);
    tr.className = cs.TrTitleWeek;
    for (i = 0; i < colNums; i++) {
        divideCols = (100 / (b.IsOutCurrentMonth && i == 0) ? 8 : 7) + "%";
        // 当显示非本月日期的时候，显示当前为全年第几周
        if (b.IsOutCurrentMonth && i == 0) {
            td = tr.insertCell(-1);
            td.style.width = divideCols;
        }
        td = tr.insertCell(-1);
        td.style.width = divideCols;
        td.innerHTML = b.__Lang__.WeekName[i];
    }
    // 数据填充
    for (i = 0; i < rowNums; i++) {
        tr = table.insertRow(-1);
        for (ii = 0; ii < colNums; ii++) {
            // 当显示非本月日期的时候，显示当前为全年第几周
            if (b.IsOutCurrentMonth && ii == 0) {
                tdWeek = tr.insertCell(-1);
                tdWeek.className = cs.TdWeek;
            }

            var curDate;
            var tmp = j + ii;
            td = tr.insertCell(-1);
            td.className = cn;

            // 本月日期填入
            if (tmp >= fWD && dMs + fWD > tmp) {
                var n = c.N();
                if (jj == n.D && cM == n.M && cY == n.Y) {
                    // 当前天
                    td.className = cs.TdDataCurrent;
                } else if (jj == cD && cM == nM && cY == nY) {
                    // 选择天
                    td.className = b.__Object__.value ? cs.TdDataSelect : "";
                } else if (ii == 0 || ii == colNums - 1) {
                    // 星期六星期天
                    td.className = cs.TdDataRest;
                } else {
                    td.className = cs.TdData;
                }

                td.innerHTML = jj++;
                td.title = b.__FormatDateOut__(cY, cM, td.innerHTML);
                curDate = new Date(cY, cM, td.innerHTML);
            } else {
                if (this.IsOutCurrentMonth) {
                    var yV, mV, dV, preY = nxtY = cY;
                    var preM = cM - 1, nxtM = cM + 1;
                    if (preM < 0) {
                        preM = 11;
                        preY = cY - 1;
                    }
                    if (nxtM > 11) {
                        nxtM = 0;
                        nxtY = cY + 1;
                    }
                    if (tmp > fWD) {
                        // 日期累加 - 本月日期 - 本月1号星期 + 1（jj为1，多减了1，所以加1） + 1（tmp是从0开始的）
                        dV = tmp - jj - fWD + 1 + 1;
                        mV = nxtM;
                        yV = nxtY;
                    } else {
                        // 前月日期 - 本月1号星期 + 1（本月1号星期是从0开始） + ii
                        dV = dayNums[preM] - fWD + 1 + ii;
                        mV = preM;
                        yV = preY;
                    }
                    td.className = cs.TdDataDisabled;
                    td.innerHTML = dV;
                    td.title = b.__FormatDateOut__(yV, mV, dV);
                    curDate = new Date(yV, mV, dV);
                } else { continue; }
            }
            if (curDate > b.__DateE__ || curDate < b.__DateS__) {
                td.className = cs.TdDisabled;
            } else {
                td.onclick = function () {
                    if (b.FunCall != null)
                        b.FunCall(yV, mV, dV);
                    b.__Set__(b.__Type__ == "all" ? (this.title == "" ? this.dypop : this.title) : this.innerHTML);
                    b.__Hidden__();
                    __C__.StopDefault();
                };
                td.onmouseover = function () {
                    cn = this.className;
                    this.className = cs.TdDataOver;
                };
                td.onmouseout = function () {
                    this.className = cn;
                };
            }
            // 一年中第几周加入
            if (tdWeek && this.IsOutCurrentMonth && ii == colNums - 1) {
                var days = (curDate - new Date(curDate.getFullYear(), 0, 1)) / 86400000;
                var weekNums = Math.floor(days / 7);
                var weekNo = days % 7 > 0 ? weekNums + 1 : weekNums;
                // 当一年的一月一日是星期六时，所有周加 1
                var flag = new Date(curDate.getFullYear(), 0, 1).getDay() == 6;
                tdWeek.innerHTML = flag ? weekNo + 1 : weekNo;
            }
        }
        j = (i + 1) * ii;
    }
};

// 时
CalendarClass.prototype.__CalendarHour__ = function () {
    var tr, td, hour = 0, b = this, c = b.__CSS__;
    var table = b.__Element__.TableMiddle;

    __C__.Display(table, "");
    __C__.Del(table);

    for (var i = 0; i < 6; i++) {
        tr = table.insertRow(-1);
        for (var ii = 0; ii < 4; ii++) {
            td = tr.insertCell(-1);
            td.innerHTML = hour < 10 ? "0" + hour : hour;
            b.__TdYM__(td, true);
            td.onclick = function () {
                b.__Set__(parseInt(this.innerHTML));
                b.__Hidden__();
                __C__.StopDefault();
            };
            if (hour == b.__Hour__ && b.__Object__.value != "") td.className = c.TdDataSelect;
            if (hour == new Date().getHours()) td.className = c.TdDataCurrent;
            hour += 1;
        }
    }
};

// 分
CalendarClass.prototype.__CalendarMinute__ = function () {
    var tr, td, minute = 0, b = this, c = b.__CSS__;
    var table = this.__Element__.TableMiddle;

    __C__.Display(table, "");
    __C__.Del(table);

    for (var i = 0; i < 9; i++) {
        tr = table.insertRow(-1);
        for (var ii = 0; ii < 7; ii++) {
            td = tr.insertCell(-1);
            if (minute <= 59) {
                td.innerHTML = minute < 10 ? "0" + minute : minute;
                b.__TdYM__(td, true);
                td.onclick = function () {
                    b.__Set__(parseInt(this.innerHTML));
                    b.__Hidden__();
                    __C__.StopDefault();
                };
                if (minute == b.__Minute__ && b.__Object__.value != "") td.className = c.TdDataSelect;
                if (minute == new Date().getMinutes()) td.className = c.TdDataCurrent;
            }
            else {
                b.__TdYM__(td, false);
            }
            minute += 1;
        }
    }
};
/* --------------------------------------------------------------------------------------------------------------------- */
CalendarClass.prototype.__TdYM__ = function (td, flag) {
    var cn;
    var b = this, c = b.__CSS__;

    td.className = flag ? c.TdYMLink : c.TdDisabled;
    if (flag) {
        td.onclick = function () {
            var value = parseInt(this.innerHTML);
            var flag = value <= b.__Year__ ? true : false;
            b.__Year__ = value;
            if (b.Type == "All") {
                b.__TableTop__();
                b.__CalendarDay__();
            }
            else {
                b.__Set__(value);
                b.__Hidden__();
            }
            __C__.StopDefault();
        };
    }
    td.onmouseover = function () {
        cn = this.className;
        this.className = flag ? c.TdYMOver : c.TdDisabled;
    };
    td.onmouseout = function () {
        this.className = cn;
    };
};

CalendarClass.prototype.__ShowAttach__ = function () {
    var b = this;
    if (this.IsAttach) {
        var tr, td, table = b.__Element__.TableAttach;
        __C__.Display(table, "");
        __C__.Del(table);

        table.cellPadding = table.cellSpacing = 0;
        tr = table.insertRow(-1);
        td = tr.insertCell(-1);
        td.className = b.__CSS__.TdData;
        td = tr.insertCell(-1);
        td.className = b.__CSS__.TdData;
        ShowAttachEvent();
        setInterval(function () { ShowAttachEvent(); }, 1000);
    }

    function ShowAttachEvent() {
        var tds = table.getElementsByTagName("td");
        var n = __C__.N();
        var y = n.Y, M = n.M, d = n.D, h = n.H, m = n.m, s = n.S;

        h = h < 10 ? "0" + h : h;
        m = m < 10 ? "0" + m : m;
        s = s < 10 ? "0" + s : s;

        tds[0].innerHTML = b.__FormatDateOut__(y, M, d);
        tds[1].innerHTML = h + ":" + m + ":" + s;
    };
};

CalendarClass.prototype.__InitIsOver__ = function () {
    var b = this;

    if (b.IsOver) {
        b.__Element__.DivBody.onmouseout = function (event) {
            try {
                var evt = __C__.SearchEvent();
                var src = evt.srcElement || evt.target;
                var to = evt.toElement || evt.relatedTarget;
                if (b.__Object__ != src && !__C__.Contains(this, to)) {
                    //!b.__Element__.DivBody.contains(evt.toElement)) {
                    b.__Hidden__();
                }
            } catch (e) { };
        }
    }
};

CalendarClass.prototype.__InitIsClick__ = function () {
    var b = this;

    if (b.IsClick) {
        document.onclick = function (event) {
            try {
                if (b.__Element__.DivBody) {
                    var evt = __C__.SearchEvent();
                    var src = evt.srcElement || evt.target;
                    if (b.__srcElement__ != src && !__C__.Contains(b.__Element__.DivBody, src)) {
                        //!b.__Element__.DivBody.contains(evt.srcElement)) {
                        b.__Hidden__();
                    }
                }
            }
            catch (e) { };
        }
    }
    else {
        __C__.ClearEvent(document);
    }
};

CalendarClass.prototype.__Location__ = function (flag) {
    var o = this.IsPosition ? this.__srcElement__ : this.__Object__;
    var divBody = this.__Element__.DivBody;
    var div = this.__Element__.DivMaster;
    // 要显示层，不然 OffsetWidth 会恒为 0
    divBody.style.display = "block";

    var xLeft = 0, yTop = 0;
    var fL = __C__.L(o), dL = __C__.L(div);
    // 表单对象绝对位置参数
    var formT = fL.AbsoluteTop, formL = fL.AbsoluteLeft;
    var formH = fL.OffsetHeight, formW = fL.OffsetWidth;
    // 日历层绝对位置参数
    var divH = dL.OffsetHeight, divW = dL.OffsetWidth;
    // 屏幕相关参数
    var de = __C__.E().clientWidth > 0 ? __C__.E() : __C__.B();
    var bodyW = de.clientWidth, bodyH = de.clientHeight;
    var scrollL = de.scrollLeft, scrollT = de.scrollTop;
    var a = o.parentNode;
    while (a != de) {
        scrollL += a.scrollLeft;
        scrollT += a.scrollTop;
        a = a.parentNode;
    }
    // 改变内嵌 iframe 的宽高
    //var frame = document.frames("frameCalendarEstop").frameElement;
    var frame = document.getElementById("frameCalendarEstop");
    frame.width = divW + "px";
    frame.height = divH + "px";

    if (flag) {
        xLeft = formL;
        yTop = formT + formH - scrollT;
        // 超过屏幕宽
        if (xLeft + divW - scrollL > bodyW) { xLeft = bodyW - divW + scrollL; }
        // 超过屏幕高
        if (yTop + divH > bodyH) { yTop = formT - divH - scrollT; }

        divBody.style.left = xLeft + "px";
        divBody.style.top = yTop + "px";
    }
};

CalendarClass.prototype.__Hidden__ = function () {
    var c = __C__, e = this.__Element__, n = "none";
    c.Display(e.DivBody, n);
    c.Display(e.TableTop, n);
    c.Display(e.TableMiddle, n);
    c.Display(e.TableBottom, n);
    c.Display(e.TableAttach, n);
};

CalendarClass.prototype.__Set__ = function (v) {
	var o = this.__Object__;
	//o.onfocus();	
	o.value = v;
	if ($(o)) {
		$(o).change();
	}
	// 引发事件
	try {
		if (this.FireEventObjectId && this.FireEventName) {
			var fireObject = __C__.G(this.FireEventObjectId);
			if (fireObject) { __C__.FireEvent(fireObject, this.FireEventName) }
		}
	}
	catch (e) { alert("{0}\n\n{1}".Format(this.__Lang__.FireEventError, e)); }
};
/* --------------------------------------------------------------------------------------------------------------------- */
// 字符串转达化成日期
CalendarClass.prototype.__StrToDate__ = function (str) {
	var n = __C__.N();
	
    var c = this.__ConvertReg__;
    var year = n.Y, month = n.M, day = n.D, hour = n.H, minute = n.m;

    if (c.exec(str) != null) {
        switch (c) {
            case __DateRgExp__.YMDCONVERT: year = RegExp.$1; month = RegExp.$3; day = RegExp.$4; break;
            case __DateRgExp__.MDYCONVERT: year = RegExp.$3; month = RegExp.$1; day = RegExp.$2; break;
            case __DateRgExp__.DMYCONVERT: year = RegExp.$3; month = RegExp.$2; day = RegExp.$1; break;
            case __DateRgExp__.Y: year = RegExp.$1; break;
            case __DateRgExp__.M: month = RegExp.$1; break;
            case __DateRgExp__.D: month += 1; day = RegExp.$1; break;
            case __DateRgExp__.H: hour = RegExp.$1; break;
            case __DateRgExp__.m: minute = RegExp.$1; break;
        }
    }

    return new Date(year, str ? month - 1 : month, day, hour, minute);
};
// 日期输出格式化，传入年月日数据:2008,4,1
CalendarClass.prototype.__FormatDateOut__ = function (y, m, d) {
    var date;
    var arg = arguments;
    var result = this.__Format__;

    if (arg.length == 3) {
        date = new Date(y, m, d);
    }
    if (isNaN(date)) date = __C__.N();

    var o =
    {
        "M+": date.getMonth() + 1,
        "d+": date.getDate(),
        "h+": date.getHours(),
        "m+": date.getMinutes()
    };

    if (/(y+)/.test(result)) {
        result = result.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(result)) {
            result = result.replace(RegExp.$1, RegExp.$1.length == 1
                ? o[k]
                : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return result;
};
/* --------------------------------------------------------------------------------------------------------------------- */
var __C__ = {
    // 返回 document
    D: function () {
        return document;
    },
    // 返回 document.body
    B: function () {
        return document.body;
    },
    // 返回 document.documentElement;
    E: function () {
        return document.documentElement;
    },
    // 获得元素 by id
    G: function (id) {
        return document.getElementById(id);
    },
    // 创建元素
    C: function (tagName) {
        return document.createElement(tagName);
    },
    // 当前日期时间的值
    N: function () {
        var now = new Date();
        return { Y: now.getFullYear(), M: now.getMonth(), D: now.getDate(), H: now.getHours(), m: now.getMinutes(), S: now.getSeconds() };
    },
    // 删除 table 的所有行
    Del: function (o) {
        var rows = o.rows.length;
        while (rows-- > 0) o.deleteRow(-1);
    },
    // 显示
    Display: function (o, s) {
        o.style.display = s;
    },
    // 获得对象的绝对位置
    L: function (o) {
        if (arguments.length != 1 || o == null) return null;
        var t = o.offsetTop, l = o.offsetLeft;
        var w = o.offsetWidth, h = o.offsetHeight;
        while (o = o.offsetParent) {
            t += o.offsetTop;
            l += o.offsetLeft;
        }
        return { AbsoluteTop: t, AbsoluteLeft: l, OffsetWidth: w, OffsetHeight: h };
    },
    // 语言
    Lang: function (type) {
        switch (type.toLowerCase()) {
            case "cn": return __CalendarCN__; break;
            case "en": return __CalendarEN__; break;
            case "hk": return __CalendarHK__; break;
        }
    },
    // 清除事件
    ClearEvent: function (o) {
        o.onclick = {};
        o.onmouseover = {};
        o.onmouseout = {};
    },
    IsIE: function () {
        return document.all;
    },
    SearchEvent: function () {
        if (document.all) { return window.event; }

        var func = this.SearchEvent.caller;
        while (func != null) {
            var arg0 = func.arguments[0];
            if (arg0 instanceof Event) { return arg0; }
            func = func.caller;
        }
        return null;
    },
    Contains: function (a, b) {
        return a.contains ? a != b && a.contains(b) : !!(a.compareDocumentPosition(b) & 16);
    },
    StopDefault: function () { // 取消事件冒泡
        var event = this.SearchEvent();
        if (event.stopPropagation) event.stopPropagation();
        else event.cancelBubble = true;
        if (event.preventDefault) event.preventDefault();
        else event.returnValue = false;
    },
    FireEvent: function (element, eventName) { // 引发事件
        if (this.IsIE()) {
            element.fireEvent(eventName);
        } else {
            var evt = document.createEvent('HTMLEvents');
            evt.initEvent(eventName.replace(/^(on)|(ON)/, ""), false, false);
            element.dispatchEvent(evt);
        }
    }
};

String.prototype.Format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g, function (m, i) { return args[i]; });
};
/* --------------------------------------------------------------------------------------------------------------------- */
var __CalendarHK__ = {
    WeekName: new Array("日", "一", "二", "三", "四", "五", "六"),
    Today: "今 天",
    Year: "本 年",
    Month: "本 月",
    Day: "本 日",
    Hour: "本 時",
    Minute: "本 分",
    Clear: "清 空",
    Close: "關 閉",
    PrevYear: "前翻一年",
    NrevMonth: "前翻一月",
    NextYear: "後翻一年",
    NextMonth: "後翻一月",
    SelectYear: "單擊選擇年份",
    SelectMonth: "單擊選擇月份",
    DateFormatError: "設置的日期格式有誤",
    FireEventError: "引發事件的對象找不到或者是引發事件名稱不對"
};
var __CalendarCN__ = {
    WeekName: new Array("日", "一", "二", "三", "四", "五", "六"),
    Today: "今 天",
    Year: "本 年",
    Month: "本 月",
    Day: "本 日",
    Hour: "本 时",
    Minute: "本 分",
    Clear: "清 空",
    Close: "关 闭",
    PrevYear: "前翻一年",
    PrevMonth: "前翻一月",
    NextYear: "后翻一年",
    NextMonth: "后翻一月",
    SelectYear: "单击选择年份",
    SelectMonth: "单击选择月份",
    DateFormatError: "设置的日期格式有误",
    FireEventError: "引发事件的对象找不到或者是引发事件名称不对"
};
var __CalendarEN__ = {
    WeekName: new Array("Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat"),
    Today: "Today",
    Year: "Year",
    Month: "Month",
    Day: "Day",
    Hour: "Hour",
    Minute: "Minute",
    Clear: "Clear",
    Close: "Close",
    PrevYear: "Previous year",
    PrevMonth: "Previous month",
    NextYear: "Next year",
    NextMonth: "Next month",
    SelectYear: "Click select year",
    SelectMonth: "Click select month",
    DateFormatError: "Date format error",
    FireEventError: "Can't find the object or the fire event name wrong"
};
var __DateRgExp__ = {
    DMY: /^(0?[1-9]|[12][0-9]|3[01])[-\.\/](0?[1-9]|1[012])[-\.\/]((19|20)[0-9]{2})$/,
    MDY: /^(0?[1-9]|1[012])[-\.\/](0?[1-9]|[12][0-9]|3[01])[-\.\/]((19|20)[0-9]{2})$/,
    YMD: /^((19|20)[0-9]{2})[-\.\/](0?[1-9]|1[012])[-\.\/](0?[1-9]|[12][0-9]|3[01])$/,
    Y: /^((19|20)[0-9]{2})$/,
    M: /^(0?[1-9]?|1[012]?)$/,
    D: /^(0?[1-9]|[12][0-9]|3[01])$/,
    H: /^(0?[0-9]|1[0-9]|2[0-3])$/,
    m: /^(0?[0-9]|[1-5][0-9])$/,
    DMYCONVERT: /^(0?[1-9]|[12][0-9]|3[01])(0?[1-9]|1[012])((19|20)[0-9]{2})$/,
    MDYCONVERT: /^(0?[1-9]|1[012])(0?[1-9]|[12][0-9]|3[01])((19|20)[0-9]{2})$/,
    YMDCONVERT: /^((19|20)[0-9]{2})(0?[1-9]|1[012])(0?[1-9]|[12][0-9]|3[01])$/,
    DMYTYPE: /^[dD]{1,2}[-\.\/][mM]{1,2}[-\.\/][yY]{4}$/,
    MDYTYPE: /^[mM]{1,2}[-\.\/][dD]{1,2}[-\.\/][yY]{4}$/,
    YMDTYPE: /^[yY]{4}[-\.\/][mM]{1,2}[-\.\/][dD]{1,2}$/
};

