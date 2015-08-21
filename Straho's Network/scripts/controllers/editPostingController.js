'use strict';
socialNetwork.controller('EditPostingController', function EditPostingController ($scope, $modalInstance, posting) {
    if (posting.postContent) {
        $scope.content = posting.postContent;
    } else {
        $scope.content = posting.commentContent;
    }

    $scope.ok = function () {
        if ($scope.content.length == 0) {
            return;
        }
        $modalInstance.close($scope.content);
    };
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});