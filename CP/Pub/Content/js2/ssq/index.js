(function (g, e, f, c, b, d) {
    if (e.getUrlPara("orderType") == "5") { g.location.replace("/order/ssq/jixuan.html" + g.location.search); return }
    var h = c.alert, a = c.confirm, i = c.ssq.core;
    e.extend(f
        , { 
            config: { zhuihaoAjax: "/bet/queryBetType_newfollowBuy.html?gameId={gameId}"
            , hemaiAjax: "/bet/queryBetType_newgroupBuy.html?gameId={gameId}"
            , numAjax: "/bet/queryBetType_getnumStatistic.html?gameEn=ssq&periodNum={0}"
            , maxRedNum: i.maxRedNum || 20
          }
        , quickInit: function () {
            e.each(this.config, function (j, k) {
                if (typeof k == "string") { f.config[j] = k.replace(/\{gameId\}/g, c.config.gameId) }
            });
            e(document).delegate("#login_pswUrl,#login_reg,.goOld,[href*=/ssq/danshi.html]", "mousedown", function () { f.saveData2LS() });
            this.initMainTab().betTimerInit().groupbuyTimerInit().numberStatInit().betAreaInit().betPoolInit().dantuoAreaInit().dingDanShaHaoAreaInit().betButtonInit().buyButtonInit().initBuyTab();
            c.checkGamePause("ssq"); this.betTimer.start(this.betTimer.initTime - c.COMS.PT.Timer.getPageRunTime()); 
            this.bindModule({ "loadPoolFav savePoolFav": { css: "css2/lotteryBet/collect.css", js: "js2/game/fav/poolFav.js"} });
            f.gameAct.is("zhuihaoTip", "iconZhuihaoTip", function (j) { j && e("#zhuihaoTab").prepend('<span class="iconZhuihaoTip"></span>') }); 
            f.gameAct.is("ssq", "3yiJiajiang", function (j) {
                if (j) { e('.titleBox[game_en="ssq"] .gameintr').remove() }
            });
            f.bindModule({ newbieGuideInit: { js: ["js2/guide.js", "js2/activity/ssqNewbieGuide201312/ssqNewbieGuide201312.js"], css: "css2/activity/ssqNewbieGuide201312/ssqNewbieGuide201312.css"} });
            if (e.hash("xinshou").trim() === "true" && !e.cookie("ssqNewbieGuide")) {
                f.newbieGuideInit(this.betArea, this.betPool) 
             } 
             new f.CompareHistory(e(".selected_history a"), "ssq", "双色球", e.proxy(f.betPool.serialize, f.betPool), e.proxy(f.betPool.random, f.betPool))
        }
        , myInit: function () { 
            this.luckyBuyInit(); var n = g.setTimeout(function () { e(".selected_btnbox").append('<i class="randomImgTip">试试手气，随机选一注</i>').find(".randomImgTip").one("click", function () { f.betPool.random(1) }) }, 60000); this.betPool.onChange(function () { g.clearTimeout(n); e(".randomImgTip").remove() }); this.initShotEnter(); var k = f.jfb || {}, m = k.showTip, l = k.rel, j = k.ygtime; if (m == "1") { e(".betBtnBox > .betBtns").prepend("<i class='getJfb'></i>"); e(".betBtnBox > .betBtns").prepend("<a href=" + l + " class='aHref' target='_balank'></a>"); e(".aHref").click(function () { e(".getJfb").hide(); e(".aHref").hide(); e(".betting_Btn").trigger("click") }) } }, initShotEnter: function () { this.betPool.beiNumber.set(+(this.config.quickBetTimes) || 1); this.betPool.periodNumber.set(+(this.config.quickBetPeriods) || 1); this.loadLSData(true); if (this.initData) { this.scrollWhenNeed(".bettingBox") } var k = (this.config.orderType || e.getPara("buyType") || e.getPara("orderType")).replace(/\D/g, ""), j = this.helper.gameType, o = false, l = this.config; e.each(this.initGroupData || {}, function (p, q) { if (q) { o = true } }); k = k != "2" && o ? "3" : k; if ((k == "2" || k == "3") && (j == 2 || j == 3)) { this.helper.setBuyType(+k, true); this.scrollWhenNeed([".ballarea", ".dantuo_com"][j - 2]) } var n = +(e.getPara("random").replace(/\D/g, "") || 0); if (n > 0 && !this.initData) { this.betPool.random(Math.min(n, 99)) } this.activityType = e.getUrlPara("activityType").replace(/\D/g, "") || 24; if (e.getPara("goodnum") == "yes") { var m = i.random("", 1)[0]; this.betArea.push("red", m[0]).push("blue", m[1]) } if (l.followMode) { this.betPool.addactionalBet.setMode(l.followMode, l.stopAwardAmount) } delete this.initShotEnter }, giveFocus: function (j) { if (e(j)[0]) { try { var l, m = jQuery(j)[0], k = jQuery(m).is("input:text,textarea") ? jQuery(m).attr("readonly") ? false : m.createTextRange() ? true : false : false; if (k) { l = m.createTextRange(); l.moveStart("character", m.value.length); l.collapse(true); l.select() } } catch (m) { e(j)[0].focus(); return true } } }, helper: { gameType: 1, buyType: 1 }, initMainTab: function () {
            var n = e(".gameMenu"), m = e("#docBody"), j = e("#mainPanels"), p = e(".navList"), k, o = function () { var q = f.helper.gameType; n.find(".active").removeClass("active"); switch (q) { case 1: case 6: if (q == 1) { m[0].className = "docBody clearfix kuaijie"; n.find("li:first").addClass("active"); j.find(">div").addClass("hide").eq(0).removeClass("hide"); f.betArea.onChange() } else { if (q == 6) { m[0].className = "docBody clearfix luckyBuy"; n.find("li:eq(2)").addClass("active"); j.find(">div").addClass("hide").eq(3).removeClass("hide"); f.luckyBuy && f.luckyBuy.onChange() } } break; case 2: k = f.betArea.onChange(); case 3: k = f.dantuoArea.onChange(); case 4: k = f.dingDanShaHaoArea.onChange(); case 5: m[0].className = q === 4 ? "docBody kill clearfix" : "docBody clearfix putong"; n.find("li:eq(1)").addClass("active"); j.find(">div").addClass("hide").eq(q - 2).removeClass("hide"); p.find(".active").removeClass().end().find("li:eq(" + (q - 2) + ")").addClass("active"); k && k.onChange(); f.helper.syncBuyType(true); break } f.updateBetCountInfo() }, l = function () {
                var q = n.find(".active").attr("rel"); if (q == "2") { q = p.find(".active").attr("rel") || 2 } return f.helper.gameType = +q 
            }; n.delegate("li", "click focusin", function (r) { var q = (e(this).attr("rel") || "").replace(/\D/g, ""); if (q) { if (q == "2") { q = p.find(".active").attr("rel") || 2 } f.helper.setGameType(+q); r.preventDefault() } }); p.delegate("li", "click focusin", function (r) { var q = (e(this).attr("rel") || "").replace(/\D/g, ""); if (q) { f.helper.setGameType(+q); r.preventDefault() } }); this.helper.syncGameType = o; this.helper.readGameType = l; this.helper.setGameType = function (q) { this.gameType = +(q || 1); this.syncGameType() }; l(); delete this.initMainTab; return this }, initBuyTab: function () {
                var m = e("#moreOperateBox"), p = m.find(".tips"), n = e(".moreOperate_con>div"), o = this, j = e(".betresult")
        , l = function (r) {
            var u = o.helper.buyType, 
            q = m.find("dd[rel=" + u + "]"),
            v = q.attr("info"), 
            s = q.attr("pnl");
            //q.find("[name=operate]")[0].checked = true;
            n.addClass("hide");
            s && e("#" + s).removeClass("hide"); 
            p.html(v);
            switch (u) {
                case 1: o.updateBetCountInfo(); break;
                case 2: o.loadHtmlAndInit(s, o.config.zhuihaoAjax, o.mulitiBuyInit, o.updateBetCountInfo); r || o.scrollWhenNeed("#select_list_box"); break;
                case 3: o.loadHtmlAndInit(s, o.config.hemaiAjax, o.groupBuyInit, o.updateBetCountInfo); r || o.scrollWhenNeed("#select_list_box"); break
            }
            j[0].className = "betresult buyType" + u; o.betPool.onChange()
        }
, k = function () {
    var q = m.find("[name=operate]:checked").closest("dd").attr("rel"); return o.helper.buyType = +q
}; 
m.delegate("dd", "click", function (r) {
    var q = (e(this).attr("rel") || "").replace(/\D/g, "");
    if (q) { o.helper.setBuyType(+q) }
});
this.helper.syncBuyType = l; this.helper.readBuyType = k; 
this.helper.setBuyType = function (r, q) {
    this.buyType = +(r || 1); this.syncBuyType(q)
};
l();
delete this.initMainTab;
return this
}
, getNumberHtml: function (j) {
    return "<em>" + c.fillZero(j, 2).split("").join("</em><em>") + "</em>"
}, betTimerInit: function () { var k = e(".betTimer"), o = e(".betTimer2"), l = this, n = function (v, r, p, q, w) { var u = "", t = w ? function (s) { return s } : l.getNumberHtml; if (v == 1 && r == 0) { v = 0; r = 24 } if (r == 1 && p == 0) { r = 0; p = 60 } if (v > 0) { u = t(v) + " 天 " + t(r) + " 时" } else { if (r > 0) { u = t(r) + " 时 " + t(p) + " 分" } else { u = t(p) + " 分 " + t(q) + " 秒" } } return u }; var j = c.createCom("COMS.PT.Timer", { autoSaveKey: "ssqBetTimer2" }).onRunning(function (u, t, p, r) { var q = n(u, t, p, r); if (q != this._ssqBetTimer2HTML) { k.html("本期已截止，距下期开售还剩: " + n(u, t, p, r)); o.html(n(u, t, p, r, true)); this._ssqBetTimer2HTML = q } }).onStop(function () { if (!c.gameStop) { if (j.initTime > 0 || m.initTime > 0) { l.saveData2LS(); g.location.reload(true) } } }); j.initTime = +(k.attr("rel2") || 0); var m = c.createCom("COMS.PT.Timer", { autoSaveKey: "ssqBetTimer" }).onRunning(function (u, t, p, r) { var q = n(u, t, p, r); if (q != this._ssqBetTimerHTML) { k.html("本期投注剩余时间: " + q); this._ssqBetTimerHTML = q } }).onStop(function () { if (!c.gameStop) { e.dialog(); var p = "您好，第 <span class='c_ba2636'>" + c.config.period + "</span>期已截止"; if (c.config.nextPeriod) { p += "，当前是停售时间，下一期开售时间为 " + c.config.nextPeriodSaleStartTime + "，请休息片刻，再来博取大奖。" } h(p); j.start(j.initTime - c.COMS.PT.Timer.getPageRunTime()) } }); m.initTime = +(k.attr("rel") || 0); delete this.betTimerInit; this.betTimer = m; return this }, groupbuyTimerInit: function () { var k = c.createCom("COMS.PT.Timer", { autoSaveKey: "ssqGroupTimer" }).onStop(function () { f.groupBuyStop = 1; var l = f.submitOrderType; if (!c.gameStop && (l == 3 || l == 5.1)) { e.dialog(); h("您好，第 <span class='c_ba2636'>" + c.config.period + "</span> 期合买投注已截止，请采用<br>其他投注方式 ， 其他投注方式截止时间：<span class='c_ba2636'>" + c.config.periodBetSaleEndTime + "</span><br> ，下一期开售时间为 " + c.config.nextPeriodSaleStartTime + "；") } }), j = c.createCom("COMS.PT.Timer", { autoSaveKey: "ssqGroupTimer2" }).onStop(function () { f.groupBuyStop = 2 }); timerWrap = e(".groupbuyTimer"), lib = this; k.initTime = +(timerWrap.attr("rel") || 0); k.start(k.initTime - c.COMS.PT.Timer.getPageRunTime()); j.initTime = +(timerWrap.attr("rel2") || 0); j.start(j.initTime - c.COMS.PT.Timer.getPageRunTime()); this.groupbuyTimer = k; this.groupbuyTimer2 = j; delete this.groupbuyTimerInit; return this }, numberStatInit: function () { var k = {}, m = this, l = function (n) { n.find("[name=statPeriods]").change(function () { j(n, e(this).val()) }); n.find(".showType[value=" + (n[0]._index) + "]").click() }, j = function (p, q) { var s = +q, r = p[0].id; if (k[s]) { p.html(e.format(k[s], { showTypeName: "showTypeFor" + r })); l(p) } else { var o = g.setTimeout(function () { p.html("<div class='statHolder'><span>数据加载中，请稍候...</span></div>") }, 500); m.get(e.format(m.config.numAjax, s), function (t, u) { o && g.clearTimeout(o); var n = t ? "<div class='statHolder statErr'><span>数据加载失败，请稍后重试。</span></div>" : u; p.html(e.format(n, { showTypeName: "showTypeFor" + r })); if (!t) { k[s] = u; l(p) } }, "stat") } }; e("#mainPanels .selectNum .hmtj").each(function () { var p = e(this), o = e("#" + p.attr("cid")), n = 0; o.delegate(".showType", "click", function () { var q = +this.value; o[q == 2 ? "addClass" : "removeClass"]("showHotCold"); o[0]._index = q }).delegate(".redballs a,.redNums a", "click", function () { var q = [+e(this).html()]; if (m.helper.gameType == 3) { f.dantuoArea.push("dan", "red", q) } else { if (m.helper.gameType == 4) { f.dingDanShaHaoArea.dan("red", q) } else { m.betArea.push("red", q) } } }).delegate(".blueballs a,.blueNums a", "click", function () { var q = [+e(this).html()]; if (m.helper.gameType == 3) { f.dantuoArea.push("tuo", "blue", q) } else { if (m.helper.gameType == 4) { f.dingDanShaHaoArea.dan("blue", q) } else { m.betArea.push("blue", q) } } }); l(o); e(this).toggle(function () { e(this).addClass("active"); o.removeClass("hide"); j(o, n || 100) }, function () { e(this).removeClass("active"); o.addClass("hide") }).setControlEffect("down") }); delete this.numberStatInit; return this }, betAreaInit: function () { var k = e(".ballarea"), j = k.find(".selectInfo span"), l = c.createCom("COMS.PT.BetArea", k).onBallChange(function (p, n, r) { var m = f.config.maxRedNum; if (m < 33 && p == "red" && n == 1) { var q = c.unique(this.get("red"), r), o = q.length <= m; if (!o) { h("红球数量不能超过" + m + "！") } return o } }).onChange(function () { var m = this.get("red").length, o = this.get("blue").length, n = c.c(m, 6) * o; j.html(['您当前选中了<strong class="c_ba2636">', m, "</strong>个红球，", '<strong class="c_1e50a2">', o, "</strong>个蓝球，", '共<strong class="c_ba2636">', n, "</strong>注，", '共<strong class="c_ba2636">', n * 2, "</strong>元"].join("")) }); this.initBetAreaCtrl(k, l); delete this.initBetAreaCtrl; delete this.betAreaInit; this.betArea = l; return this }, initBetAreaCtrl: function (j, k) { e.each(["Red", "Blue"], function (m, l) { var n = l.toLowerCase(); j.find("." + n + "BallBox").delegate(".clearing", "click", function () { k.clear(n) }).delegate(".radom_" + n + "btn", "click", function () { k.random(n, +e(this).prev().val()) }).find("select").change(function () { var p = +e(this).val(); k.random(n, p); g.LS.set("ssq" + l + "RandomSel", p) }); var o = [6, 1][m]; j.find("." + n + "BallBox select option[value=" + (g.LS.get("ssq" + l + "RandomSel") || o) + "]")[0].selected = true }) }, dingDanShaHaoAreaInit: function () { var l = e("#mainPanels>.dingdan"), k = l.find(".selectInfo span"), n = c.createCom("COMS.PT.DingDanShaHaoArea", l).onBallChange(function (r, p, u) { var t, s = this, q = true; if (r == "red" && p == 1) { t = c.unique(this.get("red")["dan"], u), q = t.length <= 5; if (!q) { a("您好，红球最多只能定5个胆码。<br/>是否把（" + u[0] + "）做为杀号选择？", ["*确定", "取消"], function (w) { if (w) { s.sha(r, u) } }) } return q } if (p == -1) { var v = { red: { name: "红球", count: 27 }, blue: { name: "蓝球", count: 15}}[r]; t = c.unique(this.get(r)["sha"], u), q = t.length <= v.count; if (!q) { h("您好，" + v.name + "最多可杀" + v.count + "个号码！") } return q } }).onChange(function () { m("red"); m("blue") }); e.each(["Red", "Blue"], function (q, p) { var r = p.toLowerCase(); l.find("select." + r + "_random_count_select").change(o); l.find("select.zu_count_select").change(j) }); function m(w) { var p = l.find("select." + w + "_random_count_select"), t = p.val(), y = t == p.find("option:eq(0)").val(), x = n.get(w), q = [+p.find("option").first().val(), +p.find("option").last().val()], v = w == "red" ? [6, 20] : [1, 16], r = [], s = w == "red" ? 33 : 16; r[0] = v[0] - x.dan.length; r[0] = r[0] < 0 ? 0 : r[0]; r[1] = s - x.sha.length - x.dan.length; r[1] = r[1] > v[1] ? v[1] : r[1]; if (r[0] === q[0] && r[1] === q[1]) { j(); return } var z = []; for (var u = r[0]; u <= r[1]; ++u) { z.push('<option val="' + u + '" ' + (!y && u == t ? 'selected="selected"' : "") + ">" + u + "</option>") } p.html(z.join("")); if (p.val() != t) { o() } j() } function o() { var w = n.get(), y = l.find("select.red_random_count_select"), x = l.find("select.blue_random_count_select"), r = l.find("select.zu_count_select"), v = r.val(), t = 33 - w[0].dan.length - w[0].sha.length, p = 16 - w[1].dan.length - w[1].sha.length, s = (c.c(t, y.val()) || 1) * (c.c(p, x.val()) || 1), u = [1, 2, 5, 10, 20, 50, 100], q = []; e.each(u, function (z, A) { if (A <= s) { q.push(A) } }); r.html(e.map(q, function (z) { return '<option value="' + z + '" ' + (z == v ? 'selected="selected"' : "") + ">" + z + "</option>" }).join("")); j() } function j() { var w = n.get(), s = l.find("select.red_random_count_select"), p = l.find("select.blue_random_count_select"), q = l.find("select.zu_count_select"), r = w[0].dan.length + (+s.val()), u = w[1].dan.length + (+p.val()), t, v; if (w[0].dan.length > 0 && r > 6) { t = c.c((+s.val()), 6 - w[0].dan.length) * u * q.val() } else { t = c.c(r, 6) * u * q.val() } k.html(["您当前选中了", '<strong class="c_ba2636">', t, "</strong>注，", '共<strong class="c_ba2636">', t * 2, "</strong>元"].join("")) } delete this.dingDanShaHaoAreaInit; this.dingDanShaHaoArea = n; return this }, dantuoAreaInit: function () { var k = e("#mainPanels>.dantuo"), j = k.find(".selectInfo span"), l = c.createCom("COMS.PT.DantuoArea", k).onBallChange(function (r, o, m, q) { if (r == "dan" && o == "red" && m == 1) { var p = c.unique(this.get("dan", "red"), q), n = p.length <= 5; if (!n) { h("最多可选择5个红球胆码！") } return n } }).onChange(function () { var q = this.get(), m = q[0].length, r = q[2].length, n = m + r, p = q[3].length, o = f.poolConfig.counter(q)[0]; j.html(['您选择了<strong class="c_ba2636">', n, "</strong>个红球", "(", m, "个胆码，", r, "个拖码)，", '<strong class="c_1e50a2">', p, "</strong>个蓝球，", '共<strong class="c_ba2636">', o, "</strong>注，", '共<strong class="c_ba2636">', o * 2, "</strong>元"].join("")) }); delete this.dantuoAreaInit; this.dantuoArea = l; return this }, luckyBuyInit: function () { c.createCom("ssq.luckyBuy", { wrap: ".luckbuyBox", currentPeriod: c.config.period }, function (j) { var l = e(".addbtnBox"), k = l.find(".addbtn"); j.onChange(function (n) { if (n != "num" && f.helper.gameType == 6) { var m = this.getKey(); k[m.key ? "removeClass" : "addClass"]("disabled") } }); f.luckyBuy = j; j.onChange() }); delete this.luckyBuyInit; return this }, betButtonInit: function () { var k = e(".addbtnBox"), j = k.find(".addbtn"); j.click(this.betButtonClick); this.betArea.onChange(function () { if (f.helper.gameType <= 2) { var l = this.get("red").length, m = this.get("blue").length; j[(l >= 6 && m > 0) ? "removeClass" : "addClass"]("disabled").attr("title", "确认选号") } }); this.dantuoArea.onChange(function () { if (f.helper.gameType == 3) { var o = this.get(), l = o[0].length, p = o[2].length, m = l + p, n = o[3].length; j[(m > 6 && l > 0 && l < 6 && p > 1 && n > 0) ? "removeClass" : "addClass"]("disabled").attr("title", "确认选号") } }); if (f.helper.gameType == 4) { j.removeClass("disabled").attr("title", "开始机选") } this.dingDanShaHaoArea.onChange(function () { if (f.helper.gameType == 4) { j.removeClass("disabled").attr("title", "开始机选") } }); this.syncBetButton = function () { var p = f.betPool, m = f.helper.gameType, o = p.editType, n = p.inEditDataGuid, l = false; switch (o) { case "normal": if (m < 3 && n) { l = true } break; case "dantuo": if (m == 3 && n) { l = true } break } k[l ? "addClass" : "removeClass"]("revisebtnBox").attr("title", f.helper.gameType == 4 ? "开始机选" : "确认选号") }; delete this.betButtonInit; return this }, betButtonClick: function () { var z = f.helper.readGameType(), m, s = f.betPool, v = f.betArea, o, j, n = "", D, A, G, x, B, F = f.config.maxRedNum, r = function (H, l) { return { red: "<span class='c_ba2636'>" + l + "</span>", blue: "<span class='c_1e50a2'>" + l + "</span>"}[H] || l }; switch (z) { case 1: case 2: m = v.onChange(); o = m.get("red"), j = m.get("blue"); D = o.length, A = j.length; if (D >= 6 && D <= F && A >= 1) { if (s.inEditDataGuid && s.editType == "normal") { s.edit(s.inEditDataGuid, [o, j]); delete s.inEditDataGuid } else { s.push([o, j]) } m.clear(); f.scrollWhenNeed(".bettingBox"); return } if (D == 0 && A == 0) { a("至少选择1注号码才能投注，是否机选1注碰碰运气？", ["*机选1注", "取消"], function (l) { if (l) { s.random(1) } }); return } n = D > F ? "至多能选择" + F + "个红球<br/>" : ""; if (D < 6 || A == 0) { n += "您选了（" + r("red", D) + "红 + " + r("blue", A) + "蓝），请至少再选择 " + (D < 6 ? r("blue", 6 - D) + " 个红球 " : "") + (A == 0 ? r("blue", 1) + " 个蓝球" : ""); a(n, ["*机选补全", "手工选号"], function (l) { if (l) { D < 6 && v.random("red", 6); A == 0 && v.random("blue", 1); f.betButtonClick() } }); return } break; case 3: m = f.dantuoArea.onChange(); G = m.get(), x = G[0].length, B = G[2].length, D = x + B, A = G[3].length; if (D > 6 && x > 0 && x < 6 && B > 1 && A > 0) { if (s.inEditDataGuid && s.editType == "dantuo") { s.edit(s.inEditDataGuid, G); delete s.inEditDataGuid } else { s.push(G) } m.clear(); f.scrollWhenNeed(".bettingBox"); return } if (D == 0 && A == 0) { a("至少选择1注号码才能投注，是否机选1注碰碰运气？", ["*机选一注", "取消"], function (l) { l && f.betPool.random(1) }); return } n = x == 0 ? "请至少选择一个红球胆码" : x > 5 ? "至多能选择5个红球胆码" : B < 2 ? "请至少选择2个红球拖码" : D < 7 ? "红球胆码 + 红球拖码至少需要7个" : A == 0 ? "请至少选择一个篮球" : ""; break; case 6: if (f.luckyBuy) { G = f.luckyBuy.getData(); if (G.data.length) { s.push2(G.data); f.scrollWhenNeed(".bettingBox") } else { n = G.err } } break; case 4: m = f.dingDanShaHaoArea.onChange(); var p = e("#mainPanels>.dingdan"), t = p.find("select.red_random_count_select"), u = p.find("select.blue_random_count_select"), q = p.find("select.zu_count_select"), k = +t.val(), C = +u.val(), E = []; for (var y = 0, w = +q.val(); y < w; ++y) { G = m.random(k, C); E.push(G) } s.push.apply(s, E); m.clear(); f.scrollWhenNeed(".bettingBox"); return; break } n && h(n) }, initAddactionalBet: function () {
    var l = e(".additionalTip"), m = l.find('[name="stopWhenFeed"]'), j = l.find('[name="stopMoney"]'), k = this.betPool, n = k.periodNumber; m.attr("checked", false); j.val(5000); n.onChange(function () { var o = this.get() || 1; o > 1 ? l.addClass("additionalTipShow").removeClass("additionalTipHide") : l.addClass("additionalTipHide").removeClass("additionalTipShow") }); m.click(function () { e(this).parent().find("span")[this.checked ? "show" : "hide"]() }); f.loadCdnJS("js2/liveCheck.js", function () { return !!e.fn.bindLiveCheck }, function () {
        //j.bindNumberLiveCheck()
    });
    k.addactionalBet = { getStopMoney: function () { return +j.val() || 0 }, setStopMoney: function (o) { o = +o; o && j.val(o) }, isStopWhenFeed: function () { return m.attr("checked") }, setMode: function (p, o) { switch (parseInt(p)) { case 1: case 2: m.click(); if (p == 2) { this.setStopMoney(o || 0) } break } } }; return this
}
, betPoolInit: function () {
    var j = e(".betresult"), m = j.find(".betNumCount"), k = j.find(".betMoneyCount"),
    l = c.createCom("COMS.PT.BetPool", this.poolConfig).onDelete(function (o) {
        if (o.guid == this.inEditDataGuid) {
            var n = o.guid; a("该条投注数据正在编辑！确定删除吗？", function (p) {
                if (p) { delete l.inEditDataGuid; l.del(n) }
            }); return false
        }
    }).onEdit(function (n) {
        if (n.guid == this.inEditDataGuid) {
            delete this.inEditDataGuid
        }
    }).onChange(function () {
        f.updateBetCountInfo(this)
    }).onRandom(function () {
        f.scrollWhenNeed(".bettingBox")
    });
    l.resultDom = { wrap: j, betNum: m, betMoney: k };
    this.initPoolCtrl(l);
        l.beiNumber = c.createCom("COMS.PT.iNumber", { wrap: j.find(".beiNumWrap"), min: 1, max: 99999
    }).onChange(function () { f.updateBetCountInfo(l) });
        l.periodNumber = c.createCom("COMS.PT.iNumber", { wrap: j.find(".periodNumWrap"), min: 1, max: 154
        }).onChange(function () { f.updateBetCountInfo(l); f.groupBuy && f.groupBuy.setFollowPeriods(this.get()) });
        delete this.betPoolInit;
        delete this.initPoolCtrl;
        this.betPool = l;
        this.initAddactionalBet(); return this
    }
    , poolConfig: e.extend({}, i, { edit: function (k, l) {
        var j = f.helper.gameType;
        if (l.length == 2) {
            if (j > 2) {
                f.helper.setGameType(2)
            }
            f.betArea.set(l);
            f.betPool.editType = "normal"
        }
        else {
            f.helper.setGameType(3); f.dantuoArea.set(l); f.betPool.editType = "dantuo"
        }
        f.betPool.inEditDataGuid = k;
        f.scrollWhenNeed(".bettingBox", true);
        f.syncBetButton();
        e.sendMsg("startEdit")
    }
})
        , initPoolCtrl: function (l) {
            var j = ".randomBtn label input", k = ".randomBtn label span";
            e("#select_list_box").next(".selected_btnbox").delegate(".selected_btn", "click", function () {
                var m = e(this).attr("num"), o;
                switch (m) {
                    case "u": o = +(e(this).parent().find("input").val().replace(/\D/g, "") || 5); l.random(o);
                        break;
                    case "x": o = l.getData().length; if (o > 0) {
                            a("您确定删除所有选号？", function (n) {
                                if (n) {
                                    l.clear();
                                    if (f.betPool.inEditDataGuid) { delete f.betPool.inEditDataGuid } f.syncBetButton()
                                } 
                            })
                        }
                        break;
                    default: o = +m.replace(/\D/g, ""); l.random(o)
                }
            }).find(j).focus(function () {
                e(this).parent().find("span").hide();
                this.value = this.value.replace(/\D/g, "");
                f.giveFocus(this)
            }).blur(function () {
                var m = this.value.replace(/\D/g, "");
                if (!m) { e(this).parent().find("span").show() } else {
                    this.value = m + "注"
                } 
            }).keyup(function () {
                var m = this.value.replace(/\D/g, ""), o;
                if (m != this.value) { this.value = m }
                if (m) {
                    o = +m < 1 ? 1 : +m > 99 ? 99 : +m;
                    if (o + "" != this.value) { this.value = o } 
                }
            }).end().delegate(k, "click", function () {
                e(this).parent().find("input").focus()
            }).delegate(".storeUpBtn", "click", function () {
                var m = i.serialize(f.betPool.getData()).replace(/t[^\,]+/g, "");
                f.savePoolFav(c.config.gameId, m, f.numberHelper, "双色球")
            }).delegate(".exportBtn", "click", function () {
                f.loadPoolFav(c.config.gameId, f.numberHelper, function (m) { f.betPool.push2(i.parse(m)) })
            });
            e(j)[0].value = ""
        }, numberHelper: function (l) {
            var k = i.parse(l), j = [0, 0];
            e.each(k, function (m, o) { var n = i.counter(o); j[0] += n[0]; j[1] += n[1] }); return j
        }
        , updateBetCountInfo: function (q) {
            var m = q || this.betPool, l = m.resultDom, j = m.getCount(), n, p, k, r = this.helper.gameType, o = this.helper.buyType;
            switch (r) {
                case 1:
                case 6: n = m.beiNumber.get(); p = m.periodNumber.get(); break;
                case 2: case 3: case 4:
                case 5: switch (o) {
                        case 1: n = m.beiNumber.get();
                            p = m.periodNumber.get(); 
                            break;
                        case 2:
                            n = 1;
                            p = 1; 
                            break;
                        case 3:
                            n = m.beiNumber.get();
                            p = m.periodNumber.get();
                            this.groupBuy && this.groupBuy.setFollowPeriods(m.periodNumber.get()); break
                    }
            }
            l.betNum.html(j[0]);
            l.betMoney.html(c.getMoneyText(j[1] * n * p));
            this.syncBetButton()
        }
            , loadLSData: function (k) {
                var j = this.initData || g.LS.get("ssqPoolCache");
                if (j) { this.betPool.push2(this.poolConfig.parse(j)) }
                if (k) { g.LS.remove("ssqPoolCache") }
            }
            , saveData2LS: function () { g.LS.set("ssqPoolCache", this.betPool.serialize()) }
            , loadHtmlAndInit: function (j, m, l, o) {
                var k = e("#" + j), n = this; if (!k[0]) { return this }
                if (k[0].loaded) { e.isFunction(o) && o.call(n); return this } this.get(m, function (q, r) {
                    if (k[0]) {
                        var p = q ? "<div class='loadingData'>数据加载失败，请稍候再试。</div>" : r; if (!q) { k[0].loaded = true } k.html(p);
                        if (e.isFunction(l)) { l.call(n, o) } else { e.isFunction(o) && o.call(n) } 
                    } 
                }, j); return this
            }
            , mulitiBuyInit: function (n) {
                var k = e("#zhuihaoWrap"), j = this.config, m = this, l; if (!k[0] || !k[0].loaded) { return this }
                j.followPeriods = j.followPeriods.replace(/\D/g, "").replace(/^0*/, ""); 
            if (j.followBetTimes) { l = j.followBetTimes.split(","); j.followPeriods = l.length } c.createCom("COMS.PT.MultiBuy", { wrap: k, beiNum: this.betPool.beiNumber.get(), chooseNum: j.followPeriods || 20, gameId: c.config.gameId, gameCn: "双色球", stopWhenFeed: !!{ 1: 1, 2: 1}[j.followMode], stopMoney: { 2: 1}[j.followMode] ? j.stopAwardAmount : d }, function (o) { var p = f; p.betPool.onChange(function () { if (p.helper.buyType == 2) { o.setBaseMoney(p.betPool.getCount()[1]) } }); p.betTimer.onStop(function () { o.setCurPeriod(true) }); if (l) { o.setBetTimes(l) } p.mulitiBuy = o; p.betPool.onChange(); n.call(p) }); delete this.mulitiBuyInit; return this }, groupBuyInit: function (k) { var j = e("#groupbuyWrap"); if (!j[0] || !j[0].loaded) { return this } c.createCom("COMS.PT.GroupBuy", j, function (l) { var m = f; m.betPool.onChange(function () { if (m.helper.buyType == 3) { l.setBaseMoney(this.getCount()[1] * m.betPool.beiNumber.get()) } }); m.betPool.beiNumber.onChange(function () { if (m.helper.buyType == 3) { l.setBaseMoney(m.betPool.getCount()[1] * this.get()) } }); m.betTimer.onStop(function () { }); m.groupBuy = l; m.betPool.onChange(); e.each(m.initGroupData || {}, function (n, o) { l.set(n, o) }); k.call(m) }); delete this.groupBuyInit; return this }, buyButtonInit: function () { var k = e("#normalBtnBox"), n = e("#groupbuyEndBtnBox"), l = e("#waitBtnBox"), j = e(".betBtns>span"), m = function (o) { var p = { 1: k, 2: n, 3: l}[o]; if (p) { j.addClass("hide"); p.removeClass("hide") } }; k.find(".betting_Btn").click(this.buyButtonClick); this.betTimer.onStop(function () { m(3) }); this.changeBtnBox = m; delete this.buyButtonInit; return this }, buyButtonClick: function () { if (c.gameStop) { return } var s = f, t = s.helper.buyType, w = s.helper.gameType, u = t, p, o, v, y, l, n, r, q = false, j, x = { gameId: c.config.gameId, activityType: s.activityType, stakeNumber: s.betPool.serialize() }; if (!e("#agree_rule").is(":checked")) { h("请先阅读并同意《委托投注规则》后才能继续"); return } j = function (A, z, B) { var D = A || 1, C = z || "至少选择" + D + "注号码才能投注，是否机选" + D + "注碰碰运气？"; a(C, B || ["*机选1注", "取消"], function (E) { E && s.betPool.random(D, function () { s.buyButtonClick() }) }) }; if (!x.stakeNumber) { if (!e(".addbtnBox .addbtn").hasClass("disabled")) { e(".addbtnBox .addbtn").click(); x.stakeNumber = s.betPool.serialize() } else { q = true } } t = u = (w == 1 || w == 6) ? 1 : u; t = t == 2 && !s.mulitiBuy ? 1 : t == 3 && !s.groupBuy ? 1 : t; switch (t) { case 1: if (q) { j(); return } x.betTimes = s.betPool.beiNumber.get(); n = s.betPool.periodNumber.get(); if (n > 1) { x.followBuyPeriods = n; x.betWay = "ZHUIHAO"; x.followName = "双色球 追号" + n + "期"; u = 2.1 } o = s.betPool.addactionalBet; x.followMode = !o.isStopWhenFeed() ? 0 : o.getStopMoney() > 0 ? 2 : 1; if (x.followMode == 2) { x.stopAwardAmount = o.getStopMoney() } break; case 2: if (q) { j(); return } p = s.mulitiBuy.getData(), o = s.mulitiBuy.getInfo(), r = 0, l = []; e.each(p, function (z, A) { r++; x[z + "_times"] = A; l.push(z) }); if (r == 0) { h("追号金额不能为空！"); return } y = s.mulitiBuy.getCurPeriod(); if (r == 1 && p[y]) { delete x[y + "_times"]; x.betTimes = p[y]; u = 1 } else { x.selectPeriods = l.join(","); x.followMode = !o.stopWhenFeed ? 0 : o.stopMoney > 0 ? 2 : 1; if (x.followMode == 2) { x.stopAwardAmount = o.stopMoney } x.followType = 1; x.betWay = "ZHUIHAO"; x.followName = "双色球 追号" + l.length + "期"; v = s.mulitiBuy.getSkipPeriod(); if (v.skiped > 0) { a("您的追号中，没有选择<br/>" + v.join(",") + "<br/>期次，真的要这样做么？", function (z) { z && f.payOrder(u, x) }); return } } break; case 3: if (f.groupBuyStop == 1) { h("当前期次的合买已经截止，请稍后再试。"); return } x.betTimes = f.betPool.beiNumber.get(); var k = s.betPool.getCount()[1] * x.betTimes; if (k < 8) { var m = Math.ceil((8 - k) / x.betTimes / 2); j(m, "发起合买的投注金额不能少于8元，是否机选" + m + "注碰碰运气？", ["*机选" + m + "注", "取消"]); return } o = s.groupBuy.selfCheck(); if (o !== true) { return } p = s.groupBuy.getData(); x.caseTitle = p.title; x.caseDesc = p.desc; x.totalPieces = p.totalPieces; x.createrBuyPieces = p.createrBuyPieces; x.proportion = p.feeType; x.secretLevel = p.secretLevel; x.guarantee = p.baodi; if (p.followPeriods > 1) { x.followBuyPeriods = p.followPeriods; u = 5.1 } break } f.payOrder(u, x) }, payOrder: function (k, l) {
    var j = l; if (l.selectPeriods) {
        j = []; e.each(l.selectPeriods.split(","), function (n, m) { j.push("selectPeriods=" + m) });
        e.each(l, function (m, n) {
            if (m !== "selectPeriods") { j.push(m + "=" + n) }
        });
        j = "@" + j.join("&")
    } 
    f.submitOrderType = +k;
    this.loadJS(this.cdnUrl + "/js2/pay/pay.js", function () {
        return !!f.pay
    }
    , function () {
        f.pay.toPay({ data: j, orderType: k, payCallBack: e.noop 
     }) 
})
} 
})
})(window,jQuery,Core,Game,easyNav);