socialNetwork.controller('HomeController',
    function HomeController($scope, $location, authentication, postsData, profileData, notify) {

        var usedStartPostIds = [];

        $scope.startPostId = "";
        $scope.nextPageBlocked = true;

        if (!authentication.isLogged()) {
            $location.path('/welcome');
            return;
        }

        $scope.name = authentication.getName();
        $scope.username = authentication.getUserName();
        $scope.isUserPreviewVisible = false;

        profileData.getNewsFeedPages($scope.startPostId)
            .then(
            function successHandler(data) {
                $scope.posts = data;
                $scope.startPostId = data[data.length - 1].id;
                if (data.length === 0) {
                    $scope.isNewsFeedEmpty = true;
                }

                $scope.nextPageBlocked = false;
            },
            function errorHandler(error) {
                notify.error('Error loading news feed.');
            }
        );

        $scope.nextPage = function () {

            $scope.nextPageBlocked = true;

            if (usedStartPostIds.indexOf($scope.startPostId) < 0) {

                usedStartPostIds.push($scope.startPostId);

                profileData.getNewsFeedPages($scope.startPostId)
                    .then(
                    function successHandler(data) {
                        $scope.startPostId = data[data.length - 1].id;

                        for (var i = 0; i < data.length; i++) {
                            $scope.posts.push(data[i]);
                        }
                    },
                    function errorHandler(error) {
                        notify.error('Error loading news feed.');
                    }
                );
            }

            $scope.nextPageBlocked = false;
        };

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

        $scope.addNewPost = function () {
            postsData.addPost($scope.postContent, authentication.getUserName())
                .then(
                function successHandler(data) {
                    notify.info("Post successful.");
                    $scope.postContent = '';
                    //$scope.posts.push(data);
                },
                function errorHandler(error) {
                    console.log(error);
                }
            );
        };

        $scope.$on('deletePost', function(event, data) {
            var index = $scope.posts.indexOf(data);

            if (index > -1) {
                $scope.posts.splice(index, 1);
            }
        });
    });