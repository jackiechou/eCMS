function handleAjaxErrors(jqXHR, textStatus, errorThrown) {
    if (0 == jqXHR.status) {
        showMessageWithTitle("Network Error [0]\n", "Not connect.n Verify Network.", "error", 50000);
    } else if (401 == jqXHR.status) {
        showMessageWithTitle("401 Unauthorized response! Sorry, your session has expired.", "error", 10000);
        window.location.reload();
    }
    else if (403 == jqXHR.status) {
        showMessageWithTitle("403 Forbidden! Sorry, your session has expired.", "error", 10000);
        window.location.reload();
    }
    else if (404 == jqXHR.status) {
        console.log('Requested page not found. [404]');
    } else if (500 == jqXHR.status) {
        showMessageWithTitle("Internal Server Error [500] \n", "Internal Server Error", "error", 50000);
    } else if (503 == jqXHR.status) {
        showMessageWithTitle("Service Unavailable [503] \n", "Service Unavailable", "error", 50000);
    } else if (590 == jqXHR.status) {
        showMessageWithTitle("Unexpected time-out", "Unexpected time-out", "error", 50000);
        return document.location = document.location;
    } else if ('timeout' == errorThrown) {
        showMessageWithTitle("TIME OUT \n", "Time out error.", "error", 1000);
        //location.href = '/Admin/login?return_url=' + document.location;
        window.location.reload();
    } else if ('error' == errorThrown) {
        showMessageWithTitle("Error Exception \n", "Error Ajax request", "error", 50000);
    } else if ('abort' == errorThrown) {
        showMessageWithTitle("Abort Exception \n", "Ajax request aborted", "error", 50000);
    } else if ('parsererror' == errorThrown) {
        showMessageWithTitle("JSON PARSE ERROR \n", "Requested JSON parse failed.", "error", 50000);
    } else {
        showMessageWithTitle("Uncaught Ajax Error (" + jqXHR.status + ") \n", jqXHR.responseText, "error", 50000);
    }
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function ShowDataTable(controlId) {
    if (LanguageId == 124) {
        $(controlId).dataTable({
            "aoColumnDefs": [{
                "bSortable": false,
                "aTargets": ["no-sort"]
            }]

        });
    } else {
        $(controlId).dataTable({
            "language": {
                "sProcessing": "Đang xử lý...",
                "sLengthMenu": "Xem _MENU_ mục",
                "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
                "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
                "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
                "sInfoFiltered": "(được lọc từ _MAX_ mục)",
                "sInfoPostFix": "",
                "sSearch": "Tìm:",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": "Đầu",
                    "sPrevious": "Trước",
                    "sNext": "Tiếp",
                    "sLast": "Cuối"
                }
            },
            "aoColumnDefs": [{
                "bSortable": false,
                "aTargets": ["no-sort"]
            }],
            "scrollX": true
        });
    }

    $(".dataTables_scrollHeadInner .use-dataTable").css("width", "99%").css("margin-left", "0px");
    $(".dataTables_scrollBody .use-dataTable").css("width", "99%").css("margin-left", "0px");
    $(".dataTables_scrollBody").css("width", "100%").css("margin-left", "0px");
    $(".dataTables_scrollHeadInner").css("width", $(".dataTables_scrollBody .use-dataTable").width() * 100 / 99).css("margin-left", "0px");
}


function convertFormToJSON(formId) {
    var array = $("#" + formId).serializeArray();
    var json = {};
    $.each(array, function () {
        json[this.name] = this.value || '';
    });
    return JSON.stringify(json);
}

function removeQtip() {
    $('span.error:first').qtip("hide");
    $(".qtip").remove();
}

function handleCheckBox(checkFieldId) {
    var name = $("#" + checkFieldId).attr("name");
    var checkBoxSelector = $("input:checkbox[name=" + name + "]");
    var hiddenSelector = $("input:hidden[name=" + name + "]");
    var chkStatus = checkBoxSelector.is(":checked");


    checkBoxSelector.attr("checked", chkStatus);
    checkBoxSelector.val(chkStatus);
    hiddenSelector.val(chkStatus);

    checkBoxSelector.click(function () {
        var checkBoxStatus = $(this).is(":checked");
        checkBoxSelector.attr("checked", checkBoxStatus);
        checkBoxSelector.val(checkBoxStatus);
        hiddenSelector.val(checkBoxStatus);
    });
}

function handleCheckBoxStatus(checkFieldId, chkStatus) {
    var name = $("#" + checkFieldId).attr("name");
    var checkBoxSelector = $("input:checkbox[name=" + name + "]");
    var hiddenSelector = $("input:hidden[name=" + name + "]");

    checkBoxSelector.attr("checked", chkStatus);
    checkBoxSelector.val(chkStatus);
    hiddenSelector.val(chkStatus);

    checkBoxSelector.click(function () {
        var checkBoxStatus = $(this).is(":checked");
        checkBoxSelector.attr("checked", checkBoxStatus);
        checkBoxSelector.val(checkBoxStatus);
        hiddenSelector.val(checkBoxStatus);
    });
}

function uploadFile(folderKey, fileKey, hiddenFileId) {
    if ($('input[type="file"]').val() != '') {
        var width = null;
        var height = null;
        var hiddenBox = $("input[name=" + hiddenFileId + "]:hidden");
        var fileId = hiddenBox.val();

        var formData = new FormData();
        formData.append('fileKey', fileKey);
        formData.append('FileUpload', $('input[type=file]')[0].files[0]);
        formData.append('folderKey', folderKey);
        if (width != null && width > 0)
            formData.append('width', width);
        if (height != null && height > 0)
            formData.append('height', height);
        if (fileId != null && fileId > 0)
            formData.append('fileId', fileId);

        var baseUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');

        $.ajax({
            type: "POST",
            url: baseUrl + "/Handlers/UploadFile.ashx",
            contentType: false,
            processData: false,
            data: formData,
            success: function (result) {
                if (result >= 1) {
                    hiddenBox.val(result);
                }
            },
            error: function () {
                showMessageWithTitle('error', '@Html.Raw(Eagle.Resource.LanguageResource.UploadFileError)', "error", 3000);
            }
        });
    }
}

function showUITooltip() {
    $("#myform :input").tooltip({
        // place tooltip on the right edge
        position: "center right",

        // a little tweaking of the position
        offset: [-2, 10],

        // use the built-in fadeIn/fadeOut effect
        effect: "fade",

        // custom opacity setting
        opacity: 0.7,
        //content: function () {
        //    return $(this).prop('title');
        //}
    });
}

