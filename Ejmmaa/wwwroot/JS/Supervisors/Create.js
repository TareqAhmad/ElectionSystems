
$(document).ready(function () {

    loadBallotBoxIntoSelect();

    $('#showPassword').on('change', function () {

        var password = $('#Password');

        if ($(this).is(':Checked')) {
            password.attr('type', 'text');
        } else {
            password.attr('type', 'password');

        }


    });


    $('#createSupervisorForm').on('submit', function (e) {
        e.preventDefault();
        saveSupervisor();
    });

});



function loadBallotBoxIntoSelect() {

    $.get('/BallotBoxes/GetAllBallotBoxes', function (res) {
        if (res.success) {
            var select = $('#BallotBoxSelect');
            select.empty(); // تفريغ القائمة أولاً لمنع تكرار العناصر
            select.append('<option value="">اختر الصندوق</option>'); // إضافة الخيار

            res.data.forEach(b => {
                select.append(`<option value="${b.boxId}">${b.boxNumber}</option>`);
            });
        }
    });
}


function saveSupervisor() {
    supervisorData = {
        boxId: $('#BallotBoxSelect').val(),
        fullName: $('#SupervisorName').val(),
        nationalId: $('#NationalId').val(),
        phoneNumber: $('#PhoneNumber').val(),
        userName: $('#UserName').val(),
        passwordHash: $('#Password').val(),
    }



    apiAdd(
        '/Supervisors/AddSupervisor',
        supervisorData,
        function (res) {
            if (res.success)
                showToast(res.message, 'success');
        },
        function (error) {
            showToast(res.message, 'error');
        }

    );




}