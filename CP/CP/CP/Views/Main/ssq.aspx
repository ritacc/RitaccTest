﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<link rel="shortcut icon" href="/favicon.ico"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="application-name" content=""/>
<meta name="msapplication-TileColor" content="#b82337"/>
<meta name="msapplication-TileImage" content="e0226958-5751-416d-bee6-4f2a40735ac2.png"/>
<meta name="keywords" content="双色球机选,双色球复式"/>
<meta name="description" content=""/>
<title>双色球机选号码_双色球复式投注_双色球在线机选-XX彩票网</title>

<link href="/Content/csscp/base.css" rel="stylesheet" type="text/css" />
<link href="/Content/csscp/core.css" rel="stylesheet" type="text/css" />

<link rel="stylesheet" href="/Content/csscp/lotteryBet/scz/common.css?180299"/>
<link rel="stylesheet" href="/Content/csscp/lotteryBet/scz/ssq.css?180299"/>
<link rel="stylesheet" href="/Content/csscp/lotteryBet/luckybuy.css?180299"/>
<link rel="stylesheet" href="/Content/csscp/popularize/forFrame.css?180299"/>
<link rel="stylesheet" href="/Content/csscp/lotteryBet/history.css?180299"/>

<script src="/Content/js2/jquery-1.4.2.js?201205221605"></script>
<script src="/Content/js2/globalConfig.js"></script>
<script src="/Content/js2/easyCore.js?179842"></script>
</head>
<body class="">
<noscript>
    <div id="noScript">
        <div><h2>请开启浏览器的Javascript功能</h2><p>亲，没它我们玩不转啊！求您了，开启Javascript吧！<br/>不知道怎么开启Javascript？那就请
        <a href="http://www.baidu.com/s?wd=%E5%A6%82%E4%BD%95%E6%89%93%E5%BC%80Javascript%E5%8A%9F%E8%83%BD" rel="nofollow" target="_blank">猛击这里</a>！</p></div>
    </div>
</noscript>

<nav id="topNav">
	<div id="topNavWrap">
		<div id="topNavLeft"><script>Core.navInit("http://localhost:555/", "ritacc@126.com", "180299", "1396695306010")</script></div>

		<ul id="topNavRight">
			<li><a href="#" id="myEpay" notice="false" target="_blank">我的彩票网</a>&nbsp;&nbsp;<span id="topEpayInfo"></span>|</li>
			<li>
                <div class="mcDropMenuBox myorder">
				    <a target="_top" user="y" class="topNavHolder" href="#" rel="nofollow"><em class="text_icon"></em>我的订单<i></i></a><b class="holderLine">|</b>
				    <div class="mcDropMenu">
					    <a target="_top" user="y" href="#" rel="nofollow">定制跟单</a>
				    </div>
			    </div>
            </li>
		</ul>
	</div>
</nav>

<header id="docHead" rel="">
    <div id="docHeadWrap">
	    <a href="#" class="logoLnk" title="彩票" hidefocus="true"><h1>彩票<img src="#" alt="彩票" title="彩票网"/></h1></a>
	    <a href="#" rel="nofollow" class="guideLnk" target="_blank" hidefocus="true"><span>彩票</span></a>
	    <p>
		    <span class="serviceTel">
			    <span class="serviceTel_tel">
			    <span>客服热线</span><br/>
			    <strong>000-88888080</strong>
			    </span>
                <a class="onlineService " href="#" target="_blank">在线客服</a>
		    </span>
	    </p>
    </div>
</header>

