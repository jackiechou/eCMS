
function BindChart(chartPlaceholder, chartType, actionUrl, chartLabel, xAxisLabel, yAxisLabel) {
    $.ajax({
        url: actionUrl,
        type: "GET",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: function () { console.log("ajax error"); },
        success: function (response) {  
            switch (chartType) {
                case '1':
                    DrawPieChart(chartPlaceholder, response);                   
                    break;
                case '2':
                    DrawBarChart(chartPlaceholder, response, chartLabel, xAxisLabel, yAxisLabel);
                    break;
                case '3':
                    DrawLineChart(chartPlaceholder, response);
                    break;
                default:
                    DrawPieChart(chartPlaceholder, response);
                    break;
            }
        }
    });
}


function DrawBarChart(chartPlaceholder, data, chartLabel, xAxisLabel, yAxisLabel) {
    var pieChart = $("#" + chartPlaceholder);
    var myData = [];
    var ticks = [];
    $.each(data, function (index, item) {      
        myData[index] = [index + 1, item.data];
        ticks[index] = [index + 1, item.label];
    });

    var dataset = [
      { label: chartLabel, data: myData, color: "#5482FF" }
    ];

    var options = {
        series: {
            bars: {
                show: true
            }
        },
        bars: {
            align: "center",
            barWidth: 0.5,
            order: 1
        },
        xaxis: {
            axisLabel: xAxisLabel,
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 12,
            axisLabelFontFamily: 'Verdana, Arial',
            axisLabelPadding: 10,
            ticks: ticks

        },
        yaxis: {
            axisLabel: yAxisLabel,
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 12,
            axisLabelFontFamily: 'Verdana, Arial',
            axisLabelPadding: 3,
            tickFormatter: function (v, axis) {
                return v;
            }
        },
        legend: {
            noColumns: 0,
            labelBoxBorderColor: "#000000",
            position: "nw"
        },
        grid: {
            hoverable: true,
            clickable: true,
            autoHighlight: true,
            borderWidth: 2,
            backgroundColor: { colors: ["#ffffff", "#EDF5FF"] }
        },
        valueLabels: {
            show: false
        },
        tooltip: true,
        tooltipOpts: {          
            content: "<strong>%s</strong><br>%x : <strong>%y</strong>",
            shifts: {
                x: -60,
                y: 25
            }
        }
    };

   


    $.plot(pieChart, dataset, options);
    pieChart.bind("plotclick", function (event, pos, item) {
        if (item) {           
            pieChart.highlight(item.series, item.datapoint);
        }
    });
}

function DrawLineChart(chartPlaceholder, data, chartLabel, xAxisLabel, yAxisLabel) {
    var chartHolder = $("#" + chartPlaceholder);
    ////Rome, Italy
    //var d1 = [[1262304000000, 12], [1264982400000, 13], [1267401600000, 15], [1270080000000, 18], [1272672000000, 23], [1275350400000, 27], [1277942400000, 30], [1280620800000, 30], [1283299200000, 27], [1285891200000, 22], [1288569600000, 16], [1291161600000, 13]];
    //// Paris, France
    //var d2 = [[1262304000000, 6], [1264982400000, 7], [1267401600000, 12], [1270080000000, 16], [1272672000000, 20], [1275350400000, 23], [1277942400000, 25], [1280620800000, 24], [1283299200000, 21], [1285891200000, 16], [1288569600000, 10], [1291161600000, 7]];
    //// Madrid, Spain
    //var d3 = [[1262304000000, 11], [1264982400000, 13], [1267401600000, 16], [1270080000000, 18], [1272672000000, 22], [1275350400000, 28], [1277942400000, 33], [1280620800000, 32], [1283299200000, 28], [1285891200000, 21], [1288569600000, 15], [1291161600000, 11]];
    //// London, UK
    //var d4 = [[1262304000000, 7], [1264982400000, 7], [1267401600000, 10], [1270080000000, 13], [1272672000000, 16], [1275350400000, 20], [1277942400000, 22], [1280620800000, 21], [1283299200000, 19], [1285891200000, 15], [1288569600000, 10], [1291161600000, 8]];

  
   
    //var symbols = ["circle", "diamond", "square", "triangle", "cross"];
    ////var colors = ["#058DC7", "#AA4643", "#50B432", "#ED561B", "#ED561B"]

    //var myData = [];
    //$.each(data, function (index, item) {
    //    alert(item.data);

    //    myData[index] = {
    //        label: item.label,
    //        data: item.data,
    //        points: item.points,
    //        color:item.color
    //    }
    //});
   
    var myData = [
        { label: "2010", data: [[1, 48], [2, 49], [5, 130], [6, 134], [7, 138], [8, 129], [9, 132], [10, 60], [11, 61], [12, 62]], points: { symbol: "circle", fillColor: "#487C84" }, color: '#487C84' },
        { label: "2011", data: [[1, 36], [2, 37], [3, 264], [4, 122], [5, 132], [6, 134], [7, 128], [8, 169], [9, 112], [10, 60], [11, 61], [12, 35]], points: { symbol: "diamond", fillColor: "#058DC7" }, color: '#058DC7' },
        { label: "2012", data: [[1, 24], [2, 25], [3, 262], [4, 121], [5, 134], [6, 131], [7, 118], [8, 119], [9, 132], [10, 61], [11, 62], [12, 35]], points: { symbol: "square", fillColor: "#50B432" }, color: '#50B432' },
        { label: "2013", data: [[1, 48], [2, 49], [3, 261], [4, 127], [5, 136], [6, 137], [7, 108], [8, 13], [9, 16], [10, 156], [11, 160], [12, 129]], points: { symbol: "triangle", fillColor: "#ED561B" }, color: '#ED561B' },
        { label: "2014", data: [[1,6],[2,7],[3,96],[4,126],[5,130],[6,134],[7,138],[8,129],[9,132],[10,156],[11,160],[12,129]], points: { symbol: "square", fillColor: "#9F7AB2" }, color: '#9F7AB2' }
    ];


   
    $.plot(chartHolder, myData, {
        xaxis: {
            //min: (new Date(2009, 11, 18)).getTime(),
            //max: (new Date(2010, 11, 15)).getTime(),
            mode: "time",
            tickSize: [1, "month"],
            monthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            tickLength: 0,
            axisLabel: 'Month',
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 12,
            axisLabelFontFamily: 'Verdana, Arial, Helvetica, Tahoma, sans-serif',
            axisLabelPadding: 5
        },
        yaxis: {
            axisLabel: yAxisLabel,
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 12,
            axisLabelFontFamily: 'Verdana, Arial, Helvetica, Tahoma, sans-serif',
            axisLabelPadding: 5
        },
        series: {
            lines: { show: true },
            points: {
                radius: 3,
                show: true,
                fill: true
            },
        },
        grid: {
            hoverable: true,
            borderWidth: 1
        },
        legend: {
            labelBoxBorderColor: "none",
            position: "right"
        }
    });
}

