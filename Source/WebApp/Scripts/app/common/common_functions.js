function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}


function FormatLine() {
    $(".row-fluid .span2 label").each(function () {
        if ($(this).height() == 40) {
            $(this).css("margin-top", "-10px").css("padding", "0px");
        }
    });
}

function HandleBootstrapCheckBox() {
    var checkBoxSelector = $('input[type=checkbox][class="check-box"]');
    var hiddenSelector = $("input:hidden[name=" + checkBoxSelector.attr("name") + "]");  
    checkBoxSelector.click(function () {
        var checkBoxStatus = $(this).is(":checked");
        checkBoxSelector.attr("checked", checkBoxStatus);
        checkBoxSelector.val(checkBoxStatus);
        hiddenSelector.val(checkBoxStatus);
    });
}


function handleRadioButton(elementId) {
    var radioSelector = $("input:radio[name=" + elementId + "]");
    var hiddenSelector = $("input:hidden[name=" + elementId + "]");
    radioSelector.click(function () {
        var radioStatus = $(this).is(":checked");
        radioSelector.attr("checked", radioStatus);
        radioSelector.val(radioStatus);
        hiddenSelector.val(radioStatus);
    });
}

function handleCheckBox(checkFieldId) {
    $("input:checkbox[name=" + checkFieldId + "]").click(function () {
        var checkBoxStatus = $(this).is(":checked");
        $(this).attr("checked", checkBoxStatus);
        $(this).val(checkBoxStatus);
        $("input:hidden[name=" + checkFieldId + "]").val(checkBoxStatus);
    });
}

function handleCheckBoxes() {
    $('input:checkbox').click(function () {
        var checkBoxStatus = $(this).is(":checked");
        $(this).attr("checked", checkBoxStatus);
        $(this).val(checkBoxStatus);
        $("input:hidden[name=" + $(this).attr("name") + "]").val(checkBoxStatus);
    });
}

function handleCheckBoxesWithStatus(status) {
    $('input:checkbox').attr("checked", status);
    $('input:checkbox').val(status);
    $("input:hidden[name=" + $(this).attr("name") + "]").val(status);

    $('input:checkbox').click(function () {
        var checkBoxStatus = $(this).is(":checked");
        $(this).attr("checked", checkBoxStatus);
        $(this).val(checkBoxStatus);
        $("input:hidden[name=" + $(this).attr("name") + "]").val(checkBoxStatus);
    });
}

function handleCheckBoxStatus(checkFieldId, chkStatus) {
    var checkBoxSelector = $("input:checkbox[name=" + checkFieldId + "]");
    var hiddenSelector = $("input:hidden[name=" + checkFieldId + "]");

    checkBoxSelector.attr("checked", chkStatus);
    checkBoxSelector.val(chkStatus);
    hiddenSelector.val(chkStatus);

    checkBoxSelector.click(function () {
        var checkBoxStatus = $(this).is(":checked");
        $(this).attr("checked", checkBoxStatus);
        $(this).val(checkBoxStatus);
        hiddenSelector.val(checkBoxStatus);
    });
}

function PopulateDataTable(num_rows) {
    if (LanguageId == 124) {
        var table = $('.use-dataTable').dataTable({
            //"bAutoWidth": false,
            "aoColumnDefs": [{
                "bSortable": false,
                "aTargets": ["no-sort"]
            }],
            "scrollX": true,
            'iDisplayLength': num_rows
        });
    } else {
        var table = $('.use-dataTable').dataTable({
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
            "scrollX": true,
            'iDisplayLength': num_rows
        });
    }
    ResizeDataTable();
}

function InvokeDataTables() {
    if (LanguageId == 124) {
        var table = $('.use-dataTable').dataTable({
            //"bAutoWidth": false,
            "aoColumnDefs": [{
                "bSortable": false,
                "aTargets": ["no-sort"]                
            }],
            "scrollX": true,
            'iDisplayLength': 10
        });
    } else {
        var table = $('.use-dataTable').dataTable({
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
            "scrollX": true,
            'iDisplayLength': 10
        });
    }
    ResizeDataTable();
}

function ResizeDataTable() {
    $(".dataTables_scrollHead").css("width", "100%").css("margin-left", "0px");
    $(".dataTables_scrollHeadInner").css("width", "100%").css("margin-left", "0px");
    $(".dataTables_scrollHeadInner .use-dataTable").css("width", "99%").css("margin-left", "0px");
    $(".dataTables_scrollBody .use-dataTable").css("width", "100%").css("margin-left", "0px");
    $(".dataTables_scrollBody").css("width", "100%").css("margin-left", "0px");
    //$(".dataTables_scrollHeadInner").css("width", $(".dataTables_scrollBody .use-dataTable").width() * 100 / 99).css("margin-left", "0px");
}

