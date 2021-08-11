$(document).ready(function () {
    dataLoad();
    $("#btnsave").show();
    $("#btnedit").hide();
    $("#btnload").hide();
    $("#Resourcename").inputmask(
        {
            regex: '^[a-zA-Z-()._ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $('#btnedit').click(function () {
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnedit").hide();
        if (valid) {
            var frmData = $('#frmresourceMaster').serialize() + "&FileOperation=update&Resource=RESOURCEMASTER";
            $.ajax({
                url: "/Resource/editresourceMaster",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmresourceMaster').trigger('reset');
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

    $('#btnsave').click(function () {
        var valid = this.form.checkValidity();
        if (valid) {
            var frmData = $('#frmresourceMaster').serialize() + "&FileOperation=add&Resource=RESOURCEMASTER";
            $("#btnload").show();
            $("#btnsave").hide();
            $.ajax({
                url: "/Resource/SaveResourceMaster",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmresourceMaster').trigger('reset');
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
});

function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $("#resourcetbl").DataTable({
        destroy: true,
        "pagingType": "simple_numbers_no_ellipses",
        "order": [1, "asc"],
        "processing": true,
        "serverSide": true,
        "searching": false,
        "oLanguage": {
            "sLengthMenu": "SHOW _MENU_ ENTRIES",
        },
        "ajax": {
            url: "/Resource/resourceDT",
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
                "data": "ResourceName",
                "className": "text-left"
            }
        ],
    });
}

function fetchData(primaryId) {
    $("#btnsave").hide();
    $("#btnedit").show();
    $.ajax({
        url: "/Resource/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=RESOURCEMASTER",
        type: "GET",
        success: function (data, status, xhr) {
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                $('#ID').val(data.primaryId);
                $('#Resourcename').val(data.ResourceName);
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