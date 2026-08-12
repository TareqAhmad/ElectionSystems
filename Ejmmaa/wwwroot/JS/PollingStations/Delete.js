
$(document).ready(function () {


    $('#deletePollingStationForm').on('submit', function (e) {
        e.preventDefault();
        DeletePollingStation();
    });
});


function DeletePollingStation() {

    var pollingStation = {
        stationId: $('#StationId').val()
    };

    apiDelete(
        '/PollingStations/DeletePollingStation',
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