socialNetwork.controller('GroupMembersController',
    function GroupMembersController($scope, authentication, groupsData, notify) {

        groupsData.getGroupMembersPreviewData($scope.test)
            .then(
                function successHandler(data) {
                    $scope.owner = data.owner;
                    $scope.members = data.members;
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );
    });