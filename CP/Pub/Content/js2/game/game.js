/**
 * import jQuery.1.4.2.js
 * 功能：
 * 彩票 Game 对象公共接口类
 * interface :
* Game.c(n, m)
 *    功能 :计算组合从n个数字中取m个
 *    n 和 m 是必选参数
 *    @param n  number 或   stirng  如果是string必须是个string类型的数字
 *    @param m  number 或   stirng  如果是string必须是个string类型的数字
 *    @return 返回一个组合结果，如果参数非法，返回undefined
 * Game.random(arr, num)
 *    功能:随机的从一个数组中取num个值出来
 *    @param arr array 或者    string
 *     arr 如果是数组，不限定arr的中元素的类型
 *     arr 如果是字符串则输入内容必须是 1-33这种2边是数字中间用-分开的字符串
 *    @param num 随机的数
 *     num必须小于arr的长度,或者小于字符串最大的那个整形值
 *    @return 返回  随机取出的num个元素的数组
 * Game.unique(arr1, arr2 ...)
 *    功能 : 合并多个数组并排重排序
 *    @param arr1  array必选
 *    @param arr2  其他等待合并的数组，可选
 *
 * Game.randomSortArr(arr)
 *    功能 :对数组进行随机的排序
 *    arr 必选参数
 *    @param arr array
 *    @return 返回排序后的数组,注意 此方法会修改传入的数组
 * Game.loadMolude(name, [fn])
 *    功能 :通过config中的配置加载对应的功能模块
 *    name 必选参数
 *    @param name   array 或者  string   其值必须是config中的module模块中的一个属性,否则会不执行回调
 *    @param fn  回调函数，当对应模块准备好之后调用
 *
 * Game.CR(arr, num)
 *    功能 :返回排列组合的结果
 *    @param arr  数组
 *    @param num  取的个数
 *    @return array 返回排列组合的结果
 *
 * Game.c1(arr, num)
 *    功能 : 体彩计算专用方法，从arr数组中挑选 num个结果出来 num是串关方式  num可以是个数组，也可以是个数字,
 *    @param arr  数组 其格式是 [3,2,1]
 *    @param num  num | array 表示串关，如3串1 则是3，记住这里只能是m串1
 *    @return 返回计算后的结果
 *
 * Game.c2(t, d, num)
 *    功能 : 体彩计算专用方法，从t和d的数组中挑选 num个结果出来 num是串关方式  num可以是个数组，也可以是个数字,
 *    @param t, d arr  数组 t表示托，d表示胆，其格式是[3,2,1] 3代表用户选择了三个结果
 *    @param num  num | array 表示串关，如3串1 则是3，记住这里只能是m串1
 *    @return 返回计算后的结果
 *
 * Game.groupNum(arr)
 *    功能 :对元素进行分组，体彩专用方法
 *    @param arr   array 一个数组
 *    @return arr  分组后的数组是个二维数组
 *
 * Game.addArr(arr)
 *    功能 :数组相加
 *    @param arr   array 数组元素,其内容必须是个数字或者字符串的数字
 *    @return number  返回计算后的值
 *
 * Game.multipleArr(arr)
 *    功能 :数组相乘
 *    @param arr   array 数组元素,其内容必须是个数字或者字符串的数字
 *    @return number  返回计算后的值
 *
 * Game.sortNum(arr, ad)//
 *    功能 :对数组元素排序,默认升序
 *    @param arr   array 要排序的数组
 *    @param ad  如果传递为desc则是降序，其他情况认为是升序
 *    @return 返回排序后的数组
 *
 * Game.index(arr, val)//
 *    功能 :查找数组的元素
 *    @param arr   array 要查找的数组
 *    @param val   要查找的元素
 *    @return 返回查找元素的位置
 *
 * Game.checkGamePause(configOrAjax, callback)
 *	  @param configOrAjax	[必选]停售配置对象或者ajax查询url，具体对象描述参照 notice/pause.js
 *	  @param callback	[可选]检查结果通知，接受一个参数：-1 ajax查询错误 -2 配置参数有误 1 已经停售并打开停售对话框 0正常销售
 *
 * Game.regBaseCom2Lib(name, events, prototype);
 *	  @param name  [必选]注册的组件位置以及名称
 *	  @param events	[必选]组件合法的事件名称，建议以on开头
 *	  @param prototype	[必选]组件原型，必须有init构造方法
 *
 * Game.createCom(name, options, callback)
 *	  @param name  [必选]组件位置以及名称
 *	  @param options	[可选]实例化参数（将传递给组件的init构造方法）
 *	  @param callback	[可选]实例化完毕回调，如果是异步加载，则必须设置此回调，以接收组件对象
 *	  @return 如果是同步组件，则直接返回实例化好的组件，如果是异步组件，则不返回内容
 *
 * modify log :
 *   create by cjx  2011-05-21
 *   2012-07-06 马超 修改版本号默认值，默认取Core.version
 *	 2012-09-05 新增 CR,c1,c2,indexOf,addArr,multipleArr, groupNum,sortNum 方法
 *	 2013-01-10 马超 增加 Game.checkGamePause(configOrAjax, callback) 查询彩种停售状态接口
 *	 2013-02-13 马超 增加 消息组件 调用相关接口：regBaseCom2Lib、createCom
 *	 2013-02-13 马超 增加 Game.unique 多数组合并排重排序方法
 *	 2013-05-04 马超 删除与Core上相同的公共代码
 *	 2013-10-26 曹建雄 修改CR方法提高效率
 *	 2013-12-24 马超 增加 getMoneyText 方法，当总金额超过亿元的时候推荐使用
 *	 2014-01-22 马超 增加 Game.setTask/Game.clearTask 方法，用于启动一个定时任务
 */
