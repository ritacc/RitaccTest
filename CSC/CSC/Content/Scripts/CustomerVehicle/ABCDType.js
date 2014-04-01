//全局变量
var repairCountA = ""; 
var repairCountB = "";
var countB = "";
var repairCountC = "";
var countC = "";
var countD = "";

var repairAmountA = "";
var repairAmountB = "";
var amountB = "";
var repairAmountC = "";
var amountC = "";
var amountD = "";

var countFlag = false;
var amountFlag = false;

var except = "";

$(function () {

	//首次加载的时候 获取文本框初始值
	repairCountA = $("#RepairCountA").val();
	repairCountB = $("#RepairCountB").val();
	repairCountC = $("#RepairCountC").val();
	countB = $("#CountB").val();
	countC = $("#CountC").val();
	countD = $("#CountD").val();

	repairAmountA = $("#RepairAmountA").val();
	repairAmountB = $("#RepairAmountB").val();
	repairAmountC = $("#RepairAmountC").val();
	if (parseInt(repairAmountA) == 0) {
		$("#RepairAmountA").val("0.00");
		$("#AmountB").val("0.00");
	}
	if (parseInt(repairAmountB) == 0) {
		$("#RepairAmountB").val("0.00");
		$("#AmountC").val("0.00");
	}
	if (parseInt(repairAmountC) == 0) {
		$("#RepairAmountC").val("0.00");
		$("#AmountD").val("0.00");
	}
	$("#RepairAmountD").val("0.00");
	amountB = $("#AmountB").val();
	amountC = $("#AmountC").val();
	amountD = $("#AmountD").val();
	countFlag = $("#RepairCountFlag").attr("checked");
	amountFlag = $("#RepairAmountFlag").attr("checked");
	except = $("#ExceptConstType").val();

	//首次加载的时候 给TextBox加上背景色  
	$("#RepairCountA").css("background-color", "#FEF6D0");
	$("#RepairCountB").css("background-color", "#FEF6D0");
	$("#RepairCountC").css("background-color", "#FEF6D0");

	$("#RepairAmountA").css("background-color", "#FEF6D0");
	$("#RepairAmountB").css("background-color", "#FEF6D0");
	$("#RepairAmountC").css("background-color", "#FEF6D0");
	$("#ExceptConstType").css("background-color", "#FEF6D0");

	//弹出确认框(还有未保存数据,确定离开此页面？)
	lastFormHtml = $(window.document.body).find("form").formSerialize();

	//if (countFlag == false) {
	//	$("#RepairCountA").css("background-color", "#FEF6D0");
	//	$("#RepairCountB").css("background-color", "#FEF6D0");
	//	$("#RepairCountC").css("background-color", "#FEF6D0");
	//} else {
	//	$("#RepairCountA").css("background-color", "");
	//	$("#RepairCountB").css("background-color", "");
	//	$("#RepairCountC").css("background-color", "");
	//}

	//if (amountFlag == false) {
	//	$("#RepairAmountA").css("background-color", "#FEF6D0");
	//	$("#RepairAmountB").css("background-color", "#FEF6D0");
	//	$("#RepairAmountC").css("background-color", "#FEF6D0");
	//	$("#ExceptConstType").css("background-color", "#FEF6D0");
	//} else {
	//	$("#RepairAmountA").css("background-color", "");
	//	$("#RepairAmountB").css("background-color", "");
	//	$("#RepairAmountC").css("background-color", "");
	//	$("#ExceptConstType").css("background-color", "");
	//}

	//修改按钮不存在时 才解除事件
	if ($("#btnModify").css("display") != null) {
		$("#RepairCountFlag").unbind("click").click(function () {
			return false;
		});

		$("#RepairAmountFlag").unbind("click").click(function () {
			return false;
		});
	}

	if ($("#btnModify").css("display") != null) {
		$("#btnSave").hide();
		$("#btnCancel").hide();
	}

	$("#CountA").focus(function () {
		$("#CountA").blur();
	});
	$("#CountB").focus(function () {
		$("#CountB").blur();
	});
	$("#CountC").focus(function () {
		$("#CountC").blur();
	});
	$("#CountD").focus(function () {
		$("#CountD").blur();
	});
	$("#RepairCountD").focus(function () {
		$("#RepairCountD").blur();
	});

	$("#AmountA").focus(function () {
		$("#AmountA").blur();
	});
	$("#AmountB").focus(function () {
		$("#AmountB").blur();
	});
	$("#AmountC").focus(function () {
		$("#AmountC").blur();
	});
	$("#AmountD").focus(function () {
		$("#AmountD").blur();
	});
	$("#RepairAmountD").focus(function () {
		$("#RepairAmountD").blur();
	});

	//当CheckBox在未选中状态下 让TextBox无法获取焦点
	//维修次数
	$("#RepairCountA").focus(function () {
		if ($("#RepairCountFlag").attr("checked") == false) {
			$("#RepairCountA").blur();
		}

		if ($("#btnModify").css("display") != null && $("#btnModify").css("display") != "none") {
			$("#RepairCountA").blur();
		}
	});
	$("#RepairCountA").blur(function () {
		var count_a = parseInt($("#RepairCountA").val());
		if (count_a > 0) {
			$("#CountB").val(count_a - 1);
		} else {
			$("#CountB").val("0");
		}
	});
	$("#RepairCountB").focus(function () {
		if ($("#RepairCountFlag").attr("checked") == false) {
			$("#RepairCountB").blur();
		}

		if ($("#btnModify").css("display") != null && $("#btnModify").css("display") != "none") {
			$("#RepairCountB").blur();
		}
	});
	$("#RepairCountB").blur(function () {
		var count_b = parseInt($("#RepairCountB").val());
		if (count_b > 0) {
			$("#CountC").val(count_b - 1);
		} else {
			$("#CountC").val("0");
		}
	});
	$("#RepairCountC").focus(function () {
		if ($("#RepairCountFlag").attr("checked") == false) {
			$("#RepairCountC").blur();
		}

		if ($("#btnModify").css("display") != null && $("#btnModify").css("display") != "none") {
			$("#RepairCountC").blur();
		}
	});
	$("#RepairCountC").blur(function () {
		var count_c = parseInt($("#RepairCountC").val());
		if (count_c > 0) {
			$("#CountD").val(count_c - 1);
		} else {
			$("#CountD").val("0");
		}
	});

	//维修金额
	$("#RepairAmountA").focus(function () {
		if ($("#RepairAmountFlag").attr("checked") == false) {
			$("#RepairAmountA").blur();
		}

		if ($("#btnModify").css("display") != null && $("#btnModify").css("display") != "none") {
			$("#RepairAmountA").blur();
		}
	});
	$("#RepairAmountA").blur(function () {
		var amount_a = parseFloat($("#RepairAmountA").val());
		if (amount_a > 0) {
			$("#AmountB").val(amount_a - 0.01);
			if (isInt(amount_a - 0.01)) {
				$("#AmountB").val(amount_a - 0.01 + ".00");
			}
		} else {
			$("#AmountB").val("0.00");
		}
	});
	$("#RepairAmountB").focus(function () {
		if ($("#RepairAmountFlag").attr("checked") == false) {
			$("#RepairAmountB").blur();
		}

		if ($("#btnModify").css("display") != null && $("#btnModify").css("display") != "none") {
			$("#RepairAmountB").blur();
		}
	});
	$("#RepairAmountB").blur(function () {
		var amount_b = parseFloat($("#RepairAmountB").val());
		if (amount_b > 0) {
			$("#AmountC").val(amount_b - 0.01);
			if (isInt(amount_b - 0.01)) {
				$("#AmountC").val(amount_b - 0.01 + ".00");
			}
		} else {
			$("#AmountC").val("0.00");
		}
	});
	$("#RepairAmountC").focus(function () {
		if ($("#RepairAmountFlag").attr("checked") == false) {
			$("#RepairAmountC").blur();
		}

		if ($("#btnModify").css("display") != null && $("#btnModify").css("display") != "none") {
			$("#RepairAmountC").blur();
		}
	});
	$("#RepairAmountC").blur(function () {
		var amount_c = parseFloat($("#RepairAmountC").val());
		if (amount_c > 0) {
			$("#AmountD").val(amount_c - 0.01);
			if (isInt(amount_c - 0.01)) {
				$("#AmountD").val(amount_c - 0.01 + ".00");
			}
		} else {
			$("#AmountD").val("0.00");
		}
	});

	//除外条件
	$("#ExceptConstType").focus(function () {
		if ($("#RepairAmountFlag").attr("checked") == false) {
			$("#ExceptConstType").blur();
		}
	});

	//维修次数CheckBox单击事件
	$("#RepairCountFlag").click(function () {
		if ($("#btnModify").css("display") != null) {
			if ($("#btnModify").css("display") == "none") {
				if ($("#RepairCountFlag").attr("checked") == false) {
					$("#RepairCountA").css("background-color", "#FEF6D0");
					$("#RepairCountB").css("background-color", "#FEF6D0");
					$("#RepairCountC").css("background-color", "#FEF6D0");
				} else {
					$("#RepairCountA").css("background-color", "");
					$("#RepairCountB").css("background-color", "");
					$("#RepairCountC").css("background-color", "");
				}
			} else {
				//$("#RepairCountFlag").attr("checked", $("#RepairCountFlag").attr("checked"));
				//alert($g["ClickModify"]);
			}
		} else {
			if ($("#RepairCountFlag").attr("checked") == false) {
				$("#RepairCountA").css("background-color", "#FEF6D0");
				$("#RepairCountB").css("background-color", "#FEF6D0");
				$("#RepairCountC").css("background-color", "#FEF6D0");
			} else {
				$("#RepairCountA").css("background-color", "");
				$("#RepairCountB").css("background-color", "");
				$("#RepairCountC").css("background-color", "");
			}
		}
	});

	//维修金额CheckBox单击事件
	$("#RepairAmountFlag").click(function () {
		if ($("#btnModify").css("display") != null) {
			if ($("#btnModify").css("display") == "none") {
				if ($("#RepairAmountFlag").attr("checked") == false) {
					$("#RepairAmountA").css("background-color", "#FEF6D0");
					$("#RepairAmountB").css("background-color", "#FEF6D0");
					$("#RepairAmountC").css("background-color", "#FEF6D0");
					$("#ExceptConstType").css("background-color", "#FEF6D0");
				} else {
					$("#RepairAmountA").css("background-color", "");
					$("#RepairAmountB").css("background-color", "");
					$("#RepairAmountC").css("background-color", "");
					$("#ExceptConstType").css("background-color", "");
				}
			} else {
				//$("#RepairAmountFlag").attr("checked", false);
				//alert($g["ClickModify"]);
			}
		} else {
			if ($("#RepairAmountFlag").attr("checked") == false) {
				$("#RepairAmountA").css("background-color", "#FEF6D0");
				$("#RepairAmountB").css("background-color", "#FEF6D0");
				$("#RepairAmountC").css("background-color", "#FEF6D0");
				$("#ExceptConstType").css("background-color", "#FEF6D0");
			} else {
				$("#RepairAmountA").css("background-color", "");
				$("#RepairAmountB").css("background-color", "");
				$("#RepairAmountC").css("background-color", "");
				$("#ExceptConstType").css("background-color", "");
			}
		}
	});

	$("#btnModify").click(function () {
		$("#btnModify").hide();
		$("#btnSave").show();
		$("#btnCancel").show();

		if (countFlag == false) {
			$("#RepairCountA").css("background-color", "#FEF6D0");
			$("#RepairCountB").css("background-color", "#FEF6D0");
			$("#RepairCountC").css("background-color", "#FEF6D0");
		} else {
			$("#RepairCountA").css("background-color", "");
			$("#RepairCountB").css("background-color", "");
			$("#RepairCountC").css("background-color", "");
		}

		if (amountFlag == false) {
			$("#RepairAmountA").css("background-color", "#FEF6D0");
			$("#RepairAmountB").css("background-color", "#FEF6D0");
			$("#RepairAmountC").css("background-color", "#FEF6D0");
			$("#ExceptConstType").css("background-color", "#FEF6D0");
		} else {
			$("#RepairAmountA").css("background-color", "");
			$("#RepairAmountB").css("background-color", "");
			$("#RepairAmountC").css("background-color", "");
			$("#ExceptConstType").css("background-color", "");
		}

		$("#RepairCountFlag").unbind("click").click(function () {
			ValidateCountFlag();
			return true;
		});

		$("#RepairAmountFlag").unbind("click").click(function () {
			ValidateAmountFlag();
			return true;        });
        //弹出确认框(还有未保存数据,确定离开此页面？)         lastFormHtml = $(window.document.body).find("form").formSerialize();
        window.checkChanged = true;

		//		repairCountA = $("#RepairCountA").val();
		//		repairCountB = $("#RepairCountB").val();
		//		repairCountC = $("#RepairCountC").val();
		//		countB = $("#CountB").val();
		//		countC = $("#CountC").val();
		//		countD = $("#CountD").val();

		//		repairAmountA = $("#RepairAmountA").val();
		//		repairAmountB = $("#RepairAmountB").val();
		//		repairAmountC = $("#RepairAmountC").val();
		//		amountB = $("#AmountB").val();
		//		amountC = $("#AmountC").val();
		//		amountD = $("#AmountD").val();

		//		countFlag = $("#RepairCountFlag").attr("checked");

		//		except = $("#ExceptConstType").val();
	});

	$("#btnCancel").click(function () {
		$("#btnModify").show();
		$("#btnSave").hide();
		$("#btnCancel").hide();

		$("#RepairCountFlag").unbind("click").click(function () {
			return false;
		});

		$("#RepairAmountFlag").unbind("click").click(function () {
			return false;
		});

		//$("#RepairCountFlag").attr("checked", false);
		//$("#RepairAmountFlag").attr("checked", false);

		$("#RepairCountA").val(repairCountA);
		$("#RepairCountB").val(repairCountB);
		$("#RepairCountC").val(repairCountC);
		$("#CountB").val(countB);
		$("#CountC").val(countC);
		$("#CountD").val(countD);

		$("#RepairAmountA").val(repairAmountA);
		$("#RepairAmountB").val(repairAmountB);
		$("#RepairAmountC").val(repairAmountC);
		$("#AmountB").val(amountB);
		$("#AmountC").val(amountC);
		$("#AmountD").val(amountD);
		$("#ExceptConstType").val(except);
		$("#RepairCountFlag").attr("checked", countFlag);
		$("#RepairAmountFlag").attr("checked", amountFlag);

		$("#RepairCountA").css("background-color", "#FEF6D0");
		$("#RepairCountB").css("background-color", "#FEF6D0");
		$("#RepairCountC").css("background-color", "#FEF6D0");

		$("#RepairAmountA").css("background-color", "#FEF6D0");
		$("#RepairAmountB").css("background-color", "#FEF6D0");
		$("#RepairAmountC").css("background-color", "#FEF6D0");
		$("#ExceptConstType").css("background-color", "#FEF6D0");

		//if (countFlag == false) {
		//	$("#RepairCountA").css("background-color", "#FEF6D0");
		//	$("#RepairCountB").css("background-color", "#FEF6D0");
		//	$("#RepairCountC").css("background-color", "#FEF6D0");
		//} else {
		//	$("#RepairCountA").css("background-color", "");
		//	$("#RepairCountB").css("background-color", "");
		//	$("#RepairCountC").css("background-color", "");
		//}

		//if (amountFlag == false) {
		//	$("#RepairAmountA").css("background-color", "#FEF6D0");
		//	$("#RepairAmountB").css("background-color", "#FEF6D0");
		//	$("#RepairAmountC").css("background-color", "#FEF6D0");
		//	$("#ExceptConstType").css("background-color", "#FEF6D0");
		//} else {
		//	$("#RepairAmountA").css("background-color", "");
		//	$("#RepairAmountB").css("background-color", "");
		//	$("#RepairAmountC").css("background-color", "");
		//	$("#ExceptConstType").css("background-color", "");
		//}
	});

	//added by jason in 20120416 for bug CVF03010#001.doc
	//维修次数CheckBox初始化信息
	if ($("#btnModify").css("display") == null) {
		if ($("#RepairCountFlag").attr("checked") == false) {
			$("#RepairCountA").css("background-color", "#FEF6D0");
			$("#RepairCountB").css("background-color", "#FEF6D0");
			$("#RepairCountC").css("background-color", "#FEF6D0");
		} else {
			$("#RepairCountA").css("background-color", "");
			$("#RepairCountB").css("background-color", "");
			$("#RepairCountC").css("background-color", "");
		}
	}
	//维修金额CheckBox初始化信息
	if ($("#btnModify").css("display") == null) {
		if ($("#RepairAmountFlag").attr("checked") == false) {
			$("#RepairAmountA").css("background-color", "#FEF6D0");
			$("#RepairAmountB").css("background-color", "#FEF6D0");
			$("#RepairAmountC").css("background-color", "#FEF6D0");
			$("#ExceptConstType").css("background-color", "#FEF6D0");
		} else {
			$("#RepairAmountA").css("background-color", "");
			$("#RepairAmountB").css("background-color", "");
			$("#RepairAmountC").css("background-color", "");
			$("#ExceptConstType").css("background-color", "");
		}
	}
	//end added by jason in 20120416 bug CVF03010#001.doc
})

