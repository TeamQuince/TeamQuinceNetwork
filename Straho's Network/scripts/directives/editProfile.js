'use strict';

socialNetwork.directive('editProfile', function() {
    return {
        restrict: 'AE',
        templateUrl: 'partials/directives/edit-profile-form.html',
        controller: 'EditProfileController',
        replace: true
    }
});