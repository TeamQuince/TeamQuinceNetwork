'use strict';

socialNetwork.directive('login', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/login-form.html',
        replace: true
    }
});