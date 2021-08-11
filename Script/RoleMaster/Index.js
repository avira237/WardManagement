$(document).ready(function () {
    $("#ROLEID").change(function () {
        dataLoad();
    });
    //getrole
    $.ajax({
        type: "GET",
        url: "/Role/getRole",
        data: "{}",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].ROLE_NAME == 'SUPER-ADMIN') {
                    continue;
                }
                else {
                    var s = '<option value="' + data[i].ID + '">' + data[i].ROLE_NAME + '</option>';
                    $("#ROLEID").append(s);
                }
            }
            dataLoad();
        }
    });
});

 //get datatable data
var check;
function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $("#Righttbl").DataTable({
        destroy: true,
        "pagingType": "simple_numbers_no_ellipses",
        "serverside": "true",
        "order": [0, "asc"],
        "processing": true,
        "serverSide": true,
        "oLanguage": {
            "sLengthMenu": "SHOW _MENU_ ENTRIES",
            "sSearch": "SEARCH:",
        },
        "ajax": {
            url: "/Role/RightDT",
            method: "post",
            data: {
                "ID": $("#ROLEID").val()
            },
            datatype: "json"
        },
        "columnDefs": [{ 'orderable': false, 'targets':[1,2,3,4,5] }],
        "columns": [
            {
                "data": "RESOURCE_NAME",
                "className": "text-left"
            },
            {
                "data": null,
                "orderable":false,
                "render": function (data, row, type) {
                    if (data.ISADD == false) {
                        check = true;
                        return '<input type="checkbox" id="editisadd" onclick="editIsAdd('+data.ID+','+check+')" /> ';
                    }
                    else {
                        check = false;
                        return '<input type="checkbox" checked id="editisadd" onclick="editIsAdd(' + data.ID + ',' + check +')" />';
                    }
                }
            },
            {
                "data": null,
                "render": function (data, row, type) {
                    if (data.ISUPDATE == false) {
                        check = true;
                        return '<input type="checkbox" id="editisupdate" onclick="editIsUpdate(' + data.ID + ',' + check + ')" /> ';
                    }
                    else {
                        check = false;
                        return '<input type="checkbox" checked id="editisupdate" onclick="editIsUpdate(' + data.ID + ',' + check + ')" />';
                    }
                }
            },
            {
                "data": null,
                "render": function (data, row, type) {
                    if (data.ISVIEW == false) {
                        check = true;
                        return '<input type="checkbox" id="editisview" onclick="editIsView(' + data.ID + ',' + check + ')" /> ';
                    }
                    else {
                        check = false;
                        return '<input type="checkbox" checked id="editisview" onclick="editIsView(' + data.ID + ',' + check + ')" />';
                    }
                }
            },
            {
                "data": null,
                "render": function (data, row, type) {
                    if (data.ISPRINT == false) {
                        check = true;
                        return '<input type="checkbox" id="editisprint" onclick="editIsPrint(' + data.ID + ',' + check + ')" /> ';
                    }
                    else {
                        check = false;
                        return '<input type="checkbox" checked id="editisprint" onclick="editIsPrint(' + data.ID + ',' + check + ')" />';
                    }
                }
            },
            {
                "data": null,
                "render": function (data, row, type) {
                    if (data.ISDELETE == false) {
                        check = true;
                        return '<input type="checkbox" id="editisdelete" onclick="editIsDelete(' + data.ID + ',' + check + ')" /> ';
                    }
                    else {
                        check = false;
                        return '<input type="checkbox" checked id="editisdelete" onclick="editIsDelete(' + data.ID + ',' + check + ')" />';
                    }
                }
            },
        ],
    });
}

function editIsAdd(primaryId,check) {
    $.ajax({
        url: "/Role/IsAddChange?primaryId=" + primaryId,
        type: "POST",
        data: {
            "check":check
        },
        success: function (data, status, xhr) {
            var Status = data.Status;
            var msg = data.msg;
            if (xhr.status === 200 && Status === 1) {
               
            }
        }
    });
    return false;
}

function editIsUpdate(primaryId, check) {
    $.ajax({
        url: "/Role/IsUpdateChange?primaryId=" + primaryId,
        type: "POST",
        data: {
            "check": check
        },
        success: function (data, status, xhr) {
            var Status = data.Status;
            var msg = data.msg;
            if (xhr.status === 200 && Status === 1) {
               
            }
        }
    });
    return false;
}

function editIsView(primaryId, check) {
    $.ajax({
        url: "/Role/IsViewChange?primaryId=" + primaryId,
        type: "POST",
        data: {
            "check": check
        },
        success: function (data, status, xhr) {
            var Status = data.Status;
            var msg = data.msg;
            if (xhr.status === 200 && Status === 1) {
               
            }
        }
    });
    return false;
}

function editIsPrint(primaryId, check) {
    $.ajax({
        url: "/Role/IsPrintChange?primaryId=" + primaryId,
        type: "POST",
        data: {
            "check": check
        },
        success: function (data, status, xhr) {
            var Status = data.Status;
            var msg = data.msg;
            if (xhr.status === 200 && Status === 1) {
              
            }
        }
    });
    return false;
}

function editIsDelete(primaryId, check) {
    $.ajax({
        url: "/Role/IsDeleteChange?primaryId=" + primaryId,
        type: "POST",
        data: {
            "check": check
        },
        success: function (data, status, xhr) {
            var Status = data.Status;
            var msg = data.msg;
            if (xhr.status === 200 && Status === 1) {
              
            }
        }
    });
    return false;
}
