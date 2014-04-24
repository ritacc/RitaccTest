/*
 * 简易tip组件
 * 2013-03-04 马超
 *
 * $(selector).jtip( ops ); //直接绑定
 * 或
 * $.jtip(target, ops); //代理模式
 *
 * $.jtip.hide(); //关闭所有tip
 *
 *
 * [配置参数]
 * css : "", //额外追加的css样式
 * wraper : "<div><i></i>{content}</div>", //tip包装容器，其中 {content} 将会被替换为实际tip内容
 * maxWidth : 225 //默认最大宽度
 * center : true //居中方式显示到元素下方
 */
(function(window,$,undefined){
//主对象
var Tip = function( type, dom, ops ){
	//处理配置对象
	this.config = $.extend({}, Config, ops||{});
	//初始化
	var init = type==1 ? this.init : this["init"+ type];
	if( $.isFunction(init) ){
		init.call(this,dom);
	}else{
		this.init2(dom);
	}
},
//默认配置
Config = {
	css : "",
	wraper : "<div class='jtipWraper'><i></i>{content}</div>",
	maxWidth : 225
},
//查找服务对象的层叠高度 css("zIndex")在IE下返回的一直是数字，而其他标准浏览器返回是字符串
findZindex = function( obj ){
	var Z = 0,
	findZ = function( o ){
		var p =o.offsetParent(), z = p[0] ? ((p.css("zIndex")||"")+"").replace(/\D/g, "") : "";
		if( z ) Z = Math.max(Z, +(z || 1));
		if( !p[0] || p[0] == document.body )return;
		findZ(p);
	};
	findZ( $(obj) );
	//没有定位参考的，不处理层级高度
	//有定位参考的，则比最大zIndex增加2（但有时也未必能满足要求，比如附近的元素层级较高）
	return Z <= 0 ? undefined : (Z+2);
};
//主对象原型
Tip.prototype = {
	//直接绑定模式的初始化
	init : function(dom){
		var com = this;
		$(dom).bind({
			mouseenter : function(e){ com.mouseenter(e, this); },
			mouseleave : function(e){ com.mouseleave(e, this); }
		});
	},
	//代理模式的初始化
	init2 : function(selector){
		var com = this;
		$(document).delegate(selector, "mouseenter", function(e){ com.mouseenter(e, this);})
				.delegate(selector, "mouseleave", function(e){ com.mouseleave(e, this); });
	},
	//点击打开模式（代理）
	init3 : function(selector){
		var com = this;
		$(document).delegate(selector, "click", function(e){ com.mouseenter(e, this, true);})
				.delegate(selector, "mouseleave", function(e){ com.mouseleave(e, this); });
	},
	//鼠标移上
	mouseenter : function(e, dom, notDelay){
		var com = this, me = $(dom), title = me.attr("title");
		//如果有title，则立即处理，防止出现多个提示的情况
		if( title ) me.attr("inf", title).removeAttr("title");
		dom.timer && window.clearTimeout(dom.timer);
		dom.timer = window.setTimeout(function(){
			Tip.hide();
			com.showTipFor( dom );
		},notDelay ? 0 : 200);
	},
	//鼠标离开
	mouseleave : function(e, dom){
		dom.timer && window.clearTimeout(dom.timer);
		dom.timer = window.setTimeout(Tip.hide, 200);
	},
	//分析Dom节点
	showTipFor : function( dom ){
		var me=$(dom), tip = me.attr("tip"), title = me.attr("title"), inf = me.attr("inf"), target, that=this, tmpl = this.config.wraper, pos, size, autoCreate, left, maxLeft, orgLeft, fix=0, conf;
		//转移title
		if( title )me.attr("inf", title).removeAttr("title");
		//优先级：tip > title > inf
		target = tip ? $(tip) : (title || inf ) ? $(tmpl.replace(/{content}/g, title || inf )).appendTo(document.body) : [null];
		//如果没有找到tip元素，则不处理
		if( !target[0] )
			return;
		//重新设定tip，方便下次使用
		if( !target[0].id ){
			target[0].id = "tip"+(+new Date())+(parseInt(Math.random()*1000));
			me.attr("tip", "#"+ target[0].id);
		}
		autoCreate = target[0].autoCreate || !tip;
		//如果是脚本自动创建的，则添加标记
		if( autoCreate )
			target[0].autoCreate = true;
		//2013-08-20 马超 增加读取dom节点上的自定义配置
		conf = (new Function("return {"+ (me.attr("tipconf")||"").replace(/[\{\}]/g, "") + "}"))();
		//目前仅仅支持maxWidth / css 两个自定义配置
		$.each(["maxWidth", "css"], function(i, key){
			that.config[key] = conf[key] || that.config[key];
		});
		
		//检查css
		this.config.css && target.addClass(this.config.css);
		//存入全局缓存，以方便关闭
		Tip.Current = dom;
		//初始化tip的事件
		if( !target[0].initTipEvent ){
			target.appendTo(document.body)
			.bind({
				mouseenter : function(){ dom.timer && window.clearTimeout(dom.timer); },
				mouseleave : function(){ dom.timer = window.setTimeout(Tip.hide, 200); }
			});
			target[0].initTipEvent = true;
		}
		//检查tip宽度，仅仅对自动创建的元素生效
		target.removeAttr("style").show();
		if( target[0].autoCreate && target.outerWidth() > this.config.maxWidth ){
			target.width( this.config.maxWidth );
		}
		//重新定位
		pos = me.offset();
        size = {width: me.outerWidth(), height:me.outerHeight()};
        if(conf.center){
            left = pos.left + size.width/2 - target.outerWidth()/2;
        }else{
            left = pos.left + size.width/2 - 25;
        }
		//2013-05-30 优化 定位逻辑，防止tip超出屏幕
		//仅仅适用于自动创建的tip
		if( target[0].autoCreate ){
			maxLeft = Math.max($(window).width(), $(document.body).width()) - target.outerWidth() + $(window).scrollLeft(),
			orgLeft = left;
			if( maxLeft < orgLeft ){ //右侧溢出
				fix = orgLeft - maxLeft;
				left = maxLeft;
			}else if(orgLeft < 0){ //左侧溢出
				fix = - orgLeft;
				left = 0;
			}
            if(conf.center){
                target.find("i").css("left", target.outerWidth()/2-target.find("i").width()/2);
            }else{
                target.find("i").css("left", 19+fix);
            }
		}
		var cssIndex = (target.css("zIndex")+"").replace(/\D/g, ""),
			myIndex = (autoCreate || !cssIndex) ? findZindex(me) : undefined;
		target.hide().css({
			left : left,
			top : pos.top + size.height + 10,
			position : "absolute",
			zIndex : myIndex
		}).show();
		//设置了zIndex的进行标记，以便页面滚动的时候隐藏
		if( myIndex ) target.attr("autoIndex", myIndex);
	}
};
//全局隐藏方法
Tip.hide = function( onlyForFix ){
	var dom = Tip.Current, myTip;
	if( dom ){
		dom.timer && window.clearTimeout(dom.timer);
		myTip = $($(dom).attr("tip"));
		if( !onlyForFix || (onlyForFix && myTip.attr("autoIndex")) ){
			myTip.hide();
			Tip.Current = null;
		}
	}
};

/*
 * 对外接口
 */
$.fn.jtip = function(ops){	
	return this.each(function(){
		new Tip(1, this, ops);
	});
};
//2013-04-28 马超 优化，防止重复绑定处理
var _tipCache = {};
$.jtip = function(target, ops){
	//必须是selector
	if( typeof target != "string" )
		return this;
	//如果处理过，则忽略
	if( _tipCache[target] )
		return this;
	//否则开始监听
	_tipCache[target] = 1;
	//2013-12-12 增加method方法
	var type = {"click":3}[(ops||{}).method||""] || 2;
	new Tip(type, target, ops);
	return this;
};
$.jtip.hide = function(){ Tip.hide() };

//2013-08-01 增加对滚动条监听
$(window).scroll(function(){ Tip.hide(true); });
})(window,jQuery);