//验证表单数据的合法性
function ValidateForm() {
	var count_a = parseInt($("#RepairCountA").val());
	var count_b = parseInt($("#RepairCountB").val());
	var count_c = parseInt($("#RepairCountC").val());

	var amount_a = parseFloat($("#RepairAmountA").val());
	var amount_b = parseFloat($("#RepairAmountB").val());
	var amount_c = parseFloat($("#RepairAmountC").val()); 

	if ($("#GcMake") != undefined && $("#GcMake").val() == "") {
		$("#V_GcMake").text($g["GcMake"]);
		return false;
	}

    //alert($("#RepairCountFlag").attr("checked") + "--" + $("#RepairAmountFlag").attr("checked"));
    /*
    if ($("#RepairCountFlag").attr("checked") == false && (count_a != 0 && count_b != 0 && count_c != 0)) {
        alert($g["CheckedRepairCount"]);
        return false;
    }

    if ($("#RepairAmountFlag").attr("checked") == false && (amount_a != 0 && amount_b != 0 && amount_c != 0)) {
        alert($g["CheckedRepairAmount"]);
        return false;
    }

    if(count_a == 0 || count_b == 0 || count_c == 0 || amount_a == 0 || amount_b == 0 || amount_c == 0){
        alert($g["GreaterThanZero"]);
        return false;
    }
    */

    if ($("#RepairCountFlag").attr("checked") == true && (count_a == 0 || count_b == 0 || count_c == 0)) {
        alert($g["GreaterThanZero"]);
        return false;
    }

    if ($("#RepairAmountFlag").attr("checked") == true && (amount_a == 0 || amount_b == 0 || amount_c == 0)) {
        alert($g["GreaterThanZero"]);
        return false;
    }

	if ((count_b >= count_a && count_a !=0) || (count_c >= count_b && count_b != 0)) {
		alert($g["RepairCount"]);
		return false;
	}

	if ((amount_b >= amount_a && amount_a != 0) || (amount_c >= amount_b && amount_b != 0)) {
		alert($g["RepairAmount"]);
		return false;
    }

    if (!confirm($g["ConfirmSave"])) { 
        return false;
    }

	return true;
}

