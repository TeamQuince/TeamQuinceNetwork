'use strict';

socialNetwork.directive('postToUser', function() {

    return {
        restrict: 'A',
        templateUrl: 'partials/directives/post-to-user.html',
        controller: 'PostToUserController',
        replace: false,
        scope: {
            test2: "@test2",
            friend: "@friend"
        }
    }
});