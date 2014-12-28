/*--create site map--*/
$(document).ready(function () {
    $('#navigation *').show();
    $('.breadcrumbs').find('sitemap').remove();
    $('[isview]:first').addClass('sitemap site-finish');
    $('.breadcrumbs').append($('[isview]:first').clone());
    $('[isview]:first').parents('ul.ulparent').each(function () {
        var sitemap = $(this).prev('a').find('.pagename').parent();
        if (sitemap.length) {
            sitemap.addClass('sitemap');
            $('.breadcrumbs').find('.sitemap:first').before(sitemap.clone());
            $('.breadcrumbs').find('[isview]').removeAttr('isview');
        }
    });
    $('.breadcrumbs').find('.ui-icon').remove();
});

/*=========================== Define After Insert Html============= Start ==================*/
// Define Function 
// create a reference to the old `.html()` function
var htmlOriginal = $.fn.html;

// redefine the `.html()` function to accept a callback
$.fn.html = function (html, callback) {
    // run the old `.html()` function with the first parameter
    var ret = htmlOriginal.apply(this, arguments);
    // run the callback (if it is defined)
    if (typeof callback == "function") {
        callback();
    }
    // make sure chaining is not broken
    return ret;
}

/*=========================== Define After Insert Html============= End ===================*/

function formatEditing(form) {
	$('.tbtheme [row]').each(function (index) {
		var row = $(this).attr('row');
		var objtr = $('#tr_' + row);
		var firstobj = null;
		if (objtr.length > 0) {
			$(this).prev().html(objtr.find('td:first').html()).addClass('tdtitle CaptionTD');
			$(this).html(objtr.find('td:nth-child(2) *').clone()).addClass('tdvalue DataTD');
			$(this).find('[validate="text"]').each(function(){
				//$(this).after('<span class="spwarning spspec">*</span>');
			});
			objtr.remove();
		}
	});
	//$('.tbtheme tbody .clsremove').remove();
	//$('#TblGrid_Main_JQgrid tr').addClass('clsremove');
	//$('.tbtheme tbody').append($('#TblGrid_Main_JQgrid tr').clone());
	//form.find('table').html($('.tbtheme tbody').clone());
}
/*multiple select*/
function createMultipleSelect(obj) {
    obj.multiSelect({
        selectableHeader: "<input type='text' class='search-input' autocomplete='off' placeholder='Tìm kiếm cần chọn'><br /><button class='li-all' isadd='1' ref-id='" + obj.attr('id') + "' >Chọn tất cả</button>",
        selectionHeader: "<input type='text' class='search-input' autocomplete='off' placeholder='Tìm kiếm đã chọn'><br /><button isadd='0' class='li-all'>Chọn tất cả</button>",
        afterInit: function (ms) {
            var that = this,
                $selectableSearch = that.$selectableUl.parent().find('.search-input'),
                $selectionSearch = that.$selectionUl.parent().find('.search-input'),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
            .on('keydown', function (e) {
                if (e.which === 40) {
                    that.$selectableUl.focus();
                    return false;
                }
            });

            that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
            .on('keydown', function (e) {
                if (e.which == 40) {
                    that.$selectionUl.focus();
                    return false;
                }
            });
        },
        afterSelect: function () {
            this.qs1.cache();
            this.qs2.cache();
        },
        afterDeselect: function () {
            this.qs1.cache();
            this.qs2.cache();
        }
    });
    $(document).on('click', '.li-all', function () {
        $(this).next('ul').find('li:visible').click();
    });
}
/*end multiple*/
/*================================= button JQgrid ============================*/
function AddButtonRenderSection() {
    if ($('table.ui-pg-table.navtable:not([isCopy="1"])').size() > 0) {
        $('table.ui-pg-table.navtable:not([isCopy="1"])').each(function () {
            var html = $(this).find('tbody tr').html();
            $('.buttonaction tr').append(html);
            $(this).attr('isCopy', '1');
        });

        $('.buttonaction tr td').each(function () {
            var id = $(this).attr('id');
            $(this).removeAttr('id');
            $(this).attr('ref_id', id);
            TriggerClickNav(this);
        });
    }
}


