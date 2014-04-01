/*
*秒表
*依赖于jQuery 
*by xh 2012
*/
function stopwatch(element) {
    this.element = element;
    this.hour = 0;
    this.minute = 0;
    this.second = 0;
    //this.millisecond = 0;
    this.executor = function () {
        //this.millisecond++;
        // if (this.millisecond >= 59) {
        //this.millisecond = 0;
        this.second++;
        if (this.second >= 59) {
            this.second = 0;
            this.minute++;
            if (this.minute >= 59) {
                this.minute = 0;
                this.hour++;
            }
        }
        //}

        var _Hz = "";
        var _mmz = "";
        var _ssz = "";
        var _TCz = "";

        if (this.hour < 10) { _Hz = "0" } //小于两位数时，在前面加0
        if (this.minute < 10) { _mmz = "0" }
        if (this.second < 10) { _ssz = "0" }
        //if (this.millisecond < 10) { _TCz = "0" }

        _Str = _Hz + this.hour + ":" + _mmz + this.minute + ":" + _ssz  + this.second; // +":" + _TCz + this.millisecond;
        if (this.element) {
            var ctl = $("#" + this.element);
            if(ctl.attr("type") == "input")
                ctl.val(_Str);
            else
                ctl.html(_Str);
        }
    };
    this.start = function () {
        this.timer = setInterval(this.executor, 1000);
    };

    this.timer = null;
    this.stop = function () {
        clearInterval(this.timer);
    };
    return this;
}