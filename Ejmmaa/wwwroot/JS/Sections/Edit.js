
$(document).ready(function () {
    // Your code here

    $('#EditSectionForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission

        UpdateSection();
    });
});


function UpdateSection() {

    var sectionData = {
        sectionId: $('#SectionId').val(),
        sectionName: $('#SectionName').val(),
    };

    apiUpdate(
        '/Sections/UpdateSection',
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