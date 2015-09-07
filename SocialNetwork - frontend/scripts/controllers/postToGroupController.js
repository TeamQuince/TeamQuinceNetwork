socialNetwork.controller('PostToGroupController',
    function PostToGroupController($scope, groupPostsData, profileData, notify) {

        $scope.addNewPost = function() {

            groupPostsData.addPost($scope.postContent, $scope.test2)
                .then(
                    function successHandler(data) {
                        notify.info("Post successful.");
                        $scope.postContent = '';
                        $scope.$emit('addedPost', data.post);
                    },
                    function errorHandler(error) {
                        notify.error(error.message);
                    }
                );
        };
    });