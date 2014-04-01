function AddItem(obj) {
	var tbody = $("#tbItems").find("tbody");

	var strItem = "<tr>";

	strItem += "<td class=\"t_center\">";
	strItem += "<input type=\"checkbox\" adminType='new' name=\"cbKey\" value='" + obj.SalesMemoItemId + "' "
			+ " BrandId='" + obj.BrandId + "'"
			+ " ModelId='" + obj.ModelId + "'"
			+ " PartsId='" + obj.PartsId + "'"
			+ " SrcSalesMemoItemId='" + obj.SrcSalesMemoItemId + "'"
			+ " UnitPrice='" + obj.UnitPrice + "'"
			+ " CreatedBy=\""+ obj.CreatedByName + "\""
			+ " CreationDate='"+ obj.CreationDate + "'"
			+ " LastUpdatedBy='" + obj.LastUpdatedByName +"'"
			+ " LastUpdateDate='"+ obj.LastUpdateDate + "' />";

	strItem += "</td>";
	strItem += "<td class=\"BrandCode t_left\">";
	strItem += obj.BrandCode;
	strItem += "</td>";

	strItem += "<td class=\"PartsNo t_left\">";
	strItem += obj.PartsNo;
	strItem += "</td>";

	strItem += "<td class=\"PartsDesc t_left\">";
	strItem += obj.PartsDesc;
	strItem += "</td>";

	strItem += "<td class='SalesQty t_right'>";
	strItem += obj.SalesQty;
	strItem += "</td>";

	strItem += "<td class=\"t_right\">";
	strItem += obj.SalesQty;
	strItem += "</td>";
	strItem += "<td class=\"t_right\">";
	if ($('#radio1').attr("checked")) {
		strItem += obj.SalesQty;
	}
	else {
		strItem += "0";
	}
	strItem += "</td>";
	strItem += "<td class=\"t_right\">0";
	strItem += "</td>";
	
	//			strItem +="<td class=\"t_left\">";
	//			strItem +=obj.DFT_UNIT_PRICE;
	//			strItem +="</td>";

	strItem += "<td class=\"UnitPrice t_right\">";
	strItem += obj.UnitPrice;
	strItem += "</td>";

	strItem += "<td class=\"DiscountPer t_right\">";
	strItem += obj.DiscountPer;
	strItem += "</td>";

	strItem += "<td class=\"TotalPrice t_right\">";
	strItem += obj.TotalPrice;
	strItem += "</td>";
	strItem += "<td></td>"
	strItem += "</tr>";
	strItem = $(strItem);
	strItem.click(function () {
		$("div.grid_list :checkbox[name='cbKey']").each(function () {
			$(this).attr("checked", false);
			$(this).parent().parent().css("background-color", "");
		});

		var cb = $(this).find(":checkbox[name='cbKey']");
		cb.attr("checked", !cb.attr("checked"));
		cb.parent().parent().css("background-color", "#FFD58D");
		tr.check.call(strItem);
	});
	tbody.append(strItem);
	SetSalesMemoAmt();
}

function RdioCheckByItems() {
	var items = $("div.grid_list :checkbox[name='cbKey']");
	 
	if (items.length>0) {
		RdioCheckSet(false);
	}
	else {
		RdioCheckSet(true);
	}
}

function RdioCheckSet(res) {
	$("#radio1").unbind("click");
	$("#radio2").unbind("click");
	if (res) {
		RadioBtnClick();
		$("#radio2").attr("disabled", "");
		$("#radio1").attr("disabled", "");
	}
	else {
		 
		$("#radio1").click(function () { return false; });
		$("#radio2").click(function () { return false; });

		$("#radio2").attr("disabled", "disabled");
		$("#radio1").attr("disabled", "disabled");
	}
}

