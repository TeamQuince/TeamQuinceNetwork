socialNetwork.controller('LoginController',
    function LoginController($scope, $location, authentication, notify) {
        $scope.login = function(loginData, loginForm) {
            if (loginForm.$valid) {
                loginData.grant_type = 'password';
                authentication.login(loginData)
                    .then(
                        function successHandler(data) {
                            authentication.setCredentials(data);
                            notify.info("Login successful.");
                            $location.path('/users/me');
                        },
                        function errorHandler(error) {
                            notify.error(error.message);
                        }
                    )
            }
        };

        $scope.cancelLogin = function() {
            $location.path('/welcome');
        }
    });