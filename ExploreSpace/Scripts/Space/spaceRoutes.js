var app = angular.module('space', ['ngRoute']);

app.config(function ($routeProvider) {

    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: '/Scripts/Space/Templates/home.html',
            controller: 'homeController'
        })
        .when('/Profile', {
            templateUrl: '/Scripts/Space/Templates/profile.html',
            controller: 'profileController'
        })
        .when('/Messages', {
            templateUrl: '/Scripts/Space/Templates/messages.html',
            controller: 'messagesController'
        })
        .when("/Ships", {
            templateUrl: '/Scripts/Space/Templates/ships.html',
            controller: 'shipsController'
        })
        .when("/Map", {
            templateUrl: '/Scripts/Space/Templates/map.html',
            controller: 'mapController'
})
    ;

});