/// <reference Path="jquery-1.4.1-vsdoc.js" />
function getTopWindow(winx) {
    var win = winx;
    while (win != win.parent) {
        win = win.parent;
    }
    return win;
}


(function () {
    $.dialog = function (options) {
        var defaults = {
            element: null,
            title: "弹出层",
            modal: true,
            width: "500px",
            height: "400px",
            autoFocus: true,
            onopen: null,
            onbeforeclose: null,
            onclose: null,
            oncloseing:null,
            hideCloseBtn:false
        };
        var settings = $.extend(defaults, options);
        this.element = settings.element;
        this.title = settings.title;
        this.modal = settings.modal;
        this.width = settings.width;
        this.height = settings.height;
        this.autoFocus = settings.autoFocus;
        this.onopen = settings.onopen;
        this.onbeforeclose = settings.onbeforeclose;
        this.onclose = settings.onclose;
        this.oncloseing =settings.oncloseing;
        this.hideCloseBtn = settings.hideCloseBtn;
        this._init();
        this._mouse();
    };

    $.dialog.prototype = {
        _init: function () {
            var self = this, dialogIndex = parseInt($(document.body).data("dialogindex"));
            this.topContainter = $("<div>",{style:"background-color:#fff"});
            this.divContent = this.divContent || this.element.wrap("<div></div>").wrap("<div></div>").parent()
                .css({ overflow: "auto", /*margin: "5px",*/ width: this.width, height: this.height });
            this.divContainer = this.divContainer || this.divContent.parent().addClass("div_container_dialog")
                .css("z-index", dialogIndex);
            this.divTitle = this.divTitle || $("<div></div>")
                .prependTo(this.topContainter).addClass("div_title_dialog").text(this.title)
                .bind("selectstart", function () { return false; });
            if(!this.hideCloseBtn){
                this.divTitleClose = this.divTitleClose || $("<div></div>")
                    .appendTo(this.divTitle).addClass("div_title_close_dialog").click(function () {
                        if($(this).attr("noClosing") == "true")
                             self.close2();
                        else
                            self.close();
                    });
            }
            if (this.modal === true) {
                var win = getTopWindow(window);
                var divShadeContainer = win.document.body;

               
                var tmplate =   '<table cellspacing="0" cellpadding="0" border="0"><tbody>'+ 
                                '<tr>' +
                                    '<td style="border-radius:5px 0 0 0;width:5px;height:5px;" class="dialog-border"></td>' +
                                    '<td style="height:5px;overflow: hidden;" class="dialog-border"></td>' +
                                    '<td style="border-radius:0 5px 0 0;width:5px;height:5px;" class="dialog-border"></td>'+ 
                                '</tr><tr>'+
                                    '<td class="dialog-border"></td>'+
                                    '<td class="topContainter" id="dlgContiner" valign="top"></td>' +
                                    '<td class="dialog-border"></td>'+
                                '</tr><tr>'+
                                    '<td style="border-radius:0 0 0 5px; width:5px; height:5px;" class="dialog-border"></td>'+
                                    '<td style="height:5px;overflow: hidden;" class="dialog-border"></td>'+
                                    '<td style="border-radius:0 0 5px 0; width:5px; height:5px;" class="dialog-border"></td>'+
                                '</tr></tbody></table>';
                
                this.divContainer.appendTo(divShadeContainer);
                var table$ = $(tmplate);
                table$.appendTo(this.divContainer);
                this.divContent.appendTo(this.topContainter);
                this.topContainter.appendTo(table$.find(".topContainter"));

                this.divShade = this.divShade || $("<div></div>").appendTo(divShadeContainer)
                    .addClass("div_shade_dialog").css("z-index", dialogIndex - 1);
                winResize = function () {
                    var windowW = $(win).width(), windowH = $(win).height(),
                        bodyW = $(win.document.body).width(), bodyH = $(win.document.body).height();
                    self.divShade.width(windowW < bodyW ? bodyW : windowW)
                    .height(windowH < bodyH ? bodyH : windowH);
                };
                $(win).resize(winResize);
                
                setTimeout(winResize, 10);
            }
        },
        _mouse: function () {
            var self = this;
            var win = getTopWindow(window);

            this.divTitle.mousedown(function (e) {
                x = $(win.document).scrollTop() + e.originalEvent.clientY - self.divContainer.offset().top;
                y = $(win.document).scrollLeft() + e.originalEvent.clientX - self.divContainer.offset().left;
                self.divContainer.data = { MoveFlag: true, X: x, Y: y, DocTop:$(win.document).scrollTop(), DocLeft: $(win.document).scrollLeft() };
                win.document.onselectstart = new Function("return false");
            });

            $(win.document).mouseup(function (e) {
                if (self.divContainer.data.MoveFlag) { document.onselectstart = {}; }
                self.divContainer.data = { MoveFlag: false, X: 0, Y: 0 };
            }).mousemove(function (e) {
                if (self.divContainer.data.MoveFlag) {
                    self.divContainer.css({
                        top: self.divContainer.data.DocTop + e.originalEvent.clientY - self.divContainer.data.X,
                        left: self.divContainer.data.DocLeft + e.originalEvent.clientX - self.divContainer.data.Y
                    });
                }
            });
            /*$(win.document).mousedown(function (e) {
                var elementFlag = self.divTitle[0] === $(e.target)[0],
                x = $(win.document).scrollTop() + e.originalEvent.clientY - self.divContainer.offset().top,
                y = $(win.document).scrollLeft() + e.originalEvent.clientX - self.divContainer.offset().left
                self.divContainer.data = { MoveFlag: elementFlag, X: x, Y: y };
                if (elementFlag) { win.document.onselectstart = new Function("return false"); }
            }).mouseup(function (e) {
                if (self.divContainer.data.MoveFlag) { document.onselectstart = {}; }
                self.divContainer.data = { MoveFlag: false, X: 0, Y: 0 };
            }).mousemove(function (e) {
                
                if (self.divContainer.data.MoveFlag) {
                    var buttonFlag = e.originalEvent.button == 1 && $.browser.msie || e.originalEvent.button == 0 && $.browser.mozilla;
                    if (buttonFlag) {
                        self.divContainer.css({
                            top: $(win.document).scrollTop() + e.originalEvent.clientY - self.divContainer.data.X,
                            left: $(win.document).scrollLeft() + e.originalEvent.clientX - self.divContainer.data.Y
                        });
                    }
                }
            });*/
        },
        open: function () {
            var win = getTopWindow(window);
            var self = this;
            var left = ($(win).width() - this.divContainer.width()) / 2;
            var top = ($(win).height() - this.divContainer.height()) / 2 + $(win).scrollTop();
            if (this.modal === true) { this.divShade.show(); }
            this.divContainer.css({
                left: left < 0 ? 0 : left,
                top: top < 0 ? 0 : top
            }).fadeIn("fast", function () {
                if (self.autoFocus === true) { self.divContainer.focus(); }
                if (self.onopen && $.isFunction(self.onopen)) { self.onopen(); }
            });
        },
        beforeclose: function () { // 此方法的最后返回一个 bool 弄的变量，决定是否调用 close
            if (this.onbeforeclose && $.isFunction(this.onbeforeclose)) {
                return this.onbeforeclose();
            }
            return true;
        },
        close: function (closeBack) {
            var self = this;
            if (self.oncloseing && $.isFunction(self.oncloseing)) {
                 if(!self.oncloseing(self.divContainer)) {
                     self.oncloseing = false;
                     return;
                }
            }
            self.close2(closeBack);
        },
        close2: function(closeBack) {
            var self = this;
            if (this.beforeclose()) {
                if (this.modal === true) { this.divShade.hide(); }
                this.divContainer.fadeOut("fast", function () {
                    if (self.onclose && $.isFunction(self.onclose)) { self.onclose(self.divContainer); }
					if(closeBack && $.isFunction(closeBack)) { closeBack(self.divContainer); }
                    setTimeout(function() {
                        self.divContainer.find("iframe").remove();
                        self.divContainer.remove();
                        self.divShade.remove();
                    }, 1000);
                    
                });
            }
        },
        isOpen: function () {
            return !this.divContainer.is(":hidden");
        }
    }

    $.fn.dialog = function (options) {
        
        var win =getTopWindow(window);
        var thisId ="dialog"  +  win.$(this).attr("id");
        var winBody = win.$(win.document.body);

        var dialogIndex =winBody.data("dialogindex");
        var dialog = winBody.data(thisId);
        
        dialogIndex = dialogIndex == undefined ? 11 : dialogIndex;
       
        if (dialog) {
            //return dialog;
        } 

       winBody.data("dialogindex", dialogIndex + 2);
       dialog = new win.$.dialog(win.$.extend({ element: win.$(this) }, options));
       winBody.data(thisId, dialog);
     
        return dialog;
       
    }
})();