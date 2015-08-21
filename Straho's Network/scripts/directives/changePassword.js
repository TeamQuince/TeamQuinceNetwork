'use strict';

socialNetwork.directive('changePassword', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/change-password-form.html',
        replace: true
    }
});