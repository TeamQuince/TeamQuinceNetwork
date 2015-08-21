socialNetwork.controller('CommentController',
    function CommentController($scope, $modal, authentication, commentsData, usersData, profileData, notify) {

        toLocalTimeZone($scope.comment);

        $scope.isUserPreviewVisible = false;

        $scope.showReplyForm = function () {
            $scope.replyFormVisible = !$scope.replyFormVisible;
        };

        usersData.getUserPreviewData($scope.comment.author.username)
            .then(
            function successHandler(data) {
                $scope.commenterData = data;
            },
            function errorHandler(error) {
                console.log(error);
            }
        );

        $scope.addComment = function () {

            if (!verifyCommentOperation()) {
                notify.error("You can only comment on posts of your friends or posts on their walls.");
                return;
            }

            commentsData.addCommentToPost($scope.post.id, $scope.replyContent)
                .then(
                function successHandler(data) {
                    notify.info("Commented successfully.");
                    $scope.commentContent = '';
                    $scope.replyFormVisible = false;
                    $scope.post.comments.push(data);
                },
                function errorHandler(error) {
                    notify.error("Comment failed.");
                }
            );
        };

        $scope.likeComment = function (commentObject) {

            if (!verifyCommentOperation()) {
                notify.error("You can only like comments of your friends or comments on friends' walls.");
                return;
            }

            commentsData.likeComment($scope.post.id, commentObject.id)
                .then(
                function successHandler(data) {
                    notify.info('Comment liked.');
                    $scope.comment.liked = true;
                    commentsData.getCommentPreviewLikes($scope.post.id, commentObject.id)
                        .then(
                        function successHandler(likesData) {
                            $scope.comment.likesCount = likesData.totalLikeCount;
                        }
                    );
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.unlikeComment = function (commentObject) {
            commentsData.unlikeComment($scope.post.id, commentObject.id)
                .then(
                function successHandler(data) {
                    notify.info('Comment unliked');
                    $scope.comment.liked = false;
                    commentsData.getCommentPreviewLikes($scope.post.id, commentObject.id)
                        .then(
                        function successHandler(likesData) {
                            $scope.comment.likesCount = likesData.totalLikeCount;
                        }
                    );
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.previewUser = function () {
            $scope.isUserPreviewVisible = true;
        };

        $scope.inviteFriend = function () {
            profileData.sendFriendRequest($scope.comment.author.username)
                .then(
                function successHandler(data) {
                    notify.info("Invitation sent.");
                    $scope.commenterData.hasPendingRequest = true;
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.deleteComment = function () {

            if (!verifyDeleteOperation($scope.comment)) {
                notify.error("Delete allowed for own comments.");
                return;
            }

            commentsData.deletePostComment($scope.post.id, $scope.comment.id)
                .then(
                function successHandler(data) {
                    notify.info("Comment deleted.");
                    $scope.commentContent = '';
                    $scope.$root.$broadcast('deleteComment', $scope.comment);

                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.open = function (modalName) {

            if (!verifyEditOperation($scope.comment)) {
                notify.error("Edit allowed for own comments only.");
                return;
            }

            var modalInstance = $modal.open({
                templateUrl: 'partials/directives/edit-posting.html',
                controller: 'EditPostingController',
                resolve: {
                    'posting': function () {
                        return $scope.comment;
                    }
                }
            });

            modalInstance.result.then(
                function edit(response) {
                    commentsData.editPostComment($scope.post.id, $scope.comment.id, response)
                        .then(
                        function successHandler(data) {
                            $scope.comment.commentContent = response;
                            notify.info("Comment edited.");
                        },
                        function (error) {

                        }
                    );
                },
                function cancelEdit() {
                    console.log('Modal dismissed at: ' + new Date());
                });
        };

        function verifyCommentOperation() {
            if ($scope.post.author.isFriend) {
                return true;
            }

            if ($scope.post.wallOwner.isFriend) {
                return true;
            }

            if ($scope.post.author.username === authentication.getUserName()) {
                return true;
            }

            return false;
        }

        function verifyDeleteOperation(posting) {
            var currentUser = authentication.getUserName();

            if (currentUser === posting.author.username) {
                return true;
            }

            if (currentUser === $scope.post.author.username) {
                return true;
            }

            return false;
        }

        function verifyEditOperation(posting) {
            var currentUser = authentication.getUserName();

            if (currentUser === posting.author.username) {
                return true;
            }

            return false;
        }

        function toLocalTimeZone (post) {
            post.date = new Date(post.date);
        }
    });