socialNetwork.controller('UserFriendsController',
    function UserFriendsController($scope, authentication, usersData) {

        usersData.getFriendsFriendsPreview($scope.test)
            .then(
            function successHandler(data) {
                $scope.friends = data.friends;
                $scope.friendsCount = data.totalCount;
            },
            function errorHandler(error) {
                console.log(error);
            }
        );
    });