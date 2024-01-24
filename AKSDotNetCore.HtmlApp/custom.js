function successMessage(message) {
    // Swal.fire({
    //     title: "Success",
    //     text: message,
    //     icon: "success"
    // });

    Notiflix.Report.success(
        'Success',
        message,
        'Okay',
        );
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

    // return new Promise(function (myResolve, myReject){
    //     Notiflix.Confirm.show(
    //         'Confirm',
    //         message,
    //         'Yes',
    //         'No',
    //         function okCb() {
    //             myResolve(true);
    //         },
    //         function cancelCb() {
    //             myReject(false);
    //         },
    //         {
    //         },
    //     );
    // });
}
