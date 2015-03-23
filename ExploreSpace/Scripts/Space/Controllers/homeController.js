app.controller('homeController', ['$scope', '$rootScope', function($scope, $rootScope) {


    $scope.testing = ['test', 'testy', 'testes'];

    $scope.players = ['playerOne', 'playerTwo', 'playerThree', 'playerFour'];
    $scope.currentPlayer = '';

    $scope.updatePlayer = function () {
        $rootScope.currentPlayer = $scope.currentPlayer;
        console.info('changed to ' + $scope.currentPlayer);
    };

}]);