function CommaFormatted(amount) {
    var delimiter = "."; // replace comma if desired
    amount = new String(amount);
    var a = amount.split('.', 2)
    var d = a[1];
    var i = parseInt(a[0]);
    if (isNaN(i)) { return ''; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    var n = new String(i);
    var a = [];
    while (n.length > 3) {
        var nn = n.substr(n.length - 3);
        a.unshift(nn);
        n = n.substr(0, n.length - 3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    if (d.length < 1) { amount = n; }
    else { amount = n + '.' + d; }
    amount = minus + amount;
    return amount;
}

function addDot(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    return x1 + x2;
}


function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function ShowToolTip(elementId, message) {
    if (elementId != undefined && elementId != null) {
        var qtipId = 'qtip-' + elementId;
        var element = $('input[name="' + elementId + '"]');

        if (element.hasClass('input-validation-error'))
            element.addClass('input-validation-error');
        element.attr({ 'data-hasqtip': elementId, 'aria-describedby': qtipId });


        var valSpan = $('span[data-valmsg-for="' + elementId + '"]');
        if (valSpan == undefined && valSpan == null)
            valSpan = $('<span/>', { attr: { 'data-valmsg-replace': true, 'data-valmsg-for': elementId } }).insertAfter(element);
        valSpan.addClass('field-validation-error').removeClass('field-validation-valid');

        var divContent = $('<div/>', {
            id: qtipId + '-content',
            // 'class': 'qtip qtip-default ui-tooltip-red qtip-pos-rc qtip-focus',
            'class': 'ui-tooltip-red qtip-pos-rc qtip-focus',
            attr: { 'aria-atomic': "true" },
            html: '<span id="' + qtipId + '-content-span" for="' + elementId + '"></span>'
        });

        var divWrapper = $('<div/>', {
            id: qtipId,
            'class': 'qtip-default ui-tooltip-red qtip-pos-rc qtip-focus',
            attr: {
                style: "z-index: 15003; display: block; top: 375px; left: 737.467px;",
                tracking: "false", role: "alert", "aria-live": "polite", "aria-atomic": "false",
                "aria-describedby": "qtip-1-content", "aria-hidden": "false", "data-qtip-id": "1",
            }
        });

        if (valSpan.siblings('#' + qtipId).length == 0) {
            divWrapper.append(divContent);
            divWrapper.insertAfter(valSpan);
        }


        showMessageWithTitle('warning', message, "warning", 3000);
        $('#' + qtipId + '-content-span').html(message);
        element.show();
    }
}

function CloseToolTip(elementId) {
    $('#qtip-' + elementId).remove();
}

function ShowDateTimePicker() {
    $('.datetimepicker').datetimepicker({
        format: 'dd/MM/yyyy',
        todayBtn: true,
        pickTime: false
    }).on('changeDate', function (e) {

        /* Chọn date tại datetime picker => cập nhật lại hidden input theo format của nước: 
        // EN => MM/dd/yyyy; 
        // VN => dd/MM/yyyy;*/
        var tmpId = $(this).find('input[type=text]').attr('id');
        var hiddenId = $(this).find('input[type=hidden]').attr('id');
        $(this).datetimepicker('hide');

        var result = '';
        var arr = $('#' + tmpId).val().split("/");
        result = arr[1] + '/' + arr[0] + '/' + arr[2];
        if (arr[0] != undefined && arr[1] != undefined && arr[2] != undefined) {
            $('#' + hiddenId).val(result);
            //$('#' + hiddenId).trigger('change');
        } else
            $('#' + hiddenId).val('');

        /*remove qtip error message*/
        var qtip = $(this).find('input[type=text]').attr('aria-describedby');
        $('#' + qtip).remove();
    });

    /* Gõ vào textbox datetime picker => cập nhật lại hidden input theo format của nước: 
    // EN => MM/dd/yyyy; 
    // VN => dd/MM/yyyy;*/
    $(".datetimepicker input").on("blur", function (e) {
        // if (LanguageId == 124) {
        var vietnamsesDate = $(this).val();

        var dd = vietnamsesDate.substring(0, 2);
        var MM = vietnamsesDate.substring(3, 5);
        var yyyy = vietnamsesDate.substring(6, 10);

        var dateFormat = MM + "/" + dd + "/" + yyyy;

        if (dateFormat != "//") {
            // cập nhật lại hidden input
            $(this).next('input').val(dateFormat);
        }

        return false;
        // }
        //var vietnamsesDate = $(this).val();
        //$(this).next('input').val(vietnamsesDate);

        /*remove qtip error message*/
        var qtip = $(this).attr('aria-describedby');
        $('#' + qtip).remove();
    });


    $('.timepicker').datetimepicker({
        language: 'vi-VN',
        pickDate: false,
        timeFormat: 'hh:mm',
        useSeconds: false
    });
}


function validateBirthDay(dateValue) {
    var flag = false;

    var current_date = new Date();
    var minDate = 1900;
    var maxDate = parseInt(current_date.getFullYear()) - 18;
    var dteDate;
    var dteDate2;
    var Months = new Array('JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC');

    //dateValue is dd/MM/yyyy
    if (dateValue != undefined && dateValue != '') {
        var obj1 = dateValue.split("/");
        var obj2 = new Array();


        //day
        obj2[0] = parseInt(obj1[1], 10);

        //month
        obj2[1] = parseInt(obj1[0], 10) - 1;

        //year
        obj2[2] = parseInt(obj1[2], 10);

        dteDate = new Date(obj2[2], obj2[1], obj2[0]);

        var indexOfM = -1;
        for (var i = 0; i < Months.length; i++) {
            if (Months[i] == obj1[2].toUpperCase()) {
                indexOfM = i;
            }
        }

        dteDate2 = new Date(obj2[2], indexOfM, obj2[0]);
        if (
          (
               (obj2[0] == dteDate.getDate()) &&
               (obj2[1] == dteDate.getMonth()) &&
               (obj2[2] == dteDate.getFullYear()) &&
               (dteDate.getFullYear() > minDate) &&
               (dteDate.getFullYear() < maxDate)
           ) ||
          (
               (obj1[0] == dteDate2.getDate()) &&
               (indexOfM == dteDate2.getMonth()) &&
               (obj1[2] == dteDate2.getFullYear()) &&
               (dteDate2.getFullYear() > minDate) &&
               (dteDate2.getFullYear() < maxDate)
           )
        )
            flag = true;
        else
            flag = false;
    }
    else
        flag = false;
    return flag;
}

function validateBirthDate(controlId) {
    var flag = false;

    var current_date = new Date();
    var minDate = 1900;
    var maxDate = parseInt(current_date.getFullYear()) - 18;
    var dteDate;
    var dteDate2;
    var Months = new Array('JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC');

    var dateValue = document.getElementById(controlId).value;

    if (dateValue != '') {
        var obj1 = document.getElementById(controlId).value.split("/");
        var obj2 = new Array();

        obj2[0] = parseInt(obj1[0], 10);
        obj2[1] = parseInt(obj1[1], 10) - 1;
        obj2[2] = parseInt(obj1[2], 10);

        dteDate = new Date(obj2[2], obj2[1], obj2[0]);

        var indexOfM = -1;
        for (var i = 0; i < Months.length; i++) {
            if (Months[i] == obj1[1].toUpperCase()) {
                indexOfM = i;
            }
        }

        dteDate2 = new Date(obj2[2], indexOfM, obj2[0]);
        if (
          (
               (obj2[0] == dteDate.getDate()) &&
               (obj2[1] == dteDate.getMonth()) &&
               (obj2[2] == dteDate.getFullYear()) &&
               (dteDate.getFullYear() > minDate) &&
               (dteDate.getFullYear() < maxDate)
           ) ||
          (
               (obj1[0] == dteDate2.getDate()) &&
               (indexOfM == dteDate2.getMonth()) &&
               (obj1[2] == dteDate2.getFullYear()) &&
               (dteDate2.getFullYear() > minDate) &&
               (dteDate2.getFullYear() < maxDate)
           )
        )
            flag = true;
        else
            flag = false;
    }
    else
        flag = false;
    return flag;
}




/// <reference path="spin.js" />
function replaceButtonText(buttonId, text) {
    if (document.getElementById) {
        var button = document.getElementById(buttonId);
        if (button) {
            if (button.childNodes[0]) {
                button.childNodes[0].nodeValue = text;
            }
            else if (button.value) {
                button.value = text;
            }
            else //if (button.innerHTML)
            {
                button.innerHTML = text;
            }
        }
    }
}


// Validate Date Format
function isDateFormat(value) {
    try {
        //Change the below values to determine which format of date you wish to check. It is set to dd/mm/yyyy by default.
        var DayIndex = 0;
        var MonthIndex = 1;
        var YearIndex = 2;

        value = value.replace(/-/g, "/").replace(/\./g, "/");
        var SplitValue = value.split("/");
        var ret = true;
        if (SplitValue.length > 3) {
            ret = false;
        }
        if (!(SplitValue[DayIndex].length == 1 || SplitValue[DayIndex].length == 2)) {
            ret = false;
        }
        if (ret && !(SplitValue[MonthIndex].length == 1 || SplitValue[MonthIndex].length == 2)) {
            ret = false;
        }
        if (ret && SplitValue[YearIndex].length != 4) {
            ret = false;
        }
        if (ret) {
            var Day = parseInt(SplitValue[DayIndex], 10);
            var Month = parseInt(SplitValue[MonthIndex], 10);
            var Year = parseInt(SplitValue[YearIndex], 10);

            if (ret = ((Year > 1900) && (Year < 3000))) {
                if (ret = (Month <= 12 && Month > 0)) {

                    var LeapYear = (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0));

                    if (ret = Day > 0) {
                        if (Month == 2) {
                            ret = LeapYear ? Day <= 29 : Day <= 28;
                        }
                        else {
                            if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
                                ret = Day <= 30;
                            }
                            else {
                                ret = Day <= 31;
                            }
                        }
                    }
                }
            }
        }
        return ret;
    }
    catch (e) {
        return false;
    }
}

function ConvertToInt(obj) {
    var iResult = 0;
    try {
        if (isNaN(parseInt(obj)) == false) {
            iResult = parseInt(obj);
        }
    } catch (e) {
        iResult = 0;
    }
    return iResult;
}

function ParseDateTime(value) {
    var DayIndex = 0;
    var MonthIndex = 1;
    var YearIndex = 2;

    value = value.replace(/-/g, "/").replace(/\./g, "/");
    var SplitValue = value.split("/");

    var Day = parseInt(SplitValue[DayIndex], 10);
    var Month = parseInt(SplitValue[MonthIndex], 10);
    var Year = parseInt(SplitValue[YearIndex], 10);

    return new Date(Year, Month - 1, Day);
}

function CompareDate(StartDate, EndDate) {

    try {
        var ret = false;
        if (isDateFormat(StartDate) && isDateFormat(EndDate)) {
            if (ParseDateTime(StartDate) <= ParseDateTime(EndDate)) {
                ret = true;
            }
            else {
                ret = false;
            }
        }
        else {
            ret = false;
        }
        return ret;
    } catch (e) {
        return false;
    }
}

function checkDate(field) {
    var allowBlank = true;
    var minYear = 1902;
    var maxYear = (new Date()).getFullYear();
    var errorMsg = "";
    // regular expression to match required date format 
    re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
    if (field.value != '') {
        if (regs = field.value.match(re)) {
            if (regs[1] < 1 || regs[1] > 31) { errorMsg = "Invalid value for day: " + regs[1]; }
            else if (regs[2] < 1 || regs[2] > 12) { errorMsg = "Invalid value for month: " + regs[2]; }
            else if (regs[3] < minYear || regs[3] > maxYear) { errorMsg = "Invalid value for year: " + regs[3] + " - must be between " + minYear + " and " + maxYear; }
        } else { errorMsg = "Invalid date format: " + field.value; }
    } else if (!allowBlank) {
        errorMsg = "Empty date not allowed!";
    } if (errorMsg != "") {
        alert(errorMsg);
        field.focus();
        return false;
    }
    return true;
}

function isValidDateWithFormatMMDDYYYY(dateStr) {
    // Checks for the following valid date formats:
    // MM/DD/YY   MM/DD/YYYY   MM-DD-YY   MM-DD-YYYY
    // Also separates date into month, day, and year variables

    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/;

    // To require a 4 digit year entry, use this line instead:
    // var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{4})$/;

    var matchArray = dateStr.match(datePat); // is the format ok?
    if (matchArray == null) {
        alert("Date is not in a valid format.")
        return false;
    }
    month = matchArray[1]; // parse date into variables
    day = matchArray[3];
    year = matchArray[4];
    if (month < 1 || month > 12) { // check month range
        alert("Month must be between 1 and 12.");
        return false;
    }
    if (day < 1 || day > 31) {
        alert("Day must be between 1 and 31.");
        return false;
    }
    if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
        alert("Month " + month + " doesn't have 31 days!")
        return false
    }
    if (month == 2) { // check for february 29th
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || (day == 29 && !isleap)) {
            alert("February " + year + " doesn't have " + day + " days!");
            return false;
        }
    }
    return true;  // date is valid
}
// Validation Email
function ValidateEmail(x) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (x.indexOf(" ") > -1)
        return false;
    return reg.test(x);
}

