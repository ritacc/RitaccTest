(function (a, b, c) {
    (function () {
        try { document.execCommand("BackgroundImageCache", false, true) }
        catch (h) { }
        b.extend(Number.prototype, {
            Round: function (e, i) {
                var f = Math.pow(10, e || 0);
                return i == 0 ? Math.ceil(this * f) / f : Math.round(this * f + (5 - (i || 5)) / 10) / f
            }, Cint: function (e) { return this.Round(0, e) } 
        }); var g = /./, d = g.compile && g.compile(g.source, "g"); RegExp.regCompile = d; b.extend(String.prototype, { trim: function () { return this.replace(/^(?:\s|\xa0|\u3000)+|(?:\s|\xa0|\u3000)+$/g, "") }, byteLen: function () { return this.replace(/([^\x00-\xff])/g, "ma").length }, cutString: function (i, e) { var m = /([^\x00-\xff])/g, j = /([^\x00-\xff]) /g; if (e) { var l = String(e), f = l.length, k = this.replace(m, "$1 "); i = i >= f ? i - f : 0; e = k.length > i ? l : ""; return k.substr(0, i).replace(j, "$1") + e } return this.substr(0, i).replace(m, "$1 ").substr(0, i).replace(j, "$1") } }); b.fn.fixPosition = function () { var m = this, q, p, i, e, j = function (r, l) { var s = (r[0].currentStyle[l]); return s.indexOf("%") + 1 ? false : (r.css(l).replace(/\D/g, "") || null) }, k = b(a), o, f, n; if (m.css("position") == "absolute") { q = j(m, "top"); p = j(m, "bottom"); i = j(m, "left"); e = j(m, "right"); o = +k.scrollTop(); f = +k.scrollLeft(); n = function (t) { var l = t.type == "resize", r; if (l) { r = m.is(":hidden"); if (!r) { m.hide() } } var u = +k.scrollTop(), s = +k.scrollLeft(); p && m.css("bottom", +p + 1).css("bottom", p + "px"); q && m.css("top", (+q + u - o) + "px"); e && m.css("right", +e + 1).css("right", e + "px"); i && m.css("left", (+i + s - f) + "px"); if (l && !r) { m.show() } }; k.scroll(n).resize(n) } return m }
    })(); (function () { b.isIE678 = "\v" == "v"; if (b.isIE678) { b.isIE8 = !!"1"[0]; b.isIE6 = !b.isIE8 && (!document.documentMode || document.compatMode == "BackCompat"); b.isIE7 = !b.isIE8 && !b.isIE6; b.fn.extend({ _bind_: b.fn.bind, bind: function (f, d, i) { /^click$/gi.test(f) && h(this); return this._bind_(f, d, i) } }); var h = function (d) { var k = d.length, f = 0, j; for (; f < k; f++) { j = d[f]; if (!j.fixClick) { j.fixClick = true; b(j).bind("dblclick", function (l) { var i = l.target, m = 0; while (i && i.nodeType !== 9 && (i.nodeType !== 1 || i !== this)) { if (i.nodeType === 1) { if (i.fixClick) { return } } i = i.parentNode } l.type = "click"; l.source = "dblclick"; b(l.target).trigger(l) }) } } }; var g = "abbr,article,aside,audio,canvas,datalist,details,dialog,eventsource,figure,footer,header,hgroup,mark,menu,meter,nav,output,progress,section,time,video".split(","), e = g.length; while (e--) { document.createElement(g[e]) } } })(); b.extend({ getUrlPara: function (f) { var d = a.location.search.replace(/^\?/g, ""), g = d; try { g = decodeURI(d) } catch (h) { g = d.replace(/"%26"/g, "&") } return b.getParaFromString(g, f) }, getHashPara: function (d) { return b.getParaFromString(a.location.hash.replace(/^#*/, ""), d) }, getPara: function (d) { return b.getUrlPara(d) || b.getHashPara(d) }, getParaFromString: function (e, h) { var g = e.split("&"), f = {}; b.each(g, function () { var d = this.split("="); d[0] && d.length > 1 && (f[d[0]] = decodeURIComponent(d[1])) }); if (h === c) { return f } else { return f[h] || "" } }, safeHTML: function (d) { return String(d).replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#39;") }, safeRegStr: function (d) { return String(d).replace(/([\\\(\)\{\}\[\]\^\$\+\-\*\?\|])/g, "\\$1") }, falseFn: function () { return false }, stopProp: function (d) { d.stopPropagation() }, preventDft: function (d) { d.preventDefault() }, isLeftClick: function (d) { return d.button == ("\v" == "v" ? 1 : 0) }, addUrlPara: function (e, d) { var f = (e + "").indexOf("?") + 1 ? "&" : "?"; return e + f + d }
    , addFav: (a.sidebar && a.sidebar.addPanel) ? function (e, d) { a.sidebar.addPanel(d, e, "") } : function (f, d) { try { a.external.addFavorite(f, d) } catch (g) { a.alert("请尝试点击 Ctrl + D 来添加！") } }, formatTime: function (t, r) { var o = /^\d+$/gi.test(t + "") ? +t : Date.parse(t); if (isNaN(o)) { return t } var f = new Date(o), i = function (d) { return ("0" + d).slice(-2) }, l = f.getFullYear(), q = f.getMonth() + 1, v = i(q), p = f.getDate(), u = i(p), n = f.getHours(), j = i(n), g = f.getMinutes(), k = i(g), x = f.getSeconds(), w = i(x); return (r || "yyyy-MM-dd hh:mm:ss").replace(/yyyy/g, l).replace(/MM/g, v).replace(/M/g, q).replace(/dd/g, u).replace(/d/g, p).replace(/hh/g, j).replace(/h/g, n).replace(/mm/g, k).replace(/m/g, g).replace(/ss/g, w).replace(/s/g, x) }, template: function (e, d, i) { var h = i || "%", g = new Function("var p=[],my=this,data=my,print=function(){p.push.apply(p,arguments);};p.push('" + e.replace(/[\r\t\n]/g, " ").replace(new RegExp("<" + h + "=\\s*([^\\t]*?);*\\s*" + h + ">", "g"), "<" + h + "print($1);" + h + ">").split("<" + h).join("\t").replace(new RegExp("((^|" + h + ">)[^\\t]*)'", "g"), "$1\r").replace(new RegExp("\\t=(.*?)" + h + ">", "g"), "',$1,'").split("\t").join("');").split(h + ">").join("p.push('").split("\r").join("\\'") + "');return p.join('');"); return d ? g.call(d) : g } });
    b.fn.extend({
        disabled: function (d) {
            return this.each(function () { 
            var e = this.bindDownCssFix || "", f = !d ? "disabled" + e : d; b(this).attr("disabled", "disabled").addClass(f)[0].disabled = true }) }
,enabled: function (d) {
        return this.each(function () {
            var e = this.bindDownCssFix || "", f = !d ? "disabled" + e : d; b(this).removeClass(f).removeAttr("disabled")[0].disabled = false
        })
    }
, disableDrag: function () { 
    return this.bind("dragstart", b.falseFn) }, enableDrag: function () { return this.unbind("dragstart", b.falseFn) } }); (function () {
        var d = RegExp.regCompile ? /./.compile("\\{([\\w\\.]+)\\}", "g") : /\{([\w\.]+)\}/g; b.format = function (l, j) {
            var i = true, k, g, h = j === c ? null : b.isPlainObject(j) ? (i = false, j) : b.isArray(j) ? j : Array.prototype.slice.call(arguments, 1);
            if (h === null) { return l } k = i ? h.length : 0; g = RegExp.regCompile ? /./.compile("^\\d+$") : /^\d+$/; return String(l).replace(d, function (f, e) { var r = g.test(e), q, o, p; if (r && i) { q = parseInt(e, 10); return q < k ? h[q] : f } else { o = e.split("."); p = h; for (var m = 0; m < o.length; m++) { p = p[o[m]] } return p === c ? f : p } })
        }
    })();
    b.fn.bindTab = function (m, l, k, j, e) {
        if (!b.isFunction(m)) { e = j; j = k; k = l; l = m; m = b.noop }
        return this.each(function () {
            var p = b(this), o, h = j || "active", d = k || "li", i = e || "rel", n = l || "mouseenter", g = n == "mouseenter", f = function (r) {
                b(p.find("." + h).removeClass(h).attr(i)).hide();
                var q = b(r.addClass(h).attr(i)).show()[0]; m.call(r[0], q)
            };
            p.delegate(d, n, function () {
                var q = b(this); if (q.hasClass(h) || this.disabled) { return }
                if (g) { o && a.clearTimeout(o); o = a.setTimeout(function () { f(q) }, 200) }
                else { f(q) } 
            }); g && p.delegate(d, "mouseleave", function () {
                o && a.clearTimeout(o); o = 0
            }); d == "a" && p.delegate(d, "click", function (q) { q.preventDefault() })
        })
    }; 
    (function (l) {
        if (isNaN(new Date("2013-12-09T08:39:15"))) {
            Date.prototype.toJSON = function () {
                var d = function (e) { return ("0" + e).slice(-2) }; return this.getFullYear() + "/" + d(this.getMonth() + 1) + "/" + d(this.getDate()) + " " + d(this.getHours()) + ":" + d(this.getMinutes()) + ":" + d(this.getSeconds())
            } 
        }
        if (l.JSON) { return }
        var k = { "\b": "\\b", "\t": "\\t", "\n": "\\n", "\f": "\\f", "\r": "\\r", '"': '\\"', "\\": "\\\\" }, m = function (d) {
            if (/["\\\x00-\x1f]/.test(d)) {
                d = d.replace(/["\\\x00-\x1f]/g, function (e) {
                    var o = k[e]; if (o) { return o } o = e.charCodeAt();
                    return "\\u00" + Math.floor(o / 16).toString(16) + (o % 16).toString(16)
                })
            }
            return '"' + d + '"'
        }, g = function (p) {
            var e = ["["], o = p.length, d, q, r; for (q = 0; q < o; q++) {
                r = p[q];
                switch (typeof r) { case "undefined": case "function": case "unknown": break; default: if (d) { e.push(",") } e.push(j(r)); d = 1 } 
            } e.push("]");
            return e.join("")
        }, h = function (d) { return d < 10 ? "0" + d : d }, n = function (d) {
            if (d.toJSON) { return '"' + d.toJSON() + '"' }
            return '"' + d.getUTCFullYear() + "-" + h(d.getUTCMonth() + 1) + "-" + h(d.getUTCDate()) + "T" + h(d.getUTCHours()) + ":" + h(d.getUTCMinutes()) + ":" + h(d.getUTCSeconds()) + '"'
        }, i = Object.prototype.hasOwnProperty, f = function (o) {
            var e = ["{"], d, p;
            for (var q in o) {
                if (i.call(o, q)) {
                    p = o[q];
                    switch (typeof p) {
                        case "undefined":
                        case "unknown": 
                        case "function": break;
                        default: d && e.push(","); d = 1; e.push(j(q) + ":" + j(p))
                    } 
                }
            } 
            e.push("}"); return e.join("")
        }, j = function (d) {
            switch (typeof d) {
                case "unknown": case "function": case "undefined": return; case "number": return isFinite(d) ? String(d) : "null";
                case "string": return m(d); case "boolean": return String(d); default: return d === null ? "null" : d instanceof Array ? g(d) : d instanceof Date ? n(d) : f(d)
            } 
        };
        l.JSON = { parse: function (d) {
            d = d.replace(/("|')\\?\/Date\((-?[0-9+]+)\)\\?\/\1/g, "new Date($2)");
            return (new Function("return " + d))()
        }
        , stringify: function (d) { return j(d) } 
    }
})(a); 
        (function (j) {
        var f, i = function () { }, d = j.document, e = { set: i, get: i, remove: i, clear: i, each: i, obj: i };
         (function () {
             if ("localStorage" in j) {
                 try { f = j.localStorage; return } catch (s) { } 
             }
             var k = d.getElementsByTagName("head")[0], h = j.location.hostname || "localStorage", t = new Date(), v, p;
             if (!k.addBehavior) {
                 try { f = j.localStorage } catch (s) { f = null } return
             }
             try {
                 p = new ActiveXObject("htmlfile"); p.open();
                 p.write('<script>document.w=window;<\/script><iframe src="/favicon.ico"></frame>');
                 p.close(); v = p.w.frames[0].document; k = v.createElement("head"); v.appendChild(k)
             }
             catch (s) { k = d.getElementsByTagName("head")[0] }
             try {
                 t.setDate(t.getDate() + 36500);
                 k.addBehavior("#default#userData");
                 k.expires = t.toUTCString(); k.load(h); k.save(h)
             } catch (s) { return } 
             var u, w;
             try { u = k.XMLDocument.documentElement; w = u.attributes }
             catch (s) { return }
             var r = "p__hack_", m = "m-_-c", n = new RegExp("^" + r), l = new RegExp(m, "g")
             , q = function (o) {
                 return encodeURIComponent(r + o).replace(/%/g, m)
             }
     , g = function (o) {
         return decodeURIComponent(o.replace(l, "%")).replace(n, "")
      };
     f = { length: w.length, isVirtualObject: true
     , getItem: function (o) { return (w.getNamedItem(q(o)) || { nodeValue: null }).nodeValue || u.getAttribute(q(o)) }, setItem: function (x, o) {
         try { u.setAttribute(q(x), o); k.save(h); this.length = w.length } catch (y) { } 
     }, removeItem: function (o) {
         try {
             u.removeAttribute(q(o)); k.save(h);
             this.length = w.length
         } catch (x) { } 
     }, clear: function () { while (w.length) { this.removeItem(w[0].nodeName) } this.length = 0 }
      , key: function (o) { return w[o] ? g(w[o].nodeName) : c } 
 };
 if (!("localStorage" in j)) { j.localStorage = f } 
})(); j.LS = !f ? e : { set: function (h, g) {
    if (this.get(h) !== c) { this.remove(h) } f.setItem(h, g)
}, get: function (g) {
    var h = f.getItem(g); return h === null ? c : h
}, remove: function (g) { f.removeItem(g) }, clear: function () { f.clear() }, each: function (h) {
    var g = this.obj(), l = h || function () { }, k; for (k in g) {
        if (l.call(this, k, this.get(k)) === false) { break } 
    } 
}, obj: function () {
    var g = {}, k = 0, l, h; if (f.isVirtualObject) { g = f.key(-1) } else { l = f.length; for (; k < l; k++) { h = f.key(k); g[h] = this.get(h) } } return g
} 
};
if (j.jQuery) { j.jQuery.LS = j.LS }
})(a); 
b.hash = function (m, l) {
    if (typeof m === "string" && l === c) { return b.getHashPara(m) }
    var j = a.location.hash.replace(/^#*/, "").split("&"), o = {}, e = j.length, h = 0, p, g = {}, d = {}, f, q;
    for (; h < e; h++) {
        p = j[h].split("="); if (p.length == 2 && p[0].length) {
            q = decodeURIComponent(p[0]); f = q.toLowerCase();
            if (!d[f]) { g[q] = decodeURIComponent(p[1]); d[f] = q } 
        } 
    } if (m === c) { return g } if (b.isPlainObject(m)) { o = m } else { o[m] = l } for (q in o) {
        l = o[q];
        f = q.toLowerCase(); d[f] && g[d[f]] !== c && delete g[d[f]];
        if (l !== null) { d[f] = q; g[q] = String(l) }
    } 
    j.length = 0;
    for (q in g) {
        j.push(encodeURIComponent(q) + "=" + encodeURIComponent(g[q]))
    }
    a.location.hash = "#" + j.join("&")
};
b.cookie = function (g, f, l) {
    if (arguments.length > 1 && (f === null || typeof f !== "object")) {
        l = b.extend({}, l);
        if (f === null) { l.expires = -1 } if (typeof l.expires === "number") {
            var k = l.expires, h = l.expires = new Date(); h.setDate(h.getDate() + k)
        }
        return (document.cookie = [encodeURIComponent(g), "=", l.raw ? String(f) : encodeURIComponent(String(f)), l.expires ? "; expires=" + l.expires.toUTCString() : "", l.path ? "; path=" + l.path : "", l.domain ? "; domain=" + l.domain : "", l.secure ? "; secure" : ""].join(""))
    } l = f || {};
         var j, i = l.raw ? function (d) { return d } : decodeURIComponent; return (j = new RegExp("(?:^|; )" + encodeURIComponent(g) + "=([^;]*)").exec(document.cookie)) ? i(j[1]) : null }; (function () {
            var f = "163.com", j = /\.163\.com$/i, n = function (m) { var p = (m + "").toLowerCase(), o = p.indexOf("http"); return o < 0 ? j.test(p) ? p : "" : o ? "" : p.replace(/^https?:\/\//, "").replace(/\/.+$/, "") }, i = {}, e = function (u, t) { var s = n(u), w = a.location.host + "", p = i[s], m = u.replace(/\/$/g, "") + "/agent/ajaxAgent.htm?v=" + (+new Date()); if (m.indexOf("http") < 0) { m = "http://" + m } if (!s || s == w) { t(b); return } if (p) { try { var v = p._loading } catch (r) { p = null } } if (p && p._loading) { p.push(t); return } if (p && !p._loading) { t(p); return } if (!document.body) { a.setTimeout(function () { e(u, t) }, 1); return } i[s] = [t]; i[s]._loading = true; var q = a, o = b("<iframe scrolling='no' frameborder='0' width='0' height='0'/>").appendTo(document.body).bind("load", function () { var x = i[s], C = x.length, z = 0, A, y; try { A = o[0].contentWindow.jQuery; y = o[0].contentWindow.document.domain; if (y != document.domain) { throw "" } } catch (B) { e(u, t); return } i[s] = A; if (A) { for (; z < C; z++) { x[z] && x[z](A) } } else { a.console && a.console.log("跨域代理文件错误！", m) } }).attr("src", m) }
        , g = function (m) {
            m = m.replace(/("|')\\?\/Date\((-?[0-9+]+)\)\\?\/\1/g, "new Date($2)"); 
        return (new Function("return " + m))() }, h = {}, k = function (v, t, s, r, q) {
            var p = a.location.host + "", o = n(t) || p, w = "http:", m = "80", u;
            if (/^(https?:)/i.test(t)) {
                w = RegExp.$1.toLowerCase();
                if (/:(\d+)/i.test(t)) { m = RegExp.$1 || "80" }
            } else {
                w = a.location.protocol; m = a.location.port || "80" 
            }
            if (a.location.protocol != w || (a.location.port || "80") != m) {
                u = b.isFunction(r) ? r : b.isFunction(s) ? s : b.noop; u.call(Core, 2, "", "protocols or ports not match"); return
            } 
            if (j.test(o) && j.test(p) && o.indexOf(document.domain) >= 0 && w == "http:") {
                e(o, function (x) { l(x, v, t, s, r, q) })
            } 
            else {
                l(jQuery, v, t.replace(/https?:\/\/[^\/]+/, ""), s, r, q) 
            }
        }, d = {}, l = function (x, v, u, s, r, p) {
            var o = b.isFunction(r) ? r : b.noop, y = u, C, m, z = Core, q = false, t = (y.indexOf("?") + 1 ? "&" : "?") + "cache=" + (+new Date()), B, A;
            if (b.isFunction(s)) { o = s; s = null; p = r }
            if (p && p.indexOf("*") == 0) { q = true; p = p.substr(1) }
            if (p) {
                if (p.indexOf("!") === 0) {
                    p = p.substr(1);
                    if (d[p]) { d[p].push(o); return } d[p] = []; r = o;
                    o = function () {
                        var E = arguments, D = this; r.apply(D, E); b.each(d[p], function (G, F) { F.apply(D, E) });
                        delete d[p]
                    }
                } 
                C = h[p]; 
                if (C) {
                    if (p.indexOf("@") !== 0) { return }
                    m = C.readyState; 
                    if (m > 0 && m < 5) {
                        try {
                            C.aborted = true
                        } catch (w) { } 
                        C.abort()
                    } 
                }
            }
            B = v.split(".");
            A = B.length > 1 ? B[1] : "";
            C = x.ajax({ url: y + (q ? "" : t), type: B[0], data: s, success: function (E, D, G) {
                delete h[p];
                if (G.aborted) { return } E = G.responseText;
                if (E == c || E == null || E == "" || E.indexOf("<!DOCTYPE") >= 0) {
                    if (A == "JSON") {
                        try { E = g(E) } catch (F) {
                            o.call(z, 3, G.responseText, D); return
                        }
                    } o.call(z, 1, E, D); return
                } 
                o.call(z, 0, E, D) 
             }, error: function (E, D) {
                delete h[p]; if (!D || D == "error") {
                    o.call(z, 1, "", D); return 
        } if (E.aborted) { return } o.call(z, 1, E.responseText, D) } }); p && (h[p] = C) }; b.extend({ get2: function (o, m, q, p) { k("GET", o, m, q, p); return this }, post2: function (o, m, q, p) { k("POST", o, m, q, p); return this }, getJSON2: function (o, m, q, p) { k("GET.JSON", o, m, q, p); return this }, postJSON2: function (o, m, q, p) { k("POST.JSON", o, m, q, p); return this } })
})();
b.bindModule = function (h, f, e) {
    if (typeof f != "object") { e = f; f = h; h = 0 }
    var g = h || this; 
    b.each(f || {}, function (d, i) {
        i && i.js && b.each(d.split(" "), function (o, m) {
            if (g[m]) {
                return
            }
            var q = [], p = [];
            var n = g[m] = function () {
                var k = arguments; q.push(this);
                p.push(k);
                if (n.autoLoaded == 1) { return }
                n.autoLoaded = 1;
                var j = a.setTimeout(function () { n.autoLoaded = 0 }, 1000); i.css && b.loadCss(i.css, e);
                b.loadJS(i.js, function () {
                    j && a.clearTimeout(j);
                    if (g[m] === n) {
                        a.console && a.console.log("方法" + m + "在" + i.js + "中未被定义！自动加载模块处理失败！"); g[m] = b.noop; return
                    }
                    for (var l = p.length, r = 0; r < l; r++) { g[m].apply(q[r], p[r]) } p.length = 0
                }, e)
            } 
        })
    }); 
    return this
}; 
(function () {
var d = {}, f = function (x, B, w, v, A) {
    var r = B.toLowerCase().replace(/#.*$/, "").replace("/?.*$/", ""), q, u, t = b.isFunction, C = d[r] || [], s = (w || b.noop)(B), y = a.CollectGarbage || b.noop;
    if (s === true) {
        t(v) && v();
        return
    } d[r] = C; 
    if (!C || !C.loaded || s == false) {
        t(v) && C.push(v); 
        C.loaded = 1;
        q = document.createElement(x), u = document.getElementsByTagName("head")[0] || document.documentElement; 
        B = B + (B.indexOf("?") >= 0 ? "&" : "?") + Core.version;
        if (x == "link") {
            q.rel = "stylesheet"; q.type = "text/css"; q.media = "screen"; q.charset = A || "UTF-8"; q.href = B
        } 
        else {
            q.type = "text/javascript"; q.charset = A || "UTF-8"; var z = false;
            q.onload = q.onreadystatechange = function () {
                if (!z && (!this.readyState || { loaded: 1, complete: 1}[this.readyState])) {
                    z = true; q.onload = q.onreadystatechange = null; this.parentNode.removeChild(this);
                    var i = d[r], g = i.length, h = 0; i.loaded = 2;
                    for (; h < g; h++) {
                        t(i[h]) && i[h]()
                    }
                    i.length = 0;
                    i = u = q = null; 
                    y()
                }
            }; 
            q.src = B
        } 
        u.appendChild(q, u.lastChild)
 } 
 else {
     if (C.loaded == 2) {
         t(v) && v(); C = null; y()
     } else {
         t(v) && C.push(v); C = null; y() 
     } 
 } 
}, e = function (g, h) {
    if (!h) { return g }
    return /^http/i.test(g) ? g : (h.replace(/\/*$/, "") + (g.indexOf("/") == 0 ? "" : "/") + g)
};
         b.extend({ loadJS: function (p, o, k, l, n) {
             if (!b.isFunction(k)) { n = l; l = k; k = o; o = null }
             if (!b.isFunction(k)) { n = l; l = k; k = null }
             if (/^http/i.test(l)) { n = l; l = "" } 
             if (b.isArray(p)) {
                var m = p.length, q = function (g) {
                    if (g < m) {
                        f("script", e(p[g], n), o, function () { q(g + 1) }, l) 
                    }
                    else {
                        b.isFunction(k) && k() 
                    }
                }; 
                q(0)
            } 
            else {
                f("script", e(p, n), o, k, l)
            } 
            return this
        }
    , loadCss: function (k, i) {
        if (b.isArray(k)) {
            var h = k.length, j = 0;
            for (; j < h; j++) { f("link", e(k[j], i)) }
        } else { f("link", e(k, i)) } return this 
    } 
    })
})(); (function () {
    var e = b("<div>"), d = Array.prototype.slice;
    b.extend({ sendMsg: function (f) { return e.triggerHandler("msg." + f, d.call(arguments, 1)) }
     , bindMsg: function (j, k, i, l) {
         if (!j || !b.isFunction(k)) { return this }
         var h = function (f) { return k.apply(i || a, d.call(arguments, 1)) }; if (k.guid) { h.guid = k.guid } e[l ? "one" : "bind"]("msg." + j, h); if (!k.guid) { k.guid = h.guid } return this
     }
     , bindMsgOnce: function (g, h, f) { return this.bindMsg(g, h, f, 1) }
     , unbindMsg: function (f, g) { if (!f || !b.isFunction(g)) { return this } e.unbind("msg." + f, g); return this } }) })()
})(window, jQuery);

var Core = (function (c, d, h) {
    var b = document.domain; if (/.*\.caipiao\.163\.com$/i.test(b) && b.indexOf("bbs") < 0) {
        try { document.domain = "caipiao.163.com" } catch (f) { } 
    }
    var a = { connectTime: c.performance && c.performance.timing ? c.performance.timing.connectStart || 0 : 0, serverInitTime: +new Date(), localInitTime: +new Date()
        , getServerTime: function () {
            var e = this.localInitTime - this.connectTime, i = this.serverInitTime + (+new Date()) - this.localInitTime; return new Date(this.connectTime > 0 && e > 0 ? i + e : i)
        } 
    };
var g = { countLoadingTime: d.noop, version: "2.0"
    , serverTime: function () { return a.getServerTime() }
    , now: function () { return (new Date).getTime() }
    , GC: c.CollectGarbage || d.noop, log: d.getPara("debugger") && c.console ? c.console.log || d.noop : d.noop
    , infoCdnUrl: "http://pimg1"
    , cdnUrl: "http://cdn"
    , navConfig: { appName: "XX彩票", appID: "caipiao", regUrl: (function () {
        var e = document.URL, j, i = encodeURIComponent
        ,k = function (m, l) {
            var n = (m + "").indexOf("?") + 1 ? "&" : "?"; return m + n + l
        };
        e = e.indexOf("from=") + 1 ? e : k(e, "from=reg"); 
        j = k(e, "isShowLogin=1");
        return "http://reg" + i("http://caipiao" + i(e)) + "&loginurl=" + i(j)
    })() 
    }
, navInit: function (e, k, j, i, l) {
    this.configInit && this.configInit(e, k, j, i);
    this.navConfig.welcomeUser = '{time}好，{funcEntry}<span id="mailInfoHolder"></span>，欢迎来到{appName}！{logoutLink}';
    if (this.userPage || this.privatePage) {
        this.navConfig.logoutUrl = null;
    }
    this.easyNav.init(this.navConfig, l);
    delete this.navInit
}
, configInit: function (e, k, j, i) {
    k = d.cookie("S_INFO") ? easyNav.account : "";
    this.easyNav = c.easyNav;
    this.cdnUrl = e;
    this.version = j || this.version;
    this.navConfig.loginUrl = this.navConfig.loginUrl || "javascript:easyNav.login()";
    this.navConfig.loginJsPath = e + "/js2";
    this.navConfig.sessionId = k || "";
    if (i) { a.serverInitTime = +i }
    d.bindModule({
        dialog: { js: "js2/dialog.js" },
        scrollWhenNeed: { js: "js2/easyTools/scroll.js" },
        easyEvents: { js: "js2/easyTools/event.js" }
    }, this.cdnUrl);
    d.bindModule(d.fn, { "disableSelection enableSelection disableRightClick enableRightClick disableIME enableIME setControlEffect": { js: "js2/easyTools/tools.js" },
        bindDrag: { js: "js2/easyTools/drag.js" }, "scrollGrid xScrollGrid": { js: "js2/easyTools/scroll.js" }, easyEvents: { js: "js2/easyTools/event.js" }
    },
    this.cdnUrl);
    this.bindModule({ insertMobDownFix: { js: "js2/phoneclient/mobileCom.js", css: "css2/phoneclient/mobileCom.css" }, "shareAction shareGet sharePost": { js: "js2/shareAction.js"} });
    c.mailInfoConf = { infoTmpl: "( {0})", holderId: "mailInfoHolder" };
    this.loadGolbalConfig();
    this.popularizeNavConfig();
    delete this.configInit
}
, popularizeNavConfig: function () {
    var k = function (m, l) {
        var n = (m + "").indexOf("?") + 1 ? "&" : "?";
        return m + n + l
    };
    if (d.cookie("caipiao_tcy")) {
        var i = d.cookie("caipiao_tcy"); i = i.split("|");
        var e = document.URL; e = e.indexOf("from=") + 1 ? e : k(document.URL, "from=reg");
        var j = k(e, "isShowLogin=1");
        this.navConfig.regUrl = "http://reg" + encodeURIComponent(e) + "&loginurl=" + encodeURIComponent(j) + "&username_r=" + (i[0] || "");
        if (i[1]) {
            this.navConfig.loginConfig = this.navConfig.loginConfig || {};
            this.navConfig.loginConfig.initUserName = i[1]
        }
    }
}
, loadGolbalConfig: function () {
    c.gameActConf ? d.sendMsg("globalConfig") : this.loadJS("http://caipiao?" + (+new Date()), function () { d.sendMsg("globalConfig") })
}
, bindModule: function (e) { d.bindModule(this, e, this.cdnUrl); return this }
, gameAct: { is: function (r, m, q) {
    if (!g.gameActConfig) {
        g.loadCdnJS("js2/config.js", function () {
            g.gameAct.is(r, m, q)
        }); return
    }
    var l = g.gameActConfig[r], i = g.serverTime(), p = false, e, k = 0, o = "", n;
    if (d.isFunction(m)) { q = m; m = "" }
    if (l) {
        n = d.isFunction(q) ? function (s) { q.call(l, s) } : d.noop; e = d.isArray(l.type);
        if (l.range) {
            if (l.range == "server") {
                var j = function () {
                    var t = c.gameActConf, s = false;
                    if (!t) { return }
                    if (t[r]) { s = m ? e ? l.type[t[r] - 1] == m : l.type == m : e ? l.type[t[r] - 1] : l.type } return s
                };
                if (c.gameActConf) { p = j() } else { d.isFunction(n) && d.bindMsgOnce("globalConfig", function () { n(j()) }); return } d.isFunction(n) && n(p);
                return p
            }
            d.each(l.range, function (x, v) {
                var y, z, w, u;
                if (v.indexOf("-") + 1) {
                    u = v.split("-");
                    if (u[1].indexOf(":") < 0) {
                        u[1] += " 23:59:59"
                    }
                    y = new Date(u[0]);
                    z = new Date(u[1])
                } else {
                    w = new Date(v);
                    y = new Date(w.getFullYear(), w.getMonth(), w.getDate());
                    z = new Date(w.getFullYear(), w.getMonth(), w.getDate() + 1)
                }
                if (i >= y && i <= z) {
                    p = true; k = x; if (e && x >= l.type.length) { k = 0 } return false
                }
            });
            o = e ? l.type[k] : l.type
        } else { p = i <= l.end && (l.start ? i >= l.start : true); o = e ? l.type[0] : l.type } p = m ? (p && (o == m)) : p ? o : ""
    } d.isFunction(n) && n(p)
}
, list: function (i) {
    if (!d.isFunction(i)) { return }
    var j = this, k = function (l) { j.is(l, function (m) { m && i.call(this, l, m) }) };
    if (!g.gameActConfig) {
        g.loadCdnJS("js2/config.js", function () {
            for (var l in g.gameActConfig) { k(l) }
        })
    } else { for (var e in g.gameActConfig) { k(e) } }
}
}
, fastInit: function (i) {
    if (this.eventWrap && this.events && d.fn.easyEvents) { d(this.eventWrap).easyEvents(this.events, this) }
    this.navInit && this.navInit(this.cdnUrl, "", +new Date(), "", true); try {
        this.initMyCoupon && this.initMyCoupon();
        this.updateLogo().initLotteryList().dealCPPara()
    } catch (j) { }
    g.bindModule({ popularizeConfigForBetPage: { js: "js2/lottery/popularizeConfigForBetPage.js"} });
    g.bindModule({ mobileDialogInit: { js: "js2/mobileDialogInit.js"} });
    g.bindModule({ buyRedBag: { js: "js2/coupon/buyRedBagCom.js"} });
    i && this.quickInit && this.quickInit();
    delete this.fastInit
}
, dealCPPara: function () {
    var o = c.document, m = o.getElementsByTagName("a"), p = m.length, l = 0, e, j, k; for (; l < p; l++) {
        j = m[l]; e = j.getAttribute("cppara") || "";
        if (e) { k = j.innerHTML; j.setAttribute("href", j.getAttribute("href") + "?" + e); j.removeAttribute("cppara"); j.innerHTML = k }
    } return this
}
, virualViewStat: function (e, i) { if (!e) { return } neteaseTracker(true, e, i); return this }
, updateLogo: function () {
    var e = function () {
        var k = c.logoConf, j, l, i; if (k) {
            l = d("#docHead");
            j = l.attr("rel") || "white"; i = k.img ? k : k[j]; if (i && i.img) {
                l.css("background-image", "url(" + i.img + ")");
                d("#docHeadWrap .guideLnk").attr("href", i.url).attr("title", i.title || "")[i.url ? "show" : "hide"]()
            }
        }
    };
    if (c.logoConf) { e() } else { d.bindMsgOnce("globalConfig", e) } return this
}
, checkAd: function () {
    if (this.is4050x || c.self !== c.top) { return }
    var m = this, k = function () {
        var q = c.adConfig, o, p = m.pageId4Ad, n; if (q && p) {
            d.each(q, function (r, s) {
                if (s.id && s.png && s.url) {
                    s.page = +(s.page || 3);
                    if (s.page & p) { m.showPngAd(s) }
                }
            })
        } i()
    }, j = function () {
        var n = JSON.parse(LS.get("seoAdCache") || "null");
        if (n) { n.zoom = 0.5; m.showPngAd(n, "seo") }
    };
    this.bindModule({ showPngAd: { js: "js2/popularize/hello.js" }, fireSEO: { js: "js2/popularize/SSEEOO.js"} });
    var e = d.getUrlPara("referered") || document.referrer, i = d.noop, l = (e.replace(/^https?:\/\//g, "").replace(/\/.*/, "")).toLowerCase();
    if (/reg\.163\.com/i.test(l) || /reg\.youdao\.com/i.test(l)) { l = "" }
    if (l && !/caipiao\.163\.com$/.test(l) && !/cp\.163\.com$/.test(l) && !this.is4050x && c.top == c.self) {
        i = function () {
            m.fireSEO(l, e, function (o) {
                if (o.length == 0) { return } o.length = 1; var p, n = o.length; d.each(o, function (q, r) {
                    m.showPngAd(r, "seo", function (s) {
                        if (s) { LS.remove("seoAdCache"); if (s == 2 && r.mode == 5) { p = JSON.stringify(r) } } n--; if (n == 0) {
                            if (p) { LS.set("seoAdCache", p) } else { j() }
                        }
                    })
                })
            })
        }
    }
    else { j() }
    if (c.adConfig) { k() }
    else { d.bindMsgOnce("globalConfig", k) } return this
}
, repaint: function () {
    g.loadCdnJS("js2/popularize/mail.js", d.falseFn, d.noop); g.getEpayInfo(); if (d.cookie("COOKIEYIMAFROM") !== null && d.cookie("COOKIEYIMACPC") !== null) {
        g.loadCdnJS("js2/popularize/entry.js")
    }
}, init: function () {
    this.fastInit && this.fastInit(1); d("#showmore").click(function () { d("#hot_block").css("height", "auto") }); d("#friendlyLink .more a").click(function () {
        d("#friendlyLink .hide").show(); d("#friendlyLink .more").hide()
    });
    d(document).delegate("a[href*=javascript:;],a[href*=javascript:void(]", "click", d.preventDft);
    this.easyNav.onLogin(this.repaint); this.checkAd(); this.myInit(); this.initNav();
    g.bindModule({ sendPopularizeOrder: { js: "js2/popularize/entry.js"} }); if (d.cookie("COOKIEYIMAFROM") !== null && d.cookie("COOKIEYIMACPC") !== null) {
        //g.loadCdnJS("js2/popularize/entry.js")
    } 
    //this.loadCdnJS("js2/jtip.js", function () { return !!d.jtip }, function () { d.jtip(".jtip") });
    var i = d(".tgTextWrap"); i[0] && i.scrollGrid({ perScroll: 1, interval: 3000 });
    var e = d(".scrollWrap"); e[0] && e.xScrollGrid(); this.is4050x || this.hideSysMessage || (c.top == c.self && /^caipiao\.163\.com$/i.test(c.location.host) && this.loadCdnJS("js2/userCenter/userMessage.js"));
    delete this.init;
    delete this.myInit;
    this.quickInit && delete this.quickInit; 
    this.GC()
}
, emptySendHttp: function (i) {
    var k = "imgLoad_" + (+new Date()) + parseInt(Math.random() * 100), e, j; e = c[k] = new Image(); e.onload = function () { c[k] = null; _ntes_void() };
    e.onerror = function () { c[k] = null }; i = i.replace(/#\S*$/, ""); j = (i + "").indexOf("?") + 1 ? "&" : "?"; e.src = i + j + "d=" + (+new Date()); e = null
}
, zclip: c.clipboardData ? function (i, e, j) {
    d(i).click(function () {
        var l = true, k = d.isFunction(e) ? e() : String(e);
        try { c.clipboardData.clearData(); c.clipboardData.setData("Text", k) } catch (m) { l = false } j && j(l)
    })
} : function (k, e, l) {
    var j = d(k), i = this.cdnUrl + "/swf/ZeroClipboard20130715.swf"; this.loadCdnJS("Content/js2/zClip.js", function () { return !!d.fn.zclip }, function () { j.zclip({ path: i, copy: e, afterCopy: l }) })
}
, insertFlash: function (i, e, j) {
    e.path = e.path || this.cdnUrl + "/swf/" + e.swf; this.loadCdnJS("js2/flash.js", function () { return !!d.easyFlash }, function () {
        var k = d.isFunction(j) ? j(d.easyFlash.support, d.easyFlash.version) : null; if (k !== false) { d.easyFlash.insert(i, e) }
    })
}, loadGame: function (e, j) {
    var i = typeof e == "object"; this.loadCdnJS("js2/game/game.js", function () {
        return !!c.Game && !!c.Game.loadMolude
    }, function () {
        var l = c.Game, m; g.Game = g.G = l; if (i) { m = e } else { m = {}; m[e] = j || d.noop }
        for (var k in m) {
            //l.loadMolude(k, m[k] || d.noop) 
        }
    }); return this
}, getTimeDescription: function (i) {
    var k = +i > 0 ? +i : 0, m = 60000, l = 60 * m, j = 24 * l, e = function (o) {
        return ("0" + Math.floor(o)).slice(-2)
    }; return [e(k / j), e(k % j / l), e(k % j % l / m), e(k % j % l % m / 1000)]
}
, scrollWhenNeed: function (i, e) { d.scrollWhenNeed(i, e) }
, formatTime: function (i, e) { return d.formatTime(i, e) }
, initMyCoupon: function () {
    var j = d("#myCoupon .couponContent"), i, k = function (q) {
        var w, u, m = '<dl><dt><span class="couponName">名称</span><span class="couponMoney">可用金额(元)</span><span class="couponRange">适用范围</span></dt>{0}</dl>'
        , r = '<dd class="{ddClass}"><span class="couponName">{couponName}</span><span class="couponMoney">{remainAmount}</span><span class="couponRange" title="{ruleDesc}">{ruleDesc}</span></dd>'
        , t = '<a target="_blank" href="http://caipiao" class="seeCoupon">查看全部<i>{couponNum}</i>个红包</a>',
        o = [], v = [], p = 0, n;
        try {
            if (typeof q == "string") {
                q = this.parseJSON(q)
            }
            u = q.accountId;
            if (u && u != c.easyNav.sessionId) { easyNav.setSessionId(u) }
            if (q && q.couponList) {
                w = q.couponList; n = w.length;
                if (n > 0) {
                    for (; p < n; p++) {
                        w[p].couponName = w[p].couponName || ""; w[p].remainAmount = w[p].remainAmount || ""; w[p].ruleDesc = w[p].ruleDesc || "";
                        if (p == n - 1) {
                            w[p].ddClass = "last"
                        } else {
                            w[p].ddClass = ""
                        } v.push(d.format(r, w[p]))
                    }
                    o.push(d.format(m, v.join("")));
                    if (q.couponNum && q.couponNum > 3) {
                        o.push(d.format(t, q))
                    } j.html(o.join(""))
                } else {
                    j.html('<div class="tip"><i class="iconHappy"></i>您当前没有可以使用的红包</div>')
                }
            }
            else {
                if (q.status == -1) {
                    j.html('<div class="tip"><i class="iconHappy"></i>您没有登录，请<a href="javascript:easyNav.login();">登录</a>后查看</div>')
                } else {
                    if (q.status == -2) {
                        j.html('<div class="tip"><i class="iconHappy"></i>您当前没有可以使用的红包</div>')
                    } else {
                        j.html('<div class="tip"><i class="iconSad"></i>获取失败，请稍后再试</div>')
                    }
                }
            }
            c.myCouponCallBack = null; delete k
        }
        catch (s) {
            c.myCouponCallBack = null;
            j.html('<div class="tip"><i class="iconSad"></i>获取失败，请稍后再试</div>')
        }
    }
    , e = function () {
        j.html('<div class="tip"><i class="iconLoading"></i>努力加载中</div>');
        c.myCouponCallBack = k; d.ajax({ url: "http://caipiao"
        , cache: false
        , complete: function () {
            if (c.myCouponCallBack) { c.myCouponCallBack() }
        }
        , context: c.Core
        , dataType: "jsonp"
        , jsonp: "callBackName"
        , jsonpCallback: "myCouponCallBack", timeout: 5000
        })
    };
    d("#myCoupon").delegate(".topNavHolder", "mouseenter", function () {
        i = c.setTimeout(function () { e() }, 100)
    }).delegate(".topNavHolder", "mouseleave", function () {
        i && c.clearTimeout(i)
    });
    return this
}
, initNav: function () {
    var e = function () {
        var i = d(this); if (!!i.attr("user") && !easyNav.isLogin()) {
            g.easyNav.login(this.href); f.preventDefault(); return
        }
    };
    d(".mcDropMenuBox").each(function () {
        g.easyNav.bindDropMenu(this, d(".mcDropMenu", this)[0], "mouseover", "dropMenuBoxActive", e, 200); d(".topNavHolder", this).click(e)
    });
    if (d.getUrlPara("isShowLogin") && !this.easyNav.isLogin(true)) { this.easyNav.login() } d(".wordsNum2,.wordsNum4", "#funcTab").each(function () {
        g.easyNav.bindDropMenu(this, d(".mcDropMenu", this)[0], "mouseover", "hover", d.noop, 200)
    });
    d("#funcTab a").not("#lotteryListEntry a").removeAttr("title");
    delete this.initNav; return this
}
, initLotteryList: function () {
    var n = d("#lotteryListEntry"),
    o = d("#lotteryList"),
    j = d("#funcTab"),
    k = d(".lotteryListWrap", o),
    m = false, l, q = function (r) {
        r.prepend('<iframe class="iFrameGround" frameborder="0"></iframe>');
        r.find(".iFrameGround:first").width(r.outerWidth()).height(r.outerHeight())
    },
    i = function (r) {
        if (!d.isIE678) { o.show(); k.stop().animate({ height: l }, 300, function () { m = true }) }
    },
    e = function (r) {
        if (!d.isIE678) {
            if (r && r.type === "click") {
                n.addClass("open");
                return
            }
            k.stop().animate({ height: 0 }, 300, function () { o.hide(); m = false })
        }
    };
    this.dealKuai2(); k.delegate("a[href]", "click", function (r) {
        var s; s = this.getAttribute("href", 2); s = /^http/.test(s) ? s : (location.protocol + "//" + location.host + s);
        if (d(this).attr("target") !== "_blank" && s.split("#")[0] == location.href.split("#")[0]) {
            location.href = s; location.reload(); r.preventDefault()
        }
    });
    if (o[0]) {
        function p() {
            var y = c.lotteryListConf, r, z, s, x, A, w, B, t = ["", "11选5", "时时", "低频"], u = { jclq_mix_p: "jclq" }, v; if (y) {
                r = y.top; z = y.gp; s = y.jj; x = y.sz; v = d.map([r, z, s, x], function (D, C) {
                    if (C === 0) {
                        A = '<li class="zyGame {className}"><a gid="{gameEn}" href="{url}#from=leftnav"><em class="cz_logo35 logo35_{logoName}"></em><strong>{gameCn}</strong>{grayHTML}{redHTML}</a></li>'; w = ""; B = ""
                    } else {
                        A = '<em class="{className}"><a gid="{gameEn}" title="{title}" href="{url}#from=leftnav">{gameCn}{redHTML}</a></em>'; w = '<li class="otherGames clearfix ' + (C === 3 ? "end" : "") + '"><h3>' + t[C] + "</h3><div>"; B = "</div></li>"
                    }
                    return w + d.map(D, function (G, E) {
                        var F = ""; if (C === 0) {
                            if (G.isTJ) { F = "tjGame" }
                        } else {
                            if (E % 2 === 0) {
                                F = "left"
                            }
                        }
                        return d.format(A, d.extend({}
       , G, { url: G.url.replace(/\#from\=.*/, "")
       , className: F, grayHTML: G.gray ? '<span class="grayWords">' + G.gray + "</span>" : ""
       , redHTML: G.red ? '<span class="redWords"><i class="arrowsIcon"></i>' + G.red + "</span>" : ""
       , logoName: u[G.gameEn] || G.gameEn
       }))
                    }).join("") + B
                }).join(""); k.find("ul").html(v); k.trigger("contentChange")
            } 
        }
        if (c.lotteryListConf) {
            p()
        }
        else {
            d.bindMsgOnce("globalConfig", p)
        }
        this.gameAct.list(function (r, t) {
            var s = "cz_" + t; j.find("[pid=" + r + "]").each(function () {
                d(this).find("a").prepend("<span class='" + s + "'></span>")
            })
        })
    }
    if (n[0] && o[0] && d.contains(n[0], o[0])) {
        this.easyNav.bindDropMenu(n[0], o[0], "mouseover", "open", d.noop, 200, i, e);
        if (d.isIE678) { q(o) } else {
            l = k.outerHeight(); o.hide().css({ left: 0 }); k.height(0);
            k.bind("contentChange", function () {
                if (!k.is(":animated")) {
                    if (!m) { o.css({ left: -9999 }).show() }
                    k.height("auto"); l = k.outerHeight();
                    if (!m) {
                        o.hide().css({ left: 0 }); k.height(0)
                    }
                }
            })
        }
    }
    delete this.initLotteryList;
    return this
}
, dealKuai2: function () {
    var e = d("#lotteryList")
    , i = function (k) {
        var l = location.host.toLowerCase()
        , j = l == "caipiao"
        , m = l == "zx.caipiao.xx.com";
        if ((k == 1 && g.pageId4Ad == 1) || k == 2) {
            e.find("[gid=kuai2]").parent().remove();
            j && g.pageId4Ad == 1 && d(".chartList a[href*=/gdkuai2/]").parent().remove()
        }
        if (k == 2) {
            if (j) {
                d("#docBody").find(".lately,.allList").find("[gid=kuai2]").closest("li").remove();
                d(".main .awardList a[href*=/gdkuai2/]").closest("tr").remove();
                d(".index_help_right .icon_kuai2").closest("li").remove();
                d("#siderbar").find("[href*=/99KJ821500754IHE.html]").closest("li").remove()
            }
            if (m) {
                d(".zx_nav,.quick_in").find("a[href*=/gdkuai2/]").remove();
                d("#kuai2").remove()
            }
        }
    };
    if (c.kuai2Display) { i(c.kuai2Display) } else {
        d.bindMsgOnce("globalConfig", function () {
            i(c.kuai2Display)
        })
    }
    delete this.dealKuai2
}
, getEpayInfo: function () {
    if (!d("#myEpay")[0] || this.getEpayInfo.lock || this.isStatic) { return this }
    this.getEpayInfo.lock = 1; c.setTimeout(function () { g.getEpayInfo.lock = 0 }, 1000);
    var j = this.easyNav.isLogin(true)
    , k = encodeURIComponent
    , i = easyNav.sessionId || easyNav.account
    , e = {
        charge: "https://epay" + k(document.URL),
        center: "https://epay",
        active: "https://epay.xx.com/servlet/controller?operation=activateAccount&method=activateView&sourceOp=main&platformReturnUrl={0}"
    };
    j && this.get("http://caipiao.xx.com/epayAccountInfo.html"
    , function (u, p) {
        if (u || !p) { return } var o = this.parseJSON(p), r = o.accountId || "", t; easyNav.setSessionId(r);
        var m = o.ifDisplayBalance == 1, s = +o.epayStatus || 0, n = d("#myEpay"), q, l;
        switch (s) {
            case 1: case 123: l = e.center;
                if (m) { q = "余额：<strong class='c_ba2636'>" + o.epayBalance + "</strong> 元 <a href='" + e.charge + "' target='_blank'>充值</a>" }
                break;
            case 3:
            case 4: e.active = d.format(e.active, k(o.platformReturnUrl || ""));
                t = "您的奖金将派发至xx，补全账户信息<br/>即可查看和使用。<a class='nowrap c_1e50a2' target='_blank' href='" + e.active + "'>立即补全账户信息</a>";
                q = "余额：<strong class='c_ba2636'>***</strong> 元 <a href='https://epay.xx.com/' target='_blank' inf=\"" + t + "\" class='jtip'>查看</a><a href='" + e.charge + "' target='_blank'>充值</a>";
                n.addClass("jtip").attr("inf", t);
                break
        }
        l && n.attr("href", l);
        q && d("#topEpayInfo").html(q).show()
    });
    return this
}
, myInit: d.noop
, parseJSON: function (e) {
    e = e.replace(/("|')\\?\/Date\((-?[0-9+]+)\)\\?\/\1/g, "new Date($2)"); return (new Function("return " + e))()
}
, get: d.get2
, post: d.post2, getJSON: d.getJSON2, postJSON: d.postJSON2, loadJS: d.loadJS, loadCss: d.loadCss
, loadCdnJS: function () { var e = arguments; Array.prototype.push.call(arguments, this.cdnUrl); return this.loadJS.apply(this, e) }
, loadCdnCss: function (e) { return this.loadCss(e, this.cdnUrl) }
, isLogin: function (e) {
    this.get("http://caipiao.xx.com/identity/queryLoginStatus.html"
    , function (k, i) { var j = k ? this.easyNav.isLogin() ? easyNav.account : "" : i == "0" ? "" : i; e && e.call(this, j) }) }
    };
    return g
})(window, jQuery);  

jQuery(window).unload(function () {
    document.oncontextmenu = null; window.Core = null;
    window.onload = null; window.onresize = null; window.onunload = null;
    window.onerror = null; window.onbeforeunload = null; (window.CollectGarbage || function () { })()
});

jQuery(document).ready(function () {
    Core.init && Core.init()
}); 

var easyNav = (function (A, z, x, w) {
    var v = A.document
    , d = function (l, i) { return z(l, i) }
    , g = encodeURIComponent;
    d.log = A.console ? function (i) { A.console.log(i) } : d.noop;
    d.addClass = function (l, i) { z(l).addClass(i) };
    d.removeClass = function (l, i) { z(l).removeClass(i) }; 
    d.bind = function (l, i, m) { z(l).bind(i, m) };
    d.getCookie = function (i) { return z.cookie(i) || "" };
    d.format = z.format;
    d.contains = z.contains;
    d.noop = z.noop;
    d.trim = z.trim;
    d.addUrlPara = function (l, i) {
        var m = (l + "").indexOf("?") + 1 ? "&" : "?";
        return l + m + i
    };
    d.fillUrl = function (l) {
        if (typeof l !== "string" || l == "") { return l }
        if (!/^http/i.test(l)) {
            var i = A.location.port || "80", m = /^\//.test(l);
            if (!m) { l = v.URL.replace(/\/[^\/]*$/g, "/") + l }
            else { l = A.location.protocol + "//" + A.location.host + (i == "80" ? "" : (":" + i)) + l } 
        } return l
    };
    d.isObject = function (i) { return Object.prototype.toString.call(i) === "[object Object]" };
    d.isFunction = function (i) { return Object.prototype.toString.call(i) === "[object Function]" };
    var p = {
        appName: "",
        appID: "",
        sessionId: "",
        navWrap: "#topNavLeft",
        welcomeUser: "{time}好，{funcEntry}，欢迎来到{appName}！{logoutLink}", welcomeGuest: "欢迎来到{appName}！{loginLink} {regLink}", logoutTxt: "安全退出",
        logoutUrl: "http://reg" + g(v.URL), loginTxt: "请登录",
        loginUrl: "", loginJsPath: null,
        preload: 1, formAgent: "/agent/formAgent.htm",
        jumpAgent: "#", 
        regTxt: "注册",
        regUrl: "", 
        loginConfig: {}
    }
    , F = {
        "xz.com": ["xz.com", "http://123"]
    }
    , f = [
        "进入我的彩票", "epay.xx.com", "http://45",
        "进入我的个人中心", "t.xx.com", "http://123"]
    , H = function () {
        var i = v.URL.replace(/\?.*$/g, "").replace(/#.*$/g, "");
        if (/^[^:]+:\/\/([^\/\?\#]+).*$/gi.test(i)) { return RegExp.$1 } return i
    }
    , c = function (l, i) { return (new RegExp(i + "$", "i")).test(l) }
    , E = function (P, O, M, s, r, q, o, n) {
        var m = r || d.noop, l, L = s ? d.addClass : d.noop, Q = s ? d.removeClass : d.noop
        , N = { mouseout: function (R) {
            var i = R.relatedTarget || R.toElement; if (i !== this && !d.contains(this, i) && i !== O && !d.contains(O, i)) {
                if (!t) { O.style.display = "none" } Q(P, s); n && n()
            } l && A.clearTimeout(l)
        }
    }
    , t = d.contains(P, O); N[M || "click"] = function (R) {
        if (q && (M || "").indexOf("mouse") >= 0) {
            var i = this; 
            l && A.clearTimeout(l);
            l = A.setTimeout(function () { l = 0; if (!t) { O.style.display = "block" } L(i, s); o && o(R) }, q)
        }
        else {
            if (!t) { O.style.display = "block" }
            L(this, s);
            o && o(R)
        } 
        d.contains(O, R.target) || R.preventDefault()
    };
    d.bind(P, N);
    d.bind(O, { mouseout: function (R) {
        var i = R.relatedTarget; if (i !== this && !d.contains(this, i) && i !== P && !d.contains(P, i)) {
            if (!t) { this.style.display = "none" } Q(P, s); n && n(R)
        } l && A.clearTimeout(l)
    }
    , click: function (R) {
        var i = R.target.tagName.toLowerCase() == "a" ? R.target : R.target.parentNode.tagName.toLowerCase() == "a" ? R.target.parentNode : null; if (!i || m.call(i, R) !== false) {
            if (!t) { O.style.display = "none" } Q(P, s); n && n(R)
        } 
    } 
})
}
, B = function () {
    var i = d.getCookie("P_INFO").replace(/\"|\'/g, "");
    if (i.split("|").length > 1 && /^.+@.+$/.test(i.split("|")[0])) { return i } return ""
}, u = function () {
    var i = d.getCookie("S_INFO"); 
if (i.split("|").length > 1) { return i } return "" }, K = B().split("|"), b = function (l) { 
        var i = "", m; K = B().split("|"); i = K[0]; if (l) { return i } m = K[K.length - 1]; if (/^1\d{10}@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(m)) { i = m } return i }, D = function (l, i) {
        var n = d(l)[0], m = []; if (!n) { return 1 } if (n.tagName.toLowerCase() !== "form") { return 2 } if (n.enctype.indexOf("multipart") >= 0) { return 3 }
        if (!i) { return 4 } m = d(n).serialize().replace(/\"/, '\\"'); if (!m) { return 5 } A.LS.set(i, '{action:"' + (n.action || v.URL) + '",method:"' + n.method + '",data:"' + m + '"}'); return 0
    }
, h = function (l, i) {
    if (i) { if (i.cookie) { l = d.addUrlPara(l, "cookie=" + g(i.cookie)) } if (i.ls) { l = d.addUrlPara(l, "ls=" + g(i.ls)) } } l = d.addUrlPara(l, "ver=4");
    return d.fillUrl(l)
}, J = { account: b(), from: K[3], isLogin: function (i) { return i ? !!this.sessionId && !!u() : !!u() && !!B() && !!this.account }
, setSessionId: function (l) {
    var i = null; if (/^.+@.+$/.test(l) || l == "") { if (b(true) == l) { i = b() } else { i = l } } if (i != null) {
        if (this.sessionId != i) {
            this.sessionId = i; this.account = i || b(); this.onLogin()
        } 
    } 
}, $: d, bindDropMenu: E
, login: function (l, i) { this._loginUrlCache = l || v.URL; this._loginOpCache = i; if (!this._loginLoaded) { this._loginLoaded = 1; a(this.options.loginJsPath) } }
, login2: function (l, i) { this.login(l || function () { }, i) }, saveForm: function (l, i, q, o) {
    if (!A.LS) { d.log("saveForm:请先加载window.LS本地存储组件！"); return "" }
    if (!this.options.formAgent || !l || !i) { return "" } if (d.isObject(q)) { o = q; q = "" }
    var n = D(l, i), m; switch (n) {
        case 0: return this.getFormAgentUrl(i, q, o); case 5: return this.getJumpAgentUrl(d(l)[0].action, o);
        default: d.log("saveForm:" + ["表单不存在", "不是form元素", "form类型不支持", "缺少key"][n - 1]); return ""
    } 
}
, getFormAgentUrl: function (l, i, n) { if (!this.options.formAgent || !l) { return "" } var m = d.addUrlPara(this.options.formAgent, "data=" + g(l) + "&bak=" + g(i || v.URL)); return h(m, n) }
, getJumpAgentUrl: function (l, i) { if (!this.options.jumpAgent || !i) { return l } var m = d.addUrlPara(this.options.jumpAgent, "bak=" + g(l || v.URL)); return h(m, i) }
, init: function (l, i) {
    var n = I(l), m; this.from = n.appID || this.from || ""; this.options = n; this.sessionId = n.sessionId || this.sessionId || ""; this.account = this.sessionId || this.account; m = !!this.sessionId; i || v.write(k());
    if (m) { E(d("#user163Box")[0], d("#user163List")[0], "mouseover", "user163BoxActive", d.noop, 200) } else { if (n.loginJsPath !== w && n.loginJsPath !== null) { n.preload && d(function () { a(n.loginJsPath) }) } } delete this.init
}
, repaint: function (l) {
    var i = d(l || this.options.navWrap).eq(0);
    if (i[0]) {
        i.empty().html(k()); !!this.sessionId && E(d("#user163Box")[0], d("#user163List")[0], "mouseover", "user163BoxActive", d.noop, 200) 
    } 
}
, onLogin: function (m) { var l = J.isLogin(true); if (d.isFunction(m)) { G.push(m); l && m(J.sessionId) } else { if (l) { var r = G, q = r.length, o = 0; for (; o < q; o++) { d.isFunction(r[o]) && r[o](J.sessionId) } } } } 
}
, G = [function () { J.repaint() } ], I = function (m) {
    var l = m || {}, n, o, i = z.extend({}, p);
    for (var q in i) {
        if (l[q] !== w) { i[q] = l[q] } 
    }
    l = i; 
    if (l.loginUrl.substr(0, 1) == "@") {
        l.loginJsPath = l.loginUrl.substr(1);
        l.loginUrl = "javascript:easyNav.login();"
    }
    l.logoutUrl = d.format(l.logoutUrl, { username: g(l.sessionId || J.sessionId || J.account || "") });
    if (/^javascript:/i.test(l.loginUrl)) { l.loginUrl = 'javascript:void(0);" onclick="' + l.loginUrl.substring(11) }
    if (!l.regUrl) {
        n = v.URL; n = n.indexOf("from=") + 1 ? n : d.addUrlPara(v.URL, "from=reg"); o = d.addUrlPara(n, "isShowLogin=1");
        l.regUrl = "http://reg.xx.com/reg/reg.jsp?product=" + l.appID + "&url=" + g(n) + "&loginurl=" + g(o)
    } return l
}
, k = function () {
    var O = J, s = O.options, P = new Date().getHours(), N = P > 5 && P <= 11 ? "上午" : P > 11 && P <= 13 ? "中午" : P > 13 && P <= 17 ? "下午" : P > 17 || P <= 2 ? "晚上" : "凌晨",
    t = O.sessionId
    //, L = ['<span id="user163Box"><a href="{0}" target="_blank" rel="nofollow" hideFocus="true" id="user163Name" title="', t, '"><em>', t, '</em></a><i id="userBoxArrow"></i><div id="user163List">']
    , L = ['<span id="user163Box"><a href="{0}" target="_blank" rel="nofollow" hideFocus="true" id="user163Name" title="', t, '"><em>', t, '</em></a><i id="userBoxArrow"></i><div id="user163List">']
    , Q = '<a target="_blank" href="{0}">{1}</a>'
    , r = {
        account: t,
        from: O.from,
        time: N,
        appName: s.appName,
        logoutLink: '<a href="' + s.logoutUrl + '">' + s.logoutTxt + "</a>", 
        loginLink: '<a href="' + s.loginUrl + '">' + s.loginTxt + "</a>"
        //, regLink: '<a href="' + s.regUrl + '">' + s.regTxt + "</a>" }
        , regLink: '<a href="' + s.regUrl + '">' + s.regTxt + "</a>"
    }
    , M = !!O.sessionId, o = H(), m = f, l = m.length, q = 0;
    for (; q < l; q = q + 3) {
        m[q + 2] = d.format(m[q + 2], r);
        if (m[q + 1] && !c(o, m[q + 1])) {
            L[L.length] = d.format(Q, m[q + 2], m[q]) 
        }
        if (q == 0) {
            L[0] = d.format(L[0], m[q + 2])
        }
    }
    r.funcEntry = L.join("") + "</div></span>";
    return d.format(M ? s.welcomeUser : s.welcomeGuest, r)
}
, a = function (l) {
    var i = !l ? "" : /\\\/$/.test(l) ? l : l + "/", n, m = v.getElementsByTagName("head")[0] || v.documentElement; a = d.noop;
    if (!z.dialog) {
        n = v.createElement("script"); n.type = "text/javascript"; n.charset = "UTF-8"; n.src = i + "dialog.js?" + x.version; m.appendChild(n, m.lastChild) 
    }
    if (!z.fn.autoFill) { n = v.createElement("script"); n.type = "text/javascript"; n.charset = "UTF-8"; n.src = i + "autoComplete.js?" + x.version; m.appendChild(n, m.lastChild) } n = v.createElement("script");
    n.type = "text/javascript"; n.charset = "UTF-8"; n.src = i + "login.js?" + x.version; m.appendChild(n, m.lastChild)
}; 
    if (K[0]) {
    var j = K[0].split("@")[1], e = F[j.toLowerCase()], y = f.length, C = 0; if (e) {
        for (; C < y; C = C + 3) {
            if (!f[C + 1] && !f[C + 2]) { f[C + 1] = e[0]; f[C + 2] = e[1]; break } 
        } 
    } 
} return J
})(window,jQuery,Core);