function getTopWindow$() {
    var win = window;
    while (win != win.parent) {
        win = win.parent;
    }
    return win.$;
}

//选 择 弹出层
//isMultipleChoose 是否可以多选 
function ChooseDialogDiv(url, data, viewEditId, width, height, title,isMultipleChoose,callBack) {    //编辑

    var $ = getTopWindow$();
    
    window.editUrl = url;
    window.ChooseDialogData = data;
    // 创建结果显示层
    var editChoose = $("#" + viewEditId + "");
    
    if (editChoose.length == 0) {
      var  editChoose = $("<div>", { id: viewEditId }).appendTo($(document.body));
    }
    var dlgChoose = editChoose.dialog({
    	width: width,
    	height: height,
    	modal: true,
    	title: title,
    	onopen: function () {
    		$.post(window.editUrl, window.ChooseDialogData, function (resultHtml) {
    			editChoose.html(resultHtml);
    			//$t.renderTable(editChoose.find(".dialog_grid_list")); 
    			if ($.isFunction(isMultipleChoose)) {
    				new ChooseDialog(dlgChoose, editChoose, false, isMultipleChoose);
    			} else {
    				new ChooseDialog(dlgChoose, editChoose, isMultipleChoose, callBack);
    			}

    		});
    	},
    	onclose: function () {
    	}
    });
    editChoose.html("");
    dlgChoose.open();
    
}

function ChooseDialog(digChoose, editChoose, isMultipleChoose, callBack) {
    var $ = getTopWindow$();
    this.dlgChoose = digChoose; //编辑层对象
    this.editChoose = editChoose;
    this.isMultipleChoose = isMultipleChoose;
    this.chooseCallBack = callBack;
    this.chooseResult = null;
    this._init();
}

ChooseDialog.prototype = {

    _init: function () //初始化
    {
        var $ = getTopWindow$();
        var currentObj = this;

        var grid_list = $(".dialog_grid_list");
        if (this.isMultipleChoose)
            grid_list.addClass("isMultipleChoose");
        else
            grid_list.removeClass("isMultipleChoose");
        $(".search_lov_dialog #btnSearch").die("click").live("click", function () {
            currentObj.Search(this, currentObj);
        });

        $("#btnChoose").die("click").live("click", function () {
            currentObj.TrdblClick(this, currentObj);
        }); //选择点击事件

        grid_list.find("tr").die("dblclick").live("dblclick", function () {
            currentObj.TrdblClick(this, currentObj);
        }); //行双击事件

        grid_list.find("span.aselect_lov").click(function (e) { //点击事件
            currentObj.TrdblClick(this, currentObj);
            e.preventDefault();
        });
        $("#btnClose").die("click").live("click", function () {
            currentObj.DlgChooseClose(this, currentObj);
        });

        //        $(".pager a").die("click").live("click", function (e) {
        //            currentObj.PagerAClick(e, this, currentObj);
        //        });
        //分页，排序的使用ajax提交
        var id = currentObj.editChoose.attr("id");

        $("#" + id).find("a").die("click").live("click", function (e) {
            currentObj.PagerAClick(e, this, currentObj);
        });

        var pager = function (obj) {
            var iv = currentObj.editChoose.find("div.pager input.i").val();
            var hv = currentObj.editChoose.find("div.pager :hidden").val();
            $.post(hv.replace(/-p-/ig, iv == "" ? 1 : iv), null, function (html) {
                currentObj.editChoose.html(html);
            });
        };
        currentObj.editChoose.find("div.pager :button").die("click").live("click", pager);
        currentObj.editChoose.find("div.pager input.i").die("keypress").live("keypress", function (e) {
            if (e.which == 13) { pager(); }
            else { return e.which >= 48 && e.which <= 57; }
        });
    },
    PagerAClick: function (e, obj, currentObj) {
        $.post($(obj).attr("href"), null, function (html) {
            currentObj.editChoose.html(html);
            //$t.renderTable(s.div);
            //divc.grid({ scroll: s.scroll, isAjax: s.isAjax });
        });
        e.preventDefault();
    },
    Search: function (e, currentObj) {
        var frm = $(e).parents("form");
        $.post(frm.attr("action"), frm.serialize(), function (html) {
            currentObj.editChoose.html(html);
        });
    },
    SureClick: function (e, currentObj) {
        var $ = getTopWindow$();
        var txtArray = null;
        txtArray = $(".dialog_grid_list :text");

        if (txtArray.length <= 0) {
            return;
        }
        if ($.isFunction(currentObj.chooseCallBack)) {
            if (currentObj.isMultipleChoose == false) {
                currentObj.chooseCallBack(txtArray.first().parent().parent(), txtArray.first(), currentObj);
            } else {
                var no = [];
                var no_value = [];
                $(txtArray).each(function (index) {
                    no_value.push(this.value);
                    no.push($("#No" + (index + 1)).val());
                });
                currentObj.chooseCallBack(no, no_value, currentObj);
            }
            currentObj.DlgChooseClose(e, currentObj);
        }
    },
    //双击事件
    TrdblClick: function (e, currentObj) {
        var $ = getTopWindow$();
        var cbs = null;
        cbs = $(".dialog_grid_list :checkbox[name='cbKey'][checked='true']");

        /*if (cbs.length <= 0) {
            return;
        }*/
        if ($.isFunction(currentObj.chooseCallBack)) {
            if (currentObj.isMultipleChoose == false) {
                currentObj.chooseCallBack(cbs.first().parent().parent(), cbs.first(), currentObj);
            } else {
                var trsArry = [];
                var cbsArry = [];
                $(cbs).each(function () {
                    cbsArry.push(this);
                    trsArry.push($(this).parent().parent());
                });
                currentObj.chooseCallBack(trsArry, cbsArry, currentObj);
            }
            currentObj.DlgChooseClose(e, currentObj);
        }
    },
    DlgChooseClose: function (e, currentObj) {  //关闭层方法
        if (currentObj.dlgChoose != null) {
            currentObj.dlgChoose.close();
        }
    }
};




    