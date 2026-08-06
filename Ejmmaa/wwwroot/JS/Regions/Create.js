

$(document).ready(function () {

    // 2. إرسال البيانات
    $('#createRegionForm').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serialize(); // تحويل بيانات النموذج لـ Object
        SaveRegion();
    });
});



function SaveRegion()
{
    regionData = {
        regionName : $('#RegionName').val()
    }; 

    apiAdd(
     '/Regions/AddRegion',
     regionData,
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