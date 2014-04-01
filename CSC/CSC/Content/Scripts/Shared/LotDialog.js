/// <reference path="../../jQuery/jquery-1.4.1-vsdoc.js" />
/// <reference path="../../Shared/Edit.js" />


var dlgEdit;
var lovLocation;
var div = $("#divresult");
var _callBack;
var warehouseCode;
var selectlotDiv;
$(document).ready(function () {
    $("#btndialog").attr("disabled", "true");    
    $("#LotBatch_BatchNo").die("changed").live("changed", selectBatch); 
   //window.myAjaxForm();
});

function ShowTrgLotDialogForText(unitBigText, baseUnitText, baseUnitId, warehouse, currentSelectItem, product, lotId, callBack) {

    var pId = $lovs[product.attr("id")].getValue();     
    if (pId == undefined || pId <= 0) {
        pId = product.attr("val");
    }
    if (pId == undefined || pId <= 0) {
        return false;
    }
    else {
        _callBack = callBack;
        $("#btnlotClose").die("click").live("click", DlgEditClose);
        $("#btnlotsave").die("click").live("click", SaveBatch);
        warehouseCode = warehouse;

        selectlotDiv = $("#selectlotbatch");
        if (selectlotDiv.length == 0) { selectlotDiv = $("<div>", { id: "selectlotbatch" }).appendTo($(document.body)); }

        dlgEdit = selectlotDiv.dialog({
            width: "750px",
            height: "500px",
            modal: true,
            title: $m["LotDialog"]
        });
        getbatchflag = true;
        var measure = "";

        if (unitBigText == baseUnitText) {
            measure = baseUnitText;
        } else {
            measure = unitBigText + "/" + baseUnitText;
        }
        var warehouseId = warehouseCode.attr("val");
        if (currentSelectItem != null) {
            if (currentSelectItem.ModelStatus == 1) {
                dlgEdit.onopen = function () {
                    $.post(window.GetTrgBatchInfo, { productid: pId, productcode: product.val(), unitId: baseUnitId, measure: measure, warehouseId: warehouseId, lotid: lotId }, function (resultHtml) {
                        selectlotDiv.html(resultHtml);
                    });
                }
            }
        } else {
            dlgEdit.onopen = function () {
                $.post(window.GetTrgBatchInfo, { productid: pId, productcode: product.val(), unitId: baseUnitId, measure: measure, warehouseId: warehouseId, lotid: lotId }, function (resultHtml) {
                    selectlotDiv.html(resultHtml);
                });
            }
        }
        dlgEdit.open();
    }
}



function ShowLotDialogForText(unitBigText, baseUnitText, baseUnitId, warehouse, currentSelectItem, product, lotId, callBack) {
    if (product.attr("val") == undefined || product.attr("val") <= 0) {
        return false;
    }
    else {
        _callBack = callBack;
        $("#btnlotClose").die("click").live("click", DlgEditClose);
        $("#btnlotsave").die("click").live("click", SaveBatch);
        warehouseCode = warehouse;

        selectlotDiv = $("#selectlotbatch");
        if (selectlotDiv.length == 0) { selectlotDiv = $("<div>", { id: "selectlotbatch" }).appendTo($(document.body)); }

        dlgEdit = selectlotDiv.dialog({
            width: "750px",
            height: "500px",
            modal: true,
            title: $m["LotDialog"]
        });
        getbatchflag = true;
        var measure = "";

        if (unitBigText == baseUnitText) {
            measure = baseUnitText;
        } else {
            measure = unitBigText + "/" + baseUnitText;
        }
        var warehouseId = warehouseCode.attr("val");
        if (currentSelectItem != null) {
            if (currentSelectItem.ModelStatus == 1) {
                dlgEdit.onopen = function () {
                    $.post(window.GetBatchInfo, { productid: product.attr("val"), productcode: product.val(), unitId: baseUnitId, measure: measure, warehouseId: warehouseId, lotid: lotId }, function (resultHtml) {
                        selectlotDiv.html(resultHtml);
                    });
                }
            }
        } else {
            dlgEdit.onopen = function () {
                $.post(window.GetBatchInfo, { productid: product.attr("val"), productcode: product.val(), unitId: baseUnitId, measure: measure, warehouseId: warehouseId, lotid: lotId }, function (resultHtml) {
                    selectlotDiv.html(resultHtml);
                });
            }
        }
        dlgEdit.open();
    }
}

