/*
-- 下载软件 on 2009-12-10 于深圳
*/
function MenuClass(options) {
    var s = this;
    s.containerId = "divmenu"; // 菜单放置的 element 的 id
    s.arrowUrl = options.arrowUrl == undefined ? "" : options.arrowUrl; // 箭头图标 url
    s.baseTarget = 0; // 0 表示新窗口，1 表示本窗口
    s.exportType = 0; // 0 表示主菜单水平显示，1 表示主菜单垂直，2 表示右键菜单，3 表示自定义显示或隐藏
    s.repairLeft = s.isIE() ? 8 : 3; // 修补量，主要是什么 border 啊，什么的
    s.repairTop = s.isIE() ? 4 : 2; // 修补量，主要是什么 border 啊，什么的
    s.zIndex = 4; // zIndex
    s.css = { // css 样式类名
        containerDivHorizontal: "containerDivHorizontal_menu",
        lineHorizontal: "lineHorizontal_menu",
        containerTable: "containerTable_menu",
        containerDiv: "containerDiv_menu",
        rowReadOnly: "rowReadOnly_menu",
        row: "row_menu",
        colIcon: "colIcon_menu",
        colText: "colText_menu",
        colArrow: "colArrow_menu",
        over: "over_menu",
        line: "line_menu"
    };
};

MenuClass.prototype = {
    $: function (id) { return typeof id == "string" ? document.getElementById(id) : id; },
    isIE: function () { return document.all; },
    show: function (o) { o.style.display = ""; },
    hide: function (o) { o.style.display = "none"; },
    ce: function (tagName) { return document.createElement(tagName); },
    contains: function (a, b) {
        try {
            return a.contains ? a != b && a.contains(b) : !!(a.compareDocumentPosition(b) & 16);
        } catch (e) { return false; }
    },
    pos: function (o) {
        if (arguments.length != 1 || o == null) return null;
        var t = o.offsetTop, l = o.offsetLeft;
        var w = o.offsetWidth, h = o.offsetHeight;
        while (o = o.offsetParent) { t += o.offsetTop; l += o.offsetLeft; }
        return { AbsoluteTop: t, AbsoluteLeft: l, OffsetWidth: w, OffsetHeight: h };
    },
    event: function () {
        if (document.all) { return window.event; }
        var func = this.event.caller;
        while (func != null) {
            var arg0 = func.arguments[0];
            if (arg0 instanceof Event) { return arg0; }
            func = func.caller;
        }
        return null;
    },
    stopDefault: function () {
        var event = this.event();
        if (event.stopPropagation) { event.stopPropagation(); }
        else { event.cancelBubble = true; }
        if (event.preventDefault) { event.preventDefault(); }
        else { event.returnValue = false; }
    },
    createItemContainer: function (element, zIndex) {
        var s = this;
        var div = this.ce("div"), table = this.ce("table");
        div.appendChild(table);
        //div.addEventListener("mouseout", function () { s._hideAll(); }, false);
        element.appendChild(div);
        div.className = this.css.containerDiv;
        div.style.zIndex = zIndex;
        table.className = this.css.containerTable;
        return { div: div, table: table };
    },
    hideItemContainer: function (objects) {
        for (var o in objects) {
            var container = objects[o].container;
            this.changeRowClass(container.table);
            this.hide(container.div);
            if (objects[o].items.length > 0) { this.hideItemContainer(objects[o].items); }
        }
    },
    changeRowClass: function (table) {
        var cr, childRows = table.getElementsByTagName("tr");
        for (var r in childRows) {
            cr = childRows[r];
            if (cr.className != "" & cr.className != this.css.rowReadOnly) {
                cr.className = this.css.row;
                if (cr.style) {
                    cr.style.backgroundColor = "";
                    cr.style.color = "";
                }
            }
        }
    },
    createFirstRow: function (flag, row) {
        if (flag) {
            var table = this.ce("table");
            table.insertRow(-1);
            table.className = this.css.containerTable;
            var col = row.insertCell(-1);
            col.appendChild(table);
            row = table.rows[0];
        }
        return row;
    },
    getIndex: function () { return this.zIndex + this.counter++; }
};

MenuClass.prototype._hideAll = function () {
    var s = this;
    s.changeRowClass(s.container.table);
    s.hideItemContainer(s.items);
    s.isclick = false;
    if (s.exportType > 1) { s.hide(s.container.div); }
};
MenuClass.prototype.Apply = function () {
    var s = this, b = document.body;
    s.base = s;
    s.items = [];
    s.counter = 0;
    s.direction = 1;
    s.floor = s.exportType === 0 ? -1 : 0;
    s.object = s.containerId === "" ? b.appendChild(s.ce("div")) : s.$(s.containerId);
    s.object = s.object || b.appendChild(s.ce("div"));
    s.container = s.container || s.createItemContainer(s.exportType > 1 ? b : s.object, s.getIndex());
    s.isclick = false;
    var c = s.container;
    if (s.exportType === 0) {
        c.table.insertRow(-1);
        c.div.className = s.css.containerDivHorizontal;
    } else { c.div.style.width = "260px"; }
    if (s.exportType > 1) { s.hide(c.div); } else {
        var pos = s.pos(c.div);
        s.direction = pos.AbsoluteLeft + pos.OffsetWidth <= document.documentElement.clientWidth;
        c.div.style.position = "relative";
    }
    if (document.addEventListener) {
        document.addEventListener("mouseup", function () { s._hideAll(); }, false);
    } else if (document.attachEvent) {
        document.attachEvent("onmouseup", function () { s._hideAll(); });
    } else { document.onmouseup = function () { s._hideAll(); }; }
};

