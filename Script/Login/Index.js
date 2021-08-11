$(document).ready(function () {
    $("#USER_ID").inputmask(
        {
            regex: '^[a-zA-Z0-9_ ]*$'
        });
    $("#PASSWORD").inputmask(
        {
            regex: '^[a-zA-Z0-9().-_ ]*$'
        });
    $("#btn_submit").click(function () {
        var frmData = $('#frmlogin').serialize();
        $.ajax({
            url: "/Login/Loginuser",
            type: "POST",
            data: frmData,
            success: function (data, status,xhr) {
                var Status = data.Status;
                var msg = data.msg;
                if (xhr.status === 200 && Status === 1) {
                    if (data.data.IsLocked==true) {
                        alert("Temporary locked..");
                        $('#frmlogin').trigger('reset');
                    }
                    else {
                        window.location.href = data.redirecturl;
                    }
                }
                else
                {
                    alert(msg);
                    $('#frmlogin').trigger('reset');
                }
            },
        });
        return false;
    });
});