'use strict';

socialNetwork.directive('allFriends', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/all-friends.html',
        controller: 'FriendsController',
        replace: true
    }
});