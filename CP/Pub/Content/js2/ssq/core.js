(function (a, b, e, f, c) {
    f.ssq = f.ssq || {}; f.loadMolude("ssq.clientRandom"); f.ssq.core = { maxRedNum: 33, wrap: "#select_list_box", useServerRandom: false,
        serverRandomUrl: "http://caipiao.", random: function (h, j) {
            var l = [], g, m, k;
            if (h) {
                g = h.split(","); b.each(g, function (o, p) { var n = p.split(":"), q = [n[0].split(" "), [n[1]]]; q.random = 2; l.push(q) }); return l
            } m = +j; if (f.ssq && f.ssq.clientRandom) { return f.ssq.clientRandom(m) } else { for (k = 0; k < m; k++) { d = [f.random("1-33", 6), f.random("1-16", 1)]; d.random = 1; l[k] = d } } return l
        }, htmlTmpl: ['<span class="type">{type}</span>', '<span class="nums" title="{red}|{blue} [共{num}注 {cost}元]"><strong class="c_ba2636">{red}</strong>|<strong class="c_1e50a2">{blue}</strong></span>', '<span class="edit">{edit}{del}</span>', '<span class="sum">{cost}元</span>'].join("")
, adapter: function (j) {
    var h = function (l) { var p = l.length, m = 0, o = []; for (; m < p; m++) { o[m] = f.fillZero(l[m], 2) } return o.join(" ") }, g = { lucky: "幸运选号"
    }, k = j.length == 2 ? { type: g[j.type || ""] || ((j[0].length > 6 || j[1].length > 1) ? "复式" : "单式"), red: h(j[0]), blue: h(j[1])} : { type: g[j.type || ""] || "胆拖", red: "(" + h(j[0]) + ")" + h(j[2]), blue: h(j[3]) }, i = this.counter(j); k.num = i[0]; k.cost = i[1]; if (j.type == "lucky") { k.edit = "<em></em>" } return b.format(this.htmlTmpl, k)
}
, counter: function (h) { var g = h.length == 2 ? f.c(h[0].length, 6) * h[1].length : (h[0].length + h[2].length > 6 && h[0].length * h[2].length > 0) ? f.c(h[2].length, 6 - h[0].length) * h[3].length : 0; return [g, 2 * g] }, serialize: function (j) {
    var k = [], h = function (l) {
        b.each(l, function (m, o) { l[m] = f.fillZero(o) }); return l.join(" ")
    }
, i, g = this; b.each(j, function (l, m) {
    if (m.length == 2) {
        i = g.getT(m); k[l] = h(m[0]) + ":" + h(m[1]) + (i ? "t:" + i : "")
    } else { k[l] = "(" + h(m[0]) + ")" + h(m[2]) + ":" + h(m[3]) }
}); 
return k.join(",")
}
, parse: function (j) {
    var i = [], h = j.split(","), g = this; b.each(h, function (m, o) {
        var k, l, n; if (o.indexOf("(") == 0) {
            k = o.substr(1).split(")");
            l = k[1].split(":"); n = [k[0].split(" "), [], l[0].split(" "), l[1].split(" ")]
        } else { k = o.replace(/t:/g, ":").split(":"), n = [k[0].split(" "), k[1].split(" ")]; if (k.length > 2) { g.setT(n, +k[2]) } } i[m] = n
    }); return i
}
,getT:function(h){var g={upload:2,dingdan:3,lucky:5}[h.type];return g||(h.random?1:0)},setT:function(h,g){h.type={"2":"upload","3":"dingdan","5":"lucky"}[g];h.random=+g==5||+g==1?1:c}}})(window,jQuery,Core,Game);