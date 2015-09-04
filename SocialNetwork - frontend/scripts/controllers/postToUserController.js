socialNetwork.controller('PostToUserController',
    function PostToUserController($scope, postsData, profileData, notify) {

        $scope.addNewPost = function() {

            // if (!verifyPostOperation()) {
            //     notify.error("Posting allowed only on friends' walls.");
            //     return;
            // }

            postsData.addPost($scope.postContent, $scope.test2)
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

        function verifyPostOperation() {
            if ($scope.friend === "true") {
                return true;
            } else {
                return false;
            }
        }

    });