function LoadSalesMemoItemBySalesMemoId(SalesMemoId) {
	$.ajax({
		type: "get",
		url: '../SalesMemo/LoadSalesMemoItemBySalesMemoId?SalesMemoId=' + SalesMemoId,
		success: function (dataContent) {
			var objArr = $.tryUnescape(dataContent);
			$.each(objArr, function (i, o) {
				o.SrcSalesMemoItemId = o.SalesMemoItemId;
				o.CreationDate = o.StrDate;
				o.LastUpdateDate = o.StrDate;
				AddItem(o);
			});
			RdioCheckByItems();//2013-12-12
		},
		complete: function (XMLHttpRequest, textStatus) { },
		error: function () { }
	});     //end ajax
	//Load Dispoist
	$.ajax({
		type: "get",
		url: '../SalesMemo/LoadSalesMemoForJoson?SalesMemoId=' + SalesMemoId,
		success: function (dataContent) {
			var obj = $.tryUnescape(dataContent);

			$("#SalesMemoObj_DepositAmt").val("");
			$("#PaidDepositAmount").html(obj.DepositAmt);
			$("#SalesMemoObj_MISC_CHARGE").val(obj.MISC_CHARGE);
			SetSalesMemoAmt();
		},
		complete: function (XMLHttpRequest, textStatus) { },
		error: function () { }
	});          //end ajax
}

function LoadSalesMemoItem() {
	$.ajax({
		type: "get",
		url: '../SalesMemo/LoadSalesMemoItemBySalesMemoId?SalesMemoId=' + PageSalesMemoID,
		success: function (dataContent) {
			var objArr = $.tryUnescape(dataContent);
			$.each(objArr, function (i, o) {
				if (!o.SrcSalesMemoItemId) {
					o.CreationDate = o.StrDate;
					o.LastUpdateDate = o.StrDate;
					AddItem(o);
				}
				SetSalesMemoAmt();
			});
		},
		complete: function (XMLHttpRequest, textStatus) { },
		error: function () { }
	});       //end ajax
}


function ItemEdit(ItemOR) {
	$("div.grid_list :checkbox[name='cbKey']").each(function () {
		var cbs = $(this);
		if (cbs.val() == ItemOR.SalesMemoItemId) {

			cbs.attr("BrandId", ItemOR.BrandId);
			cbs.attr("ModelId", ItemOR.ModelId);
			cbs.attr("PartsId", ItemOR.PartsId);
			cbs.attr("UnitPrice", ItemOR.UnitPrice);

			var trItem = cbs.parents("tr:first");
			trItem.find(".BrandCode").html(ItemOR.BrandCode);
			trItem.find(".PartsNo").html(ItemOR.PartsNo);
			trItem.find(".PartsDes").html(ItemOR.PartsDes);

			trItem.find(".SalesQty").html(ItemOR.SalesQty);
			trItem.find(".UnitPrice").html(ItemOR.UnitPrice);
			trItem.find(".DiscountPer").html(ItemOR.DiscountPer);
			trItem.find(".TotalPrice").html(ItemOR.TotalPrice);
		}
	});
	SetSalesMemoAmt();
}

function SetSalesMemoAmt() {
	var Amt = 0.0;
	$(".TotalPrice").each(function () {
		var obj = $(this);
		Amt += parseFloat(obj.html());
	});
	Amt += parseFloat($("#SalesMemoObj_MISC_CHARGE").val());
	var SalesMemoAmt = Math.round(Amt * 100) / 100;
	$("#lbSalesMemoAmt").html(SalesMemoAmt);
	if ($("#SalesMemoObj_SalesMemoStatus").val() != "O") {
		$("#PaymentAmount").html(GetPaymentAmt());
	}
}

function GetPaymentAmt() {
	var fPaymentAmount = 0.0;
	var SalesMemoAmt = parseFloat($("#lbSalesMemoAmt").html());
	var DepositAmt = parseFloat($("#SalesMemoObj_DepositAmt").val());
	if ($("#SalesMemoObj_SalesMemoStatus").val() != "O") {
		DepositAmt = parseFloat($("#SalesMemoObj_DepositAmt").html());
	}
	if (DepositAmt == NaN)
		DepositAmt = 0;
	if ($('#radio1').attr("checked")) {//DEPOSIT
		fPaymentAmount = DepositAmt;
	}
	else {
		DepositAmt = 0;
		if ($("#PaidDepositAmount").html() != "") {
			DepositAmt =  parseFloat($("#PaidDepositAmount").html());
		}
		if (DepositAmt > 0) {
			fPaymentAmount= SalesMemoAmt - DepositAmt;
		}
		else {
			fPaymentAmount= SalesMemoAmt;
		}
	}
	return fPaymentAmount;
}

