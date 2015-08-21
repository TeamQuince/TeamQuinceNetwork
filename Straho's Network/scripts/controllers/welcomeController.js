'use strict';

socialNetwork.controller('WelcomeController',
    function WelcomeController($scope, $location) {
        $scope.showLogin = function showLogin() {
            $location.path('/login');
        };

        $scope.showRegister = function showRegister() {
            $location.path('/register');
        }
    });