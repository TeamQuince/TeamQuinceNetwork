socialNetwork.controller('LoginController',
    function LoginController($scope, $location, authentication, notify) {
        $scope.login = function(loginData, loginForm) {
            if (loginForm.$valid) {
                authentication.login(loginData)
                    .then(
                    function successHandler(data) {
                        authentication.setCredentials(data);
                        notify.info("Login successful.");
                        $location.path('/users/me');
                    },
                    function errorHandler(error) {
                        notify.error("Login failed.");
                    }
                )
            }
        };

        $scope.cancelLogin = function () {
            $location.path('/welcome');
        }
    });