<nav id="topTabBox">
	<div id="topTab">
		<ul id="funcTab">
        <li id="lotteryListEntry"><a class="topNavHolder" hidefocus="true" rel="nofollow">选择彩种<i></i></a>
            <div id="lotteryList">
                <div class="lotteryListWrap">
		        <ul>
		            <li class="zyGame"><a href="ssq.aspx" gid="ssq"><em class="cz_logo35 logo35_ssq"></em>
                        <strong>双色球</strong></a>
                    </li>
		            <li class="zyGame"><a href="#" gid="jczq"><em class="cz_logo35 logo35_jczq"></em><strong>竞彩足球</strong></a></li>
                    <li class="zyGame"><a href="#" gid="jclq_mix_p"><em class="cz_logo35 logo35_jclq"></em><strong>竞彩篮球</strong></a></li>
		            <li class="zyGame"><a href="#" gid="d11"><em class="cz_logo35 logo35_d11"></em><strong>11选5</strong></a></li>
		            <li class="zyGame"><a href="#" gid="gxkuai3"><em class="cz_logo35 logo35_gxkuai3"></em><strong>新快3</strong></a></li>

		            <li class="otherGames clearfix">
			            <h3>11选5</h3>
			            <div>
				            <em class="left"><a href="#" title="猜对1个号就中奖，每天84期" gid="gdd11">广东11选5</a></em>
				            <em><a href="#" title="每天78期，任猜1-8个号都中奖" gid="jxd11">老11选5</a></em>
				            <em class="left"><a href="#" title="猜中一个号就中奖，返奖率59%" gid="hljd11">好运11选5</a></em>
				            <em><a href="#" title="5分钟一期，最高奖500万" gid="kl8">快乐8</a></em>
				            <em class="left"><a href="#" title="10分钟一期，快乐猜大小" gid="kuai3">快3</a></em>
				            <em><a href="#" title="最容易中奖，全天82期" gid="oldkuai3">老快3</a></em>				
				            <em class="left"><a href="#" title="10分钟一期，最高奖11.6万" gid="jxssc">新时时彩</a></em>
				            <em><a href="#" title="独有夜间版，01：55截止" gid="ssc">老时时彩</a></em>
				            <em class="left"><a href="#" title="猜中一个号就中奖，5分钟一期" gid="kuai2">快2</a></em>
			            </div>
		            </li>
		            <li class="otherGames clearfix">
			            <h3>时时彩</h3>
			            <div>
				            <em class="left"><a href="#" title="猜一场即中奖" gid="kl8">快乐8</a></em>
				            <em><a href="#" gid="jxssc">湖南幸运赛车</a></em>
				            <em class="left"><a href="#" title="快乐十分" gid="football_sfc">快乐十分</a></em>
				            <em><a href="#" gid="kuai3">PK10</a></em>
				            <em class="left"><a href="#" title="重庆时时彩" gid="football_dcspf">重庆时时彩</a></em>
                            <em class="left"><a href="#" title="江西时时彩" gid="football_f4cjq">江西时时彩</a></em>
				            <em><a href="#" gid="football_sfc">黑龙江时时彩</a></em>
				            <em class="left"><a href="#" title="新疆时时彩" gid="football_bqc">新疆时时彩</a></em>
				            <em><a href="#" gid="football_9">天津时时彩</a></em>
				            <em class="left"><a href="#" title="上海时时彩" gid="football_9">上海时时彩</a></em>
			            </div>
		            </li>
		            <li class="otherGames end clearfix">
			            <h3>低频彩</h3>
			            <div>
				            <em class="left"><a href="#" title="2元赢取1000元，天天开奖" gid="x3d">福彩3D</a></em>
				            <em><a href="#" title="大奖500万 每周一、三、五开奖" gid="qlc">体彩P3P5</a></em>
			            </div>
		            </li>
	            </ul>
	        </div>
            </div>
        </li>
        <li pid="home"><a href="/">首页</a>|</li>
			<li pid="hall"><a href="#" title="彩票购彩大厅">购彩大厅</a>|</li>
			<li pid="award" class="wordsNum4"><a href="#" title="中国彩票开奖">彩票开奖<i></i></a>|
                <div class="mcDropMenu">
                    <a href="#">开奖公告</a>
                    <a href="#" >中奖排行</a>
                </div>
            </li>
            <li pid="trend"><a href="#" title="福彩体彩走势图">走势图</a>|</li>
            <li pid="cpTx"><a href="#" title="提现">提现</a>|</li>
            <li pid="cpTx"><a href="#" title="提现">个人</a>|</li>
            <li pid="cpHd"><a href="#" title="代理">代理</a>|</li>
            <li pid="cpHd"><a href="#" title="公告">活动</a>|</li>
            <li pid="cpInfo"><a href="#" title="公告">公告</a>|</li>
            <li pid="mobile"><a href="#" title="邮件">邮件</a></li>
        </ul>
	</div>
</nav>

<article class="docBody putong clearfix" id="docBody">
<script>    window.Core.jfb = { "showTip": "", "rel": "", "ygtime": "1396713600000" } </script>