function ShowLotDialog(cbxUnitBig1, cbxUnit1, cbwarehouseId1, selectedItem1, productId1, lotId, callBack) {
    if (productId1.attr("val") == undefined || productId1.attr("val") <= 0) {
        return false;
    } else {
        _callBack = callBack;
        $("#btnlotClose").die("click").live("click", DlgEditClose);
        $("#btnlotsave").die("click").live("click", SaveBatch);
        warehouseCode = cbwarehouseId1;
        selectlotDiv = $("#selectlotbatch");
        if (selectlotDiv.length == 0) { selectlotDiv = $("<div>", { id: "selectlotbatch" }).appendTo($(document.body)); }
        dlgEdit = selectlotDiv.dialog({
            width: "750px",
            height: "500px",
            modal: true,
            title: $m["LotDialog"]
        });
        getbatchflag = true;
        var unitId = 0;
        var measure = "";
        unitId = cbxUnitBig1.getValue();
        if (unitId == "") {
            measure = cbxUnit1.getText();
        } else {
            measure = cbxUnitBig1.getText() + "/" + cbxUnit1.getText();
        }
        unitId = cbxUnit1.getValue();
        var warehouseId = warehouseCode.attr("val");
        if (selectedItem1 != null) {
            if (selectedItem1.ModelStatus == 1) {
                dlgEdit.onopen = function () {
                    $.post(window.GetBatchInfo, { productid: productId1.attr("val"), productcode: productId1.val(), unitId: unitId, measure: measure, warehouseId: warehouseId, lotid: lotId }, function (resultHtml) {
                        selectlotDiv.html(resultHtml);
                    });
                }
            }
        } else {
            dlgEdit.onopen = function () {
                $.post(window.GetBatchInfo, { productid: productId1.attr("val"), productcode: productId1.val(), unitId: unitId, measure: measure, warehouseId: warehouseId, lotid: lotId }, function (resultHtml) {
                    selectlotDiv.html(resultHtml);
                });
            }
        }
        dlgEdit.open();
    }
}

function selectBatch() {
    lovLocation = null;
    if (lovLocation == null) {
        lovLocation = $lovs[$("#Lot_LocationCode").attr("id")];
    }
    var lovLotBatch = $lovs[$("#LotBatch_BatchNo").attr("id")];
    $.getJSON(window.BatchRelation, { batchid: lovLotBatch.getValue(), batchno: lovLotBatch.getText(), productid: $("#Lot_ProductId").val(), productcode: $("#LotBatch_ProductCode").val() }, function (json) {
        $("#LotBatch_ThirdBatchNo").val(json.ThirdBatchNo);
        $("#LotBatch_ProductionDate").val(json.ProductionDate);
        $("#LotBatch_ExpiredDate").val(json.ExpiredDate);
        $("#LotBatch_AltBatchNo").val(json.AlterCode);
        $("#LotBatch_Description").val(json.Description);
        $("#LotBatch_DescriptionEN").val(json.DescriptionEn);
        $("#LotBatch_Remarks").val(json.Remark);
        var hidlottype = $("#hidlottype");
        if (hidlottype.val() == "in") {
            if (parseInt(json.ProductId) == 0 && lovLotBatch.getText() != "") {
                var value = window.confirm($m["IsCreateLot"]);
                if (value == false) {
                    lovLotBatch.setText("");
                    setLotDisabled(false);
                } else {
                    setLotDisabled(true);
                }
            } else {
                setLotDisabled(false);
            }
        }
        lovLocation.lovData =
        {
            productid: $("#Lot_ProductId").val(),
            lotbatchid: lovLotBatch.getValue(),
            warehouseid: $("#Lot_WarehouseId").val(),
            productcode: $("#LotBatch_ProductCode").val(),
            warehousecode: warehouseCode.val(),
            type: hidlottype.val()
        };
    });
}

