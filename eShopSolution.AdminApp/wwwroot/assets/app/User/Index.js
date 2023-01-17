//Js tại đây
function DeleteUser(id) {
    var name = ''; var id = '';
    $("button[name='delete']").each(function () {
        //name = $(this).attr('name'); // grab name of original
        id = $(this).attr('value'); // grab value of original
    });
    
    $.ajax({
        url: "/User/Delete",
        type: "POST",
        dataType: "json",
        async: false,
        data: { id: id },
        success: function (data) {
            if (data.length > 0) {
                $.notify(data.id, "success");
            }
            else {
                $.notify("Người dùng không tồn tại.", "error");
            }
        },
        error: function () {
            $.notify(id, "error");
        }
    });
}


function EditUser() {
    //Get id user
    var id = event.target.id;
    //call ajax
    $.ajax({
        url: "/User/EditTest",
        type: "Get",
        dataType: "json",
        async: false,
        data: { id: id },
        success: function (data) {
            if (data != null || data != undefined) {
                $.notify('Xin chào: ' + data.FullName, "success");
                window.location.href = '/user/Edit/' + data.id;
                
            }
            else {
                $.notify("Người dùng không tồn tại.", "error");
            }
        },
        error: function () {
            $.notify(id, "error");
        }
    });
}


