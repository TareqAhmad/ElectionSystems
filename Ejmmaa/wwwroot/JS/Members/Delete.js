
$(document).ready(function () {
    // Your jQuery code here

    $('#deleteMemberForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission
        DeleteMember(); // Call the function to update the member
    });
});


function DeleteMember() {

    var memberData = {
        memberId: $('#memberId').val(),
    };

    apiDelete(
        '/Members/DeleteMember',
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