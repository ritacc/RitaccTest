/// <reference Path="jquery-1.4.1-vsdoc.js" />
(function () {
	$.tabs = function (options) {
		var defaults = {
			element: null,
			index: 0,
			onselect: null,
			onleave: null
		};
		var settings = $.extend(defaults, options);
		this.element = settings.element;
		this.index = settings.index;
		this.onselect = settings.onselect;
		this.onleave = settings.onleave;
	};

	$.tabs.prototype = {
		_init: function () {
			var s = this, o = $(s.element); 
			var ul = o.children("ul"), li = ul.children("li"), a = li.find("a"), div = o.children("div").not(".tab_top");
			ul.addClass("tabs");
			div.addClass("tab_content");
			li.die().click(function (e) {
				var index = li.index($(this)), divcur = div.eq(index);
				var isContinue = true;
				if (s.onselect && $.isFunction(s.onselect)) {
					isContinue = s.onselect(divcur, index);
				}
				if (isContinue == undefined)
					isContinue = true;
				if (!isContinue)
					return false;
				li.removeClass("active");
				$(this).addClass("active");
				div.hide();
				divcur.show();
				var href = window.location.href.replace(/#tab-\d*/g, '');
				window.location.href = href + "#tab-" + index;

				if (s.onleave && $.isFunction(s.onleave)) {  //add by lm
					s.onleave(divcur, index);
				}

				e.preventDefault();
			}).eq(s.index).click();
		}
	};

	$.fn.tabs = function (options) {
		var tabIndex = 0;
		if (options) {
			tabIndex = options.index;
			if (options.enableGoToPageFromUrl) {
				var matched = window.location.href.match(/#tab-(\d*)/);
				if (matched && matched.length)
					tabIndex = matched[1];
			}
		}
		new $.tabs($.extend({ element: $(this), index: (tabIndex ? parseInt(tabIndex) : 0) }, options))._init();
	};
})();