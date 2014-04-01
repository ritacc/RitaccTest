/// <reference Path="jquery-1.4.1-vsdoc.js" />
(function () { // change 事件被 changed 事件代替
	$.numericTextbox = function (options) {
		var defaults = {
			element: null, // 绑定元素
			maxValue: 100000000000, // 最大值
			minValue: -100000000000, // 最小值
			digits: 0, // 小数位数
			step: 1, // 递增(减)步长, 可为小数
			hideUpDownBtn: false
		};
		var settings = $.extend(defaults, options);
		this.element = settings.element;
		this.maxValue = settings.maxValue;
		this.minValue = settings.minValue;
		this.digits = settings.digits;
		this.intlen=8;
		this.step = settings.step;
		this.hideUpDownBtn = settings.hideUpDownBtn;
		this.oldValue="";
	};
	$.numericTextbox.prototype = {
		_init: function () {

			var s = this;
			if (s.digits == 0) {//如果小数位数为0，则将值转为int
				var number = parseInt(s.element.val());
				if (!isNaN(number))
					s.element.val(number);
			}

			if (s.maxValue > 0 && s.element.attr("maxlength") == 2147483647) { //根据最大值的算出最大宽度
				var len = (parseInt(s.maxValue) + "").length; //直接忽略掉小点
				if (parseInt(s.digits) > 0)
					len += parseInt(s.digits) + 1;
				s.element.attr("maxlength", len);
			}

			if(s.element.attr("maxlength") && s.digits)
			{
				var cLen=0;
				var mdig=parseInt(s.digits);
				if(mdig >0){
					cLen = mdig + 1;
				}								 	
				var maxLen=parseInt(s.element.attr("maxlength"));
				this.intlen = maxLen - cLen;
			}
			 

			var divContainer = s.element.wrap('<div>').parent()
                .attr("class", "div_container_numeric").css({
                	width: s.element.outerWidth(),
                	height: s.element.outerHeight()
                }).bind({
                	dblclick: s._preventDefault,
                	click: s._preventDefault,
                	//selectstart: s._preventDefault,
                	dragstart: s._preventDefault
                });
			var events = {
				mouseup: s._delegate(s, s._clearTimer),
				mouseout: s._delegate(s, s._clearTimer),
				dblclick: s._delegate(s, s._clearTimer),
				click: s._preventDefault,
				dragstart: s._preventDefault
			};

			if (s.hideUpDownBtn != "True") {
				var aUp = $("<a>", { "class": "a_up_numeric" }).appendTo(divContainer);
				var aDown = $("<a>", { "class": "a_down_numeric" }).appendTo(divContainer)
				var isDidsabled = (this.element.attr("disabled") || this.element.attr("readonly"));
				if (!isDidsabled) {
					aUp.bind(events).mousedown($.proxy(function (e) { this._step(e, 1); }, s));
					aDown.bind(events).mousedown($.proxy(function (e) { this._step(e, -1); }, s));
				}
			}
			//s._modifyInput(s.element, 0);
			s.element.bind({
				keydown: s._delegate(s, s._keydown),
				focus: s._delegate(s, s._focus),
				blur: s._delegate(s, s._blur),
				keyup: s._delegate(s, s._keyup),
			});
		},
		_preventDefault: function (e) { // 阻止默认行为
			e.preventDefault();
		},
		_delegate: function (context, handler) { // 绑定事件
			return function (e) {
				handler.apply(context, [e, this]);
			};
		},
		_inRange: function (key, min, max) {
			return (min !== null ? key >= min : true) && (max !== null ? key <= max : true);
		},
		_keyup: function(e)
		{
			var $input = this.element;
			if (/(^0+)/.test($input.val())) {
   				$input.val($input.val().replace(/^0*/, '0'));
   			}
			if($input.val().indexOf(".") > this.intlen )
			{
				$input.val(this.oldValue);
			}
		},
		_keydown: function (e) {
			var separator = { "110": ".", "190": ".", "189": "-","109":"-" };
			var allowKeyCodes = [8, 9, 35, 36, 37, 38, 39, 40, 46, 109, 110, 189, 190];
			var keyCode = e.which;
			 
			var $input = this.element;
			
			if (keyCode == 38 || keyCode == 40) {
				this._modifyInput($input, this.step * (keyCode == 38 ? 1 : -1));
				return true;
			}
			
			if (separator[keyCode] == "-" && this.minValue >= 0) {
				e.preventDefault();
				return false;
			}

			if (separator[keyCode] === ".") {
				if ($input.val().length > 0 && this.digits > 0 && $input.val().indexOf(".") == -1) {
					return true;
				} else {
					e.preventDefault();
				}
			}
			
			if($input.val().indexOf(".") == -1 )
			{
				if($input.val().length >= this.intlen )
				{
                    //var rng = window.getSelection(); //非IE 获取选中文本内容
                    var rng = document.selection.createRange(); //只针对IE 获取选中文本内容

					if( keyCode != 110 && keyCode != 190 && keyCode != 8 && rng.text == "")
					{
						e.preventDefault();
						return false;
					}
				}
			}
			 
			var flag = this._inRange(keyCode, 48, 57)
                || this._inRange(keyCode, 96, 105)
                || $.inArray(keyCode, allowKeyCodes) != -1;
			if (!flag) { e.preventDefault(); }
			this.oldValue=$input.val();
		},
		_focus: function () {
			this.element.select();
		},
		_blur: function () {
			this._modifyInput(this.element, 0);
		},
		_step: function (e, stepMod) {
			if (e.which == 1) {
				var input = this.element;
				var step = this.step;

				this._modifyInput(input, stepMod * step);

				this.timeout = setTimeout($t.delegate(this, function () {
					this.timer = setInterval($t.delegate(this, function () {
						this._modifyInput(input, stepMod * step);
					}), 80);
					this.acceleration = setInterval(function () { step += 1; }, 1000);
				}), 200);
			}
		},
		_clearTimer: function (e) {
			clearTimeout(this.timeout);
			clearInterval(this.timer);
			clearInterval(this.acceleration);
		},
		_modifyInput: function ($input, step) {
			if (this.element.val() == "") {
				return;
			} //by xh

			//Modify by JLD on 2012-11-27
			//保持原来的数字框验证
			var className = this.element.attr("class");

			//if (className == "numeric") {

			if (className.indexOf("decimal_numeric") != -1) {
				//不做任何操作
			} else if (className.indexOf("numeric") != -1) { //by lm
				var value = this._parse(this.element.val());

				var min = this.minValue;
				var max = this.maxValue;

				value = value ? value + step : step;
				//value = (min !== null && value < min) ? min : (max !== null && value > max) ? max : value;

				var fixedValue = this._round(value, this.digits);
				$input.val(fixedValue);
				$input.trigger("changed");
			}
		},
		_round: function (value, digits) {
			if (value || value == 0) {
				return this._parse(value).toFixed(digits);
			}
			return null;
		},
		_parse: function (value, separator) {
			var result = null;
			if (value || value == "0") {
				if (typeof value == typeof 1) { return value; }
				if (separator && separator != '.') {
					value = value.replace(separator, '.');
				}
				result = parseFloat(value);
			}
			return isNaN(result) ? null : result;
		},
		refresh: function (options) {
			var defaults = {
				maxValue: 100000000000, // 最大值
				minValue: -100000000000, // 最小值
				digits: 0, // 小数位数
				step: 1 // 递增(减)步长, 可为小数
			};
			var settings = $.extend(defaults, options);
			this.maxValue = settings.maxValue;
			this.minValue = settings.minValue;
			this.digits = settings.digits;
			this.step = settings.step;
			this._modifyInput(this.element, 0);
		}
	}

	$.fn.numericTextbox = function (options) {
		var ins = new $.numericTextbox($.extend({ element: $(this) }, options));
		ins._init();
		return ins;
	}
})();
