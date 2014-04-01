/*
*MasterControl方法封装
*依赖于jQuery 
*by xh 2012
*/
var masterControl = (function () {
    var _selectContactSwitch = false;
    var _temporaryRing = false;
    //CTI状态枚举
    var _enumJobStatus = {
        Init: 0,
        LoadContact: 1,
        UnSaved: 2,
        Saved: 3
    };
    var _enumCtiStatus = {
        Init: 0, //初始化状态
        Call: 1, //通话中
        Answer: 2, //已拨出
        Ring: 3, //响铃
        Hung: 4//通话结束
    };

    var _currentPhoneContact = {
        lastCallPhone: null,
        lastAnswerPhone: null
    };

    var _self = {
        clearAttractTarget: function () {
            for (var i in currentAttractTarget) {
                if (typeof (currentAttractTarget[i]) != "function")
                    currentAttractTarget[i] = null;

            }
            _self.setSwithCallStatus(true);
            _self.clearAttractTxt();
        },
        clearAttractTxt: function () {
            $("#AttractTarget_CUST_NAME").valOrHtml("");
            $("#CUTC_NAME").valOrHtml("");
            $(".attract").valOrHtml("");
        },
        enumJobStatus: _enumJobStatus,
        enumCtiStatus: _enumCtiStatus,
        currentPhoneContact: _currentPhoneContact,
        jobStatus: (function () {
            var _currentStatus = _enumJobStatus.Init;
            return {
                setCurrentStatus: function (val) {
                    _currentStatus = val;
                    if (val == _enumJobStatus.UnSaved) {
                        _self.setCtlStatus.unAllowChangeContactSetCtl();
                    }
                },
                getCurrentStatus: function () {
                    return _currentStatus;
                }
            };
        })(),
        setSwithCallStatus: function (val) {
            //currentAttractTarget.SwithCallStatus = val;
            /*if (!val) {
            $("#callStatusComplete").click();
            }*/
        },
        ctiStatus: (function () {
            var _currentStatus = _enumCtiStatus.Init;
            return {
                setCurrentStatus: function (val) {
                    _currentStatus = val;
                    switch (val) {
                        case _enumCtiStatus.Init:
                            _self.setCtlStatus.initSetCtl();
                            break;
                        case _enumCtiStatus.Call:
                            _self.setCtlStatus.callSetCtl();
                            break;
                        case _enumCtiStatus.Answer:
                            _self.setCtlStatus.answerSetCtl();
                            break;
                        case _enumCtiStatus.Ring:
                            _self.setCtlStatus.ringSetCtl();
                            break;
                        case _enumCtiStatus.Hung:
                            _self.setCtlStatus.hungSetCtl();
                            break;
                    }
                },
                getCurrentStatus: function () {
                    return _currentStatus;
                }

            };
        })(),
        //设置投诉
        setCMHDID: function (id) {
            currentAttractTarget.CMHD_ID = id;
            //暂存
            _self.TemporaryJob(null, true);
            $("#btnTemporary").removeAttr("disabled");
            _self.ajaxLoadData();
        },
        //设置问卷
        setISURVID: function (id) {
            currentAttractTarget.ISURV_ID = id;
            //暂存
            _self.TemporaryJob(null, true);
            $("#btnTemporary").removeAttr("disabled");
            _self.ajaxLoadData();
        },
        //受控
        controlledStatus: (function () {
            var _controlledByVeh = false;
            var _controlledByVehLPN = null; //车牌号
            var _controlledByVehFN = null; //车架号
            var _controlledByContact = false;
            var _controlledByVehID = null;
            var _controlledByVehIDCopy = null;
            var _controlledByContactID = null;
            var _controlledByContactName = null;
            return {
                setControlledByVeh: function (vehID, lpn, fn) {
                    _controlledByVeh = true;
                    _controlledByVehID = vehID;
                    _controlledByVehIDCopy = vehID;
                    _controlledByVehLPN = lpn;
                    _controlledByVehFN = fn;
                    $("#txtControlledByVehLpn").valOrHtml(lpn);
                    $("#txtControlledByVehFn").valOrHtml(fn);
                    $("#controlledByVeh").attr("checked", true);
                    $("#controlledByVeh").val(vehID);
                    parentOpretion.showTabPage(4);
                },
                getControlledByVeh: function () {
                    return { isControlled: _controlledByVeh, id: _controlledByVehID, idCopy: _controlledByVehIDCopy };
                },
                setControlledByContact: function (id, name) {
                    _controlledByContact = true;
                    _controlledByContactID = id;
                    _controlledByContactName = name;
                    parentOpretion.showTabPage(5);
                },
                getControlledByContact: function () {
                    return { isControlled: _controlledByContact, id: _controlledByContactID, name: _controlledByContactName };
                },
                clearControlledByVeh: function () {
                    _controlledByVeh = false;
                    _controlledByVehID = null;
                    parentOpretion.showTabPage(0);
                },
                clearControlledByContact: function () {
                    _controlledByContact = false;
                    _controlledByContactID = null;
                    _controlledByContactName = null;
                    $("#controlledByAttractAtrget").valOrHtml("");
                    parentOpretion.showTabPage(5);
                }
            };
        })(),
        setCtlStatus: (function () {
            var _result = {
                ctiCtlEnable: function () {
                    quickInvoker.enableCtl("btnHung,CALL_DETAIL,IMPORTANT_INFO");
                    $("input[name='radioCallRemarkType']").removeAttr("disabled");
                },

                unAllowChangeContactSetCtl: function () {
                    quickInvoker.disableCtl("btnAttractProc,btnAttractObject,btnUnlockAttractObject,btnResume");
                },
                //未进行拨出时控件状态设置
                initSetCtl: function () {
                    quickInvoker.disableCtl("btnHung,btnKeeping,btnTransferAgent,btnForward,btnThreeWayCalling,btnAnswer,btnTemporary,btnSave");
                    if (currentAttractTarget.InAttractStatus() || !checkIsNullVal(currentAttractTarget.CUTC_NAME)) {
                        quickInvoker.disableCtl("btnAttractObject,btnResume");
                    }
                },
                //拨出时控件状态设置
                callSetCtl: function () {
                    _result.ctiCtlEnable();
                    quickInvoker.enableCtl("btnKeeping,btnThreeWayCalling,CallFail");

                    $("#btnKeeping").valOrHtml(textConfig.hold);
                    $("input[name='callStatus']").removeAttr("disabled");

                    quickInvoker.disableCtl("btnCall, btnAnswer,btnSave,btnTemporary");
                    _result.unAllowChangeContactSetCtl();
                },

                //接听时控件状态设置
                answerSetCtl: function () {
                    _result.callSetCtl();
                    quickInvoker.enableCtl("btnKeeping,btnForward,btnTransferAgent");
                    quickInvoker.disableCtl("btnThreeWayCalling");
                    $("#btnCall").attr("disabled", true);
                    $("#AttractTarget_PHONE_NO1").attr("disabled", true).val("");
                },

                //有电话打进来时
                ringSetCtl: function () {
                    _result.ctiCtlEnable();
                    $("#btnAnswer").removeAttr("disabled");
                    $("#btnCall").attr("disabled", true);
                },

                //挂线时控件状态设置
                hungSetCtl: function () {
                    _self.validateCanSave();
                    quickInvoker.disableCtl("btnHung,btnKeeping,btnTransferAgent,btnForward,btnThreeWayCalling,btnAnswer");
                    $("#btnKeeping").valOrHtml(textConfig.hold);
                }
            };
            return _result;
        })(),
        //判断是否可以保存
        validateCanSave: function () {
            var result = false;
            if (masterControl.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Call || _self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Answer) {
                return false;
            }

            if (!checkIsNullVal(currentAttractTarget.CALL_ID) || currentAttractTarget.F_STS_KEY == "D" || currentAttractTarget.C_STS_KEY == "D" || currentAttractTarget.S_STS_KEY == "F" || currentAttractTarget.S_STS_KEY == "P") {
                quickInvoker.enableCtl("btnTemporary,btnSave");
                result = false;
            }
            if (
				(!checkIsNullVal(currentAttractTarget.F_STS_KEY) && currentAttractTarget.F_STS_KEY != "D") ||
				(!checkIsNullVal(currentAttractTarget.C_STS_KEY) && currentAttractTarget.C_STS_KEY != "D") ||
				(!checkIsNullVal(currentAttractTarget.S_STS_KEY) && currentAttractTarget.S_STS_KEY != "F")) {
                quickInvoker.enableCtl("btnSave");
                result = true;
            }
            if (checkIsNullVal(currentAttractTarget.F_STS_KEY) && checkIsNullVal(currentAttractTarget.C_STS_KEY) && checkIsNullVal(currentAttractTarget.S_STS_KEY) && !checkIsNullVal(currentAttractTarget.CALL_ID)) {
                quickInvoker.enableCtl("btnSave");
                result = true;
            }
            return result;
        },
        reloadSelf: function (data, callback) {
            parentOpretion.showTabPage(0).hideAllPage().loadFrame("masterControlContainer", getUrl(urlConfig.MasterControl, data), callback);
        },
        //加载招揽子项列表
        reloadAttractInfo: function (url) {
            parentOpretion.showTabPage(0).loadFrame("tabAttractInfo", url);
        },
        //判断能否改变联系人
        canChangeContact: function () {
            if (checkIsNullVal(currentAttractTarget.F_STS_KEY) && checkIsNullVal(currentAttractTarget.C_STS_KEY) && checkIsNullVal(currentAttractTarget.S_STS_KEY)) {
                return true;
            }
            return false;
        },
        //解锁招揽对象
        unLockCurrentObcu: function (callback) {
            var unLockUrl = getUrl(urlConfig.UnLockAttract, { OBCU_ID: currentAttractTarget.OBCU_ID });
            $.ajax({
                url: unLockUrl,
                cache: false,
                data: {},
                success: function (json) {
                    if (json.Success) {
                        _self.clearAttractTarget();
                        callback(json);
                    } else {
                        alert(json.Message);
                    }
                }
            });
        },
        //加载电话，联系人信息
        loadAttractTargetByPhone: function (phone, resetOBCU, areaCode, contactId, custId, callBack, noAutoLoadContact, noAutoLoadPhone, selectObjectFlag, contactInfo, isRing, searchCustomerFlag) {
            phone = $.trim(phone);
            areaCode = $.trim(areaCode);
            phone = areaCode + phone;
            contactId = $.trim('' + contactId + '');
            custId = $.trim('' + custId + '');
            phone = phone.replace(/\s/g, "");
            var indexOf_ = phone.indexOf("-");
            if (indexOf_ <= 4) {
                phone = phone.replace(/-/, "");
            }
            indexOf_ = phone.indexOf("-");
            if (indexOf_ > 0) {
                phone = phone.substring(0, indexOf_);
            }

            /*if (_self.jobStatus.getCurrentStatus() != _self.enumJobStatus.Init) {
            alert(textConfig.beforeUnsavedCanNotReplace);
            return false;
            }*/

            if (!isRing && currentAttractTarget.InAttractStatus() && !selectObjectFlag) {
                alert(textConfig.attractStateCanNotUpdateContact);
                return false;
            }

            if (selectObjectFlag) {
                if (_self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Ring || _self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Call || _self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Answer) {
                    alert(textConfig.callTimeDenyOpretion);
                    return false;
                }
                $("#AttractTarget_PHONE_NO1").valOrHtml(phone);
                $("#btnCall").removeAttr("disabled");
                return false;
            }

            if (searchCustomerFlag) {
                if (!checkIsNullVal(currentAttractTarget.CALL_ID)) {
                    alert(textConfig.afterCallDenyOpretion);
                    return false;
                }
                $("#AttractTarget_PHONE_NO1").valOrHtml("");
            }

            if (!_self.canChangeContact()) {
                alert(textConfig.contactLocked);
                return false;
            }

            function setVal(data) {
                if (contactInfo) {
                    data.CUST_NAME = contactInfo.CUST_NAME;
                    data.CUST_ID = contactInfo.CUST_ID;
                    data.CUTC_ID = contactInfo.CUTC_ID;
                    data.NAME = contactInfo.CUTC_NAME;
                    data.INDENTITY = contactInfo.INDENTITY;
                    data.GC_TITLE = contactInfo.GC_TITLE;
                    data.IMPORTANT_INFO = null;
                    data.CUST_TYPE = contactInfo.CUST_TYPE;
                }
                $("#AttractTarget_CUST_NAME").valOrHtml(data.CUST_NAME);
                $("#CUTC_NAME").valOrHtml(data.NAME);
                $("#LastCall").valOrHtml(data.LAST_CALL_TIME ? data.LAST_CALL_TIME.formatDate() : "");
                try {
                    $("#CUTC_IDENTITY").valOrHtml(data.INDENTITY ? eval('cutcIdentity.' + data.INDENTITY) : "").attr("val", data.INDENTITY);
                    $("#TITLE").valOrHtml(data.GC_TITLE ? eval('title.CODE_' + data.GC_TITLE) : "").attr("val", data.GC_TITLE);
                    $("#CustomerType").valOrHtml(data.CUST_TYPE ? eval('customerType.' + data.CUST_TYPE) : "").attr("val", data.CUST_TYPE);
                }
                catch (e) { }
                $("#IMPORTANT_INFO").valOrHtml(data.IMPORTANT_INFO);
                if (_self.ctiStatus.getCurrentStatus() != _self.enumCtiStatus.Answer && _self.ctiStatus.getCurrentStatus() != _self.enumCtiStatus.Call && data.PHONE_NO != "") {
                    $("#AttractTarget_PHONE_NO1").valOrHtml(data.PHONE_NO);
                    $("#btnCall").removeAttr("disabled");
                }


                currentAttractTarget.CUST_ID = data.CUST_ID;
                currentAttractTarget.CUST_NAME = data.CUST_NAME;
                currentAttractTarget.CUTC_ID = data.CUTC_ID;
                currentAttractTarget.CUTC_NAME = data.NAME;
                currentAttractTarget.CUST_TYPE = data.CUST_TYPE;
                currentAttractTarget.CUTC_IDENTITY = data.INDENTITY;
                currentAttractTarget.TITLE = data.GC_TITLE;
                currentAttractTarget.PHONE_NO1 = data.PHONE_NO;
            }

            if (checkIsNullVal(phone) && checkIsNullVal(custId) && checkIsNullVal(contactId)) {
                var data = {};
                setVal(data);
                parentOpretion.hideAllPage();
                return false;
            }

            _selectContactSwitch = false;
            $.ajax({
                url: 'GetContact',
                data: { phone: phone, areaCode: areaCode, contactId: contactId, custId: custId, noAutoLoadContact: noAutoLoadContact, noAutoLoadPhone: noAutoLoadPhone, getAll: isRing },
                cache: false,
                success: function (data) {
                    if (data && data.Success) {
                        if (data.Result.popDialog) {
                            _selectContactSwitch = true;
                            _self.openSelectObjectDialog();
                            return;
                        }
                        setVal(data.Result);
                        if (resetOBCU) {
                            currentAttractTarget.OBCU_ID = '';

                            parentOpretion.showTabPage(0);
                        }
                        _self.ajaxLoadData();
                    }
                    else {
                        $("#AttractTarget_CUST_NAME").valOrHtml("");
                        $("#CUTC_NAME").valOrHtml("");
                        $("#LastCall").valOrHtml("");
                        $("#CUTC_IDENTITY").valOrHtml("");
                        $("#TITLE").valOrHtml("");
                        $("#CustomerType").valOrHtml("");
                        $("#IMPORTANT_INFO").valOrHtml("");
                        _self.clearAttractTarget();
                        alert(data.Message);
                    }
                    if (callBack) {
                        callBack();
                    }
                }

            });
            return true;
        },
        //加载招揽对象
        loadAttractTarget: function (cutcTarget, callBack, unLock) {
            if (_self.ctiStatus.getCurrentStatus() != _self.enumCtiStatus.Init) {
                message(textConfig.noCallAllowAction, null, false);
                return false;
            }


            var tmpFun = function (win) {
                var setCtlVal = function () {
                    win.currentAttractTarget.JOB_ID = cutcTarget.JOB_ID;
                    win.currentAttractTarget.CUST_ID = cutcTarget.CUST_ID;
                    win.currentAttractTarget.CUST_TYPE = cutcTarget.CUST_TYPE;
                    win.currentAttractTarget.CUST_NAME = cutcTarget.CUST_NAME;
                    win.currentAttractTarget.CUTC_NAME = cutcTarget.CUTC_NAME;
                    win.currentAttractTarget.TITLE = cutcTarget.TITLE;
                    win.currentAttractTarget.CUTC_ID = cutcTarget.CUTC_ID;
                    win.currentAttractTarget.CALL_ID = cutcTarget.CALL_ID; //恢复CALL
                    win.currentAttractTarget.FLP_ID = cutcTarget.FLP_ID;
                    win.currentAttractTarget.CMHD_ID = cutcTarget.CMHD_ID;
                    win.currentAttractTarget.ISURV_ID = cutcTarget.ISURV_ID;
                    win.currentAttractTarget.CALL_STATUS = cutcTarget.CALL_STATUS;
                    win.currentAttractTarget.PHONE_NO1 = cutcTarget.PHONE_NO;
                    if (cutcTarget.CALL_TYPE == "I") {
                        win.$("#txtCallFrom").valOrHtml(cutcTarget.PHONE_NO);
                        win.$("#AttractTarget_PHONE_NO1").attr("disabled", true);
                        var html = $("#INBOUND_TYPE").html();
                        $("#txtCallType").replaceWith($("<select>", { id: 'txtCallType', name: 'CALL_SUB_TYPE', html: html }));
                        $("#txtCallType").valOrHtml(cutcTarget.CALL_SUB_TYPE);
                        win.currentAttractTarget.CALL_SUB_TYPE = cutcTarget.CALL_SUB_TYPE;
                    } else {
                        win.$("#AttractTarget_PHONE_NO1").valOrHtml(cutcTarget.PHONE_NO);
                    }
                    if (!checkIsNullVal(cutcTarget.CUST_NAME))
                        win.$("#AttractTarget_CUST_NAME").valOrHtml(cutcTarget.CUST_NAME);

                    win.$("#CUTC_NAME").valOrHtml(cutcTarget.CUTC_NAME);
                    try {
                        win.$("#CustomerType").valOrHtml(cutcTarget.CUST_TYPE ? eval('customerType.' + cutcTarget.CUST_TYPE) : "").attr("val", cutcTarget.CUST_TYPE);
                        win.$("#CUTC_IDENTITY").valOrHtml(cutcTarget.CUTC_IDENTITY ? eval('cutcIdentity.' + cutcTarget.CUTC_IDENTITY) : "").attr("val", cutcTarget.CUTC_IDENTITY);
                        win.$("#TITLE").valOrHtml(cutcTarget.TITLE ? eval('title.CODE_' + cutcTarget.TITLE) : "").attr("val", cutcTarget.TITLE);
                    } catch (e) { }
                    win.$("#IMPORTANT_INFO").valOrHtml(cutcTarget.IMPORTANT_INFO);
                    win.$("#CALL_DETAIL").valOrHtml(cutcTarget.CALL_DETAIL);


                    win.$("#GC_END_RESULT").val(cutcTarget.GC_END_RESULT);
                    win.$("#GC_FAIL_REASON").val(cutcTarget.GC_FAIL_REASON);
                    win.$("#OUTBOUND_STS").val(cutcTarget.OUTBOUND_STS);


                    win.masterControl.iniCallResult(win);

                    if (!checkIsNullVal(win.$("#AttractTarget_PHONE_NO1").val())) {
                        win.$("#btnCall").removeAttr("disabled");
                    } else {
                        win.$("#btnCall").attr("disabled", true);
                    }
                    parentOpretion.showTabPage(0);
                    win.masterControl.validateCanSave();
                };
                setCtlVal();
                if (cutcTarget.CUTC_ID) {
                    win.masterControl.loadAttractTargetByPhone(null, true, null, cutcTarget.CUTC_ID, null, setCtlVal);
                } else {
                    setCtlVal();
                }
            };
            //解锁当前对象
            if (currentAttractTarget.InAttractStatus() && unLock) {
                _self.unLockCurrentObcu(function (data) {
                    if (data.Success) {
                        if (callBack) {
                            callBack();
                        }
                        _self.reloadSelf(null, function (win) {
                            tmpFun(win);
                        });
                    }
                    else {
                        message(data.Message, null, false);
                    }
                });
            }
            else {
                tmpFun(window);
                if (callBack)
                    callBack();
            }
        },

        setCtiMessage: function (msg) {
            var html = $("#ctiMessage").html();
            var msgCtl = $("#ctiMessage");
            msgCtl.html(html + '<br />[' + new Date().format('h:m:s') + ']:' + msg).scrollTop(msgCtl[0].scrollHeight);
        },

        ctiOnRing: function (ANI, callID, subID, grpID, area) {
            _self.ctiStatus.setCurrentStatus(_self.enumCtiStatus.Ring);
            $("#txtCallFrom").val(ANI);
        },

        ctiCallStateChange: function (state, hook, occupy) {
            if (state == $Enums.Status.Idle && (_self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Call || _self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Answer || _self.ctiStatus.getCurrentStatus() == _self.enumCtiStatus.Ring)) {
                $('#btnHung').click();
            }

            _self.setCtiMessage('Status:' + $Enums.Status.Name[state]);
        },
        loadJob: function (JOB_ID) {
            $.getJSON('GetJobInfo', { _t: new Date().getTime(), JOB_ID: JOB_ID }, function (json) {
                if (json.Success) {

                    var con = {
                        JOB_ID: json.Result.JOB_ID,
                        CUST_ID: json.Result.CUST_ID,
                        CUTC_ID: json.Result.CUTC_ID,
                        CALL_ID: json.Result.CALL_ID,
                        FLP_ID: json.Result.FLP_ID,
                        CMHD_ID: json.Result.CMHD_ID,
                        ISURV_ID: json.Result.ISURV_ID,
                        CUST_NAME: json.Result.CUST_NAME,
                        CUST_TYPE: json.Result.CUST_TYPE,
                        CUTC_NAME: json.Result.CUTC_NAME,
                        CUTC_IDENTITY: json.Result.CUTC_IDENTITY,
                        TITLE: json.Result.TITLE,
                        IMPORTANT_INFO: json.Result.IMPORTANT_INFO,
                        CALL_DETAIL: json.Result.CALL_DETAIL,
                        PHONE_NO: checkIsNullVal(json.Result.PHONE_NO) ? json.Result.PHONE_NO1 : json.Result.PHONE_NO,
                        OUTBOUND_STS: json.Result.OUTBOUND_STS,
                        GC_END_RESULT: json.Result.GC_END_RESULT,
                        GC_FAIL_REASON: json.Result.GC_FAIL_REASON,
                        CALL_SUB_TYPE: json.Result.CALL_SUB_TYPE,
                        CALL_TYPE: json.Result.CALL_TYPE,
                        CALL_STATUS: json.Result.CALL_STATUS,
                        LAST_CALL_TIME: json.Result.LAST_CALL_TIME
                    };


                    $.getJSON('UnLoadJob', { JOB_ID: JOB_ID }, function (unResult) {
                        if (checkIsNullVal(json.Result.OBCU_ID)) { //招揽则恢复CALL,否则恢复JOB
                            _self.loadAttractTarget(con, null, false);
                            _self.jobStatus.setCurrentStatus(_self.enumJobStatus.UnSaved);
                        } else {
                            _self.reloadSelf({
                                getAttract: true,
                                callID: json.Result.CALL_ID,
                                OBCU_ID: json.Result.OBCU_ID,
                                JOB_ID: json.Result.JOB_ID
                            }, function (win) {
                                win.currentAttractTarget.JOB_ID = json.Result.JOB_ID;
                            });
                        }
                    });
                } else {
                    message(json.Message, null, false);
                }
            });
        },
        //检查是否有异常的招揽
        checkExceptionAttract: function (callIfNoException, noConfirm) {
            $.getJSON(urlConfig.CheckExcepiton, { _t: new Date().getTime() }, function (data) {
                if (data.Success) {
                    if (data.Result.HasException) {
                        /*if (confirm(textConfig.lastException)) {
                        if (!checkIsNullVal(data.Result.JOB_ID)) {
                        _self.loadJob(data.Result.JOB_ID);
                        } else {
                        _self.reloadSelf({
                        getAttract: true,
                        OBCU_ID: data.Result.OBCU_ID,
                        callID: data.Result.CALL_ID,
                        UnLock: true
                        });
                        }

                        }*/

                        var tmpFun = function () {
                            _self.loadJob(data.Result.JOB_ID);
                            /*if (checkIsNullVal(data.Result.OBCU_ID)) {
                            _self.loadJob(data.Result.JOB_ID);
                            } else {
                            _self.reloadSelf({
                            getAttract: true,
                            OBCU_ID: data.Result.OBCU_ID,
                            callID: data.Result.CALL_ID,
                            UnLock: true
                            }, function (win) {
                            win.currentAttractTarget.JOB_ID = data.Result.JOB_ID;
                            });
                            }*/
                        };

                        if (!noConfirm) {
                            showConfirm(null, textConfig.attractExceptionFormat.replace("{0}", data.Result.JOB_CODE), null, tmpFun, callIfNoException);
                        } else {
                            message(textConfig.lastExceptionNoConfirm, tmpFun, true);
                        }
                    } else {
                        if (callIfNoException) {
                            callIfNoException();
                        }
                    }
                }
            });
        },

        ajaxLoadData: function () {
            getMemberNumber();
            //加载跟进,投诉，问卷状态
            $.ajax({
                url: getUrl(urlConfig.GetStatus, {
                    F_ID: currentAttractTarget.FLP_ID,
                    S_ID: currentAttractTarget.ISURV_ID,
                    C_ID: currentAttractTarget.CMHD_ID
                }),
                cache: false,
                data: { custId: currentAttractTarget.CUST_ID },
                success: function (data) {
                    if (data.Success) {
                        currentAttractTarget.C_STS_KEY = data.Result.C_STS_KEY;
                        currentAttractTarget.F_STS_KEY = data.Result.F_STS_KEY;
                        currentAttractTarget.S_STS_KEY = data.Result.S_STS_KEY;
                        $("#lblFSts").html(data.Result.F_STS);
                        $("#lblSSts").html(data.Result.S_STS);
                        $("#lblCSts").html(data.Result.C_STS);
                        _self.validateCanSave();
                    }
                }
            });


            //加载跟进项目个数
            $.ajax({
                url: urlConfig.GetFollowUpCount,
                cache: false,
                data: {},
                success: function (data) {
                    if (data.Success) {
                        $("#lblFollowUpCount").html(data.Result);
                    }
                }
            });

            //加载逾期投诉个数
            $.ajax({
                url: urlConfig.GetLateCount,
                cache: false,
                data: {},
                success: function (data) {
                    if (data.Success) {
                        $("#lblLateComplaintCount").html(data.Result);
                    }
                }
            });
        },
        TemporaryJob: function (successCall, autoSave, failCall) {
            if (_temporaryRing) {
                if (failCall) {
                    failCall();
                }
                if (!autoSave) {
                    alert("有暂存任务正在进行，请稍候重试");
                }
                return false;
            }
            _temporaryRing = true;
            if (checkIsNullVal(currentAttractTarget.CALL_ID) && checkIsNullVal(currentAttractTarget.CUTC_NAME)) {
                if (!autoSave) {
                    alert("没有Call或没有联系人信息不能暂存");
                }
                if (failCall) {
                    failCall();
                }
                return false;
            }
            var options = {
                url: 'TemporarilyStoredJob',
                data: {
                    autoSave: autoSave,
                    JOB_ID: currentAttractTarget.JOB_ID,
                    CALL_ID: currentAttractTarget.CALL_ID,
                    OBCU_ID: currentAttractTarget.OBCU_ID,
                    CUTC_NAME: currentAttractTarget.CUTC_NAME, //$("#CUTC_NAME").valOrHtml(),
                    CUTC_IDENTITY: currentAttractTarget.CUTC_IDENTITY, //$("#CUTC_IDENTITY").valOrHtml(),
                    TITLE: currentAttractTarget.TITLE, //$("#TITLE").valOrHtml(), //GC_TITLE
                    CUST_TYPE: currentAttractTarget.CUST_TYPE, //$("#CustomerType").valOrHtml(),
                    CUST_NAME: currentAttractTarget.CUST_NAME, //$("#AttractTarget_CUST_NAME").valOrHtml(),
                    CUST_ID: currentAttractTarget.CUST_ID,
                    CUTC_ID: currentAttractTarget.CUTC_ID,
                    FLP_ID: currentAttractTarget.FLP_ID,
                    CMHD_ID: currentAttractTarget.CMHD_ID,
                    ISURV_ID: currentAttractTarget.ISURV_ID,
                    CALL_SUB_TYPE: currentAttractTarget.CALL_SUB_TYPE
                },
                success: function (data) {
                    _self.jobStatus.setCurrentStatus(_self.enumJobStatus.Saved);
                    currentAttractTarget.JOB_ID = data.Result.JOB_ID;
                    
                    if (successCall) {
                        successCall(data);
                    }
                },
                complete: function () {
                    _temporaryRing = false;
                }
            };
            $("#CALL_SUB_TYPE").val(currentAttractTarget.CALL_SUB_TYPE);


            $("#formCallRemark").ajaxSubmit(options);
        },
        openSelectObjectDialog: function () {
            var phone = $("#txtCallFrom").val();
            if ($.trim(phone) == "") {
                phone = $("#AttractTarget_PHONE_NO1").val();
            }
            popDialog(urlConfig.WPListener + '?SearchCustomerForListen.PhoneNo=' + phone, 'selectAttractObject', 1000, 600, textConfig.selectObjectText);
        },
        iniCallResult: function (win) {
            if (win.$("#OUTBOUND_STS").val() == "") {
                return false;
            }
            win.$("#CallComplete").val(win.$("#GC_END_RESULT").val());
            win.$("#CallFail").val(win.$("#GC_FAIL_REASON").val());
            win.$("input[name='callStatus']").filter(function (i) {
                var sts = win.$("#OUTBOUND_STS").val();
                if (sts == "")
                    return true;
                return win.$(this).val() == sts;
            }).click().attr("checked", true);
        },
        run: function () {
            parentOpretion.injection("setISURVID", _self.setISURVID);
            parentOpretion.injection("setCMHDID", _self.setCMHDID);
            parentOpretion.injection("controlledStatus", _self.controlledStatus);
            parentOpretion.injection("currentAttractTarget", currentAttractTarget);
            parentOpretion.injection("loadAttractTargetByPhone", _self.loadAttractTargetByPhone);
            parentOpretion.injection("setSwithCallStatus", _self.setSwithCallStatus);
            parentOpretion.injection("masterControl", _self);

            _self.reloadAttractInfo(getUrl(urlConfig.AttractInfo, { OBCU_ID: currentAttractTarget.OBCU_ID, CustomerID: currentAttractTarget.CUST_ID, ContactID: currentAttractTarget.CUTC_ID }));
            _self.ajaxLoadData();
            _self.iniCallResult(window);
            _self.ctiStatus.setCurrentStatus(_self.enumCtiStatus.Init);
            quickInvoker.catchDo(function () {
                parentOpretion.initCti();
                cti = parentOpretion.getCti();
                var settings = cti.settings;

                settings.findCtl = function (exp) {
                    return $(document).find(exp);
                };
                settings.onAgtReqReturn = function (msg, cmdItem) {
                    if ($Enums.AgtReqItem.reqWorkAftCallOver == cmdItem) {
                        $('#btnHung').click();
                    }
                    _self.setCtiMessage(msg);

                };
                settings.getTelNumber = function () {
                    return $("#AttractTarget_PHONE_NO1").val();
                };
                settings.onRing = _self.ctiOnRing;
                settings.onAgtState = _self.ctiCallStateChange;
            }, function (e) {
                //alert(e);
            });
            $("#txtMode").valOrHtml(parentOpretion.getCtiMode().getCurrentMode().Text);


        }

    };
    return _self;
})();





	