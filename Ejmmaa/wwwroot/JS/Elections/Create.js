
$(document).ready(function(){

   $('#createElectionForm').on('submit', function (e) {
        e.preventDefault();
        //var formData = $(this).serialize(); // تحويل بيانات النموذج لـ Object
        SaveNewElection();
    });
}); 



function SaveNewElection()
{
    electionData = {
        electionTitle : $('#ElectionTitle').val(),
        startDate : $('#StartDate').val(),
        endDate : $('#EndDate').val()
    }; 


    apiAdd(
       '/Elections/AddElection',
       electionData,
       function(response){
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



