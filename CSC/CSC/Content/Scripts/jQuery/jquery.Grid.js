/// <reference Path="jquery-1.4.1-vsdoc.js" />
(function () {
	$.grid = function (options) {
		var defaults = {
			div: null,
			scroll: 15,
			isAjax: false
		};
		var settings = $.extend(defaults, options);
		this.div = settings.div;
		this.scroll = settings.scroll;
		this.isAjax = settings.isAjax;
	};

	$.grid.prototype = {
		_delegate: function (context, handler) { // 绑定事件
			return function (e) {
				handler.apply(context, [e, this]);
			};
		},
		_init: function () {
			// 表头处理
			var s = this;
			var divc = $(this.div);
			var divb = $("<div>", { "class": "grid_bg" }).appendTo(divc);
			var told = divc.find("table");
			var oldths = told.find("thead > tr > th");
			var tnew = $("<table>", { "class": "grid_title" });
			var head = told.find("thead").clone().appendTo(tnew);
			tnew.insertBefore(told);
			// scroll bar
			var v = $("<div>", { "class": "grid_v" }).appendTo(divc).height(divc.height() - s.scroll);
			var vm = $("<div>").appendTo(v).css({ height: told.outerHeight(), width: 1 });
			var h = $("<div>", { "class": "grid_h" }).appendTo(divc).width(divc.width() - s.scroll);
			var hm = $("<div>").appendTo(h).css({ width: told.outerWidth(), height: 1 });
			this._divc = divc; this._told = told; this._v = v; this._vm = vm; this._h = h; this._hm = hm;
			$(v).hide().scroll(function () {
				told.css("top", 0 - $(this).scrollTop());
				//备注:表格的outer高度+ 如果 横向滚动条有的话,就是s.scroll(17px)
				//如果<= grid_list的高度的话,就隐藏，否则显示
				var hScro = ($(h).is(":hidden") ? 0 : s.scroll);
				if ((told.outerHeight() + hScro) <= divc.height()) {
					$(this).hide();
				} else {
					$(this).show();
					vm.height(told.outerHeight());
					v.height(divc.height() - hScro);
				}
			}).scroll();
			$(h).hide().scroll(function () {
				told.css("left", 0 - $(this).scrollLeft());
				tnew.css("left", 0 - $(this).scrollLeft());
				//同理
				var vScro = ($(v).is(":hidden") ? 0 : s.scroll);
				if ((told.outerWidth()) <= divc.width()) { // if ((told.outerWidth() + vScro) <= divc.width()) {
					$(this).hide();
				} else {
					$(this).show();
					h.width(divc.width()); //- vScro
				}
			}).scroll();
			// 改变列宽
			head.find("th").each(function (i, n) {
				$(n).bind({
					mousemove: s._delegate(n, _mouse.move),
					mousedown: s._delegate(n, _mouse.down),
					mouseup: s._delegate(n, _mouse.up)
				}).data("th", oldths.eq(i));
			});
			divc.bind({
				mousemove: function (e) {
					s._delegate(this, _mouse.Move)(e),
                    hm.width(told.outerWidth()).scroll();
					vm.width(told.outerHeight()).scroll();
					h.scroll(); v.scroll();
				},
				mouseup: s._delegate(divc, _mouse.Up)
			});
			_mouse.Wheel(divc, v, s.scroll);
			// ajax 引用
			if (this.isAjax) {
				var s = this;
				divc.find("a").click(function (e) {
					$.post($(this).attr("href"), null, function (html) {
						divc.html(html);
						$t.renderTable(s.div);
						divc.grid({ scroll: s.scroll, isAjax: s.isAjax });
					});
					e.preventDefault();
				});
			}
			// 改变窗口大小
			$(window).resize(function (e) { divc.mousemove(); });
		},
		refresh: function () {
			this._divc.mousemove();
		}
	}
	var _mouse = { // 鼠标事件
		params: {},
		move: function (e) {
			var s = this, x = e.pageX, ow = $(s).outerWidth(), os = $(s).offset();
			if (os.left + ow >= x && (os.left + ow - x) <= 6) { $(s).addClass("grid_move"); }
			else { $(s).removeClass("grid_move"); }
		},
		down: function (e) {
			if ($(this).hasClass("grid_move")) {
				document.onselectstart = function () { return false; }
				_mouse.params = {
					flag: e.target === this,
					X: $(this).width() - e.pageX,
					target: this
				};
				if (this.setCapture) { this.setCapture(); }
			}
		},
		up: function () {
			_mouse.params = {};
			document.onselectstart = {};
			$(this).removeClass("grid_move");
			if (this.releaseCapture) { this.releaseCapture(); }
		},
		Up: function () {
			_mouse.params = {};
			$(this).css("cursor", "default");
		},
		Move: function (e) {
			if (_mouse.params.flag) {
//				var buttonFlag = e.originalEvent.button <= 1 && $.browser.msie
				//|| e.originalEvent.button == 0 && (typeof ($.browser.mozilla) != "undefined" && $.browser.mozilla);
				var buttonFlag = e.originalEvent.button <= 1 && $.browser.msie
					|| (e.originalEvent.button == 0 && (typeof ($.browser.mozilla) != "undefined" && $.browser.mozilla))
					|| (e.originalEvent.button == 0 && ($.browser.webkit && !!window.chrome))
					|| (e.originalEvent.button == 0 && ($.browser.webkit && !window.chrome));
				if (buttonFlag) {
					$(this).css("cursor", "e-resize");
					var w = _mouse.params.X + e.pageX, ow = w <= 10 ? 10 : w;
					$(_mouse.params.target).width(ow).data("th").width(ow);

				}
			}
		},
		Wheel: function (s, v, l) { // 滚轮事件
			var wheel = function (e) {
				var dis, direct = 0, evt = e || window.event, w = h = 0;
				if (evt.wheelDelta) {
					direct = evt.wheelDelta > 0 ? 1 : -1;
				} else if (evt.detail) {
					direct = evt.detail < 0 ? 1 : -1;
				}
				var st = v.scrollTop();
				v.scrollTop(st += (direct == 1 ? 0 - l * 2 : l * 2));
			};
			s.bind($.browser.msie ? "mousewheel" : "DOMMouseScroll", wheel);
		}
	}

	$.fn.grid = function (options) {
		var _grid = new $.grid($.extend({ div: $(this) }, options));
		_grid._init();
		return _grid;
	}
})();