/// <reference path="../jQuery/jquery-1.4.1-vsdoc.js" />

$(function () {
    (function () { // tool button click
        $("div.btn > a").click(function (e) {
            var href = $(this).attr("href"), cls = $(this).attr("class");
            var cbs = $(":checkbox:checked:not(:disabled)[name='cbKey']");
            switch (cls) {
                case "tool_help": // 帮助
                case "tool_new": // 新增
                    // 帮助和新增直接转向
                    document.location.assign(href);
                    break;
                case "tool_report": // 产生报表
                case "tool_edit": // 编辑
                    if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
                    document.location.assign(href.replace(/-id-/ig, cbs.val())); break;
                case "tool_delete": // 删除
                    if (cbs.length !== 1) { alert($g["SelectSingle"]); break; }
                    if (window.confirm($g["DeleteConfirm"])) {
                        document.location.assign(href.replace(/-id-/ig, cbs.val()));
                    }
                    break;
                case "tool_print": // 打印
                    if (cbs.length !== 1) {
                        alert($g["SelectSingle"]); break;
                    }
                    else {
                        var isPrint = cbs.parent().find("input:hidden").val();
                        if (isPrint == "True") {
                            if (typeof RSClientPrint.Print == "undefined") {
                                alert($g["LoadPrintControlError"]);
                                return;
                            }
                            RSClientPrint.MarginLeft = 0;
                            RSClientPrint.MarginTop = 0;
                            RSClientPrint.MarginRight = 0;
                            RSClientPrint.MarginBottom = 0;
                            RSClientPrint.PageHeight = 296.926;
                            RSClientPrint.PageWidth = 210.058;

                            RSClientPrint.Culture = 2052;
                            RSClientPrint.UICulture = 2052;
                            RSClientPrint.UseSingleRequest = true;
                            RSClientPrint.UseEmfPlus = true;

                            RSClientPrint.Print(href, "&id=" + escape(cbs.val()), "");
                            break;
                        }
                        else {
                            alert($g["IsPrint"]); break;
                        }
                    }
            }; e.preventDefault();
        });
    })();
});
