app.controller('shipsController',['$scope', 'spaceFactory', function ($scope, spaceFactory) {


    $scope.testing = ['test', 'testy', 'testes'];

    $scope.ships = spaceFactory.getShips();


    $scope.viewShip = function () {
        console.log(this.ship);
    };

}]);