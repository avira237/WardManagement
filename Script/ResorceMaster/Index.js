$(document).ready(function () {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $("#btn1").show();
    $("#btnedit").hide();
    $("#btnreset").show();
    $("#btnload").hide();

    $("#btnreset").click(function () {
        $("#btn1").show();
        $("#btnreset").show();
        $("#btnedit").hide();
    });

    $("#resorcetbl").DataTable({
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
            url: "/Resorce/ResourceDT",
            method: "post",
            datatype: "json"
        },
        "columnDefs": [{ 'orderable': false, 'targets': 0 }],
        "columns": [
            {
                "data": null,
                "render": function (data, row, type) {
                    return ' <a href="javascript:void(0);" onclick="fetchData(' + data.primaryId + ');"><i class="fa fa-pencil" aria-hidden="true"></i></a>';
                }
            },

            
              { "data": "ResourceName" }

        ],
    });
});

function fetchData(primaryId) {

    $("#btn1").hide();
    $("#btnedit").show();
    $.ajax({
       
        url: "/Resorce/Getdataid?primaryId=" + primaryId,
        type: "GET",
        success: function (data, status, xhr) {
                
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                $('#ID').val(data.ID);
                $('#ResourceName').val(data.ResourceName);
            }

            else {
                alert('request failed');
            }
        }
    });
}