// wwwroot\js\common.js
window.ShowToastr = (type, message) => {
    if (type === "success") {
        // Display a success, with a title
        toastr.success(message, 'Operation Successful');
    }
    if (type === "error") {
        // Display a success, with a title
        toastr.error(message, 'Operation Failed');
    }
}

window.ShowSweetAlert = (type, message) => {
    if (type === "success") {
        // Display a success, with a title
        Swal.fire('Operation Successful', message, 'success');
    }
    if (type === "error") {
        // Display a error, with a title
        Swal.fire('Operation Failed', message, 'error');
    }
}

//use Promise object to capture the response of the confirm dialog
window.CustomConfirmDialog = (title, message, type) => {
    return new Promise((resolve) => {
        Swal.fire({
            title: title,
            text: message,
            icon: type,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirm Operation'
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(true); //use promise object
            } else {
                resolve(false);
            }
        });
    });
}