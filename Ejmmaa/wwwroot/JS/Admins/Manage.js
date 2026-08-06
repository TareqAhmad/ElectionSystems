$(document).ready(function () {
   
   initializePage();

});

    

// --- 2. Initialization ---
function initializePage() {
    loadClanSections()
    loadClanMembers();
}


function loadClanSections()
{
    
    apiRetrieve(
      '/Admins/GetClanSections',
      null,
      function(response) {   
           if (response.success) {
                var TableSection = $("#ClanSectionsBody"); 
                TableSection.empty(); 
                response.data.forEach(cs =>{
                    TableSection.append(`
                        <tr>
                            <td><input type="checkbox" class="section-filter" value="${cs.sectionId}" id="Select-Section"></td>
                            <td> ${cs.sectionName}</td>
                            <td> 5 </td>
                            <td>
                                <button class="btn btn-sm btn-warning" onclick="editSection(${cs.sectionId})">تعديل</button>
                                <button class="btn btn-sm btn-danger"  onclick="deleteSection(${cs.sectionId})">حذف</button>
                            </td>
                        </tr>`); 
                });
          } else {
             showToast(response.message,'error');
          }
       }, 
        function(error) {
             $("#MsgError").removeClass("d-none").text("حدث خطأ أثناء محاولة تسجيل الدخول. ");
        }
    ); 
}


function loadClanMembers() {
   
    apiRetrieve(
         '/Admins/GetClanMembers',
         null,
         function(response) {   
            if (response.success) {
                    var TableSection = $("#membersTableBody"); 
                    TableSection.empty(); 
                    response.data.forEach(cm =>{
                        TableSection.append(`
                            <tr>
                                <td> ${cm.fullName}</td>
                                <td> ${cm.nationalId}</td>
                                <td> ${cm.phoneNumber}</td>
                                <td> ${cm.birthDate}</td>
                                <td> ${cm.gender == 'M' ? 'ذكر' : 'أنثى'}</td>
                                <td>
                                    <button class="btn btn-sm btn-warning" onclick="editMember(${cm.memberId})">تعديل</button>
                                    <button class="btn btn-sm btn-danger"  onclick="deleteMember(${cm.memberId})">حذف</button>
                                </td>
                            </tr>`); 
                    });
            } else {
                showToast(response.message,'error');
            }
            }, 
            function(error) {
                $("#MsgError").removeClass("d-none").text("حدث خطأ أثناء محاولة تسجيل الدخول. ");
            }
    ); 

}

