'use strict';

socialNetwork.factory('usersData', function usersData($http, requester, authentication, baseServiceUrl) {
    var service = {},
        serviceUrl = baseServiceUrl + 'users';

    service.getUserPreviewData = function (username) {
        return requester('GET', serviceUrl + '/' + username + '/preview', authentication.getHeaders());
    };

    service.getUserFullData = function (username) {
        return requester('GET', serviceUrl + '/' + username, authentication.getHeaders());
    };

    service.searchUsersByName = function (keyword) {
        return requester('GET', serviceUrl + '/search?searchTerm=' + keyword, authentication.getHeaders());
    };

    service.getFriendWallByPages = function (username, StartPostId) {
        return requester('GET', serviceUrl + '/' + username + '/wall?StartPostId=' + StartPostId + '&PageSize=5', authentication.getHeaders());
    };

    service.getFriendsDetailedFriendsList = function (username) {
        return requester('GET', serviceUrl + '/' + username + '/friends', authentication.getHeaders());
    };

    service.getFriendsFriendsPreview = function (username) {
        return requester('GET', serviceUrl + '/' + username + '/friends/preview', authentication.getHeaders());
    };

    return service;
});