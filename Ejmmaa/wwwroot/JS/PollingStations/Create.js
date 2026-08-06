
$(document).ready(function () {

    loadRegionsIntoSelect();
    // 2. إرسال البيانات
    $('#createPollingStationForm').on('submit', function (e) {
        e.preventDefault();
        SavePollingStation();
    });
});


function loadRegionsIntoSelect() {
  
    $.get('/Regions/GetAllRegions', function (res) {
        if (res.success) {
            var select = $('#RegionSelect');
            select.empty(); // تفريغ القائمة أولاً لمنع تكرار العناصر
            select.append('<option value="">اختر المنطقة</option>'); // إضافة الخيار
           
            res.data.forEach(s => {
                select.append(`<option value="${s.regionId}">${s.regionName}</option>`);
            });
        }
    });
}


function SavePollingStation()
{
    var pollingStation = {
        stationName : $('#StationName').val(),
        regionId : $('#RegionSelect').val(),
        locationDetails : $('#LocationDetails').val()
    };
       
     console.log('Test'); 

    if (!pollingStation.regionId) {
        showToast("الرجاء اختيار المنطقة", "error");
        return;
    }

    apiAdd(
     '/PollingStations/AddPollingStation',
     pollingStation,
     function(response)
     {
          if(response.success)
             {
                showToast(response.message,'success'); 
             }
     },
     function(error)
     {
         showToast(response.message,'error'); 
     }
    ); 
}