function CheckPartsItem(ItemOR) {
	var Resul = false;
	$("div.grid_list :checkbox[name='cbKey']").each(function () {
		var obj = $(this);
		if (parseInt(obj.attr("PartsId")) == parseInt(ItemOR.PartsId) && obj.val() != ItemOR.SalesMemoItemId) {
			Resul = true;
		}
	});
	return Resul; 
}

function GetEditUrl(cbs) {
	var urlHref = "&BrandId=" + cbs.attr("BrandId");
	urlHref += "&ModelId=" + cbs.attr("ModelId");
	urlHref += "&PartsId=" + cbs.attr("PartsId");
	urlHref += "&UnitPrice=" + cbs.attr("UnitPrice");

	var trItem = cbs.parents("tr:first");
	urlHref += "&SalesQty=" + trItem.find(".SalesQty").html();
	urlHref += "&DiscountPer=" + trItem.find(".DiscountPer").html();
	urlHref += "&TotalPrice=" + trItem.find(".TotalPrice").html();
	return urlHref;
}

function goToPage(index) {
	window.parent.$("div.tab_container").find("ul>li").eq(index).click();
}

//验证
var B_Check = true;
function CheckResult(mid) {
	if ($("#" + mid).val() == "") {
		B_Check = false;
		$("#Validation_" + mid).html("This field is required");
		$("#Validation_" + mid).show();
	}
	else {
		$("#Validation_" + mid).hide();
	}
}

//计算价格
function ChangeTotalPrice() {
	var strUnit = $("#UnitPrice").val();
	var strQty = $("#SalesQty").val();
	var strDiscPer = $("#DiscountPer").val();

	if (strUnit == "" || strQty == "")
		return;
	if (strDiscPer == "") {
		$("#lblTotalPrice").html(parseInt(strUnit) * parseInt(strQty));
	}
	else {
		var Result = parseFloat(strUnit) * parseInt(strQty) * (1 - parseInt(strDiscPer) / 100);
		$("#lblTotalPrice").html(parseInt(Result));
	}
}


