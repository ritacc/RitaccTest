(function (j, e, i, c, d) {
    var a = 0, k = function (n, r) {
        var o = e.browser, s = []; for (var q in o) {
            if (q == "version") { s.push("version=" + o[q]) } else { s.push("browser=" + q) }
        }
        var p = j.setInterval(function () {
            if (j.neteaseTracker) {
                j.clearInterval(p);
                i.virualViewStat("http://caipiao.?err=" + r + "&" + s.join("&") + "&page=" + encodeURIComponent(document.URL), "脚本com组件错误")
            }
        }, 200);
        if (a == 0) {
            j.alert('脚本遇到错误："' + n + '"\n错误信息已经搜集，我们会尽快解决，或者联系客服解决 ')
        } else {
            j.alert("该页面遇到多处脚本错误，请联系我们的客服（020-83568090），我们会安排技术人员直接为您服务。") 
        } a++
    };
    var m = c.regBaseCom2Lib("COMS.PT.Timer", "onStart onStop onRunning onPause onResume", {
        config: {
            timeCount: null, timeInterval: 0, autoSaveKey: ""
        }, STATUS: { inited: 0, running: 1, paused: 2, stoped: 3 }
    , init: function (o) { var n = c.getType(o), p; switch (n) { case "number": this.config = e.extend({}, this.config, { timeCount: o }); break; case "object": this.config = e.extend({}, this.config, o || {}); break; default: this.config = e.extend({}, this.config); return } this.status = this.STATUS.inited; p = this.config.timeCount; if (p !== null && p !== d) { this.config.timeCount = +(p || 0); this.start() } }
    , start: function (s) { var p = this.config; if (s) { p.timeCount = +s } var o = p.timeInterval || 500, n, r = this, q = +new Date(), t; this.timer && j.clearInterval(this.timer); this.timer = j.setInterval(n = function () { var v = +new Date(), u; p.timeCount -= (v - q); q = v; if (p.timeCount < 500) { r.stop() } else { u = r.getTimeDescription(); if (u.join(",") !== t.join(",")) { t = u; r.callEvent("onRunning", t[0], t[1], t[2], t[3]) } } }, Math.max(o, 100)); t = this.getTimeDescription(); if (this.status == this.STATUS.inited || this.status == this.STATUS.stoped) { this.callEvent("onStart", t[0], t[1], t[2], t[3]) } this.status = this.STATUS.running; n() }, stop: function () { this.timer && j.clearInterval(this.timer); this.status = this.STATUS.stoped; this.started = false; this.callEvent("onStop") }, getTimeDescription: function () { var n = this.config.timeCount, p = +n > 0 ? +n : 0, r = 60000, q = 60 * r, o = 24 * q; return [Math.floor(p / o), Math.floor(p % o / q), Math.floor(p % o % q / r), Math.floor(p % o % q % r / 1000)] }, setAutoSaveKey: function (n) { } 
    }); m.connectTime = j.performance && j.performance.timing ? j.performance.timing.connectStart || new Date() : new Date(); m.getPageRunTime = function () { return new Date() - m.connectTime }; var b = c.regBaseCom2Lib("COMS.PT.BetArea", "onChange onBallClick onBallChange onRandom onClear", { config: { wrap: "", redBallSelector: ".redBallBox li a", redBallActive: "got", redBallHightL: "active", blueBallSelector: ".blueBallBox li a", blueBallActive: "got", blueBallHightL: "active", useShortCut: true, animate: true }, init: function (q) { var p = c.getType(q), n = this; switch (p) { case "string": case "element": this.config = e.extend({}, this.config, { wrap: q }); break; case "object": if (q.jquery) { this.config = e.extend({}, this.config, { wrap: q }); break } this.config = e.extend({}, this.config, q || {}); break; default: return } var o = e(this.config.wrap); if (!o[0]) { k("投注区容器设置错误，初始化失败！", "area001"); return } o.delegate(this.config.redBallSelector, "click contextmenu", function (r) { return n.ballClick(this, r, "red") }).delegate(this.config.blueBallSelector, "click contextmenu", function (r) { return n.ballClick(this, r, "blue") }); this.cache = { redBalls: o.find(this.config.redBallSelector), blueBalls: o.find(this.config.blueBallSelector), redBallLast: null, blueBallLast: null} }, ballClick: function (v, y, o) {
        var D = e(v), u = this.config[o + "BallActive"], n = this.config[o + "BallHightL"], q = this, E, t = y.type == "contextmenu", w = y.type == "click", z = function (G, J) {
            var I = [], H; e.each(J, function (K, L) { I[K] = +e(L).text() }); if (q.callEvent("onBallClick", o, G, I) === false) {
                H = false
            } if (q.callEvent("onBallChange", o, G, I) === false) { H = false } if (H == false) { return false } e.each(J, function (K, L) { e(L)[G ? "addClass" : "removeClass"](u + " " + n) }); q.callEvent("onChange", "click")
        }; if (!q.config.useShortCut) { E = z(D.hasClass(u) ? 0 : 1, D) } else { if (y.ctrlKey && w) { E = z(1, D) } else { if (y.altKey && w) { E = z(0, D) } else { if ((y.shiftKey && w) || t) { var r = this.cache[o + "BallLast"], F = this.cache[o + "Balls"], x = r ? F.index(r) : 0, B = F.index(D), A = r ? r.hasClass(u) ? 1 : 0 : 1, C = [], p = Math.min(x, B), s = Math.max(x, B); if (x == B) { if (w) { E = z(D.hasClass(u) ? 0 : 1, D) } } else { F.each(function (G, H) { if (G >= p && G <= s) { C.push(H) } }); E = z(A, C) } y.preventDefault() } else { if (w) { E = z(D.hasClass(u) ? 0 : 1, D) } } } } } if (E !== false) { this.cache[o + "BallLast"] = D } 
    }, get: function (o, n) { if (!o || !{ red: 1, blue: 1}[o]) { return [this.get("red", n), this.get("blue", n)] } var q = this.cache[o + "Balls"], r = this.config[o + "BallActive"], p = []; q.each(function (t, u) { if (e(u).hasClass(r)) { var s = e(u).text(); p.push(n ? s : +s) } }); return p }, clear: function (q, n) { if (!q || !{ red: 1, blue: 1}[q]) { this.clear("red", n); this.clear("blue", n); return this } var s = this.cache[q + "Balls"], t = this.config[q + "BallActive"], p = this.config[q + "BallHightL"], r = this.get(q), o; if (!r.length) { return this } if (!n) { if (this.callEvent("onClear", q) === false) { o = false } } if (this.callEvent("onBallChange", q, 0, r) === false) { o = false } if (o == false) { return this } this.__stopAniTimer(q); s.removeClass(t + " " + p); this.callEvent("onChange", "clear"); return this }
    , push: function (o, p, n) { return this.__change(o, 1, p, n) }, pull: function (n, o) { return this.__change(n, 0, o) }, __change: function (z, w, o, p) { if (!z || !{ red: 1, blue: 1}[z] || !o || !o.length) { return this } var q = e.map(o, function (n) { return +n }), u = this.config[z + "BallActive"], t = this.config[z + "BallHightL"], r = this, A = [], y = [], x = w == 0; this.cache[z + "Balls"].each(function (B, D) { var C = e(D), n = +C.text(); if (e.inArray(n, q) >= 0 && C.hasClass(u) === x) { y.push(D); A.push(n) } }); if (!A.length) { return this } if (this.callEvent("onBallChange", z, w, A) === false) { return this } this.__stopAniTimer(z); e.each(y, function (n, B) { if (w) { e(B).addClass((!r.config.animate || r._closeAnimteOnce) ? u + " " + t : u) } else { e(B).removeClass(u + " " + t) } }); if (w && r.config.animate && !r._closeAnimteOnce) { var s = y.length, v = 0; this["__" + z + "hltimer"] = j.setInterval(function () { if (v < s) { e(y[v]).addClass(t) } else { r.__stopAniTimer(z) } v++ }, 50) } if (r._closeAnimteOnce) { delete r._closeAnimteOnce } this.callEvent("onChange", p || (w ? "push" : "pull")); return this }, __stopAniTimer: function (q) { var n = "__" + q + "hltimer", o = this, r = o.config[q + "BallActive"], p = o.config[q + "BallHightL"]; if (o[n]) { j.clearInterval(o[n]); o[n] = d; o.cache[q + "Balls"].each(function (s, u) { var t = e(u); t[t.hasClass(r) ? "addClass" : "removeClass"](p) }) } }, random: function (p, o) {
        if (!p || !{ red: 1, blue: 1}[p] || !o) { return this } var q = [], n = [], u = [], t, r, s = this.config[p + "BallActive"]; this.cache[p + "Balls"].each(function (w, x) { var v = e(x), y = +e(x).text(); if (v.hasClass(s)) { n.push(y) } else { u.push(y) } q[w] = y }); if (this.callEvent("onRandom", p, r = Math.min(q.length, +o)) === false) {
            return this
        }
        if (n.length < r) {
            t = c.random(u, r - n.length).concat(n);
            this._closeAnimteOnce = n.length > 0
        }
        else {
            t = c.random(q, r)
        }
        return this.set(p, t, "random")
    }
    , set: function (o, p, n) {
        if (c.getType(o) == "array" && o.length == 2 && c.getType(o[0]) == "array" && c.getType(o[1]) == "array") {
            return this.set("red", o[0]).set("blue", o[1])
        } if (!o || !{ red: 1, blue: 1}[o] || !p) {
            return this
        } 
    return this.clear(o, true).push(o, p, n) }, toggle: function (p, q) {
        if (!p || !{ red: 1, blue: 1}[p] || !q || !q.length) { return this }
        var s = e.map(q, function (t) { return +t })
        , n = [], o = []
        , r = this.config[p + "BallActive"]; 
        this.cache[p + "Balls"].each(function (u, w) {
            var v = e(w), t = +v.text(); if (e.inArray(t, s) >= 0) {
                v.hasClass(r) ? n.push(t) : o.push(t) 
            }
        });
        return !o.length ? this.pull(p, n) : this.push(p, o)
    }
});
var g = c.regBaseCom2Lib("COMS.PT.DingDanShaHaoArea", "onChange onBallClick onBallChange onClear", {
    config: {
        wrap: "", redBallSelector: ".redBallBox li a"
    , redBallActive: "active"
    , redBallDisable: "killNum"
    , blueBallSelector: ".blueBallBox li a"
    , blueBallActive: "active"
    , blueBallDisable: "killNum"
    }
    , init: function (q) {
        var p = c.getType(q), n = this;
        switch (p) {
            case "string":
            case "element":
                this.config = e.extend({},this.config, { wrap: q});
                break; 
            case "object": 
                if (q.jquery) {
                    this.config = e.extend({}, this.config, { wrap: q }); 
                    break
                }
                this.config = e.extend({}, this.config, q || {});
                break;
            default: 
                return
        }
        var o = e(this.config.wrap); if (!o[0]) {
            k("投注区容器设置错误，初始化失败！", "area001"); return
        }
        o.delegate(this.config.redBallSelector, "click", function (r) {
            return n.ballClick(this, r, "red")
        }).delegate(this.config.blueBallSelector, "click", function (r) {
            return n.ballClick(this, r, "blue")
        });
        this.cache = { redBalls: o.find(this.config.redBallSelector), blueBalls: o.find(this.config.blueBallSelector) }
    }
    , ballClick: function (o, s, w) {
        var v = e(o), p = this.config[w + "BallActive"], q = this.config[w + "BallDisable"], n = this, t, r = v.text();
        function u(x) {
            var y = true; if (n.callEvent("onBallClick", w, x, [r]) === false) { y = false }
            if (n.callEvent("onBallChange", w, x, [r]) === false) { y = false } return y
        } t = v.hasClass(p) ? -1 : v.hasClass(q) ? 0 : 1;
        if (!u(t)) { return false } v[t === 1 ? "addClass" : "removeClass"](p); v[t === -1 ? "addClass" : "removeClass"](q); n.callEvent("onChange", "click")
    }
    , get: function (o, n) {
        if (!o || !{ red: 1, blue: 1}[o]) {
            return [this.get("red", n), this.get("blue", n)]
        }
        var r = this.cache[o + "Balls"], s = this.config[o + "BallActive"], p = this.config[o + "BallDisable"], q = { dan: [], sha: [] };
        r.each(function (u, v) {
            if (e(v).hasClass(s)) { var t = e(v).text(); q.dan.push(n ? t : +t) } if (e(v).hasClass(p)) {
                var t = e(v).text(); q.sha.push(n ? t : +t)
            } 
        }); return q
    }, clear: function (p, n) {
        if (!p || !{ red: 1, blue: 1}[p]) {
            this.clear("red", n); this.clear("blue", n); return this
        } var s = this.cache[p + "Balls"], t = this.config[p + "BallActive"], q = this.config[p + "BallDisable"], r = this.get(p), o;
        if (!r.dan.length && !r.sha.length) { return this } if (!n) { if (this.callEvent("onClear", p) === false) { o = false } }
        if (this.callEvent("onBallChange", p, 0, r) === false) { o = false } if (o == false) { return this } s.removeClass(t + " " + q);
        this.callEvent("onChange", "clear"); return this
    }
    , dan: function (n, o) { this.__change(n, 1, o) }
    , pull: function (n, o) { this.__change(n, 0, o) }
    , sha: function (n, o) { this.__change(n, -1, o) }
    , __change: function (u, s, n) {
        if (!u || !{ red: 1, blue: 1}[u] || !n || !n.length) { return this }
        var o = e.map(n, function (w) { return +w }), q = this.config[u + "BallActive"], r = this.config[u + "BallDisable"], p = this, v = [], t = []; s = +s;
        this.cache[u + "Balls"].each(function (y, A) {
            var z = e(A), x = +z.text(), w = false;
            if (e.inArray(x, o) >= 0) {
                if (s == 0) { (z.hasClass(q) || z.hasClass(r)) && (w = true) } else { if (s == 1) { !z.hasClass(q) && (w = true) } else { !z.hasClass(r) && (w = true) } }
            }
            if (w) { t.push(A); v.push(x) } 
        }); if (!v.length) { return this }
        if (this.callEvent("onBallChange", u, s, v) === false) {
            return this
        }
        e.each(t, function (x, y) {
            var w = e(y); w.removeClass([q, r].join(" ")); if (s == 1) { w.addClass(q) } if (s == -1) { w.addClass(r) }
        });
        this.callEvent("onChange", ["sha", "pull", "dan"][s + 1]); return this
    }, random: function (o, q) {
        var u = this.get(), p = u[0], r = u[1], t = [], n = [], s = this; e.each(["red", "blue"], function (y, A) {
            var z = [], x = s.config[A + "BallActive"], w = s.config[A + "BallDisable"], v = A === "red" ? o : q; s.cache[A + "Balls"].each(function (C, D) {
                var B = e(D); !B.hasClass(x) && !B.hasClass(w) && z.push(+B.text())
            }); if (A == "red") { t = c.random(z, v) } else { n = c.random(z, v) }
        }); 
        n = c.unique(r.dan, n).sort(function (w, v) { return +w - +v }); if (p.dan.length && p.dan.length + o > 6) {
            return [p.dan, [], t, n] 
    } else { return [c.unique(p.dan, t).sort(function (w, v) { return +w - +v }), n] } } }); var l = c.regBaseCom2Lib("COMS.PT.DantuoArea", "onChange onBallClick onBallChange onClear", { config: { wrap: "", danWrap: ".danMa", tuoWrap: ".tuoMa", redBallSelector: ".redBallBox li a", redBallActive: "active", blueBallSelector: ".blueBallBox li a", blueBallActive: "active" }, init: function (s) { var r = c.getType(s), p = this; switch (r) { case "string": case "element": this.config = e.extend({}, this.config, { wrap: s }); break; case "object": if (s.jquery) { this.config = e.extend({}, this.config, { wrap: s }); break } this.config = e.extend({}, this.config, s || {}); break; default: return } var q = e(this.config.wrap), o = q.find(this.config.danWrap), n = q.find(this.config.tuoWrap); if (!q[0] || !o[0] || !n[0]) { k("胆拖投注区容器设置错误，初始化失败！", "dantuo001"); return } e.each(["dan", "tuo"], function (t, u) { e.each(["red", "blue"], function (v, w) { q.delegate(p.config[u + "Wrap"] + " " + p.config[w + "BallSelector"], "click", function (x) { return p.ballClick(this, x, u, w) }) }) }); this.cache = { danWrap: o, dan: { redBalls: o.find(this.config.redBallSelector), blueBalls: o.find(this.config.blueBallSelector) }, tuoWrap: n, tuo: { redBalls: n.find(this.config.redBallSelector), blueBalls: n.find(this.config.blueBallSelector)}} }, ballClick: function (p, r, o, u) { var t = e(p), q = this.config[u + "BallActive"], n = this, s, v = function (w, z) { var y = [], x; e.each(z, function (A, B) { y[A] = +e(B).text() }); if (n.callEvent("onBallClick", o, u, w, y) === false) { x = false } if (n.callEvent("onBallChange", o, u, w, y) === false) { x = false } if (x == false) { return false } e.each(z, function (A, B) { e(B)[w ? "addClass" : "removeClass"](q) }); if (w == 1) { n.pull({ dan: "tuo", tuo: "dan"}[o], u, y) } n.callEvent("onChange", "click") }; s = v(t.hasClass(q) ? 0 : 1, t) }, get: function (s, o, n) { if (!s || !{ dan: 1, tuo: 1}[s]) { return [this.get("dan", "red", n), this.get("dan", "blue", n), this.get("tuo", "red", n), this.get("tuo", "blue", n)] } if (!o || !{ red: 1, blue: 1}[o]) { return [this.get(s, "red", n), this.get(s, "blue", n)] } var q = this.cache[s][o + "Balls"], r = this.config[o + "BallActive"], p = []; q.each(function (u, v) { if (e(v).hasClass(r)) { var t = e(v).text(); p.push(n ? t : +t) } }); return p }, clear: function (t, p, n) { if (!t || !{ dan: 1, tuo: 1}[t]) { this.clear("dan", "red", n); this.clear("dan", "blue", n); this.clear("tuo", "red", n); this.clear("tuo", "blue", n); return this } if (!p || !{ red: 1, blue: 1}[p]) { this.clear(t, "red", n); this.clear(t, "blue", n); return this } var r = this.cache[t][p + "Balls"], s = this.config[p + "BallActive"], q = this.get(t, p), o; if (!q.length) { return this } if (!n) { if (this.callEvent("onClear", t, p) === false) { o = false } } if (this.callEvent("onBallChange", t, p, 0, q) === false) { o = false } if (o == false) { return this } r.removeClass(s); this.callEvent("onChange", "clear"); return this }, push: function (q, o, p, n) { return this.__change(q, o, 1, p, n) }, pull: function (p, n, o) { return this.__change(p, n, 0, o) }, __change: function (q, v, s, n, o) { if (!q || !{ dan: 1, tuo: 1}[q] || !v || !{ red: 1, blue: 1}[v] || !n || !n.length) { return this } var p = e.map(n, function (x) { return +x }), r = this.config[v + "BallActive"], w = [], u = [], t = s == 0; this.cache[q][v + "Balls"].each(function (y, A) { var z = e(A), x = +z.text(); if (e.inArray(x, p) >= 0 && z.hasClass(r) === t) { u.push(A); w.push(x) } }); if (!w.length) { return this } if (this.callEvent("onBallChange", q, v, s, w) === false) { return this } e.each(u, function (x, y) { e(y)[s ? "addClass" : "removeClass"](r) }); if (s == 1) { this.__change({ dan: "tuo", tuo: "dan"}[q], v, 0, w) } this.callEvent("onChange", o || (s ? "push" : "pull")); return this }, set: function (q, o, p, n) { if (c.getType(q) == "array" && q.length == 4 && c.getType(q[0]) == "array" && c.getType(q[1]) == "array" && c.getType(q[2]) == "array" && c.getType(q[3]) == "array") { return this.set("dan", "red", q[0]).set("dan", "blue", q[1]).set("tuo", "red", q[2]).set("tuo", "blue", q[3]) } if (!q || !{ dan: 1, tuo: 1}[q] || !o || !{ red: 1, blue: 1}[o] || !p) { return this } return this.clear(q, o, true).push(q, o, p, n) }, toggle: function (t, p, q) { if (!t || !{ dan: 1, tuo: 1}[t] || !p || !{ red: 1, blue: 1}[p] || !q || !q.length) { return this } var s = e.map(q, function (u) { return +u }), n = [], o = [], r = this.config[p + "BallActive"]; this.cache[t][p + "Balls"].each(function (v, x) { var w = e(x), u = +w.text(); if (e.inArray(u, s) >= 0) { w.hasClass(r) ? n.push(u) : o.push(u) } }); return !o.length ? this.pull(t, p, n) : this.push(t, p, o) } }); var h = c.regBaseCom2Lib("COMS.PT.BetPool", "onChange onDelete onAdd onEdit onRandom onClear", { config: { wrap: "", highlight: "ddactive", hover: "ddhover", inedit: "inEdit", useServerRandom: true, serverRandomUrl: "", random: null, edit: null, adapter: null, counter: null, serialize: null, animate: true }, init: function (q) { this.config = e.extend({}, this.config, q || {}); if (!e.isFunction(this.config.random)) { k("没有提供机选算法接口random，初始化失败！", "pool001"); return } if (!e.isFunction(this.config.edit)) { k("没有提供投注数据修改接口edit，初始化失败！", "pool002"); return } if (!e.isFunction(this.config.adapter)) { k("没有提供数据转化器adapter，初始化失败！", "pool003"); return } if (!e.isFunction(this.config.counter)) { k("没有提供数据统计器counter，初始化失败！", "pool004"); return } var p = e(this.config.wrap); if (!p[0]) { k("号码篮容器设置错误，初始化失败！", "pool005"); return } if (!p.find(">dl")[0]) { p.html("<dl></dl>") } this.config.wrap = p; if (e.browser.msie && e.browser.version < 7) { var o = this.config.hover; p.delegate("dd", "mouseenter", function () { e(this).addClass(o) }).delegate("dd", "mouseleave", function () { e(this).removeClass(o) }) } var n = this; p.delegate("a[rel=betPoolAct]", "click", function (u) { var t = e(this), s = t.attr("pid") || 0, r = this.hash.substr(1); switch (r) { case "del": n.del(s); break; case "edit": n.__edit(s); break } u.preventDefault() }); this.cache = { list: p.find("dl"), data: [], guid: 1} }, push: function () { return this.push2(Array.prototype.slice.call(arguments, 0)) }, push2: function (n, o) { if (c.getType(n) !== "array") { return this } this.notHeightLight = !!o; this.insert(0, -1, n); delete this.notHeightLight; return this }, getData: function () { return this.cache.data.slice(0) }, serialize: function () { if (!e.isFunction(this.config.serialize)) { k("没有提供数据序列化接口serialize！", "pool006"); return "" } return this.config.serialize(this.getData()) }, getCount: function (r) { var s = 0, u = 0, o = r || this.cache.data, t = o.length, p = 0, q; for (; p < t; p++) { q = this.config.counter(o[p]); if (!q || q.length != 2) { k("数据统计器返回值错误！", "pool007") } else { s += q[0]; u += q[1] } } return [s, u] }, insert: function (A, s, q, n) { var u = this.cache.data, z = u.length, p = this, D = q.slice(0), t, B = n == d ? "onAdd" : n, o = z == 0 || A == 0 ? 0 : !A ? z : -1, y; if (o < 0) { e.each(u, function (E, F) { if (F.guid == +A) { o = s > 0 ? E + 1 : E; return } }) } if (o < 0) { return this } e.each(D, function (E, F) { D[E] = p.__setGuid(F) }); t = [].concat(u.slice(0, o), D, u.slice(o)); if (B) { if (this.callEvent(B, D, t) === false) { return this } } y = []; e.each(D, function (E, F) { y[E] = p.__getItem(F, F.guid) }); if (o == 0) { this.cache.list.prepend(y.join("")).scrollTop(0); if (q.length == 1 && p.config.animate) { this.cache.list.find("dd:first").hide().show(300) } } else { if (!A) { this.cache.list.append(y.join("")).scrollTop(this.cache.list[0].scrollHeight) } else { var C = this.cache.list.find("dd[gid=" + A + "]"), w = C.height(), x = C.offset().top - this.cache.list.offset().top + w, v = x - this.cache.list.height() + w, r = this.cache.list.scrollTop(); C[s > 0 ? "after" : "before"](y.join("")); if (curPos < v || r > x) { this.cache.list.scrollTop(parseInt((v + x) / 2)) } } } this.__highlight(D); this.cache.data = t; B && this.callEvent("onChange", B.replace(/^on/g, "").toLowerCase()); return this }, del: function (r) { var q = this.__inCache(r), o, s, p, n; if (q < 0) { return this } o = this.cache.data; s = o[q]; p = [].concat(o.slice(0, q), o.slice(q + 1)); if (this.callEvent("onDelete", s, p) === false) { return this } n = this.cache.list.find("dd[gid=" + r + "]"); if (this.config.animate) { n.find("a").attr("href", "#"); n.hide(function () { e(this).remove() }) } else { n.remove() } this.cache.data = p; this.callEvent("onChange", "del"); return this }, clear: function () { var o = this.cache.data, p = o.length; if (p == 0) { return this } if (this.callEvent("onClear") === false) { return this } this.cache.list.empty().html(""); this.cache.data.length = 0; this.callEvent("onChange", "clear"); return this }, edit: function (o, p) { var n = this.__inCache(o), r, q; if (!p) { return this } if (n >= 0 && this.callEvent("onEdit", this.cache.data[n], p) === false) { return this } if (n < 0) { return this.push(p) } r = this.cache.list.find("dd[gid=" + o + "]"); q = this.__setGuid(p, o); r.after(this.__getItem(q, q.guid)); r.remove(); this.cache.data[n] = q; this.__highlight([q]); this.callEvent("onChange", "edit"); return this }, __edit: function (q) { var o = this, n = o.cache.data, r = o.config.inedit, p = o.__inCache(q); if (p >= 0) { o.cache.list.find("." + r).removeClass(r).end().find("[gid=" + q + "]").addClass(r); o.config.edit(q, o.cache.data[p]) } return this }, random: function (p, u) { var r = p || 1, n = this, t = this.config.serverRandomUrl, o, s, q = function (v) { var w = n.config.random(v, r); if (!w || !w.length) { k("机选算法接口返回值错误！", "pool008"); return } if (n.callEvent("onRandom", v ? "server" : "client", w) === false) { return } n.insert(0, 1, w, ""); n.callEvent("onChange", "random"); e.isFunction(u) && u(w); q = e.noop }; if (this.config.useServerRandom && t) { o = e.isFunction(t) ? t.call(this.config) || "" : t; !o ? q("") : i.get(o.replace(/{n}/g, r), function (w, v) { s && j.clearTimeout(s); q(w ? "" : v) }, "@randomJX"); s = j.setTimeout(function () { q("") }, 300) } else { q("") } return this }, __getItem: function (q, n) { var p = "<dd gid=" + n + ">" + this.config.adapter(q) + "</dd>", o = { del: '<a rel="betPoolAct" pid="' + n + '" href="#del">删除</a>', edit: '<a rel="betPoolAct" pid="' + n + '" href="#edit">修改</a>' }; return c.format(p, o) }, __setGuid: function (p, n) { if (p.guid) { return p } var o = c.getType(p), q; switch (o) { case "string": q = new String(p); break; case "number": q = new Number(p); break; default: q = p; break } q.guid = n || (this.cache.guid++); return q }, __inCache: function (p, r) { var o = r || this.cache.data, s = o.length, q = 0; for (; q < s; q++) { if (o[q].guid == +p) { return q } } return -1 }, __highlight: function (r) { if (this.notHeightLight) { return this } var p = this.__highlight.Cache, o = this, q = o.config.highlight, n = function (s) { var t = []; o.cache.list.find("dd").each(function () { var v = e(this).attr("gid") || 0, u = o.__inCache(v, s); if (u >= 0) { t.push(e(this).addClass(q)) } }); return t }; if (p && p.length > 0) { this.__highlight.Cache = p.concat(n(r)); return this } this.__highlight.Cache = n(r); j.setTimeout(function () { var s = o.__highlight.Cache; j.setTimeout(function () { e.each(s, function (u, t) { t.removeClass(q) }); s = null }, 900); o.__highlight.Cache = [] }, 500) } }); var f = c.regBaseCom2Lib("COMS.PT.iNumber", "onChange", { config: { wrap: "", addSelector: ".add", reduceSelector: ".subtract", addDisCss: "addDisable", addDownCss: "addDown", reduceDisCss: "subtractDisable", reduceDownCss: "subtractDown", min: 1, max: 9999, step: 1, editable: true }, init: function (q) {
        var p = c.getType(q), n = this; switch (p) {
            case "string": case "element": this.config = e.extend({}, this.config, { wrap: q });
                break; case "object": if (q.jquery) {
                    this.config = e.extend({}, this.config, { wrap: q }); break
                }
                this.config = e.extend({}, this.config, q || {});
                break; default: return
        }
        var o = e(this.config.wrap);
        if (!o[0]) {
            k("数字容器设置错误，初始化失败！", "num001"); return 
        } 
         this.cache = { wrap: o, input: o.find("input"), add: o.find(this.config.addSelector).disableDrag(), reduce: o.find(this.config.reduceSelector).disableDrag() }; this.cache.input.val(this.get()); this.__initCtrl(this.config.addSelector, +n.config.step, this.config.addDownCss).__initCtrl(this.config.reduceSelector, -n.config.step, this.config.reduceDownCss); if (this.config.editable) {
             i.loadCdnJS("js2/liveCheck.js", function () {
                 return !!e.fn.bindLiveCheck 
            }, function () {
//                n.cache.input.bindLiveCheck(/\D/g, function () {
//                    var s = n.get(), r = this.value; if (s + "" != r && r) {
//                        this.value = s
//                    } r && n.callEvent(200, "onChange", +r)
//                }).blur(function () { n.set(this.value) }).disableIME()
            })
        } else { this.cache.input.attr("readonly", "readonly") } n.onChange(n.__checkCtrl); n.__checkCtrl()
    }
, __initCtrl: function (n, q, r) {
    var p = function () {
        this.ctimer && j.clearTimeout(this.ctimer); this.stimer && j.clearInterval(this.stimer)
    }
, o = this; this.cache.wrap.delegate(n, "click", function (s) {
    p.call(this); return o.__ctrlClick(this, s, q)
}).delegate(n, "mousedown", function (t) {
    var s = this; this.ctimer = j.setTimeout(function () {
        s.stimer = j.setInterval(function () { o.__ctrlClick(s, t, q) }, 150)
    }, 400)
}).delegate(n, "mouseleave", function (s) { p.call(this) }); if (e.fn.setControlEffect && r) {
    this.cache.wrap.find(n).setControlEffect(r)
} return this
}
, __ctrlClick: function (p, o, n) {
    if (e(p).hasClass("disabled")) {
        return
    } this.set(this.get() + n)
}
, __convert: function (q) {
    var p = (q + "").replace(/\D/g, ""), r = this.config.min, o = this.config.max, s; if (!p.length) { p = r } s = +p; s = s < r ? r : s > o ? o : s; return s
}
, __checkCtrl: function () {
    var p = this.config, q = p.min, o = p.max, r = this.get();
    this.cache.add[o == r ? "addClass" : "removeClass"](p.addDisCss);
    this.cache.reduce[q == r ? "addClass" : "removeClass"](p.reduceDisCss)
}, get: function () {
    return this.__convert(this.cache.input[0].value)
}
, set: function (p) {
    var q = this.__convert(p), o = this.cache.input[0]; if (q + "" != o.value) { o.value = q; this.callEvent("onChange", q) } return this
}
, hide: function () { this.config.wrap.hide(); return this }
, show: function () { this.config.wrap.show(); this.onChange(); return this } 
})
})(window,jQuery,Core,Game);