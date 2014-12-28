
/*
 * + Function name: ValidateBeforeClickButton()
 *
 * + Input: 
 *
 * + Output: 
 *
 * + Description: validate before input data 
 *
 */
function ValidateBeforeClickButton()
{
	$('input[type="submit"]').each(function(){
		$(this).click(function(){
			if($(this).attr('validate')!='no'){
				if(!ValidateClick()){
					return false;
				}else{
					return true;
				}	
			}
		});
	}); 
}



/*
 * + Function name: ValidateInputChange()
 *
 * + Input: 
 *
 * + Output: 
 *
 * + Description: validate change input to other control 
 *
 */
function executeFunctionByName(functionName, context /*, args */) {
    var args = Array.prototype.slice.call(arguments, 2);
    var namespaces = functionName.split(".");
    var func = namespaces.pop();
    for (var i = 0; i < namespaces.length; i++) {
        context = context[namespaces[i]];
    }
    return context[func].apply(context, args);
}
function ValidateInputChange()
{
	$('input[type="text"]').change(function(){
		if($(this).is('[prefix]')){
			validateTextBox(false);
			validateNumber(false);
			validateDateDDMMYY(false); 
			validateSpecialCharacter(false); 
			validateEmail(false); 
			validatePhone(false);
			validateRequireHours($(this),false);
		}
	});	
	$('textarea').change(function(){
		validateTextArea(false);
	});
	$('select').change(function(){
		validateRequireCombobox(false);
	});
}
$(document).on('click', '[btvalidate]', function () {
   return  onValidate($(this));
});
function onValidate(obj) {
    var attr = obj.attr('btvalidate');
    var objparent = attr == "" || attr == undefined ? $('html') : $('.' + attr);
    //objparent = objparent == null ? $('html') : objparent;
    var ischeck = true;
    objparent.find('[validate]').each(function () {
        if (!validateObject($(this))) {
            ischeck = false;
        }
    });
    return ischeck;
}
function jqgridvalidate(obj) {
    var objparent = obj;
    //objparent = objparent == null ? $('html') : objparent;
    var ischeck = true;
    objparent.find('[validate]').each(function () {
        if (!validateObject($(this))) {
            ischeck = false;
        }
    });
    return ischeck;
}
$(document).on('change', '[validate]', function () {
    return validateObject($(this));
});

