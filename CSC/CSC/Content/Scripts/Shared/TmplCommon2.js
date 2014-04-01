/// <reference path="../../jQuery/jquery-1.4.1-vsdoc.js" />
/// <reference path="../../jQuery/jquery.tmpl.js" />
/// <reference path="../../Shared/Edit.js" />
/// <reference path="../../Shared/Common.js" />

//Purchase:采购.Sales:销售
var DocumentationTypeEnum = { Purchase: 100, Sales: 101,Assembly:102,Other:109 };
Namespace.register("TmplCommon");
TmplCommon.Product = function (nicQuantity, quantity, cbxUnit, basicQuantity, cbxBasicUnit, productDesc,
        fixedDigits, convertQuantityRate, productRelationUrl) {
    this.nicQuantity = nicQuantity;   //基于计量单位换算后的数量的 numerics对象
    this.quantity = quantity; //基于计量单位换算后的数量 对象
    this.cbxUnit = cbxUnit; //cbxUnit 计量单位 cbx 对象
    this.basicQuantity = basicQuantity; // 基本单位 的数量 对象
    this.cbxBasicUnit = cbxBasicUnit; //基本单位 的 cbx 对象
    this.productDesc = productDesc;
    this.fixedDigits = 4; //保留小数位数
    this.convertQuantityRate = convertQuantityRate; // 全局换算比例
    this.productRelationUrl = productRelationUrl; //产品相关请求地址
    //this.unitCostRelationUrl = unitCostRelationUrl; //计量单位 价格相关地址
    this.unitConvert = null; //产品计量单位 的JSON对象
    this.UnitCostRelation = null; //单位成本 JSON 对象
    this.costSum = 0; //总成本
    this.BaseUnitCostRelation = null; //私有,当前基本单位单位成本,相关的数据 的JSON 对象
}