<header class="game_header clearfix">
<div class="headerBox">
	<div class="clearfix titleBox" game_en='ssq'>
		<span class="cz_logo cz_ssq"></span>
		<h1><a href="#">双色球</a></h1>
		<span class="today_kj" title="今日开奖"></span>
		<span class="gameintr">每注2元，最高奖1000万</span>
		<span>　投注截止时间: 2014-04-06 19:57　全网最晚截止</span>
		<strong class="gameperiod">第<span id="currentPeriod">2014037</span>期</strong>
	</div>

    <div class="clearfix menuBox">
        <ul class="gameMenu">
	        <li rel="1" ><a hidefocus="true" href="#">快捷投注</a></li>
	        <li rel="2" class="active"><a hidefocus="true" href="#">高级投注</a></li>
        </ul>
	    <div class="countDown">
		    <p class="betTimer" rel='38995134' rel2="0"></p>
		    <p class="groupbuyTimer hide" rel='38995134' rel2="0"></p>
	    </div>
    </div>
</div>
</header>

	<section class="main clearfix ssq_main">
		<nav class="betNav">
            <ul class="navList">
	            <li rel="2" class="active"><a hidefocus="true" href="#">常规</a><em></em></li>
	            <li rel="3" ><a hidefocus="true" href="#">胆拖</a><em></em></li>
	            <li rel="4" ><a hidefocus="true" href="#">定胆杀号</a><em></em></li>
            </ul>
            <div class="moreLinks"> 
                <a href="#" target="_blank">走势图</a> 
                <a href="#" target="_blank">预测</a> 
                <a href="#" target="_blank" class="jtip" tip="#ssqWfTip">玩法说明<i class="questionMark"></i></a> 
                <a href="javascript:;" target="_blank" class="jtip" tip="#ssqaward_popuTip" >中奖细则<i class="questionMark"></i></a> 
            </div>
		</nav>
		<section class="bettingBox">
			<div id="mainPanels" class="newbieGuideStep1">
<div class="selectNum cleafix">
	<div class="title">
		<h2 class="redTitle"><strong>红球区</strong> <span>至少选择6个红球</span></h2>
		<h2 class="blueTitle"><strong>蓝球区</strong> <span>至少选择1个蓝球</span></h2>
	</div>
	<div id="statisticsWrap" class="hide showHotCold"></div>
	<div class="ballarea clearfix">
		<p class="omitTip jtip" inf="遗漏是指该号码自上次开出以来至本次未出现的期数">遗漏<i></i></p>
		<div class="redBallBox" >
	<ul class="clearfix">
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">01</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">02</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">03</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">04</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">05</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">06</a>
			<span >8</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">07</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">08</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">09</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">10</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">11</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">12</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">13</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">14</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">15</a>
			<span class="maxomit">15</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">16</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">17</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">18</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">19</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">20</a>
			<span >12</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">21</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">22</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">23</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">24</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">25</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">26</a>
			<span >14</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">27</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">28</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">29</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">30</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">31</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">32</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">33</a>
			<span >1</span>
		</li>
	</ul>
			<p class="random_selection">
				<select name="">
					<option value="6" selected="selected">6</option>
					<option value="7">7</option>
					<option value="8">8</option>
					<option value="9">9</option>
					<option value="10">10</option>
					<option value="11">11</option>
					<option value="12">12</option>
					<option value="13">13</option>
					<option value="14">14</option>
					<option value="15">15</option>
					<option value="16">16</option>
					<option value="17">17</option>
					<option value="18">18</option>
					<option value="19">19</option>
					<option value="20">20</option>
				</select>
				<a hidefocus="true" class="radom_redbtn jtip" href="javascript:;" rel="nofollow" inf="连续点击将获取不同的红球" >机选红球</a>
				<a hidefocus="true" class="clearing" href="javascript:;" rel="nofollow">清空</a>
				</p>
		</div>
		<div class="blueBallBox">
	<ul class="clearfix">
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">01</a>
			<span >9</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">02</a>
			<span >25</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">03</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">04</a>
			<span class="maxomit">31</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">05</a>
			<span >8</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">06</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">07</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">08</a>
			<span >20</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">09</a>
			<span >12</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">10</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">11</a>
			<span >26</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">12</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">13</a>
			<span >23</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">14</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">15</a>
			<span >16</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">16</a>
			<span >11</span>
		</li>
	</ul>
			<p class="random_selection">
				<select name="">
					<option value="1" selected="selected">1</option>
					<option value="2">2</option>
					<option value="3">3</option>
					<option value="4">4</option>
					<option value="5">5</option>
					<option value="6">6</option>
					<option value="7">7</option>
					<option value="8">8</option>
					<option value="9">9</option>
					<option value="10">10</option>
					<option value="11">11</option>
					<option value="12">12</option>
					<option value="13">13</option>
					<option value="14">14</option>
					<option value="15">15</option>
					<option value="16">16</option>
				</select>
				<a hidefocus="true" class="radom_bluebtn jtip" href="javascript:;" rel="nofollow" inf="连续点击可获取不同的蓝球" >机选蓝球</a>
				<a hidefocus="true" class="clearing" href="javascript:;" rel="nofollow">清空</a>
				</p>
		</div>
		<p class="selectInfo"><span>您当前选中了<strong class="c_ba2636">0</strong>个红球，<strong class="c_1e50a2">0</strong>个蓝球，共<strong class="c_ba2636">0</strong>注，共<strong class="c_ba2636">0</strong>元</span><i></i></p>
	</div>
