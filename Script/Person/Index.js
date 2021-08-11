$(document).ready(function () {
    $("#btnreset").click(function () {
        $("#btnsave").show();
        $("#btnupdate").hide();
        $("#MATDANID").empty();
        $("#BOOTHID").empty();
        $("#SOCIETYID").empty();
    });
    $("#DOB").blur(function () {
        var dob = $('#DOB').val();
        console.log(dob);
        if (dob != '') {
            dob = new Date(dob);
            var today = new Date();
            var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
            $('#AGE').val(age);
        }
    });
    $("#VOTERID").inputmask(
        {
            mask: "aaa9999999",
            onincomplete: function () {
                alert('Enter Correct Voter Id.');
                $(this).focus();
            }
        });
    $("#NAME").inputmask(
        {
            regex: '^[a-zA-Z-()._ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#AGE").inputmask('numeric',
        {
            min: 18,
            max: 200,
            rightAlign: false
        });
   
    $("#ADDRESS").inputmask(
        {
            regex: '^[a-zA-Z-().,0-9_ ]*$',
            oncomplete: function () {
                $(this).val($(this).val().replace("  ", " "));
            }
        });
    $("#UNIQUENUMBER").inputmask(
        {
            regex: '^[0-9]{1,6}'
        });
    $("#btnsave").show();
    $("#btnupdate").hide();
    $("#btnload").hide();
    dataLoad();
    $('#btnsave').click(function () {
        $("#VOTERID").inputmask(
            {
                mask: "aaa9999999",
                onincomplete: function () {
                    alert('Enter Correct Voter Id.');
                    $(this).focus();
                }
            });
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnsave").hide();
        if (valid) {
            var frmData = $('#frmPerson').serialize() + "&FileOperation=add&Resource=PERSONINFO";          
            $.ajax({
                url: "/Person/SavePerson",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmPerson').trigger('reset');
                        $("#MATDANID").empty();
                        $("#BOOTHID").empty();
                        $("#SOCIETYID").empty();
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

    // update
    $('#btnupdate').click(function () {
        $("#VOTERID").inputmask(
            {
                mask: "aaa9999999",
                onincomplete: function () {
                    alert('Enter Correct Voter Id.');
                    $(this).focus();
                }
            });
        var valid = this.form.checkValidity();
        $("#btnload").show();
        $("#btnupdate").hide();
        if (valid) {
            var frmData = $('#frmPerson').serialize() + "&FileOperation=update&Resource=PERSONINFO";
            $.ajax({
                url: "/Person/editPerson",
                type: "POST",
                data: frmData,
                success: function (data, status, xhr) {
                    var Status = data.Status;
                    var msg = data.msg;
                    if (xhr.status === 200 && Status === 1) {
                        $('#frmPerson').trigger('reset');
                        $("#MATDANID").empty();
                        $("#BOOTHID").empty();
                        $("#SOCIETYID").empty();
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
        url: "/Person/getwarddata",
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
            url: "/Person/getmatdanmathak",
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

    $("#BOOTHID").change(function () {
        // alert("called");
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
    });

    $("#MATDANID").change(function () {
        // alert("called");
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
    });
});
function dataLoad() {
    $.fn.DataTable.ext.pager.numbers_length = 4;
    $("#Persontbl").DataTable({
        destroy: true,
        "pagingType": "simple_numbers_no_ellipses",
        "serverside": "true",
        "order": [1, "asc"],
        "processing": true,
        "serverSide": true,
        "ajax": {
            url: "/Person/PersonDT",
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
            { "data": "UniqueNumber" },
            { "data": "VoterId" },
            { "data": "PersonName", "className": "text-left" },
            { "data": "Gender", "className": "text-left" },
            {
                "data": "Dob",
                "type": "date ",
            },
            { "data": "Age" },
            { "data": "SocietyName", "className": "text-left" },
            { "data": "Address", "className": "text-left" },
            { "data": "WardName", "className": "text-left" },
            { "data": "MatdanName", "className": "text-left" },
            { "data": "Booth1" },
        ],
    });
}
var userData;
//fetchdata for update
function fetchData(primaryId) {
    $("#btnsave").hide();
    $("#btnupdate").show();
    $.ajax({
        url: "/Person/Getdataid?primaryId=" + primaryId + "&FileOperation=update&Resource=PERSONINFO",
        type: "GET",
        success: function (data, status, xhr) {           
            var Status = data.Status;
            if (xhr.status === 200 && Status === 1) {
                var data = data.data[0];
                userData = data;
                getMatdanData(userData.WardId);
                dropgetbooth(userData.MatdanId);
                dropgetsociety(userData.BoothId);
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
    $('#UNIQUENUMBER').val(data.UniqueNumber);
    $('#VOTERID').val(data.VoterId);
    $('#NAME').val(data.PersonName);
    var gen = data.Gender;   
    if (gen.toLowerCase() === 'male') {
        $('input:radio[name=GENDER][value=Male]').prop('checked', true);
    } else if (gen.toLowerCase() === 'female') {
        $('input:radio[name=GENDER][value=Female]').prop('checked', true);
    } else if (gen.toLowerCase() === 'other') {
        $('input:radio[name=GENDER][value=Other]').prop('checked', true);
    }
    $('#DOB').val(userData.Dob);
    $('#AGE').val(data.Age);
    $('#ADDRESS').val(data.Address);
    $('#WARDID').val(data.WardId);
}
function getMatdanData(WardId) {
    $.ajax({
        url: "/Person/editgetmatdanmathakdata",
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
        url: "/Person/editboothdata",
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
function dropgetsociety(BoothId) {
    $.ajax({
        url: "/Person/editsocietydata",
        method: "POST",
        data: {
            "ID": BoothId
        },
        success: function (data) {
            $("#SOCIETYID").empty();
            $.each(data.data, function (index, row) {

                var Tag = '<option value="' + data.data[index].ID + '">' + data.data[index].SOCIETYNAME + '</option>';
                $("#SOCIETYID").append(Tag);
            });
            $('#SOCIETYID').val(userData.SocietyId);
        }
    });
}