function TriggerClickNav(obj) {
    $(obj).on('click', function () {
        var ref_id = $(this).attr('ref_id');
        $('#' + ref_id).trigger('click');
    });
}

/*==================== Can giua Popup JQgrid=================================*/
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

/*====================== Define Dialog=====================================*/
//-----------------------------------------------
var isUserAnswered = true;

// Show dialog with OK button: Information, warning, Error - START
function showDialog(msg, title) {

    // Reset answered flag
    isUserAnswered = false;

    // Define button OK
    var buttonOpts = {};
    buttonOpts['OK'] = function () {
        isUserAnswered = true;
        $(this).dialog("close");
    };

    var $dialog = $('<div title="' + title + '"></div>')
                    .html(msg)
                    .dialog({
                        modal: true,
                        autoOpen: false,
                        buttons: buttonOpts,
                        create: function (event, ui) {
                            $(event.target).parent().css({ 'position': 'fixed', 'z-index': '1051' });
                        },
                        close: function () { isUserAnswered = true; }
                    });

    // Show dialog
    $dialog.dialog('open');
}

function showDialogs(id, msg, titles) {

    // Reset answered flag
    isUserAnswered = false;

    // Define button OK
    var buttonOpts = {};
    buttonOpts['OK'] = function () {
        isUserAnswered = true;
        $(this).dialog("close");
    };

    $(id).html(msg)
                    .dialog({
                        modal: true,
                        title: titles,
                        autoOpen: false,
                        buttons: buttonOpts,
                        close: function () { isUserAnswered = true; }
                    });

    // Show dialog
    $(id).dialog('open');
}
// Show dialog with OK button: Information, warning, Error - END

// Show dialog with OK button: Information, warning, Error
function showConfirmDialog(msg, title, yesFunct, noFunct) {

    // Reset answered flag
    isUserAnswered = false;

    // Define button YES
    var buttonOpts = {};
    buttonOpts['Có'] = function () {
        isUserAnswered = true;
        if (null != yesFunct) {
            yesFunct();
        }
        $(this).dialog("close");
    };

    // Define button NO
    buttonOpts['Không'] = function () {
        isUserAnswered = true;
        if (null != noFunct) {
            noFunct();
        }
        $(this).dialog("close");
    };

    var $dialog = $('<div title="' + title + '"></div>')
                    .html(msg)
                    .dialog({
                        modal: true,
                        autoOpen: false,
                        buttons: buttonOpts,
                        create: function (event, ui) {
                            $(event.target).parent().css('position', 'fixed');
                        },
                        open: function () {
                            $('.ui-dialog-buttonpane')
                                    .find('button:contains("' + 'Không' + '")')
                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-cancel"></span>');
                            $('.ui-dialog-buttonpane')
                                    .find('button:contains("' + 'Có' + '")')
                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-disk"></span>');
                        },
                        close: function () {
                            CloseDialog();
                        }
                    });

    // Show dialog
    $dialog.dialog('open');
}

// Show dialog with OK button: Information, warning, Error
function showConfirmDialog_Close(msg, title, yesFunct, noFunct, closeFunct) {

    // Reset answered flag
    isUserAnswered = false;

    // Define button YES
    var buttonOpts = {};
    buttonOpts['Yes'] = function () {
        isUserAnswered = true;
        if (null != yesFunct) {
            yesFunct();
        }
        $(this).dialog("close");
    };

    // Define button NO
    buttonOpts[CtrNo()] = function () {
        isUserAnswered = true;
        if (null != noFunct) {
            noFunct();
        }
        $(this).dialog("close");
    };

    var $dialog = $('<div title="' + title + '"></div>')
                    .html(msg)
                    .dialog({
                        modal: true,
                        autoOpen: false,
                        buttons: buttonOpts,
                        create: function (event, ui) {
                            $(event.target).parent().css('position', 'fixed');
                        },
                        open: function () {
                            $('.ui-dialog-buttonpane')
                                    .find('button:contains("' + 'No' + '")')
                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-cancel"></span>');
                            $('.ui-dialog-buttonpane')
                                    .find('button:contains("' + 'Yes' + '")')
                                    .prepend('<span style="float:left; margin-top: 3px;" class="ui-icon ui-icon-disk"></span>');
                        },
                        close: function () {
                            if (null != closeFunct) {
                                closeFunct();
                            }
                        }
                    });

    // Show dialog
    $dialog.dialog('open');
}