function InvokeDateTimePicker() {
    $('.datetimepicker2').datetimepicker({
        format: 'dd/mm/yyyy',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        pickTime: false,
        minDate: '1/1/1900'
    }).on('changeDate', function (e) {
        var tmpId = $(this).find('input[type=text]').attr('id');
        var hiddenId = tmpId.substring(0, tmpId.length - 3);

        $(this).datetimepicker('hide');

        $('#' + hiddenId).val($('#' + tmpId).val());
        $('#' + hiddenId).trigger('change');

        /*remove qtip error message*/
        var qtip = $(this).find('input[type=text]').attr('aria-describedby');
        $('#' + qtip).remove();
    });


    $('.datetimepicker').datetimepicker({
        format: 'dd/mm/yyyy',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        pickTime: false,
        minDate: '1/1/1900'
    }).on('changeDate', function (e) {

        var tmpId = $(this).find('input[type=text]').attr('id');
        var hiddenId = tmpId.substring(0, tmpId.length - 3);


        $(this).datetimepicker('hide');
        /* Chọn date tại datetime picker => cập nhật lại hidden input theo format của nước: 
        // EN => MM/dd/yyyy; 
        // VN => dd/MM/yyyy;*/

        var result = '';
        //if ($('input[name=' + tmpId + ']').val() != '') {
        //    if (LanguageId == 124) {
        var arr = $('input[name=' + tmpId + ']').val().split("/");
        result = arr[1] + '/' + arr[0] + '/' + arr[2];
        //} else {
        //    result = $('input[name=' + tmpId + ']').val();
        //}
        //}


        $('input[name=' + hiddenId + ']').val(result);
        $('input[name=' + hiddenId + ']').trigger('change');
        /*remove qtip error message*/
        var qtip = $(this).find('input[type=text]').attr('aria-describedby');
        $('#' + qtip).remove();
    });

    //$('.timepicker').datetimepicker({
    //    language: 'vi-VN',
    //    pickDate: false,
    //    timeFormat: 'hh:mm',
    //    pickSeconds: false,
    //    showSecond: false
    //});
}

function ValidateFormWithQtip(formId) {
    var formName = (formId == undefined || formId == null || formId == '') ? 'myform' : formId;
    $('.qtip').remove();
    // Run this function for all validation error messages
    $('#' + formName + ' .field-validation-error').each(function () {

        // Get the name of the element the error message is intended for
        // Note: ASP.NET MVC replaces the '[', ']', and '.' characters with an
        // underscore but the data-valmsg-for value will have the original characters
        var inputElem = '#' + $(this).attr('data-valmsg-for').replace('.', '_').replace('[', '_').replace(']', '_');

        var corners = ['left bottom', 'top left'];
        var flipIt = $(inputElem).parents('span.right').length > 0;

        // Hide the default validation error
        $(this).addClass('Hidden');

        // Show the validation error using qTip
        $(inputElem).filter(':not(.valid)').qtip({
            content: { text: $(this).text() }, // Set the content to be the error message
            position: {
                my: corners[flipIt ? 0 : 1],
                at: corners[flipIt ? 1 : 0],
                viewport: $(window)
            },
            show: { event: false, when: false, ready: true },
            hide: false,
            style: { classes: 'ui-tooltip-red' }
        });
    });
    $.validator.unobtrusive.parse($('#' + formName));
}

function PopulateDropDownListAutoComplete(strSelectBox, strHiddenId, strHiddenName, strPlaceholder, requestUrl) {
    var pageSize = 20;
    var SelectBox = $("#" + strSelectBox);
    var HiddenId = $("#" + strHiddenId);
    var HiddenName = $("#" + strHiddenName);

    SelectBox.select2({
        placeholder: strPlaceholder,
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            url: requestUrl,
            dataType: 'json',
            quietMillis: 100,  //How long the user has to pause their typing before sending the next request
            data: function (term, page) {
                return {
                    pageSize: pageSize,
                    pageNum: page,
                    searchTerm: term
                };
            },
            results: function (data, page) {
                //Used to determine whether or not there are more results available,
                //and if requests for more data should be sent in the infinite scrolling
                var more = (page * pageSize) < data.Total;
                return { results: data.Results, more: more };
            }
        },
        formatSelection: function (result) {
            HiddenId.val(result.id);
            if (LanguageId == 124) {
                HiddenName.val(result.name);
                HiddenId.trigger("change");
                return result.name;
            } else {
                HiddenName.val(result.text);
                HiddenId.trigger("change");
                return result.text;
            }
        },
        formatResult: function (result) {
            if (LanguageId == 124) {
                return result.name;
            } else {
                return result.text;
            }
        },
        initSelection: function (element, callback) {

            var selected_id = HiddenId.val();
            var selected_name = HiddenName.val();
            var data = { id: selected_id, name: selected_name, text: selected_name };
            callback(data);
        }
    });
    SelectBox.select2('val', HiddenId.val());
    SelectBox.on("select2-removed", function (e) {
        HiddenId.val('');
        HiddenName.val('');
    });
}

