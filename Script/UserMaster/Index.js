$(document).ready(function () {
    $("#btnsave").show();
    $("#btnupdate").hide();
    $("#btnload").hide();
    $("#PASSWORD").attr("readonly", true);
    $("#MOBILE").attr("readonly", true);
    $("#EMAIL").attr("readonly", true);
    $("#ROLEID").attr("readonly", true);
    $("#USERNAME").attr("readonly", true);
    $("#LOCKEDREASON").attr("readonly", true);
    $("input[type=radio]").attr('disabled', true);
    $("#USERID").inputmask(
        {
            regex: '^[a-zA-Z0-9-@_ ]{1,10}',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#USERNAME").inputmask(
        {
            regex: '^[a-zA-Z_ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#PASSWORD").inputmask(
        {
            regex: '^[a-zA-Z-()@#$&{}./\_ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#MOBILE").inputmask(
        {
            regex: '^[6-9]{1}[0-9]{9}'
        });
    $("#EMAIL").inputmask(
        'email'
     );
    $("#LOCKEDREASON").inputmask(
        {
            regex: '^[a-zA-Z-()._ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    dataLoad();
    $("#btnreset").click(function () {
        $("#btnsave").show();
        $("#btnupdate").hide();
        $("#PASSWORD").attr("readonly", true);
        $("#MOBILE").attr("readonly", true);
        $("#EMAIL").attr("readonly", true);
        $("#ROLEID").attr("readonly", true);
        $("#USERNAME").attr("readonly", true);
        $("#LOCKEDREASON").attr("readonly", true);
        $("input[type=radio]").attr('disabled', true);
      
       
    });
    $("#True").click(function () {
        $("#LOCKEDREASON").attr("readonly",false);
    });
    $("#False").click(function () {
        $("#LOCKEDREASON").attr("readonly",true);
    });
    $("#USERID").blur(function () {
        var uid = $("#USERID").val();
        $.ajax({
            type: "GET",
            url: "/User/getuserid?uid=" + uid,           
            success: function (data,status,xhr) {
                var Status = data.Status;
                if (xhr.status === 200 && Status === 1) {                   
                    var a = data;
                    if (data.data == false) {
                        alert("User Id is Already Taken..");
                        $("#PASSWORD").attr("readonly", true);
                        $("#MOBILE").attr("readonly", true);
                        $("#EMAIL").attr("readonly", true);
                        $("#ROLEID").attr("readonly", true);
                        $("#USERNAME").attr("readonly", true);
                        $("#LOCKEDREASON").attr("readonly", true);
                        $("input[type=radio]").attr('disabled', true);
                    }
                    else {
                        $("#PASSWORD").attr("readonly",false);
                        $("#MOBILE").attr("readonly",false);
                        $("#EMAIL").attr("readonly",false);
                        $("#ROLEID").attr("readonly",false);
                        $("#LOCKEDREASON").attr("readonly",false);
                        $("#USERNAME").attr("readonly",false);
                        $("input[type=radio]").attr('disabled', false);
                    }                                
                }

            }
        });
    });
    
    $.ajax({
        type: "GET",
        url: "/Role/getRole",
        data: "{}",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                    var s = '<option value="' + data[i].ID + '">' + data[i].ROLE_NAME + '</option>';
                    $("#ROLEID").append(s);
            }
            dataLoad();
        }
    });
    $('#btnsave').click(function () {
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnsave").hide();
        if (valid) {
            var frmData = $('#frmuser').serialize() + "&FileOperation=add&Resource=USERMASTER";           
            $.ajax({
                url: "/User/SaveUser",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmuser').trigger('reset');
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
            var frmData = $('#frmuser').serialize() + "&FileOperation=update&Resource=USERMASTER";
            $.ajax({
                url: "/User/UpdateUser",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmuser').trigger('reset');
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
    $("#usertbl").DataTable({
        destroy: true,
        "pagingType": "simple_numbers_no_ellipses",
        "serverside": "true",
        "order": [1, "asc"],
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: "/User/UserMasterDT",
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
                "data": "UserName",
                "className": "text-left"
            },
            { "data": "UserId" },          
            {
                "data": "Email",
                "className": "text-left"
            },
            {
                "data": "RoleName",
                "className": "text-left"
            },
            { "data":"Mobile"},
            { "data": "Is_Locked" },
            {
                "data": "Locked_Reason",
                "className": "text-left"
            }
        ],
    });
}
var userData;

function fetchData(primaryId) {
    $("#btnsave").hide();
    $("#btnupdate").show();
    $("#PASSWORD").attr("readonly", true);
    $("#USERID").attr("readonly", true);
    $("#MOBILE").attr("readonly", false);
    $("#EMAIL").attr("readonly", false);
    $("#ROLEID").attr("readonly", false);
    $("#LOCKEDREASON").attr("readonly", false);
    $("#USERNAME").attr("readonly", false);
    $("input[type=radio]").attr('disabled', false);
    $.ajax({
        url: "/User/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=USERMASTER", 
        type: "GET",
        success: function (data, status, xhr) {           
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                userData = data;
                getRoleData(userData.RoleId);
                setFetchedData(userData);
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
function setFetchedData(data) {
    $('#ID').val(data.PrimaryId);
    $('#USERNAME').val(data.UserName);
    $('#USERID').val(data.UserId);
    $('#MOBILE').val(userData.Mobile);
    $('#EMAIL').val(data.Email);
    $('#ROLEID').val(data.RoleName);
    var islocked = data.Is_Locked;    
    if (islocked === false) {
        $('input:radio[name=ISLOCKED][value=False]').prop('checked', true);
        $("#LOCKEDREASON").attr("readonly", true);
    } else if (islocked === true) {
        $('input:radio[name=ISLOCKED][value=True]').prop('checked', true);
        $("#LOCKEDREASON").attr("readonly",false);
    } 
    $('#LOCKEDREASON').val(data.Locked_Reason);
}
function getRoleData(RoleId) {
    $.ajax({
        type: "GET",
        url: "/Role/getRole",
        data: "{}",
        success: function (data) {
            $("#ROLEID").empty();
            for (var i = 0; i < data.length; i++) {
                var s = '<option value="' + data[i].ID + '">' + data[i].ROLE_NAME + '</option>';
                $("#ROLEID").append(s);
            }
            $("#ROLEID").val(RoleId);
            dataLoad();
        }
    });
}
