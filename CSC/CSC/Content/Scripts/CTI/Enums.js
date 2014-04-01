/// ********* Copyright ************
/// 作者:张大为 2008-6-4
/// 公司:三地信息技术有限公司

/// ********************************

/// 枚举值
var $Enums = new function(){
	var Me = this;
	Me.Status = new function(){
		var index = 0;
		this.Name = [];
		this.Tag = [];
		
		this.Name.push("空闲");this.Tag.push("Free");
		this.Name.push("振铃");this.Tag.push("Ring");
		this.Name.push("通话");this.Tag.push("Call");
		this.Name.push("外拨");this.Tag.push("Dial");
		this.Name.push("监听");this.Tag.push("Pause");

		this.Name.push("保持");this.Tag.push("Call");
		this.Name.push("创建会议");this.Tag.push("Pause");
		this.Name.push("参与会议");this.Tag.push("Pause");
		this.Name.push("参与通话");this.Tag.push("Pause");
		this.Name.push("三方会议外拨");this.Tag.push("Call");
		
		this.Name.push("三方会议外拨中");this.Tag.push("Call");
		this.Name.push("三方会议外拨完成");this.Tag.push("Call");
		this.Name.push("三方会议");this.Tag.push("Call");
		this.Name.push("三方会议呼入");this.Tag.push("Call");
		this.Name.push("三方会议振铃");this.Tag.push("Call");

		this.Name.push("三方会议摘机");this.Tag.push("Call");
		this.Name.push("已加入三方会议");this.Tag.push("Call");
		this.Name.push("Unknown17");this.Tag.push("Call");
		this.Name.push("Unknown18");this.Tag.push("Call");
		this.Name.push("Unknown19");this.Tag.push("Call");
		
		this.Name.push("拨座席开始");this.Tag.push("Call");
		this.Name.push("拨放工号");this.Tag.push("Call");
		this.Name.push("取DTMF码");this.Tag.push("Call");
		
		this.Idle		= index++;
		this.Ringing	= index++;
		this.Calling	= index++;
		this.Dial		= index++;
		this.Listening	= index++;
		
		this.Hold		= index++;
		this.CreateConf	= index++;
		this.InConf		= index++;
		this.CallInConf	= index++;
		this.StartDialInMutiConf	= index++;
		
		this.DialingInMutiConf		= index++;
		this.DialedInMutiConf		= index++;
		this.InMutiConf				= index++;
		this.CallInInMutiConf		= index++;
		this.RingingInMutiConf		= index++;
		
		this.OffHookInMutiConf		= index++;
		this.EnteredMutiConf		= index++;
		this.Unkown17				= index++;
		this.Unkown18				= index++;
		this.Unkown19				= index++;
		
		this.StartDialAgent			= index++;
		this.PlayID					= index++;
		this.GetDTMF				= index++;
	}
	Me.CallState = new function(){
		var index = 0;
		this.Name = [];
		
		this.Idle		= index++;		//挂机,空闲
		this.Ringing	= index++;		//振铃
		this.Calling	= index++;		//摘机通话
		this.Working	= index++;		//后处理

		this.Holding	= index++;		//保持
		this.Listening	= index++;		//监听
		this.Transing	= index++;		//转接中

		
		this.Name.push("挂机,空闲");
		this.Name.push("振铃");
		this.Name.push("摘机通话");
		this.Name.push("后处理");
		this.Name.push("保持");
		this.Name.push("监听");
		this.Name.push("转接中");
	}
	Me.AgtReqItem = new function()
	{
		var index = 0;
		this.Name = [];
        
		this.reqHold = index++; //HOLD呼叫请求
		this.reqLink = index++; //将线路连接请求

		this.reqListen = index++; //监听请求
		this.reqLogOff = index++; //注销请求
		this.reqLogOn = index++;  //登录请求
        
		this.Name.push("HOLD呼叫请求");
		this.Name.push("将线路连接请求");
		this.Name.push("监听请求");
		this.Name.push("注销请求");
		this.Name.push("登录请求");
        
		this.reqOffHook = index++;//软摘机请求

		this.reqOnHook = index++; //软挂机请求

		this.reqPause = index++;  //暂停请求
		this.reqRestore = index++;//恢复请求
		this.reqRetrieve = index++; //HOLD后的拉回请求
        
		this.Name.push("软摘机请求");
		this.Name.push("软挂机请求");
		this.Name.push("暂停请求");
		this.Name.push("恢复请求");
		this.Name.push("HOLD后的拉回请求");
        
		this.reqReturnIVR = index++;//将呼叫返回IVR的请求

		this.reqTransfer = index++; //转接呼叫请求
		this.reqUnlisten = index++; //取消监听请求
		this.reqWorkAftCall = index++; //呼叫后处理请求

		this.reqWorkAftCallOver = index++; //呼叫后处理结束请求

        
		this.Name.push("将呼叫返回IVR的请求");
		this.Name.push("转接呼叫请求");
		this.Name.push("取消监听请求");
		this.Name.push("呼叫后处理请求");
		this.Name.push("呼叫后处理结束请求");
        
		this.reqBookAgt = index++; //预订指定座席
		this.reqDisconnCall = index++; //强拆
		this.reqStartDial = index++;   //外拨
		this.reqGetSpecAgtCallId = index++; //返回指定座席的当前CallId
		this.reqJoinTalk = index++; //已加入会话

        
		this.Name.push("预订指定座席");
		this.Name.push("强拆");
		this.Name.push("外拨");
		this.Name.push("返回指定座席的当前CallId");
		this.Name.push("已加入会话");
        
		this.reqExitTalk = index++; //已退出会话

		this.reqMsgToAgt = index++; //向指定座席发消息
		this.reqReqAgtCallAct = index++; //请求指定座席执行呼叫动作
        
		this.Name.push("已退出会话");
		this.Name.push("向指定座席发消息");
		this.Name.push("请求指定座席执行呼叫动作");
	}
    Me.Return = new function()
    {
        var index = 0;
		this.retOK = index++; //请求处理成功
        this.retFail = index++; //请求处理失败
        this.retBusy = index++; //座席忙，不能处理此类请求
        this.retWarn = index++;  //请求处理告警
    }

}