(function(w, Core, $, undefined) {
	"use strict";
	/*
	 * 限制函数递归
	 * 2013-05-07 曹建雄 增加
	 * fn [必选]一个函数， 调用该方法后将返回一个新的函数，这个函数会做 fn做的事情，但是这个方法无法被递归，一旦递归则会返回owner或者 windo
	 * owner 函数所有者
	 * [警告]，fn中如果有异步处理，该方法则会失效
	 */
	(function(){var c=1,cache={},fnCache={};$.getStopRecursionFn=function(a,b){if($.isFunction(a)){if(a.__stopRecursion&&fnCache[a.__stopRecursion]){return fnCache[a.__stopRecursion]}else{a.__stopRecursion=c;fnCache[a.__stopRecursion]=function(){if(!cache[a.__stopRecursion]){cache[a.__stopRecursion]=true;a.apply(b||window,arguments);delete cache[a.__stopRecursion]}else{return b||window}};c=c+1;return fnCache[a.__stopRecursion]}}}})();
	//创建并引用Game
	var G = w.Game = w.Game || {},
	//模块加载核心判断函数
	ex = function(name) {//解析传入对象串，返回URL地址或true 或空，返回空查找模块失败，返回URL则需要加载模块，返回true代表该模块已经加载
		var cdnURL = G.getCdnUrl(), //查找模块的url
			versionId = G.getVersionId(),
			play = G, url = ''; //玩法对象
		cdnURL += '/js2/game';//加上game的路径
		if( typeof name === 'string') {
			name = name.split('.');
			for(var i = 0; i < name.length; i++) {
				if(play[name[i]]) {//检测模块是否存在
					play = play[name[i]];
					continue;
				} else {
					url = [cdnURL, '/', name.join('/'), '.js?', versionId].join('');
				}
			}
			if(url) {//找到末尾
				return url;
			}else {
			  return true;
			}
		}
		return null;
	},
	/*
	 * 动态加载js/css
	*/
	load = function(type, url, fn) {
		switch(type){
			case "link": Core.loadCss(url); break;
			case "script": Core.loadJS(url, fn); break;
		}
	},
	/**获取对象类型，一个参数*/
	getType = function(item) {
		var toStrings = G._toStrings, toString = Object.prototype.toString,
			nodeTypes = {
				1: 'element',
				3: 'textnode',
				9: 'document',
				11: 'fragment'
			};
		if( !toStrings ){
			toStrings = {};
			var types = 'Arguments Array Boolean Date Document Element Error Fragment Function NodeList Null Number Object RegExp String TextNode Undefined Window'.split(' ');
			for (var i = types.length; i--;) {
				var type = types[i],
					constructor = window[type];
				if (constructor) {
					try {
						toStrings[toString.call(new constructor)] = type.toLowerCase();
					} catch (e) {}
				}
			}
			G._toStrings = toStrings;
		}
		return item == null && (item === undefined ? 'undefined' : 'null') || item.nodeType && nodeTypes[item.nodeType] || typeof item.length == 'number' && (
			item.callee && _arguments || item.alert && 'window' || item.item && 'nodelist') || toStrings[toString.call(item)];
	},		

	/*
	 * 基础组件模块（仅含事件注册和调用机制）
	 * 2013-02-04 马超
	 */
	COM = function(){};
	COM.fn = COM.prototype = {
		//私有接口，需要覆盖
		getEventCache : function(){ return {}; },
		//注册合法事件
		//evts 可以是字符串、数组、对象，来注册合法的事件类型
		//	   一旦注册完毕，即不可修改；建议事件名都添加 on/before/after 前缀，以明确表明是注册事件，防止和其他控制接口混淆
		//举例：
		// var com = new COM("onChange onClick onClose");
		// com.onClick(function(){ alert("click fired!") }); //绑定事件回调（快捷）
		// com.onClick();		//主动激发回调（无参数）
		// com.bind("onClose", function(){ alert("close fired!") }); //绑定事件回调（标准）
		// com.unbind("onClick", fn);	//卸载事件（仅仅支持有名函数的卸载，或全部卸载）
		// com.callEvent("onChange", true, "changed!"); //主动激发回调（有参数）
		// com.callEvent(300, "onEdit", "test"); //带时间缓冲的事件回调
		regEvent : function( evts ){
			var type = getType(evts), me = this, __eventCache = me.getEventCache();
			//挂接
			switch(type){
				case "string":
					evts = evts.split(" ");
					//这里没有break，应为拆成数组后，继续进入数组的流程进行处理
				case "array":
					$.each(evts, function(i, evt){
						__eventCache[evt] = __eventCache[evt] || [];
					});
					break;
			}
			//绑定快捷调用以及注册接口
			$.each(__eventCache, function(evt){
				me[evt] = function(fn){
					if( $.isFunction(fn) ){
						this.bind(evt, fn);
					}else if( fn === undefined ){
						this.callEvent(evt);
					}
					//返回对象本身，以支持语法连写
					return this;
				};
			});
			//仅仅允许注册一次
			this.regEvent = function(){};
		},
		//激发注册事件
		//如果有注册的事件返回false，则返回false，否则以第一个非undefined的返回内容作为返回值
		// cacheTime [可选]时间缓冲设置，默认0，不缓冲，缓冲类型为后到优先类型
		// evt [必选]事件名
		// para [可选，多组]传递的事件参数
		callEvent : function(cacheTime, evt, para){
			var t, evts, n, i, p, ret, r, com = this, p = Array.prototype.slice.call(arguments,1);
			//如果设置了cacheTime，则是缓存模式，事件将无返回值
			if( !isNaN(cacheTime) ){
				t = parseInt(cacheTime) || 200, evts = this.getEventCache()[evt], n = evts.length;
				//没有挂接事件，则返回
				if( n == 0 )
					return;
				//缓冲参数
				evts.paras = p;
				if( !evts.t ){ //如果没有定时器，则定时处理
					evts.t = window.setTimeout(function(){
						delete evts.t;
						com.callEvent.apply(com, evts.paras);
					}, t);
				}
				return;
			}
			//如果无缓冲，则正常处理事件返回值
			evt = cacheTime;
			evts = this.getEventCache()[evt] || [], n = evts.length, i=0;
			//没有挂接事件，则返回
			if( n == 0 )
				return;
			//遍历事件，调用
			for(; i<n; i++){
				r = evts[i].apply(this, p);
				if( ret === undefined || r === false )
					ret = r;
			}
			return ret;
		},
		//绑定事件（bind），支持对象形式多组绑定
		bind : function(evt, fn){
			//如果是对象列表，则逐个绑定
			if( Game.getType(evt) == "object" ){
				for(var k in evt){
					this.bind(k, evt[k]);
				}
				//返回对象本身，以支持语法连写
				return this;
			}
			var evts = this.getEventCache()[evt];
			if( evts ){ //仅仅允许注册合法事件
				$.isFunction(fn) && evts.push(fn);
			}else{
				window.alert("未知的事件类型（"+ evt +"）！请检查大小写。");
			}
			return this;
		},
		//卸载事件
		unbind : function(evt, fn){
			//如果是对象列表，则逐个解绑
			if( Game.getType(evt) == "object" ){
				for(var k in evt){
					this.unbind(k, evt[k]);
				}
				return this;
			}
			var evts = this.getEventCache()[evt];
			if( evts ){ //仅仅允许卸载同一个函数引用的处理
				this.getEventCache()[evt] = fn ? $.grep(evts, function( FN, i ){
					return FN !== fn;
				}) : [];
			}else{
				window.alert("未知的事件类型（"+ evt +"）！请检查大小写。");
			}
			return this;
		},
		extend : function( obj ){
			$.extend(this, obj);
			return this;
		}
	};
	
	/*
	 * 定时任务组件
	 * 2014-01-22 马超 新增
	 * Game.setTask(task, interval, max); @return  一个任务标识，数字类型
	 *		task		任务函数，接受三个参数，第一个参数是回调，用于通知任务执行结果， 第二个参数是当前任务执行次数，第三个参数是最大执行次数
	 *					如果不通知任务执行结果，则不继续执行下次的定时任务，如果执行结果失败，则继续执行，如果执行成功，则终止任务
	 * 		interval	任务频率（非精确），单位是秒，如果设置为0，则仅仅执行一次
	 *		max			任务执行最大次数，如果不设置或设置为0，则永不停止，除非任务执行成功
	 * Game.clearTask( id );
	 *		id			任务执行标识，用于强制终止任务
	 */
	(function(G){
		//任务缓存
		var guid = 1, taskCache = {}, timer;
		//任务
		var Task = function( task, interval, max ){
			this.work = task;
			this.guid = guid ++;
			this.runNum = 0;
			this.interval = isNaN(interval) ? 0 : +interval * 1000;
			this.maxNum = this.interval ? max||0 : 1;
			taskCache[this.guid] = this;
			Task.start();
		};
		Task.start = function(){
			if( timer )return;
			//基本定时长度为250ms
			timer = window.setInterval(Task.loop, 250);
		};
		Task.loop = function(){
			var n = 0;
			for(var id in taskCache){
				n ++;
				taskCache[id].run();
			}
			if( n == 0 ){
				window.clearInterval(timer);
				timer = 0;
			}
		};
		Task.prototype = {
			stop : function(){
				if( taskCache[this.guid] ){
					delete taskCache[this.guid];
				}
				this.running = 0;
				this.wrok = null;
				Core.GC();
			},
			run : function(){
				var now = new Date();
				//如果上次任务尚未执行返回，则忽略一次
				if( this.running )
					return;
				
				//如果还没有运行过，或上次运行时间距离现在的时间超过设置的间隔
				if( !this.lastRunTime || (now - this.lastRunTime >= this.interval && this.interval && (this.runNum < this.maxNum || !this.maxNum)) ){
					this.running = 1;
					this.lastRunTime = now;
					this.runNum ++;
					try{
						//传递this对象做为隐式控制接口
						this.work( $.proxy(this.notice, this), this.runNum, this.maxNum );
					}catch(e){
						Core.log && Core.log("定时任务("+ this.guid +")运行错误！任务已经强制终止！", e);
						this.stop();
					}
				}
				//如果达到最大运行次数，则强制停止
				if( this.maxNum && this.runNum >= this.maxNum )
					this.stop();
			},
			notice : function( ok ){
				//修改运行标志位
				this.running = 0;
				//如果运行成功，则终止任务
				if( ok ) this.stop();
			}
		};
		/*
		 * （接口）设置任务
		 */
		G.setTask = function(task, interval, max){
			if( !$.isFunction(task) ) return -1;
			var ctr = new Task(task, interval, max);
			return ctr.guid;
		};
		/*
		 * （接口）取消任务
		 */
		G.clearTask = function( id ){
			if( !id )return;
			taskCache[id] && taskCache[id].stop();
		};
	})(G);
	
	/*
	 * 扩展Game
	 */
	$.extend(G, {
		config : {
			cdnUrl : 'http://img3.126.net/caipiao',
			versionId :  +new Date()
		},
		format : $.format,
		getType : getType,
		
		//这两个方法必须在Core.navInit 调用后调用以获得正确的配置信息
		getVersionId : function() {return Core.version || this.config.versionId;},
		getCdnUrl : function(){ return Core.cdnUrl || this.config.cdnUrl;},
		
		//组合次数 从n个中选取m个有几种
        c : function(n, m) {
			n = +n;
			m = +m;
			if(getType(n) === 'number' && getType(m) === 'number') {
				var n1 = 1, n2 = 1;
				for(var i = n, j = 1; j <= m; n1 *= i--, n2 *= j++);
				return n1 / n2;
			}
			return 0;
		},
		// 组合结果[1,2,3,4,5,6,7], 3 从数组中取三个有多少种组合
		CR : function(arr, num) {
			var result = [],
				rec = function(arr, result, current, start, n) {
					var m, l = arr.length - n, i;
					for (i = start; i <= l; i++) {
						m = current.slice(0);
						m.push(arr[i]);
						if (n == 1) {
							result.push(m);   
						} else {
							rec(arr, result, m, i + 1, n - 1);
						}
						
					}
				};
			rec(arr, result, [], 0, num);
			return result;
		},
		// 定制方法，算排列组合  arr 是个数组，可以是 就是将当前的选择传入即可    num 串数，可以是数组，整形
		c1: function(arr, num) {
			var r = 0,
				n,
				self = G,
				temp,
				kk,
				nn = 0;
			if (getType(arr[0]) !== "array") {
				arr= self.groupNum(arr);
			}
			if (getType(num) !== "array") {
				num = [num];
			}
			n = arr.length;
			kk = function(i, a) {
				temp.push(Math.pow(arr[i][0], a) * self.c(arr[i][1], a));
			};
			(function f(t, i) {
				if (i == n) {
					nn = 0;
					temp = self.addArr(t);
					for (var i = 0; i < num.length; i++) {
						if (num[i] == temp) {
							nn += 1;
						}
					};
					if (nn > 0) {
						temp = [];
						$.each(t, kk);//改temp的值
						r += self.multipleArr(temp) * nn;
					} 
					return;
				}
				for (var j = 0; j <= arr[i][1]; j++) {
					f(t.concat(j), i + 1);
				}
			})([], 0);
			return r;
		},
		//定制方法，算排列组合，带胆码，事实上通常都调用这个方法，就可以了   t 托球 如[[1,2],[2,2]]   d 胆球 如[[1,2],[2,2]]  num 串数，可以是数组 
		c2: function(t, d, num) {
			var dn = 0,
				mp = 1,
				n = 0,
				self = G;
			if (getType(t[0]) !== 'array') {
				t = self.groupNum(t);
			}
			if (getType(d[0]) !== 'array') {
				d = self.groupNum(d);
			}
			if (getType(num) !== "array") {
				num = [num];
			}
			for (var i = 0, l = d.length; i < l; i++) {
				dn += d[i][1];
				mp *= Math.pow(d[i][0], d[i][1]);
			}
			if (dn == 0) {
				n = self.c1(t, num);
			} else {
				$.each(num, function(m) {
					n += num[m] > dn ? self.c1(t, num[m] - dn) * mp : self.c1(d, num[m]);
				});
			}
			return n;
		},
		//将数组中相同的元素分组，并记其次数
		groupNum : function(arr) {
			var r = [], o = {};
			if (getType(arr) === 'array') {
				for (var i = 0, l = arr.length; i < l; i++) {
					o[arr[i]] ? o[arr[i]]++ : o[arr[i]] = 1;
				}
				for (var j in o) {
					r.push([j, o[j]]);				
				}
			}
			return r;
		},
		//数组相乘
		multipleArr : function(arr){
			var n = 1;
			if (getType(arr) === 'array') {
				for(var i = 0, l = arr.length; i < l; i++)
				n *= arr[i];
				return n;
			} else {
				return 0;
			}
		},
		//对数组元素排序
		sortNum : function(arr, ad) {
			var f = ad != "desc" ? function(a, b) {
				return a - b
			} : function(a, b) {
				return b - a
			};
			if (getType(arr) === 'array') {
				return arr.sort(f);
			} else {
				return arr;
			}
		},
		//数组相加
		addArr : function(arr) {
			var n = 0;
			if (getType(arr) === 'array') {
				for(var i = 0, l = arr.length; i < l; i++) {
					n += arr[i];
				}
			}
			return n;
		},
		//实现数组的indexOf方法--没找到返回-1 找到了，返回位置，位置从0开始
		indexOf : function(arr, val) {
			if (getType(arr) === 'array' && arr.length) {
				if (Array.prototype.indexOf) {
					return Array.prototype.indexOf.call(arr, val);
				} else {
					for (var i = 0; i < arr.length; i++) {
						if (arr[i] == val) {//不做===的判断
							return i;
						}
					};					
				}
				return -1 ;
			}
			return -1;
		},
		//随机方法，从arr个数字中随机num个  arr可以是一个数组，中有元素，元素可以是任意对象或字符串或数字，也可以是字符串如1-9指从1到9个数字中随机num不能超过arr数组的长度
		//如果是数字会排序
		random : function(arr, num) {
			var randomArr = [], s, e, i = 0,
			num = +num, reg = /^\d+$/;
			if( typeof arr === 'string') {
				arr = arr.split('-');
				if(arr.length === 2) {
					s = +arr[0];
					e = +arr[1];
					arr = [];
					if(typeof s == 'number' && e && s < e) {
						for( i = s; i <= e; i++) {
							arr.push(i);
						}
					}
				}
			}
			if($.isArray(arr) && num && num < arr.length) {
				arr = arr.slice(0);//拷贝一遍数组，避免修改传入的数组
				for( i = 0; i < num; i++) {
					this.randomSortArr(arr);
					var index = Math.ceil(Math.random() * (arr.length - 1));
					randomArr.push(arr[index]);
					arr.splice(index, 1);
				}
			} else if(num == arr.length) {
				reg.test(arr.join('')) && arr.sort(function(a,b){return +a-+b;})
				return arr;
			}
			reg.test(arr.join('')) && randomArr.sort(function(a,b){return +a-+b;});
			//对随机后的结果排序
			return randomArr;
		},
		//对数组随机排序
		randomSortArr : function(arr) {
			if($.isArray(arr)) {
				arr.sort(function(a, b) {
					return Math.random() > 0.5 ? -1 : 1;
				});
			}
			return arr;
		},
		
		/*
		 * 补足两位数字
		 * num  输入的数字[整数]
		 * pos  要显示的位数，默认为2，当num位数不足时，前缀补零
		 */
		fillZero : function( num, pos ){
			var N = Math.floor(+num), str = N+"", holder = [], p = pos||2, n = p-str.length, i = 0;
			for(; i<n; i++)	holder[i] = "0";
			return n > 0 ? holder.join("")+N : N+"";
		},
		
		/*
		 * 多个数组合并排重
		 */
		unique : function(){
			var result = [], arrs = Array.prototype.slice.call(arguments, 0), n = arrs.length, i = 0, arr;
			if( n == 0 )return;
			for(; i<n; i++){
				arr = arrs[i];
				$.each(arr, function(i, v){
					if( $.inArray(v, result) < 0 ){
						result.push(v);
					}
				});
			}
			//排序
			result.sort();
			return result;
		},
		
		/*
		 * 限制函数递归
		 * 2013-05-07 曹建雄 增加
		 * fn [必选]一个函数， 调用该方法后将返回一个新的函数，这个函数会做 fn做的事情，但是这个方法无法被递归，一旦递归则会返回owner或者 windo
		 * owner 函数所有者
		 * [警告]，fn中如果有异步处理，该方法则会失效
		 */
		getStopRecursionFn: function(fn, owner) {return $.getStopRecursionFn(fn, owner);},

		/*
		 * 动态加载Game下js模块
		 */
		loadMolude : function(name, fn) {//加载模块,异步加载每个模块的代码  参数是模块名称
			var data;
			fn = $.isFunction(fn) ? fn : $.noop;
			if( typeof name === 'string') {
				data = ex(name);
				if(data) {
				  data === true ? fn() : load('script', data, fn);
				}
			} else if($.isArray(name)) {//如果是数组，则并发加载 如果其中有一个地址是错误的，则不会调用回调
				var N = name.length, i = 0, lastNum = N, loadOK = $.isFunction(fn) ? function() {--lastNum == 0 && fn();
				} : $.noop;
				for(; i < N; i++) {
					data = ex(name[i]);
					if(data) {
						data === true  ? loadOK() : load('script', data, loadOK);
					}
				}
			}
		},
		
		/*
		 * 检查是当前彩种是否停售
		 * info		销售配置对象或彩种gameEn
		 * callback	处理回调，-1 ajax查询错误 -2 配置参数有误 1 已经停售并打开停售对话框 0正常销售
		 * 2013-01-29 马超 增加对春节停售的处理
		 */
		checkGamePause : function( info, callback ){
			var fn = callback || function(){}, check = function( config ){
				if( config.gameStatus !== 0 ){//没有停售，则不处理
					fn( 0 );
					return;
				}

				if(config.stopType == 6){//春节停售
					//弹框提示
					$.dialog({
						type : "shell",
						content : '<div id="festivalDialog"><p>您可以购买：<a href="#" style="margin-left: 7px;">双色球</a><a href="#">大乐透</a><a href="#">返回首页</a></p><a class="maShangFaLink" href="#"></a></div>',
						width : 641,
						height : 371
					});

					fn( 999 );
					G.gameStop = true;
					return;
				}

				G.gameStop = true;
				G.loadMolude("notice.pause", function(){ G.notice.pause(config, fn); });
			};
			if( G.getType(info)==="object" ){
				check(info);
			}else{//异步请求
				//所有彩种投注页 添加推广图 2013-12-26 by machao, lifenping
				jQuery(function(){ Core.popularizeConfigForBetPage(info); });
				Core.get("/order/queryStopSaleInfo.html", {gameEn:info}, function(hasErr, data){
					if( hasErr ){ fn( -1 ); return; }
					check( this.parseJSON(data) );
				});
			}
		},
		
		/*
		 * 创建一个基础组件对象，并且入库（Game.COMS）
		 * 2013-02-06 马超
		 * 〔注〕为了控制复杂度，基础组件仅仅支持一个可配参数
		 */
		regBaseCom2Lib : function(name, events, prototype){
			var com = function( ops ){
				//创建私有事件缓存
				var eventCache = {};
				this.getEventCache = function(){return eventCache};
				//注册事件
				events && this.regEvent(events);
				//创建私有缓存
				this.init( ops );
				return this;
			};
			com.fn = com.prototype = new COM();
			//强制检查预定义接口覆盖情况
			if( !prototype || !prototype.init ){
				alert("原型链数据错误，缺少init构造函数！");
				return;
			}
			//扩充原型
			$.extend(com.fn, prototype);
			//入库
			var path = name.split("."), n=path.length, i=0, obj = this;
			for(; i<n; i++){
				if( i<n-1 ){ //中间路径仅仅检查，不做修改
					obj = obj[path[i]] = obj[path[i]]||{};
				}else{ //末级节点保存组件
					obj[path[i]] = com;
				}
			}
			//返回
			return com;
		},
		
		/*
		 * 创建一个业务组件实例
		 * 2013-02-06 马超
		 *〔注〕如果是异步加载，则通过回调函数返回创建好的实例，否则立即返回
		 */
		createCom : function(name, options, callback){
			//查找目标组件
			var findCom = function( name ){
					var targetCom = G||{}, path = name.split("."), n=path.length, i=0;
					for( ; i<n; i++ ){
						targetCom = targetCom[path[i]];
						if( !targetCom ) break;
					}
					return targetCom;
				};
			//如果对象已经存在，则直接创建
			var targetCom = findCom(name), com = targetCom ? new targetCom( options ) : null;
			if( com ){
				//如果有回调，则通知回调
				$.isFunction(callback) && callback( com );
				//直接返回
				return com;
			}
			//动态加载模块
			this.loadMolude(name, function(){
				var targetCom = findCom(name), com = targetCom ? new targetCom( options ) : null;
				//如果有回调，则通知回调
				$.isFunction(callback) && callback( com );
			});
		},
		
		/*
		 * 弹框包装
		 * 2013-02-16 马超 增加，以修改默认值
		 */
		dialog : function( op, callback ){
			$.dialog($.extend({}, op, {
				title: op.title || "提示",
				css : op.className || "betDialog",
				button: op.button || ["*确定"],
				content: "<div class='betDialogContent'><em></em><div class='betDialogContent2'>"+ op.content +"</div></div>",
				width : op.width == undefined ? 430 : op.width,
				height: op.height || 0,
				dragable: 1,
				animate : 0,
				check : op.check || $.noop
			}), callback||$.noop);
		},
		alert : function(content, btn, callback, _defBtn){
			if( $.isFunction(btn) ){
				callback = btn;
				btn = null;
			}
			G.dialog({content:content, button:btn||_defBtn}, callback);
		},
		confirm : function(content, btn, callback){
			G.alert(content, btn, callback, ["*确定", "取消"]);
		},
		
		/*
		 * 当显示金额可能超过亿元的时候
		 * 进行缩略显示
		 * 2013-12-24 马超 增加
		 */
		getMoneyText : function( num ){
			if( isNaN(num) )return num;
			var sp = 1e8, SP = 1e12;
			return num < sp ? num : num < SP ? ("<span title='"+ num +"元！\n土豪，我们做朋友吧！'>"+ (num/sp).Round(2) +"亿</span>") : ("<span title='"+ num +"元！\n震精！请问您是哪路高人？'>"+ (num/SP).Round(2) +"万亿</span>");
		}
	});
})(window, Core, jQuery);