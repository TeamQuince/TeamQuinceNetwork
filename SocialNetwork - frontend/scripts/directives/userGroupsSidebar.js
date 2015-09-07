'use strict';

socialNetwork.directive('userGroupsSidebar', function() {

	return {
		restrict: 'A',
		templateUrl: 'partials/directives/user-groups-sidebar.html',
		controller: 'UserGroupsController',
		replace: false,
		scope: {
			test: "@test"
		}
	}
});