function CloseDialog() {
}
// Waiting until user close Dialog to execute func
function ActionAfterDialog(func) {
    var interval = window.setInterval(function () {
        if (true == isUserAnswered) {
            func();
            window.clearInterval(interval);
        }
    }, 1000);
}

function showIFrame(url, title, width, height) {

    // Reset answered flag
    isUserAnswered = false;

    // Define button OK
    var buttonOpts = {};
    //buttonOpts['Quay Lại'] = function () {
    //    var objDialog = $(this);
    //    isUserAnswered = true;
    //    $('#FormReminder').contents().find("#RollBack").trigger('click');
    //    var isClickOk = setInterval(function () {
    //        if ($('#FormReminder').contents().find("#is_Finish").val() == 1) {
    //            objDialog.dialog("close");
    //            clearInterval(isClickOk);
    //            CallBackShowReminder();
    //        }
    //    }, 100);
    //};
    buttonOpts['Xác Nhận'] = function () {
        var objDialog = $(this);
        isUserAnswered = true;
        $('#FormReminder').contents().find("#SendRequest").trigger('click');
        var isClickOk = setInterval(function () {
            if ($('#FormReminder').contents().find("#is_Finish").val() == 1) {
                objDialog.dialog("close");
                clearInterval(isClickOk);
                CallBackShowReminder();
            }
        }, 100);
    };
    buttonOpts['Thoát'] = function () {
        isUserAnswered = true;
        $(this).dialog("close");
        $(this).remove();
    };
    if (width == undefined) {
        width = 800;
        height = 600;
    }
    var $dialog = $('<div title="' + title + '"></div>')
                    .html('<iframe id="FormReminder" src="' + url + '" style="width: 100%; height: 100%"></iframe>')
                    .dialog({
                        modal: true,
                        autoOpen: false,
                        width: width,
                        height: height,
                        buttons: buttonOpts,
                        create: function (event, ui) {
                            $(event.target).parent().css({ 'position': 'fixed', 'z-index': '1051' });
                        },
                        close: function () {
                            var objDialog = $(this);
                            $(this).remove();
                            isUserAnswered = true;
                        }
                    });

    // Show dialog
    $dialog.dialog('open');
}

function CallBackShowReminder() {

}
/*============================ Convert =================================*/
function ConvertDateTimeVnToSQL(str) {
    try {
        var split = str.split('/');
        var day = split[0];
        var month = split[1];
        var year = split[2];
        return year + '/' + month + '/' + day;
    } catch (e) {
        return null;
    }
}

/*============================= Action Input ========================================*/
// Kiem tra ki tu duoc nhap trong cac textbox co kieu du lieu so.
function ValidateKeyDownDataIsNumber(e) {
    if ((48 <= e.keyCode && e.keyCode <= 57)
        || (33 < e.keyCode && e.keyCode < 40)
        || e.keyCode == 0
        || e.keyCode == 8
        || e.keyCode == 9
        || e.keyCode == 32
        || e.keyCode == 45
        || e.keyCode == 46)
        return true;
    return false;
}

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

function unformatNumber(cellvalue, options) {
    try {
        return cellvalue.replace(/\$|\./g, '').replace(',', '.');
    } catch (e) {
        return 0;
    }
}

function addDotToNumber(nStr) {
    try {
        return parseFloat(nStr).formatMoney(2, ',', '.');
    } catch (e) {
        return 0.00;
    }
}


/*=============================== Convert ============================================*/
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

function ConvertToFloat(obj) {
    var iResult = 0;
    try {
        if (isNaN(parseFloat(obj)) == false) {
            iResult = parseFloat(obj);
        }
    } catch (e) {
        iResult = 0;
    }
    return iResult;
}
