/// <reference Path="jquery-1.4.1-vsdoc.js" />
(function () { // change 事件被 changed 事件代替
    $.combobox = function (options) {
        var defaults = {
            element: null, // 绑定元素, 必须是 select, 一定要用 div 框住
            size: 10, // 显示的项数量
            isInput: false // 是否允许输入, 为 true 刚获取 text, false 获取 value
        };
        var settings = $.extend(defaults, options);
        this.element = settings.element;
        this.size = settings.size;
        this.isInput = settings.isInput;
    };
    $.combobox.prototype = {
        _contains: function (a, b) {
            return a.contains ? a != b && a.contains(b) : !!(a.compareDocumentPosition(b) & 16);
        },
        _init: function () {
            this._divc = $("<div>", { "class": "div_container_combobox" }).insertBefore(this.element);
            this._input = $("<input>", {
                "class": "input_combobox " + this.element.attr("class"),
                type: "text",
                readonly: !this.isInput
            }).appendTo(this._divc);
            this._a = $("<a>", { "class": "a_combobox" }).appendTo(this._divc);

            //this._divc.css("display", "inline");
            //this._a.css("left", (this._input.outerWidth(true) - this._a.outerWidth(true))).css("top","-2px");

            this._divs = $("<div>", { "class": "div_select_combobox" }).insertAfter(this.element);
            this._hidden = $("<input>", { type: "hidden" }).insertAfter(this._divs);
            this._isDidsabled = (this.element.attr("disabled") || this.element.attr("readonly"));
            if (this._isDidsabled) { this._input.attr("disabled", true); }
        },
        _stopAll: function (handler, context) { // 阻止 all
            return function (e) {
                e.preventDefault(); e.stopPropagation();
                handler.apply(context || this, arguments);
            }
        },
        _delegate: function (context, handler) { // 绑定事件
            return function (e) { handler.apply(context, [e, this]); };
        },
        _preventDefault: function (e) { // 阻止默认行为
            e.preventDefault();
        },
        _scroll: function (o) { // 绝对偏移
            var a = o.parentNode, l = 0, t = 0;
            while (a != document.body && a != undefined) {
                l += a.scrollLeft; t += a.scrollTop; a = a.parentNode;
            }
            return { top: t, left: l };
        },
        _click: function () {
            var s = this;
            var t = function (e) {
                this._input.focus(); this._divs.toggle(); e.stopPropagation();
                if ($(this._divs).not(":hidden")) {
                    var of = this._input.offset(), ih = this._input.outerHeight();
                    var dw = this._divs.outerWidth(), dh = this._divs.outerHeight();
                    var ww = $(window).width(), wh = $(window).height();
                    var left = of.left, top = of.top + ih;
                    var stop = (this._index > this.size - 1 ? this._index : 0) * this._divs.children().eq(0).outerHeight();
                    if (of.top + ih + dh > wh) { top = of.top - dh; }
                    if (of.left + dw > ww) { left = ww - dw; }
                    //this._divs.css({ top: top, left: left }).scrollTop(stop);  //解决弹出层中的combobox位置不正确的问题
                    this._divs.css({ top: 0, left: 0 }).scrollTop(stop);
                    this._divs.offset({ top: top, left: left }).scrollTop(stop);
                    //使用offset 方法..这个元素的位置是相对于document对象的
                    //以前，因为父的DIV 是绝定定位,或相对定位, 层的top,left是相对于 父DIV来的。没有相对于 当前document对象
                }
            };
            var b = function (e) {
                var to = document.activeElement;
                if (!(this._contains(this._divc[0], to) || this._contains(this._divs[0], to) || this._divc[0] === to || this._divs[0] === to)) {
                    var value = this._input.val(), istest = false;
                    if (value !== "") {
                        if (/ - /ig.test(value)) { value = value.split(" - ")[0]; }
                        $(this._text).each(function (i, n) {
                            if (/ - /ig.test(n)) { istest = n.split(" - ")[0] === value; }
                            else { istest = value === n; }
                            if (istest) { s._delegate(s, s._changeValueForInput)(i); return false; }
                            else {
                                if (i == s._text.length - 1) {
                                    s._delegate(s, s._changeValueForInput)(0);
                                    s._input.focus(); alert("\"" + value + "\" 未发现")
                                }
                            }
                        });
                    }
                    this._hide();
                }
            }
            this._a.bind("click", this._delegate(this, t)).attr("tabindex", "-1"); ;
            this._input.bind({
                click: this._delegate(this, t),
                blur: this._delegate(this, b), // 检查输入的值
                focus: function () { s._input.select(); }
            });
            this._divs.parents().bind({ scroll: this._delegate(this, this._hide) });
            this._divs.mouseover(function () { $(this).children().removeClass("current"); });
            $(document).bind("click", function (e) {
                var divcurrent = $(e.target)[0];
                if (!(s._contains(s._divc[0], divcurrent) || s._contains(s._divs[0], divcurrent))) { s._hide(); }
            });
        },
        _keydown: function () {
            var kd = function (e) {
                var up = 38, down = 40, esc = 27, enter = 13;
                if (e.which == up) { this._index--; }
                else if (e.which == down) { this._index++; }
                else if (e.which == esc) { this._hide(); }
                //else if (e.which == enter) { this._input.blur(); e.preventDefault(); }
                else { return false; }
                if (this._index >= this._text.length - 1) { this._index = this._text.length - 1; }
                else if (this._index <= 0) { this._index = 0; }
                this._divs.children().removeClass("current").eq(this._index).addClass("current");
                this._changeValueForInput(this._index);
            };
            this._input.bind("keydown", this._delegate(this, kd));
            this._divs.bind("keydown", this._delegate(this, kd));
        },
        _hide: function () {
            this._divs.hide();
        },
        _changeValueForInput: function (i) {
            this._index = i; this._input.val(this._text[i]); this._hidden.val(this._value[i]);
            this._input.attr("val", this._hidden.val());
            if (this.isInput) { $(this._input).trigger("changed"); }
        },
        _changeValue: function (i) {
            this._index = i; this._input.select();
            this._input.val(this._text[i]); this._hidden.val(this._value[i]);
            if ($(this._divs).not(":hidden")) { this._hide(); }
            this._input.attr("val", this._hidden.val());
            if (!this.isInput) { $(this._input).trigger("changed"); }
        },
        _loc: function () { // 位置
            this._index = 0, this._text = [], this._value = [];

            var id = this.element.attr("id"), name = this.element.attr("name");
            this.element.removeAttr("class").removeAttr("id").removeAttr("name").hide();
            this._input.attr("id", id); this._hidden.attr("id", id + "_1");
            //if (this.isInput) { this._input.attr("name", name); }
            //else { this._hidden.attr("name", name); }
            this._hidden.attr("name", name);

            this.refresh();
            this._setValue();
        },
        _apply: function () {
            if (this.element.attr("tagName") == "SELECT") {
                this._init(); this._loc();
                if (!this._isDidsabled) {
                    this._click(); this._keydown();
                }
            }
        },
        _setValue: function () {
            this._input.val(this._text[this._index]);
            this._hidden.val(this._value[this._index]);
            this._input.attr("val", this._hidden.val());
            this._divs.children().removeClass("current").eq(this._index).addClass("current");
        },
        refresh: function () {
            var s = this, a = null, c = 0;
            this._divs.empty().show(), this._text = [], this._value = [];
            $("option", this.element).each(function () {
                var text = $(this).text(), value = $(this).val();
                s._text.push(text); s._value.push(value);
                if ($(this).attr("selected")) { s._index = c; }
                a = $("<a>", { id: c }).bind({
                    "selectstart": s._preventDefault,
                    "click": function () { s._delegate(s, s._changeValue)($(this).attr("id")); }
                }).html(text == "" ? "&nbsp;" : text).appendTo(s._divs);
                ++c;
            });
            var ah = a != null ? a.outerHeight() : 0;
            var w, iw = this._input.outerWidth(), ew = this.element.outerWidth(), ww = $(window).width();
            ew += 6 + (c > this.size ? 20 : 0); // 加上 padding 和 scrollbar 宽
            w = iw > ew ? iw : ew;
            this._a.height(this._input.outerHeight() - 4);
            this._divs.css({
                height: (this.size > c ? c : this.size) * ah,
                width: (w > ww ? ww : w) - 2,
                display: "none"
            });
        },
        getValue: function () {
            return this._hidden.val();
        },
        getText: function () {
            return this._input.val();
        },
        setValue: function (value) {
            var s = this;
            $.each(this._value, function (i, n) {
                if (n == value) { s._index = i; return; }
            });
            this._setValue();
        },
        setText: function (text) {
            var s = this;
            $.each(this._text, function (i, n) {
                if (n == text) { s._index = i; return; }
            });
            this._setValue();
        }
    }

    $.fn.combobox = function (options) {
        var comBox = new $.combobox($.extend({ element: $(this) }, options));
        comBox._apply();
        return comBox;
    }
})();