function PopulateCountryProvinceDistrictToDropDownList(SelectBox, strInputCountry, strInputProvince, strInputDistrict) {
    var requestUrl = areas + '/LS_tblCountry/DropdownList';
    var pageSize = 20;
    var countrySelectBox = $("#Select" + SelectBox + 'CountryID');
    var countryHiddenId = $("#" + SelectBox + 'CountryID');
    var countryHiddenName = $("#" + SelectBox + 'CountryName');
    countrySelectBox.select2({
        placeholder: strInputCountry,
        minimumInputLength: 0,
        allowClear: true,
        multiple: false,
        ajax: {
            url: requestUrl,
            dataType: 'json',
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            quietMillis: 100,  //How long the user has to pause their typing before sending the next request
            data: function (term, page) {
                return {
                    pageSize: pageSize,
                    pageNum: page,
                    searchTerm: term
                };
            },
            results: function (data, page) {
                //Used to determine whether or not there are more results available,
                //and if requests for more data should be sent in the infinite scrolling
                var more = (page * pageSize) < data.Total;
                return { results: data.Results, more: more };
            }
        },
        //Chọn xong => làm gì đó
        formatSelection: function (result) {
            countryHiddenId.val(result.id)
            PopulateProvincesToDropDownList(SelectBox);
            if (LanguageId == 124) {
                countryHiddenName.val(result.name)
                return result.name;
            } else {
                countryHiddenName.val(result.text)
                return result.text;
            }
        },
        //Chọn xong => return kết quả hiển thị
        formatResult: function (result) {
            if (LanguageId == 124) {
                return result.name;
            } else {
                return result.text;
            }
        },
        //Đầu tiên gán vào từ đầu
        initSelection: function (element, callback) {
            var selected_id = countryHiddenId.val();
            var selected_name = countryHiddenName.val();
            var data = { id: selected_id, name: selected_name, text: selected_name };
            callback(data);
        }
    });

    countrySelectBox.select2('val', countryHiddenId.val());
    PopulateProvincesToDropDownList(SelectBox);
}

function PopulateProvincesToDropDownList(SelectBox) {

    var requestUrl = areas + '/LS_tblProvince/DropdownList';
    var pageSize = 20;
    var provinceSelectBox = $("#Select" + SelectBox + 'ProvinceID');
    var provinceHiddenId = $("#" + SelectBox + 'ProvinceID');
    var provinceHiddenName = $("#" + SelectBox + 'ProvinceName');
    var countryid = $("#" + SelectBox + "CountryID").val();

    provinceSelectBox.select2({
        placeholder: strInputProvince,
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            url: requestUrl,
            dataType: 'json',
            quietMillis: 100,  //How long the user has to pause their typing before sending the next request
            data: function (term, page) {
                return {
                    pageSize: pageSize,
                    pageNum: page,
                    searchTerm: term,
                    CountryID: countryid
                };
            },
            results: function (data, page) {
                //Used to determine whether or not there are more results available,
                //and if requests for more data should be sent in the infinite scrolling
                var more = (page * pageSize) < data.Total;
                return { results: data.Results, more: more };
            }
        },
        formatSelection: function (result) {
            provinceHiddenId.val(result.id);
            PopulateDistrictsToDropDownList(SelectBox);
            if (LanguageId == 124) {
                provinceHiddenName.val(result.name);
                return result.name;
            } else {
                provinceHiddenName.val(result.text);
                return result.text;
            }
        },
        formatResult: function (result) {
            if (LanguageId == 124) {
                return result.name;
            } else {
                return result.text;
            }
        },
        initSelection: function (element, callback) {
            var selected_id = provinceHiddenId.val();
            var selected_name = provinceHiddenName.val();
            var data = { id: selected_id, name: selected_name, text: selected_name };
            callback(data);
        }
    });
    provinceSelectBox.select2('val', provinceHiddenId.val());
    PopulateDistrictsToDropDownList(SelectBox);
}

