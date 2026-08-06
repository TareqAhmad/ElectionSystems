$(document).ready(function() {
    // Your code here
     
    $('#showPassword').change(function(){
        if($(this).is(":checked")){
            $("#password").attr("type","text");
        }else{
            $("#password").attr("type","password");
        } 
    });

    $('#btnLogin').on('click', function(e) {
        e.preventDefault(); // منع الإرسال الافتراضي للنموذج  
        loginUser(); // استدعاء الدالة loginUser مباشرة عند الضغط على زر تسجيل الدخول
    }); 
});



function loginUser(){
   
    var userdata = {
        userName : $("#username").val(),
        password : $("#password").val(),
    }

    apiLogin(
        pathEndpoint = '/Supervisors/Login',
        userdata, 
        function(response) {   
           if (response.success) {
                showToast(response.message, 'success');
                setTimeout(function() {
                     window.location.href = '/Supervisors/Panel';
                }, 1000);
          } else {
        
             $("#MsgError").removeClass("d-none").text(response.message);
             $("#MsgError").text(response.message);
          }
       }, 
        function(error) {
             $("#MsgError").removeClass("d-none").text("حدث خطأ أثناء محاولة تسجيل الدخول. ");
        }
    );
}