TmplCommon.Product.prototype = {
    //lov产品 Change
    //参数说明:(共)
    //1.e 对象
    //2.计量单位ID值
    //3.基于计量单位换算后的数量的 numerics对象
    //4.cbxUnit 计量单位 cbx 对象
    //5.//基本单位 的数量 对象
    //6.回调方法
    ProductChange: function (e, unitId, type, callBack) {
        //var unitId, numBig, cbxUnitBig, itemQuantity, callBack;
        if (DocumentationTypeEnum.Purchase === type) {

        }
        else if (DocumentationTypeEnum.Sales === type) {

        } else if (DocumentationTypeEnum.Assembly === type) {

        } else if (DocumentationTypeEnum.Other === type) {

        } else {
            alert("少 type参数,代表单据的类型.接收以下枚举值。\n1. " +
            "DocumentStatusEnum.Purchase(采购的单据);\n2.DocumentStatusEnum.Sales(销售的单据);\n3.DocumentationTypeEnum.Assembly(组装的单据)");
            return;
        }
        var currentObj = this;
        var s = arguments.length;
        currentObj.cbxUnit.element.empty();
        currentObj.UnitCostRelation = null;
        currentObj.BaseUnitCostRelation = null;
        currentObj.cbxUnit.setText($g["Select"]);
        currentObj.cbxUnit.refresh();
        currentObj.nicQuantity.refresh({ maxValue: 0, minValue: 0 });
        var pid = $(e.currentTarget).attr("val");
        if (pid > 0) {
            $.getJSON(currentObj.productRelationUrl, { productId: pid, documentStatus: type }, function (json) {
                currentObj.productDesc.val(json.ProductDesc);
                currentObj.unitConvert = json.UnitList;
                if (currentObj.unitConvert.length > 0) {
                    $("<option value=''>" + $g["Select"] + "</option>").appendTo(cbxUnitBig.element);
                    $.each(currentObj.unitConvert, function (i, n) {
                        //var option = $("<option value='" + n.Value + "' " + (n.Selected == true ? 'selected="selected"' : '') + ">" + n.Text + "</option>");
                        if (unitId == undefined || unitId <= 0) { //是修改 还是添加(修改不会进)
                            if (n.Selected == true) {
                                unitId = n.Value;
                            }
                        }
                        var option = $("<option value='" + n.Value + "'>" + n.Text + "</option>");
                        option.appendTo(currentObj.cbxUnit.element);
                    });
                    currentObj.nicQuantity.refresh({ minValue: 0 });
                }
                currentObj.cbxUnit.refresh();
                if (unitId) {
                    currentObj.cbxUnit.setValue(unitId);
                    currentObj.basicQuantity.trigger("changed", [unitId]);
                }
                if ($.isFunction(callBack)) {
                    callBack(json);
                }
            });
        }

    },
    ///基本单位 数量的 Change
    QuantityChange: function (e, id, callBack) {
        var currentObj = this;
        var uc = currentObj.cbxUnit.getValue();
        var ucs = currentObj.cbxBasicUnit.getValue();
        uc = id > 0 ? id : uc;
        if (uc > 0 && uc != ucs) {
            var vs = parseInt($(e.currentTarget).val());
            var vt = parseInt(currentObj.quantity.val());
            vs = isNaN(vs) ? 0 : vs;
            vt = isNaN(vt) ? 0 : vt;
            var uq = currentObj.convertQuantityRate = currentObj.FindConvertQuantity(uc, currentObj.unitConvert);
            // currentObj.quantity.val(vt + (Math.floor(vs / uq)));
            currentObj.quantity.val(vt + (parseInt(vs / uq)));
            $(e.currentTarget).val(vs % uq);
        }
        if ($.isFunction(callBack)) {
            callBack(e);
        }
    },
    //计量单位的 Change
    //参数:产品ID
    CalcQuantity: function (e, callBack) {
        var currentObj = this;
        //获取 单位 成本(基本计量单位)
        currentObj._CalcQuantity(e, callBack);

        //        if (currentObj.UnitCostRelation == null &&
        //        currentObj.unitCostRelationUrl != null && currentObj.unitCostRelationUrl != "") {
        //            $.getJSON(currentObj.unitCostRelationUrl, { productId: productId }, function (json) {
        //                currentObj.UnitCostRelation = json.UnitCostRelation;
        //                currentObj._CalcQuantity(e, callBack);
        //            });
        //        } else {
        //            currentObj._CalcQuantity(e, callBack);
        //        }

    },
    //计算数量
    _CalcQuantity: function (e, callBack) {
        var currentObj = this;
        var uc = currentObj.cbxUnit.getValue();
        var vs = parseInt(currentObj.basicQuantity.val());
        var vt = parseInt(currentObj.quantity.val());
        vs = isNaN(vs) ? 0 : vs;
        vt = isNaN(vt) ? 0 : vt;
        var uq = currentObj.FindConvertQuantity(uc, currentObj.unitConvert);
        var vbase = vt * currentObj.convertQuantityRate + vs;
        if (uc > 0) {
            currentObj.quantity.val(parseInt(vbase / uq));
            currentObj.basicQuantity.val(vbase % uq);
        } else {
            currentObj.quantity.val(0);
            currentObj.basicQuantity.val(vbase);
        }
        currentObj.convertQuantityRate = uq;
        if ($.isFunction(callBack)) {
            callBack(e);
        }
    },
    //计算 以 基本 单位 为准 的 数量
    CalcBaseUnit: function () {
        var currentObj = this;
        var uc = currentObj.cbxUnit.getValue();
        var ucs = currentObj.cbxBasicUnit.getValue();
        if (uc > 0 && uc != ucs) { //有大单位存在
        } else { //如果没有大单位存在, 大单位 数量为0的.
            currentObj.quantity.val("0");
        }
        var vs = parseInt(currentObj.basicQuantity.val());
        var vt = parseInt(currentObj.quantity.val());
        vs = isNaN(vs) ? 0 : vs;
        vt = isNaN(vt) ? 0 : vt;
        return vt * currentObj.convertQuantityRate + vs;
    },
    //计算 以 BIG 大 单位 为准 的 数量  
    CalcBigUnitNum: function () {
        var currentObj = this;
        var vs = parseInt(currentObj.basicQuantity.val());
        var vt = parseInt(currentObj.quantity.val());
        vs = isNaN(vs) ? 0 : vs;
        vt = isNaN(vt) ? 0 : vt;
        return parseInt(vt) + parseFloat((vs / currentObj.convertQuantityRate).toFixed(6));
    },
    //,查找当前选择的计量单位的 换算值
    FindConvertQuantity: function (uid) {
        var result = 1;
        if (this.unitConvert == null) {
            return result;
        }
        $.each(this.unitConvert, function (i, n) {
            if (n.Value == uid) {
                result = n.Quantity;
                return;
            }
        });
        return parseInt(result);
    },
    //查找单位成本相关的数据
    FindeUnitCostRelation: function (uid) {
        var currentObj = this;
        var unitCostRelation = null;
        if (currentObj.UnitCostRelation == null) {
            return unitCostRelation;
        }
        if (uid != undefined && uid != "") {
            $(currentObj.UnitCostRelation).each(function (index, n) {
                if (n.Value == uid) {
                    unitCostRelation = currentObj.UnitCostRelation[index];
                    return;
                }
            });
        }

        //IsBaseUnit
        //如果没有找到,返回基本计量单位
        if (unitCostRelation == null) {
            unitCostRelation = currentObj.BaseUnitCostRelation;
        }
        return unitCostRelation;
    },
    //构造 计量单位  描述
    BuilderUnitDesc: function () {
        var currentObj = this;
        var sb = new $t.stringBuilder();
        var qb = currentObj.quantity.val();
        var qub = currentObj.cbxUnit.getValue();
        if (qub > 0) {
            sb.cat(qb);
            sb.cat(" ");
            sb.cat(currentObj.cbxUnit.getText());
            sb.cat(" - ");
        }
        sb.cat(currentObj.basicQuantity.val());
        sb.cat(" ");
        sb.cat(currentObj.cbxBasicUnit.getText());
        return sb.string();
    }

}
