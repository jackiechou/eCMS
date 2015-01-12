//Add and update post action
$(document).on("click", "#btnAdd", function () {
    if (!$("#frmAdd").valid()) { // Not Valid
        $('.qtip').qtip("show");
        return false;
    } else {
        $("body").addClass("loading");
        if ($('#btnAdd').hasClass("AddModel")) {
            //Add new Item
            $.ajax({
                type: "POST",
                url: urlAddNew,
                data: $("#frmAdd").serialize(),
                success: function (data) {
                    if (data == 'success') {
                        $.ajax({
                            type: "GET",
                            url: urlReset,
                            success: function (data) {
                                $('#MainBody').html(data);
                                AlterAddSuccess();
                            }
                        });
                    } else if (data == 'timeout') {
                        window.location.href = '/';
                    } else {
                        $('#divEdit').html(data);
                        $('html, body').animate({ scrollTop: 80 }, 'slow');
                    }
                }
            });
        } else {
            $("body").addClass("loading");
            // Update Item
            $.ajax({
                type: "POST",
                url: urlUpdate,
                data: $("#frmAdd").serialize(),
                success: function (data) {
                    if (data == 'success') {
                        $.ajax({
                            type: "GET",
                            url: urlReset,
                            success: function (data) {
                                $('#MainBody').html(data);
                                AlterUpdateSuccess();
                            }
                        });
                    } else if (data == 'timeout') {
                        window.location.href = '/';
                    }
                    else {
                        $('#divEdit').html(data);
                        $('#btnAdd').removeClass("AddModel").addClass("EditModel");
                        $('html, body').animate({ scrollTop: 80 }, 'slow');
                    }
                }
            });
        }
    }
});

// Reset form
$(document).on("click", "#btnReset", function () {
    $("body").addClass("loading");
    $.ajax({
        type: "GET",
        url: urlReset,
        success: function (data) {
            $('#MainBody').html(data);

        }
    });
});

// Click edit (get)
$(document).on("click", ".editItem", function () {
    $("body").addClass("loading");
    var lsID = $(this).data('id');  // ID cua dong du lieu

    $.ajax({
        type: "GET",
        url: urlEdit + lsID,
        success: function (data) {
            //Create edit form
            $('#divEdit').html(data);
            //Add => Edit
            $('#btnAdd').removeClass("AddModel").addClass("EditModel");
        }
    })
    //Go to top
    $('html, body').animate({ scrollTop: 80 }, 'slow');
    return false;
});

// Click delete (get)
$(document).on("click", ".deleteItem", function (e) {
    var lsID = $(this).data('id');  // ID cua dong du lieu
    bootbox.confirm("@Html.Raw(Eagle.Resource.LanguageResource.DoYouWantToContinue)", function (result) {
        if (result) {
            $("body").addClass("loading");
            $.ajax({
                type: "POST",
                url: urlDelete + lsID,
                success: function (data) {
                    if (data == 'success') {
                        $.ajax({
                            type: "GET",
                            url: urlReset,
                            success: function (data) {
                                $('#MainBody').html(data);
                                AlterUpdateSuccess();
                                $('html, body').animate({ scrollTop: 80 }, 'slow');
                            }
                        });
                    } else {
                        $('#divEdit').html(data);
                        $('html, body').animate({ scrollTop: 80 }, 'slow');
                    }
                }
            });
            return false;
        }
    });


});
