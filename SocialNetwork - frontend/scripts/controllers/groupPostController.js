socialNetwork.controller('GroupPostController',
    function GroupPostController($scope, $document, $modal, authentication, groupPostsData, groupCommentsData, usersData, profileData, notify) {

        $scope.isUserPreviewVisible = false;
        $scope.showComments = false;

        $scope.showCommentForm = function() {
            $scope.commentFormVisible = !$scope.commentFormVisible;
        };

        usersData.getUserPreviewData($scope.post.authorUsername)
            .then(
                function successHandler(data) {

                    $scope.posterData = data;
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );

        $scope.addComment = function() {
            groupCommentsData.addCommentToPost($scope.post.id, $scope.commentContent)
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
            groupPostsData.likePostById($scope.post.id)
                .then(
                    function successHandler(data) {
                        notify.info('Post liked.');
                        $scope.post.liked = true;
                        groupPostsData.getPostPreviewLikes($scope.post.id)
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
            groupPostsData.unlikePostById($scope.post.id)
                .then(
                    function successHandler(data) {
                        notify.info('Post unliked');
                        $scope.post.liked = false;
                        groupPostsData.getPostPreviewLikes($scope.post.id)
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

        $scope.inviteFriend = function() {
            profileData.sendFriendRequest($scope.post.authorUsername)
                .then(
                    function successHandler(data) {
                        notify.info("Invitation sent.")
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.previewUser = function() {
            $scope.isUserPreviewVisible = true;
            $scope.isMe = $scope.post.AuthorUsername === authentication.getUserName() ? true : false;
        };

        $scope.toggleComments = function() {
            $scope.showComments = !$scope.showComments;
        };

        $scope.deletePost = function() {
            groupPostsData.deletePostById($scope.post.id)
                .then(
                    function successHandler(data) {
                        notify.info("Post deleted.");
                        $scope.$emit('deletePost', $scope.post);
                    },
                    function errorHandler(error) {
                        notify.error(error.Message);
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
                    groupPostsData.editPostById(response, $scope.post.id)
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

        function verifyEditOperation(posting) {
            var currentUser = authentication.getUserName();

            //If it is an own post:
            if (currentUser === posting.authorUsername) {
                return true;
            }

            return false;
        }
    });