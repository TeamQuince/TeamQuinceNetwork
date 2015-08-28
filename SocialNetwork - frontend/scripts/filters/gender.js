'use strict';

socialNetwork.filter('gender', function() {
    return function(input) {
        input = parseInt(input);
        switch (input) {
            case 1: return "img/male.png"; break;
            case 2: return "img/female.png"; break;
            case 3: return "img/transgender.png"; break;
            default: return "img/transgender.png"; break;
        }
    }
});