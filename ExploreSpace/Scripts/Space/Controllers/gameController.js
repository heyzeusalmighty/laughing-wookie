app.controller("gameController", ['$scope','spaceFactory', '$rootScope', '$location', function ($scope, spaceFactory, $rootScope, $location) {


    $scope.isActive = function (route) {
        return route === $location.path();
    }


    //Sort out the player situation for testing
    if ($rootScope.currentPlayer == null || $rootScope.currentPlayer == undefined) {
        console.info('go home');
        $location.path('/');
    } else {
        $scope.currentPlayer = $rootScope.currentPlayer;
    }

    $scope.reset = function() {
        spaceFactory.resetButton().then(function(data) {
            console.log(data);
        });
    }

}]);