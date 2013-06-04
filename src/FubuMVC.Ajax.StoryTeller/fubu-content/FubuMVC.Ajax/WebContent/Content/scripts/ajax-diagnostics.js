(function ($, continuations) {
    var pendingRequests = [];
    var initialized = false;
    
    var updateRequests = function () {
        var id = 'PendingRequests';
        if(!initialized) {
            $('<input type="hidden" value="0" />').attr("id", id).appendTo('body');
            initialized = true;
        }
        
        $('#' + id).val(pendingRequests.length);
    };

    continuations.bind('AjaxStarted', function (request) {
        pendingRequests.push(request);
        updateRequests();
    });

    continuations.bind('AjaxCompleted', function (response) {
        var id = response.correlationId;
        var tmpRequests = [];
        for (var i = 0; i < pendingRequests.length; i++) {
            var r = pendingRequests[i];
            if (r.correlationId != id) {
                tmpRequests.push(r);
            }
        }

        pendingRequests.length = 0;
        pendingRequests = tmpRequests;
        updateRequests();
    });
} (jQuery, jQuery.continuations));