function GetSaveContent() {
	B_Check = true;
	var aymentCode= $.dropdown.GetCode({ id: "PAYMENT_METHOD_ID" });

	CheckResult("PAYMENT_METHOD_ID");
	if (aymentCode != "AR PAYMENT" && aymentCode != "RESERVE") {
		CheckResult("PaymentAmount");
	}

	CheckResult("SalesMemoObj_MISC_CHARGE");

	var PaymentType = "NORMAL";
	var DepositAmt = 0;
	if ($('#radio1').attr("checked")) {
		PaymentType = "DEPOSIT";
		CheckResult("SalesMemoObj_DepositAmt");
		DepositAmt = $("#SalesMemoObj_DepositAmt").val();
	}
	else {
		$("#Validation_SalesMemoObj_DepositAmt").hide();
		if ($("#PaidDepositAmount").html() != "") {
			DepositAmt = $("#PaidDepositAmount").html();
		}
	}
	if (BANK_CODE_REQ_IND == 'Y') { 
		CheckResult("SalesMemoObj_BankName");
	}

	if (PAY_REF_REQ_IND == 'Y') {
		$("#Validation_SalesMemoObj_ChequeDate").html("This field is required.");
		CheckResult("SalesMemoObj_ChequeNo");
		CheckResult("SalesMemoObj_ChequeDate"); 
	}
	
	if (!B_Check)
		return false;
	if ($("#SalesMemoObj_ChequeDate").val()) {
		var regDateFomart = /^(\d{2})-(\d{2})-(\d{4})$/  //特定的格式化 形如 dd-mm-yyyy
		var NowDateStr_Date = NowDateStr.replace(regDateFomart, "$3/$2/$1");
		var NowDateStr6_Date = NowDateStr6.replace(regDateFomart, "$3/$2/$1");
		var date3Str_Date = $("#SalesMemoObj_ChequeDate").val().replace(regDateFomart, "$3/$2/$1");

		var date1 = new Date(NowDateStr_Date);
		var date2 = new Date(NowDateStr6_Date);
		var date3 = new Date(date3Str_Date);

		if (!(date2 < date3 && date3 < date1)) {
			$("#Validation_SalesMemoObj_ChequeDate").html("Cheque date valid only within 6 months and must not greater than today.");
			$("#Validation_SalesMemoObj_ChequeDate").show();
			return false;
		}
		else {
			$("#Validation_SalesMemoObj_ChequeDate").hide();
		}
	}
	if (aymentCode != "AR PAYMENT" && aymentCode != "RESERVE") {
		//验证输入金额
		$("#Validation_PaymentAmount").hide();
		if (GetPaymentAmt() != parseFloat($("#PaymentAmount").val()) || GetPaymentAmt() > parseFloat($("#lbSalesMemoAmt").html())) {
			$("#Validation_PaymentAmount").html("Input Payment Amount Error.");
			$("#Validation_PaymentAmount").show();
			return false;
		}

		if (PaymentType == "DEPOSIT") {
			if (parseFloat(DepositAmt) == 0) {
				$("#Validation_SalesMemoObj_DepositAmt").html("Must be greater than 0");
				$("#Validation_SalesMemoObj_DepositAmt").show();
				return false;
			}
		}
	}

	DepositAmt = parseFloat(DepositAmt);
	if(!DepositAmt)
		DepositAmt = 0;

	var Content = "{";
	Content += "\"PaymentType\": \"" + PaymentType + '\"';
	Content += ",\"SalesMemoId\": \"" + $("#SalesMemoObj_SalesMemoId").val() + '\"';
	Content += ",\"SrcSalesMemoId\": \"" + $("#SrcSalesMemoId").val() + '\"';
	Content += ",\"DepositAmt\": \"" + DepositAmt + '\"';
	Content += ",\"MISC_CHARGE\": \"" + $("#SalesMemoObj_MISC_CHARGE").val() + '\"';
	Content += ",\"PAYMENT_METHOD_ID\": \"" + $("#PAYMENT_METHOD_ID").val() + '\"';
	Content += ",\"BankName\": \"" + escape($("#SalesMemoObj_BankName").val()) + '\"';
	Content += ",\"ChequeNo\": \"" + escape($("#SalesMemoObj_ChequeNo").val()) + '\"';
	Content += ",\"ChequeDate\": \"" + $("#SalesMemoObj_ChequeDate").val() + '\"';
	
	if ($("div.grid_list :checkbox[name='cbKey']").length == 0) {
		showMsg("Please Add Details!");
		return false;
	} 

	Content += ",\"Items\": ";
	var json = "[";
	
	//Items
	$("div.grid_list :checkbox[name='cbKey']").each(function (index, element) {
		var cbs = $(this);
		var trItem = cbs.parents("tr:first");
		var DiscountPer = trItem.find(".DiscountPer").html();
		if (DiscountPer == "") {
			DiscountPer = "0";
		}

		var SrcSalesMemoItemId = cbs.attr("SrcSalesMemoItemId");
		if (SrcSalesMemoItemId == 'null')
			SrcSalesMemoItemId = '';

		json += '{"strBrandId":\"' + cbs.attr("BrandId") + '\"'
		 + ',"SalesMemoItemId":' + cbs.val()
		 + ',"strModelId":\"' + cbs.attr("ModelId") + '\"'
		 + ',"PartsId":' + cbs.attr("PartsId")
		  + ',"UnitPrice":' + cbs.attr("UnitPrice")
		 + ',"adminType": \"' + cbs.attr("adminType") + '\"'
		 + ',"strSrcSalesMemoItemId": \"' + SrcSalesMemoItemId + '\"'

		 + ',"SalesQty":' + trItem.find(".SalesQty").html()
		 + ',"DiscountPer":' + DiscountPer
		 + '},';
	});
	if (json == "")
		return false;
	if (json.substr(json.length - 1, 1) == ',')
		json = json.substr(0, json.length - 1);
	json += "]";
	Content += json;

	Content += "}";
	return Content;
}

