$(document).ready(function () {
    $("#btnsave").show();
    $("#btnedit").hide();
    $("#btnload").hide();
    $("#Wardname").inputmask(
        {
            regex: '^[a-zA-Z-()._ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    dataLoad();
    $('#btnsave').click(function () {
        var frmData = $('#frmWardMaster').serialize() + "&FileOperation=add&Resource=WARDMASTER";
        $("#btnload").show();
        $("#btnsave").hide();
        $.ajax({
            url: "/WardMaster/SaveWardMaster",
            type: "POST",
            data: frmData,
            success: function (data, status, xhr) {
                var Status = data.Status;
                var msg = data.msg;
                if (xhr.status === 200 && Status === 1) {
                    $('#frmWardMaster').trigger('reset');
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
        $("#btnsave").show();
        $("#btnload").hide();
    });

    $('#btnedit').click(function () {
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnedit").hide();
        if (valid) {
            var frmData = $('#frmWardMaster').serialize() + "&FileOperation=update&Resource=WARDMASTER";
            $.ajax({
                url: "/WardMaster/editWardMaster",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmWardMaster').trigger('reset');
                        dataLoad();
                        $("#btnsave").show();
                        $("#btnload").hide();
                    }
                    else {
                        $("#btnedit").show();
                        $("#btnload").hide();
                    }
                    alert(msg);
                },
                complete: function (xhr, txterrorcode) {
                    if (xhr.status === 302) {
                        alert("Access denied");
                        $("#btnedit").hide();
                        $("#btnsave").show();
                        $("#btnload").hide();
                    }
                }
            });
            return false;
        }
        else {
            $("#btnedit").show();
            $("#btnload").hide();
        }
    });
});
function fetchData(primaryId) {
    $("#btnsave").hide();
    $("#btnedit").show();
    $.ajax({
        url: "/WardMaster/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=WARDMASTER",
        type: "GET",
        success: function (data, status, xhr) {
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                $('#ID').val(data.PrimaryId);
                $('#Wardname').val(data.WardName);
            }
            else {
                alert('request failed');
            }
        },
        complete: function (xhr, txterrorcode) {
            if (xhr.status === 302) {
                alert("Access denied");
                $("#btnedit").hide();
                $("#btnload").hide();
                $("#btnsave").show();
            }
        }
    });
}
$(document).ready(function () {
    $("#btnreset").click(function () {
        $("#btnsave").show();
        $("#btnedit").hide();
    });
});

function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $("#WMtbl").DataTable({
        destroy: true,
        "pagingType": "simple_numbers_no_ellipses",
        "serverside": "true",
        "order": [1, "asc"],
        "processing": true,
        "serverSide": true,
        "oLanguage": {
            "sLengthMenu": "SHOW _MENU_ ENTRIES",
            "sSearch": "SEARCH:",
        },
        "ajax": {
            url: "/WardMaster/WardDT",
            method: "post",
            datatype: "json"
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
        ],
    });
}