'use strict';

socialNetwork.factory('groupsData', function groupsData($http, requester, authentication, baseServiceUrl) {
    var service = {},
        serviceUrl = baseServiceUrl + 'groups';

    service.getGroupMembersPreviewData = function(groupId) {
        return requester('GET', serviceUrl + '/' + groupId + '/members', authentication.getHeaders());
    };

    service.getGroupById = function(groupId) {
        return requester('GET', serviceUrl + '/' + groupId, authentication.getHeaders());
    };

    service.getGroupsForUser = function(username) {
        return requester('GET', serviceUrl + '/' + username + '/list', authentication.getHeaders());
    };

    service.searchGroupsByName = function(keyword) {
        return requester('GET', serviceUrl + '/search?searchTerm=' + keyword, authentication.getHeaders());
    };

    service.getGroupWall = function(groupId) {
        return requester('GET', serviceUrl + '/' + groupId + '/wall', authentication.getHeaders());
    };

    service.createGroup = function(data) {
        return requester('POST', serviceUrl, authentication.getHeaders(), data);
    };

    service.editGroup = function(groupId, data) {
        return requester('PUT', serviceUrl + '/' + groupId, authentication.getHeaders(), data);
    };

    service.deleteGroup = function(groupId) {
        return requester('DELETE', serviceUrl + '/' + groupId, authentication.getHeaders());
    };

    service.joinGroup = function(groupId) {
        return requester('POST', serviceUrl + '/' + groupId + '/join', authentication.getHeaders());
    };

    service.leaveGroup = function(groupId) {
        return requester('POST', serviceUrl + '/' + groupId + '/leave', authentication.getHeaders());
    };

    return service;
});