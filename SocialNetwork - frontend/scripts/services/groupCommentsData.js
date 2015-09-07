'use strict';

socialNetwork.factory('groupCommentsData', function groupCommentsData($http, requester, authentication, baseServiceUrl) {
    var service = {},
        serviceUrl = baseServiceUrl + 'groupposts';

    service.getPostComments = function getPostComments(postId) {

        return requester('GET', serviceUrl + '/' + postId + '/comments', authentication.getHeaders());
    };

    service.addCommentToPost = function(postId, content) {
        var data = {
            commentContent: content
        };
        return requester('POST', serviceUrl + '/' + postId + '/comments', authentication.getHeaders(), data);
    };

    service.editPostComment = function(postId, commentId, content) {
        var data = {
            commentContent: content
        };
        return requester('PUT', serviceUrl + '/' + postId + '/comments' + '/' + commentId, authentication.getHeaders(), data);
    };

    service.deletePostComment = function(postId, commentId) {
        return requester('DELETE', serviceUrl + '/' + postId + '/comments' + '/' + commentId, authentication.getHeaders());
    };

    service.getCommentDetailedLikes = function(postId, commentId) {
        return requester('GET', serviceUrl + '/' + postId + '/comments' + '/' + commentId + '/likes', authentication.getHeaders());
    };

    service.getCommentPreviewLikes = function(postId, commentId) {
        return requester('GET', serviceUrl + '/' + postId + '/comments' + '/' + commentId + '/likes/preview', authentication.getHeaders());
    };

    service.likeComment = function(postId, commentId) {
        return requester('POST', serviceUrl + '/' + postId + '/comments' + '/' + commentId + '/likes', authentication.getHeaders());
    };

    service.unlikeComment = function(postId, commentId) {
        return requester('DELETE', serviceUrl + '/' + postId + '/comments' + '/' + commentId + '/likes', authentication.getHeaders());
    };

    return service;
});