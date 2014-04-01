/*
*MasterControl绑定事件
*依赖于jQuery 
*by xh 2012
*/
/// <reference path="parentOpretion.js" />
/// <reference path="quickInvoker.js" />
/// <reference path="MasterControl.js" />
$(function () {
    masterControl.run();

    $("input[readonly]").each(function (i) {
        var $this = $(this);
        var target = $('<label>', { Class: "label_readonly " + $this.attr("class"), html: $this.val(), style: "min-width:" + $this.css("width"), id: $this.attr("id") });

        if (!$this.hasClass("noreplace")) {
            $this.replaceWith(target);
        }
    });

    $("#btnSelectObject").click(function () {
        if (checkIsNullVal(currentAttractTarget.CALL_ID)) {
            alert(textConfig.noCallDenyAction);
            return false;
        }
        masterControl.openSelectObjectDialog();
    });

    $("#controlledByAttractAtrget").click(function () {
        if (!masterControl.controlledStatus.getControlledByContact().isControlled) {
            return false;
        }
        var _this = $(this);
        var defaultContact = $.trim($(this).valOrHtml());
        var htmlObj = $('<div>', { html: '<input type="text" name="contact" value="' + defaultContact + '"/><input type="hidden" name="defaultContact" value="' + defaultContact + '"/>' });
        popContentDialog(htmlObj, 200, 80, textConfig.inputCutcNameTitle, [
            {
                text: textConfig.currentContactText,
                onclick: function (dlg) {
                    currentAttractTarget.CurrentTargetName = currentAttractTarget.CUTC_NAME;
                    _this.valOrHtml(currentAttractTarget.CurrentTargetName);
                    masterControl.controlledStatus.setControlledByContact(null, currentAttractTarget.CurrentTargetName);
                    dlg.close();
                }
            },
            {
                text: textConfig.okText,
                onclick: function (dlg) {
                    currentAttractTarget.CurrentTargetName = htmlObj.find("input[name='contact']").val();
                    _this.valOrHtml(currentAttractTarget.CurrentTargetName);
                    masterControl.controlledStatus.setControlledByContact(null, currentAttractTarget.CurrentTargetName);
                    dlg.close();
                }
            },
            {
                text: textConfig.cancelText,
                onclick: function (dlg) {
                    dlg.close();
                }
            }
        ], null, null);
    });

    //接听电话号码
    $("#txtCallFrom").keyup(function (e) {
        if (e.keyCode == 37 || e.keyCode == 39) {
            return;
        }
        var callFrom = $(this).val();
        if (callFrom != "") {
            quickInvoker.catchDo(function () {
                masterControl.ctiOnRing(callFrom); //test;
            });
            $("#btnAnswer").removeAttr("disabled");
        }
        else
            $("#btnAnswer").attr("disabled", true);
    }).keyup();

    //拨出电话码号
    $("#AttractTarget_PHONE_NO1").keyup(function () {
        if ($(this).val() != "")
            $("#btnCall").removeAttr("disabled");
        else
            $("#btnCall").attr("disabled", true);
    }).keyup();


    //受控车辆
    $("#controlledByVeh").click(function () {
        if ($(this).val() == "") {
            message(textConfig.useLocaionFrist, null, false);
            return false;
        }
        if ($(this).attr("checked")) {
            masterControl.controlledStatus.setControlledByVeh($(this).val());
        }
        else {
            masterControl.controlledStatus.clearControlledByVeh();
        }
    });

    //受控联系人
    $("#controlledByAttractTarget").click(function () {
        /*if ($(this).val() == "") {
        message(textConfig.noAttractNoUseFunctionMessage, null, false);
        return false;
        }*/
        if ($(this).attr("checked")) {
            $("#controlledByAttractAtrget").valOrHtml(currentAttractTarget.CUTC_NAME);
            masterControl.controlledStatus.setControlledByContact($(this).val(), $("#controlledByAttractAtrget").valOrHtml());
        }
        else {
            masterControl.controlledStatus.clearControlledByContact();
        }
    });



    //拨出完成或失败
    $("input[name='callStatus']").click(function () {
        var $this = $(this);
        if (!currentAttractTarget.SwithCallStatus && $this.attr("id") != "callStatusComplete") {
            alert(textConfig.calledDenyChangeStatus);
            $("#callStatusComplete").attr("checked", true);
            $("#callStatusFail").attr("disabled", true);
            $("#CallFail").attr("disabled", true);
            return false;
        }


        currentAttractTarget.CallStatus = $this.val();
        $("input[name='callStatus']").not($this).siblings("select").attr("disabled", true).find("option").eq(0).attr("selected", true);
        $this.siblings("select").removeAttr("disabled");
        $("#OUTBOUND_STS").val($this.val());
    });

    $("#CallComplete").click(function () {
        $("#callStatusComplete").click();
    });

    $("#CallFail").click(function () {
        $("#callStatusFail").click();
    });

    $("#CallComplete").change(function () {
        $("#GC_END_RESULT").val($(this).val());
    });

    $("#CallFail").change(function () {
        $("#GC_FAIL_REASON").val($(this).val());
    });


    $("#PHONE_NO1").focus();

    if (checkIsNullVal(currentAttractTarget.OBCU_ID)) {
        //$("#btnUnlockAttractObject").attr("disabled", "disabled");
    }

    $(":radio[name='radioCallRemarkType']").click(function () {
        $(".radioCallRemarkType").hide();
        $("#" + $(this).val()).show();
    }).filter(":checked").click();

    //搜索客户
    $("#btnSearchCustomer").click(function () {
        popDialog(urlConfig.SearchCustomer, 'SearchCustomer', 1280, 700, textConfig.searchCustomer);
    });

    //搜索车队
    $("#btnSearchFleet").click(function () {
        popDialog(urlConfig.SearchFleet, 'SearchFleet', 1280, 700, textConfig.searchFleet);
    });

    //恢复
    $("#btnResume").click(function () {
        masterControl.checkExceptionAttract(function () {
            popDialog('WPAttract/HaveBeenTemporaryCall', 'HaveBeenTemporaryCall', 1000, 600, textConfig.HaveBeenTemporaryCallText, [{ text: textConfig.selectObjectText, onclick: function (div, win, dlg) {
                if (masterControl.ctiStatus.getCurrentStatus() != masterControl.enumCtiStatus.Init) {
                    message(textConfig.noCallAllowAction, null, false);
                    return false;
                }
                var selectedCbs = $(win.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
                if (selectedCbs.length != 1) {
                    message($g["SelectSingle"], null, false);
                    return false;
                }
                var obcuID = selectedCbs.attr("OBCU_ID");
                if (!checkIsNullVal(obcuID)) {
                    masterControl.reloadSelf({
                        getAttract: true,
                        CallID: selectedCbs.attr("CALL_ID"),
                        JOB_ID: selectedCbs.attr("JOB_ID")
                    });
                } else {
                    masterControl.loadJob(selectedCbs.attr("JOB_ID"));
                }
                dlg.close();
            }
            }, { text: textConfig.cancelText, onclick: function (div, win, dlg) {
                dlg.close();
            }
            }]
                    );
        }, true);
    });

    //招揽对象
    $("#btnAttractObject").click(function () {
        masterControl.checkExceptionAttract(function () {
            popDialog('WPAttract/AttractObjectsGrid', 'AttractObjectsGrid', 900, 500, textConfig.AttractTargetText, [{ text: textConfig.selectObjectText, onclick: function (div, win, dlg) {
                if (masterControl.ctiStatus.getCurrentStatus() != masterControl.enumCtiStatus.Init) {
                    message(textConfig.noCallAllowAction, null, false);
                    return false;
                }
                var selectedCbs = $(win.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
                if (selectedCbs.length != 1) {
                    message($g["SelectSingle"], null, false);
                    return false;
                }
                masterControl.reloadSelf({
                    getAttract: true,
                    OBCU_ID: selectedCbs.val()
                });
                dlg.close();
            }
            }, { text: textConfig.cancelText, onclick: function (div, win, dlg) {
                dlg.close();
            }
            }]
                    );
        }, true);
    });
    //招揽处理
    $("#btnAttractProc").click(function () {
        var jumpSeq = currentAttractTarget.SEQ;
        var fun = function () {
            masterControl.checkExceptionAttract(function () {
                if (masterControl.ctiStatus.getCurrentStatus() != masterControl.enumCtiStatus.Init) {
                    message(textConfig.noCallAllowAction, null, false);
                    return false;
                }
                masterControl.reloadSelf({ getAttract: true, jumpSeq: jumpSeq });
            }, true);
        };
        if (currentAttractTarget.InAttractStatus()) {
            masterControl.unLockCurrentObcu(fun);
        } else {
            fun();
        }
    });

    //跟进
    $("#btnFollowUp").click(function () {
        if (checkIsNullVal(currentAttractTarget.CALL_ID)) {
            alert(textConfig.calllingAllowOpretion);
            return false;
        }
        if (checkIsNullVal(currentAttractTarget.CUTC_NAME)) {
            alert(textConfig.attractTargetIsNull);
            return false;
        }
        currentAttractTarget.PHONE_NO1 = $("#AttractTarget_PHONE_NO1").val();
        if (checkIsNullVal(currentAttractTarget.PHONE_NO1))
            currentAttractTarget.PHONE_NO1 = $("#txtCallFrom").val();
        popDialog(getUrl(urlConfig.FollowCreate
                , {
                    hideButtons: true
                    , FLP_PERSON: currentAttractTarget.CUTC_NAME
                    , FLP_ID: currentAttractTarget.FLP_ID
                    , CALL_ID: currentAttractTarget.JOB_ID
                    , CUST_ID: currentAttractTarget.CUST_ID
                    , CUST_NAME: currentAttractTarget.CUST_NAME
                    , GC_TITLE: currentAttractTarget.TITLE
                    , CUTC_ID: currentAttractTarget.CUTC_ID
                    , CUTC_IDENTITY: currentAttractTarget.CUTC_IDENTITY
                    , FLP_PHONE: currentAttractTarget.PHONE_NO1
                    , inWorkplace: true
                })
                , 'WPFollowUp'
                , 700
                , 450
                , textConfig.followUp, [{ text: textConfig.saveText, onclick: function (div, win, dlg) {
                    $(win.document).find("form").ajaxSubmit({
                        beforeSubmit: function (arr, $form, options) {
                            var validateObj = Sys.Mvc.FormContext.getValidationForForm($form[0]);
                            if (validateObj && validateObj.validate().length > 0) {
                                return true;
                            }
                        },
                        success: function (data) {
                            if (data.Success) {
                                currentAttractTarget.FLP_ID = data.Result.FLP_ID;
                                currentAttractTarget.F_STS_KEY = data.Result.FLP_STS;
                                //暂存
                                masterControl.TemporaryJob(null, true);
                                masterControl.setSwithCallStatus(false);
                                masterControl.ajaxLoadData();

                            }
                            message(data.Message, function () { dlg.close(); }, data.Success);
                        }
                    });
                }
                },
        /*{ text: textConfig.complete, onclick: function (div, win, dlg) {
        if (checkIsNullVal(currentAttractTarget.FLP_ID)) {
        return false;
        }
        var dDiv = getDeleteDiv();
        loadFrame(dDiv, urlConfig.FollowUpComplete + '?FLP_ID=' + currentAttractTarget.FLP_ID, true, null, function () {
        masterControl.ajaxLoadData();
        });
        masterControl.setSwithCallStatus(false);
        dlg.close();
        }
        },*/
                {text: textConfig.cancelFollowUp, onclick: function (div, win, dlg) {
                    $(win.document).find("form").ajaxSubmit({
                        data: {
                            ENFORCE_FLP_STS: 'R'
                        },
                        beforeSubmit: function (arr, $form, options) {
                            var validateObj = Sys.Mvc.FormContext.getValidationForForm($form[0]);
                            if (validateObj && validateObj.validate().length > 0) {
                                return true;
                            }
                        },
                        success: function (data) {
                            if (data.Success) {
                                currentAttractTarget.FLP_ID = data.Result.FLP_ID;
                                currentAttractTarget.F_STS_KEY = data.Result.FLP_STS;
                                //暂存
                                masterControl.TemporaryJob(null, true);
                                masterControl.setSwithCallStatus(false);
                                masterControl.ajaxLoadData();

                            }
                            message(data.Message, function () { dlg.close(); }, data.Success);
                        }
                    });
                }
            },
				 { text: textConfig.returnText, onclick: function (div, win, dlg) {
				     dlg.close();
				 }
				 }]);
    });

    //问卷
    $("#btnSurvey").click(function () {
        if (checkIsNullVal(currentAttractTarget.CALL_ID)) {
            alert(textConfig.calllingAllowOpretion);
            return false;
        }
        if (checkIsNullVal(currentAttractTarget.CUTC_NAME)) {
            alert(textConfig.attractTargetIsNull);
            return false;
        }
        var url = getUrl(urlConfig.SurveyInterface, {
            runInWorkplace: 'true',
            CALL_ID: currentAttractTarget.CALL_ID,
            CUST_ID: currentAttractTarget.CUST_ID,
            CUST_TYPE: currentAttractTarget.CUST_TYPE,
            CUST_NAME: currentAttractTarget.CUST_NAME,
            CUTC_NAME: currentAttractTarget.CUTC_NAME,
            ISURV_ID: currentAttractTarget.ISURV_ID,
            CUTC_ID: currentAttractTarget.CUTC_ID,
            CUTC_IDENTITY: currentAttractTarget.CUTC_IDENTITY,
            GC_TITLE: currentAttractTarget.TITLE,
            JOB_ID: currentAttractTarget.JOB_ID
        });

        popDialog(url, 'SurveyDialog', 1000, 708, textConfig.SurveyText, null, null, null, function (div) {
            try {
                var btnPause = $(div.find("iframe")[0].contentWindow.document).find("#btnPause");
                if (!btnPause.length)
                    return true;
                btnPause.click();
                return false;
            }
            catch (e) {
                return true;
            }
        });
    });

    //投诉
    $("#btnComplaint").click(function () {
        if (checkIsNullVal(currentAttractTarget.CALL_ID)) {
            alert(textConfig.calllingAllowOpretion);
            return false;
        }
        if (checkIsNullVal(currentAttractTarget.CUTC_NAME)) {
            alert(textConfig.attractTargetIsNull);
            return false;
        }

        currentAttractTarget.PHONE_NO1 = $("#AttractTarget_PHONE_NO1").val();
        if (checkIsNullVal(currentAttractTarget.PHONE_NO1))
            currentAttractTarget.PHONE_NO1 = $("#txtCallFrom").val();


        if (!checkIsNullVal(currentAttractTarget.CUST_ID)) {

            popDialog(
			getUrl(urlConfig.WPComplaint,
			{ "ComplaintHDR.CustID": currentAttractTarget.CUST_ID,
			    "ComplaintHDR.IsValidate": "N",
			    "ComplaintHDR.CmhdID": currentAttractTarget.CMHD_ID,
			    "ComplaintHDR.CmtcPhone": currentAttractTarget.PHONE_NO1, //getAllWinElementById("AttractTarget_PHONE_NO1").val(),
			    "ComplaintHDR.ComplainerName": currentAttractTarget.CUTC_NAME,
			    "ComplaintHDR.GcTitle": currentAttractTarget.TITLE,
			    "ComplaintHDR.JobID": currentAttractTarget.JOB_ID,
			    "level": "C"
			}),
			 'Complaint', 1400, 600, $g["ObjectComplaint"], null, null, function () {
			     masterControl.ajaxLoadData();
			 });
        } else {
            popDialog(
			getUrl(urlConfig.WPComplaint, {
			    "ComplaintHDR.CustomerName": currentAttractTarget.CUST_NAME,
			    "ComplaintHDR.CustType": currentAttractTarget.CUST_TYPE,
			    "ComplaintHDR.CmtcName": currentAttractTarget.CUTC_NAME,
			    "ComplaintHDR.CmtcTitle": currentAttractTarget.TITLE,
			    "ComplaintHDR.CmtcPhone": currentAttractTarget.PHONE_NO1,
			    "ComplaintHDR.ComplainerName": currentAttractTarget.CUTC_NAME,
			    "ComplaintHDR.GcTitle": currentAttractTarget.TITLE,
			    "ComplaintHDR.JobID": currentAttractTarget.JOB_ID,
			    "level": "C"
			}),
			 'Complaint', 1200, 600, $g["ObjectComplaint"], null, null, function () {
			     masterControl.ajaxLoadData();
			 });
        }
    });


    //跟进处理
    $("#btnFollowUpProc").click(function () {
        popDialog(urlConfig.WPFollowUp, 'FollowUpGrid', 1200, 600, textConfig.followUpProc, [
                { text: textConfig.followUpContact, onclick: function (div, win, dlg) {
                    var cbs = win.$(":checkbox:checked:not(:disabled)[name='cbKey']");
                    if (cbs.length != 1) {
                        message($g["SelectSingle"], null, false);
                        return;
                    }
                    var cutcTarget = {
                        CUTC_ID: cbs.attr("CUTC_ID")
                        , CUST_NAME: cbs.attr("CUST_NAME")
                        , CUTC_NAME: cbs.attr("CUTC_NAME")
                        , PHONE_NO: cbs.attr("PHONE_NO")
                        , CUST_TYPE: cbs.attr("CUST_TYPE")
                        , TITLE: cbs.attr("TITLE")
                        , CUTC_IDENTITY: cbs.attr("CUTC_IDENTITY")
                        , CUST_ID: cbs.attr("CUST_ID")
                    };

                    masterControl.loadAttractTarget(cutcTarget, function () { dlg.close(); }, true);
                }
                },
                    { text: textConfig.complete, onclick: function (div, win, dlg) {
                        var cbs = win.$(":checkbox:checked:not(:disabled)[name='cbKey']");
                        if (cbs.length != 1) {
                            message($g["SelectSingle"], null, false);
                            return;
                        }
                        var dDiv = getDeleteDiv();
                        loadFrame(dDiv, urlConfig.FollowUpComplete + '?FLP_ID=' + cbs.val(), true, null, function () {
                            masterControl.ajaxLoadData();
                        });
                        masterControl.setSwithCallStatus(false);
                    }
                    },
                    { text: textConfig.cancelFollowUp, onclick: function (div, win, dlg) {
                        var cbs = win.$(":checkbox:checked:not(:disabled)[name='cbKey']");
                        if (cbs.length != 1) {
                            message($g["SelectSingle"], null, false);
                            return;
                        }
                        var dDiv = getDeleteDiv();
                        loadFrame(dDiv, urlConfig.FollowUpDeny + '?FLP_ID=' + cbs.val(), true, null, function () {
                            masterControl.ajaxLoadData();
                        });
                        masterControl.setSwithCallStatus(false);
                    }
                    },
                    { text: textConfig.returnText, onclick: function (div, win, dlg) {
                        dlg.close();
                    }
                    }]);
    });

    //逾期投诉
    $("#btnDealWithComplaint").click(function () {
        popDialog(getUrl(urlConfig.DealWithComplaintList, { "SearchLate.IsLate": "Y" }), 'DealWithComplaint', 1300, 600, textConfig.complaintProc, [{
            text: textConfig.statrtContact, onclick: function (div, win, dlg) {
                var cbs = $(win.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
                if (cbs.length != 1) {
                    message($g["SelectSingle"], null, false);
                    return;
                }
                var cutcTarget = {
                    CUTC_NAME: cbs.attr("CUTC_NAME"),
                    PHONE_NO: cbs.attr("PHONE_NO"),
                    CUST_ID: cbs.attr("CUST_ID"),
                    CUST_TYPE: cbs.attr("CUST_TYPE"),
                    CUST_NAME: cbs.attr("CUST_NAME"),
                    TITLE: cbs.attr("GC_TITLE")
                };

                masterControl.loadAttractTarget(cutcTarget, function () { dlg.close(); }, true);
                dlg.close();
            }
        }, { text: textConfig.complaintProc, onclick: function (div, win, dlg) {
            var cbs = $(win.document).find(":checkbox:checked:not(:disabled)[name='cbKey']");
            if (cbs.length != 1) {
                message($g["SelectSingle"], null, false);
                return;
            }
            popDialog(urlConfig.WPComplaint + '?ComplaintHDR.CmhdID=' + cbs.val() + '&ComplaintHDR.IsValidate=Y', 'Complaint', 1350, 600, $g["Complaint"]);
            dlg.close();
        }
        }]);
    });


    //暂存
    $("#btnTemporary").click(function () {
        var $this = $(this);
        $this.attr("disabled", "disabled");
        masterControl.TemporaryJob(function (result) {
            $this.removeAttr("disabled");
            message(result.Message, function () {
                if (result.Success) {
                    masterControl.clearAttractTarget();

                    masterControl.reloadSelf(null, function (win) {
                        if (masterControl.ctiStatus.getCurrentStatus() == masterControl.enumCtiStatus.Ring) {
                            var callFrom = $("#txtCallFrom").val();
                            win.$("#txtCallFrom").val(callFrom);
                            win.masterControl.ctiOnRing(callFrom);
                        }
                    });
                }
            }, result.Success);
        });
    }, false, function () {
        $this.removeAttr("disabled");
    });

    //解锁
    $("#btnUnlockAttractObject").click(function () {
        if (masterControl.ctiStatus.getCurrentStatus() != masterControl.enumCtiStatus.Init) {
            message(textConfig.noCallAllowAction, null, false);
            return false;
        }
        var $this = $(this);
        $this.attr("disabled", "disabled");
        if (currentAttractTarget.InAttractStatus()) {
            masterControl.unLockCurrentObcu(function (data) {
                message(data.Message, function () {
                    $this.removeAttr("disabled");
                    masterControl.reloadSelf();
                }, data.Success);
            });
        } else {
            masterControl.reloadSelf();
        }
    });

    //保存
    $("#btnSave").click(function () {
        if (!masterControl.validateCanSave()) {
            alert(textConfig.denySave);
            return false;
        }
        if ($("#CallComplete").val() == "" && $("#CallFail").val() == "") {
            alert(textConfig.contactResultRequre);
            return false;
        }


        if (checkIsNullVal(currentAttractTarget.CUTC_NAME) || checkIsNullVal(currentAttractTarget.CUST_NAME) || checkIsNullVal(currentAttractTarget.CUST_TYPE)) {
            alert(textConfig.contactInfoRequred);
            return false;
        }

        if (currentAttractTarget.CALL_TYPE == "I" && $("#txtCallType").val() == "") {
            alert(textConfig.callTypeRequred);
            return false;
        }

        var callSubType = currentAttractTarget.CALL_TYPE == "I" ? $("#txtCallType").val() : (currentAttractTarget.InAttractStatus() ? "O" : "C");
        $("#CALL_SUB_TYPE").val(callSubType);
        var postData = {
            CALL_SUB_TYPE: callSubType,
            CALL_ID: currentAttractTarget.CALL_ID,
            OBCU_ID: currentAttractTarget.OBCU_ID,
            CUTC_NAME: $("#CUTC_NAME").valOrHtml(),
            CUTC_IDENTITY: currentAttractTarget.CUTC_IDENTITY,
            GC_TITLE: currentAttractTarget.TITLE,
            CUST_TYPE: customerType.GetKeyByValue($("#CustomerType").valOrHtml()),
            CUST_NAME: $("#AttractTarget_CUST_NAME").valOrHtml(),
            JOB_ID: currentAttractTarget.JOB_ID
        };



        if (currentAttractTarget.CALL_ID <= 0) {
            postData = { JOB_ID: currentAttractTarget.JOB_ID, unSaveCall: true };
        }
        var options = {
            url: urlConfig.SaveCall,
            data: postData,
            success: function (result) {
                message(result.Message, function () {
                    if (result.Success) {
                        masterControl.clearAttractTarget();
                        masterControl.jobStatus.setCurrentStatus(masterControl.enumJobStatus.Saved);
                        masterControl.reloadSelf(null, function (win) {
                            if (masterControl.ctiStatus.getCurrentStatus() == masterControl.enumCtiStatus.Ring) {
                                var callFrom = $("#txtCallFrom").val();
                                win.$("#txtCallFrom").val(callFrom);
                                win.masterControl.ctiOnRing(callFrom);
                            }
                        });
                    }
                }, result.Success);
            }
        };
        masterControl.TemporaryJob(function () {
            $("#formCallRemark").ajaxSubmit(options);
        }, false);
    });

    //模式切换
    $("#btnMode").click(function () {
        parentOpretion.getCtiMode();
        parentOpretion.getCtiMode().Switching();
        $("#btnMode").valOrHtml(parentOpretion.getCtiMode().getCurrentMode().Text);
    });


    //外拨电话
    $('#btnCall').click(function () {
        currentAttractTarget.CALL_TYPE = "O";
        if (masterControl.ctiStatus.getCurrentStatus() == masterControl.enumCtiStatus.Call || masterControl.ctiStatus.getCurrentStatus() == masterControl.enumCtiStatus.Answer) {
            alert(textConfig.callingDenyCallOut);
            return false;
        }
        if (currentAttractTarget.CALL_STATUS == "E") {
            currentAttractTarget.CALL_ID = null;
        }

        var callPhone = $("#AttractTarget_PHONE_NO1").val();
        currentAttractTarget.PHONE_NO1 = callPhone;
        if (checkIsNullVal(currentAttractTarget.CUTC_NAME) && masterControl.currentPhoneContact.lastCallPhone != callPhone && !currentAttractTarget.InAttractStatus()) {
            var lResult = masterControl.loadAttractTargetByPhone(callPhone, true, null, null, null, function () {
                masterControl.TemporaryJob(null, true);
            }, null, null, null, null, true);
            if (!lResult) {
                $("#AttractTarget_PHONE_NO1").val(masterControl.currentPhoneContact.lastCallPhone);
                return false;
            }
            masterControl.currentPhoneContact.lastCallPhone = callPhone;
        }

        quickInvoker.catchDo(function () {
            cti.dial(callPhone);
        });



        masterControl.ctiStatus.setCurrentStatus(masterControl.enumCtiStatus.Call);
        $("#txtCallFrom").val("");
        var $this = $(this);
        quickInvoker.stopwatchStart();
        if (currentAttractTarget.InAttractStatus()) {
            $("#txtCallType").valOrHtml(textConfig.codeOdsc);
            currentAttractTarget.CALL_SUB_TYPE = "O";
        } else {
            $("#txtCallType").valOrHtml(textConfig.codeCdsc);
            currentAttractTarget.CALL_SUB_TYPE = "C";
        }



        $this.attr("disabled", "disabled");
        /*if (currentAttractTarget.CALL_ID > 0) {
        $("#txtHungTime").valOrHtml(new Date().format("hh:mm:ss"));
        $("#txtCallTime").valOrHtml("00:00:00");
        $this.removeAttr("disabled");
        return;
        }*/


        $.ajax({
            url: urlConfig.Call,
            cache: false,
            data: {
                CALL_ID: currentAttractTarget.CALL_ID,
                OBCU_ID: currentAttractTarget.OBCU_ID,
                CUST_ID: currentAttractTarget.CUST_ID,
                CUST_NAME: currentAttractTarget.CUST_NAME,
                CUTC_IDENTITY: currentAttractTarget.CUTC_IDENTITY,
                GC_TITLE: currentAttractTarget.TITLE,
                CUTC_ID: currentAttractTarget.CUTC_ID,
                PHONE_NO: $("#AttractTarget_PHONE_NO1").val(),
                CUTC_NAME: $("#CUTC_NAME").valOrHtml(),
                CUST_TYPE: customerType.GetKeyByValue($("#CustomerType").valOrHtml())
            },
            success: function (data) {
                if (data.Success) {
                    currentAttractTarget.CALL_ID = data.Result.CallID;
                    $("#txtHungTime").valOrHtml(data.Result.CallStartTime.formatDate("hh:mm:ss"));
                    $("#txtCallTime").valOrHtml("00:00:00");
                    $this.removeAttr("disabled");
                    //暂存

                    masterControl.TemporaryJob(null, true);
                }
                else {
                    message(data.Message, null, false);
                }
            }
        });
    });

    //挂线
    $('#btnHung').click(function () {
        /*if ($("#btnKeeping").valOrHtml() == textConfig.pullback) {
        alert(textConfig.doPullbackFrist);
        return false;
        }*/
        quickInvoker.catchDo(function () {
            cti.hung();
        });


        masterControl.TemporaryJob(null, true);

        var $this = $(this);
        quickInvoker.stopwatchStop();
        if (masterControl.ctiStatus.getCurrentStatus() == masterControl.enumCtiStatus.Ring) {
            $("#txtCallFrom").val("");
            return;
        }

        masterControl.jobStatus.setCurrentStatus(masterControl.enumJobStatus.UnSaved);
        masterControl.ctiStatus.setCurrentStatus(masterControl.enumCtiStatus.Hung);

        if (checkIsNullVal(currentAttractTarget.CALL_ID)) {
            return;
        }

        $this.attr("disabled", "disabled");
        $.ajax({
            url: urlConfig.Hung,
            cache: false,
            data: { callID: currentAttractTarget.CALL_ID },
            success: function (data) {
                if (data.Success) {
                    $("#txtHungTime").valOrHtml(data.Result.HungTime.formatDate("hh:mm:ss"));
                    $("#txtCallTime").valOrHtml(data.Result.CallTime.formatDate("hh:mm:ss"));
                }
                else {
                    message(data.Message, null, false);
                }
            }
        });

    });

    //转座席
    $('#btnTransferAgent').click(function () {
        var agentNo = $("#txtAgentNo").val();
        if (agentNo == "") {
            alert(textConfig.pleaseEnterAgentNo);
            return false;
        }
        quickInvoker.catchDo(function () {
            cti.transAgent(agentNo);
        });

    });

    $("#txtAgentNo").keyup(function () {
        if ($(this).val() != "") {
            $("#btnTransferAgent").removeAttr("disabled");
        }
    });

    $("#txtAgentNo").blur(function () {
        $(this).click();
    });

    /*$("#txtTransPhoneNo").keyup(function () {
    if ($(this).val() != "") {
    $("#btnForward").removeAttr("disabled");
    }
    });*/

    //转驳
    $('#btnForward').click(function () {

        var phoneNo = $("#txtTransPhoneNo").val();
        if (phoneNo == "") {
            alert(textConfig.pleaseEnterPhoneNo);
            return false;
        }
        quickInvoker.catchDo(function () {
            cti.transCall(1, phoneNo);
        });

        popContentDialog(textConfig.calllingToAgent, 300, 120, textConfig.opretion, [{ text: textConfig.hungCopyText, onclick: function (dlg) {
            quickInvoker.catchDo(function () {
                cti.transCall(2, phoneNo);
            });
            dlg.close();
        }
        }, { text: textConfig.pullbackCopy2Text, onclick: function (dlg) {
            quickInvoker.catchDo(function () {
                cti.transCall(3, phoneNo);
            });
            dlg.close();
        }
        }]);
    });

    //创建三方通话
    $('#btnThreeWayCalling').click(function () {
        var callPhone = $("#txtTransPhoneNo").val();
        if (callPhone == "") {
            alert(textConfig.pleaseEnterPhoneNo);
            return false;
        }
        quickInvoker.catchDo(function () {
            cti.dial(callPhone);
        });
        popContentDialog(textConfig.callToOtherPhone, 200, 100, textConfig.opretion, [{ text: textConfig.createThreeCall, onclick: function (dlg) {
            quickInvoker.catchDo(function () {
                cti.meeting();
            });
            dlg.close();
        }
        }, { text: textConfig.pullbackcopy, onclick: function (dlg) {
            quickInvoker.catchDo(function () {
                cti.hung();
            });
            dlg.close();
        }
        }]);
    });

    //摘机
    $('#btnAnswer').click(function () {
        currentAttractTarget.PHONE_NO1 = $("#txtCallFrom").val();
        var answerToServer = function () {
            $.ajax({
                url: urlConfig.Anwser,
                cache: false,
                data: { OBCU_ID: currentAttractTarget.OBCU_ID, CUST_ID: currentAttractTarget.CUST_ID,
                    CUST_NAME: currentAttractTarget.CUST_NAME, CUTC_IDENTITY: currentAttractTarget.CUTC_IDENTITY,
                    GC_TITLE: currentAttractTarget.TITLE, CUTC_ID: currentAttractTarget.CUTC_ID, PHONE_NO: $("#txtCallFrom").val()
                },
                success: function (data) {
                    if (data.Success) {
                        currentAttractTarget.OBCU_ID = null;
                        currentAttractTarget.JOB_ID = '';
                        currentAttractTarget.CALL_ID = data.Result.CallID;

                        $("#txtHungTime").valOrHtml(data.Result.CallStartTime.formatDate("hh:mm:ss"));

                        if (!currentAttractTarget.InAttractStatus()) { //如果没有招揽对象时
                            var html = $("#INBOUND_TYPE").html();
                            $("#txtCallType").replaceWith($("<select>", { id: 'txtCallType', name: 'CALL_SUB_TYPE', html: html }));
                        }

                        $(this).removeAttr("disabled");
                        //masterControl.TemporaryJob();
                        masterControl.TemporaryJob(null, true);
                    }
                    else {
                        message(data.Message, null, false);
                    }
                }
            });
        };

        var tmpFun = function () {
            currentAttractTarget.CALL_TYPE = "I";
            var answerPhone = $("#txtCallFrom").val();
            if (masterControl.currentPhoneContact.lastAnswerPhone != answerPhone) {
                var lResult = masterControl.loadAttractTargetByPhone(answerPhone, true, null, null, null, function () {
                    masterControl.TemporaryJob();
                }, null, null, null, null, true);
                if (!lResult) {
                    return false;
                } else {
                    masterControl.currentPhoneContact.lastAnswerPhone = answerPhone;
                }
            }
            masterControl.ctiStatus.setCurrentStatus(masterControl.enumCtiStatus.Answer);
            masterControl.setSwithCallStatus(false);
            quickInvoker.stopwatchStart();
            quickInvoker.catchDo(function () {
                cti.answer();
            });
            answerToServer();
        };

        if (checkIsNullVal(currentAttractTarget.CALL_ID)) { //如果没有拨出则解锁
            if (currentAttractTarget.InAttractStatus()) { //如果是招揽状态
                masterControl.unLockCurrentObcu(function (data) {
                    if (data.Success) {
                        tmpFun();
                    } else {
                        message(data.Message, null, false);
                    }
                });
            } else {
                tmpFun();
            }
        } else { //拨出了则暂存
            //if (masterControl.jobStatus.getCurrentStatus() >= masterControl.enumJobStatus.UnSaved) {
            masterControl.TemporaryJob(function () {
                masterControl.clearAttractTarget();
                alert(textConfig.currentTaskTemporaryd);
                tmpFun();
            }, false);
            /*} else {
            tmpFun();
            }*/
        }

    });

    //保持
    $('#btnKeeping').click(function () {
        if ($(this).valOrHtml() != textConfig.pullback) {
            $(this).valOrHtml(textConfig.pullback);
            quickInvoker.catchDo(function () {
                cti.hold();
            });
        } else {
            $(this).valOrHtml(textConfig.hold);
            quickInvoker.catchDo(function () {
                cti.callback();
            });
        }
    });
});
