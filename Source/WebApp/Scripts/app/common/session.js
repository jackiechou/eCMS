var totalTimeOut = 0;
var sessionTimeout =@Session.Timeout
sessionTimeout *= 60;
totalTimeOut = sessionTimeout;
function ShowTime() {
    $("#timeOut").text(sessionTimeout);
    sessionTimeout -= 10;
    if (sessionTimeout >= 10) {
        //TODO
    }
    else {
        alert("Session will be over!");
        //TODO
    }
    if (sessionTimeout <= 0) {
        clearInterval(interneal);
    }
}
//If use the ajax (e.g. Jquery ajax) to call the server anywhere, this function should be call
function ResetTimeOut() {
                     
    sessionTimeout = totalTimeOut;
}
var interneal = window.setInterval("ShowTime()", 10000);