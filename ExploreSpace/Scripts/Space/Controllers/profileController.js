//app.controller('profileController', function ($scope, $location, $http, toaster, spaceFactory) {
app.controller('profileController', function ($scope, $location, spaceFactory, $rootScope) {

    $scope.testing = ['test', 'testy', 'testes'];


    //Sort out the player situation for testing
    if ($rootScope.currentPlayer == null || $rootScope.currentPlayer == undefined) {
        console.info('go home');
        $location.path('/');
    } else {
        $scope.currentPlayer = $rootScope.currentPlayer;
    }

    //$scope.ships = spaceFactory.getShips();


    //$scope.viewShip = function () {
    //    console.log(this.ship);
    //};


    var incomes = [28, 24, 21, 18, 15, 12, 10, 8, 6, 4, 3, 2];

    $scope.orange = calculateIncome(6);
    $scope.pink = calculateIncome(5);
    $scope.brown = calculateIncome(10);

    $scope.currentOrange = 5;
    $scope.currentPink = 10;
    $scope.currentBrown = 2;

    $scope.gearTrack = [];
    $scope.starTrack = [];
    $scope.gridTrack = [];

    //spaceFactory.getScienceTrack().then(function(data) {
    //    console.log(data);

        
        
    //});

    spaceFactory.getPlayerInfo().then(function(data) {

        processScienceTrack(data.ScienceTrack);

    });


    function calculateIncome(uncovered) {
        var inc = [];
        for (var i = 0; i < incomes.length; i++) {
            if (incomes[i] > uncovered) {
                var newIncome = {
                    income: incomes[i],
                    covered: true
                };
                inc.push(newIncome);
            } else {
                var newIncome = {
                    income: incomes[i],
                    covered: false
                };
                inc.push(newIncome);
            }
            
        }
        return inc;
    }


    function processScienceTrack(data) {
        console.log(data);

        for (var i = 0; i < data.GearTiles.length; i++) {
            data.GearTiles[i].CostReduction = (data.GearTiles[i].CostReduction > 0) ? -data.GearTiles[i].CostReduction : 0;
            $scope.gearTrack.push(data.GearTiles[i]);
        }

        for (i = 0; i < data.StarTiles.length; i++) {
            data.StarTiles[i].CostReduction = (data.StarTiles[i].CostReduction > 0) ? -data.StarTiles[i].CostReduction : 0;

            //if (data.StarTiles[i].Position == 1) {
            //    data.StarTiles[i].Tile = { name: "Star Base", img: "blankScienceTile.svg" };
            //}
            $scope.starTrack.push(data.StarTiles[i]);
        }

        for (i = 0; i < data.GridTiles.length; i++) {
            data.GridTiles[i].CostReduction = (data.GridTiles[i].CostReduction > 0) ? -data.GridTiles[i].CostReduction : 0;
            $scope.gridTrack.push(data.GridTiles[i]);
        }

        $scope.scienceTrack = data;
    }

});