function GetToday() {
    var D = new Date();
    var Now = D.getFullYear() + '-' +
    FormatD(D.getMonth() + 1) + '-' +
    FormatD(D.getDate()) + ' ' +
    FormatD(D.getHours()) + ':' +
    FormatD(D.getMinutes()) + ':' +
    FormatD(D.getSeconds()) + '';
    return Now;
}
function FormatD(n) {
    return n < 10 ? '0' + n : n;
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
    $("#" + retJId).html(FloatSub(parseFloat(amt), parseFloat(v))); //（扣减后）面值余额
}

var cashCouponSelctor = function (options) {
    var defaults = {
        ctlIdOfBp: "",
        tableId: "tCashCoupons",
        membersCashCouponsNotEnougthMsg: "",
        cashOverflow: "列表中现金券金额超出",
        noCashCouponsSelect: "",
        waringContentSelector: ".waringContent",
        requredCashCoupons: "",
        confirmClearCashCouponsList: "",
        selectCouponsUrl: "",
        custId: "",
        vehId: "",
        getVehId: function () {
            return options.vehId;
        },
        selectCashcouponseTile: "",
        btnOKText: "",
        existsCashcouponseMsg: "",
        btnCancelText: "",
        autoModeSelector: "#chkAutoMode",
        selectCashSelector: "#btnSelectCash"
    };
    this.settings = $.extend(defaults, options);
    var _this = this;
    var _thisSettings = _this.settings;
    this.arrayOfSelectedCashCoupons = new Array();
    //验证填入的现金券列表是否正确
    this.checkArrayOfSelectedCashCoupons = function () {
        var chcpnAmt = parseFloat($("#" + _thisSettings.ctlIdOfBp).val());
        var inputSum = 0;
        $("#" + _thisSettings.tableId).find(".numeric").each(function () {
            //inputSum += parseFloat($(this).val());
            inputSum = FloatAdd(inputSum, parseFloat($(this).val()));
        });
        $("#" + _thisSettings.tableId).find(".decimal_numeric").each(function () {
            inputSum = FloatAdd(inputSum, parseFloat($(this).val()));
        });
        if (chcpnAmt > inputSum) {
            //$(_thisSettings.waringContentSelector).html(_thisSettings.membersCashCouponsNotEnougthMsg + (chcpnAmt - inputSum)).parent().slideDown('fast');
            $(_thisSettings.waringContentSelector).html(_thisSettings.membersCashCouponsNotEnougthMsg + (FloatSub(chcpnAmt, inputSum))).parent().slideDown('fast');
            return false;
        }
        if (chcpnAmt < inputSum) {
            //$(_thisSettings.waringContentSelector).html(_thisSettings.cashOverflow + (inputSum - chcpnAmt)).parent().slideDown('fast');
            $(_thisSettings.waringContentSelector).html(_thisSettings.cashOverflow + (FloatSub(chcpnAmt, inputSum))).parent().slideDown('fast');
            return false;
        }
        if (chcpnAmt == inputSum) {
            $(_thisSettings.waringContentSelector).html("").parent().slideUp('fast');
        }
        return true;
    };


    //删除指定现金券对应的列
    this.removeSelctedCashCoupons = function (chpnCode, obj) {
        _this.arrayOfSelectedCashCoupons = _this.arrayOfSelectedCashCoupons.remove(function (item) {
            return item.chpnCode == chpnCode;
        });
        $(obj).parent().parent().remove();
        _this.genHtmlWitharrayOfSelectedCashCoupons(true);
    };

    //根据数组生成现金券列表的HTML
    this.genHtmlWitharrayOfSelectedCashCoupons = function (notReBind) {
        if (!_this.arrayOfSelectedCashCoupons.length) {
            if (_thisSettings.ctlIdOfBp == "CC_AMT" || _thisSettings.ctlIdOfBp == "CHCPN_AMT") { // by lm
                $("#" + _thisSettings.tableId + " tbody").html("<tr><td colspan=\"8\" style=\"padding:35px;\" align=\"center\"><h2>" + _thisSettings.noCashCouponsSelect + "</h2></td></tr>"); 
            } 
            else {
                $("#" + _thisSettings.tableId + " tbody").html("<tr><td colspan=\"5\" style=\"padding:35px;\" align=\"center\"><h2>" + _thisSettings.noCashCouponsSelect + "</h2></td></tr>");
            }

            _this.checkArrayOfSelectedCashCoupons();
            return;
        }

        var html = "";
        if (!notReBind) {
            $(_this.arrayOfSelectedCashCoupons).each(function (i) {
                //CHCPN_AMT固定死 此ID只用于在积分导入新增 选择现金券列表
                //这段动态HTML代码区别：class=decimal_numeric class=numeric 和对应的options参数有些区别
                if (_thisSettings.ctlIdOfBp == "CHCPN_AMT") {
                    /*html += "<tr>"
                        + "<td>" + this.chpnCode + "</td>"
                        + "<td>" + this.amt + "</td>"
                        + "<td><input type='text' value=\"" + this.chpnAmt + "\" "
                            + "options=\"{hideUpDownBtn: 'True', maxValue: '" + this.amt + "', minValue: '0.01', digits: '2', step: '1'}\""
                            + " class=\"decimal_numeric required\" name=\"cash_" + this.chpnId + "_" + this.chpnCode + "\" /></td>"
                        + "<td><a href='#' chpnCode=\"" + this.chpnCode + "\"  class='delete'>删除</a></td>"
                        + "<td></td>"
                        + "</tr>";*/

                    html += "<tr>"
                        + "<td>" + this.chpnCode + "</td>"
                        + "<td>" + this.desc + "</td>"
                        + "<td>" + this.usage + "</td>"
                        + "<td>" + this.amt + "</td>"
                        + "<td><input type='text' value=\"" + this.chpnAmt + "\" "
                        + "options=\"{hideUpDownBtn: 'True', maxValue: '" + this.amt + "', minValue: '0.01', digits: '2', step: '1'}\""
                        + " class=\"decimal_numeric required\" name=\"cash_" + this.chpnId + "_" + this.chpnCode + "\" id=\"cc_" + this.chpnId + "\" onblur=\"onblurChangeAfterSubstract('" + this.amt + "','cc_" + this.chpnId + "','ccsub_" + this.chpnId + "')\" /></td>"
                        + "<td><span  id=\"ccsub_" + this.chpnId + "\" >" + FloatSub(this.amt, this.chpnAmt) + "</span></td>"
                        + "<td><a href='#' chpnCode=\"" + this.chpnCode + "\"  class='delete'>删除</a></td>"
                        + "<td></td>"
                        + "</tr>";
                } else if (_thisSettings.ctlIdOfBp == "CC_AMT") { //现金券和礼品兑换也需要支持小数 lm
                    html += "<tr>"
                        + "<td>" + this.chpnCode + "</td>"
                        + "<td>" + this.desc + "</td>"
                        + "<td>" + this.usage + "</td>"
                        + "<td>" + this.amt + "</td>"
                        + "<td><input type='text' value=\"" + this.chpnAmt + "\" "
                        + "options=\"{hideUpDownBtn: 'True', maxValue: '" + this.amt + "', minValue: '0.01', digits: '2', step: '1'}\""
                        + " class=\"decimal_numeric required\" name=\"cash_" + this.chpnId + "_" + this.chpnCode + "\" id=\"cc_" + this.chpnId + "\" onblur=\"onblurChangeAfterSubstract('" + this.amt + "','cc_" + this.chpnId + "','ccsub_" + this.chpnId + "')\" /></td>"
                        + "<td><span  id=\"ccsub_" + this.chpnId + "\" >" + FloatSub(this.amt,this.chpnAmt) + "</span></td>"
                        + "<td><a href='#' chpnCode=\"" + this.chpnCode + "\"  class='delete'>删除</a></td>"
                        + "<td></td>"
                        + "</tr>";
                } else {
                    html += "<tr>"
                        + "<td>" + this.chpnCode + "</td>"
                        + "<td>" + this.amt + "</td>"
                        + "<td><input type='text' value=\"" + this.chpnAmt + "\" "
                        + "options=\"{hideUpDownBtn: 'True', maxValue: '" + this.amt + "', minValue: '0', digits: '0', step: '1'}\""
                        + " class=\"numeric required\" name=\"cash_" + this.chpnId + "_" + this.chpnCode + "\" /></td>"
                        + "<td><a href='#' chpnCode=\"" + this.chpnCode + "\"  class='delete'>删除</a></td>"
                        + "<td></td>"
                        + "</tr>";
                }
            });

            $("#tCashCoupons tbody").html(html).find(".required").blur(_this.checkArrayOfSelectedCashCoupons).end().find('.delete').click(function () {
                _this.removeSelctedCashCoupons($(this).attr("chpnCode"), this);
            });
            $("#tCashCoupons tbody tr:even").css("background-color", "#f6f9fc");

            (function () { // 数字输入框
                //$("input.numeric").each(function () {
                $("input.numeric,input.decimal_numeric").each(function () {
                    var opt = {};
                    try { eval("var opt = " + $(this).attr("options")); } catch (e) { }
                    $numerics[$(this).attr("id")] = $(this).numericTextbox(opt);
                });
            })();
        }
        _this.checkArrayOfSelectedCashCoupons();
    };


    this.bindEvent = function () {
        _this.genHtmlWitharrayOfSelectedCashCoupons();
        //自动扣现金券
        $(_thisSettings.autoModeSelector).click(function () {
            if ($("#" + _thisSettings.ctlIdOfBp).val() == "") {
                alert(_thisSettings.requredCashCoupons);
                return false;
            }
            if (confirm(_thisSettings.confirmClearCashCouponsList)) {
                _this.arrayOfSelectedCashCoupons = new Array();
                var chcpnAmt = parseFloat($("#" + _thisSettings.ctlIdOfBp).val());
                var breakLoop = false;
                $.ajax({
                    url: _thisSettings.selectCouponsUrl,
                    data: { "Search.CUST_ID": _thisSettings.custId, "Search.VEH_ID": _thisSettings.getVehId(), "Search.BEGIN_EXPIRY_DATE": GetToday() },
                    cache: false,
                    success: function (data) {
                        if (data.Success) {
                            $(data.Result.List).each(function () {
                                var item = this;
                                var pruchrs = 0;
                                var issueflag = item.ISSUE_FLAG.toUpperCase(); //未生效 过期 或 已用完的现金券不允许选择 忽略
                                var expiryflag = item.EXPIRY_FLAG.toUpperCase();
                                if (issueflag == "N" || expiryflag == "Y" || parseFloat(item.AMT) == 0) {
                                    return;
                                }
                                if (chcpnAmt >= item.AMT) {
                                    pruchrs = item.AMT;
                                    //chcpnAmt -= item.AMT;
                                    chcpnAmt = FloatSub(chcpnAmt, item.AMT);
                                } else {
                                    pruchrs = chcpnAmt;
                                    breakLoop = true;
                                }
                                if (pruchrs != 0)
                                    _this.arrayOfSelectedCashCoupons.push({ chpnId: item.CHCPN_ID, chpnCode: item.CHCPN_CODE, amt: item.AMT, chpnAmt: pruchrs, usage: item.USAGE, desc: item.CHCPN_DSC });

                                return !breakLoop;
                            });
                        }

                        _this.genHtmlWitharrayOfSelectedCashCoupons();
                    }
                });
            }
        });

        //选取现金券 
        $(_thisSettings.selectCashSelector).click(function () {
            if ($("#" + _thisSettings.ctlIdOfBp).val() == "") {
                alert(_thisSettings.requredCashCoupons);
                return false;
            }
            popDialog(_thisSettings.selectCouponsUrl + '?Search.CUST_ID=' + _thisSettings.custId + '&Search.VEH_ID=' + _thisSettings.getVehId() + "&Search.BEGIN_EXPIRY_DATE=" + GetToday(), "SelectCashCouponsDialog", 700, 400, _thisSettings.selectCashcouponseTile, [{
                text: _thisSettings.btnOKText,
                onclick: function (div, win, dlg) {
                    var cbs = win.$(":checkbox:checked:not(:disabled)[name='cbKey']");

                    var onError = false;
                    var ispass = true;
                    var addedStr = "";
                    $(cbs).each(function (i) {
                        var $this = $(this);
                        var chpnId = $this.val();
                        var chpnCode = $this.attr("CHCPN_CODE");
                        var amt = $this.attr("AMT");

                        var CHCPNDSC = $this.attr("CHCPNDSC");
                        var CCUASGE = $this.attr("CCUASGE");

                        var issueflag = $this.attr("ISSUE_FLAG").toUpperCase(); //未生效 过期 或 已用完的现金券不允许选择
                        var expiryflag = $this.attr("EXPIRY_FLAG").toUpperCase();
                        if (issueflag == "N" || expiryflag == "Y" || parseFloat(amt) == 0) {
                            alert("选择的现金券中存在未生效或已过期或者已用完的现金券");
                            ispass = false;

                            return false;
                        }
                        if (_this.arrayOfSelectedCashCoupons.any(function (item) {
                            return item.chpnCode == chpnCode;
                        })) {
                            onError = true;
                            addedStr += chpnCode + " ";
                        } else {
                            _this.arrayOfSelectedCashCoupons.push({ chpnId: chpnId, chpnCode: chpnCode, amt: amt, chpnAmt: 0, usage: CCUASGE, desc: CHCPNDSC });
                        }
                        return true;
                    });

                    _this.genHtmlWitharrayOfSelectedCashCoupons();
                    if (onError) {
                        alert(addedStr + _thisSettings.existsCashcouponseMsg);
                    }

                    if (ispass) {
                        dlg.close();
                    }
                }
            }, {
                text: _thisSettings.btnCancelText,
                onclick: function (div, win, dlg) {
                    dlg.close();
                }
            }]);

        });


        $("#" + _thisSettings.ctlIdOfBp).bind("blur", function () {
            _this.checkArrayOfSelectedCashCoupons();
        });

    };

};