//销售方式 选中逻辑
function PaymentLogic() {
	if ($('#radio1').attr("checked")) {
		$.dropdown.disabled({ id: 'SrcSalesMemoId', isDisabled: true, isClearValue: true, Target: "" });
		if ($("#SalesMemoObj_SalesMemoStatus").val() == "O") {
			$("#SalesMemoObj_DepositAmt").attr("readonly", "");
			$("#SalesMemoObj_DepositAmt").removeClass("label_readonly");
			//$("#SalesMemoObj_DepositAmt").val("");
			$("#PaidDepositAmount").html("");
		}
		$("#SrcSalesMemoId").change();
	}
	else {
		$.dropdown.disabled({ id: 'SrcSalesMemoId', isDisabled: false });
		$("#SalesMemoObj_DepositAmt").attr("readonly", "readonly");
		$("#SalesMemoObj_DepositAmt").addClass("label_readonly");
		$("#SalesMemoObj_DepositAmt").val("");
	}
}

//押金 隐显逻辑
function DepositShowHide() {
	var SrcSalesMemoId = $("#SrcSalesMemoId").val();
	if (SrcSalesMemoId == "") {
		$(".spanSalesBtn").show();
	}
	else {
		$(".spanSalesBtn").hide();
	}
}

function RadioBtnClick() {
	$("#radio2").click(function () {
		//var PaymentType = "NORMAL";
		HeadleArPayment();
		PaymentLogic();
		//如果选择了
		var tbody = $("#tbItems").find("tbody");
		tbody.html("");
		DepositShowHide();

		if (RefSalesMemoID) {
			$.dropdown.SetValue({ id: "SrcSalesMemoId", val: RefSalesMemoID });
			return;
		}
		if (SalesMemoObjSalesMemoType == 'NORMAL') {
			LoadSalesMemoItem();
		}
		SetSalesMemoAmt();
	});
	$("#radio1").click(function () {
		HeadleArPayment();
		PaymentLogic();

		var tbody = $("#tbItems").find("tbody");
		tbody.html("");
		DepositShowHide();
		if (SalesMemoObjSalesMemoType != 'NORMAL') {
			LoadSalesMemoItem();
		}
		else {
			SetSalesMemoAmt();
		}
	});
}