// Validation Phone: Chi duoc nhap So, Dau Gach.
function ValidatePhone(x) {
    var ValidateChar = /^[0-9\-]+$/;

    if (x.trim() == "") {
        return true;
    }

    if (x.match(ValidateChar)) {
        return true;
    }
    else {
        return false;
    }
}
function ValidateTelephone(x) {
    var ValidateChar = /^[0-9\-\(\)]+$/;

    if (x.trim() == "") {
        return true;
    }

    if (x.match(ValidateChar)) {
        return true;
    }
    else {
        return false;
    }
}

function validatePhoneLength(oSrc, args) {
    args.IsValid = (args.Value.length >= 8);
}

function FormatPhone(obj) {
    var strvalue;
    if (eval(obj))
        strvalue = eval(obj).value;
    else
        strvalue = obj;
    var num;
    num = strvalue.toString().replace(/\$|\,/g, '');

    if (isNaN(num))
        num = "";
    eval(obj).value = num;
}

// Validation Code: Chi duoc nhap So, Dau Gach va dau gach duoi.
function ValidateCode(x) {

    var ValidateSpecialChar = /^[0-9\_\-]+$/;

    if (x.match(ValidateSpecialChar)) {
        return true;
    }
    else {
        return false;
    }
}
/// <summary>
/// Check Double Type
/// </summary>
/// <param name="x">object for checking</param>
/// <returns>True: if Object is Double</returns>
function ValidateIsDouble(x) {
    x = unformatCurrency(x);
    var ValidateSpecialChar = /^[\d\.]+$/;
    if (x.match(ValidateSpecialChar)) {
        return true;
    }
    else {
        return false;
    }
}

