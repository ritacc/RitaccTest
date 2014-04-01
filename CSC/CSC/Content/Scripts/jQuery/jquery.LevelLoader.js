(function () {
	$.LevelLoader = function (options) {
		var defaults = {
			element: null,
			getList: function () { },
			getItem: function () { },
			itemCount: 0,
			autoAddDefaultItem: true,
			//defaultItem: '<option value="-1"></option>',
			defaultItem: '<option></option>',
			defaultValue: '',
			htmlType: '<select>',
			loaderIdPrix: 'levelLoader',
			wrapTag: '<div>',
			data: [],
			autoLoad: false
		};
		this.settings = $.extend(defaults, options);

	};
	$.LevelLoader.prototype = {
		_loadData: function (tmplevelUrl, tmplevelObj, postData) {
			var _settings = this.settings;
			var t = new Date().getTime();
			$.getJSON(tmplevelUrl.indexOf('?') >= 0 ? tmplevelUrl + "&t=" + t : tmplevelUrl + "?t=" + t, postData, function (data) {
				var items = [];
				var list = _settings.getList(data);
				//  alert(list);
				$.each(list, function (i, item) {
					items.push(_settings.getItem(item));
				});
				var html = _settings.autoAddDefaultItem ? _settings.defaultItem + items.join('') : items.join('');
				tmplevelObj.html(html);
				tmplevelObj.change();
				if (_settings.callback) {
					_settings.callback();
				}
			});
		},
		_init: function () {
			var _this = this;
			var _settings = this.settings;
			var _element = _settings.element;
			var urlAttr = 'childUrl';
			var loaderIdPrix = _settings.loaderIdPrix;
			$.each(_settings.data, function (i, dataItem) {

				var htmlObjId = dataItem.htmlObj ? dataItem.htmlObj : loaderIdPrix + i;
				var levelObj = $('#' + htmlObjId);

				var hiddenForSelected = $('<input>', { type: 'hidden', id: 'hiddenPostName' + i, name: dataItem.postName });
				_element.append(hiddenForSelected);

				if (levelObj.length < 1) {
					levelObj = $(_settings.htmlType, { id: htmlObjId });
					var wrap = $(_settings.wrapTag);
					wrap.append(levelObj);
					_element.append(wrap);
				}

				levelObj.change(function () {
					var val = $(this).val();
					var tmpHidden = $('#hiddenPostName' + i);
					tmpHidden.val(val);
					if (i >= _settings.data.length - 1)
						return;
					var nextDataItem = _settings.data[i + 1];
					var childHtmlObjId = nextDataItem.htmlObj ? nextDataItem.htmlObj : loaderIdPrix + (i + 1);
					var childLevelObj = $('#' + childHtmlObjId);
					if (val == _settings.defaultValue) {
						childLevelObj.empty();
						childLevelObj.change();
					}
					else {
						var tmp = "";
						if (dataItem.parentKey != undefined && dataItem.parentObj != undefined) {
							var vall = $('#' + dataItem.parentObj).val();
							tmp = '({"' + dataItem.postKey + '":"' + val + '","' + dataItem.parentKey + '":"' + vall + '"})';
						} else {
							tmp = '({"' + dataItem.postKey + '":"' + val + '"})';
						}
						var postData = eval(tmp);
						_this._loadData(nextDataItem.url, childLevelObj, postData);
					}
				});

				if (i == 0 && _settings.autoLoad)
					_this._loadData(dataItem.url, levelObj, {});

			});

		}
	};

	$.fn.LevelLoader = function (options) {
		var levelLoader = new $.LevelLoader($.extend({ element: $(this) }, options));
		levelLoader._init();
		return levelLoader;
	};
})();

