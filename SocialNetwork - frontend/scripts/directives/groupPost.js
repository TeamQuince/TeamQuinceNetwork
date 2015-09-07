'use strict';

socialNetwork.directive('groupPost', function() {
	return {
		restrict: 'AE',
		templateUrl: 'partials/directives/group-post.html',
		controller: 'GroupPostController',
		replace: true
	}
});