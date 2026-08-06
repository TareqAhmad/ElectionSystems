$(document).ready(function () {
    // Initialize the dashboard

    InitializeDashboard();
});


function InitializeDashboard() {
    // Fetch and display the total number of clans
     FetchTotalClans();

    // Fetch and display the total number of members
   // FetchTotalMembers();
 };


 function FetchTotalClans() {

   apiRetrieve(
         '/Tenants/GetAllTenantsIsActive',
         null,
         function(response) {   
            if (response.success) {
                    var TableSection = $("#TenantSubscriptionsBody"); 
                    TableSection.empty(); 
                    response.data.forEach(t =>{
                        TableSection.append(`
                            <tr>
                                <td> ${t.subscriptionID}</td>
                                <td> ${t.tenantName}</td>
                                <td> ${t.packageName}</td>
                                <td> ${t.price}</td>
                                <td> ${(t.startDate).toString('dd/MM/yyyy')}</td>
                                <td> ${(t.endDate).toString('dd/MM/yyyy')}</td>
                                <td> ${t.status}</td>
                                <td>
                                    <button class="btn btn-sm btn-warning" onclick="editMember(${t.subscriptionID})">تعديل</button>
                                    <button class="btn btn-sm btn-danger"  onclick="deleteMember(${t.subscriptionID})">حذف</button>
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