</div>

<div class="selectNum dantuo hide">
	<div class="title">
		<p class="playTip"><strong>玩法提示：</strong>红球胆码：选择1～5个你认为必出的；红球拖码：至少选择2个你认为可能出的；蓝球至少选1个。</p>
	</div>
	<div id="statisticsWrap2" class="hide showHotCold"></div>
	<div class="dantuo_com">
		<h2><strong>胆码区<em>您认为<span class="c_ba2636">必出</span>的号码</em></strong><i></i></h2>
		<div class="danMa clearfix">
			<p class="omitTip jtip" inf="遗漏是指该号码自上次开出以来至本次未出现的期数">遗漏<i></i></p>
			<div class="redBallBox" >
					<p class="dantuoCom_title"><strong class="c_ba2636">红球</strong>至少选择1个，最多5个</p>
	<ul class="clearfix">
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">01</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">02</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">03</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">04</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">05</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">06</a>
			<span >8</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">07</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">08</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">09</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">10</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">11</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">12</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">13</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">14</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">15</a>
			<span class="maxomit">15</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">16</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">17</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">18</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">19</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">20</a>
			<span >12</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">21</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">22</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">23</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">24</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">25</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">26</a>
			<span >14</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">27</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">28</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">29</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">30</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">31</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">32</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">33</a>
			<span >1</span>
		</li>
	</ul>
			</div>
		</div>
		<h2><strong>拖码区<em>您认为<span class="c_ba2636">可能出</span>的号码</em></strong><i></i></h2>
		<div class="tuoMa">
			<p class="omitTip jtip" inf="遗漏是指该号码自上次开出以来至本次未出现的期数">遗漏<i></i></p>
			<div class="redBallBox" >
					<p class="dantuoCom_title"><strong class="c_ba2636">红球</strong>至少选择2个</p>
	<ul class="clearfix">
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">01</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">02</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">03</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">04</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">05</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">06</a>
			<span >8</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">07</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">08</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">09</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">10</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">11</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">12</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">13</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">14</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">15</a>
			<span class="maxomit">15</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">16</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">17</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">18</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">19</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">20</a>
			<span >12</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">21</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">22</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">23</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">24</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">25</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">26</a>
			<span >14</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">27</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">28</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">29</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">30</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">31</a>
			<span >2</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">32</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">33</a>
			<span >1</span>
		</li>
	</ul>
			</div>
			<div class="blueBallBox">
					<p class="dantuoCom_title"><strong class="c_1e50a2">蓝球</strong>至少选择1个</p>
	<ul class="clearfix">
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">01</a>
			<span >9</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">02</a>
			<span >25</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">03</a>
			<span >0</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">04</a>
			<span class="maxomit">31</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">05</a>
			<span >8</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">06</a>
			<span >1</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">07</a>
			<span >4</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">08</a>
			<span >20</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">09</a>
			<span >12</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">10</a>
			<span >6</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">11</a>
			<span >26</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">12</a>
			<span >3</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">13</a>
			<span >23</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">14</a>
			<span >5</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">15</a>
			<span >16</span>
		</li>
		<li>
			<a href="javascript:;" hidefocus="true" rel="nofollow">16</a>
			<span >11</span>
		</li>
	</ul>
			</div>
		</div>
		<p class="selectInfo"><span>您选择了<strong class="c_ba2636">0</strong>个红球，(0个胆码，0个拖码)
        <strong class="c_1e50a2">0</strong>个蓝球，共<strong class="c_ba2636">0</strong>注，共
        <strong class="c_ba2636">0</strong>元</span><i></i></p>
	</div>
</div>

