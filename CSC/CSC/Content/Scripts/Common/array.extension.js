/*
* Array扩展，目前主要扩展其remove方法
* power by xianhong 2011-5-31
* 
* 版本1.0.0 //由于多个系统均使用此js，更改后记得更换版本
*/
Array.prototype.remove = function() {
    var newArr = new Array();
    var a = arguments[0];
    for (var i = 0; i < this.length; i++) {
        if (typeof(a) == "function") {
            if (!a(this[i]))
                newArr.push(this[i]);
        } else if (this[i] != a) {
            newArr.push(this[i]);
        }
    }

    return newArr;
};

//全部删除
Array.prototype.removeAll = function () {
    return this.remove(function (item) {
        return true;
    });
};

//是否包含满足条件的元素，返回true 或 false
Array.prototype.any = function () {
    var args = arguments[0];
    for (var i = 0; i < this.length; i++) {
        if (typeof (args) == "function") {
            if (args(this[i])) {
                return true;
            }
        }
    }
    return false;
};