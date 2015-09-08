'use strict';

socialNetwork.controller('GroupWallController',
    function GroupWallController($scope, authentication, $location, $routeParams, groupsData, groupPostsData, profileData, notify) {

        if (!authentication.isLogged()) {
            $location.path('/welcome');
            return;
        }

        groupsData.getGroupById($routeParams.id)
            .then(
                function successHandler(data) {
                    $scope.isOwner = data.isOwner;
                    $scope.isMember = data.isMember;
                    $scope.wallName = data.name;
                    $scope.description = data.description;
                    $scope.wallPicture = data.wallPicture;
                    $scope.id = data.id;

                    groupsData.getGroupWall($routeParams.id)
                        .then(
                            function successHandler(data) {
                                $scope.posts = data;
                                if (data.length == 0) {
                                    $scope.isWallEmpty = true;
                                } else {
                                    $scope.isWallEmpty = false;
                                }
                            },
                            function errorHandler(error) {
                                notify.error(error.message);
                            }
                        );
                },
                function errorHandler(error) {
                    notify.error(error.message);
                    $location.path("/users/me");
                }
            );

        $scope.addNewPost = function() {
            var data = {
                postContent: $scope.postContent,
                groupId: $scope.id
            };

            groupPostsData.addPost(data)
                .then(
                    function successHandler(data) {
                        notify.info("Post successful.");
                        $scope.postContent = '';
                        $scope.posts.push(data);
                        // $scope.$emit('addedPost', data.post);
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.deleteCurrentGroup = function() {
            groupsData.deleteGroup($scope.id)
                .then(
                    function successHandler(data) {
                        notify.info("Deleted group successfully.");
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.joinCurrentGroup = function() {
            groupsData.joinGroup($scope.id)
                .then(
                    function successHandler(data) {
                        notify.info("Joined group successfully.");
                        $scope.isMember = true;
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.leaveCurrentGroup = function() {
            groupsData.leaveGroup($scope.id)
                .then(
                    function successHandler(data) {
                        notify.info("Left group successfully.");
                        $scope.isMember = false;
                    },
                    function(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.editCurrentGroup = function() {
            $location.path("/groups/" + $scope.id + "/edit");
        };

        $scope.$on('addedPost', function(event, data) {
            $scope.posts.push(data);
        });

        $scope.$on('deletePost', function(event, data) {
            var index = $scope.posts.indexOf(data);

            if (index > -1) {
                $scope.posts.splice(index, 1);
            }
        });
    });