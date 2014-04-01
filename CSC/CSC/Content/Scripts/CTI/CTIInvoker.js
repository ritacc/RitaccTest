/*
*调用CTI Activex控件方法
*依赖于jQuery 
*by xh 2012
*/
var citInvoker = function (options) {
    var defaults = {
          logonID: ''//登陆账号
        , logonPWD: ''//登陆密码
        , logonGroups: ''//登录组
        , chnID: ''//通道号
        , getTelNumber: function () { } //获取电话号码
        , getGroupNumber: function () { } //获取组号码
        , onRing: function (ANI, callID, subID, grpID, area) { }
        , onAgtReqReturn:function(str){}
        , onAgtState:function(){}
    };
    this.callId = null;
    this.ctiClient = null;
    this.ctiActer = null;
    this.settings = $.extend(defaults, options);
    this.callState = null;
    this.init = function () {
        this.ctiClient = CssAgent2007.object;
        this.ctiActer = this.ctiClient.g_objCTI;
        this.bindEvent();
        this.login();
    };
    this.restore=function(){
         this.ctiActer.AgtRestore();
    };
    this.pause = function(){
        this.ctiActer.AgtPause();
    };
    this.eventIsBind=false;

     //外拨电话
    this.dial = function(phone){
        var group = settings.getGroupNumber();
        var tel = phone || settings.getTelNumber();
        this.ctiActer.Run("Dial", tel, "X", parseInt(group));
    };
  
    //拨座席
   this.dialAgt = function(group,tel){
//        var group = settings.getGroupNumber();
//        var tel = settings.getTelNumber();
        this.ctiActer.Run("Dial", tel, "A", parseInt(group));
    };

    //转接，此方法只有在通话过程中拨通第三方后才可使用
    this.trans= function () {
        this.ctiActer.Run("Dial_Trans");
    };

    //转驳
    this.transCall = function(actionCode, phoneNo) {
        this.ctiActer.AgtTransExt(actionCode, phoneNo);
    };

    //转坐席
    this.transAgent = function(agentNo) {
        this.ctiActer.AgtReturnIVR(1, this.callId, agentNo);
    };

    //创建三方通话
    this.meeting = function () {
        this.ctiActer.Run("TalkingDial_Join");
    };

    //摘机
    this.answer = function () {
        this.ctiActer.Run("OffHook");
    };

    //挂机
    this.hung = function () {
        this.ctiActer.Run("OnHook");
    };

    //保持
    this.hold = function () {
        this.ctiActer.AgtHold(this.callId);
    };

    //拉回
    this.callback = function(){
        this.ctiActer.AgtRetrieve(this.callId);
    };

    //登陆
    this.login=function(){
        var _self = this;
        var settings = _self.settings;
        if (settings.chnID == "") {
            _self.ctiClient.g_objCTI.AgtLogOn(settings.logonID, settings.logonPWD, settings.logonGroups);
        } else {
            _self.ctiClient.g_objCTI.AgtLogOnWithHostName(settings.logonID, settings.logonPWD, settings.logonGroups,settings.chnID);
        }
    };
    
     //退出释放
    this.quit=function(){
        if(!this.ctiClient)return;
        this.ctiClient.g_objCTI.AgtLogOff();
        this.ctiClient.g_objCTI.DisconnectCTI();
        this.ctiClient.ReleaseObj();
    };

    this.bindEvent = function () {
        var _self = this;
        var settings = _self.settings;

       
        if(_self.eventIsBind) return;
        //来电
        function CssAgent2007::Ring(ANI, callID, subID, grpID, area) {
            _self.callId = callID;
            settings.onRing(ANI,callID,subID,grpID,area);
        };

        //连接
        function CssAgent2007::Connect() {
            
        };

        //断开连接
        function CssAgent2007::Disconnect() {
            
        }
            
        //状态变化
        function CssAgent2007::AgtState(state, hook, occupy) {
            /*var CallState=state;
            switch (state) {
                case $Enums.Status.Idle: CallState = $Enums.CallState.Idle; break;
                case $Enums.Status.Ringing: CallState = $Enums.CallState.Ringing; break;
                case $Enums.Status.Calling: $Enums.CallState.Calling; break;
                case $Enums.Status.Hold: $Enums.CallState.Holding; break;
                case $Enums.Status.Listening: CallState = $Enums.CallState.Listening; break;
                default: ;
            }
            if (occupy == 1) CallState = $Enums.CallState.Working;
            */
            _self.settings.onAgtState(state, hook, occupy);
        }

        //请求操作反馈
        function CssAgent2007::AgtReqReturn(cmdItem, retCode, description) {
            var str = $Enums.AgtReqItem.Name[cmdItem] + " " + ((retCode == $Enums.Return.retOK) ? "成功" : "失败") + ", 消息:" + description;
             _self.settings.onAgtReqReturn(str,cmdItem);
        }

        //用户有效点击了提交按钮
        function CssAgent2007::SubmitInfo() {
               
        }
        _self.eventIsBind = true;
    };
    return this;
};