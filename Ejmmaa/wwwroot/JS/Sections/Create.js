$(document).ready(function () {
   
    // 2. إرسال البيانات
    $('#createSectionForm').on('submit', function (e) {
        e.preventDefault();
        SaveSection();
    });
});


function SaveSection()
{
    var AddData = {
        sectionName : $('#SectionName').val(),
    }; 

    apiAdd(
        '/Sections/SaveSection',
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