socialNetwork.controller('FriendsController',
    function FriendsController($scope, $routeParams, authentication, profileData, usersData) {

        $scope.currentUser = authentication.getUserName();
        $scope.otherUser = $routeParams['username'];

        if ($scope.currentUser === $scope.otherUser) {

            $scope.otherUserName = $scope.currentUser;

            profileData.getOwnFriends()
                .then(
                function successHandler(data) {
                    $scope.friends = data;
                    $scope.friendsCount = data.length;
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        } else {

            usersData.getUserPreviewData($scope.otherUser)
                .then(
                function successHandler(data) {
                    $scope.otherUserName = data.name;
                    usersData.getFriendsDetailedFriendsList($scope.otherUser)
                        .then(
                        function successHandler(data) {
                            $scope.friends = data;
                            $scope.friendsCount = data.length;
                        },
                        function errorHandler(error) {
                            console.log(error);
                        }
                    );
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        }
    });



