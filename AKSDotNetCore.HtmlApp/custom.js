function successMessage(message) {
    Swal.fire({
        title: "Success",
        text: message,
        icon: "success"
    });
}

function confirmMessage(message) {
    return new Promise(function (myResolve, myReject) {
        Swal.fire({
            title: "Confirm",
            text: message,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes"
        }).then((result) => {
            myResolve(result.isConfirmed);
        });
    });

    // return new Promise(function (myResolve, myReject) {
    //     Swal.fire({
    //         title: "Confirm",
    //         text: message,
    //         icon: "warning",
    //         showCancelButton: true,
    //         confirmButtonText: "Yes"
    //     }).then((result) => {
    //         myResolve(result.isConfirmed);
    //     });
    // });
}