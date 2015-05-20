
$(document).ready(function() {

    var central = "rgba(215,40,40,0.8)";
    var divisionOne = "rgba(103,155,153,0.8)";
    var divisionTwo = "rgba(65,129,127,0.8)";
    var divisionThree = "rgba(13, 77, 75, 0.8)";
    var background = "rgba(43, 62, 80, 1)";
    var occupied = "rgba(13,39,74,0.8)";
    var orange = "#bf5a16";
    var brown = "#532709";
    var pink = "#D46A6A";
    var radius = 45;
    var sx = 6;
    var sy = 5;
    var cols = 13;
    var rows = 11;


    var hexagonGrid = new HexagonGrid("HexCanvas", radius, background, orange, brown, pink, cols, rows, true);

    $('#loadingSpinner').show();


    $.ajax({ url: "/api/map" }).done(function(data) {
        $('#loadingSpinner').hide();

        hexagonGrid.processShips(data.Ships);
        
        processMapTiles(data.MapTiles);


    });



    







    //~~~~~~~~~
    //  Buttons
    //~~~~~~~~~
    $('#actionExplore').click(function() {
        $('#actionMessage').show();
        $('#messaging').text('Explore by clicking the hex you want');
        $('#msgPanelHeading').text('Exploring');
        hexagonGrid.toggleExploreMode(true);
    });

    $('span').on('click', 'button.exploreConfirm', function () {
        console.log('do eet');
    });
    

    $('#cancelAction').click(function() {
        $('#actionMessage').hide();
        $('#messaging').text('');
        hexagonGrid.toggleExploreMode(false);
    });




    function processMapTiles(tiles) {
        console.log(tiles);
        for (var i = 0; i < tiles.length; i++) {

            switch (tiles[i].Division) {
                case 1:
                    tiles[i].color = divisionOne;
                    break;
                case 2:
                    tiles[i].color = divisionTwo;
                    break;
                case 3:
                    tiles[i].color = divisionThree;
                    break;
                default:
                    console.log('division not found');
            }

            //hexagonGrid.buildGameHex(tiles[i]);
        }
        hexagonGrid.setGameHexes(tiles);
    }


    function exploreCall(x, y, div) {
        console(x, y, div);
    }

});