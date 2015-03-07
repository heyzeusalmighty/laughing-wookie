//app.controller('profileController', function ($scope, $location, $http, toaster, spaceFactory) {
app.controller('profileController', ['$scope', '$location', 'spaceFactory', '$rootScope', 'toaster', function ($scope, $location, spaceFactory, $rootScope, toaster) {

    //Sort out the player situation for testing
    $rootScope.currentPlayer = "player2";
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

    $scope.discs = [
        { cost: -30, occupied: true },
        { cost: -25, occupied: true },
        { cost: -21, occupied: true },
        { cost: -17, occupied: true },
        { cost: -13, occupied: true },
        { cost: -10, occupied: true },
        { cost: -7, occupied: true },
        { cost: -5, occupied: true },
        { cost: -3, occupied: true },
        { cost: -2, occupied: false },
        { cost: -1, occupied: false },
        { cost: 0, occupied: false },
        { cost: 0, occupied: false }

    ];

    $scope.colonyShips = [{ ship: true }, { ship: true }, { ship: false }];

    $scope.takingAction = false;
    $scope.currentAction = '';


    $scope.loading = true;
    spaceFactory.getPlayerInfo().then(function(data) {

        processScienceTrack(data.ScienceTrack);
        $scope.loading = false;
    });



    //=================================
    //  ACTIONS
    //=================================
    
    $scope.takeAction = function(action) {
        if (checkForEnoughDiscs()) {
            if (takeDisc(action)) {

            }
        } else {
            toaster.pop('warning', '', 'Not enough discs');
        }
    };

    $scope.cancelAction = function() {
        $scope.takingAction = false;
        returnDiscForCancelledAction();
    };


    function calculateIncome(uncovered) {
        var inc = [];
        for (var i = 0; i < incomes.length; i++) {
            var newIncome;
            if (incomes[i] > uncovered) {
                newIncome = {
                    income: incomes[i],
                    covered: true
                };
                inc.push(newIncome);
            } else {
                newIncome = {
                    income: incomes[i],
                    covered: false
                };
                inc.push(newIncome);
            }
            
        }
        return inc;
    }


    function processScienceTrack(data) {
        //console.log(data);

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

    function checkForEnoughDiscs() {
        var toBeUsed = $scope.discs[0];
        return toBeUsed.occupied;
    }

    function takeDisc(action) {
        if ($scope.takingAction) {
            toaster.pop('warning', '', 'Already taking another action');
            return false;
        }

        var discs = $scope.discs;

        var toBeUsed = discs[0];
        for (var i = 0; i < discs.length; i++) {
            if (discs[i].occupied && discs[i].cost > toBeUsed.cost) {
                toBeUsed = discs[i];
            }
        }

        if (toBeUsed.occupied == true) {
            toBeUsed.occupied = false;
            $scope.takingAction = true;
            $scope.currentAction = action;
            return true;
        } else {
            return false;
        }
    }

    function returnDiscForCancelledAction() {
        var discs = $scope.discs;
        var badChoice = discs[discs.length - 1];

        for (var i = 0; i < discs.length; i++) {
            if ((!discs[i].occupied) && (discs[i].cost < badChoice.cost)) {
                badChoice = discs[i];
                console.log('new champion => ' + badChoice.cost);
            }

            //if (!discs[i].occupied) {
            //    console.log('disc v. champion => ' + (discs[i].cost < badChoice.cost))
            //    if (discs[i].cost < badChoice.cost) {
            //        badChoice = discs[i];
            //        console.log('new champion => ' + badChoice.cost);
            //    }
            //}
            //console.log(badChoice.cost + " vs. " + discs[i].cost + ' :: ' + !discs[i].occupied + ' :: ' + (discs[i].cost < badChoice.cost));
            //if ((!discs[i].occupied) && (discs[i].cost > badChoice.cost)) {
            //    badChoice = discs[i];
            //    console.log('new champion => ' + badChoice.cost);
            //}
        }
        badChoice.occupied = true;
    }

}]);