function setLotDisabled(obj) {
    var tblotmarkcode = $("#LotBatch_ThirdBatchNo");
    var tblotaltercode = $("#LotBatch_AltBatchNo");
    var tblotdescription = $("#LotBatch_Description");
    var tblotdescriptionen = $("#LotBatch_DescriptionEN");
    var callotproductdate = $("#LotBatch_ProductionDate");
    var callotexpirydate = $("#LotBatch_ExpiredDate");
    var tblotremark = $("#LotBatch_Remarks");
    if (obj == true) {
        tblotmarkcode.removeClass("in_readonly").removeAttr("disabled");
        tblotaltercode.removeClass("in_readonly").removeAttr("disabled");
        tblotdescription.removeClass("in_readonly").removeAttr("disabled");
        tblotdescriptionen.removeClass("in_readonly").removeAttr("disabled");
        callotproductdate.removeClass("in_readonly").removeAttr("disabled");
        callotexpirydate.removeClass("in_readonly").removeAttr("disabled");
        tblotremark.removeClass("in_readonly").removeAttr("disabled");
    } else {
        tblotmarkcode.addClass("in_readonly").attr("disabled", "disabled");
        tblotaltercode.addClass("in_readonly").attr("disabled", "disabled");
        tblotdescription.addClass("in_readonly").attr("disabled", "disabled");
        tblotdescriptionen.addClass("in_readonly").attr("disabled", "disabled");
        callotproductdate.addClass("in_readonly").attr("disabled", "disabled");
        callotexpirydate.addClass("in_readonly").attr("disabled", "disabled");
        tblotremark.addClass("in_readonly").attr("disabled", "disabled");
    }
}

function SaveBatch() {
    var tblotproductcode = $("#LotBatch_ProductCode");
    var tblotmeasure = $("#Lot_UnitCode");
    var lotbatch = $("#LotBatch_BatchNo");
    var lovlotbatch = $lovs[lotbatch.attr("id")];
    var tblotmarkcode = $("#LotBatch_ThirdBatchNo");
    var tblotaltercode = $("#LotBatch_AltBatchNo");
    var tblotdescription = $("#LotBatch_Description");
    var tblotdescriptionen = $("#LotBatch_DescriptionEN");
    var callotproductdate = $("#LotBatch_ProductionDate");
    var callotexpirydate = $("#LotBatch_ExpiredDate");
    var tblotremark = $("#LotBatch_Remarks");
    var tbproductid = $("#Lot_ProductId");
    var tbwarehouseid = $("#Lot_WarehouseId");
    var location = $("#Lot_LocationCode");
    var lovlocation = $lovs[location.attr("id")];

    var tbhidismustinput = $("#hidismustinput");
    var hidlottype = $("#hidlottype");
    var flag = true;
    flag = $v.Input(lotbatch, "MustInput") && flag;
    if (tbhidismustinput.val() == "1") {
        flag = $v.Input(callotproductdate, "MustInput") && flag;
    }
    flag = $v.Input(location, "MustInput") && flag;
    if (!flag) {
        return false;
    }
    if (hidlottype.val() != "in") {
        $.getJSON(window.GetLotInfo, { productid: tbproductid.val(), lotbatchid: lovlotbatch.getValue(), warehouseid: tbwarehouseid.val(), locationid: lovlocation.getValue() }, function (json) {
            if (parseInt(json.model.LotId) == 0) {
                alert($m["LotError"]);
                // alert("请检查库存,无法保存!");
                return;
            } else {
                if ($.isFunction(_callBack)) {
                    _callBack(json.model);
                    DlgEditClose();
                }
            }
        });
    } else {
        var frm = $("#addLotFrm");
        $.post(frm.attr("action"), frm.serialize(), function (json) {
            var result = json.result;
            if (result == "success") {
                if ($.isFunction(_callBack)) {
                    _callBack(json.model);
                    DlgEditClose();
                }
            } else {
                if (div.length == 0) { div = $("<div>", { id: "divresult" }).appendTo($(document.body)); }
                div.html(json).dialog({ title: $("span.s_none", div).html(), height: 150 }).open();
            }
        });
    }
    return flag;
}

function DlgEditClose() {  //关闭层方法
    if (dlgEdit != null) {
        dlgEdit.close();
    }
    lovLocation = null;
}