'use strict';

socialNetwork.directive('groupMembersSidebar', function() {

	return {
		restrict: 'A',
		templateUrl: 'partials/directives/group-members-sidebar.html',
		controller: 'GroupMembersController',
		replace: false,
		scope: {
			test: "@test"
		}
	}
});