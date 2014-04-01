/*
*弹出window
*依赖于jQuery 
*by xh 2012
*/
var index = 0;
function ItemToolEdit(url,title) { //编辑
    index++;
    ItemToolEditBase(url, "viewEdit" + index, 600, 400, title);
}
var dlgEdit; //编辑层对象
function LoadComplate() {
    $("#divLoading").remove();
}

function ItemToolEditBase(url, viewEditId, width, height, title) {    //编辑
    // 创建结果显示层
    var editObj = $("#" + viewEditId + "");
    if (editObj.length == 0) {
        editObj = $("<div>", { id: viewEditId });
    }
	//<div id='divLoading' style='width:100%;height:400px;background-color:#fff; z-index: 3;left:0px;top:0px;text-align:center;'>正在加载……</div>
    var str = "<iframe id='popWindowSrc'  style='position: absolute;  z-index: 2;left: 0px; width:100%;height:" + (height - 20) + "px;' marginheight='0' marginwidth='0' frameborder='0' scrolling='no'  src='" + url + "'/>";
    editObj.empty();
    editObj.html(str);

    editObj.appendTo($(document.body));

    dlgEdit = editObj.dialog({
        width: width,
        height: height,
        modal: true,
        title: title
    });
    dlgEdit.open();
}


var div = $("#divresult"); // 创建结果显示层
function ItemToolDelete(url) { //删除
    location.href = url;
}


$(document).ready(function () {
    (function () { // tool button click
        $(".popWindowBtn").click((function (e) { //新增,编辑,删除

            var href = $(this).attr("href");

            var toolsId = $(this).attr("id");
			var title = $(this).attr("title");
            var cbs = $("div.grid_list :checkbox:checked:not(:disabled)[name='cbKey']");
            switch (toolsId) {
                case "tool_new": // 新增
                case "": // 修改
                	ItemToolEdit(href, title);
                    break;
                case "tool_edit": // 编辑
                    if (cbs.length != 1) { alert($g["SelectSingle"]); break; }
                    ItemToolEdit(href.replace(/-id-/ig, cbs.val()), title);
                    break;
                case "tool_delete": // 删除
                    if (cbs.length != 1) { alert($g["SelectSingle"]); break; }
                    if (window.confirm($g["DeleteConfirm"])) {
                        ItemToolDelete(href.replace(/-id-/ig, cbs.val()));
                        break;
                       }
                       break;
                case "tool_freezeGroup": // 冷冻
					if (cbs.length != 1) { alert($g["SelectSingle"]); break; }
					if (window.confirm($g["FrozenConfirm"])) {
						ItemToolDelete(href.replace(/-id-/ig, cbs.val()));
						break;
					}
					break;
				case "tool_cancelFreezeGroup": // 解除冷冻
					if (cbs.length != 1) { alert($g["SelectSingle"]); break; }
					if (window.confirm($g["CancelFrozenConfirm"])) {
						ItemToolDelete(href.replace(/-id-/ig, cbs.val()));
						break;
					}
					break;
				case "tool_suspend": // 暂停
					if (cbs.length != 1) { alert($g["SelectSingle"]); break; }
					if (window.confirm($g["SuspendConfirm"])) {
						ItemToolDelete(href.replace(/-id-/ig, cbs.val()));
						break;
					}
					break;
				case "tool_cancelSuspend": // 解除暂停
					if (cbs.length != 1) { alert($g["SelectSingle"]); break; }
					if (window.confirm($g["CancelSuspendConfirm"])) {
						ItemToolDelete(href.replace(/-id-/ig, cbs.val()));
						break;
					}
					break;
            }; e.preventDefault();
        }));
    })();
});