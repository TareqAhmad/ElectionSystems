$(document).ready(function () {
    // 1. تحميل الأقسام عند فتح الصفحة
   
    loadSectionsIntoSelect();

    // 2. إرسال البيانات
    $('#addMemberForm').on('submit', function (e) {
        e.preventDefault();
        var formData = $(this).serialize(); // تحويل بيانات النموذج لـ Object
        SaveMember();
    });
});



function loadSectionsIntoSelect() {
  
    $.get('/Admins/GetClanSections', function (res) {
        if (res.success) {
            var select = $('#sectionSelect');
            res.data.forEach(s => {
                select.append(`<option value="${s.sectionId}">${s.sectionName}</option>`);
            });
        }
    });
}


function SaveMember()
{
    var AddData = {
        fullName : $('#fullName').val(),
        nationalId : $('#nationalId').val(),
        phoneNumber : $('#phoneNumber').val(),
        sectionId : $('#sectionSelect').val(),
        birthDate : $('#birthDate').val(),
        gender : $('#genderSelect').val() 
    }; 

    apiAdd(
        '/Members/SaveMember',
        AddData,
         function(response) { 
            if(response.success)
            {
                showToast(response.message,'success'); 
            } 
         },
         function(error){
                showToast(response.message,'error'); 
         }
    ); 



}