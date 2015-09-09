socialNetwork.controller('PostController',
    function PostController($scope, $document, $modal, authentication, postsData, commentsData, usersData, profileData, notify) {

        toLocalTimeZone($scope.post);

        $scope.isUserPreviewVisible = false;
        $scope.showComments = false;
        $scope.isMe = authentication.getUserName() === $scope.post.authorUsername ? true : false;

        commentsData.getPostComments($scope.post.id)
            .then(
                function successHandler(data) {
                    $scope.post.comments = data;
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );

        usersData.getUserPreviewData($scope.post.authorUsername)
            .then(
                function successHandler(data) {
                    data.hasPendingRequest = data.hasPendingRequest == 'true' ? true : false;
                    data.isFriend = data.isFriend == 'true' ? true : false;
        $scope.posterData = data;
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );

        $scope.showCommentForm = function() {
            $scope.commentFormVisible = !$scope.commentFormVisible;
        };

        $scope.addComment = function() {
            commentsData.addCommentToPost($scope.post.id, $scope.commentContent)
                .then(
                    function successHandler(data) {
                        notify.info("Commented successfully.");
                        $scope.commentContent = '';
                        $scope.post.comments.push(data);
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
            $scope.commentFormVisible = false;
        };

        $scope.likePost = function() {
            postsData.likePostById($scope.post.id)
                .then(
                    function successHandler(data) {
                        notify.info('Post liked.');
                        $scope.post.liked = true;
                        postsData.getPostPreviewLikes($scope.post.id)
                            .then(
                                function successHandler(likesData) {
                                    $scope.post.likesCount = likesData.totalLikeCount;
                                }
                            );
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.unlikePost = function() {
            postsData.unlikePostById($scope.post.id)
                .then(
                    function successHandler(data) {
                        notify.info('Post unliked');
                        $scope.post.liked = false;
                        postsData.getPostPreviewLikes($scope.post.id)
                            .then(
                                function successHandler(likesData) {
                                    $scope.post.likesCount = likesData.totalLikeCount;
                                }
                            );
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.previewUser = function() {
            $scope.isUserPreviewVisible = true;
            $scope.isMe = $scope.post.author.username === authentication.getUserName() ? true : false;
        };

        $scope.inviteFriend = function() {
            profileData.sendFriendRequest($scope.post.authorUsername)
                .then(
                    function successHandler(data) {
                        $scope.posterData.hasPendingRequest = true;
                        notify.info("Invitation sent.");
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.toggleComments = function() {
            $scope.showComments = !$scope.showComments;
        };

        $scope.deletePost = function() {
            postsData.deletePostById($scope.post.id)
                .then(
                    function successHandler(data) {
                        notify.info("Post deleted.");
                        $scope.$emit('deletePost', $scope.post);
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };


        $scope.open = function(modalName) {

            if (!verifyEditOperation($scope.post)) {
                notify.error("Edit allowed for own posts only.");
                return;
            }

            var modalInstance = $modal.open({
                templateUrl: 'partials/directives/edit-posting.html',
                controller: 'EditPostingController',
                resolve: {
                    'posting': function() {
                        return $scope.post;
                    }
                }
            });

            modalInstance.result.then(
                function edit(response) {
                    postsData.editPostById(response, $scope.post.id)
                        .then(
                            function successHandler(data) {
                                $scope.post.postContent = response;
                                notify.info("Post edited.");
                            },
                            function(error) {

                            }
                        );
                },
                function cancelEdit() {
                    console.log('Modal dismissed at: ' + new Date());
                });
        };

        $scope.$on('deleteComment', function(event, data) {
            var index = $scope.post.comments.indexOf(data);

            if (index > -1) {
                $scope.post.comments.splice(index, 1);
            }
        });


        function verifyDeleteOperation(posting) {
            var currentUser = authentication.getUserName();

            //If it is an own post:
            if (currentUser === posting.author.username) {
                return true;
            }

            //If it is a post on the user's wall:
            if (currentUser === posting.wallOwner.username) {
                return true;
            }

            return false;
        }

        function verifyEditOperation(posting) {
            var currentUser = authentication.getUserName();

            //If it is an own post:
            if (currentUser === posting.authorUsername) {
                return true;
            }

            return false;
        }

        function verifyLikePostOperation(posting) {
            var currentUser = authentication.getUserName();

            if (currentUser === posting.author.username) {
                return true;
            }

            if (currentUser === posting.wallOwner.username) {
                return true;
            }

            if ($scope.post.author.isFriend) {
                return true;
            }

            if ($scope.post.wallOwner.isFriend) {
                return true;
            }

            return false;
        }

        function toLocalTimeZone(post) {
            post.date = new Date(post.date);
        }

    });