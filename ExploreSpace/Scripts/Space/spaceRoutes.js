var app = angular.module('space', ['ui.router', 'toaster']);

app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/home");

    $stateProvider
        // route for the home page
        .state('Home', {
            url: '/Home',
            templateUrl: '/Scripts/Space/Templates/home.html',
            controller: 'homeController'
        })
        .state('Profile', {
            url: '/Profile',
            templateUrl: '/Scripts/Space/Templates/profile.html',
            controller: 'profileController'
        })
        .state('Messages', {
            url: '/Messages',
            templateUrl: '/Scripts/Space/Templates/messages.html',
            controller: 'messagesController'
        })
        .state("Ships", {
            url: '/Ships',
            templateUrl: '/Scripts/Space/Templates/ships.html',
            controller: 'shipsController'
        })
        .state("Map", {
            url: "/Map",
            templateUrl: '/Scripts/Space/Templates/map.html',
            controller: 'mapController'
        })
        .state("Game", {
            url: "/Game",
            templateUrl: '/Scripts/Space/Templates/game.html',
            controller: 'gameController'
        });

}]);


app.directive('menuBar', function() {
    return {
        restrict: 'AE',
        //replace: true,
        templateUrl: '/Scripts/Space/Templates/menu.html',
        controller: 'menuController'
    }
});