(function ($) {
    InvokeQtip();
    //accordion ========================================================================================================
    $('.accordion-group').on('show hide', function (n) {
        $(n.target).siblings('.accordion-heading').find('.accordion-toggle i').toggleClass('icon-chevron-up icon-chevron-down');
    });
    //==================================================================================================================


    //NOTIFICATION =======================================================================   
    $(document).click(function () {
        $("#notificationContainer").hide();
    });
    //Popup Click
    $(document).on("click", "#notificationLink", function () {
        $("#notificationContainer").animate({ "width": 'toggle' }, 'slow');
        // $("#notification_count").fadeOut("slow");
        return false;
    });
    $(document).on("click", "#notificationContainer", function () {
        return false;
    });
    $(document).on("click", "#notificationContainer a", function () {
        window.location.href = $(this).attr("href");
    });
    //==================================================================================

    //change width of datatable 
    ResizeDataTable();
    // SetRightWidth();
    $(document).on("click", '#changeSidebarPos', function (event) {
        $('body').toggleClass('hide-sidebar');
        SetRightWidth();
        ResizeDataTable();
    });
   

    function ResizeDataTable() {
        $('.dataTables_scrollHead').css('width', '100%');
        $(".dataTables_scrollHeadInner").css("width", "100%");
        $(".dataTables_scrollHeadInner .use-dataTable").css("width", "100%");
        $(".dataTables_scrollBody").css("width", "100%");
        $(".dataTables_scrollBody .use-dataTable").css("width", "100%");
        //$(".dataTables_scrollHeadInner").css("width", $(".dataTables_scrollBody .use-dataTable").width() * 100 / 99).css("margin-left", "0px");
    }

    function SetRightWidth() {
        var browserWidth = $(window).outerWidth();
        var leftWidth = $("#LeftPane").outerWidth();

        var leftPercentWidth = Math.round(leftWidth / browserWidth * 100);

        var paddingWidth = 0;
        var rightPercentWidth = 0;
        
        rightPercentWidth = 100 - (leftPercentWidth + paddingWidth);
        //alert(leftPercentWidth + " - " + rightPercentWidth);
        $("#ContentPane").width(rightPercentWidth + '%');
    }

    LoadSiteMap();
    function LoadSiteMap() {
        var MenuId = GetURLParameter(0);
        $.ajax({
            type: "GET",
            url: "/Admin/Menu/PopulateSiteMapByMenuId",
            data: { "MenuId": MenuId },
            success: function (data, statusCode, xhr) {
                $('#SiteMap').html(data);
            }, error: function (jqXHR, textStatus, errorThrown) {
                handleAjaxErrors(jqXHR, textStatus, errorThrown);
            }
        });
        
    }
    
    function GetURLParameter() {
        var sPageURL = window.location.href;
        var indexOfLastSlash = sPageURL.lastIndexOf("/");

        if (indexOfLastSlash > 0 && sPageURL.length - 1 != indexOfLastSlash)
            return sPageURL.substring(indexOfLastSlash + 1);
        else
            return 0;
    }

    //$(document).on('ajaxSuccess', function (event, xhr) {
    //    if (xhr.status === 401 || xhr.status === 403 || xhr.status === 590) {
    //        alert("Sorry, your session has expired.");
    //        window.location.href = "/login";
    //    }
    //});

    return $(document).ajaxError(function (exception, xhr, opts) {
        if (0 == xhr.status) {
            console.log('[0] Not connect.n Verify Network.');
        }
        else if (401 == xhr.status) {
            console.log("[401] Unauthorized response! Sorry, your session has expired.");
            window.location.reload();
        }
        else if (403 == xhr.status) {
            console.log("[403] Forbidden! Sorry, your session has expired.");
            window.location.reload();
        }
        else if (404 == xhr.status) {
            console.log('[404] Requested page not found.');
        } else if (500 == xhr.status) {
            console.log('Internal Server Error [500].');
        } else if (590 == xhr.status) {
            console.log("[590] Unexpected time-out.");
            return document.location = document.location;
        } else if ('parsererror' == exception) {
            console.log("JSON PARSE ERROR \n", "Requested JSON parse failed.");
        }
        else if ('timeout' == exception) {
            return document.location = '/administraion/login?return_url=' + document.location;
        } else if (exception == 'abort') {
            console.log('Ajax request aborted.');
        } else if ('abort' == exception) {
            console.log("Abort Exception \n", "Ajax request aborted");
        }
        else {
            console.log('Uncaught Error.n' + xhr.responseText);
        }
    });

    //USAGE: $("#form").serializefiles();
    $.fn.serializefiles = function () {
        var obj = $(this);
        /* ADD FILE TO PARAM AJAX */
        var formData = new FormData();
        $.each($(obj).find("input[type='file']"), function (i, tag) {
            $.each($(tag)[0].files, function (i, file) {
                formData.append(tag.name, file);
            });
        });
        var params = $(obj).serializeArray();
        $.each(params, function (i, val) {
            formData.append(val.name, val.value);
        });
        return formData;
    };

    //$.ajaxSetup({
    //    error: function (jqXHR, exception) {
    //        if (jqXHR.status === 0) {
    //            alert('Not connect.n Verify Network.');
    //        } else if (jqXHR.status == 404) {
    //            alert('Requested page not found. [404]');
    //        } else if (jqXHR.status == 500) {
    //            alert('Internal Server Error [500].');
    //        } else if (exception === 'parsererror') {
    //            alert('Requested JSON parse failed.');
    //        } else if (exception === 'timeout') {
    //            alert('Time out error.');
    //        } else if (exception === 'abort') {
    //            alert('Ajax request aborted.');
    //        } else {
    //            alert('Uncaught Error.n' + jqXHR.responseText);
    //        }
    //    }
    //});

    //Loading Process Spin
    //var waitimageUrl = '/Content/Admin/images/Wait.gif';
    //var sessionoutRedirect = '@Url.Action("Login", "User")';
    //$(document).ready(function () {
    //    var options = {
    //        AjaxWait: {
    //            AjaxWaitMessage: "<img style='height: 40px' src='" + waitimageUrl + "' />",
    //            AjaxWaitMessageCss: { width: "50px", left: "45%" }
    //        },
    //        AjaxErrorMessage: "<h6>Error! please contact the specialist!</h6>",
    //        SessionOut: {
    //            StatusCode: 590,
    //            RedirectUrl: sessionoutRedirect
    //        }
    //    };

    //    AjaxGlobalHandler.Initiate(options);
    //});

})(jQuery);