'use strict';

socialNetwork.directive('postToGroup', function() {

    return {
        restrict: 'A',
        templateUrl: 'partials/directives/post-to-group.html',
        controller: 'PostToGroupController',
        replace: false,
        scope: {
            test2: "@test2",
            friend: "@friend"
        }
    }
});