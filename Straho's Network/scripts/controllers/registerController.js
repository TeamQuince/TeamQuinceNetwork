socialNetwork.controller('RegisterController',
    function RegisterController($scope, $location, authentication, notify) {
        $scope.register = function(registerData, registerForm) {
            if (registerForm.$valid) {
                authentication.register(registerData)
                    .then(
                    function successHandler(data) {
                        authentication.setCredentials(data);
                        notify.info("Registration successful.");
                        $location.path('/users/me');
                    },
                    function errorHandler(error) {
                        notify.error("Registration failed.");
                    }
                )
            }
        };

        $scope.passwordPattern = (function() {
            var regexp = /.{6,}/;
            return {
                test: function(value) {
                    return regexp.test(value);
                }
            };
        })();

        $scope.cancelRegister = function () {
            $location.path('/welcome');
        }
    });