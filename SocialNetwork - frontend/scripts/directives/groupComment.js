'use strict';

socialNetwork.directive('groupComment', function() {
	return {
		restrict: 'C',
		templateUrl: 'partials/directives/comment.html',
		controller: 'GroupCommentController',
		replace: false
	}
});