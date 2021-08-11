$(document).ready(function () {
    $("#btnreset").click(function () {
        $("#btnsave").show();
        $("#btnupdate").hide();
        $("#btnload").hide();
        $("#MATDANID").empty();
    });
    $("#BoothNo").inputmask(
        {   
            regex: '^[0-9]{3}'
        });
    $("#BoothNo2").inputmask(
        {
            regex: '^[0-9]{3}'
        });
    $("#BoothNo3").inputmask(
        {
            regex: '^[0-9]{3}'
        });
    $("#BoothNo4").inputmask(
        {
            regex: '^[0-9]{3}'
        });
    $("#BoothNo5").inputmask(
        {
            regex: '^[0-9]{3}'
        });
    $("#btnsave").show();
    $("#btnupdate").hide();
    $("#btnload").hide();
    dataLoad();
    $('#btnsave').click(function () {
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnsave").hide();
        if (valid) {

            var frmData = $('#frmBooth').serialize() + "&FileOperation=add&Resource=BOOTHMASTER";
            $.ajax({
                url: "/Booth/SaveBooth",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmBooth').trigger('reset');
                        $("#MATDANID").empty();
                        dataLoad();
                        $("#btnsave").show();
                        $("#btnload").hide();

                    }
                    else {
                        $("#btnsave").show();
                        $("#btnload").hide();
                    }
                    alert(msg);
                },
                complete: function (xhr, txterrorcode) {
                    if (xhr.status === 302) {
                        alert("Access denied");
                        $("#btnsave").show();
                        $("#btnload").hide();
                        $("#btnupdate").hide();
                    }
                }
            });
            return false;
        }
        else {
            $("#btnsave").show();
            $("#btnload").hide();
        }
    });

    $('#btnupdate').click(function () {
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnupdate").hide();
        if (valid) {
            var frmData = $('#frmBooth').serialize() + "&FileOperation=update&Resource=BOOTHMASTER";
            $.ajax({
                url: "/Booth/editBooth",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmBooth').trigger('reset');
                        $("#MATDANID").empty();
                        dataLoad();
                        $("#btnsave").show();
                        $("#btnload").hide();
                    }
                    else {
                        $("#btnupdate").show();
                        $("#btnload").hide();
                    }
                    alert(msg);
                },
                complete: function (xhr, txterrorcode) {
                    if (xhr.status === 302) {
                        alert("Access denied");
                        $("#btnsave").show();
                        $("#btnload").hide();
                        $("#btnupdate").hide();
                    }
                }
            });
            return false;
        }
        else {
            $("#btnupdate").show();
            $("#btnload").hide();
        }
    });

    $.ajax({
        type: "GET",
        url: "/Booth/getwarddata",
        data: "{}",
        success: function (data) {
            var s = '<option value="">--SELECT WARD--</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].WARDNAME + '</option>';
            }
            $("#WARDID").html(s);
        }
    });

    $("#WARDID").change(function () {
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
            }
        });
    });
});






//datable 
function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $(document).ready(function () {
        $("#Boothtbl").DataTable({
            destroy: true,
            "pagingType": "simple_numbers_no_ellipses",
            "serverside": "true",
            "order": [1, "asc"],
            "processing": true,
            "serverSide": true,

            "oLanguage": {
                "sLengthMenu": "SHOW _MENU_ ENTRIES",
                "sSearch": "SEARCH:"
            },

            "ajax": {
                url: "/Booth/BoothDT",
                method: "post",
                dataType: 'json'
            },
            "columnDefs": [{ 'orderable': false, 'targets': 0 }],
            "columns": [
                {
                    "data": null,
                    "render": function (data, row, type) {
                        return ' <a href="javascript:void(0);" onclick="fetchData(' + data.PrimaryId + ');"><i class="fa fa-pencil" aria-hidden="true"></i></a>';
                    }
                },
                {
                    "data": "WardName",
                    "className": "text-left"
                },
                {
                    "data": "MatdanMathakName",
                    "className": "text-left"
                },
                {
                    "data": "Address",
                    "className": "text-left"
                },
                { "data": "Mobile" },
                { "data": "Booth1" },
                { "data": "Booth2" },
                { "data": "Booth3" },
                { "data": "Booth4" },
                { "data": "Booth5" }
            ],

        });

    });
}




//fetchdata for update
var userData;

//fetchdata for update
function fetchData(primaryId) {

    $("#btnsave").hide();
    $("#btnupdate").show();
    $.ajax({
        url: "/Booth/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=BOOTHMASTER",
        type: "GET",
        success: function (data, status, xhr) {
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                userData = data;

                getMatdanData(userData.WardId);

                $('#ID').val(data.PrimaryId);
                $('#WARDID').val(data.WardId);
                $('#MATDANID').val(data.MatdanMathakId);

                $('#BoothNo').val(data.Booth1);
                $('#BoothNo2').val(data.Booth2);
                $('#BoothNo3').val(data.Booth3);
                $('#BoothNo4').val(data.Booth4);
                $('#BoothNo5').val(data.Booth5);

            }
            else {
                alert('request failed');
            }
        },
        complete: function (xhr, txterrorcode) {
            if (xhr.status === 302) {
                alert("Access denied");
                $("#btnsave").show();
                $("#btnload").hide();
                $("#btnupdate").hide();
            }
        }
    });

}


function getMatdanData(WardId) {
    $.ajax({
        url: "/Booth/editgetmatdanmathakdata",
        method: "POST",
        data: {
            "ID": WardId
        },
        success: function (data) {
            $("#MATDANID").empty();
            $.each(data.data, function (index, row) {
                var Tag = '<option value="' + data.data[index].PrimaryId + '">' + data.data[index].MatdanMathakName + '</option>';
                $("#MATDANID").append(Tag);
            });s          
            $('#MATDANID').val(userData.MatdanId);
        }
    });
}