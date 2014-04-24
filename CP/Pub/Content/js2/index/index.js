(function (c, f, d, b, e) {
    f.navConfig.loginUrl = "javascript:easyNav.login2()";
    var a; 
    d.extend(f, { pageId4Ad: 1
        , loadIndexAD: function () {
            var h = "/Main/headFigure.html"
            , j = d("#cpIndexAdBox")
            , n = j.find(".loadingData").removeClass("hide")
            , i = j.find(".loadedErr").addClass("hide")
            , k = j.find(".adList").addClass("hide")
            , m = j.find(".ctrol")
            , g = "[{img:'" + f.cdnUrl + "/img/index/headFigureDefault.jpg',title:'用红包，购彩更省钱',url:'http://caipiao'}]"; 
            var l = function (s) {
//                n.addClass("hide");
//                i.addClass("hide"); 
//                k.removeClass("hide");
//                var r = f.parseJSON(s), q = [], u = r.length, p = 0, t, o = parseInt(+f.serverTime() / 60000);
//                d.each(r, function (v, w) {
//                    w.img = b.$.addUrlPara(w.img, o); q[v] = "<li><a href='" + w.url + "' ii='" + (v + 1) + "' target='_blank'><img src='" + w.img + "' alt='" + w.title + "' title='" + w.title + "'/></a></li>"
//                });
//                k[0].innerHTML = q.join(""); 
//                q = [];
//                for (; p < u; p++) {
//                    t = r[p]; q[p] = "<a " + (p == 0 ? "class='active'" : "") + " href='" + t.url + "' title='" + t.title + "' ii='" + (p + 1) + "' target='_blank'>" + (p + 1) + "</a>"
//                }
//                m[r.length < 2 ? "hide" : "show"]().html(q.join("")); 
//                r.length > 1 && f.loadCdnJS("js2/index/scrollImg.js", function () {
//                      return !!d.scrollImg
//                }, function () { d.scrollImg({ panlWrap: k, ctrolWrap: m }) })
//              };
//              this.isStatic ? l(g) : this.shareGet("indexAD", 60 * 1000, h, function (o, p) {
//                  if (o) { p = g } l(p) 
//              }); 
//              j.delegate(".ctrol a,.adList a", "click", function () {
//                  var q = this.href, r = this.title, p = d(this).attr("ii"), o = "http://caipiao" + p + "/"; if (!/\/help\/special\/link/i.test(q)) { f.virualViewStat(o, r) } 
//              })
          }
          , quickInit: function () {
              d("#topInfoTab").bindTab(); d("#awardListTab").bindTab(d.noop, "mouseenter", "dd[rel]");
              d("#ballGamesTab").bindTab(); d("#ranksTab").bindTab(d.noop, "mouseenter", "dd"); d("#szcaiTab").bindTab(d.noop, "mouseenter", "span"); d("#qiucaiTab").bindTab(d.noop, "mouseenter", "span");
              d("#quickBetJJTab").bindTab(); d("#quickBetGPTab").bindTab(); this.isStatic || this.phoneVisitAlert();
              if (d.isIE678) { d("#lotteryList")[0].className = "" }
              if (d.isIE6) {
                  var h = f.serverTime(), g = h.getFullYear() + "A" + h.getMonth() + "A" + h.getDate();
                  if (c.LS.get("ie6update") != g) { d("#topNav").after("<div id='ie6update'><div>温馨提示：您的浏览器版本过旧，打开网页速度缓慢。为了提高您的使用体验，建议您 <a href='http://windows.microsoft.com/zh-CN/internet-explorer/products/ie/home' target='_blank'>升级IE浏览器</a>。<a href='javascript:;' class='closeIE6update' title='今天不再提示'>x</a></div></div>"); d("#ie6update a").click(function () { c.LS.set("ie6update", g); d("#ie6update").remove() }) } 
              } f.gameAct.is("ssq", "3yiJiajiang", function (i) { if (i) { d('.intr_foot[game_en="ssq"] strong:eq(1)').html("1500万") } })
          }, myInit: function () {
              this.saveFromInfo(); this.isStatic || c.setTimeout(function () {
                  var g = "http://widget.";
                  d("#weiboFrame").attr("src", g)
              }, 2000); 
              if (d.getHashPara("addFav") == "1") { d.addFav("#", "XX彩票-国家正规发行 福利彩票 体育彩票") }
              if (d.getUrlPara("activity") == "moblieMail") {
//                  this.bindModule({ activeMoblieMail: { js: "js2/activity/mail20130723/index.js"} });
//                  this.activeMoblieMail(d.getUrlPara("user"))
              }
              this.initAwardBox(); 
              d(".latestWin_con").scrollGrid({ interval: 3000 });
              c.setTimeout(this.accruingAmountsAnimate, 500);
              this.initGroupbuy(); 
              c.console && c.console.info && this.loadCdnJS("js2/popularize/recruit.js")
          }
          , uaFn: function () {
              a = (function () {
                  var g = navigator.userAgent.toLowerCase(); if (g.indexOf("iphone") >= 0 || g.indexOf("ipad") >= 0 || g.indexOf("ipod") >= 0) {
                      return "ios"
                  } else { if (g.indexOf("android") >= 0 || g.indexOf("adr ") >= 0) { return "android" } else { return "other" } } 
              })()
          }
          , iosFun: function (g) {
              d(g).click(function (i) {
                  var h = +new Date; try { c.location.href = "ntescaipiao://" } catch (i) { } c.setTimeout(function () {
                      if (+new Date - h < 2000) {
                          c.location.href = "http://itunes"
                      } 
                  }, 500); i.preventDefault()
              })
          }
          , phoneVisitAlert: function () {
              f.uaFn(); var g = function (j) {
                  return htmlCode = '<div class="downClient2" style="width:100%;padding:0;border-bottom:1px solid #D1D1D1;"><div class="downClient2"><span class="closeBtn"><img src="' + f.cdnUrl + '/img/index/close_btn.png" /></span><div class="leftMod2"><img src="' + f.cdnUrl + '/img/index/wapLogo.png" /></div><div class="rightBtn2"><a href="javascript:;" id="downBtn2">' + j + "</a> </div></div></div>"
              }, i = '<style id = "cssId">.downClient2{width:990px;margin:0 auto;	background: none repeat scroll 0 0 #F0F0F0;overflow: hidden;padding:45px 0; background:-webkit-gradient(linear,0 0,0 104%,from(#fff),to(#e5e5e5));background:-moz-linear-gradient(top, #fff, #e5e5e5 104%);-pie-background:linear-gradient(top, #fff, #e5e5e5 104%);position:relative;}.leftMod2 {float: left;padding: 0 0 0 88px;}.rightBtn2{padding:15px 41px 0 0;float:right;}#downBtn2{width:302px;text-align:center;border-radius:6px;font:52px/94px "Microsoft YaHei",Simhei;cursor:pointer;border:1px solid #dadada;border-bottom:1px solid #b7b7b7;background:-webkit-gradient(linear,0 0,0 104%,from(#fff),to(#d8d8d8));background:-moz-linear-gradient(top, #fff, #d8d8d8 104%);-pie-background:linear-gradient(top, #0, #d8d8d8 104%);-webkit-box-shadow: 0px 5px 5px -5px #fff;-moz-box-shadow: 0px 5px 5px -5px #fff;box-shadow: 0px 5px 5px -5px #fff;display:block;}#downBtn2, a#downBtn2:link, a#downBtn2:visited, a#downBtn2:hover{ color:#5f5f5f; text-decoration:none;}.closeBtn {position: absolute; top: 15px;left:18px;z-index: 1;cursor:pointer;}</style>', h = function () {
                  d(".closeBtn").click(function () {
                      d(".downClient2").remove(); d("#cssId").remove() 
                  })
              };
              if (a == "other") {
                  return
              } if (a == "android") { d("head").prepend(i); d(document.body).prepend(g("立即下载")); d("#downBtn2").attr("href", "/m/clientDownload.jsp?channel=wap"); h(); return } if (a == "ios") {
                  d("head").prepend(i); d(document.body).prepend(g("查看")); f.iosFun("#downBtn2");
                  h()
              }
          }
          , saveFromInfo: function () {
              var g = d.getUrlPara("source"); if (g) { d.cookie("caipiao_from", g, { path: "/" }) } return this
          }
, adLoader: function (i, g, h, j) {
    f.shareGet(h, 3 * 60 * 1000, g, function (n, m) {
        if (n && j) {
            var k = '<a href="{url}" target="_blank"><img src="{img}" alt="" title="{title}" /></a>';
            var l = d.format(k, d.extend({ title: "", img: "", url: "" }, j));
            i.html(l); 
            return
        } else {
            if (n) { i.hide(); return }
        } 
        m && i.html(d("<div>" + m + "</div>").find("a"))
    })
}
, adLoader1: function (i, g, h) {
    f.shareGet(h, 3 * 60 * 1000, g, function (l, m) {
        var k, j; try { k = f.parseJSON(m) } catch (n) { return }
        if (!k || !k.img) {
            return
        }
        j = '<a href="{url}" target="_blank"><img src="{img}" alt="" title="{title}" /></a>', html = d.format(j, d.extend({ title: "", img: "", url: "" }, k)); i.html(html)
    })
}
, accruingAmountsAnimate: function () {
    var h = d(".accruingAmounts em[data-number]"), j = 15, i = 0, k; function g() {
        if (j--) {
            h.each(function (l, m) { m.innerHTML = Math.floor(Math.random() * 10) })
        } else { c.clearInterval(k); h.each(function (l, m) { m.innerHTML = d(m).attr("data-number") }) } 
    } k = c.setInterval(g, 20)
}, initGroupbuy: function () {
    d("#groupbuyTab").bindTab(function (j) {
        var k = { 
        groupbuyRecom: "/groupbuy/recommend.html"
        , groupbuySSQ: "/groupbuy/ssq/"
        , groupbuy3D: "/groupbuy/x3d/"
        , groupbuyDLT: "/groupbuy/dlt/"
        , groupbuyDC: "/groupbuy/dczq/"
        , groupbuySPF: "/groupbuy/jczq/"
        , groupbuySFC: "/groupbuy/sfc/"
        , groupbuyRX9: "/groupbuy/rx9/"
    };
   d("#moreSchemeLnk").attr("href", k[j.id] || k.groupbuyRecom)
});
this.groupBuy();
var i = d('<span class="providerTip"></span>').appendTo(document.body).hide(); d("#groupBuy").delegate(".order_icon,.provider_link", "mouseenter", function () {
    var m = d(this).offset(), k = d(this), j = [k.outerWidth(), k.outerHeight()], l = k.attr("tip"); if (l) { i.hide().html(l).css({ left: m.left + j[0], top: m.top + j[1] }).show() }
}).delegate(".order_icon,.provider_link", "mouseleave", function () { i.hide() }); var g = d("#groupBuy .linkBox strong"), h = d("#groupBuy .hmCzTip"); g.bind({
    mouseenter: function () { h.show() }, mouseleave: function (k) { var j = k.relatedTarget || k.toElement; if (j !== this && !d.contains(this, j) && j !== h[0] && !d.contains(h[0], j)) { h.hide() } } 
}); h.bind({ mouseleave: function (k) { var j = k.relatedTarget; if (j !== this && !d.contains(this, j) && j !== g[0] && !d.contains(g[0], j)) { h.hide() } } })
}, quickInit: function () { if (f.indexAdConfig) { f.bindModule({ adInit: { js: "js2/index/homePop.js", css: "css2/index/homePop.css"} }); f.adInit(f.indexAdConfig) } } 
}); f.initAwardBox = function () {
    var r = d.format, s = d("#highFrequenceAward"), x = d("#tplHighFrequenceAward").html(), j = "http://caipiao", n = []; f.loadCdnJS("js2/timer.js", function () { if (d("#highFrequenceAward").length) { q() } }); function m(y, z) { return new v(y, z) } function v(y, z) { this._init(y, z) } v.prototype = { _init: function (y, z) {
        z = d.extend({ loop: d.noop }, z); this.option = z; this.dom = d(y); this.ul = this.dom.find(">ul"); this.lis = this.ul.find(">li"); this.isMouseOn = false; this.scrolled = 0; this._binds()
    }
    , _binds: function () { var y = this; this.dom.mouseenter(function () { y.isMouseOn = true }); this.dom.mouseleave(function () { y.isMouseOn = false }) }
    , _scrollTop: function (D, C) { var D = D || 1; var y = 0; var B = this.ul; var z = this.lis; var C = C || d.noop; for (var A = 0; A < D; A++) { y += z.eq(A).outerHeight() } B.animate({ "margin-top": "-=" + y + "px" }, C) }, scrollTop: function (z) {
        var y = this; var z = z || 1; this._scrollTop(z, function () { y.ul.find("li").slice(0, z).appendTo(y.ul); y.ul.css("margin-top", 0); y.scrolled += z; if (y.scrolled >= y.lis.length) { y.scrolled = 0; y.option.loop.call(y) } })
     }
, autoScroll: function () { var y = this; if (this.auto) { clearTimeout(this.auto) } if (this.status == "stop") { return this } this.auto = setTimeout(function () { if (!y.isMouseOn) { y.scrollTop() } y.autoScroll() }, 3000); return this }, stopAuto: function () { if (this.auto) { clearTimeout(this.auto) } this.status = "stop" }
}; 
function k() {
    d.each(n, function () { this.stop() }); n = []; q()
} 
function q() {
    l(function () {
        f.leastTime = 0;
        if (d("#highFrequenceAward li").length <= 1) {
        } else {
            f.mar = m(s, { loop: function () { }
            }).autoScroll()
        } t()
    })
}
function t() {
    var y = []; s.find(".js-time").each(function (A, z) {
        var B = d(this); y[A] = B; var C = d.timer(Math.round(Number(B.attr("data-timer")) / 1000), { progress: function (D, E) {
            y[A].html(E);
            if (D <= f.leastTime) { f.mar && f.mar.stopAuto(); k() } 
        } 
    }); n.push(C)
})
} 
function l(y) {
    f.get(j, function (A, D) {
        var z, B; if (!A) {
            try { B = d.parseJSON(D) } catch (C) { o() }
            if (B.awardWarn.isHF) { g(s, x, B.awardWarn.HFData); if (y) { y() } } else { o() } 
        } else { o() } 
    })
} 
function o() {
    h(); s.hide(); d("#promotion .js-other").show()
}
function h() { s.html(" "); s.unbind() } function g(C, y, A) {
    C.html(" ");
    var B = { jxd11: "http://caipiao", d11: "http://caipiao", gdd11: "http://caipiao" };
    var z = d("<ul></ul>"); d.each(A, function (F, E) {
        var H = ""; var G = u(E.secsLeft); var D = d('<li class="clearfix">' + r(y, E) + "</li>"); d.each(E.number.split(/[\s\|]/),
function () { H += '<em class="smallRedball">' + this + "</em>" }); H = d(H); D.find(".js-balls").append(H); D.find(".js-time").html(G); D.find(".awardBox_Btn").attr("href", w(E));
        D.find("span[data-avgmiss]").html(function () { var I = d(this).attr("data-avgmiss"); I = Number(I) || 0; return Math.round(I * 10) / 10 }); D.find(".gameEn").parent().attr("href", B[E.gameEn]);
        D.find(".js-playType").attr("href", B[E.gameEn] + "#betType=" + i(E.ruleDesc)); D.appendTo(z)
    }); C.append(z)
}
function w(B) {
    var z = B.beturl; var y = B.number + d.format("[{0}]", [A()]); var C = d.format("#betType={0}", i([B.ruleDesc])); return encodeURI(z + "?stakeNumber=" + y + "&activityType=28") + C;
    function A() { return B.playType.replace("组选", "") } 
}
function i(z) {
    var y = { REN2: "rx2", REN3: "rx3", REN4: "rx4", REN5: "rx5", REN6: "rx6", REN7: "rx7", REN8: "rx8", QIAN1: "q1", QIAN2_ZHI: "q2", QIAN2_ZU: "q2_zu", QIAN3_ZHI: "q3", QIAN3_ZU: "q3_zu" };
    return y[z]
} function u(z) { var A = z / 1000; var y = Math.floor(A / 60); A = Math.floor(A % 60); return p(y) + ":" + p(A) }
function p(y) {
    return y < 10 ? ("0" + y) : y.toString() 
} 
}
})(window,Core,jQuery,easyNav);