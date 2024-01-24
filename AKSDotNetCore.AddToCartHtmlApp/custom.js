function successMessage(message) {
    Swal.fire({
        position: "center",
        icon: "success",
        title: message,
        showConfirmButton: false,
        timer: 1500
    });

    // Notiflix.Report.success(
    //     'Success',
    //     message,
    //     'Okay',
    //     );
}

function confirmQuantityChange(product_id, type) {
    return new Promise(function (myResolve, myReject) {
        Swal.fire({
            title: `Confirm Quantity ${type.charAt(0).toUpperCase() + type.slice(1)}`,
            text: `Are you sure you want to ${type} the quantity?`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.value) {
                changeQuantityCart(product_id, type);
                Swal.fire(`${type.charAt(0).toUpperCase() + type.slice(1)}d!`, `Quantity has been ${type}ed.`, 'success');
            }
        });
    });
}