'use strict';

socialNetwork.factory('groupPostsData', function groupPostsData($http, requester, authentication, baseServiceUrl) {
    var serviceUrl = baseServiceUrl + 'groupposts';

    function getPostById(id) {

        return requester('GET', serviceUrl + '/' + id, authentication.getHeaders());
    }

    function getPostDetailedLikes(id) {

        return requester('GET', serviceUrl + '/' + id + '/likes', authentication.getHeaders());
    }

    function getPostPreviewLikes(id) {

        return requester('GET', serviceUrl + '/' + id + '/likes/preview', authentication.getHeaders());
    }

    function addPost(postData) {

        return requester('POST', serviceUrl, authentication.getHeaders(), postData);
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