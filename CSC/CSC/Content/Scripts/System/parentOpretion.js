/*
*调用父窗体方法，仅用于MasterControl
*依赖于jQuery 
*by xh 2012
*/
var parentOpretion = (function () {
    return {
        showTabPage: function (index) {
            if (!window.parent.goToPage) {
                return this;
            }
            window.parent.goToPage(index);
            return this;

        },
        hideAllPage: function () {
            window.parent.hideAllPage();
            return this;
        },
        loadFrame: function (tabId, url, callback) {
            window.parent.loadFrame(window.parent.$("#" + tabId + ""), url, true, null, callback);
            return this;
        },
        injection: function (name, value) {
            eval("window.parent." + name + "= value");
        },
        initCti: function () {
            window.parent.initCti();
        },
        getCti: function () {
            return window.parent.cti; ;
        },
        getCtiMode: function () {
            return window.parent.ctiMode;
        }
    };
})();
