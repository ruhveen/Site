

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

    $("#createNewMatrix").click(function () {
        jQuery.ajax({
            url: "/" + prefix + "Home/NewMatrix",
            type: 'Get',
            success: function (data) {
                FillTable(data);
            }
        });
    });

    
});



function FillTable(data) {
    $('#tableToSolve').empty();
    var realData = $.parseJSON(data);

    for (var i = 0; i < realData.length; i++) {
        var newRow = $('<tr></tr>');
        for (var j = 0; j < realData[i].length; j++) {

            $('<td></td>').text(realData[i][j]).appendTo(newRow);
        }
        $('#tableToSolve').append(newRow);
    }


}