/**
Format validate function
* obj : control validate
* type : warning, notification, error
* ischeck : true success false error
* mess  : message want to show
**/
function validateFormat(parr){
	if(parr!=null){
		var obj= parr['obj'];
		var type = parr['type'];
		var ischeck = parr['ischeck'];
		var mess = '<li class="validate-message">' + parr['mess'] + '</li>';
		//var mes=parr['mes'];
		var html='error';//default validate error
		if(type=="w"){
			html='notice';//warning
		}if(type=="i"){
			html='inf';//information
		}if(type=="s"){
			html='success';//success
		}
		if (ischeck) {
		    obj.next('.validate-error').html('');
		    obj.removeClass('ui-state-error');
		} else {
		    if (obj.next().hasClass('validate-error')) {
		        obj.next().html('');
		        obj.next().html(mess);
		    } else {
		        html = '<ul class="validate-error">' + mess + '</ul>';
		        obj.after(html);
		    }
		    obj.addClass('ui-state-error');
		}
	}
}
function convertmess(mes) {
    try {

        if (mes != null && mes.indexOf('{')>-1) {
            return JSON.parse(mes);
        } else {
            return mes;
        }
    } catch (e) {
        console.log(e);
    }
    return null;
}
function validateObject(obj) {
    var ischeck = true;
    var mes = "";
    var jsonCustomReturn;
    var mescus = "";
    var ismes = "";
	if(obj.length>0){
		var type=obj.attr('validate-type');
		var validate = obj.attr('validate');
		var val = $.trim(obj.val());
		var func = obj.attr('validate-function');
		var arr = validate.split(',');
		mescus = convertmess(obj.attr('mes'));
		for (var i = 0; i < arr.length ; i++) {
		    validate = arr[i];
		    if (validate == 'text') {
		        ischeck = $.trim(val) != '';
		        mes = "Không để trống";
		    } else if (validate == 'number') {
		        ischeck = isNumberNew(val);
		        mes = "Phải nhập số";
		    } else if (validate == 'date') {
		        ischeck = isFormatDate_DDMMYY(val);
		        mes = "Định dạng ngày không chính xác";
		    } else if (validate == 'hours') {
		        ischeck = isHoursFormat(val);
		        mes = "Định dạng thời gian không chính xác";
		    } else if (validate == 'phone') {
		        ischeck = isPhone(val);
		        mes = "Định dạng số điện thoại không chính xác";
		    } else if (validate == 'email') {
		        ischeck = isEmail(val);
		        mes = "Định dạng email không chính xác";
		    } else if (validate == 'special') {
		        ischeck = isSpecialCharacter(val);
		        mes = "Không nhập ký tự đặc biệt";
		    } else if (validate == 'combobox') {
		        ischeck = isnotSelect(val);
		        mes = "Phải lựa chọn";
		    } else if (validate == 'file') {
		        ischeck = isFilename(val);
		        mes = "Định dạng tập tin không chính xác";
		    } else if (validate == 'compareint') {
		        var key = obj.attr('objstart') != null ? obj.attr('objstart') : obj.attr('objend');
		        var start = $('[objstart="' + key + '"]');
		        var end = $('[objend="' + key + '"]');
		        var notmuststart = start.attr('notmust');
		        var notmustend = end.attr('notmust');

		        var startval = !IsNull(notmuststart) && IsNull(start.autoNumeric('get')) ? end.autoNumeric('get') : start.autoNumeric('get');
		        var endval = !IsNull(notmustend) && IsNull(end.autoNumeric('get')) ? start.autoNumeric('get') : end.autoNumeric('get');
		        ischeck = isCompareInt(startval, endval);
		        if (ischeck) {
		            start.next('.validate-error').html('');
		            start.removeClass('ui-state-error');
		            end.next('.validate-error').html('');
		            end.removeClass('ui-state-error');
		        }
		        mes = "khong chinh xac";
		    } else if (validate == 'comparedate') {
		        var key = obj.attr('objstart') != null ? obj.attr('objstart') : obj.attr('objend');
		        var start = $('[objstart="' + key + '"]');
		        var end = $('[objend="' + key + '"]');
		        var notmustend = end.attr('notmust');
		        var notmuststart = start.attr('notmust');

		        var startval = !IsNull(notmuststart) && IsNull(start.val()) ? '01/01/1900' : start.val();
		        var endval = !IsNull(notmustend) && IsNull(end.val()) ? '31/12/9999' : end.val();
		        ischeck = isCompareDate(startval, endval);

		        if (ischeck) {
		            start.next('.validate-error').html('');
		            start.removeClass('ui-state-error');
		            end.next('.validate-error').html('');
		            end.removeClass('ui-state-error');
		        }
		        mes = "khong chinh xac";
		    } else if (validate == 'custom') {
		        try {
		            var html = '';
		            jsonCustomReturn = JSON.parse(window[func]());
		            if (jsonCustomReturn != null) {
		                mes = jsonCustomReturn.mes;
		                ischeck = jsonCustomReturn.ischeck;
		            }
		            if (obj.context.type.indexOf('select') != -1)
		            {
		                if ($('#Error' + $(obj).attr('id')).length !=0) {
		                    $('#Error' + $(obj).attr('id')).remove();
		                }
		                if (!ischeck) {
		                    ismes = 'ismes';
		                    var mess = '<li class="validate-message">' + mes + '</li>';
		                    html = '<ul id="Error' + $(obj).attr('id') + '" class="validate-error">' + mess + '</ul>';
		                    obj.parent('td').find('.custom-combobox').after(html);		                    
		                }
		            }
		        } catch (e) {

		        }
		    } else if (validate == 'validateage') {
		        ischeck = isValidAge(val);
		        mes = "Độ tuổi >=18";
		    }

            //end
		    if (!ischeck) {
		        break;
		    }
		}
		//show message
	}

	if (ismes == "")
	{
	    if (mescus != null) {
	        mescus = mescus[validate] ? mescus[validate] : '';
	    }
	    mes = mescus != null && mescus != '' ? mescus : mes;
	    ismes = "";
	}

	validateFormat({
	    'obj': obj,
	    'type': '',
	    mess: mes,
	    ischeck: ischeck
	});
	return ischeck;
}
/*
 * + Function name: ValidateClick()
 *
 * + Input: 
 *
 * + Output: 
 *
 * + Description: validate when user click button submit 
 *
 */

function ValidateClick()
{
	if(!validateTextBox(true)){
		return false;
	}
	if(!validateNumber(true)){
		return false;
	}
	if(!validateDateDDMMYY(true)){
		return false;
	}
	if(!validateSpecialCharacter(true)){
		return false;
	}
	if(!validateEmail(true)){
		return false;
	}
	if(!validatePhone(true)){
		return false;
	}
	if(!validateRequireCombobox(true)){
		return false;
	}
	if(!validateTextArea(true)){
		return false;
	}
	if(!validateRequireHours(null,true)){
		return false;
	}
 	return true;
}

/*
 * + Function name: isFormatDate_YYMMDD(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check value input have to format by yy/mm/dd
 *
 */
function isFormatDate_YYMMDD(value)
{	var result = false;
	var arrDate = value.split("/");
	if((arrDate[0].length == 4) && (arrDate[1].length == 2 ) && (arrDate[2].length == 2) 
		&& (arrDate[0]>0)
		&& (arrDate[1]>0 && arrDate[1]<=12)
		&& (arrDate[2]>0 && arrDate[2]<=31)
	){
		result = true;
	}
	return result;
}
/*
 * + Function name: CompareDateTime(DateValue1, DateValue2)
 *
 * + Input: DateValue1, DateValue2
 *
 * + Output: true or false
 *
 * + Description: check datevalue1>=datevalue2
 *
 */
