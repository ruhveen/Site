

$(document).ready(function () {

    //var prefix = 'site/';
    var prefix = '';
    $("#start").click(function () {
        var counter = parseInt($('#counter').val());
        for (index = 0; index < counter; index++) {
            $.ajax({
                url: "/"+ prefix + "api/ReadWriteCache/Start/?numba=1000000&reqNum=" + index,
                cache: false,
                success: function (html) {
                    console.log(html);
                }
            });
        }
    });

    $("#clear").click(function () {
        $.ajax({
            url: "/" + prefix + "api/ReadWriteCache/Clear",
            cache: false,
            success: function (html) {
                console.log(html);
            }
        });
    });

    $("#fasterStart").click(function () {
        var counter = parseInt($('#counter').val());
        for(index=0;index<counter;index++)
        {
            $.ajax({
                url: "/" + prefix + "api/ReadWriteCache/FasterStart/?numba=1000000",
                cache: false,
                success: function (html) {
                    console.log(html);
                }
            });
        };
    });

    $("#mediumFastStart").click(function () {
        var counter = parseInt($('#counter').val());
        for (index = 0; index < counter; index++) {
            $.ajax({
                url: "/" + prefix + "api/ReadWriteCache/MediumFastGet/?numba=1000000",
                cache: false,
                success: function (html) {
                    console.log(html);
                }
            });
        };
    });


});