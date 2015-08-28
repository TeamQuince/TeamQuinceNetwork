socialNetwork.controller('UserHeaderController',
    function UserHeaderController($scope, $location, authentication, usersData, profileData, notify) {
        var searchX,
            searchY;

        $scope.username = authentication.getUserName();
        $scope.areFriendRequestsVisible = false;
        $scope.areSearchResultsVisible = false;

        usersData.getUserPreviewData($scope.username)
            .then(
            function successHandler(data) {
                $scope.name = data.name;

                if (data.profileImageData) {
                    $scope.profileImageData = data.profileImageData;
                } else {
                    document.getElementById('me-preview').src = "img/noavatar.jpg";
                }

                authentication.setName(data.name);
                authentication.setProfileImageData(data.profileImageData);
            },
            function errorHandler(error) {
                console.log(error);
            }
        );

        profileData.getFriendRequests()
            .then(
            function successHandler(data) {
                $scope.requests = data;
                $scope.requestsCount = data.length;
            },
            function errorHandler(error) {
                console.log(error);
            }
        );

        $scope.logout = function () {
            authentication.logout()
                .then(
                function successHandler(data) {
                    authentication.clearCredentials();
                    notify.info("Logout successful.");
                    $location.path('/');

                },
                function errorHandler(error) {
                    notify.error("Session has expired.");
                    authentication.clearCredentials();
                    $location.path('/');
                }
            );
        };

        $scope.showFriendRequests = function (event) {
            var leftPosition = event.screenX,
                topPosition = event.clientY + 10,
                container = document.getElementById('friendRequestsContainer');

            container.style.top = topPosition + 'px';
            container.style.left = leftPosition + 'px';

            $scope.areFriendRequestsVisible = true;
        };

        $scope.approveFriendRequest = function (request) {
            profileData.approveFriendRequest(request.id)
                .then(
                function successHandler(data) {
                    var index = $scope.requests.indexOf(request);
                    $scope.requests.splice(index, 1);
                    $scope.requestsCount--;
                    notify.info("Friend request accepted.");

                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.rejectFriendRequest = function (request) {
            profileData.rejectFriendRequest(request.id)
                .then(
                function successHandler(data) {
                    var index = $scope.requests.indexOf(request);
                    $scope.requests.splice(index, 1);
                    $scope.requestsCount--;
                    notify.error("Friend request rejected.");
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.searchPeople = function (keyword) {
            if(keyword.length == 0) {
                $scope.areSearchResultsVisible = false;
                return;
            }

            usersData.searchUsersByName(keyword)
                .then(
                function successHandler(data) {
                    $scope.people = data;
                    $scope.peopleCount = data.length;
                    $scope.showSearchResults();
                },
                function (error) {
                    console.log(error);
                }
            );
        };

        $scope.setCoordinates = function ($event) {
            searchX = $event.currentTarget.offsetLeft;
            searchY = $event.currentTarget.offsetTop;
        };

        $scope.showSearchResults = function ($event, keyword) {
            if(keyword.length == 0) {
                $scope.areSearchResultsVisible = false;
                return;
            }

            usersData.searchUsersByName(keyword)
                .then(
                function successHandler(data) {
                    $scope.people = data;
                    $scope.peopleCount = data.length;

                    var leftPosition = searchX,
                        topPosition = searchY + 40,
                        container = document.getElementById('peopleSearchContainer');

                    container.style.top = topPosition + 'px';
                    container.style.left = leftPosition + 'px';

                    $scope.areSearchResultsVisible = true;
                },
                function (error) {
                    console.log(error);
                }
            );
        };
    });