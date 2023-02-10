// show loading
function LoadStart() {
    //Khi Show Image Load
    $('.form-img').show();
    //$('#loading').show();
}
// hide loading
function LoadStop() {
    //Khi Hide Image Load
    $('.form-img').hide();
    //$('#loading').hide();
}

//$(document).ready(function () {
//    $(document).ajaxStart(function () {
//        $("#loading").show();
//    });
//    $(document).ajaxStop(function () {
//        $("#loading").hide();
//    });
//});

 ////Default
//swal("Thông báo!", Dob);


//----Button
//swal("Good job!", "You clicked the button!", "success"); -- "warning" , "error"

//swal({
//    title: "Good job!",
//    text: "You clicked the button!",
//    icon: "success",
//    button: "Aww yiss!",
//});

//Comfirm

//swal({
//    title: "Are you sure?",
//    text: "Once deleted, you will not be able to recover this imaginary file!",
//    icon: "warning",
//    buttons: true,
//    dangerMode: true,
//})
//    .then((willDelete) => {
//        if (willDelete) {
//            swal("Poof! Your imaginary file has been deleted!", {
//                icon: "success",
//            });
//        } else {
//            swal("Your imaginary file is safe!");
//        }
//    });