/// <summary>
/// Check Int32 Type
/// </summary>
/// <param name="x">object for checking</param>
/// <returns>True: if Object is Int32</returns>
function ValidateIsInt(x) {
    var ValidateSpecialChar = /^[\d]+$/;
    if (x.match(ValidateSpecialChar)) {
        return true;
    }
    else {
        return false;
    }
}


// Validation cac ki tu dac biet: Chi duoc nhap cac ki tu: chu (Hoa, thuong), So, ki tu gach duoi _
function ValidateSpecialCharacter(x) {
    var ValidateSpecialChar = /^[A-Za-z0-9\_\-]+$/;

    if (x.match(ValidateSpecialChar)) {
        return true;
    }
    else {
        return false;
    }
}

// Validation cac ki tu dac biet: Chi duoc nhap cac ki tu: chu (Hoa, thuong), So, ki tu gach duoi _
function ValidateSpecialCharacterCompanyOmitNames(x) {
    var ValidateSpecialChar = /[!@#$%\^&*(){}[\]<>?\/|+]/;  // /^[\<\>\~\!\@\#\$\%\^\&\/\=\;\+\-\?\.\{\}\[\]\||\\\*]/;

    if (x.match(ValidateSpecialChar)) {
        return false;
    }
    else {
        return true;
    }
}
// End Validation cac ki tu dac biet
// Validation cac ki tu dac biet: Chi duoc nhap cac ki tu: chu (Hoa, thuong), So, ki tu gach duoi _
function ValidateKeySpecialCharacter_Project(e) {
    var text = String.fromCharCode(e.which);
    var ValidateSpecialChar = /^[A-Za-z0-9\_\-]+$/;
    if (text.match(ValidateSpecialChar) || (e.keyCode == 8 || e.keyCode == 46 || (e.keyCode == 37 && text != "%") || (e.keyCode == 39 && text != "'"))) {
        return true
    }
    else {
        return false;
    }
}
function ValidateIsNum_Key(e) {
    var text = String.fromCharCode(e.which);
    var ValidateSpecialChar = /^[0-9\.]+$/;
    if (text.match(ValidateSpecialChar) || (e.keyCode == 8 || e.keyCode == 46 || (e.keyCode == 37 && text != "%") || (e.keyCode == 39 && text != "'"))) {
        return true
    }
    else {
        return false;
    }
}
function ValidateIsNum(x) {
    var ValidateSpecialChar = /^[0-9\.]+$/;
    if (x.match(ValidateSpecialChar)) {
        return true;
    }
    else {
        return false;
    }
}
function ValidateSpecialCharacter_Project(x) {
    var ValidateSpecialChar = /^[A-Za-z0-9\_\-\s]+$/;
    if (x.match(ValidateSpecialChar)) {
        return true;
    }
    else {
        return false;
    }
}
// Doan code chan nut Enter
document.onkeypress = stopRKey;

function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}

function multiLineHtmlEncode(value) {
    var lines = value.split(/\r\n|\r|\n/);
    for (var i = 0; i < lines.length; i++) {
        lines[i] = htmlEncode(lines[i]);
    }
    return lines.join('\r\n');
}

function htmlEncode(value) {
    if (value) {
        return jQuery('<div/>').text(value).html();
    } else {
        return '';
    }
}
function htmlDecode(value) {
    if (value) {
        return $('<div/>').html(value).text();
    } else {
        return '';
    }
}
function htmldecode(value) {
    if (value) {
        value.replace(/&amp;/g, '&');
    } else {
        return '';
    }
}

//-----------MAKING THREAD - START --------------
//loops through an array in segments
var threadedLoop = function () {
    var self = this;

    //holds the threaded work
    var thread = {
        work: null,
        wait: null,
        index: 0,
        //total: array.length,
        finished: false
    };

    //set the properties for the class
    //this.collection = array;
    this.finish = function () { };
    this.action = function () { throw "You must provide the action to do for each element"; };
    this.interval = 1;

    //set this to public so it can be changed
    //var chunk = parseInt(thread.total * .005);
    //this.chunk = (chunk == NaN || chunk == 0) ? thread.total : chunk;

    //end the thread interval
    thread.clear = function () {
        window.clearInterval(thread.work);
        window.clearTimeout(thread.wait);
        thread.work = null;
        thread.wait = null;
    };

    //checks to run the finish method
    thread.end = function () {
        if (thread.finished) { return; }
        self.finish();
        thread.finished = true;
    };

    //set the function that handles the work
    thread.process = function () {
        //if (thread.index >= thread.total) { return false; }

        self.action();

        // Finished action
        thread.clear();
        thread.end();

        //return the process took place
        return true;

    };

    //set the working process
    self.start = function () {
        thread.finished = false;
        thread.index = 0;
        thread.work = window.setInterval(thread.process, self.interval);
    };

    //stop threading and finish the work
    self.wait = function (timeout) {

        //create the waiting function
        var complete = function () {
            thread.clear();
            thread.process();
            thread.end();
        };

        //if there is no time, just run it now
        if (!timeout) {
            complete();
        }
        else {
            thread.wait = window.setTimeout(complete, timeout);
        }
    };

};

//-----------MAKING THREAD - END ----------------

function hideMessage() {
    var container = document.getElementById("alertMessageBox");
    if (container != undefined && container != null) {
        container.setAttribute("aria-hidden", "true");
        container.classList.toggle('hide');
        container.style.display = "none";
    }
}

function hideMessage(dvContainerId) {
    var container = $("#" + dvContainerId);
    if (container != undefined && container != null) {
        container.attr({ "class": "hide", "aria-hidden": "true" });
        container.css("display", "none");
    }
}

