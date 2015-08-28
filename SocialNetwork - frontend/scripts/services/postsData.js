'use strict';

socialNetwork.factory('postsData', function postsData($http, requester, authentication , baseServiceUrl) {
    var serviceUrl = baseServiceUrl + 'Posts';

    function getPostById(id) {

        return requester('GET', serviceUrl + '/' + id, authentication.getHeaders());
    }

    function getPostDetailedLikes(id) {

        return requester('GET', serviceUrl + '/' + id + '/likes', authentication.getHeaders());
    }

    function getPostPreviewLikes(id) {

        return requester('GET', serviceUrl + '/' + id + '/likes/preview', authentication.getHeaders());
    }

    function addPost(content, username) {

        var data = {
            postContent: content,
            username: username
        };

        return requester('POST', baseServiceUrl + 'posts', authentication.getHeaders(), data);
    }

    function editPostById(content, id) {

        var data = {
            postContent: content
        };

        return requester('PUT', serviceUrl + '/' + id, authentication.getHeaders(), data);
    }

    function deletePostById(id) {

        return requester('DELETE', serviceUrl + '/' + id, authentication.getHeaders());
    }

    function likePostById(id) {

        return requester('POST', serviceUrl + '/' + id + '/likes', authentication.getHeaders());
    }

    function unlikePostById(id) {

        return requester('DELETE', serviceUrl + '/' + id + '/likes', authentication.getHeaders());
    }

    return {
        getPostById: getPostById,
        getPostDetailedLikes: getPostDetailedLikes,
        getPostPreviewLikes: getPostPreviewLikes,
        addPost: addPost,
        editPostById: editPostById,
        deletePostById: deletePostById,
        likePostById: likePostById,
        unlikePostById: unlikePostById
    }
});