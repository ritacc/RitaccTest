/// <reference path="../../jQuery/jquery-1.4.1-vsdoc.js" />
/// <reference path="../../Shared/Edit.js" />

$(function () {
    (function () {
        $.getJSON(window.GetMenuAccess, { funccode: "40033030" }, function (json) {
            if (json.IsHided == true) {
                $("#product").hide();
            }
            else {
                $("#product").show();
            }
            if (json.IsDisabled == true) {
                $("#product").attr("disabled", "disabled");
            }
            else {
                $("#product").attr("disabled", false);
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "40032020" }, function (json) {
            if (json.IsHided == true) {
                $("#customer").hide();
            }
            else {
                $("#customer").show();
            }
            if (json.IsDisabled == true) {
                $("#customer").attr("disabled", "disabled");
            }
            else {
                $("#customer").attr("disabled", false);
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "20011010" }, function (json) {
            if (json.IsHided == true) {
                $("#purchaseorder").hide();
            }
            else {
                $("#purchaseorder").show();
            }
            if (json.IsDisabled == true) {
                $("#purchaseorder").attr("disabled", "disabled");
            }
            else {
                $("#purchaseorder").attr("disabled", false);
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "20031010" }, function (json) {
            if (json.IsHided == true) {
                $("#approveandconfirmation").hide();
            }
            else {
                $("#approveandconfirmation").show();
            }
            if (json.IsDisabled == true) {
                $("#approveandconfirmation").attr("disabled", "disabled");
            }
            else {
                $("#approveandconfirmation").attr("disabled", false);
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "10011010" }, function (json) {
            if (json.IsHided == true) {
                $("#stockIn").hide();
            }
            else {
                $("#stockIn").show();
            }
            if (json.IsDisabled == true) {
                $("#stockIn").attr("disabled", "disabled");
            }
            else {
                $("#stockIn").attr("disabled", false);
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "30011010" }, function (json) {
            if (json.IsHided == true) {
                $("#salesorder").hide();
            }
            if (json.IsDisabled == true) {
                $("#salesorder").attr("disabled", "disabled");
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "30011040" }, function (json) {
            if (json.IsHided == true) {
                $("#deliverorder").hide();
            }
            if (json.IsDisabled == true) {
                $("#deliverorder").attr("disabled", "disabled");
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "20011020" }, function (json) {
            if (json.IsHided == true) {
                $("#receipt").hide();
            }
            else {
                $("#receipt").show();
            }
            if (json.IsDisabled == true) {
                $("#receipt").attr("disabled", "disabled");
            }
            else {
                $("#receipt").attr("disabled", false);
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "30021010" }, function (json) {
            if (json.IsHided == true) {
                $("#nonsalesorder").hide();
            }
            if (json.IsDisabled == true) {
                $("#nonsalesorder").attr("disabled", "disabled");
            }
        });

        $.getJSON(window.GetMenuAccess, { funccode: "10022020" }, function (json) {
            if (json.IsHided == true) {
                $("#stocktransfer").hide();
            }
            if (json.IsDisabled == true) {
                $("#stocktransfer").attr("disabled", "disabled");
            }
        });
    })();
});