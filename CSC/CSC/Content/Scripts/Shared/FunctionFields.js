/*
 根据 权限 对表单的 字段，进行隐藏或 只读
*/

function FunctionFieldsAccess(functionFieldsJson) {
    if (functionFieldsJson != undefined) {
        $(functionFieldsJson).each(function (index, n) {
            var filedObj = $("#" + n.FieldName);
            if (n.IsHided == true) {
                var tr = filedObj.parent();
                var currentTd;
                var i = 0;
                while (i < 4) { //判断 是否找到 父节点
                    if (tr.is("td")) {
                        currentTd = tr;
                        tr = tr.parent();
                    } else if (!tr.is("tr")) {
                        tr = tr.parent();
                    } else {
                        break;
                    }
                    i++;
                }
                if (tr.is("tr")) {
                    var tds = tr.find("td");
                    if (tds.length == 2) {
                        tr.addClass("s_none");
                    }
                    else {
                        if (currentTd.is("td")) {
                            //                            currentTd.children().each(function () { 
                            //                                $(this).addClass("s_none");
                            //                            });
                            //                            currentTd.prev().children().each(function () {
                            //                                $(this).addClass("s_none");
                            //                            }); 
                            currentTd.html("");
                            currentTd.prev().html("");
                        }
                    }
                }
            } else if (n.IsDisabled == true) {
                var td = filedObj.parent();
                var j = 0;
                while (j < 4) { //判断 是否找到 父节点
                    if (!td.is("td")) {
                        td = td.parent();
                    } else {
                        break;
                    }
                    j++;
                }
                if (td.is("td")) {
                    td.find(" :input").attr("readonly", "readonly").addClass("in_readonly");
                    td.find("textarea").attr("readonly", "readonly").addClass("in_readonly");
                }
            }
        });
    }
}