$(function () {
	//保存
	$("#submitClk").click(function () {
		var editJson = GetSaveContent();
		if (!editJson)
			return false;

		$.ajax({
			type: "get",
			url: '../SalesMemo/SubmitItems?editJson=' + editJson,
			success: function (dataContent) {
				if (dataContent == 0) {
					showSuccessMsg('Save Success.',
									null,
									function () {
										window.checkFormChanged = false;
										goToPage(0);
									});
					return false;
				}
				else {
					showMsg(dataContent);
				}
			},
			complete: function (XMLHttpRequest, textStatus) { },
			error: function () { }
		}); //end ajax

		return false;
	});
	//保存确认
	$("#SaveAndConfimd").click(function () {
		var aymentCode = $.dropdown.GetCode({ id: "PAYMENT_METHOD_ID" });
		if (aymentCode == "RESERVE") {
			showMsg("Reserve can not confirm.");
			return false;
		}

		var editJson = GetSaveContent();
		if (!editJson)
			return false;

		$.ajax({
			type: "get",
			url: '../SalesMemo/SubmitItemsAndConfimd?editJson=' + editJson,
			success: function (dataContent) {
				if (dataContent == 0) {
					showSuccessMsg('Save Success.',
									null,
									function () {
										window.checkFormChanged = false;
										goToPage(0);
									});
					return false;
				}
				else {
					showMsg(dataContent);
				}
			},
			complete: function (XMLHttpRequest, textStatus) { },
			error: function () { }
		}); //end ajax
		return false;
	});


	//List 逻辑处理
	if ($("#SalesMemoObj_SalesMemoStatus").val() == "O") {
		RdioCheckSet(true);
	}
	else {
		//根据子项，处理是否可以切换。
		RdioCheckSet(false);
	}


	//押金选择  SrcSalesMemoId
	var Old_SrcSalesMemoId = "";
	$("#SrcSalesMemoId").change(function () {
		if (IsClickChange) {
			if ($("#SrcSalesMemoId").val() == "") {
				RefSalesMemoID = 0;
			}
			else {
				RefSalesMemoID = parseInt($("#SrcSalesMemoId").val());
			}
		}

		var SrcSalesMemoId = $("#SrcSalesMemoId").val();
		if (Old_SrcSalesMemoId == SrcSalesMemoId)
			return false;

		var tbody = $("#tbItems").find("tbody");
		tbody.html("");

		Old_SrcSalesMemoId = SrcSalesMemoId;

		if (SrcSalesMemoId != "") {
			LoadSalesMemoItemBySalesMemoId(SrcSalesMemoId);
		}
		DepositShowHide();
		SetSalesMemoAmt();
	});

	//Item 金额处理
	//change Head
	$("#DiscountPer").change(function () {
		ChangeTotalPrice();
	});
	$("#UnitPrice").change(function () {
		ChangeTotalPrice();
	});
	$("#SalesQty").change(function () {
		ChangeTotalPrice();
	});


	//联动
	//Brand
	$.dpdChange({
		url: "../PartsMaintenance/SearchBrandByPartsForDLL",
		data: {}, //请求参数,Request.QueryString 获取
		sourceObj: "PartsId",
		sourceObj1: "",
		changeObj: "BrandId", 		//目标dpd
		Target: "", 				//目标，Target Lbl
		Field: { Desc: "BrandDesc", Value: "BrandId", DisplayName: "BrandCode"}//和dpd一样的三个参数
	});

	//Model
	$.dpdChange({
		url: "../PartsMaintenance/SearchModelByPartsForDLL",
		data: {}, //请求参数,Request.QueryString 获取
		sourceObj: "BrandId",
		sourceObj1: "PartsId",
		changeObj: "ModelId", 		//目标dpd
		Target: "lblModelDesc", //目标，Target
		Field: { Desc: "ModelDesc", Value: "RefModelId", DisplayName: "ModelCode"}//和dpd一样的三个参数
	});


	if ($('#radio1').attr("checked")) {//DEPOSIT
		$("#PaidDepositAmount").html("");
	}

	PaymentLogic();
	//菜单隐显逻辑
	DepositShowHide();
	//按钮可用处理	2013-12-12
	RdioCheckByItems();

	//支票日期
	$("#SalesMemoObj_ChequeDate").change(function () {
		CheckChequeDate();
	});

	$("#SalesMemoObj_DepositAmt").change(function () {
		SetSalesMemoAmt();
	});

	//处理 Ar payment
	if ($("#ulPAYMENT_METHOD_ID").length == 1) {
		HeadleArPayment();
	} //end Ar payment

	$("#SalesMemoObj_MISC_CHARGE").change(function () {
		SetSalesMemoAmt();
	});
	if ($("#SalesMemoObj_SalesMemoStatus").val() == "O") {
		SetSalesMemoAmt();
	}
	HeadPaymentMustInsertItem();
});

