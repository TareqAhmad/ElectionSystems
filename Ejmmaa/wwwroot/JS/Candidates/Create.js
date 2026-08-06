
$(document).ready(function () {

    let MemberId; 
    loadElectionsIntoSelect();

    $('#NationalId').on('change',function(e){
         CheckIsMemberExists(); 
    }); 

    // 2. إرسال البيانات
    $('#createCandidateForm').on('submit', function (e) {
        e.preventDefault();
        SaveCandidate();
    });



});

function loadElectionsIntoSelect() {
  
    $.get('/Elections/GetAllElections', function (res) {
        if (res.success) {
            var select = $('#ElectionSelect');
            select.empty(); // تفريغ القائمة أولاً لمنع تكرار العناصر
            select.append('<option value="">اختر العملية الانتخابية</option>'); // إضافة الخيار
           
            res.data.forEach(e => {
                select.append(`<option value="${e.electionId}">${e.electionTitle}</option>`);
            });
        }
    });
}


function CheckIsMemberExists() {
    var enteredNationalId = $('#NationalId').val();

    if (!enteredNationalId) return; 

    $.get('/Members/GetAllMembers', function (res) {
        if (res && res.success) {
            // البحث عن العضو باستخدام رقم الهوية
            var foundMember = res.data.find(element => element.nationalId === enteredNationalId || element.NationalId === enteredNationalId);

            if (foundMember) {
                // التصحيح: حفظ الـ MemberId بشكل صحيح باستخدام element
                MemberId = foundMember.memberId !== undefined ? foundMember.memberId : foundMember.MemberId;
                console.log("Member Found, ID:", MemberId);
            } else {
                showToast("هذا العضو غير موجود", "warning");
                $('#NationalId').val('');
                MemberId = null;
            }
        }
    });
}



function SaveCandidate()
{
    var candidateData = {
        electionId : $('#ElectionSelect').val(),
        memberId : MemberId
    };
       
    if (!candidateData.electionId) {
        showToast("الرجاء اختيار العملية الانخابية", "error");
        return;
    }

    apiAdd(
     '/Candidates/AddCandidate',
     candidateData,
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