

    function renderCalendar() {
        $("input.calendar").css("padding-left", "23px").focus(function () {
            $(this).val($(this).val().replace(/[^\d]+/ig, ""));
            $(this).select();
            var c = new CalendarClass();
            c.Element = $(this)[0];
            c.Lang = window.Language;
            c.Format = window.DataFormat;
            try {
                var options = {};
                eval("var options = " + $(this).attr("options"));
                for (var i in options) { c[i] = options[i]; }
                c.Apply();
            } catch (e) { alert(e); }
        });
    }

    $(function () {
        renderCalendar();
    });