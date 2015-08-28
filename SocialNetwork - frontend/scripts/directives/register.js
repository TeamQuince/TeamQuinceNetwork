'use strict';

socialNetwork.directive('register', function($compile) {
    return {
        restrict: 'E',
        templateUrl: 'partials/directives/register-form.html',
        replace: true
    };
});