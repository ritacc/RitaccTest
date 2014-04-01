/*
*为需要加载联系人的页面绑定事件
*依赖于jQuery 
*by xh 2012
*/
$(function () {
	var fun = function () {
		var aTag = $("<a>", { href: "#" });
		var _td = $(this);
		var innerHtml = _td.html();
		_td.html("");

		aTag.html(innerHtml);
		aTag.click(function () {
			var phoneNumber = _td.text();
			var areaCode = null;
			if (_td.hasClass("contactTel")) {
				areaCode = _td.siblings(".contactAreaCode").text();
			}
			var contactId = _td.siblings().eq(0).find("input[name='CUTC_ID']").val();
			var custId = _td.siblings().eq(0).find("input[name='CUST_ID']").val();
			window.parent.parent.loadAttractTargetByPhone(phoneNumber, false, areaCode, null, null, null, null, null, true);
		});
		return aTag;
	};

	$(".phone_no").append(fun);

	$(".contactPhone").append(fun);
	$(".contactTel").append(fun);

	$("#btnSelect").click(function () {
		var cbs = $(":checkbox:checked[name='cbKey']:not(disabled)");
		if (cbs.length != 1) {
			alert($g["SelectSingle"]);
			return false;
		}
		var contactId = cbs.parent().find("input[name='CUTC_ID']").val();
		var custId = cbs.parent().find("input[name='CUST_ID']").val();
		window.parent.parent.loadAttractTargetByPhone(null, true, null, contactId, custId, null, null, null, false, null, null, true);
		window.parent.parent.$(".div_shade_dialog").remove();
		window.parent.parent.$(".div_container_dialog").remove();
	});
});