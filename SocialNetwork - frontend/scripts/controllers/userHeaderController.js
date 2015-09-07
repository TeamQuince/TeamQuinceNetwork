socialNetwork.controller('UserHeaderController',
    function UserHeaderController($scope, $location, authentication, usersData, groupsData, profileData, notify) {
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
                    notify.error(error.message);
                }
            );

        profileData.getFriendRequests()
            .then(
                function successHandler(data) {
                    $scope.requests = data;
                    $scope.requestsCount = data.length;
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );

        $scope.logout = function() {
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

        $scope.showFriendRequests = function(event) {
            var leftPosition = event.screenX,
                topPosition = event.clientY + 10,
                container = document.getElementById('friendRequestsContainer');

            container.style.top = topPosition + 'px';
            container.style.left = leftPosition + 'px';

            $scope.areFriendRequestsVisible = true;
        };

        $scope.approveFriendRequest = function(request) {
            profileData.approveFriendRequest(request.id)
                .then(
                    function successHandler(data) {
                        var index = $scope.requests.indexOf(request);
                        $scope.requests.splice(index, 1);
                        $scope.requestsCount--;
                        notify.info("Friend request accepted.");

                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.rejectFriendRequest = function(request) {
            profileData.rejectFriendRequest(request.id)
                .then(
                    function successHandler(data) {
                        var index = $scope.requests.indexOf(request);
                        $scope.requests.splice(index, 1);
                        $scope.requestsCount--;
                        notify.error("Friend request rejected.");
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.searchPeople = function(keyword) {
            if (keyword.length == 0) {
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
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.searchGroups = function(groupskeyword) {
            if (groupskeyword.length == 0) {
                $scope.areGroupSearchResultsVisible = false;
                return;
            }

            groupsData.searchGroupsByName(groupskeyword)
                .then(
                    function successHandler(data) {
                        $scope.groups = data;
                        $scope.groupsCount = data.length;
                        $scope.showGroupSearchResults();
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.setCoordinates = function($event) {
            searchX = $event.currentTarget.offsetLeft;
            searchY = $event.currentTarget.offsetTop;
        };

        $scope.showSearchResults = function($event, keyword) {
            if (keyword.length == 0) {
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
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.showGroupSearchResults = function($event, groupskeyword) {
            if (groupskeyword.length == 0) {
                $scope.areGroupSearchResultsVisible = false;
                return;
            }

            groupsData.searchGroupsByName(groupskeyword)
                .then(
                    function successHandler(data) {
                        $scope.groups = data;
                        $scope.groupsCount = data.length;

                        var leftPosition = searchX,
                            topPosition = searchY + 40,
                            container = document.getElementById('groupSearchContainer');

                        container.style.top = topPosition + 'px';
                        container.style.left = leftPosition + 'px';

                        $scope.areGroupSearchResultsVisible = true;
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.joinGroup = function(group) {
            groupsData.joinGroup(group.id)
                .then(
                    function successHandler(data) {
                        notify.info("Joined group successfully.");
                        $location.path('/groups/' + group.id);
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.leaveGroup = function(group) {
            groupsData.leaveGroup(group.id)
                .then(
                    function successHandler(data) {
                        notify.info("Left group successfully.");
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.deleteGroup = function(group) {
            groupsData.deleteGroup(group.id)
                .then(
                    function successHandler(data) {
                        notify.info("Deleted group successfully.");
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };
    });