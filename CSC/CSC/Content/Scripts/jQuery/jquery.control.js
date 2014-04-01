//by lm
(function ($) {
	//$.InputOrLabelTag({type:"label",name:'aa',cssClass:"cc1",value:'2222'})
	$.InputOrLabelTag = function (options) {
		var defaults = {
			type: "input",
			name: "",
			id: "",
			cssClass: "",
			value: ""
		};
		var settings = $.extend(defaults, options);
		if (settings.name != "" && settings.id == "")
			settings.id = settings.name;

		var InputTag = function () {
			var input = "";
			if (settings.cssClass != "")
				input = "<input type='text' name='" + settings.name + "' class='" + settings.cssClass + "' id='" + settings.id + "' value='" + settings.value + "' />"
			else
				input = "<input type='text' name='" + settings.name + "' id='" + settings.id + "' value='" + settings.value + "' />"
			return input;
		}
		var labelTag = function () {
			var label = "";
			if (settings.cssClass != "")
				label = "<label class='label_readonly " + settings.cssClass + "' id='" + settings.id + "'>" + settings.value + "</label>";
			else
				label = "<label class='label_readonly w100' id='" + settings.id + "'>" + settings.value + "</label>";
			return label;
		}
		return settings.type == "input" ? InputTag() : labelTag();
	}

	//$.DropDownTag({name:"dd",id:"dd1",cssClass:"cc1",value:"2",dataSource:[{text:'aa1',value:'1'},{text:'aa2',value:'2'}]})
	$.DropDownTag = function (options) {
		var defaults = {
			name: "",
			id: "",
			cssClass: "",
			value: "",
			dataSource: null // [{text:'',value:''},{text:'',value:''}]
		}
		var settings = $.extend(defaults, options);

		if (settings.name != "" && settings.id == "")
			settings.id = settings.name;
		var DllTag = function () {
			var ddl = "<select name='" + settings.name + "' id='" + settings.id + "' class='" + settings.cssClass + "'>";
			if (settings.dataSource != null) {
				for (var item in settings.dataSource) {
					if (settings.value != "")
						ddl += "<option value='" + (settings.dataSource)[item].value + "' selected='selected'>" + (settings.dataSource)[item].text + "</option>";
					else
						ddl += "<option value='" + (settings.dataSource)[item].value + "'>" + (settings.dataSource)[item].text + "</option>";
				}
			}
			ddl += "</select>";
			return ddl;
		}
		return DllTag();
	}
})(jQuery);