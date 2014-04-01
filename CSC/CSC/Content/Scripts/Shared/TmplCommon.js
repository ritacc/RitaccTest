/// <reference path="../../jQuery/jquery-1.4.1-vsdoc.js" />
/// <reference path="../../jQuery/jquery.tmpl.js" />
/// <reference path="../../Shared/Edit.js" />
/// <reference path="../../Shared/Common.js" />
function $Tmpl(container, template, data, tabe, gridsKey, hidenObj) {
    this.container = container; //数据源放的 容器
    this.template = template; //模板
    this.data = data; //数据源(服务器端返回的json数据的 对象)
    this.table = tabe; //需要刷新的table 对象
    this.gridsKey = gridsKey; // grids的key值
    this.currentSelectedItem = null; //当前选中的
    this.hidenObj = hidenObj;//隐藏域，对象,在提交时存放值
}

$Tmpl.prototype = {
    //读取模板，加载数据
    RenderTemplate: function () {
        $(this.container).empty();
        $.tmpl(this.template, this.data).appendTo(this.container);
        $t.renderTable(this.table);
        $grids[this.gridsKey].refresh();
    },
    // 删除。
    //返回 找到的下标
    DeleteJsonItem: function () {
        var index = $.inArray(this.currentSelectedItem, this.data);
        if (index >= 0) {
            if (this.data[index].ModelStatus == ModelStatusEnum.Edit ||
                this.data[index].ModelStatus == ModelStatusEnum.Normal) { //如果是普通或编辑状态的,(这些数据在数据库在是存在的)，改成删除状态
                this.data[index].ModelStatus = ModelStatusEnum.Delete;
            } else if (this.data[index].ModelStatus == ModelStatusEnum.New) { //如果是 新建的,就直接删除
                this.data.splice(index, 1); //直接删除
            }
        }
        return index;
    },
    //清除当前选中,移除 选中的 的背景颜色
    CleanCurrentSelected: function () {
        this.currentSelectedItem = null; //，当前选中行,清空
        $t.removeBackColor(this.table); //移除 选中的 的背景颜色
    },
    // 将 JSON对象 变成字符串，放进隐藏域,(所有状态的数据)
    PushData: function () {
        var jsonString = JSON.stringify(this.data);
        this.hidenObj.val(jsonString);
    },
    // 将 JSON对象 变成字符串，放进隐藏域 ，状态是Normal的不会被添加
    //(先循环 添加 完 删除的数据,再去 添加 其它状态的数据.防止外键引用引发的问题)
    PushDataOfNotNormal: function () {

        var notNormalData = []; //存不是 普通 的数据( 删除数据+修改数据+新建数据)
        var otherData = []; //存 不是 删除和普通 的数据
        var currentObj = this;
        $(currentObj.data).each(function (index, n) {
            if (n.ModelStatus != ModelStatusEnum.Normal) {
                if (n.ModelStatus != ModelStatusEnum.Delete) {
                    otherData.push(n);
                } else {
                    notNormalData.push(n);
                }
            }
        });
        $(otherData).each(function (index, n) {
            notNormalData.push(n);
        });

        var jsonString = JSON.stringify(notNormalData);
        this.hidenObj.val(jsonString);
    },
    //获取temp的length(去除删除状态的数据),用于判断明细信息是否输入
    GetTempDataLength: function () {
        var length = 0;
        $(this.data).each(function (index, n) {
            if (n.ModelStatus != ModelStatusEnum.Delete) {
                length += 1;
            }
        });
        return length;
    },
    //删除相关 Lot信息.(LotId,BatchNo,AltBatchNo,ThirdBatchNo,LocationCode 会将这些信息清空)(适用:重新选择业务仓库时)
    RemoveLotInfo: function () {
        $(this.data).each(function (index, n) {
            if (n.ModelStatus != ModelStatusEnum.Delete) {
                n.BatchNo = "";
                n.AltBatchNo = "";
                n.ThirdBatchNo = "";
                n.LocationCode = "";
                n.LotId = "";
                if (n.ModelStatus == ModelStatusEnum.Normal) {
                    n.ModelStatus = ModelStatusEnum.Edit;
                }
            }
        });
    },
    //判断 是否存在 相关的 Lot 信息
    IsExistLotInfo: function () {
        var flag = true;
        $(this.data).each(function (i, n) {
            if (n.LotId == undefined || n.LotId <= 0) {
                flag = false;
                return false;
            }
        });
        return flag;
    },
    //改变 明细中的 状态为Edit(如果 是Normal)
    ModelStatusChangeEdit: function () {
        $(this.data).each(function (i, n) {
            if (n.ModelStatus == ModelStatusEnum.Normal) {
                n.ModelStatus = ModelStatusEnum.Edit;
            }
        });
    }
}




