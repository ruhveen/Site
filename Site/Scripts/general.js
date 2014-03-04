

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


    $("#solveMatrix").click(function () {
        var matrix = [];
        $('#tableToSolve tr').each(function (rowIndex, row) {
            matrix[rowIndex] = [];
            $(row).find('td').each(function (colIndex, cell) {
                matrix[rowIndex][colIndex] = cell.innerText;
            });
        });
        jQuery.ajax({
            url: "/" + prefix + "Home/SolveMatrix",
            type: 'POST',
            //data: JSON.stringify(matrix),
            data: 'res=' + JSON.stringify(matrix),
            dataType: 'text',
            success: function (data) {
                var realData = JSON.parse(data);
                $('#tableToSolve tr').each(function (rowIndex, row) {
                    $(row).find('td').each(function (colIndex, cell) {
                        if (realData[rowIndex][colIndex] == "1")
                        {
                            cell.style.background = "green";
                        }
                    });
                });

                if (realData[7][7])
                    alert("Solvable");
                else
                    alert("Not Solvable");
                //console.log(realData);
            }
        });
        //console.log(matrix);
    });


});