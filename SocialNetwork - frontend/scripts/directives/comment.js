'use strict';

socialNetwork.directive('comment', function () {
    return {
        restrict: 'C',
        templateUrl: 'partials/directives/comment.html',
        controller: 'CommentController',
        replace: false
    }
});