function hideMessageWithTitle(delay) {
    var container = document.getElementById("alertMessageBox");
    if (delay == undefined) {
        delay = 0;
    }
    if (container != undefined && container != null) {
        setTimeout(function () {
            container.className = "hide";
            container.style.display = "none";
            container.setAttribute("aria-hidden", "true");
        }, delay);
    }
}

function showMessage(msg, error_type) {
    var css_class = '';
    var title = '';
    if (error_type == undefined || error_type == '') {
        error_type = 'error';
    }
    if (msg == undefined || msg == '') {
        msg = 'Error';
    }
    if (error_type.toLowerCase() == "error") {
        css_class = 'alert alert-error';
        title = 'Error';
    }
    if (error_type.toLowerCase() == "success") {
        css_class = 'alert alert-success';
        title = 'Success !';
    }
    if (error_type.toLowerCase() == "warning") {
        css_class = 'alert alert-warning';
        title = 'Warning';
    }
    if (error_type.toLowerCase() == "info") {
        css_class = 'alert alert-info';
        title = 'Info';
    }

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessageBox");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;

        //if (container.className.match(/(?:^|\s)fade(?!\S)/)) {
        //    container.classList.remove('fade');
        //if (container.className.match(/(?:^|\s)modal(?!\S)/)) {
        //   container.classList.remove('modal');
        //if (container.className.match(/(?:^|\s)hide(?!\S)/)) {
        //    container.classList.remove('hide');
        //    //replace all existing classes with one or more new classes
        //    //container.className = "MyClass";
        //    //add an additional class to an element
        //    //container.className += " MyClass";
        //    //remove a class from an element
        //    //container.className = container.className.replace(/(?:^|\s)hide(?!\S)/g, '')
        //    //toggle a class (remove if exists else add it):
        //    //container.classList.toggle('hide');
        //   //container.classList.add('class');           
        //}
    }

}

function showMessageWithTitle(title, msg, error_type) {
    var css_class = '';

    if (title == undefined || title == '')
        title = 'Error';
    if (msg == undefined || msg == '')
        msg = 'Error';
    if (error_type == undefined || error_type == '')
        error_type = 'error';
    if (error_type.toString().toLowerCase() == "error")
        css_class = 'alert alert-error';
    if (error_type.toString().toLowerCase() == "success")
        css_class = 'alert alert-success';
    if (error_type.toString().toLowerCase() == "warning")
        css_class = 'alert alert-warning';
    if (error_type.toString().toLowerCase() == "info")
        css_class = 'alert alert-info';

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessageBox");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;
    }
    $('html, body').animate({ scrollTop: 80 }, 'slow');
}

function showMessageWithTitle(title, msg, error_type, timeout) {
    var css_class = '';

    if (title == undefined || title == '')
        title = 'Error';
    if (msg == undefined || msg == '')
        msg = 'Error';
    if (error_type == undefined || error_type == '')
        error_type = 'error';
    if (error_type.toString().toLowerCase() == "error")
        css_class = 'alert alert-error';
    if (error_type.toString().toLowerCase() == "success")
        css_class = 'alert alert-success';
    if (error_type.toString().toLowerCase() == "warning")
        css_class = 'alert alert-warning';
    if (error_type.toString().toLowerCase() == "info")
        css_class = 'alert alert-info';

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessageBox");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;
    }


    if (timeout != undefined || timeout == '') {
        setTimeout(function () {
            if (container != undefined && container != null) {
                container.className = "hide";
                container.style.display = "none";
                container.setAttribute("aria-hidden", "true");
            }
        }, timeout);
    }

    $('html, body').animate({ scrollTop: 80 }, 'slow');
}

function showMessageBox(msg, error_type, container_id) {
    var css_class = '';
    var title = '';
    if (error_type == undefined || error_type == '') {
        error_type = 'error';
    }
    if (msg == undefined || msg == '') {
        msg = 'Error';
    }
    if (container_id == undefined || container_id == '') {
        container_id = 'alertMessageBox';
    }
    if (error_type.toString().toLowerCase() === "error") {
        css_class = 'alert alert-error';
        title = 'Error';
    }
    if (error_type.toString().toLowerCase() === "success") {
        css_class = 'alert alert-success';
        title = 'Success !';
    }
    if (error_type.toString().toLowerCase() === "warning") {
        css_class = 'alert alert-warning';
        title = 'Warning';
    }
    if (error_type.toString().toLowerCase() == "info") {
        css_class = 'alert alert-info';
        title = 'Info';
    }

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessageBox");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;
    }
    $('html, body').animate({ scrollTop: 80 }, 'slow');
}

function showMessagePopUpWithTitleWithoutTimeout(title, msg, error_type) {
    var css_class = '';

    if (title == undefined || title == '')
        title = 'Error';
    if (msg == undefined || msg == '')
        msg = 'Error';
    if (error_type == undefined || error_type == '')
        error_type = 'error';
    if (error_type.toLowerCase() == "error")
        css_class = 'alert alert-error';
    if (error_type.toString().toLowerCase() == "success")
        css_class = 'alert alert-success';
    if (error_type.toString().toLowerCase() == "warning")
        css_class = 'alert alert-warning';
    if (error_type.toString().toLowerCase() == "info")
        css_class = 'alert alert-info';

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessagePopUp");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;
    }
}

function showMessagePopUpWithTitle(title, msg, error_type, timeout) {
    var css_class = '';

    if (title == undefined || title == '')
        title = 'Error';
    if (msg == undefined || msg == '')
        msg = 'Error';
    if (error_type == undefined || error_type == '')
        error_type = 'error';
    if (error_type.toString().toLowerCase() == "error")
        css_class = 'alert alert-error';
    if (error_type.toString().toLowerCase() == "success")
        css_class = 'alert alert-success';
    if (error_type.toString().toLowerCase() == "warning")
        css_class = 'alert alert-warning';
    if (error_type.toString().toLowerCase() == "info")
        css_class = 'alert alert-info';

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessagePopUp");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;
    }


    if (timeout != undefined || timeout == '') {
        setTimeout(function () {
            if (container != undefined && container != null) {
                container.className = "hide";
                container.style.display = "none";
                container.setAttribute("aria-hidden", "true");
            }
        }, timeout);
    }

    $('html, body').animate({ scrollTop: 80 }, 'slow');
}

