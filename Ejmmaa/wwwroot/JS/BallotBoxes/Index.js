$(document).ready(function() {
    // Your code here

    $('#editBtn').on('click', function(e) {
        e.preventDefault(); // Prevent the default link behavior

        showToast('لا يوجد صلاحية', 'warning'); // Show the toast notification
    }); 

    $('#deleteBtn').on('click', function(e) {
        e.preventDefault(); // Prevent the default link behavior

        showToast('لا يوجد صلاحية', 'warning'); // Show the toast notification
    }); 

});
