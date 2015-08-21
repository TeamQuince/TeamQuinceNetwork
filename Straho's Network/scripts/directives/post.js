'use strict';

socialNetwork.directive('post', function () {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/post.html',
        controller: 'PostController',
        replace: true
    }
});