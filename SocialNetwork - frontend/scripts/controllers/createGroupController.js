socialNetwork.controller('CreateGroupController',
    function CreateGroupController($scope, $location, authentication, groupsData, notify) {

        if (!authentication.isLogged()) {
            $location.path('/welcome');
            return;
        }

        // document.getElementById('coverImagePreview').src = "img/nocover.png";

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
            $location.path('/users/me');
        };

        $scope.createGroup = function(data, createGroupForm) {
            if (createGroupForm.$valid) {
                groupsData.createGroup(data)
                    .then(
                        function successHandler(data) {
                            notify.info("Group created.");
                            $location.path('/groups/' + data.id);
                        },
                        function errorHandler(error) {
                            notify.error(error.message);
                        }
                    )
            }
        };
    });