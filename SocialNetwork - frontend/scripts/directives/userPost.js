'use strict';

socialNetwork.directive('userPost', function () {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/post.html',
        controller: 'UserPostController',
        replace: true
    }
});