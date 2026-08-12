
$(document).ready(function () {


    $('#EditRegionForm').on('submit', function (e) {
        e.preventDefault();
        UpdateRegion();
    });
});


function UpdateRegion() {

    regionData = {
        regionId: $('#RegionId').val(),
        regionName: $('#RegionName').val(),
        regionCode: $('#RegionCode').val(),
        governorateId: $('#GovernorateSelect').val(),
    };

    apiUpdate(
        '/Regions/UpdateRegion',
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


