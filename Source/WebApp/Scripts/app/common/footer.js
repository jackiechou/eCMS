(function ($) {
    FormatLine();
    //Loading Progress ==============================================================================
    $body = $("body");
    $(document).on({
        ajaxStop: function () {
            $body.removeClass("loading");
        }
    });
    //===============================================================================================

    $(document).on('click', 'div.accordion-heading a', function () {
        var next_element = $(this).parent('.accordion-heading').parent('.accordion-group').siblings();
        next_element.find('.accordion-body').removeClass("in").css("height", "0");
        next_element.find('.accordion-heading').find(".icon-chevron-up").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
    });

    $('div.accordion-body').on('shown', function () {

        $(this).parent("div").find(".glyphicon-chevron-down")
               .removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
    });

    $('div.accordion-body').on('hidden', function () {
        $(this).parent("div").find(".glyphicon-chevron-up")
               .removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
    });
})(jQuery);