'use strict';

socialNetwork.directive('userHeader', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/user-header.html',
        controller: 'UserHeaderController',
        replace: true
    }
});