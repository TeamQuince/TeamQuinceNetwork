socialNetwork.controller('FriendsSidebarController',
    function FriendsSidebarController($scope, authentication, usersData, sidebarUsername) {

        $scope.sidebarUsername = sidebarUsername;

        usersData.getFriendsFriendsPreview($scope.sidebarUsername)
            .then(
            function successHandler(data) {
                $scope.username = $scope.sidebarUsername;
                $scope.friends = data.friends;
                $scope.friendsCount = data.totalCount;
            },
            function errorHandler(error) {
                console.log(error);
            }
        );

        usersData.getUserPreviewData($scope.sidebarUsername)
            .then(
            function successHandler(data) {
                $scope.name = data.name;
            },
            function errorHandler(error) {
                console.log(error);
            }
        );


    });



