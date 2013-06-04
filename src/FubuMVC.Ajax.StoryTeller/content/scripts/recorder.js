var Recorder = (function() {
  var messages = [];

  return {
    addMessage: function(msg) {
      messages.push(msg);
    },
    allMessages: function() {
      return messages;
    }
  };

}());
$(function () {
  $('form').on('fubu:success', function (event, continuation) {
    Recorder.addMessage(continuation.message);
  });
});