//判断是否是整型数字
function isInt(str) {
	var reg = /^(-|\+)?\d+$/;
	return reg.test(str);
}

//判断是否是浮点型数字
function isFloat(str) {
	var reg = /^(-|\+)?\d+\.\d*$/;
	return reg.test(str);
}

function ValidateCountFlag() {
	if ($("#btnModify").css("display") != null) {
		if ($("#btnModify").css("display") == "none") {
			if ($("#RepairCountFlag").attr("checked") == false) {
				$("#RepairCountA").css("background-color", "#FEF6D0");
				$("#RepairCountB").css("background-color", "#FEF6D0");
				$("#RepairCountC").css("background-color", "#FEF6D0");
			} else {
				$("#RepairCountA").css("background-color", "");
				$("#RepairCountB").css("background-color", "");
				$("#RepairCountC").css("background-color", "");
			}
		} else {
			//$("#RepairCountFlag").attr("checked", $("#RepairCountFlag").attr("checked"));
			//alert($g["ClickModify"]);
		}
	} else {
		if ($("#RepairCountFlag").attr("checked") == false) {
			$("#RepairCountA").css("background-color", "#FEF6D0");
			$("#RepairCountB").css("background-color", "#FEF6D0");
			$("#RepairCountC").css("background-color", "#FEF6D0");
		} else {
			$("#RepairCountA").css("background-color", "");
			$("#RepairCountB").css("background-color", "");
			$("#RepairCountC").css("background-color", "");
		}
	}
}

