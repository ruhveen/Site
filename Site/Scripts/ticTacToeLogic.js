var GAME_BOARD = null;
var IS_FIRST_PLAYER_TURN = true;
var BOARD_SIZE = 3;
var EMPTY_CELL = -100;

function initializeGameBoard()
{
    GAME_BOARD = [];

    // There is Probably an easier way of initializing the array
    for (var i = 0; i < BOARD_SIZE; i++) {
        GAME_BOARD[i] = [];
        for (var j = 0; j < BOARD_SIZE; j++) {
            GAME_BOARD[i][j] = EMPTY_CELL;
        }
    }
}

function declareWinner(winner) {
    if (winner == 1)
        printMessage('First Player is the Winner');
    else if (winner == 0)
        printMessage('Second Player is the Winner');
    else
        printMessage('Tie');

    // Unbind all click events when game is over.
    $('tbody td').unbind('click');

}

function allFilled() {
    for (var i = 0; i < BOARD_SIZE; i++) {
        for (var j = 0; j < BOARD_SIZE; j++) {
            if (GAME_BOARD[i][j] < 0) {
                return false;
            }
        }
    }
    return true;
}
function getWinner() {
    // Effiency is negligable in this case.

    // Check rows
    for (var i = 0; i < BOARD_SIZE; i++) {
        var sumCurrentLine = 0;
        for (var j = 0; j < BOARD_SIZE; j++) {
            sumCurrentLine += GAME_BOARD[i][j];
        }
        if (sumCurrentLine == 0 || sumCurrentLine == BOARD_SIZE)
            return (sumCurrentLine == BOARD_SIZE);
    }

    // Check cols
    for (var i = 0; i < BOARD_SIZE; i++) {
        var sumCurrentLine = 0;
        for (var j = 0; j < BOARD_SIZE; j++) {
            sumCurrentLine += GAME_BOARD[j][i];
        }
        if (sumCurrentLine == 0 || sumCurrentLine == BOARD_SIZE)
            return (sumCurrentLine == BOARD_SIZE);
    }

    // Check diagonals
    var sumCurrentDiagonal = 0;
    for (var j = 0; j < BOARD_SIZE; j++) {
        sumCurrentDiagonal += GAME_BOARD[j][j];
    }
    if (sumCurrentDiagonal == 0 || sumCurrentDiagonal == BOARD_SIZE)
        return (sumCurrentDiagonal == BOARD_SIZE);

    sumCurrentDiagonal = 0;
    for (var j = 0; j < BOARD_SIZE; j++) {
        sumCurrentDiagonal += GAME_BOARD[j][BOARD_SIZE - 1 - j];
    }
    if (sumCurrentDiagonal == 0 || sumCurrentDiagonal == BOARD_SIZE)
        return (sumCurrentDiagonal == BOARD_SIZE);

    return -1;
}

function handlePlay(event)
{
    var row = event.data.row;
    var col = event.data.col;

    clearMessage();

    // if move legal - Out of bounds
    if (!(row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE))
    {
        printMessage('Illegal move, out of bounds!');
        return;
    }

    if (GAME_BOARD[row][col] == EMPTY_CELL) {
        GAME_BOARD[row][col] = IS_FIRST_PLAYER_TURN;

        var classToAdd = "firstPlayer";
        if (!IS_FIRST_PLAYER_TURN) {
            classToAdd = "secondPlayer";
        }

        event.target.classList.add(classToAdd)

        if ($('#showColorButtons').is(":checked"))
        {
            event.target.classList.add('colorButtons');

        }

        var currentWinner = getWinner();
        if (currentWinner != -1 || allFilled()) {

            declareWinner(currentWinner);
        }
        else {
            IS_FIRST_PLAYER_TURN = !IS_FIRST_PLAYER_TURN;

            if (IS_FIRST_PLAYER_TURN) {
                printMessage("First player's turn");
            }
            else
            {
                printMessage("Second player's turn");
            }
        }
    }
    else
    {
        printMessage('This cell was already selected!');
    }
}

function clearMessage()
{
    printMessage("");
}

function printMessage(message)
{
    $('#gameStatus').text(message);
}

function addTable(gameBoard) {
    var myTable = document.getElementById('ticTacToeTable');

    var y = document.createElement('tr');
    var tBody = document.createElement('tbody');
    myTable.appendChild(tBody);

    for (var i = 0 ; i < gameBoard.length ; i++) {
        var row = gameBoard[i];
        var newRow = document.createElement('tr');
        for (var j = 0 ; j < row.length ; j++) {
            tBody.appendChild(newRow);
            var newCell = document.createElement('td');
            newRow.appendChild(newCell);
        }
    }

    bindClickEvents(gameBoard);
}

function bindClickEvents()
{
    var rowIndex = 0;
    $("#ticTacToeTable tr").each(function () {

        var colIndex = 0;
        $('td', this).each(function () {

            $(this).click({ row: rowIndex, col: colIndex }, handlePlay);
            colIndex++;
        })
        rowIndex++;
    })
}

$(document).ready(function () {
    initializeGameBoard();
    addTable(GAME_BOARD);
    printMessage("First player's turn");

    $("#newGame").click(function () {
        
        initializeGameBoard();

        // Unbind all click events in the case a user clicked New Game
        // before the previous game ended.
        $('tbody td').unbind('click');
        bindClickEvents();
        $('tbody td').removeClass();
        printMessage("First player's turn");
    });

    $("#showColorButtons").click(function () {

        if ($(this).is(':checked')) {
            $('tbody td').addClass('colorButtons');
        }
        else
        {
            $('tbody td').removeClass('colorButtons');
        }
    });
});
