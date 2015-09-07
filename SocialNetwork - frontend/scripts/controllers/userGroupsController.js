socialNetwork.controller('UserGroupsController',
    function UserGroupsController($scope, authentication, groupsData, notify) {

        groupsData.getGroupsForUser($scope.test)
            .then(
                function successHandler(data) {
                    $scope.groups = data;
                    $scope.groupsCount = data.length;
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );
    });