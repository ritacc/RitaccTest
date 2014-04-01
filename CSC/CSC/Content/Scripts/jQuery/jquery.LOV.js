/// <reference Path="jquery-1.4.1-vsdoc.js" />
(function () {
    $.LOV = function (options) {
        var defaults = {
            element: null, // 绑定元素, 默认是 textbox
            isInput: false, // 是否允许输入
            width: 400, // 宽度
            height: 350, // 高度
            viewName: "staff", // 加载的功能视图名称
            onchoosed: null,
            isNotFindWindow: true, //是否弹出未找到选中记录的窗口.
            lovData: null

        };
        var settings = $.extend(defaults, options);
        this.element = settings.element;
        this.isInput = settings.isInput;
        this.width = settings.width;
        this.height = settings.height;
        this.viewName = settings.viewName;
        this.onchoosed = settings.onchoosed;
        this.isNotFindWindow = settings.isNotFindWindow;
        this.lovData = settings.lovData;
    };
    $.LOV.prototype = {
        _contains: function (a, b) {
            return a.contains ? a != b && a.contains(b) : !!(a.compareDocumentPosition(b) & 16);
        },
        _init: function () {
            this._hidden = $("input.hid_lov", this.element.parent());
            this._divc = this.element.wrap($("<div>", { "class": "div_container_lov" })).parent();
            this._a = $("<a>", { "class": "a_lov" }).appendTo(this._divc);
            this._divs = $("<div>", { "class": "div_select_lov" }).addClass("s_none").insertAfter(this.element);
            this._isDidsabled = (this.element.attr("disabled") || this.element.attr("readonly"));
            this._isInput = true;
        },
        _aAll: function () {
            var s = this;
            this._divs.find("a").click(function (e) {
                s._loadData($(this).attr("href"));
                e.preventDefault();
            });
        },
        _loadData: function (url, params) {
            var s = this;
            var data = $.extend({ v: this.viewName }, params, s.lovData);
            $.get(url, data, function (html) {
                s._divs.html(html); s._hidden.val(s.element.attr("val"));
                var search = s._divs.find("div.search_lov");
                var grid = s._divs.find("div.list_lov");
                var pager = s._divs.find("div.pager_lov");
                var sf = s._divs.find("input.sf").val();
                var sd = s._divs.find("input.sd").val();
                grid.height(s.height - search.outerHeight() - pager.outerHeight()).grid();
                var dblclick = function () {
                    var id = $("input:checkbox", this).val();
                    if (id === undefined) { //
                        return;
                    }
                    var name = $("input:hidden", this).val();
                    s.element.val(name).attr("val", id);
                    s._hidden.val(id);
                    //1.id.2name.3.description
                    //                    if (s.onchoosed) {
                    //                        var description = $("input:checkbox", this).parent().parent().find("td").eq(2).text();
                    //                        eval(s.onchoosed + "(\"" + id + "\",\"" + name + "\",\"" + description + "\");");
                    //                    }
                    var description = $("input:checkbox", this).parent().parent().find("td").eq(2).text();
                    s.element.trigger("changed", [id, name, description]); //1.changed
                    s._hide();
                    s._isInput = false;
                };
                var click = function () {
                    $(this).siblings("tr[backgroundColor!='']").each(function () {
                        var cb = $(this).find(":checkbox[name='cbKey']");
                        if (!cb.attr("disabled")) {
                            cb.attr("checked", false);
                            tr.check.call(this);
                        }
                    });
                    tr.click.call(this);
                };
                grid.find("table > tbody > tr").each(function () {
                    var checkbox = $("input:checkbox", this);
                    if (s._hidden.val() == checkbox.val()) {
                        checkbox.attr("checked", true);
                        $(this).css("background-color", "blue");
                    } else {
                        $(this).find(":checkbox[name='cbKey']").click(function () {
                            $(this).attr("checked", !$(this).attr("checked"));
                        }).end().bind({
                            click: $t.delegate(this, click),
                            mouseover: $t.delegate(this, tr.mouseOver),
                            mouseout: $t.delegate(this, tr.mouseOut),
                            dblclick: $t.delegate(this, dblclick)
                        });
                    }
                }).end().find("table > tbody > tr:odd").addClass("alter");
                s._aAll();
                grid.find("th.sort").eq(sf).addClass(sd == "-1" ? "sort_desc" : "sort_asc");
                grid.find("span.aselect_lov").click(function (e) {
                    dblclick.call(grid.find(":checkbox[name='cbKey'][checked='true']").parents("tr"));
                    e.preventDefault();
                });
                $("input:button", search).click(function () {
                    var form = $("form", search), params = {};
                    $.each($(form).serializeArray(), function (i, n) {
                        params[n["name"]] = n["value"];
                    });

                    s._loadData(form.attr("action"), params, s.lovData);
                });
                $("*", s._divs).andSelf().attr("tabindex", "-1");
            });
        },
        _hide: function () {
            this._divs.hide();
        },
        _loc: function () { // 位置
            this._a.height(this.element.outerHeight() - 4).attr("tabindex", "-1");
            this._divs.height(this.height).width(this.width);
        },
        _delegate: function (context, handler) { // 绑定事件
            return function (e) { handler.apply(context, [e, this]); };
        },
        _preventDefault: function (e) { // 阻止默认行为
            e.preventDefault();
        },
        _click: function () {
            var s = this;
            var t = function (e) {
                this.element.focus(); this._divs.toggle();
                if ($(this._divs).not(":hidden")) {
                    this._loadData(window.LOVPost);
                    var of = this.element.offset(), ih = this.element.outerHeight();
                    var ww = $(window).width(), wh = $(window).height();
                    var left = of.left, top = of.top + ih;
                }
            };
            var b = function (e) {
                var to = document.activeElement;
                if (!(this._contains(this._divc[0], to) || this._contains(this._divs[0], to))) {
                    var value = this.element.val();
                    this._isInput = true;
                    if (value !== "" && this._isInput) { s._modifyInput(value); }
                    else {
                        s._hidden.val("");
                        s.element.val("").attr("val", "");
                        s.element.trigger("changed", ["", "", ""]); //2.changed
                        //if (s.onchoosed) {
                        //    eval(s.onchoosed + "('','','');");
                        //}
                    }
                    this._hide();
                }
            }
            this._a.bind("click", this._delegate(this, t));
            this.element.bind({
                click: this._delegate(this, t),
                blur: this._delegate(this, b), // 检查输入的值
                focus: function () { s.element.select(); },
                keydown: function () { s._isInput = true; }
            });
            $(document).bind("click", function (e) {
                var divcurrent = $(e.target)[0];
                if (!(s._contains(s._divc[0], divcurrent) || s._contains(s._divs[0], divcurrent))) {
                    s._hide();
                }
            });
        },
        _modifyInput: function (value) {
            var s = this;
            var data = $.extend({ v: this.viewName }, { code: value }, s.lovData);
            if (value == "") {
                s._hidden.val("");
                s.element.val("").attr("val", "");
                s.element.trigger("changed", ["", "", ""]); //3.changed
                return;
            }

            $.getJSON(window.LOVCheck, data, function (json) {
                if (json.Code && json.Id) {
                    s.element.val(json.Code).attr("val", json.Id);
                    s._hidden.val(json.Id);
                    s.element.trigger("changed", [json.Id, json.Code, json.Description]); //4.changed
                    //  if (s.onchoosed) {
                    //      eval(s.onchoosed + "(\"" + json.Id + "\",\"" + json.Code + "\",\"" + json.Description + "\");");
                    //  }
                } else {
                    s._hidden.val("");
                    s.element.val(value).attr("val", "");

                    //为true ,弹出未找到记录的窗口.
                    if (s.isNotFindWindow == true) {
                        s.element.focus();
                        s.element.trigger("changed", ["", "", ""]); //5.changed
                        //                    if (s.onchoosed) {
                        //                        eval(s.onchoosed + "('','','');");
                        //                    }
                        alert($g["NotFind"] + "(" + value + ")");
                    }
                    else {
                        s.element.trigger("changed", ["", "", ""]);
                    }
                }
            });
        },
        _apply: function () {
            this._init(); this._loc(); this._click();
        },
        refresh: function () {
            alert("todo:");
        },
        getValue: function () {
            return this._hidden.val();
        },
        getText: function () {
            return this.element.val();
        },
        setValue: function (value) {
            this._hidden.val(value);
        },
        setText: function (text) {
            this.element.val(text);
        }
    }

    $.fn.LOV = function (options) {
        var lov = new $.LOV($.extend({ element: $(this) }, options));
        lov._apply();
        return lov;
    }
})();