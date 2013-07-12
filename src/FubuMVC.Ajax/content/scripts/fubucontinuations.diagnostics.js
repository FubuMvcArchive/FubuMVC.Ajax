(function ($, continuations) {
  var pendingRequests = [];
  var elementId = 'PendingRequests';

  var updateRequests = function () {
    $('#' + elementId).val(pendingRequests.length);
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

  $(function () {
    var element = $('#' + elementId);
    if (element.size() == 0) {
      $('<input type="hidden" value="0" />').attr("id", elementId).appendTo('body');
    }
  });

}(jQuery, $fubu.continuations));