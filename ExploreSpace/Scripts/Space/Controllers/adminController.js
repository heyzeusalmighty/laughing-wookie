app.controller('adminController', [
    '$scope', '$location', 'spaceFactory', function($scope, $location, spaceFactory) {


        spaceFactory.getRollsAndUsers().then(function(data) {

            console.info(data);

            $scope.users = data.Users;
            $scope.roles = data.Rolls;
        });

        $scope.addToAdmin = function() {

            
            spaceFactory.addUserToGroup(this.user.UserName, "Admin").then(function(data) {
                console.info(data);
            });
        };

    }
]);