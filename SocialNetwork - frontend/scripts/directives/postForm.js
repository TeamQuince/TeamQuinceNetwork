'use strict';

socialNetwork.directive('postForm', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/post-form.html',
        controller: 'HomeController',
        replace: true
    }
});