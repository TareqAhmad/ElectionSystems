function showToast(msg, iconType = 'success') {
    const Toast = Swal.mixin({
        toast: true,
        position: 'bottom-start', // المكان: أعلى اليمين
        showConfirmButton: false,
        timer: 5000, // يختفي بعد 3 ثوانٍ
        timerProgressBar: true,
        background:'#333',
        color:'#fff',


        customClass: {
            title: 'my-toast-title' 
        }
    });

    Toast.fire({
        icon: iconType, // 'success', 'error', 'warning', 'info'
        title: msg
    });
}



//------------------------------------------------------------------------



function apiLogin(pathEndpoint,userData, onSuccess, onError) {
  
    $.ajax({
        url: pathEndpoint,
        method: "POST", //      
        contentType: "application/json; charset=utf-8",
        dataType: "json", //      
        data: JSON.stringify(userData),
        success: function(data) {
            if (onSuccess) onSuccess(data);
        },
        error: function(xhr, status, error) {
            //          
            const errorMessage = xhr.responseJSON ? xhr.responseJSON.message : error;
            if (onError) onError(errorMessage);
        }
    });
}


function apiRetrieve(pathEndpoint, userData, onSuccess, onError) {
  
      var ajaxConfig = {
         url: pathEndpoint,
         method: "GET", //      
         contentType: "application/json; charset=utf-8",
         dataType: "json", 
        success: function(data) {
            if (onSuccess) onSuccess(data);
        },
        error: function(xhr, status, error) {
            //          
            const errorMessage = xhr.responseJSON ? xhr.responseJSON.message : error;
            if (onError) onError(errorMessage);
        }
      }; 
      if(userData){
        ajaxConfig.data = JSON.stringify(userData)
      }


    $.ajax(ajaxConfig);
}


function apiAdd(pathEndpoint, AddData, onSuccess, onError) {
  
      var ajaxConfig = {
         url: pathEndpoint,
         method: "POST", //      
         contentType: "application/json; charset=utf-8",
         dataType: "json", 
         data:JSON.stringify(AddData),
        success: function(data) {
            if (onSuccess) onSuccess(data);
        },
        error: function(xhr, status, error) {
            //          
            const errorMessage = xhr.responseJSON ? xhr.responseJSON.message : error;
            if (onError) onError(errorMessage);
        }
      }; 

    $.ajax(ajaxConfig);
}
