'use strict';

socialNetwork.factory('profileData', function profileData($http, requester, authentication , baseServiceUrl) {
    var service = {},
        serviceUrl = baseServiceUrl + 'me';

    service.getDataAboutMe = function () {
        return requester('GET', serviceUrl, authentication.getHeaders());
    };

    service.getOwnFriends = function () {
        return requester('GET', serviceUrl + '/friends', authentication.getHeaders());
    };

    service.getFriendRequests = function () {
        return requester('GET', serviceUrl + '/requests', authentication.getHeaders());
    };

    service.sendFriendRequest = function (username) {
        return requester('POST', serviceUrl + '/requests/' + username, authentication.getHeaders());
    };

    service.approveFriendRequest = function (requestId) {
        return requester('PUT', serviceUrl + '/requests/' + requestId + '?status=approved', authentication.getHeaders());
    };

    service.rejectFriendRequest = function (requestId) {
        return requester('PUT', serviceUrl + '/requests/' + requestId + '?status=rejected', authentication.getHeaders());
    };

    service.getNewsFeedPages = function (StartPostId) {
        return requester('GET', serviceUrl + '/feed?StartPostId=' + StartPostId + '&PageSize=5', authentication.getHeaders());
    };

    service.changePassword = function (passwordData) {
        return requester('PUT', serviceUrl + '/changepassword', authentication.getHeaders(), passwordData);
    };

    return service;
});