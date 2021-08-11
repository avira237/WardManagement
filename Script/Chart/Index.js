var barchart;
var barchart2;
$(document).ready(function () {
    $('#loadindIcon').hide();
    drawChart(null, null);
    loadWardData();
    $("#WARDID").change(function () {
        var wardid = $("#WARDID").val();
        $.ajax({
            url: "/Booth/getmatdanmathak",
            method: "POST",
            data: {
                "ID": $("#WARDID").val()
            },
            success: function (data) {
                $("#MATDANID").empty().append('<option value="">--SELECT MATDHANMATHAK--</option>');
                $.each(data.data, function (index, row) {
                    var Tag = '<option value="' + data.data[index].PrimaryId + '">' + data.data[index].MatdanMathakName + '</option>';
                    $("#MATDANID").append(Tag);
                });
            },
        });
        $('#loadindIcon').show();
        $('#bar_chart').hide();
        $.ajax({
            type: "GET",
            url: "/Chart/chartward?wardid=" + wardid,
            success: function (data, status, xhr) {
                $('#loadindIcon').hide();
                $('#bar_chart').show();
                if (xhr.status === 200 && data.Status === 1) {
                    drawChart(data.data.Label, data.data.Voted, data.data.NotVoted, data.data.Total);
                }
            }
        });
    });
    $($("#MATDANID")).change(function () {
        $.ajax({
            url: "/Person/getbooth",
            method: "POST",
            data: {
                "ID": $("#MATDANID").val()
            },
            success: function (data) {
                $("#BOOTHID").empty().append('<option value="">--SELECT BOOTH--</option>');
                $.each(data.data, function (index, row) {
                    var Tag = '<option value="' + data.data[index].ID + '">' + data.data[index].BOOTH1 + '</option>';
                    $("#BOOTHID").append(Tag);
                });
            }
        });
        $('#loadindIcon').show();
        $('#bar_chart').hide();
        var matdanid = $("#MATDANID").val();
        $.ajax({
            type: "GET",
            url: "/Chart/chartMatdanMathak?matdanid=" + matdanid,
            data: "{}",
            success: function (data, status, xhr) {
                if (xhr.status === 200 && data.Status === 1) {
                    $('#loadindIcon').hide();
                    $('#bar_chart').show();
                    drawChart(data.data.Label, data.data.Voted, data.data.NotVoted, data.data.Total);
                }
            }
        });
    });
    $("#BOOTHID").change(function () {
        var boothid = $("#BOOTHID").val();
        $.ajax({
            url: "/Person/getSociety",
            method: "POST",
            data: {
                "ID": $("#BOOTHID").val()
            },
            success: function (data) {
                $("#SOCIETYID").empty().append('<option value="">--SELECT SOCIETY--</option>');
                $.each(data.data, function (index, row) {
                    var Tag = '<option value="' + data.data[index].ID + '">' + data.data[index].SOCIETYNAME + '</option>';
                    $("#SOCIETYID").append(Tag);
                });
            }
        });
        $('#loadindIcon').show();
        $('#bar_chart').hide();
        $.ajax({
            type: "GET",
            url: "/Chart/chartBooth?boothid=" + boothid,
            data: "{}",
            success: function (data, status, xhr) {
                if (xhr.status === 200 && data.Status === 1) {
                    $('#loadindIcon').hide();
                    $('#bar_chart').show();
                    drawChart(data.data.Label, data.data.Voted, data.data.NotVoted, data.data.Total);
                }
            }
        });
    });
    $("#SOCIETYID").change(function () {
        $('#loadindIcon').show();
        $('#bar_chart').hide();
        var societyid = $("#SOCIETYID").val();
        $.ajax({
            type: "GET",
            url: "/Chart/chartsociety?societyid=" + societyid,
            data: "{}",
            success: function (data, status, xhr) {
                if (xhr.status === 200 && data.Status === 1) {
                    $('#loadindIcon').hide();
                    $('#bar_chart').show();
                    drawChart(data.data.Label, data.data.Voted, data.data.NotVoted, data.data.Total);
                }
            }
        });
    });
});
function drawChart(Society, voted, notvoted, total) {
    if (barchart !== undefined) {
        barchart.destroy();
    }
    barchart = new Chart($("#bar_chart"), {
        type: "bar",
        data: {
            labels: Society,
            datasets: [
                {
                    label: "Voted",
                    data: voted,
                    backgroundColor: "#34A853",
                    barThickness: 30,
                    borderColor: ["black", "black", "black", "black"],
                },
                {
                    label: "Not Voted",
                    data: notvoted,
                    backgroundColor: "#fbbc05",
                    barThickness: 30,
                    borderColor: ["black", "black", "black", "black"],
                },
                {
                    label: "Total",
                    data: total,
                    backgroundColor: "#4285f4",
                    barThickness: 30,
                    borderColor: ["black", "black", "black", "black"],
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            scales: {
                xAxes: [
                    { ticks: { beginAtZero: true } },
                ]
            },
            scales: { yAxes: [{ ticks: { beginAtZero: true } }] },
            legend: { display: false },
        }
    });
}
function loadWardData() {
    $('#loadindIcon').show();
    $('#bar_chart').hide();
    $.ajax({
        type: "GET",
        url: "/Booth/getwarddata",
        data: "{}",
        success: function (data) {
            $('#loadindIcon').hide();
            $('#bar_chart').show();
            var s = '<option value="">--SELECT WARD--</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].WARDNAME + '</option>';
            }
            $("#WARDID").html(s);
        }
    });
}
