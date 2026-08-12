
$(document).ready(function () {


    $('#EditPollingStationForm').on('submit', function (e) {
        e.preventDefault();
        UpdatePollingStation();
    });
});


function UpdatePollingStation() {

    var pollingStation = {
        stationId: $('#StationId').val(),
        stationName: $('#StationName').val(),
        regionId: $('#RegionSelect').val(),
        locationDetails: $('#LocationDetails').val()
    };

    apiUpdate(
        '/PollingStations/UpdatePollingStation',
        pollingStation,
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