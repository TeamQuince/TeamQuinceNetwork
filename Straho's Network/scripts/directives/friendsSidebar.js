'use strict';

socialNetwork.directive('friendsSidebar', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/friends-sidebar.html',
        controller: 'HomeController',
        scope: {
            sidebarUsername: "=sidebarUsername"
        },
        replace: true
    }
});