<div class="selectNum dingdan cleafix hide">
	<div class="title">
		<h2 class="redTitle"><strong>红球区</strong> <span>红球 可选胆码(1-5)个 可杀号(1-27)个</span></h2>
		<h2 class="blueTitle"><strong>蓝球区</strong> <span>蓝球 可杀号(1-15)个</span></h2>
	</div>
	<div id="statisticsWrap3" class="hide showHotCold"></div>
	<div class="killballarea clearfix">
    	<p class="useTip">使用说明：同一个号码点一下为"定胆"、点两下为"杀号"、点击三下"还原"</p>
		<p class="omitTip jtip" inf="遗漏是指该号码自上次开出以来至本次未出现的期数">遗漏<i></i></p>
		<div class="redBallBox" >
	    <ul class="clearfix">
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">01<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">02<i></i></a>
			    <span >4</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">03<i></i></a>
			    <span >2</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">04<i></i></a>
			    <span >2</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">05<i></i></a>
			    <span >3</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">06<i></i></a>
			    <span >8</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">07<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">08<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">09<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">10<i></i></a>
			    <span >5</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">11<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">12<i></i></a>
			    <span >6</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">13<i></i></a>
			    <span >3</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">14<i></i></a>
			    <span >4</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">15<i></i></a>
			    <span class="maxomit">15</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">16<i></i></a>
			    <span >5</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">17<i></i></a>
			    <span >1</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">18<i></i></a>
			    <span >6</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">19<i></i></a>
			    <span >6</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">20<i></i></a>
			    <span >12</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">21<i></i></a>
			    <span >5</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">22<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">23<i></i></a>
			    <span >3</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">24<i></i></a>
			    <span >6</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">25<i></i></a>
			    <span >2</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">26<i></i></a>
			    <span >14</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">27<i></i></a>
			    <span >5</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">28<i></i></a>
			    <span >3</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">29<i></i></a>
			    <span >4</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">30<i></i></a>
			    <span >6</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">31<i></i></a>
			    <span >2</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">32<i></i></a>
			    <span >1</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">33<i></i></a>
			    <span >1</span>
		    </li>
	    </ul>
		<p class="random_selection">
                机选
                <select name="" class="red_random_count_select">
                    <option value="6" selected="selected">6</option>
                    <option value="7">7</option>
                    <option value="8">8</option>
                    <option value="9">9</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12">12</option>
                    <option value="13">13</option>
                    <option value="14">14</option>
                    <option value="15">15</option>
                    <option value="16">16</option>
                    <option value="17">17</option>
                    <option value="18">18</option>
                    <option value="19">19</option>
                    <option value="20">20</option>
                </select>
                个<span class="c_ba2636">红球号码</span>&#12288;&#12288;&#12288;
                机选
                <select name="" class="blue_random_count_select">
                    <option value="1" selected="selected">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <option value="6">6</option>
                    <option value="7">7</option>
                    <option value="8">8</option>
                    <option value="9">9</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12">12</option>
                    <option value="13">13</option>
                    <option value="14">14</option>
                    <option value="15">15</option>
                    <option value="16">16</option>
                </select>
                个<span class="c_1e50a2">蓝球号码</span>
		</p>
		</div>
		<div class="blueBallBox">
	    <ul class="clearfix">
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">01<i></i></a>
			    <span >9</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">02<i></i></a>
			    <span >25</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">03<i></i></a>
			    <span >0</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">04<i></i></a>
			    <span class="maxomit">31</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">05<i></i></a>
			    <span >8</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">06<i></i></a>
			    <span >1</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">07<i></i></a>
			    <span >4</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">08<i></i></a>
			    <span >20</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">09<i></i></a>
			    <span >12</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">10<i></i></a>
			    <span >6</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">11<i></i></a>
			    <span >26</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">12<i></i></a>
			    <span >3</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">13<i></i></a>
			    <span >23</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">14<i></i></a>
			    <span >5</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">15<i></i></a>
			    <span >16</span>
		    </li>
		    <li>
			    <a href="javascript:;" hidefocus="true" rel="nofollow">16<i></i></a>
			    <span >11</span>
		    </li>
	    </ul>
        <p class="random_selection">
            生成
            <select name="" class="zu_count_select">
                <option value="1" selected="selected">1</option>
                <option value="2">2</option>
                <option value="5">5</option>
                <option value="10">10</option>
                <option value="20">20</option>
                <option value="50">50</option>
                <option value="100">100</option>
            </select>
            组号码
        </p>
		</div>
		<p class="selectInfo"><span>您当前选中了<strong>1</strong>注，共<strong class="c_ba2636">2</strong>元</span><i></i></p>
	</div>