function showMessageBoxWithTitle(title, msg, error_type, container_id) {
    var css_class = '';

    if (title == undefined || title == '') {
        title = 'Error';
    }
    if (msg == undefined || msg == '') {
        msg = 'Error';
    }
    if (container_id == undefined || container_id == '') {
        container_id = 'alertMessageBox';
    }

    if (error_type == undefined || error_type == '') {
        error_type = 'error';
    }
    if (error_type.toString().toLowerCase() === "error") {
        css_class = 'alert alert-error';
        _title = (title != '' ? title : 'Error');
    }
    if (error_type.toString().toLowerCase() === "success") {
        css_class = 'alert alert-success';
        _title = (title != '' ? title : 'Success !');
    }
    if (error_type.toString().toLowerCase() === "warning") {
        css_class = 'alert alert-warning';
        _title = (title != '' ? title : 'Warning');
    }
    if (error_type.toString().toLowerCase() == "info") {
        css_class = 'alert alert-info';
        _title = (title != '' ? title : 'Info');
    }

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                               + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = document.getElementById("alertMessageBox");
    if (container != undefined && container != null) {
        var status = container.getAttribute("aria-hidden");
        if (status == "true") {
            container.setAttribute("aria-hidden", "false");
        }
        container.style.display = "block";
        container.className = "open";
        container.setAttribute("aria-hidden", "false");
        container.innerHTML = message;
    }

    $('html, body').animate({ scrollTop: 80 }, 'slow');
}

function showMessageWithTitleByContainerId(dvContainerId, title, msg, error_type, timeout) {
    var css_class = '';

    if (title == undefined || title == '')
        title = 'Error';
    if (msg == undefined || msg == '')
        msg = 'Error';
    if (error_type == undefined || error_type.toString() == '')
        error_type = 'error';
    if (error_type.toString().toLowerCase() == "error")
        css_class = 'alert alert-error';
    if (error_type.toString().toLowerCase() == "success")
        css_class = 'alert alert-success';
    if (error_type.toString().toLowerCase() == "warning")
        css_class = 'alert alert-warning';
    if (error_type.toString().toLowerCase() == "info")
        css_class = 'alert alert-info';

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';
    var container = $("#" + dvContainerId);
    if (container != undefined && container != null) {
        var status = container.attr("aria-hidden");
        if (status == "true")
            container.attr("aria-hidden", "false");
        container.css("display", "block");
        container.attr({ "class": "open", "aria-hidden": "false" });
        container.html(message);
    }

    if (timeout != undefined || timeout == '') {
        setTimeout(function () {
            if (container != undefined && container != null) {
                container.attr({ "class": "hide", "aria-hidden": "true" });
                container.css("display", "none");
            }
        }, timeout);
    }
}

//-------------------------------------------------------
function ShowHiveDiv(selected_value, divBox1, divBox2) {
    var divContainer1 = $("#" + divBox1);
    var divContainer2 = $("#" + divBox2);
    if (selected_value == "1") {
        divContainer1.css("display", "none");
        divContainer2.css("display", "block");
    } else {
        divContainer1.css("display", "block");
        divContainer2.css("display", "none");
    }
    return false;
}

//---------------------------------------------------------
function hideModal(modal_id, delay) {
    if (delay == undefined) {
        delay = 0;
    }
    setTimeout(function () {
        $("#" + modal_id).modal('hide');
    }, delay);
}
//-----------------------------------------------
//-----------------------------------------------
var isUserAnswered = true;

// Show dialog with OK button: Information, warning, Error - START
function showContentPopUp(title, msg) {
    $("#modal-header").html(title);
    $("#modal-content").html(msg);
    $("#modal-popup").modal('show');
    $('html, body').animate({ scrollTop: 80 }, 'slow');
}

function hideContentPopUp() {
    $('#modal-content').html('');
    $("#modal-popup").modal('hide');


}
function scrollToElementInDiv(elementId, divContainerId) {
    $('#' + divContainerId).animate({
        scrollTop: $(this).parent().scrollTop()
    }, {
        duration: 1000,
        specialEasing: {
            width: 'linear',
            height: 'easeOutBounce'
        },
        complete: function (e) {
            console.log("animation completed");
        }
    });
}

function scrollToTopInPopUp() {
    $('#modal-content').animate({ scrollTop: $(this).parent().scrollTop() }, 'slow');
}

//function showPopUpBootstrap(id) {
//    var popUpId = 'myModal';
//    if (id != null && id != '')
//        popUpId = id;
//    $('#' + popUpId).modal('show');
//    $('html, body').animate({ scrollTop: 80 }, 'slow');
//}

//function closePopUpBootstrap(id) {
//    var popUpId = 'myModal';
//    if (id != null && id != '')
//        popUpId = id;
//    $('#' + popUpId).modal('hide');
//}

////function showDialog(msg, title) {

////    // Reset answered flag
////    isUserAnswered = false;

////    // Define button OK
////    var buttonOpts = {};
////    buttonOpts['OK'] = function () {
////        isUserAnswered = true;
////        $(this).dialog("close");
////    };

////    var $dialog = $('<div title="' + title + '"></div>')
////                    .html(msg)
////                    .dialog({
////                        modal: true,
////                        autoOpen: false,
////                        buttons: buttonOpts,
////                        create: function (event, ui) {
////                            $(event.target).parent().css('position', 'fixed');
////                        },
////                        close: function () { isUserAnswered = true; }
////                    });

////    // Show dialog
////    $dialog.dialog('open');
////}



////function showDialogs(id, msg, titles) {

////    // Reset answered flag
////    isUserAnswered = false;

////    // Define button OK
////    var buttonOpts = {};
////    buttonOpts['OK'] = function () {
////        isUserAnswered = true;
////        $(this).dialog("close");
////    };

////    $(id).html(msg)
////                    .dialog({
////                        modal: true,
////                        title: titles,
////                        autoOpen: false,
////                        buttons: buttonOpts,
////                        close: function () { isUserAnswered = true; }
////                    });

////    // Show dialog
////    $(id).dialog('open');
////}
//// Show dialog with OK button: Information, warning, Error - END

//// Show dialog with OK button: Information, warning, Error
//function showConfirmDialog(msg, title, yesFunct, noFunct) {

//    // Reset answered flag
//    isUserAnswered = false;

//    // Define button YES
//    var buttonOpts = {};
//    buttonOpts['Yes'] = function () {
//        isUserAnswered = true;
//        if (null != yesFunct) {
//            yesFunct();
//        }
//        $(this).dialog("close");
//    };

//    // Define button NO
//    buttonOpts['No'] = function () {
//        isUserAnswered = true;
//        if (null != noFunct) {
//            noFunct();
//        }
//        $(this).dialog("close");
//    };

//    var $dialog = $('<div title="' + title + '"></div>')
//                    .html(msg)
//                    .dialog({
//                        modal: true,
//                        autoOpen: false,
//                        buttons: buttonOpts,
//                        create: function (event, ui) {
//                            $(event.target).parent().css('position', 'fixed');
//                        },
//                        open: function () {
//                            $('.ui-dialog-buttonpane')
//                                    .find('button:contains("' + 'No' + '")')
//                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-cancel"></span>');
//                            $('.ui-dialog-buttonpane')
//                                    .find('button:contains("' + 'Yes' + '")')
//                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-disk"></span>');
//                        },
//                        close: function () {
//                            CloseDialog();
//                        }
//                    });

