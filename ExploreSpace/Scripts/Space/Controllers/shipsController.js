app.controller('shipsController', function ($scope, $location, $http, spaceFactory) {


    $scope.testing = ['test', 'testy', 'testes'];

    $scope.ships = spaceFactory.getShips();


    $scope.viewShip = function () {
        console.log(this.ship);
    };

});