</div>


</div>
	<div class="addbtnBox newbieGuideStep2"> <a class="addbtn disabled" rel="nofollow" title="确认选号"></a></div>
	<div class="newbieGuideStep3">
    <div class="selected_box clearfix">
	    <div id="autoSaveBox" class="jtip hide" inf="勾选后，可以帮您记住号码篮已有的数据，下次打开页面时自动填充。">
            <input type="checkbox" name="autoSavePool" id="autoSavePool"><label for="autoSavePool">记忆号码篮数据</label></div>
	    <div id="select_list_box" class="selected_list">
		    <dl></dl>
	    </div>
	    <div class="selected_btnbox"> 
		    <a hidefocus="true" class="selected_btn" num="1" rel="nofollow" href="javascript:;">机选1注</a>
            <div class="randomBtn">
        	    <label><input type="text" value=""><span>5注</span></label>
                <a hidefocus="true" class="selected_btn" num="u" rel="nofollow" href="javascript:;">机选</a>
            </div>
		    <div class="shoucangBtn">
		    </div>
		    <a hidefocus="true" class="selected_btn clear_btn" num="x" rel="nofollow" href="javascript:;">清空列表</a>
	    </div>
	    <div class="selected_history">
		    <a href="javascript:;">历史开奖号对比</a>
	    </div>
    </div>
    <div class="betresult"><span class="holderWrapLeft">共<strong class="betNumCount">0</strong>注，</span><span class="beiNumWrap">我要买<span class="addSubtract">
    <div class="jtipWraper beitouTip">
    <span>24倍可以掏空奖池&nbsp;&nbsp;<em onclick="jQuery('.beitouTip').remove()">&times;</em></span><i></i>
    </div>
    <a href="javascript:;" rel="nofollow" hidefocus="true" class="subtract"></a><input type="text" value="1"/>
        <a href="javascript:;" rel="nofollow" hidefocus="true" class="add"></a></span>倍，</span><span class="periodNumWrap">连续购买<span class="addSubtract">
        <a href="javascript:;" rel="nofollow" hidefocus="true" class="subtract"></a><input type="text" value="1"/>
        <a href="javascript:;" rel="nofollow" hidefocus="true" class="add"></a></span>期，</span><span class="holderWrapRight">共<strong class="betMoneyCount">0</strong>元&nbsp;
	    <div class="additionalTipPos">
		    <div class="jtipWraper additionalTip additionalTipHide">
			    <i></i>
			    <span><input type="checkbox" name="stopWhenFeed"> 中奖<span style="display: none;">累计 <input type="text" name="stopMoney" maxlength="6" class="zhuihaoInput"> 元</span>后停止</span>
		    </div>
	    </div>
    </span>
    </div>
        <div class="betBtnBox">
            <p class="betBtns">
	            <span id="normalBtnBox"><a herf="javascript:;" rel="nofollow" class="betting_Btn" title="立即投注"></a></span>
	            <span id="groupbuyEndBtnBox" class="hide"><a herf="javascript:;" rel="nofollow" class="groupEnd_Btn" title="合买截止"></a></span>
	            <span id="waitBtnBox" class="hide"><a herf="javascript:;" rel="nofollow" class="waitAward_Btn" title="等待开奖"></a><span><strong class="betTimer2">--分--秒</strong>后开始下一期投注</span></span>
            </p>
            <p class="treaty">
            <input type="checkbox" checked="checked" id="agree_rule" name="checkbox"/> 我已经阅读并同意<a href="#" target="_blank">《委托投注规则》 </a></p>
        </div>
    </div>
</section>
<aside class="asideBox">
<div class="totalPoolBox">
	<h3><a href="#" target="_blank">双色球奖池</a></h3>
		<p class="totalPool">
		<strong>
		1
		</strong>亿
		<strong>1525
		</strong>万
	</p>
	<p>可开出<span class="c_ba2636">23注</span>500万大奖</p>
	<p>每周二、四、日晚上21:30开奖</p>
