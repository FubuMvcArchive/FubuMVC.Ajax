(function ($) {
  $.fn.activateForm = function () {
    return this.each(function () {
      var form = $(this);
      var mode = form.data('formMode');

      if (mode != 'ajax') {
        return;
      }

      // Use the namespaced binding mechanism so that others can remove it and use their own: form.off('submit.fubu')
      form.on('submit.fubu', function () {
        form.correlatedSubmit({
          continuationSuccess: function (continuation) {
            if (continuation.success) {
              form.trigger('fubu:success', [continuation]);
            }
          }
        });
        return false;
      });

    });
  };
}(jQuery));

$(function () {
  $('form.activated-form').activateForm();
});