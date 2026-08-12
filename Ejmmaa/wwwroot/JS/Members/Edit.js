
$(document).ready(function () {
    // Your jQuery code here

    $('#EditMemberForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission
        UpdateMember(); // Call the function to update the member
    });
});


function UpdateMember() {

    var memberData = {
        memberId: $('#memberId').val(),
        fullName: $('#fullName').val(),
        nationalId: $('#nationalId').val(),
        phoneNumber: $('#phoneNumber').val(),
        sectionId: $('#sectionSelect').val(),
        birthDate: $('#birthDate').val(),
        gender: $('#genderSelect').val()
    };

    apiUpdate(
        '/Members/UpdateMember',
        memberData,
        function (response) {
            if (response.success) {
                showToast(response.message, 'success');
            }
        },
        function (error) {
            showToast(response.message, 'error');
        }
    );


}