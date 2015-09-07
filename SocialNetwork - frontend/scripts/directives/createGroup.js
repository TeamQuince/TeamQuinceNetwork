'use strict';

socialNetwork.directive('createGroup', function() {
	return {
		restrict: 'AE',
		templateUrl: 'partials/directives/create-group-form.html',
		controller: 'CreateGroupController',
		replace: true
	}
});