window.ShowToastr = (type, message) => {
    if (type === "Success") {
        toastr.success(message, 'Success!', { timeOut: 5000 });
    }

    if (type === "Error") {
        toastr.error(message, 'Error!', { timeOut: 5000 });
    }

}

window.ShowSwal = (heading, message) => {
    Swal.fire(heading, message, 'info');
}