function PopulateDistrictsToDropDownList(SelectBox) {
    var requestUrl = areas + '/LS_tblDistrict/DropdownList';
    var pageSize = 20;
    var districtSelectBox = $("#Select" + SelectBox + 'DistrictID');
    var districtHiddenId = $("#" + SelectBox + 'DistrictID');
    var districtHiddenName = $("#" + SelectBox + 'DistrictName');

    var ProvinceId = $("#" + SelectBox + "ProvinceID").val();
    districtSelectBox.select2({
        placeholder: strInputDistrict,
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            url: requestUrl,
            dataType: 'json',
            quietMillis: 100,  //How long the user has to pause their typing before sending the next request
            data: function (term, page) {
                return {
                    pageSize: pageSize,
                    pageNum: page,
                    searchTerm: term,
                    ProvinceID: ProvinceId
                };
            },
            results: function (data, page) {
                //Used to determine whether or not there are more results available,
                //and if requests for more data should be sent in the infinite scrolling
                var more = (page * pageSize) < data.Total;
                return { results: data.Results, more: more };
            }
        },
        formatSelection: function (result) {
            districtHiddenId.val(result.id);
            if (LanguageId == 124) {
                districtHiddenName.val(result.name);
                return result.name;
            } else {
                districtHiddenName.val(result.text);
                return result.text;
            }
        },
        formatResult: function (result) {
            if (LanguageId == 124) {
                return result.name;
            } else {
                return result.text;
            }
        },
        initSelection: function (element, callback) {
            var selected_id = districtHiddenId.val();
            var selected_name = districtHiddenName.val();
            var data = { id: selected_id, name: selected_name, text: selected_name };
            callback(data);
        }
    });
    districtSelectBox.select2('val', districtHiddenId.val());
}

function LoadComboTreeWithAction(controlId, action) {
    $('#' + controlId).combotree({
        url: '/CommonCompany/' + action,
        textField: 'title',
        valueField: 'id',
        onLoadSuccess: function (row, data) {
            $(this).tree("collapseAll");
        },
        onSelect: function (node) {
            $("input[name='" + controlId + "']").val(node.id);
            $("input[name='" + controlId + "']").trigger("change");
        }
    });
}

function LoadComboTreeWithActionWithRequired(controlId, action, requiredMessage, selectedvalue) {

    $('#' + controlId).combotree({
        url: '/CommonCompany/' + action,
        textField: 'title',
        valueField: 'id',
        onLoadSuccess: function (row, data) {
            if (selectedvalue == null) {
                $(this).tree("collapseAll");
            }
        },
        onSelect: function (node) {
            $("input[name='" + controlId + "']").val(node.id);
            $("input[name='" + controlId + "']").trigger("change");

            // xóa qtip
            if (!isNaN(node.id)) {
                $input = $('#' + controlId).siblings().children("input");
                if ($input.attr('aria-describedby') != undefined) {
                    $('#' + $input.attr('aria-describedby')).remove();
                }
            }
        }
    });

    if (selectedvalue != null) {
        $('#' + controlId).combotree('setValue', selectedvalue);
    }
    //$input = $('#' + controlId + '.easyui-combotree').siblings().children("input");
    //$input.addClass('input-small input-validation-error');
    //$input.attr('data-val', 'true');
    //$input.attr('data-val-required', requiredMessage);
}

function LoadComboTreeWithActionWithValue(controlId, action, selectedvalue) {

    $('#' + controlId).combotree({
        url: '/CommonCompany/' + action,
        textField: 'title',
        valueField: 'id',
        onLoadSuccess: function (row, data) {
            if (selectedvalue == null) {
                $(this).tree("collapseAll");
            }
        },
        onSelect: function (node) {
            $("input[name='" + controlId + "']").val(node.id);
            $("input[name='" + controlId + "']").trigger("change");

            // xóa qtip
            if (!isNaN(node.id)) {
                $input = $('#' + controlId).siblings().children("input");
                if ($input.attr('aria-describedby') != undefined) {
                    $('#' + $input.attr('aria-describedby')).remove();
                }
            }
        }
    });

    if (selectedvalue != null) {
        $('#' + controlId).combotree('setValue', selectedvalue);
    }
}