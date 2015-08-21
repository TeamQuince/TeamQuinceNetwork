'use strict';

socialNetwork.directive('userFriendsSidebar', function() {

    return {
        restrict: 'A',
        templateUrl: 'partials/directives/user-friends-sidebar.html',
        controller: 'UserFriendsController',
        replace: false,
        scope: {
            test: "@test"
        }
    }
});