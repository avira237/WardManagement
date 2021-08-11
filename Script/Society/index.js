//SOCIETY JS
$(document).ready(function () {
    $("#btnreset").click(function () {
        $("#btnsave").show();
        $("#btnupdate").hide();
        $("#MATDANID").empty();
        $("#BOOTHID").empty();
    });
    $.ajax({
        type: "GET",
        url: "/Society/getwarddata",
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
        // alert("called");
        $.ajax({
            url: "/Society/getmatdanmathak",
            method: "POST",
            data: {
                "ID": $("#WARDID").val()
            },
            success: function (data) {
                $("#MATDANID").empty().append('<option value="">--SELECT MATDANMATHAK--</option>');
                $.each(data.data, function (index, row) {
                    var Tag = '<option value="' + data.data[index].PrimaryId + '">' + data.data[index].MatdanMathakName + '</option>';
                    $("#MATDANID").append(Tag);
                });
            }
        });
    });
    $("#MATDANID").change(function () {
        $.ajax({
            url: "/Society/getbooth",
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
    });
    $("#btnsave").show();
    $("#btnupdate").hide();
    $("#btnload").hide();
    dataLoad();
    $("#SOCIETYNAME").inputmask(
        {
            regex: '^[a-zA-Z-()._ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $('#btnsave').click(function () {
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnsave").hide();
        if (valid) {
            var frmData = $('#frmSociety').serialize() + "&FileOperation=add&Resource=SOCIETYMASTER";
            $.ajax({
                url: "/Society/SaveSociety",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmSociety').trigger('reset');
                        $("#MATDANID").empty();
                        $("#BOOTHID").empty();
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
            var frmData = $('#frmSociety').serialize() + "&FileOperation=update&Resource=SOCIETYMASTER";
            $.ajax({
                url: "/Society/editSociety",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmSociety').trigger('reset');
                        $("#MATDANID").empty();
                        $("#BOOTHID").empty();
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
});
function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $(document).ready(function () {
        $("#Societytbl").DataTable({
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
                url: "/Society/societyDT",
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
                    "mData": null,
                    "className": "text-left",
                    "mRender": function (data, type, full) {
                        if (full['Landmark'] == null) {
                            return full['SocietyName'];
                        }
                        else {
                            return full['SocietyName'] + '/' + full['Landmark'];
                        }
                    },
                    "className": "text-left"
                },
                {
                    "data": "WardName",
                    "className": "text-left"
                },
                {
                    "data": "MardanName",
                    "className": "text-left"
                },
                { "data": "Booth1" }
            ],
        });
    });
}
var userData;
function fetchData(primaryId) {
    $("#btnsave").hide();
    $("#btnupdate").show();
    $.ajax({
        url: "/Society/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=SOCIETYMASTER",
        type: "GET",
        success: function (data, status, xhr) {
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];               
                userData = data;
                getMatdanData(userData.WardId);
                dropgetbooth(userData.MatdanId);
                $('#ID').val(data.PrimaryId);
                $('#SOCIETYNAME').val(data.SocietyName);
                $('#LANDMARK').val(data.Landmark);
                $('#WARDID').val(data.WardId);
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
        url: "/Society/editgetmatdanmathakdata",
        method: "POST",
        data: {
            "ID": WardId
        },
        success: function (data) {
            $("#MATDANID").empty();
            $.each(data.data, function (index, row) {
                var Tag = '<option value="' + data.data[index].PrimaryId + '">' + data.data[index].MatdanMathakName + '</option>';
                $("#MATDANID").append(Tag);
            });          
            $('#MATDANID').val(userData.MatdanId);
        }
    });
}
function dropgetbooth(MatdanId) {
    $.ajax({
        url: "/Society/getbooth",
        method: "POST",
        data: {
            "ID": MatdanId
        },
        success: function (data) {
            $("#BOOTHID").empty();
            $.each(data.data, function (index, row) {

                var Tag = '<option value="' + data.data[index].ID + '">' + data.data[index].BOOTH1 + '</option>';
                $("#BOOTHID").append(Tag);
            });
            $('#BOOTHID').val(userData.BoothId);
        }
    });
}