//    // Show dialog
//    $dialog.dialog('open');
//}

//// Show dialog with OK button: Information, warning, Error
//function showConfirmDialog_Close(msg, title, yesFunct, noFunct, closeFunct) {

//    // Reset answered flag
//    isUserAnswered = false;

//    // Define button YES
//    var buttonOpts = {};
//    buttonOpts['Yes'] = function () {
//        isUserAnswered = true;
//        if (null != yesFunct) {
//            yesFunct();
//        }
//        $(this).dialog("close");
//    };

//    // Define button NO
//    buttonOpts[CtrNo()] = function () {
//        isUserAnswered = true;
//        if (null != noFunct) {
//            noFunct();
//        }
//        $(this).dialog("close");
//    };

//    var $dialog = $('<div title="' + title + '"></div>')
//                    .html(msg)
//                    .dialog({
//                        modal: true,
//                        autoOpen: false,
//                        buttons: buttonOpts,
//                        create: function (event, ui) {
//                            $(event.target).parent().css('position', 'fixed');
//                        },
//                        open: function () {
//                            $('.ui-dialog-buttonpane')
//                                    .find('button:contains("' + 'No' + '")')
//                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-cancel"></span>');
//                            $('.ui-dialog-buttonpane')
//                                    .find('button:contains("' + 'Yes' + '")')
//                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-disk"></span>');
//                        },
//                        close: function () {
//                            if (null != closeFunct) {
//                                closeFunct();
//                            }
//                        }
//                    });

//    // Show dialog
//    $dialog.dialog('open');
//}

//function CloseDialog() {
//}
//// Waiting until user close Dialog to execute func
//function ActionAfterDialog(func) {
//    var int = window.setInterval(function () {
//        if (true == isUserAnswered) {
//            func();
//            window.clearInterval(int);
//        }
//    }, 1000);
//}

// Convert to Japanese Type with comma:
function ParseMoneyJapan(amount) {
    var delimiter = ","; // replace comma if desired
    var a = amount.split('.', 2)
    var d = a[1];
    var i = parseInt(a[0]);
    if (isNaN(i)) { return ''; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    var n = new String(i);
    var a = [];
    while (n.length > 3) {
        var nn = n.substr(n.length - 3);
        a.unshift(nn);
        n = n.substr(0, n.length - 3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    amount = n;
    if (undefined != d) {
        if (d.length > 1) {
            amount = n + '.' + d;
        }
    }

    amount = minus + amount;

    return amount;
}

// Convert date to YYYYMMDD
Date.prototype.yyyymmdd = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return yyyy + (mm[1] ? mm : "0" + mm[0]) + (dd[1] ? dd : "0" + dd[0]); // padding
};

// Convert date to YYYYMMDD
Date.prototype.yyyymmddFormat = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return yyyy + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + (dd[1] ? dd : "0" + dd[0]); // padding
};

function daysBetween(date1, date2) {
    //Get 1 day in milliseconds
    var one_day = 1000 * 60 * 60 * 24;

    // Convert both dates to milliseconds
    var date1_ms = date1.getTime();
    var date2_ms = date2.getTime();

    // Calculate the difference in milliseconds
    var difference_ms = date2_ms - date1_ms;

    // Convert back to days and return
    return Math.round(difference_ms / one_day);
}


// Set center cho form va doubleclick canh center form
function DragFormCenter(div, isFormJQGrid) {
    var width_div = div.width() / 2;
    var width_window = $(window).width() / 2;
    var height_div = div.height() / 2;
    var height_window = $(window).height() / 2;
    var left_div = 0;
    var top_div = 0;
    if (width_window - width_div > 0) {
        left_div = width_window - width_div;
    }
    if (height_window - height_div > 0) {
        top_div = height_window - height_div;
    }

    if (isFormJQGrid == true) {
        var form = div.find('form');
        if (width_window - width_div < 0) {
            form.css('width', $(window).width() - 10);
        }
        if (height_window - height_div < 0) {
            form.css('height', $(window).height() - (div.height() - form.height()) - 10);
        }

        if ($.isIE == false) {
            div.css({ 'width': 'auto', 'height': 'auto' });
        }
    }

    div.dblclick(function () {
        div.css({ 'left': left_div, 'top': top_div });
    });
    div.css({ 'left': left_div, 'top': top_div });

    $('.ui-widget-overlay').unbind();
}

function ClearWindowSelection(window) {
    if (jQuery.browser.msie == true) { // IE?
        if (document.selection) {
            //alert(document.selection);
            document.selection.clear();
        }
    }
    else if (jQuery.browser.chrome == true) { // Chrome
        if (window.getSelection().empty) {
            window.getSelection().empty();
        }
    }
    else if (jQuery.browser.mozilla == true) { // Firefox
        if (window.getSelection().removeAllRanges) {
            window.getSelection().removeAllRanges();
        }
    }
}

function CreateCookie(name, value, days) {
    if (days) {
        var date = new Date();

        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));

        var expires = '; expires=' + date.toGMTString();
    } else {
        expires = '';
    }

    document.cookie = name + '=' + encodeURIComponent(value) + expires + '; path=/';
}

function ReadCookie(name) {
    var nameEQ = name + '=';
    var ca = document.cookie.split(';');

    for (var i = 0; i < ca.length; i++) {
        var c = decodeURIComponent(ca[i]);

        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }

    return null;
}

// Ham dinh nghia khi Change Language
function ChangeLanguage() {
    SaveCurrentInfoPage();
}

// Ham dinh nghia khi click Menu
function CallFunctionClickMenu() {
    DestructionCookie();
}

function SaveCurrentInfoPage() {
}

function DestructionCookie() {
}

// Kiem tra trinh duyet dang thao tac
$.isOpera = !!(window.opera && window.opera.version);  // Opera 8.0+
$.isFirefox = CheckBrowser('MozBoxSizing');                 // FF 0.8+
$.isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
$.isChrome = !$.isSafari && CheckBrowser('WebkitTransform');  // Chrome 1+
$.isIE = false || CheckBrowser('msTransform');  // At least IE6

function CheckBrowser(prop) {
    return prop in document.documentElement.style;
}

function currencyFmatter(el, cellval, opts) {
    if (el != null && !isNaN(el)) {
        var current = formatCurrency(el);
        return current.replace('.00', '');
    }
    return "￥0";
}

function formatCurrency(num) {
    if (!num) return "￥0";
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
              num.substring(num.length - (4 * i + 3));
    if (parseFloat(cents) == 0) {
        return (((sign) ? '' : '-') + '￥' + num);
    }
    else {
        return (((sign) ? '' : '-') + '￥' + num + '.' + cents);
    }
}

function unformatCurrency(cellvalue, options) {

    return cellvalue.replace("￥", "").replace(/\$|\,/g, '');
}

