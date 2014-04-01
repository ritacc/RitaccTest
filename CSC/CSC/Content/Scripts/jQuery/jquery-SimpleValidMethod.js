var happy = {
	USPhone: function (val) {
		return /^\(?(\d{3})\)?[\- ]?\d{3}[\- ]?\d{4}$/.test(val)
	},

	// matches mm/dd/yyyy (requires leading 0's (which may be a bit silly, what do you think?)
	date: function (val) {
		return /^(?:0[1-9]|1[0-2])\/(?:0[1-9]|[12][0-9]|3[01])\/(?:\d{4})/.test(val);
	},

	email: function (val) {
		return /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/.test(val);
	},

	minLength: function (val, length) {
		return val.length >= length;
	},

	maxLength: function (val, length) {
		return val.length <= length;
	},

	equal: function (val1, val2) {
		return (val1 == val2);
	},
	isNum: function (val) {
		return !isNaN(val);
	},
	isInt: function (val) {
		return /^([1-9]\d*|0)$/.test(val);
	},
	matchDate: function (strDate1, jSelector) {
		var strDate2 = $(jSelector).val();
		var date1 = new Date(strDate1.replace(/\-/g, "\/"));
		var date2 = new Date(strDate2.replace(/\-/g, "\/"));
		return date1 >= date2;
	},
	sepciallMatchDate: function (strDate1, jSelector) {
		var strDate2 = $(jSelector).val();

		var regDateFomart = /^(\d{2})-(\d{2})-(\d{4})$/  //特定的格式化 形如 dd-mm-yyyy
		strDate1 = strDate1.replace(regDateFomart, "$3/$2/$1");
		strDate2 = strDate2.replace(regDateFomart, "$3/$2/$1");

		var date1 = new Date(strDate1);
		var date2 = new Date(strDate2);
		return date1 >= date2;
	}
};

//强制class=validNum的Textbox,只能输入数字
function InitValidNum() {
	$(".validNum").keydown(function (event) {
		if ($.browser.msie) {
			if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 190 || (event.keyCode > 95 && event.keyCode < 106) || event.keyCode == 110)
				return true;
			else
				return false;
		} else {
			if ((event.which > 47 && event.which < 58) || event.which == 8 || event.which == 46 || event.which == 190 || event.which == 46 || (event.which > 95 && event.which < 106) || event.which == 110 || event.which == 17)
				return true;
			else
				return false;
		}
	}).focus(function () {
		this.style.imeMode = "disabled";
	})
//	.blur(function () {
//		var reg = /(^[1-9]+([.][0-9]{1,2})?$)|(^0([.][0-9]{1,2})?$)/gi;
//		var name = $(this).attr("name") + "valid";

//		if ($(this).val() != "" && !reg.test($(this).val())) {
//			if ($("#" + name).length == 0)
//				$(this).after($("<span id='" + name + "' class='unhappyMessage'>Not a Number</span>"));
//		} else {
//			if ($("#" + name).length > 0)
//				$("#" + name).remove();
//		}
//	});
}