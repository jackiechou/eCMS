/*auto complete combobox*/
function inputautocomplete(obj) {
    //  obj.select();
    obj.next().click();
}
function resetautocomplete(obj) {
    var objselect = obj.parent().prev();
    if (obj.val() != objselect.find("option:selected").html()) {
        obj.val(objselect.find("option:selected").text());
    }
}
$.ui.autocomplete.prototype._renderItem = function (ul, item) {
    return $("<li class='" + item.class + ' ' + (this.element.val() == item.value ? ' ui-state-highlight ' : '') + "' ></li>")
      .data("item.autocomplete", item)
      .append('<a class="' + item.class + '">' + item.label + '</a>')
      .appendTo(ul);
};
(function ($) {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
            .addClass("custom-combobox")
            .insertAfter(this.element);
            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },
        _createAutocomplete: function () {
            var selected = this.element.children(":selected");
            $(this.element).attr('iscombobox', '1');
            var value = selected.val() ? selected.text() : this.element.find('option[value="' + this.element.val() + '"]').html();
            var ischeck = $(this.element).prop('disabled');
            //value = selected.val() ? selected.text() : "";
            this.input = $("<input>")
            .appendTo(this.wrapper)
            .val(value)
            .attr({ 'idvalue': selected.val() })
            .attr({ 'onClick': 'inputautocomplete($(this));', 'onmouseout': 'resetautocomplete($(this))', 'onblur': 'resetautocomplete($(this))' })
            .attr("title", "")
            .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
            .autocomplete({
                delay: 0,
                minLength: 0,
                source: $.proxy(this, "_source"),

                focus: function (event, ui) {
                    // Remove the hover class from all results

                },
                select: function (event, ui) {

                }
            });
            if (ischeck) {
                $(this.input).prop('disabled', true);
            }
            selected.attr('iscombobox', '1');

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    $(this.element).change();
                    $(ui).addClass('ui-state-focus');
                    //console.log ($(this).find('option[value="' + selected.val() + '"]'));
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                    this._trigger("change", event, {
                        item: ui.item.option
                    });
                },
                //autocompletechange: "_removeIfInvalid"
            });
        },
        _createShowAllButton: function () {
            var input = this.input,
			 ischeck = $(this.input).prop('disabled');
            wasOpen = false;
            var acontrol = $("<a>")
             .attr("tabIndex", -1)
             //.attr("title", "Show All Items")
             .tooltip()
             .appendTo(this.wrapper)
             .button({
                 icons: {
                     primary: "ui-icon-triangle-1-s"
                 },
                 text: false
             })
             .removeClass("ui-corner-all")
             .addClass("custom-combobox-toggle ui-corner-right")
             .mousedown(function () {
                 wasOpen = input.autocomplete("widget").is(":visible");
             })
             .click(function () {
                 if ($(this).hasClass('ui-state-disabled')) {
                     return false;
                 }
                 input.focus();
                 // Close if already visible
                 if (wasOpen) {
                     return;
                 }
                 // Pass empty string as value to search for, displaying all results
                 input.autocomplete("search", "");

             });
            if (ischeck) {
                acontrol.addClass('ui-state-disabled');
            }

        },
        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");

            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (!request.term || matcher.test(text))
                    return {
                        label: text,
                        value: text,
                        option: this,
                        'class': $(this).attr('class')
                    };
            }));
        },
        _removeIfInvalid: function (event, ui) {
            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }
            // Search for a match (case-insensitive)
            var value = this.input.val(),
            valueLowerCase = value.toLowerCase(),
            valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });
            // Found a match, nothing to do
            if (valid) {
                return;
            }
            // Remove invalid value
            this.input
            .val("")
            //.attr("title", value + " didn't match any item")
            .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.autocomplete("instance").term = "";
        },
        _destroy: function () {
            try {
                this.wrapper.remove();
                this.element.show();
                $(this.element).removeAttr('iscombobox')
            } catch (e) {
                console.log(e);
            }
            this._trigger("destroy", this._destroy);
        }
    });
})(jQuery);

function AutoCreateCombobox() {
    setInterval(function () {
        try {
            $(document).on();
            $('select:visible').not('[multiple]').each(function () {

                if ($(this).hasClass('ui-pg-selbox')) {
                    return;
                }
                var attr = $(this).attr('multiple');

                $(this).combobox();

            });
            $('select').each(function () {
                var obj = $(this).next('.custom-combobox').find('input');
                var val = obj.val();
                var idval = obj.attr('idval');
                if (idval != $(this).val()) {// val == "" || $(this).find('option[value="' + idval + '"]').length == 0 || 
                    var objop = $(this).find('option[value="' + $(this).val() + '"]');
                    if (objop.length < 1) {
                        return;
                    }
                    var valin = objop.html();
                    if (valin != undefined && valin.indexOf(';&') > -1) {
                        valin = objop.text();
                    }
                    obj.val(valin);
                    obj.attr('idval', $(this).val());
                }
            });
            $('.custom-combobox input').each(function () {

            });
        } catch (e) {
            console.log(e);
        }

    }, 100);
}
/*end autocomplete combobox*/