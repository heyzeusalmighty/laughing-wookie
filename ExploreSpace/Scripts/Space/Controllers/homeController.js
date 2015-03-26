app.controller('homeController', ['$scope', '$rootScope', 'spaceFactory', function($scope, $rootScope, spaceFactory) {


    $scope.testing = ['test', 'testy', 'testes'];

    $scope.players = ['playerOne', 'playerTwo', 'playerThree', 'playerFour'];
    $scope.currentPlayer = '';

    $scope.updatePlayer = function () {
        $rootScope.currentPlayer = $scope.currentPlayer;
        console.info('changed to ' + $scope.currentPlayer);
    };

    $scope.updateGame = function() {
        $rootScope.currentGame = $scope.currentGame;
        console.info('changed to ', $rootScope.currentGame);
    };

    

    spaceFactory.getAllGames().then(function(data) {
        $scope.games = data;
    });

}]);