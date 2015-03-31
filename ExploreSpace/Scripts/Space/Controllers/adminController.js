app.controller('adminController', [
    '$scope', '$location', 'spaceFactory', 'toaster', function($scope, $location, spaceFactory, toaster) {


    $scope.editingUser = false;
    $scope.user = {};
    $scope.roleRow = false;
    $scope.newRole = "";


    spaceFactory.getRollsAndUsers().then(function(data) {
        $scope.users = data.Users;
        $scope.roles = data.Rolls;
    });

    $scope.addToAdmin = function() {
        var user = $scope.user;
        spaceFactory.addUserToGroup(user.UserName, "Admin").then(function(data) {
            //console.info(data);
            if (data == 'Added') {
                user.IsAdmin = true;
            }
        });
    };

    $scope.removeAdmin = function() {
        var user = $scope.user;
        spaceFactory.removeUserFromGroup(user.UserName, "Admin").then(function(data) {
            //console.info(data);
            if (data == 'Removed') {
                user.IsAdmin = false;
            }
        });
    };

    $scope.addToPlayers = function () {
        var user = $scope.user;
        spaceFactory.addUserToGroup(user.UserName, "Player").then(function (data) {
            //console.info(data);
            if (data == 'Added') {
                user.IsPlayer = true;
            }
        });
    };

    $scope.removeFromPlayers = function () {
        var user = $scope.user;
        spaceFactory.removeUserFromGroup(user.UserName, "Player").then(function (data) {
            //console.info(data);
            if (data == 'Removed') {
                user.IsPlayer = false;
            }
        });
    };


    $scope.editUser = function() {
        $scope.user = this.user;
        $scope.editingUser = true;
    };

    $scope.cancelEditUser = function() {
        $scope.user = {};
        $scope.editingUser = false;
    }

    $scope.addRole = function() {
        //toaster.pop('success', '', 'Added');
        $scope.roleRow = true;
        $scope.newRole = "";
    };

    $scope.createRole = function() {
        if ($scope.newRole.length > 1) {


            spaceFactory.addNewRole($scope.newRole).then(function(data) {

                if (data == 'Success') {
                    toaster.pop('success', '', data);
                    $scope.roles.push($scope.newRole);
                    $scope.roleRow = false;
                } else {
                    toaster.pop('warning', '', data);
                }
            });

            


        } else {
            toaster.pop('warning', '', 'You gotta have something in there dude');
        }
    }

    $scope.cancelCreateRole = function() {
        $scope.roleRow = false;
        $scope.newRole = "";
    }

}]);