app.controller('mapController', function ($scope, $location, $http, spaceFactory) {


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

    var hexagonGrid = new HexagonGrid("HexCanvas", radius, background, orange, brown, pink);
    hexagonGrid.drawHexGrid(11, 13, radius, radius, true);

    //Centralish 
    hexagonGrid.redrawHexAtColRow(sx, sy, central);

    spaceFactory.getMapTiles().then(function(data) {
        for (var i = 0; i < data.length; i++) {

            switch (data[i].Division) {
                case 1:
                    data[i].color = divisionOne;
                    break;
                case 2:
                    data[i].color = divisionTwo;
                    break;
                case 3:
                    data[i].color = divisionThree;
                    break;
                default:
                    console.log('division not found');
            }

            hexagonGrid.buildGameHex(data[i]);
        }
    });

    

   

});