function ValidateAmountFlag() {
	if ($("#btnModify").css("display") != null) {
		if ($("#btnModify").css("display") == "none") {
			if ($("#RepairAmountFlag").attr("checked") == false) {
				$("#RepairAmountA").css("background-color", "#FEF6D0");
				$("#RepairAmountB").css("background-color", "#FEF6D0");
				$("#RepairAmountC").css("background-color", "#FEF6D0");
				$("#ExceptConstType").css("background-color", "#FEF6D0");
			} else {
				$("#RepairAmountA").css("background-color", "");
				$("#RepairAmountB").css("background-color", "");
				$("#RepairAmountC").css("background-color", "");
				$("#ExceptConstType").css("background-color", "");
			}
		} else {
			//$("#RepairAmountFlag").attr("checked", false);
			//alert($g["ClickModify"]);
		}
	} else {
		if ($("#RepairAmountFlag").attr("checked") == false) {
			$("#RepairAmountA").css("background-color", "#FEF6D0");
			$("#RepairAmountB").css("background-color", "#FEF6D0");
			$("#RepairAmountC").css("background-color", "#FEF6D0");
			$("#ExceptConstType").css("background-color", "#FEF6D0");
		} else {
			$("#RepairAmountA").css("background-color", "");
			$("#RepairAmountB").css("background-color", "");
			$("#RepairAmountC").css("background-color", "");
			$("#ExceptConstType").css("background-color", "");
		}
	}
}
