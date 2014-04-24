(function (b, e, c, a, d) {
    c.extend(e, { updateQuickBetIcon: function () {
        delete this.updateQuickBetIcon},initQuickBet:function(f){this.updateQuickBetIcon();
        var g = function (h) {
            e.initQuickGame("d11", { dom: h, isCheckLogin: false, gameExtra: ["任选三", "任选五", "前三"][+h.id.replace(/quickBetD11_/gi, "")] })
        };
        c("#quickBetD11").find(".secplay").each(function (h) {
            this.id = "quickBetD11_" + h
        }).end().find(".secBetwayTabs").find("li").each(function (h) {
            c(this).attr("rel", "#quickBetD11_" + h)
        }).end().bindTab(g).end().find(".btnBuy5Times").click(
                function(){var h=c(this).closest("form");
                h.attr("action","/order/followBuy_order_followBuyServeralPeriod.html");
                h.trigger("submit")
            }).end().find(".btnSubmint").click(function () {
                    var h=c(this).closest("form");
                    h.attr("action","/order/order_quickPreD11.html")});
                c("#quickBetTab").bindTab(function (i) {
                var h=c(this).attr("rel").replace(/\#quickBet/,"").toLowerCase();
                if (c(this).attr("timeout") != "yes") {
                    if(h=="d11"){i=c(c(".secBetwayTabs .active",i).attr("rel"))[0];
                    g(i);
                    return
                }
e.initQuickGame(h,{dom:c(".buyLottery",i),isCheckLogin:false})}});
this.initQuickGame("ssq",{dom:"#quickBetSSQ .buyLottery",isCheckLogin:false});
this.initCounter(f);
c("#quickbuyBox").delegate("[name=random_betTimes]", "keyup", function () {
    var j=this.value,h=j.replace(/\D/g,""),k=+(h||1);
    if (j != h) {
        this.value = h
    }
var i=c(this).closest("td").next();
i.next().find("em").text(+i.find("em").text() * 2 * k)
}).delegate("[name=continuousForm]", "submit",
function () {
    var h=c("[name=random_betTimes]",this);
    if (!/^[1-9]\d*$/g.test(h.val())) {
        c.dialog({ width: 210, content: "请输入正确的套餐注数！" },
function(){h[0].select()});
        return false
    }
    if (!easyNav.isLogin(true)) {
        easyNav.login(easyNav.saveForm(this,"continuousForm"));
        return false
    }
}).delegate(".hotTaocan_btnSub", "click", function () {
    var h=c(this).closest("dd").children("div").eq(0).attr("timeout");
    (!h||h!="yes")&&c(this).closest("tr").find("form").submit()}).find("[name=random_betTimes]").keyup();
c("#quickbuyBox form").attr("target", "_self")
}
, initQuickGame: function (g, f) {
    this.loadGame(g + ".quickBuy", function () {
        var h=Game[g].quickBuy(f);
    if(h){h.quickBuySelect();
        h.bindFormSubmit()
    } 
})
}
,initCounter: function (f) {
    var h = c(".retime").each(function () {
    this.isGP=!!c(this).closest(".gpcBet_con").length;
    this.relTime = +((c(this).attr("rel") || "0").replace(/\D/g, ""))
}),
    i = f
, g = function () {
    h.each(function () {
    var A=+new Date()-i,r=this.relTime-A,s,v,B,q,C;
    if (r <= 0) {
        if (this.stopCounter) { return }
    var l=c(this).closest(".buyLottery"),j=l.hasClass("quickbuy_d11"),p,o,x;
    if(j){p=l.find(".d11_no");
    o=c.trim(p.text());
    x=+o+1;
if(o.slice(-2)!="78"){p.text(" "+x+" ");
this.relTime+=600000;
return
}
}
if (this.isGP) {
    this.gpReqCount=this.gpReqCount||0;
    if (!this.getTiming && !this.endTiming) {
        var y,n=this,k,m,z;
        z=c(this).closest("dd");
        y=z.attr("id").replace("quickBet","").toLowerCase();
        k=y==="k3"?"http://order/preBet_kuai3PeriodTime.html":"http://order/preBet_periodAndAwardTime.html?gameEn="+y;
        m=y==="k3"?{cache:(new Date()).getTime()}:{stamp:(new Date()).getTime()};
function w() {
        n.getTiming=true;
        if (++n.gpReqCount > 10) {
            n.endTiming=true;
            z.find(".retimeBlock").hide();
            return
        }
        e.get(k,m,function(D,G){var E;
    try {
        E = JSON.parse(G)
    } catch (F) { }
    if (D || !E || !E.secondsLeft) {
         setTimeout(w,2000);
     return
    }
    n.getTiming=false;
    n.gpReqCount=0;
    if (E.secondsLeft == -1) {
        n.endTiming=true;
        z.find(".retimeBlock").hide()
    }
    else 
    {
    n.relTime+=E.secondsLeft;
    z.find(".gpc_period").html("&nbsp;" + E.currentPeriod + "&nbsp;")
    }
})
}
w()
}
return
}
s=c(this).closest("form").parent().hide();
s.next().show();
s.parent().attr("timeout","yes");
this.stopCounter=true;
v=s.closest(".buyLottery").parent();
if (v.is(":visible")) {
    B=v.next();
    if (B[0]) {
    var t=c.trim(B[0].id),u=c("#quickBetTab").find("[rel=#"+t+"]");
    if (u[0]) {
c("#quickBetTab li").removeClass("active");
u.addClass("active");
v.hide();
B.show();
e.initQuickGame(t.replace(/quickBet/gi, "").toLowerCase(), "#" + t)
} 
} 
} 
} else {
    if (r > 20 * 60 * 1000 && this.isGP) {
        c(this).closest("dd").find(".retimeBlock").hide()
    }
q=e.getTimeDescription(r);
C=[];
q[0]!=0&&C.push("<em>"+q[0]+"</em>天");
(q[1]!=0||q[0]!=0)&&C.push("<em>"+q[1]+"</em>小时");
C.push("<em>"+q[2]+"</em>分");
C.push("<em>"+q[3]+"</em>秒");
c(this).html(C.join(""))}})};
b.setInterval(g,500);
g()}});
e.initQuickBet(new Date())})(window,Core,jQuery,easyNav);
