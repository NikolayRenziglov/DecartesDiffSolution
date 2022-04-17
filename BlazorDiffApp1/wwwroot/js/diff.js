var Diff = {
    executeJavascriptString: function (s) {
        eval(s);
    },
    showOutput: function (s) {
        s = JSON.parse(s);
        $('pre').html(JSON.stringify(s, undefined, 2));
    },
    clearOutput: function () {
        $('pre').html('');
    },
    showWaves: function (b) {
        var _ocean = $(".ocean");
        if (b == true) _ocean.show();
        else _ocean.hide();
    },

    closeFanoutMenu: function () {
        $('#nav').toggleClass('closed');
    }

}