function DrawPieChart(chartPlaceholder, data) {
    var pieChart = $("#" + chartPlaceholder);    
   // pieChart.unbind();
    var options = {
        series: {
            pie: {
                show: true,
                label: {
                    show: true,
                    radius: 0.8,
                    formatter: function (label, series) {
                        //return '<div style="border:1px solid grey;font-size:8pt;text-align:center;padding:5px;color:white;">' +
                        //label + ' : ' +
                        //Math.round(series.percent) +
                        //'%</div>';

                        return '<div style="border:1px solid grey;font-size:8pt;text-align:center;padding:5px;color:white;">' +                    
                      series.percent +
                      '%</div>';
                    },
                    background: {
                        opacity: 0.5,
                        color: '#000'
                    }
                },
                radius: 0.8,
            }

        },
        legend: {
            show: true
        }
    };    
    $.plot(pieChart, data, options);   
}

function gd(year, month, day) {
    return new Date(year, month, day).getTime();
}

var previousPoint = null, previousLabel = null;

$.fn.UseTooltip = function () {
    $(this).bind("plothover", function (event, pos, item) {
        if (item) {
            if ((previousLabel != item.series.label) || (previousPoint != item.dataIndex)) {
                previousPoint = item.dataIndex;
                previousLabel = item.series.label;
                $("#tooltip").remove();

                var x = item.datapoint[0];
                var y = item.datapoint[1];
                var color = item.series.color;

                var html = '';
                if (item.series.xaxis.ticks[x] != undefined)
                    html = "<strong>" + item.series.label + "</strong><br>" + item.series.xaxis.ticks[x].label + " : <strong>" + y + "</strong>";
               
                    
                showTooltip(item.pageX,item.pageY,color,html);
            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
};

function showTooltip(x, y, color, contents) {
    $('<div id="tooltip">' + contents + '</div>').css({
        position: 'absolute',
        display: 'none',
        top: y - 40,
        left: x - 120,
        border: '2px solid ' + color,
        padding: '3px',
        'font-size': '9px',
        'border-radius': '5px',
        'background-color': '#fff',
        'font-family': 'Verdana, Arial, Helvetica, Tahoma, sans-serif',
        opacity: 0.9
    }).appendTo("body").fadeIn(200);
}


//function showTooltip(x, y, contents, z) {
//    $('<div id="flot-tooltip">' + contents + '</div>').css({
//        top: y - 30,
//        left: x - 135,
//        'border-color': z,
//    }).appendTo("body").fadeIn(200);
//}

//function getMonthName(numericMonth) {
//    var monthArray = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
//    var alphaMonth = monthArray[numericMonth];

//    return alphaMonth;
//}

//function convertToDate(timestamp) {
//    var newDate = new Date(timestamp);
//    var dateString = newDate.getMonth();
//    var monthName = getMonthName(dateString);

//    return monthName;
//}

//var previousPoint = null;

//$("#placeholder").bind("plothover", function (event, pos, item) {
//    if (item) {
//        if ((previousPoint != item.dataIndex) || (previousLabel != item.series.label)) {
//            previousPoint = item.dataIndex;
//            previousLabel = item.series.label;

//            $("#flot-tooltip").remove();

//            var x = convertToDate(item.datapoint[0]),
//            y = item.datapoint[1];
//            z = item.series.color;

//            showTooltip(item.pageX, item.pageY,
//                "<b>" + item.series.label + "</b><br /> " + x + " = " + y + "mm",
//                z);
//        }
//    } else {
//        $("#flot-tooltip").remove();
//        previousPoint = null;
//    }
//});