</div>
<div class="drawNotice">
	<h3><a href="#" target="_blank">开奖公告</a></h3>
	<p>双色球第<span class="drawNoticePeriod">2014036</span>期
		04-03(周四)
	</p>
	<p class="currenAward">
			<em class="smallRedball">01</em>
			<em class="smallRedball">07</em>
			<em class="smallRedball">08</em>
			<em class="smallRedball">09</em>
			<em class="smallRedball">11</em>
			<em class="smallRedball">22</em>
			<em class="smallBlueball">03</em>
		&nbsp;&nbsp;<a href="#" target="_blank" tip="#awardInfoBox" class="more jtip">详情&gt;&gt;</a></p>
	<div id="awardInfoBox">
	<table class="awardInfoTable">
		<colgroup>
			<col width="81px"/>
			<col width="60px"/>
			<col/>
		</colgroup>
		<tbody>
		<tr>
			<th>奖项</th>
			<th>注数</th>
			<th>奖金</th>
		</tr>
		<tr>
			<td>一等奖
				(6+1)</td>
			<td>30</td>
			<td>5349374</td>
		</tr>
		<tr>
			<td>二等奖
				(6+0)</td>
			<td>351</td>
			<td>44791</td>
		</tr>
		<tr>
			<td>三等奖
				(5+1)</td>
			<td>2730</td>
			<td>3000</td>
		</tr>
		<tr>
			<td>四等奖
				(5+0/4+1)</td>
			<td>104267</td>
			<td>200</td>
		</tr>
		<tr>
			<td>五等奖
				(4+0/3+1)</td>
			<td>1633294</td>
			<td>10</td>
		</tr>
		<tr>
			<td>六等奖
				(2+1/1+1/0+1)</td>
			<td>17544912</td>
			<td>5</td>
		</tr>
		</tbody>
	</table></div>
	<div class="awardInfo">
		<table class="winningNumberlist">
			<tr>
				<th width="75">期次</th>
				<th>开奖号码</th>
			</tr>
			<tr>
				<td>2014036期</td>
				<td class="t_r">			
                    <em class="c_ba2636">01</em>
			        <em class="c_ba2636">07</em>
			        <em class="c_ba2636">08</em>
			        <em class="c_ba2636">09</em>
			        <em class="c_ba2636">11</em>
			        <em class="c_ba2636">22</em>
			        <em class="c_1e50a2">03</em>
                </td>
			</tr>
			<tr>
				<td>2014035期</td>
				<td class="t_r">			
                    <em class="c_ba2636">07</em>
			        <em class="c_ba2636">08</em>
			        <em class="c_ba2636">09</em>
			        <em class="c_ba2636">17</em>
			        <em class="c_ba2636">32</em>
			        <em class="c_ba2636">33</em>
			        <em class="c_1e50a2">06</em>
                </td>
			</tr>
			<tr>
				<td>2014034期</td>
				<td class="t_r">			
                    <em class="c_ba2636">01</em>
			        <em class="c_ba2636">03</em>
			        <em class="c_ba2636">04</em>
			        <em class="c_ba2636">08</em>
			        <em class="c_ba2636">25</em>
			        <em class="c_ba2636">31</em>
			        <em class="c_1e50a2">06</em>
                </td>
			</tr>
			<tr>
				<td>2014033期</td>
				<td class="t_r">			
                    <em class="c_ba2636">05</em>
			        <em class="c_ba2636">13</em>
			        <em class="c_ba2636">23</em>
			        <em class="c_ba2636">28</em>
			        <em class="c_ba2636">32</em>
			        <em class="c_ba2636">33</em>
			        <em class="c_1e50a2">12</em>
                </td>
			</tr>
			<tr class="noborderTR">
				<td>2014032期</td>
				<td class="t_r">			
                    <em class="c_ba2636">01</em>
			        <em class="c_ba2636">02</em>
			        <em class="c_ba2636">14</em>
			        <em class="c_ba2636">22</em>
			        <em class="c_ba2636">29</em>
			        <em class="c_ba2636">33</em>
			        <em class="c_1e50a2">07</em>
                </td>
			</tr>
		</table>
	</div>
</div>
<div class="trendBox">
	<h3><a href="#" target="_blank">双色球走势图</a>
    <a href="#" target="_blank" class="more">更多&gt;&gt;</a></h3>
	<p class="trendLinks">
        <a target="_blank" href="#">基本走势图</a>
        <a target="_blank" href="#">和值走势图</a>
        <a target="_blank" href="#">蓝球走势图</a><br/>
        <a target="_blank" href="#">红球三分区</a>
        <a target="_blank" href="#">红球四分区</a>
        <a target="_blank" href="#">红球七分区</a><br/>
        <a target="_blank" href="#">大小走势图</a>
        <a target="_blank" href="#">奇偶走势图</a>
        <a target="_blank" href="#">质合走势图</a><br/><span class="hide">
        <a target="_blank" href="#">连号走势图</a>
        <a target="_blank" href="#">重邻孤分析</a>
        <a target="_blank" href="#">同期查询表</a></span></p>
	<p class="traitTip hide">特色功能：绘图工具 号码分析 快速投注</p>
