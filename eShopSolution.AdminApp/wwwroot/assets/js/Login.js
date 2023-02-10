const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener("click", () => {
    //Thêm mode
    container.classList.add("sign-up-mode");
    //Nếu là màn hình nhỏ thì giãn khung tương ứng
    const mediaQuery = matchMedia("screen and (max-width: 870px)");
    if (mediaQuery.matches)
        $('#container').css('min-height', '1200px');
});

sign_in_btn.addEventListener("click", () => {
    //Xóa mode
    container.classList.remove("sign-up-mode");
    //Nếu là màn hình nhỏ thì giãn khung tương ứng
    const mediaQuery = matchMedia("screen and (max-width: 870px)");
    if (mediaQuery.matches)
        $('#container').css('min-height', '800px');

});

$("#txtDate_Dob").kendoDatePicker({
    value: new Date('01/01/1990'),
    format: "dd/MM/yyyy"
}).closest("span.k-datepicker").width("100%");

$("#Register").click(function () {
    //Get data từ form
    var obj = {
        FullName: $("#txt_FullName").val(),
        PhoneNumber: $("#txt_PhoneNumber").val(),
        Dob: $("#txtDate_Dob").val(),
        Email: $("#txt_Email").val(),
        UserName: $("#txt_UserName").val(),
        Password: $("#txt_Password").val(),
        ComfirmPassword: $("#txt_ComfirmPassword").val()
    };
    //Call ajax
    $.ajax({
        url: "/Login/Register",
        type: "POST",
        dataType: "JSON",
        data: obj,
        beforeSend: LoadStart,
        complete: LoadStop,
        success: function (data) {
            if (data != undefined && data != null)
                if (data.isError == true) {
                    swal("Thông báo!", data.message, "warning");
                } else {
                    swal("Thông báo!", data.message, "success");
                    //Xóa class màn hình đăng kí
                    container.classList.remove("sign-up-mode");
                }
            else
                swal("Thông báo!", "Không có dữ liệu" , "error");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (XMLHttpRequest.readyState == 4) {
                swal("Thông báo!", "Có lỗi xảy ra, vui lòng kiểm tra lại!");
            }
            else if (XMLHttpRequest.readyState == 0) {
                swal("Thông báo!" , "Không có kết nối internet, vui lòng kiểm tra lại!");
            }
            else {
                swal("Thông báo!" , "Có lỗi xảy ra, vui lòng kiểm tra lại!");
            }
        }
    });
});