
$(document).ready(function () {


    $('#deleteRegionForm').on('submit', function (e) {
        e.preventDefault();
        DeleteRegion();
    });
});


function DeleteRegion() {

    regionData = {
        regionId: $('#RegionId').val(),
    };

    apiDelete(
        '/Regions/DeleteRegion',
        regionData,
        function (res) {
            if (res.success)
                showToast(res.message, 'success');
        },
        function (errorMessage) {
            // Handle error response
            showToast(res.message, 'error')
        }
    );

}


