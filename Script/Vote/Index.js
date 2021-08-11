var defaultJsonObj = {
    'SEARCH': '',
    'WARDID': '',
    'MATDANID': '',
    'BOOTHNO': '',
    'SOCIETYID': '',
    'FileOperation': 'view',
    'Resource': 'VOTEMASTER'
};
dataLoad(defaultJsonObj);
function dataLoad(passedData) {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $("#Votetbl").DataTable({
        destroy: true,
        "searching": false,
        "pagingType": "simple_numbers_no_ellipses",
        "serverside": "true",
        "order": [1, "asc"],
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: "/Vote/VoteDT",
            method: "post",
            data: passedData,
            error: function (xhr, error, code) {
                alert("access denied");
            }
        },
        "columnDefs": [{ 'orderable': false, 'targets': 0 }],
        "columns": [
            {
                "data": null,
                "render": function (data, row, type) {
                    if (data.IsVoted == false) {
                        return '<button  onclick="VoteChange(' + data.PrimaryId + ')">VOTE</button> ';
                    }
                    else {
                        return "VOTED";
                    }
                }
            },
            {
                "data": "Name",
                "className": "text-left"
            },
            { "data": "UniqueId" },
            {
                "data": "WardName",
                "className": "text-left"
            },
            {
                "data": "MatdanName",
                "className": "text-left"
            },
            {
                "mData": null,
                "mRender": function (data, type, full) {
                    if (full['Booth2'] == null || full['Booth3'] == null || full['Booth4'] == null || full['Booth5'] == null) {
                        return full['Booth1'];
                    }
                    else {
                        return full['Booth1'] + '/' + full['Booth2'] + '/' + full['Booth3'] + '/' + full['Booth4'] + '/' + full['Booth5'];
                    }
                },
                "className": "text-left"
            },
            {
                "mData": null,
                "className": "text-left",
                "mRender": function (data, type, full) {
                    if (full['LandMark'] == null) {
                        return full['SocietyName'];
                    }
                    else {
                        return full['SocietyName'] + '/' + full['LandMark'];
                    }
                },
                "className": "text-left"
            },
            {
                "data": "Address",
                "className": "text-left"
            },
        ],
    });
}
var userData;
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Vote/getWard",
        data: "{}",
        success: function (data) {
            var s = '<option value="">---SELECT WARD---</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].WARDNAME + '</option>';
            }
            $("#WARDID").html(s);
        }
    });

    //getmatdanmathak
    $.ajax({
        type: "GET",
        url: "/Vote/getMatdanMathak",
        data: "{}",
        success: function (data) {
            var s = '<option value="">---SELECT MATDANMATHAK---</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].NAME + '</option>';
            }
            $("#MATDANID").html(s);
        }
    });

    //getsociety
    $.ajax({
        type: "GET",
        url: "/Vote/getSociety",
        data: "{}",
        success: function (data) {
            var s = '<option value="">---SELECT SOCIETY---</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].SOCIETYNAME + '</option>';
            }
            $("#SOCIETYID").html(s);
        }
    });

    $('#btnsearch').click(function () {
        var defaultJsonObj = {
            'SEARCH': $('#SEARCH').val(),
            'WARDID': $('#WARDID').val(),
            'MATDANID': $('#MATDANID').val(),
            'BOOTHNO': $('#BOOTHNO').val(),
            'SOCIETYID': $('#SOCIETYID').val(),
            'FileOperation': 'update',
            'Resource': 'VOTEMASTER'
        };
        dataLoad(defaultJsonObj);
        return false;
    });
});

function VoteChange(primaryId) {
    $.ajax({
        url: "/Vote/VoteChange?primaryId=" + primaryId + "&FileOperation=update&Resource=VOTEMASTER",
        type: "POST",
        success: function (data, status, xhr) {
            var Status = data.Status;
            var msg = data.msg;
            if (xhr.status === 200 && Status === 1) {
                dataLoad(defaultJsonObj);
            }
        },
        complete: function (xhr, txterrorcode) {
            if (xhr.status === 302) {
                alert("Access denied");
            }
        }
    });
    return false;
}