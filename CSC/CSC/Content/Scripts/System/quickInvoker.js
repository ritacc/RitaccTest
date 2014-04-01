/*
*快速调用
*依赖于jQuery 
*by xh 2012
*/
var quickInvoker = (function () {
	var _stopwatch = null;
	return {
		disableCtl: function (idstr) {
			var arr = idstr.split(',');
			$.each(arr, function (i) {
				$("#" + arr[i] + "").attr("disabled", true);

			});
		},
		enableCtl: function (idstr) {
			var arr = idstr.split(',');
			$.each(arr, function (i) {
				$("#" + arr[i] + "").removeAttr("disabled");
			});
		},
		stopwatchStart: function () {
			if (_stopwatch) {
				_stopwatch.stop();
			}
			_stopwatch = stopwatch("txtCallTime");
			_stopwatch.start();
		},
		stopwatchStop: function () {
			if (_stopwatch) {
				_stopwatch.stop();
			}
		},
		catchDo: function (call, exCall) {
			try {
				call();
			} catch (e) {
				if (exCall) {
					exCall(e);
				}
			}
		}
	}
})();