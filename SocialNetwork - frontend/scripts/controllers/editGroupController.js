socialNetwork.controller('EditGroupController',
    function EditGroupController($scope, $location, $routeParams, authentication, groupsData, notify) {

        if (!authentication.isLogged()) {
            $location.path('/welcome');
            return;
        }

        groupsData.getGroupById($routeParams.id)
            .then(
                function successHandler(data) {
                    $scope.data = data;

                    if (!data.wallPicture) {
                        document.getElementById('coverImagePreview').src = "img/nocover.png";
                    }
                },
                function errorHandler(error) {
                    notify.error(error.message);
                }
            );

        $scope.getCoverImage = function() {
            $('#coverImageSelector').click()
                .on('change', function() {
                    var file = this.files[0];
                    if (file.type.match(/image\/.*/)) {
                        var reader = new FileReader();
                        reader.onload = function() {
                            $('#coverImagePreview')
                                .text(file.name)
                                .attr('src', reader.result);
                            $scope.data.wallPicture = reader.result;
                        };
                        reader.readAsDataURL(file);
                    } else {
                        notify.error("Invalid file format.");
                    }
                });
        };

        $scope.cancelSave = function() {
            $location.path('/groups/' + $routeParams.id);
        };

        $scope.editGroup = function(data, editGroupForm) {
            if (editGroupForm.$valid) {
                groupsData.editGroup($routeParams.id, data)
                    .then(
                        function successHandler(data) {
                            notify.info("Edited group successfully.");
                            $location.path('/groups/' + data.id);
                        },
                        function errorHandler(error) {
                            notify.error(error.message);
                        }
                    )
            }
        };
    });