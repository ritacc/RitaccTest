var bindLovClick = function (ctrId, url, width, height, hiddenId, codeOrdsc, callBack, title, isOverwriteMode, closeClearSelectedOfWindow,noInitFromHidden) {
    var _this = this;
    this.separator = ";";
    this.dataName = "selected" + (ctrId ? ctrId : hiddenId);
    this.dscAttrName = codeOrdsc ? codeOrdsc : "dsc";
    this.$saveWinBody = $(window.parent.document.body);
    this.isOverwriteMode = isOverwriteMode;
    this.remove = function (arr, a) {
        var newArr = new Array();
        for (i = 0; i < arr.length; i++) {
            if (typeof (a) == "function") {
                if (!a(arr[i]))
                    newArr.push(arr[i]);
            }
            else if (arr[i] != a) {
                newArr.push(arr[i]);
            }
        }
        return newArr;
    };

    this.funSelectedDefault = function (win, selectedArr) {
        if (!selectedArr) return;
        var cbs = $(win.document).find(":checkbox:not(:disabled)[name='cbKey']");
        cbs.each(function () {
            var cb = $(this);
            var cbKey = this.value;
            $(selectedArr).each(function () {
                if (cbKey == this) {
                    cb.attr("checked", true);
                    tr.check.call(cb.parents("tr"));
                }
            });
        });
    };

    this.isExists = function (arr, a) {
        for (var i in arr) {
            if (arr[i].val == a.val) {
                return true;
            }
        }
        return false;
    };
    this.saveSelectedArr = function (arr) {
        _this.$saveWinBody.data(dataName, arr);
    };
    this.getSavedSelectedArr = function () {
        return _this.$saveWinBody.data(dataName);
    };

    this.clearSelectedArr = function (win) {
        if (win) {
            var selectedCbs = $(win.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
            selectedCbs.removeAttr("checked");
        }
        _this.$saveWinBody.data(dataName, []);
    };

    this.btnClick = function (win) {
        var selectedCbs = $(win.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
        var unSelectedCbs = $(win.document).find(":checkbox[name='cbKey']").not(selectedCbs);

        var valArray = new Array();

        selectedCbs.each(function (i) {
            var $this = $(this);
            valArray.push({ val: $this.val(), dsc: $this.attr(dscAttrName) || $this.val() });
        });

        if (_this.isOverwriteMode) {
            _this.saveSelectedArr(valArray);
            return;
        }

        var selectedFromData = _this.getSavedSelectedArr();
        if (!selectedFromData)
            selectedFromData = new Array();


        var removeArray = new Array();

        for (var i in valArray) {
            for (var j in selectedFromData) {
                if (_this.isExists(selectedFromData, valArray[i])) {
                    removeArray.push(valArray[i]);
                }
            }
        }
        unSelectedCbs.each(function (i) {
            var $this = $(this);
            removeArray.push({ val: $this.val() });
        });

        for (var k in removeArray) {
            selectedFromData = remove(selectedFromData, function (m) { return m.val == removeArray[k].val; });
        }


        for (var i in valArray) {
            if (!_this.isExists(selectedFromData, valArray[i])) {
                selectedFromData.push(valArray[i]);
            }
        }
        _this.saveSelectedArr(selectedFromData);

    };
    this.getSelectedVal = function (win, getP) {
        var data = _this.getSavedSelectedArr();
        if (data) {
            var allValArray = new Array();
            $(data).each(function () {
                if (getP)
                    allValArray.push(getP(this));
                else
                    allValArray.push(this.val);
            });
            return allValArray;
        }
        return new Array();
    };
    this.initSelected = function (win) {
        //        if (this.getSelectedVal(win))
        //            console.log(this.getSelectedVal(win).join(';'));
        funSelectedDefault(win, this.getSelectedVal(win));
        $(win.document).find("div.pager>ul>li>a").unbind("click").click(function () { _this.btnClick(win); });
    };

    if (!hiddenId)
        hiddenId = ctrId;
    if (!ctrId)
        ctrId = hiddenId;
    if (!width)
        width = 1000;
    if (!height)
        height = 600;
    var htmlCtl = $("#" + ctrId);
    var hideCtl = $("#" + hiddenId);
	//commmted by jason in 20130719
    //var selected = hideCtl.val()
    //end commmted by jason in 20130719
	
    //added by jason in 20130719
    var selected = hideCtl.val() !== "0" ? hideCtl.val() : "";
    //end added by jason in 20130719
    if (selected != "" && !noInitFromHidden) {
        var selectedArr = selected.split(separator);

        var selectedFromHidden = new Array();
        for (var i in selectedArr) {
            selectedFromHidden.push({ val: selectedArr[i], dsc: selectedArr[i] });
        }
        var saved = _this.getSavedSelectedArr();
        //如果已经保存过选中项就不再从hidden中获取，只有第一次打开时需要从hidden中获取值
        if (!saved || !saved.length) {
            _this.saveSelectedArr(selectedFromHidden);
        }
    }

    var confirmSelected = function (div, win, dlg) {
        _this.btnClick(win);
        funSelectedDefault(win, _this.getSelectedVal(win));
        var selectedValues = _this.getSelectedVal(win).join(separator);
        var selectedDsc = _this.getSelectedVal(win, function (item) { return item.dsc; }).join(separator);


        htmlCtl.valOrHtml(selectedDsc);
        hideCtl.valOrHtml(selectedValues);

        if (callBack)
            callBack(selectedValues, selectedDsc);
        dlg.close();
    };

       popDialog(url, "select", width, height, title || '请选择', [{ text: $g["LOV_OK"], onclick: confirmSelected
       }, { text: $g["LOV_CLEAR"], onclick: function (div, win, dlg) {
        _this.clearSelectedArr(win);
        confirmSelected(div, win, dlg);
    }
      }, { text: $g["LOV_CANCEL"], onclick: function (div, win, dlg) {
        dlg.close();
    }
    }], this.initSelected, function (div) {
        if (typeof(closeClearSelectedOfWindow) == "undefined")
            closeClearSelectedOfWindow = true; //默认让窗口关闭时将存放在父窗口中的选择值清除
        if (closeClearSelectedOfWindow) {
            _this.clearSelectedArr(div.find("iframe")[0].contentWindow);
        }
    });

    return _this;
}