var lastFormHtml = "";
var getTopWin = function () {
	var win = window;
	while (win != win.parent) {
		win = win.parent;
	}
	return win;
};
window.checkChanged = true;
window.onbeforeunload = function () {
	var win = getTopWin();
	/*if (win == window || !win.checked) {
	win.checked = true;*/
	if ($.fn.formSerialize) {
		if (window.checkChanged) {
			var html = $(window.document.body).find("form").formSerialize();
			if (html != lastFormHtml) {
				return $g["SureLeave"] || "你还有数据没有保存，确定离开此页吗 ?";
			}
		}
		//}

	}
};

$(function () {
	$(":submit,:button").click(function () {
		window.checkChanged = false;
	});
	var form = $(window.document.body).find("form");
	if (form.parents("div.search").length > 0) {
		window.checkChanged = false;
		return;
	}
	if ($.fn.formSerialize) {
		lastFormHtml = form.formSerialize();
	}


});