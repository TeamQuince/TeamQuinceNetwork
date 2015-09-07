socialNetwork.controller('GroupCommentController',
    function GroupCommentController($scope, $modal, authentication, groupCommentsData, usersData, profileData, notify) {

        toLocalTimeZone($scope.comment);

        $scope.isUserPreviewVisible = false;

        $scope.showReplyForm = function() {
            $scope.replyFormVisible = !$scope.replyFormVisible;
        };

        usersData.getUserPreviewData($scope.comment.authorUsername)
            .then(
                function successHandler(data) {
                    $scope.commenterData = data;
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );

        $scope.addComment = function() {
            groupCommentsData.addCommentToPost($scope.post.id, $scope.replyContent)
                .then(
                    function successHandler(data) {
                        notify.info("Commented successfully.");
                        $scope.commentContent = '';
                        $scope.replyFormVisible = false;
                        $scope.post.comments.push(data);
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.likeComment = function(commentObject) {
            groupCommentsData.likeComment($scope.post.id, commentObject.id)
                .then(
                    function successHandler(data) {
                        notify.info('Comment liked.');
                        $scope.comment.liked = true;
                        groupCommentsData.getCommentPreviewLikes($scope.post.id, commentObject.id)
                            .then(
                                function successHandler(likesData) {
                                    $scope.comment.likesCount = likesData.totalLikeCount;
                                }
                            );
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.unlikeComment = function(commentObject) {
            groupCommentsData.unlikeComment($scope.post.id, commentObject.id)
                .then(
                    function successHandler(data) {
                        notify.info('Comment unliked');
                        $scope.comment.liked = false;
                        groupCommentsData.getCommentPreviewLikes($scope.post.id, commentObject.id)
                            .then(
                                function successHandler(likesData) {
                                    $scope.comment.likesCount = likesData.totalLikeCount;
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
        };

        $scope.inviteFriend = function() {
            profileData.sendFriendRequest($scope.comment.authorUsername)
                .then(
                    function successHandler(data) {
                        notify.info("Invitation sent.");
                        $scope.commenterData.hasPendingRequest = true;
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.deleteComment = function() {
            groupCommentsData.deletePostComment($scope.post.id, $scope.comment.id)
                .then(
                    function successHandler(data) {
                        notify.info("Comment deleted.");
                        $scope.commentContent = '';
                        $scope.$root.$broadcast('deleteComment', $scope.comment);

                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };

        $scope.open = function(modalName) {

            if (!verifyEditOperation($scope.comment)) {
                notify.error("Edit allowed for own comments only.");
                return;
            }

            var modalInstance = $modal.open({
                templateUrl: 'partials/directives/edit-posting.html',
                controller: 'EditPostingController',
                resolve: {
                    'posting': function() {
                        return $scope.comment;
                    }
                }
            });

            modalInstance.result.then(
                function edit(response) {
                    groupCommentsData.editPostComment($scope.post.id, $scope.comment.id, response)
                        .then(
                            function successHandler(data) {
                                $scope.comment.commentContent = response;
                                notify.info("Comment edited.");
                            },
                            function(error) {

                            }
                        );
                },
                function cancelEdit() {
                    console.log('Modal dismissed at: ' + new Date());
                });
        };

        function verifyEditOperation(posting) {
            var currentUser = authentication.getUserName();

            if (currentUser === posting.authorUsername) {
                return true;
            }

            return false;
        }

        function toLocalTimeZone(post) {
            post.date = new Date(post.date);
        }
    });