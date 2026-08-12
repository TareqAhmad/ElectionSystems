
$(document).ready(function () {
    // Your edit form initialization code here

    $('#EditSupervisorForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission
        UpdateSupervisor();
    });
});


function UpdateSupervisor() {

    supervisorData = {
        supervisorId: $('#SupervisorId').val(),
        boxId: $('#BallotBoxSelect').val(),
        fullName: $('#SupervisorName').val(),
        nationalId: $('#NationalId').val(),
        phoneNumber: $('#PhoneNumber').val(),
        userName: $('#UserName').val(),
        passwordHash: $('#Password').val(),
    };

    apiUpdate(
        '/Supervisors/UpdateSupervisor',
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