MenuClass.prototype.Item = function (text, icon) {
    var item = new _itemClass(this);
    item.create(text, icon);
    return item;
};
MenuClass.prototype.Link = function (text, url, readonly, icon, open) {
    var link = new _linkClass(this);
    link.create(text, url, readonly, icon, open);
    return link;
};
MenuClass.prototype.Line = function () {
    var line = new _lineClass(this);
    line.create();
    return line;
};
MenuClass.prototype.Show = function (element) {
    function getPosSuitClient(element, l, t, h) {
        var de = document.documentElement,
        st = de.scrollTop, sl = de.scrollLeft,
        bw = de.clientWidth, bh = de.clientHeight,
        cw = element.offsetWidth, ch = element.offsetHeight;
        bw += sl; bh += st;
        return { left: l + cw > bw ? bw - cw : l, top: t + h + ch > bh + h ? bh - ch - h : t - h }
    }
    var s = this, div = s.container.div, pos = s.pos(element);
    s.hideItemContainer(s.items);
    s.show(div);
    var suitPos = getPosSuitClient(div, pos.AbsoluteLeft, pos.AbsoluteTop, pos.OffsetHeight);
    div.style.left = suitPos.left + "px";
    div.style.top = (suitPos.top + s.repairTop) + "px";
};
MenuClass.prototype.Hide = function () {
    var evt = this.event();
    if (!this.contains(this.container.div, evt.toElement || evt.relatedTarget)) { this._hideAll(); }
};
_itemClass.prototype.Item = MenuClass.prototype.Item;
_itemClass.prototype.Link = MenuClass.prototype.Link;
_itemClass.prototype.Line = MenuClass.prototype.Line;
function _itemClass(parent) {
    this.base = parent.base;
    this.parent = parent;
    this.container = null;
    this.items = [];
    this.floor = 0;
};
function _linkClass(parent) {
    this.base = parent.base;
    this.parent = parent;
};
function _lineClass(parent) {
    this.base = parent.base;
    this.parent = parent;
};
_itemClass.prototype.create = function (text, icon) {
    var s = this, b = this.base, p = this.parent, pc = p.container;
    s.container = s.container || b.createItemContainer(b.object, b.getIndex());
    p.items.push(this);
    s.floor = p.floor + 1;
    b.hide(s.container.div);
    createRow.call(pc);
    if (b.exportType === 2) {
        b.object.oncontextmenu = function () {
            function getPosSuitClient(element, l, t) {
                var de = document.documentElement,
                st = de.scrollTop, sl = de.scrollLeft,
                bw = de.clientWidth, bh = de.clientHeight,
                cw = element.offsetWidth, ch = element.offsetHeight;
                b.direction = l + cw <= bw;
                return { left: (l + cw > bw ? l - cw : l) + sl, top: (t + ch > bh ? t - ch : t) + st }
            }
            var div = b.container.div;
            b.hideItemContainer(b.items);
            b.show(div);
            b.stopDefault();
            var evt = b.event(), suitPos = getPosSuitClient(div, evt.clientX, evt.clientY);
            div.style.left = suitPos.left + "px";
            div.style.top = suitPos.top + "px";
        }
    }
    function createRow() {
        var table, col, container = this,
        flag = b.exportType === 0 & b === p,
        row = flag ? this.table.rows[0] : this.table.insertRow(-1);
        row = b.createFirstRow(flag, row);
        row.className = b.css.row;
        col = row.insertCell(-1);
        if (flag) {
            col.colSpan = 3;
            col.innerHTML = text;
            col.style.textAlign = "center";
        } else {
            col.className = flag ? "" : b.css.colIcon;
            if (icon) { col.appendChild(b.ce("img"))["src"] = icon; }
            col = row.insertCell(-1);
            col.innerHTML = text;
            col.className = b.css.colText;
            col = row.insertCell(-1);
            col.className = b.css.colArrow;
            col.innerHTML = b.arrowUrl ? '<img src="' + b.arrowUrl + '" />'
                : flag ? (b.isIE() ? "6" : "↓") : (b.isIE() ? "4" : "→");
        }
        row.onmouseover = function (event) {
            var sthis = this;
            function getScroll(o) { // 绝对偏移
                var a = o.parentNode, l = 0, t = 0;
                while (a != document.body && a != undefined) {
                    l += a.scrollLeft; t += a.scrollTop; a = a.parentNode;
                }
                return { scrollTop: t, scrollLeft: l };
            }
            function getPosSuitClient(element, l, t, w, h) {
                var scrollBar = 17, parentDiv = sthis.parentNode.parentNode,
                de = document.documentElement,
                es = getScroll(parentDiv),
                st = de.scrollTop, sl = de.scrollLeft,
                bw = de.clientWidth, bh = de.clientHeight,
                cw = element.offsetWidth, ch = element.offsetHeight;
                bw += sl; bh += st;
                b.direction = b.direction ? l + cw <= bw - b.repairLeft : l <= cw * 2 + sl - b.repairLeft;
                if (l + cw * s.floor > bw & l > cw * 2 + sl & !b.direction & !flag) { l = l - cw * 2 + b.repairLeft; }
                else if (l + cw <= bw & b.direction & !flag) { l = l; }
                else if (flag) { l = l - w + cw > bw ? bw - cw : l - w; }
                if (flag) { t = t + ch + h > bh ? t - ch + b.repairTop : t + h; }
                else {
                    t = t + ch + es.scrollTop > bh ? (es.scrollTop > 0 ? t - es.scrollTop : t - ch + h + b.repairTop) : t;
                    t = t + ch > bh ? t - ch + h : t;
                }
                if (ch > bh) { element.style.height = (flag ? bh - h : bh) - 2; }
                return { left: parentDiv.offsetHeight > bh ? l + scrollBar : l, top: t < 0 ? flag ? h : 0 : t }
            };
            function show() {
                var div = s.container.div; b.show(div);
                var pos = b.pos(sthis),
                left = pos.AbsoluteLeft + sthis.offsetWidth, top = pos.AbsoluteTop
                suitPos = getPosSuitClient(div, left, top, sthis.offsetWidth, sthis.offsetHeight);
                div.style.left = suitPos.left + "px";
                div.style.top = suitPos.top + "px";
            };
            b.changeRowClass(container.table);
            b.hideItemContainer(p.items);
            this.className = b.css.row + " " + b.css.over;
            if (flag) {
                this.onclick = function () {
                    b.isclick = !b.isclick;
                    if (b.isclick) { show(); } else { b.hide(s.container.div); }
                }
                if (b.isclick) { show(); } else { b.hide(s.container.div); }
            } else { this.onclick = {}; show(); }
        };
        row.onmouseout = function () {
            var evt = b.event(), to = evt.toElement || evt.relatedTarget;
            if ((!b.contains(this, to) && !b.contains(b.object, to))) {
                this.style.backgroundColor = "";
                this.style.color = "";
                this.className = b.css.row;
                b.hide(s.container.div);
            }
        };
        row.onmouseup = function () { b.stopDefault(); }
    };
};
_linkClass.prototype.create = function (text, url, readonly, icon, open) {
    var s = this, b = this.base, p = this.parent, pc = p.container;
    createRow.call(pc);

    function createRow() {
        var col, container = this,
        flag = b.exportType === 0 & b === p,
        row = flag ? this.table.rows[0] : this.table.insertRow(-1);
        row = b.createFirstRow(flag, row);
        row.className = readonly ? b.css.rowReadOnly : b.css.row;
        col = row.insertCell(-1);
        if (flag) {
            col.colSpan = 3;
            col.innerHTML = text;
            col.style.textAlign = "center";
        } else {
            col.className = flag ? "" : b.css.colIcon;
            if (icon && !readonly) { col.appendChild(b.ce("img"))["src"] = icon; }
            col = row.insertCell(-1);
            col.innerHTML = text;
            col.className = b.css.colText;
            col = row.insertCell(-1);
        }
        if (!readonly) {
            row.onmouseover = function (event) {
                b.changeRowClass(container.table);
                b.hideItemContainer(p.items);
                this.className = b.css.row + " " + b.css.over;
            };
            row.onmouseout = function () {
                this.style.backgroundColor = "";
                this.style.color = "";
                this.className = b.css.row;
            };
        } else { row.onmouseup = function () { b.stopDefault(); } }
        row.onclick = function (event) {
            if (readonly) { b.stopDefault(); } else {
                if (typeof url === "string") {
                    var openFlag = b.baseTarget;
                    if (open != undefined) { openFlag = open; }
                    try {
                        //当执行onbeforeunload 事件后,用户选择取消后,这里会报错.....
                        openFlag ? window.open(url) : window.location.assign(url);
                    } catch (e) {
                    }
                    
                } else if (typeof url === "function") { eval(url).call(self); }
            }
        };
    };
};
_lineClass.prototype.create = function () {
    var s = this, b = this.base, p = this.parent, pc = p.container;
    createRow.call(pc);

    function createRow() {
        var col,
        flag = b.exportType === 0 & b === p,
        row = flag ? this.table.rows[0] : this.table.insertRow(-1);
        row = b.createFirstRow(flag, row);
        if (flag) {
            col = row.insertCell(-1);
            col.innerHTML = '<span class="' + b.css.lineHorizontal + '"></span>';
        } else {
            col = row.insertCell(-1);
            col.className = b.css.colIcon;
            col = row.insertCell(-1);
            col.colSpan = 2;
            col.innerHTML = '<span class="' + b.css.line + '"></span>';
        }
    };
};