function IsNullOrEmpty(object) {
    if (object == null) { return true; }
    else {
        if (object.toString().trim() == "") { return true; }
        else { return false; }
    }
}

function ParseString(object) {
    if (IsNullOrEmpty(object)) {
        return "";
    }

    return object.toString();
}

// Kiem tra ki tu duoc nhap trong cac textbox co kieu du lieu so.
function ValidateKeyDownDataIsNumber(e) {
    if ((48 <= e.keyCode && e.keyCode <= 57)
        || (33 < e.keyCode && e.keyCode < 40)
        || e.keyCode == 0
        || e.keyCode == 8
        || e.keyCode == 32
        || e.keyCode == 45
        || e.keyCode == 46)
        return true;
    return false;
}

/// <summary>
/// Add AfterShow Event to DatePicker
/// </summary>
function AddAfterShowDatePicker() {
    $.datepicker._updateDatepicker_original = $.datepicker._updateDatepicker;
    $.datepicker._updateDatepicker = function (inst) {
        $.datepicker._updateDatepicker_original(inst);
        var afterShow = this._get(inst, 'afterShow');
        if (afterShow)
            afterShow.apply((inst.input ? inst.input[0] : null));  // trigger custom callback
    };
}

//try {
//    $.validator.addMethod("time", function (value, element) {
//        return this.optional(element) || /^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$/i.test(value);
//    }, "Please enter a valid time.");
//} catch (e) {

//}

//$(function() {
//    $('#image').checkFileType({
//        allowedExtensions: ['jpg', 'jpeg'],
//        success: function() {
//            alert('Success');
//        },
//        error: function() {
//            alert('Error');
//        }
//    });

//});​
$.fn.checkFileType = function (options) {
    var defaults = {
        allowedExtensions: [],
        success: function () { },
        error: function () { }
    };
    options = $.extend(defaults, options);

    return this.each(function () {

        $(this).on('change', function () {
            var value = $(this).val(),
                file = value.toLowerCase(),
                extension = file.substring(file.lastIndexOf('.') + 1);

            if ($.inArray(extension, options.allowedExtensions) == -1) {
                options.error();
                $(this).focus();
            } else {
                options.success();

            }

        });

    });
};



$.fn.returnPressNumber = function (x) {
    return this.each(function () {
        $(this)
        .attr('type', 'text')
        .keypress(function (e) {
            console.log(e.keyCode);
            if (ValidateKeyDownDataIsNumber(e) == true) {
                if (x != undefined) {
                    return x(this);
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        })
        .keyup(function () {
            // Format Number
            var num = unformatNumber($(this).val());
            $(this).val(addDotToNumber(num));
        });
    });
};

$.fn.formatNumber = function () {
    return $(this).val(addDotToNumber($(this).val()));
}

function addDotToNumber(nStr) {
    try {
        return parseFloat(nStr).formatMoney(2, ',', '.');
    } catch (e) {
        return 0.00;
    }
}

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    var result = s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    if (parseFloat(result.substring(result.length - c, result.length)) == 0) {
        result = result.split(d)[0];
    }
    return result;
};


function unformatNumber(cellvalue, options) {
    return cellvalue.replace(/\$|\./g, '').replace(',', '.');
}

function DefaultClassNumber() {
    $('.number').each(function () {
        if ($(this).is('input'))
            $(this).val(addDotToNumber($(this).val()));
        else if (!isNaN($(this).html().replace(',', '.'))) // Truong hop load Server ve
            $(this).html(addDotToNumber($(this).html().replace(',', '.')));
    });
}

function FormatNumberFromServer(val) {
    if (isNaN(val))
        return val.replace(',', '.');
    else
        return val;
}

$(document).ready(function () {
    DefaultClassNumber();
});

///////////////////////////////////////////////////////////////////
/////////////////////// Start Processing Ajax /////////////////////
///////////////////////////////////////////////////////////////////

//$(document).ready(function () {
//    SetOverlayScreen(false);
//    $('#top a[role="menuitem"]').on('click', function () {
//        SetOverlayScreen(true);
//    });
//});

//$(document).ajaxStart(function () {
//    SetOverlayScreen(true);
//});

//$(document).ajaxComplete(function () {
//    SetOverlayScreen(false);
//});

var opts = {
    lines: 17, // The number of lines to draw
    length: 28, // The length of each line
    width: 15, // The line thickness
    radius: 51, // The radius of the inner circle
    corners: 1, // Corner roundness (0..1)
    rotate: 34, // The rotation offset
    direction: 1, // 1: clockwise, -1: counterclockwise
    color: '#000', // #rgb or #rrggbb or array of colors
    speed: 1.7, // Rounds per second
    trail: 60, // Afterglow percentage
    shadow: true, // Whether to render a shadow
    hwaccel: false, // Whether to use hardware acceleration
    className: 'spinner', // The CSS class to assign to the spinner
    zIndex: 2e9, // The z-index (defaults to 2000000000)
    top: '50%', // Top position relative to parent in px
    left: '50%' // Left position relative to parent in px
};

//$(document).ready(function () {
//    if ($('#container div[class="spinner"]').size() == 0) {
//        var target = document.getElementById('container');
//        var spinner = new Spinner(opts).spin(target);
//        $('div.spinner').hide();
//    }
//});

function SetOverlayScreen(isSet) {
    //if (isSet == true) {
    //   var isShow = true;
    //    // Check truong hop AutoComplete
    //    $('div[class="select2-drop select2-display-none select2-with-searchbox select2-drop-active"]').each(function () {
    //        if ($(this).css('display') == 'block') {
    //            isShow = false;
    //            return;
    //        }
    //    });

    //    if (isShow == true) {
    //        $('body div[class*="ajax_loading"]').css('display', 'block');
    //        $('div.spinner').show();
    //    }
    //}
    //else {
    $('body div[class*="ajax_loading"]').css('display', 'none');
    $('div.spinner').hide();
    //}
}
///////////////////////////////////////////////////////////////////
/////////////////////// End Processing Ajax ///////////////////////
///////////////////////////////////////////////////////////////////

//function AddAltToImg (imageTagAndImageID, imageContext) {
//    ///<signature>
//    ///<summary> Adds an alt tab to the image
//    // </summary>
//    //<param name="imgElement" type="String">The image selector.</param>
//    //<param name="ContextForImage" type="String">The image context.</param>
//    ///</signature>
//    var imageElement = $(imageTagAndImageID, imageContext);
//    imageElement.attr('alt', imageElement.attr('id').replace(/ID/, ''));
//}

//function IncludeJquery() {
//    if (typeof jQuery == 'undefined') {
//        var e = document.createElement('script');
//        e.src = '@Url.Content("~/Scripts/jquery-1.11.1.js")';
//        e.type = 'text/javascript';
//        document.getElementsByTagName("head")[0].appendChild(e);

//    }
//}