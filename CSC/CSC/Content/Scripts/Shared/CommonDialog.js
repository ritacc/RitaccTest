/*
    弹出层的一些绑定
*/
(function () { // 日历
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
})();

(function () { // Combobox
    $("select.combobox").each(function () {
        var options = {};
        try { eval("var options = " + $(this).attr("options")); } catch (e) { }
        $comboboxs[$(this).attr("id")] = $(this).combobox(options);
    });
})();

(function () { // LOV
    $("input.lov").each(function () {
        var options = {};
        try { eval("var options = " + $(this).attr("options")); } catch (e) { }
        $lovs[$(this).attr("id")] = $(this).LOV(options);
    });
})();
(function () { // 数字输入框
    $("input.numeric").each(function () {
        var options = {};
        try { eval("var options = " + $(this).attr("options")); } catch (e) { }
        $numerics[$(this).attr("id")] = $(this).numericTextbox(options);
    });
})();
(function () { // Tabs
    $("div.tab_container_dialog").each(function () {
        $(this).tabs();
    });
})();

(function () { // Grid
    $("div.dialog_grid_list").each(function () {
        $grids[$(this).attr("id")] = $(this).grid();
    });
})();

(function () { // 列表操作	
	var win = window;
	while(win != win.parent)
	{
		win = win.parent;
	}
    $t.renderTable(win.$("div.dialog_grid_list").find("table"));
 
    // 全选
    $(":checkbox.cball").click(function () {
        $(":checkbox[name='cbKey'][disabled='false']").attr("checked", $(this).attr("checked")).each(function () {
            tr.check.call($(this).parents("tr"));
        });
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