function HeadleArPayment() {
	if ($("#ulPAYMENT_METHOD_ID").length == 1) {
		var arPayItem = null;
		var reserveItem = null;
		$("#ulPAYMENT_METHOD_ID li").each(function (i, o) {
			if ($(this).find("a").html() == "AR PAYMENT") {
				arPayItem = $(this);
			}
			if ($(this).find("a").html() == "RESERVE") {
				reserveItem = $(this);
			}
		});

		var PaymentType = "NORMAL";
		var DepositAmt = 0;
		if ($('#radio1').attr("checked")) {
			PaymentType = "DEPOSIT";
		}

		var arCode = $.dropdown.GetCode({ id: "PAYMENT_METHOD_ID" });		
		var minItem = 0;//要减去多少行。
		if (arPayItem) {
			if (AR_CUST_IND && AR_CUST_IND == 'N' || PaymentType == "DEPOSIT"){
				arPayItem.hide();
				minItem += 1;
				if (arCode == "AR PAYMENT") {
					$.dropdown.ClearValue({ id: "PAYMENT_METHOD_ID" });
					$(".tdPayment").show();
				}
			}
			else {
				arPayItem.show();
			}
		}
		if (reserveItem) {
			if (PaymentType == "DEPOSIT") {
				reserveItem.hide();
				minItem += 1;
				if (arCode == "RESERVE") {
					$.dropdown.ClearValue({ id: "PAYMENT_METHOD_ID" });
					$(".tdPayment").show();
				}
			}
			else {
				reserveItem.show();
			}
		}
		//处理高度
		var dHeight = 20;
		var liItemCount = $("#ulPAYMENT_METHOD_ID li").length;
		 
		dHeight = (liItemCount - minItem) * 19;
		if (dHeight < 19)
			dHeight = 19;
		$("#popupPAYMENT_METHOD_ID").css("height", dHeight + "px");
		// end 处理高度

		if (arCode == "AR PAYMENT" || arCode == "RESERVE") {
			$(".tdPayment").hide();
		}
		else {
			$(".tdPayment").show();
		}
		$("#PAYMENT_METHOD_ID").change(function () {
			arCode = $.dropdown.GetCode({ id: "PAYMENT_METHOD_ID" });
			if (arCode == "AR PAYMENT" || arCode == "RESERVE") {
				$(".tdPayment").hide();
			}
			else {
				$(".tdPayment").show();
			}
			HeadPaymentMustInsertItem();
		});
	} //end Ar payment
}

//headle must insert
var BANK_CODE_REQ_IND = 'N';
var PAY_REF_REQ_IND = 'N';
function HeadPaymentMustInsertItem() {
	BANK_CODE_REQ_IND = 'N';
	PAY_REF_REQ_IND = 'N';
	$("#Validation_SalesMemoObj_BankName").hide();
	$("#Validation_SalesMemoObj_ChequeNo").hide();
	$("#Validation_SalesMemoObj_ChequeDate").hide();
	if ($("#PAYMENT_METHOD_ID").val() == "")
		return;

	var arCode = $.dropdown.GetCode({ id: "PAYMENT_METHOD_ID" });
	if (arCode != "AR PAYMENT" && arCode != "RESERVE") {
		var murl = '../PaymentMethod/GetItem?PAYMENT_METHOD_ID=' + $("#PAYMENT_METHOD_ID").val();
		$.ajax({
			type: 'GET',
			url: murl,
			dataType: "json",
			data: { _t: Math.random() },
			success: function (res) {
				if (res) {
					if (res.BANK_CODE_REQ_IND == 'Y') {
						$(".tdbank").show();
						BANK_CODE_REQ_IND = 'Y';
					}
					else {
						$(".tdbank").hide();
					}

					if (res.PAY_REF_REQ_IND == 'Y') {
						$(".tdChe").show();
						PAY_REF_REQ_IND = 'Y';
					}
					else {
						$(".tdChe").hide();
					}
					//alert(res.PAY_REF_REQ_IND);
				}
			},
			error: function (xhr) {
				showMsg(xhr.responseText);
			}
		});
	}
}

function CheckChequeDate() {
//	if ($("#SalesMemoObj_ChequeDate").val() == "") {
//		$("#Validation_SalesMemoObj_ChequeDate").hide();
//		return;
//	}
//	var c = new CalendarClass();
//	c.Element = $(this)[0];
//	c.Lang = window.Language;
//	c.Format = window.DataFormat;
//	c.TranDate = window.TranDate;
//	var mTranDate = c.__InitDate__(window.TranDate);
//	var mChequeDate = c.__InitDate__($("#SalesMemoObj_ChequeDate").val())
//	$("#Validation_SalesMemoObj_ChequeDate").html("Cheque/Ref Date is greater than Transaction Date.");
//	if (mTranDate < mChequeDate) {
//		$("#Validation_SalesMemoObj_ChequeDate").show();
//	}
//	else {
//		$("#Validation_SalesMemoObj_ChequeDate").hide();
//	}
}
 
