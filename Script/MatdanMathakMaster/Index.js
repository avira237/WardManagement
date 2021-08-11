$(document).ready(function () {
    $("#btnreset").click(function () {
        $("#btnsave").show();
        $("#btnupdate").hide();
        $("#btnload").hide();
    });
    $("#NAME").inputmask(
        {
            regex: '^[a-zA-Z-()._ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#ADDRESS").inputmask(
        {
            regex: '^[a-zA-Z-().,0-9_ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#MOBILE").inputmask(
        {
            regex:'[6-9]{1}[0-9]{9}'
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
            var frmData = $('#frmmatdanmathak').serialize() + "&FileOperation=add&Resource=MATDANMATHAKMASTER";
            $.ajax({
                url: "/MatdanMathak/SaveMatdanMathakMaster",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmmatdanmathak').trigger('reset');
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

    //update function
    $('#btnupdate').click(function () {
        $("#btnload").show();
        $("#btnupdate").hide();
            var frmData = $('#frmmatdanmathak').serialize() + "&FileOperation=update&Resource=MATDANMATHAKMASTER";
            $.ajax({
                url: "/MatdanMathak/editMatdanMaster",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmmatdanmathak').trigger('reset');
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
            $("#btnupdate").show();
            $("#btnload").hide();
    });

    $.ajax({
        type: "GET",
        url: "/MatdanMathak/getDepartment",
        data: "{}",
        success: function (data) {
            var s = '<option value="">---SELECT WARD---</option>';

            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].WARDNAME + '</option>';
            }
            $("#WARDID").html(s);
        }
    });
});

//datatable
function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $(document).ready(function () {
        $("#MTtbl").DataTable({
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
                url: "/MatdanMathak/MatDT",
                method: "post",
                dataType: 'json'
            },
            "columnDefs": [{ 'orderable': false, 'targets': [0, 3, 4] }],
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
                { "data": "Mobile" }
            ],
        });
    });
}

//fetchdata for update
function fetchData(primaryId) {
    $("#btnsave").hide();
    $("#btnupdate").show();
    $.ajax({
        url: "/MatdanMathak/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=MATDANMATHAKMASTER",
        type: "GET",
        success: function (data, status, xhr) {
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                $('#ID').val(data.PrimaryId);
                $('#WARDID').val(data.WardId);
                $('#NAME').val(data.MatdanMathakName);
                $('#ADDRESS').val(data.Address);
                $('#MOBILE').val(data.Mobile);
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