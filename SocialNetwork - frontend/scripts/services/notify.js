'use strict';

socialNetwork.factory('notify', function () {

    var service = {};

    function display(type, text, time) {
        var n = noty({
            text: text,
            type: type,
            dismissQueue: true,
            layout: 'bottomCenter',
            theme: 'defaultTheme',
            maxVisible: 10,
            timeout: time
        });
    }

    service.info = function (message) {
        display('success', message, 2100);
    };

    service.error = function (message) {
        display('error', message, 2100);
    };

    return service;
});