app.controller('adminController', [
    '$scope', '$location', 'spaceFactory', function($scope, $location, spaceFactory) {


        spaceFactory.getRollsAndUsers().then(function(data) {

            console.info(data);

            $scope.users = data.Users;
            $scope.roles = data.Rolls;
        });

        $scope.addToAdmin = function() {

            var user = this.user;
            spaceFactory.addUserToGroup(user.UserName, "Admin").then(function(data) {
                console.info(data);
                if (data == 'Added') {
                    user.IsAdmin = true;
                }
            });
        };

        $scope.removeAdmin = function () {
            var user = this.user;
            spaceFactory.removeUserFromGroup(user.UserName, "Admin").then(function(data) {
                console.info(data);
                if (data == 'Removed') {
                    user.IsAdmin = false;
                }
            });
        };

    }
]);