</div>
<div class="bigBonusBox">
	<h3><strong>大奖播报</strong></h3>
	<ul class="articleList">
		<li><span>·</span><a href="#" target="_blank">逆天!易友又中双色球头奖549万</a></li>
		<li><span>·</span><a href="#" target="_blank">福地霸气!2亿后再喷635万元头奖</a></li>
		<li><span>·</span><a href="#" target="_blank">2亿！史上最牛大奖花落XX彩票</a></li>
	</ul>
</div>
		</aside>
	</section>
<div class="jtipWraper ssqWfTip hide" id="ssqWfTip"><i></i>至少选择6个红球和1个蓝球组合成一注单式彩票。</div>
<div class="tipSpecial hide" id="ssqaward_popuTip">
	<div class="ssqaward_popuTip">
		<table>
			<tr>
				<th rowspan="2" width="42">等级</th>
				<th colspan="2">中奖条件</th>
				<th rowspan="2">奖金分配</th>
			</tr>
			<tr>
				<th width="77">红球</th>
				<th width="38">蓝球</th>
			</tr>
			<tr>
				<td>一等奖</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>浮动</td>
			</tr>
			<tr>
				<td>二等奖</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>浮动</td>
			</tr>
			<tr>
				<td>三等奖</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>3000元</td>
			</tr>
			<tr>
				<td rowspan="2">四等奖</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td rowspan="2">200元</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td rowspan="2">五等奖</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td rowspan="2">10元</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td rowspan="3">六等奖</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td rowspan="3">5元</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
		</table>
		<i class="w"></i> <i class="c"></i> <i class="m"></i>
         <p class="botLink">
            <a href="#" target="_blank">玩法规则</a>
            <a href="#" target="_blank">玩法介绍</a>  
            <a href="#" target="_blank">奖项规则</a>
        </p>
	</div>
</div>
</article>
<script src="/Content/js2/game/game.js?180299"></script>
<script src="/Content/js2/game/COMS/PT/baseCom.js?180299"></script>
<script src="/Content/js2/ssq/clientRandom.js?180299"></script>
<script src="/Content/js2/ssq/core.js?180299"></script>
<script src="/Content/js2/ssq/luckyBuy.js?180299"></script>
<script src="/Content/js2/game/history/history.js?180299"></script>
<script src="/Content/js2/ssq/index.js?180299"></script>
<script> 
</script>

<div class="hot_block clearfix">
    <h2 class="hot_title">热点导航：</h2>
    <p id="hot_block">
    <a href="#" target="_blank">福利彩票双色球</a>
    <a href="#" target="_blank">福彩3D预测分析</a>
    <a href="#" target="_blank">彩票走势图</a>
	<a href="#" target="_blank">双色球投注</a>
	<a href="#" target="_blank">福利彩票3D</a>
	<a href="#" target="_blank">足球胜负彩</a>
	<a href="#" target="_blank">竞彩足球</a>
	<a href="#" target="_blank">足彩单场</a>
	<a href="#" target="_blank">体彩11选5</a>
	</p>
</div>

<footer id="docFoot">
	<ul id="guideList">
		<li><em class="guide_2"></em><span>
			&middot; <a target="_blank" href="#" title="购彩流程">购彩流程</a><br />
			&middot; <a target="_blank" href="#" title="领奖流程">领奖流程</a><br />
		</span></li>
        <li><em class="guide_3"></em><span>
			&middot; <a target="_blank" href="#" title="新手购彩图解">新手购彩图解</a><br /> 
			&middot; <a target="_blank" href="#" title="常见问题">常见问题</a><br />
			&middot; <a target="_blank" href="#" title="功能指引">功能指引</a><br />
			&middot; <a target="_blank" href="#" title="彩种介绍">彩种介绍</a>		</span></li>
		<li><em class="guide_4"></em><span>
			&middot; <a target="_blank" href="#" rel="nofollow">网银支付</a><br />
			&middot; <a target="_blank" href="#" rel="nofollow">支付宝支付</a><br />
			&middot; <a target="_blank" href="#" rel="nofollow">手机充值卡支付</a>
		</span></li>
	</ul>
</footer>

</body>
</html>

