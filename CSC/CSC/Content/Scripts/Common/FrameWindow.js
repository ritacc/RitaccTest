/// <reference path="../jQuery/jquery-1.4.1-vsdoc.js" />
/*
*调用加载Frame
*依赖于jQuery 
*by xh 2012
*/
var lastHtml = "";
var lastWin = null;

var refreshFrameById = function (id) {
    var iframe = $("#" + id).find(".iframeContainer").find("iframe");
    if (iframe.length) {
        var ctl = $("#" + id).find(".ajaxLoading");
        if (ctl.length)
            ctl.show();
        iframe[0].contentWindow.location.reload();
    }
}

var setLastHtml = function (win) {
	if (!$.fn.formSerialize)
		return false;
	lastWin = win;
	topWin().lastWin = win;
	if (lastWin.checkFormChanged && $.fn.formSerialize) {
		lastWin.lastHtml = $(lastWin.document.body).find("form").formSerialize();
		//console.log('lastWin.lastHtml:' + lastWin.lastHtml);
	}
};

$(function () {
    $(document).click(function () {

    });
    $("form").submit(function () {
        window.checkFormChanged = false;
        //window.lastWin = null;
    });
});


if (!window.onbeforeunload) {
	window.onbeforeunload = function () {
		if (window.checkChanged) {
			if ($.fn.formSerialize) {
				if (this.checkFormChanged && this == topWin().lastWin) {
					var html = $(this.document.body).find("form").formSerialize();
					if (html != this.lastHtml) {
						return $g["SureLeave"] || "你还有数据没有保存，确定离开此页吗 ?";
					}
				}
			}
		}
	}
}

var getLastWinIsChanged = function () {
	try {
		if (!lastWin || !lastWin.checkFormChanged || !lastWin.document || !$.fn.formSerialize)
			return false;

		if ($(lastWin.document.body).find("div.search").find("form").length > 0) //如果是search里有form则不验证
			return false;
		var html = $(lastWin.document.body).find("form").formSerialize();

		if (html != lastWin.lastHtml) {
			if (window.checkChanged) {
				if (!confirm($g["SureLeave"] || "你还有数据没有保存，确定离开此页吗 ?")) {
					return true;
				}
			}
		}
		lastWin = null;
		lastHtml = "";
		return false;
	} catch (e) {
		return false;
	}
	return false;
};

window.topWin = function () {
    var win = window;
    while (win != win.parent) {
        win = win.parent;
    }
    return win;
};

function removeAllIframe(win) {
    if (win == null)
        return;
    var iframes = $(win.document).find(".div_container_dialog").find("iframe");
    $(iframes).each(function (i) {
        removeAllIframe(this.contentWindow);
        $(this).remove();
    });
}

function removeTopWindowIframe() {
    var win = topWin();
    removeAllIframe(win);
}

window.noAjaxLoding = false;
var loadFrame = function (divcurrent, url, reLoad, divcurrentHide, loadCall, hideLoading) {

	if (getLastWinIsChanged())
		return false;

	var href = divcurrent.attr("href");
	if (url != undefined && url) {
		if (url != href)
			reLoad = true;
		href = url;
	}
	else {
		var frame = divcurrent.find("iframe");

		if (frame.length > 0) {
			var winHref = frame[0].contentWindow.location.href;
			href = winHref;
		}
	}

	divcurrent.attr("href", href);
	if (reLoad) {
		var findIframe = divcurrent.find("iframe");
		//console.log('findIframe.length:' + findIframe.length);
		if (divcurrent.attr("id") != "hideForDeleteDiv" && findIframe.length > 0) {
			//removeAllIframe(findIframe[0].contentWindow);
			//removeTopWindowIframe();
			//$(".div_container_dialog").remove();
			//$(".div_shade_dialog").remove();
		}
		findIframe.remove();
		divcurrent.children().remove();

	}
	if (href && href.indexOf("reload") < 0) {
		href += (href.indexOf("?") >= 0 ? "&" : "?") + "reload=true";
	}


	var loadingContainer = divcurrent.find("div.ajaxLoading");
	if (loadingContainer.length <= 0) {
		loadingContainer = $("<div>", { Class: "ajaxLoading" });
		var img = $("<img>", { src: window.topWin().ImageResources + "Loading.gif" });
		loadingContainer.html("");
		loadingContainer.append(img).css({
			position: "absolute", //"absolute",
			zIndex: "9999",
			textAlign: "center",
			backgroundColor: "#fff",
			border: "solid 4px #ebebeb",
			paddingTop: "18px",
			fontSize: "14px",
			top: "50%",
			left: "50%",
			height: "31px",
			width: "150px",
			marginTop: "-38.5px",
			marginLeft: "-79px"
		});
		if (!hideLoading)
			divcurrent.append(loadingContainer); //show().
	}

	var iframeContainer = divcurrent.find(".iframeContainer");

	if (iframeContainer.length <= 0) {
		iframeContainer = $("<div>", { style: "height:100%", Class: "iframeContainer" });
		iframeContainer.hide();

		var iframe = $("<iframe>", { frameborder: "0", src: href, marginwidth: "0", scrolling: "yes", marginheight: "0", height: "100%", width: "100%" });
		iframeContainer.append(iframe);
		iframe.load(function () {
			try {
				loadingContainer.hide();
				var frameWin = iframe[0].contentWindow;

				if (frameWin.document != null) {
					var frameBody = $(frameWin.document);

					frameBody.find(".search :submit").not(".hideLoading").click(function () {
						loadingContainer.show();
					});
					/*frameBody.find(":button[url]").click(function () {
					loadingContainer.show();
					});*/
					frameBody.find(".grid_title a,.pager a").click(function () {
						var thisHref = $(this).attr("href");
						thisHref = thisHref.replace(/[\?|\&]?reload\=true/g, "");
						frameWin.location.href = thisHref;
						loadingContainer.show();
						return false;

					});
					frameBody.find(".pager :button").click(function () {
						loadingContainer.show(0).delay(2000).hide(0);
					});
				}


				iframeContainer.show();

				if (loadCall) {
					loadCall(frameWin);
				}
				//setTimeout(function () { setLastHtml(frameWin); },2000);
				setLastHtml(frameWin);
				if (divcurrentHide)
					divcurrent.hide();
			}
			catch (err) {

			}
		});
		divcurrent.append(iframeContainer);
	}
};

