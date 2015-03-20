app.controller('mapController', function ($scope, $location, $http, spaceFactory) {


    $scope.whatIsIt = function() {

        console.log('taking action', $scope.takingAction);
    };

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

    var hexagonGrid = new HexagonGrid("HexCanvas", radius, background, orange, brown, pink, cols, rows);
    //hexagonGrid.drawHexGrid(11, 13, radius, radius, true);

    //Centralish 
    //hexagonGrid.redrawHexAtColRow(sx, sy, central);

    $scope.loading = true;
    spaceFactory.getMapTiles().then(function (data) {

        $scope.mapTiles = data.MapTiles;

        processMapTiles($scope.mapTiles);

        $scope.tileCounts = data.Counts;

        $scope.loading = false;
    });


    function processMapTiles(tiles) {
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

    

   

});