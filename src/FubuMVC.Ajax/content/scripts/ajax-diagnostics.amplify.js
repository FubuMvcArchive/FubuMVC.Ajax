amplifyWatcher = (function () {
    var module = {};

    var finders = {};

    module.updateTopic = function (topic) {
        registerTopic(topic);
        var hidden = finders[topic]();
        var rev = parseInt(hidden.val());
        hidden.val(rev + 1);
    }

    var registerTopic = function (topic) {
        if (finders[topic] == undefined) {
            finders[topic] = function () {
                var id = topic + '-rev';
                $('<input type="hidden" value="0" />').attr("id", id).appendTo('body');

                finders[topic] = function () {
                    return $('#' + id);
                }

                return finders[topic]();
            }
        }
    }

    module.watch = function (topic, callback) {
        registerTopic(topic);



        amplify.subscribe(topic, function (payload) {
            callback(payload);

            module.updateTopic(topic);
        });
    }


    return module;
} ());

$(function () {
    var messages = [];
    var publish = amplify.publish;
    amplify.publish = function () {
        var msg = {
            topic: arguments[0]
        };

        if (arguments.length != 1) {
            msg.data = arguments[1];
        }

        messages.push(msg);
        publish.apply(this, arguments);
    };

    var amplifyView = null;
    var amplifyScreen = {
        activate: function () {
            amplifyView = $.diagnostics.createView('amplify messages');
            amplifyView.configure(function () {
                var inner = this.find('.messages');
                if (inner.size() != 0) {
                    inner.remove();
                }

                var list = $('<ul class="messages"></ul>');
                for (var i = 0; i < messages.length; i++) {
                    var msg = messages[i];
                    var item = $('<li style="margin-bottom:10px;list-style-type:none"></li>');

                    var data = msg.data;
                    for (var key in data) {
                        var val = data[key];
                        if (val instanceof jQuery) {
                            data[key] = '<em>jquery object...</em>';
                        }
                    }

                    item.append('<span class="topic" style="font-weight:bold; margin-right:10px;display:block;">' + msg.topic + '</span>');
                    item.append('<span class="data">' + JSON.stringify(msg.data) + '</span>');

                    item.appendTo(list);
                }

                this.append(list);
            });
            amplifyView.show();
        },
        deactivate: function () {
            amplifyView.hide();
        }
    };

    $.diagnostics.registerScreen('keydown.ctrl_m', 'amplify messages', amplifyScreen);

} ());