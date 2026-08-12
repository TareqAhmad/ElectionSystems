
$(document).ready(function () {
    // Your edit form initialization code here

    $('#deleteSupervisorForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission
        DeleteSupervisor();
    });
});


function DeleteSupervisor() {

    supervisorData = {
        supervisorId: $('#SupervisorId').val(),
    };

    apiDelete(
        '/Supervisors/DeleteSupervisor',
        supervisorData,
        function (res) {
            if (res.success)
                showToast(res.message, 'success');
        },
        function (errorMessage) {
            // Handle error response
            showToast(res.message, 'error')
        }

    )






}