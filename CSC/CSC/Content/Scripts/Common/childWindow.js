$(function () {
	if (window.parent.LoadComplate)
		window.parent.LoadComplate();
	var options = {
		data: {
			resulttype: 'json'
		},
		beforeSubmit: function (arr, $form, options) {
			var validateObj = Sys.Mvc.FormContext.getValidationForForm($form[0]);
			if (validateObj && validateObj.validate().length > 0)
				return false;
			return true;
		},
		success: function (result) {
			alert(result.Message);
			if (window.successedCall) {
				window.successedCall(result);
				return;
			}
			if (result.Success) {
				if (window.parent && window.parent.dlgEdit)
					window.parent.dlgEdit.close();
				window.parent.location.reload();
			}
		}
	};
	$("form").each(function () {
		if (!$(this).hasClass("noAjax"))
			$(this).ajaxForm(options);
	});


	$("#btnCancel").click(function () {
		if (window.parent && window.parent.dlgEdit)
			window.parent.dlgEdit.close();
	});
});