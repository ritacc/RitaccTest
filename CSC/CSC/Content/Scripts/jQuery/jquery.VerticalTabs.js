(function () {
    $.verticalTabs = function (options) {
        var defaults = {
            element:null,
            index =0,
            onselect:null,
            onleave:null
        };
        var settings = $.extend(defaults,options);
        this.element = settings.element;
        this.index = settings.index;
        this.onselect = settings.onselect;
        this.onleave = settings.onleave;
    }

    
})();