var app = angular.module('space', ['ui.router', 'toaster']);

app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/");

    $stateProvider
        // route for the home page
        .state('Home', {
            url: '/',
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
        })
        .state('Profile.Exploring', {
            url: '/Explore',
            templateUrl: '/Scripts/Space/Templates/Actions/explore.html',
            controller: 'exploreController'
        })
        .state('Profile.Influence', {
            url: '/Influence',
            templateUrl: '/Scripts/Space/Templates/Actions/influence.html',
            controller: 'profileController'
        })
        .state('Profile.Research', {
            url: '/Research',
            templateUrl: '/Scripts/Space/Templates/Actions/research.html',
            controller: 'profileController'
        })
        .state('Profile.Upgrade', {
            url: '/Upgrade',
            templateUrl: '/Scripts/Space/Templates/Actions/upgrade.html',
            controller: 'profileController'
        })
        .state('Profile.Build', {
            url: '/Build',
            templateUrl: '/Scripts/Space/Templates/Actions/build.html',
            controller: 'profileController'
        })
        .state('Profile.Move', {
            url: '/Move',
            templateUrl: '/Scripts/Space/Templates/Actions/move.html',
            controller: 'profileController'
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