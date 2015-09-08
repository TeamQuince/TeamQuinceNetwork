'use strict';

socialNetwork.directive('editGroup', function() {
	return {
		restrict: 'AE',
		templateUrl: 'partials/directives/edit-group-form.html',
		controller: 'EditGroupController',
		replace: true
	}
});