function convertFormatDT(date){
	var arrDate = date.split("/");
	var day = arrDate[0].length<2?'0'+arrDate[0]:arrDate[0];
	var mon = arrDate[1].length<2?'0'+arrDate[1]:arrDate[1];
	var year= arrDate[2];
	return year+'-'+mon+'-'+day;
} 
function CompareDateTime(DateValue1, DateValue2)
{
	var DaysDiff;
	Date1 = new Date(convertFormatDT(DateValue1));
	Date2 = new Date(convertFormatDT(DateValue2));
	DaysDiff = Math.floor((Date1.getTime() - Date2.getTime())/(1000*60*60*24));
	if(DaysDiff >= 0)
		return true;
	else
		return false;
}


/*
 * + Function name: isHoursFormat(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check format hours for hh:mm:ss
 *
 */
function isHoursFormat(value)
{	
	 var isValid = /^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$/.test(value);
	return isValid;
}
/*
 * + Function name: isHoursFormat(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check format hours for hh:mm:ss
 *
 */
function isFilename(value)
{	
	var isValid = /^[^\\/:\~`*!@#$%^&*()\-+=\[\]{};:,\?"<>\|]+$/.test(value);
	if(isChrome){
		isValid =  /[-[\]\-{}()*+?\~\`\<\>\"\_\@\,^$|#\s]/.test(value);
		isValid=!isValid;
	}
	 
	return isValid;
}


function isValidAge(value) {
    var isValid = (value >= 18)?true:false;
    return isValid;
}

/**/
function isnotSelect(value,compare) {
    return value !="" && value!='-1' && value != '0' ;
}
/*
 * + Function name: isFormatDate_DDMMYY(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check value input have to format by dd/mm/yy
 *
 */
function isFormatDate_DDMMYY(value)
{
    if (value == '') { return true; }
	var result = false;
	var arrDate = value.split("/");
	if((arrDate[0].length == 2) && (arrDate[1].length == 2 ) && (arrDate[2].length == 4) 
		&& (arrDate[0]>0 && arrDate[0]<=31)
		&& (arrDate[1]>0 && arrDate[1]<=12)
		&& (arrDate[2]>0)
	){
		result = true;
	}
	return result;
}

/*
 * + Function name: isNumber(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check value input have to number ?
 *
 */
function isNumber(value)
{
	value = ReplaceCharacter(value, ',', '')
	return $.isNumeric(value);
}


function isNumberNew(value){
	try{
		if(value!=null && value!=undefined){
			value = ReplaceCharacter(value, ',', '');
			value = value.toString().replace(/\./g, '');
		}else{
			return false;
		}	
		return /^-{0,1}\d*\.{0,1}\d+$/.test(value);
  }catch(err){
 		 return false;
  }
	
}
/*
 * + Function name: isSpecialCharacter(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check string has contianed special character, only input a->z, A->Z, 0->9 and '_'
 *
 */
function isSpecialCharacter(value)
{
    var regex = /^[a-zA-Z0-9_]*$/gi;
	if (value == '') { return true; }
	if(regex.test(value) == true) {
		return true;
	}else{
		return false;
	}
}


/*
 * + Function name: isEmail(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check format email
 *
 */

function isEmail(value) {
    if (value == '') { return true; }
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (value.indexOf(" ") > -1)
        return false;
    return reg.test(value);
}

/*
 * + Function name: isCompareInt(startobj,endobj)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check Compare Int
 *
 */
function isCompareInt(startobj, endobj) {
    if (startobj == "" && endobj == "")
    { return true; }
    startobj = tryParseFloat(startobj);
    endobj = tryParseFloat(endobj);
    return endobj >= startobj;    
}

/*
 * + Function name: isCompareDate(StartDate, EndDate)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check Compare Date
 *
 */
function isCompareDate(StartDate, EndDate) {
    try {
        if (StartDate == "" && EndDate == "")
        { return true; }
        return ParseDateTime(StartDate) <= ParseDateTime(EndDate);
        
    } catch (e) {
        return false;
    }
}


/*
 * + Function name: isPhone(value)
 *
 * + Input: Value
 *
 * + Output: true or false
 *
 * + Description: check format phone, only input number and character '-'
 *
 */

function isPhone(value) {
    var reg = /^[0-9\-]+$/;

    if (value.trim() == "") {
        return true;
    }

    if (value.match(reg)) {
        return true;
    }
    else {
        return false;
    }              
}


function ReplaceCharacter(value, searchvalue, newvalue) {
	if(value!=undefined && value!='' && value.toString().indexOf(searchvalue.toString()) >-1)
	{
		value = value.replace(new RegExp(searchvalue,'g'), newvalue);
	}
	return value;
}

	
 function getUrlVars() {
	var vars = {};
	var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function(m,key,value) {
		vars[key] = value;
	});
	return vars;
 }
 function tryParseInt(value) {
     if (isNumberNew(value)) {
         return parseInt(value);
     } else {
         return 0;
     }
 }
 function tryParseFloat(value) {
     if (isNumberNew(value)) {
         return parseFloat(value);
     } else {
         return 0;
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

 function IsNull(value) {
        return (null === value || "undefined" == typeof value || "" == $.trim(value) );
 }