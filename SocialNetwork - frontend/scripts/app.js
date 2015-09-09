var socialNetwork = angular.module('socialNetwork', ['ngRoute', 'ui.bootstrap', 'infinite-scroll']);

socialNetwork.config(['$routeProvider',
    function($routeProvider) {

        $routeProvider.
        when('/welcome', {
            templateUrl: 'partials/welcome.html',
            controller: 'WelcomeController'
        });
        $routeProvider.
        when('/login', {
            templateUrl: 'partials/login.html',
            controller: 'LoginController'
        });
        $routeProvider.
        when('/register', {
            templateUrl: 'partials/register.html',
            controller: 'RegisterController'
        });
        $routeProvider.
        when('/users/me', {
            templateUrl: 'partials/home.html',
            controller: 'HomeController'
        });
        $routeProvider.
        when('/users/:username', {
            templateUrl: 'partials/user-wall.html',
            controller: 'UserWallController'
        });
        $routeProvider.
        when('/users/:username/friends', {
            templateUrl: 'partials/friends.html',
            controller: 'FriendsController'
        });
        $routeProvider.
        when('/profile', {
            templateUrl: 'partials/edit-profile.html',
            controller: 'EditProfileController'
        });
        $routeProvider.
        when('/profile/password', {
            templateUrl: 'partials/change-password.html',
            controller: 'ChangePasswordController'
        });
        $routeProvider.
        when('/logout', {
            controller: 'LogoutController'
        });
        $routeProvider.
        when('/groups/create', {
            templateUrl: 'partials/create-group.html',
            controller: 'CreateGroupController'
        });
        $routeProvider.
        when('/groups/:id', {
            templateUrl: 'partials/group-wall.html',
            controller: 'GroupWallController'
        });
        $routeProvider.
        when('/groups/:id/edit', {
            templateUrl: 'partials/edit-group.html',
            controller: 'EditGroupController'
        });
        $routeProvider.
        otherwise({
            redirectTo: '/welcome'
        });
    }]);

socialNetwork.constant('baseServiceUrl', 'http://localhost:51477/api/');
// socialNetwork.constant('baseServiceUrl', 'http://quince-network.azurewebsites.net/api/');