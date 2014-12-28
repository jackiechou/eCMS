function AlterError(errorMessage)
{
    showMessageWithTitle('Error!', errorMessage, 'error');
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function AlterWarning(errorMessage) {
    showMessageWithTitle('Warning!', errorMessage, 'warning');
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}

function AlertWarning(divID, errorMessage) {
    var html = "<div class='alert alert-warning'>";
    html += "<a href='#' class='close' data-dismiss='alert'>&times;</a>";
    if (LanguageId == 124) {
        html += "<strong>Warning!</strong> ";
    } else {
        html += "<strong>Lưu ý! </strong>";
    }
    html += errorMessage;
    html += "</div>";
    $("#" + divID).html(html);
}

function AlterAddSuccess() {
    if (LanguageId == 124) {
        showMessageWithTitle('Success!', 'Adding successful.', 'success');
    } else {
        showMessageWithTitle('Thành Công!', 'Thêm mới thành công.', 'success');
    }
    hideMessageWithTitle(2000);
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function AlterDeleteSuccess() {
    if (LanguageId == 124) {
        showMessageWithTitle('Success!', 'Deleting successful.', 'success');
    } else {
        showMessageWithTitle('Thành Công!', 'Xóa thành công.', 'success');
    }
    hideMessageWithTitle(2000);
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function AlterAddError() {
    if (LanguageId == 124) {
        showMessageWithTitle('Error!', 'Adding error.', 'error');
    } else {
        showMessageWithTitle('Lỗi!', 'Có lỗi trong quá trình thêm dữ liệu.', 'error');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}

function AlterUpdateSuccess() {
    if (LanguageId == 124) {
        showMessageWithTitle('Success!', 'Update successful.', 'success');
    } else {
        showMessageWithTitle('Thành Công!', 'Cập nhật thành công.', 'success');
    }
    hideMessageWithTitle(2000);
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function AlterUpdateSuccessWithErrorID(ErrorID) {
    var msg = '';
    var title = '';
    if (LanguageId == 124) {
        title = 'Success!';
        msg = 'Update successful.';
    } else {
        title = 'Thành Công!';
        msg = 'Cập nhật thành công.';
    }


    var css_class = 'alert alert-success';

    var message = '<div class="' + css_class + '"><a data-dismiss="alert" class="close" href="#">×</a>'
                                + '<strong>' + title + ' :</strong> ' + msg + '</div>';

    var container = document.getElementById(ErrorID);

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

        var container = document.getElementById(ErrorID);
       

        setTimeout(function () {
            container.className = "hide";
            container.style.display = "none";
            container.setAttribute("aria-hidden", "true");
        }, 5000);


        $('.modal-body').animate({ scrollTop: 0 }, 'fast');

}
function AlterUpdateError() {
    if (LanguageId == 124) {
        showMessageWithTitle('Error!', 'Update failed.', 'error');
    } else {
        showMessageWithTitle('Lỗi!', 'Cập nhật thất bại.', 'error');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function AlterUpdateWarning() {
    if (LanguageId == 124) {
        showMessageWithTitle('Warning!', 'Do not have the object are choose to update.', 'warning');
    } else {
        showMessageWithTitle('Lưu ý!', 'Không có đối tượng nào chọn để cập nhật.', 'warning');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function hideCustomAlter() {
    $("#").html('');
}

/*Start.User - Group Management*/
function AlterUpdateWarningChooseGroup() {
    if (LanguageId == 124) {
        showMessageWithTitle('Warning!', 'Please choose a group to update.', 'warning');
    } else {
        showMessageWithTitle('Lưu ý!', 'Vui lòng chọn nhóm để cập nhật.', 'warning');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
/*End.User - Group Management*/


/*Start.User - Group Management*/
function AlterUpdateWarningHolidaysYear() {
    if (LanguageId == 124) {
        showMessageWithTitle('Warning!', 'Please click reset button to setup holidays.', 'warning');
    } else {
        showMessageWithTitle('Lưu ý!', 'Click vào nút reset để thiết lập ngày lễ.', 'warning');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
/*End.User - Group Management*/

function AlertFromdateToDate()
{
    if (LanguageId == 124) {
        showMessageWithTitle('Warning!', 'To date must be greater than or equal from date.', 'warning');
    } else {
        showMessageWithTitle('Lưu ý!', 'Từ ngày phải nhỏ hơn hoặc bằng đến ngày.', 'warning');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function AlertChooseLeaveType() {
    if (LanguageId == 124) {
        showMessageWithTitle('Warning!', 'Please choose leave type.', 'warning');
    } else {
        showMessageWithTitle('Lưu ý!', 'Chọn loại nghỉ.', 'warning');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}
function loading() {
    $("body").addClass("loading");
}
function AlertChooseDate() {
    if (LanguageId == 124) {
        showMessageWithTitle('Warning!', 'Not permission to take leave taken.', 'warning');
    } else {
        showMessageWithTitle('Lưu ý!', 'Không được phép nghỉ phép.', 'warning');
    }
    $('html, body').animate({ scrollTop: 0 }, 'fast');
}