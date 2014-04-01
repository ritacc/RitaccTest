/// <reference path="../jQuery/jquery-1.4.1-vsdoc.js" />

var $m = {};
var $v = { // 验证方法集
    _showMessage: function (jqe, resourceKey, func) {
        var p = jqe.parent();
        if (p.find("div.error_info").length == 0) {
            var div = $("<div>", { "class": "error_info" }).appendTo(p);
            div.html($m[resourceKey]);
            jqe.addClass("in_error").blur(function () {
                if ($(this).val() != "") {
                    if ($(this).val() != "0" && $(this).val() != $g["Select"]) {
                        $(this).removeClass("in_error");
                        div.remove();
                    }
                }
            });
            if ($.isFunction(func)) { func.call(jqe); }
        }
        return false;
    },
    RemoveErrorInfo: function (obj) { //删除给定 obj对象范围内的 提示错误信息
        obj.find("div.error_info").remove();
    },
    Compare: function (sourceJqe, targetJqe) //对2个 数,进行比较
    {
        var source = parseFloat(sourceJqe.val());
        var target = parseFloat(targetJqe.val());
        if (source < target) {
            return -1;
        }
        if (source > target) {
            return 1;
        }
        return 0;
    },
    ShowMessage: function (jqe, resourceKey, func) { //显示 错误信息
        return this._showMessage(jqe, resourceKey, func);
    },
    runRegexMath: function (jqe, resourceKey, func, pattern) { // 自定义
        if (!pattern.test(jqe.val())) { return this._showMessage(jqe, resourceKey, func); }
        return true;
    },
    IsEmpty: function (jqe) {
        return jqe.val() == "" || jqe.val() == "0" || jqe.val() == $g["Select"];
    },
    Input: function (jqe, resourceKey, func) { // 必填, 可以输入 0
        if (jqe.val() == "" || jqe.val() == "0" || jqe.val() == $g["Select"]) { return this._showMessage(jqe, resourceKey, func); }
        return true;
    },
    Input0: function (jqe, resourceKey, func) { // 必填
        if (jqe.val() == "" || jqe.val() == $g["Select"]) { return this._showMessage(jqe, resourceKey, func); }
        return true;
    },
    Date: function (jqe, resourceKey, func) { // 匹配日期
        var pattern = /^(19|20)[0-9]{2}[-\.\/](0?[1-9]|1[012])[-\.\/](0?[1-9]|[12][0-9]|3[01])$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Year: function (jqe, resourceKey, func) { // 匹配日期年份
        var pattern = /^(19|20)[0-9]{2}$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Month: function (jqe, resourceKey, func) { // 匹配日期月份
        var pattern = /^([1-9]|1[012])$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num: function (jqe, resourceKey, func) { // 匹配数值型数据
        var pattern = /^(([1-9]\d{0,})|0)(\.\d{1,}){0,1}$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num0: function (jqe, resourceKey, func) { // 匹配整型数据
        var pattern = /^[0-9]{1}\d*$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num2: function (jqe, resourceKey, func) { // 匹配 2 位小数数据
        var pattern = /^[0-9]{1}\d*(\.\d{1,2})?$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num3: function (jqe, resourceKey, func) { // 匹配 3 位小数数据
        var pattern = /^[0-9]{1}\d*(\.\d{1,3})?$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num4: function (jqe, resourceKey, func) { // 匹配 4 位小数数据
        var pattern = /^[0-9]{1}\d*(\.\d{1,4})?$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num5: function (jqe, resourceKey, func) { // 匹配 5 位小数数据
        var pattern = /^[0-9]{1}\d*(\.\d{1,5})?$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Num6: function (jqe, resourceKey, func) { // 匹配 6 位小数数据
        var pattern = /^[0-9]{1}\d*(\.\d{1,6})?$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Letter: function (jqe, resourceKey, func) { // 字母, 大小写均可
        var pattern = /^[a-zA-Z ]+$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    LetterNum: function (jqe, resourceKey, func) { // 字母, 数字
        var pattern = /^[a-zA-Z0-9 ]+$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    LetterNumHanZi: function (jqe, resourceKey, func) { // 字母, 数字, 汉字
        var pattern = /^[\u4e00-\u9fa5A-Za-z0-9\. ]+$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    },
    Email: function (jqe, resourceKey, func) { // Email
        var pattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i;
        return this.runRegexMath(jqe, resourceKey, func, pattern);
    }
};

$(function () {
    (function () { // ajax 提交与验证
        // 创建结果显示层
        var div = $("#divresult");
        if (div.length == 0) { div = $("<div>", { id: "divresult" }).appendTo($(document.body)); }
        window.validateForm = function () { };
        window.ajaxForm = function () {
            $("form").ajaxForm({
                dataType: "html",
                beforeSubmit: window.validateForm,
                success: function (html, statusText, xhr, $form) {
                    if (html == "") { // sucess
                        document.location.assign($(":submit", $form).attr("url"));
                    } else {
                        div.html(html).dialog({ title: $("span.s_none", div).html(), height: 150 }).open();
                    }
                }
            });
        };
    })();
    (function () { // 读入资源文件内容
        $.getJSON(window.Resources, { className: "ValidateText" }, function (o) { $m = o; });
    })();
    (function () { // 退出确认
        window.unloadForm = function (formId) {
            var a, b, isSubmit = true, c = formId == null ? "form" : "#" + formId;
            $(":submit").click(function () { isSubmit = false; });
            $(window).ajaxComplete(function () { a = $(c).serialize(); });
            if (a == undefined) { a = $(c).serialize(); }
            window.onbeforeunload = function () {
                b = $(c).serialize();
                if (a != undefined && a != b && isSubmit) { return $g["Unload"]; }
            };
        }
    })();
    (function () { // 其它
        $("input.bycode[url]").keydown(function (e) {
            if (e.which === 13) {
                window.onbeforeunload = {};
                document.location.assign($(this).attr("url").replace(/-code-/ig, $(this).val()));
                e.preventDefault();
            }
        });
    })();
});

$(document).ready(function () {
//    $(".btn .tool_copy").die("click").live("click", function (e) {
//        var frm = $("<form>", { methred: "post",action:$(this).attr("href") }).appendTo(document.body);
//        $('<input type="hidden" name="copyId" value="">').appendTo(frm);
//        frm.submit();
//        e.preventDefault();
//    });
});
