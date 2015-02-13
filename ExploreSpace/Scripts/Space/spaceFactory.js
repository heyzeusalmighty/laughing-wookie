﻿app.factory('spaceFactory', [
    '$http', '$q', function($http, $q) {
        var service = {};

        service.getShips = function() {
            return dummyShips();

        };


        service.getScienceTrack = function() {
            var deferred = $q.defer();
            $http({ method: "GET", url: "/Space/GetScienceTrack" }).success(function(data) {
                deferred.resolve(data);
            }).error(function() {
                deferred.reject('There was an error');
            });
            return deferred.promise;
        };

        service.getPlayerInfo = function() {
            var deferred = $q.defer();
            $http({ method: "GET", url: "/Space/GetPlayerDetails" }).success(function (data) {
                deferred.resolve(data);
            }).error(function () {
                deferred.reject('There was an error');
            });
            return deferred.promise;
        }



        function dummyShips() {
            var ships = [];
            ships.push({
                name: "Interceptor",
                count: 2,
                slots: 4,
                components: [{ name: "Nuclear Source" }, { name: "Ion Cannons" }, { name: "Nuclear Drive" }]
            }, {
                name: "Cruiser",
                count: 1,
                slots: 6,
                components: [{ name: "Nuclear Source" }, { name: "Ion Cannons" }, { name: "Nuclear Drive" }, { name: "Hull" }, { name: "Electron Computer" }]
            });
                        
            return ships;
        }

        function dummyScienceTrack() {
            
        }


        return service;
    }


    
]);