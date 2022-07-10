// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ChangeStatus(id) {

    $.ajax({
        url: '/GuidModel/ChangeStatus',
        type: 'POST',
        data: JSON.stringify(id),
        contentType: 'application/json',
        success: function (result, status, xhr) {
            if (xhr.status == 200) {
                $("#" + id).remove();
                $("#modalLabel").text('Success');
                $("#modalBody").text('You have successfully changed the status to ready to save.');
                $("#myModal").modal('show');
            }
        }
    });

    $("#btnCloseModal").click(function () {
        $("#myModal").modal("hide");
        $("#modalBody").text('');
    });
}