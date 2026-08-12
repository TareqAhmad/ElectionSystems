
$(document).ready(function () {
    // Your code here

    $('#deleteSectionForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission
        DeleteSection();
    });
});


function DeleteSection() {

    var sectionData = {
        sectionId: $('#SectionId').val(),
    };

    apiDelete(
        '/Sections/DeleteSection',
        sectionData,
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