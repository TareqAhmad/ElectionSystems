

$(document).ready(function () {
      
    console.log('Test 1');

    loadPollingStationsIntoSelect();

    console.log('Test 2');

  
    // 2. إرسال البيانات
    $('#createBallotBoxForm').on('submit', function (e) {
        e.preventDefault();
        console.log('Test 3');
        SaveBallotBoxes();
    });
});



function loadPollingStationsIntoSelect() {
  
    $.get('/PollingStations/GetAllPollingStations', function (res) {
        if (res.success) {
            var select = $('#StationSelect');
            select.empty(); // تفريغ القائمة أولاً لمنع تكرار العناصر
            select.append('<option value="">اختر مركز الاقتراع</option>'); // إضافة الخيار
           
            res.data.forEach(p => {
                console.log(p.stationId);
                select.append(`<option value="${p.stationId}">${p.stationName}</option>`);
            });
        }
    });
};


function SaveBallotBoxes()
{
    var ballotBox = {
        stationId : $('#StationSelect').val(),
        boxNumber : $('#BoxNumber').val()
    }; 

     console.log(ballotBox);

    apiAdd(
        '/BallotBoxes/AddBallotBox',
        ballotBox,
        function(response)
        {
          if(response.success)
             {
                showToast(response.message,'success'); 
             }
        },
        function(errorMessage)
        {
         showToast(errorMessage,'error'); 
       }
    )
}