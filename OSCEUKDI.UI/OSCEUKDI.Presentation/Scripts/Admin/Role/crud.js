//------CRUD Form------
//##. Deklarasi data
//##. validasi inputan
//##. function Create
//##. function postCreate
//##. Function Update
//##. function PostUpdate
//##. function Delete

//1.Deklarasi data
const tmpUrlink = {
    Load: "/Admin/Role",
    CreatePage: "Role/CreatePage",
    PostCreate: "/Admin/Role/AddRole",
    PostUpdate: "/Admin/Role/UpdateRole",
    PostDelete: "/Admin/Role/DeleteRole",
    StatusMenu: "/Admin/Role/StatusMenu",
    StatusView: "/Admin/Role/StatusView",
    StatusCreate: "/Admin/Role/StatusCreate",
    StatusUpdate: "/Admin/Role/StatusUpdate",
    StatusDelete: "/Admin/Role/StatusDelete",
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.Code = $('#Code').val();
    data.RoleName = $('#RoleName').val();
}
function clearValueOnForm() {
    $("#Code").val('');
    $("#RoleName").val('');
}
function CreateRole() {
    window.location.href = tmpUrlink.CreatePage;
}
function PostCreate() {
    PostDataAjax(data,tmpUrlink.PostCreate,tmpUrlink.Load);
}

function PostUpdate() {
    UpdateDataAjax($('#id_Role').val(),data, tmpUrlink.PostUpdate, tmpUrlink.Load);
}
function Delete(id) {
    DeleteDataAjax(id,  tmpUrlink.PostDelete, tmpUrlink.Load);
}
function statusMenu(RoleID, MenuID, tmpStat) {
    if (tmpStat == "Active") {
        StatusMenu = false;
    } else if (tmpStat == "NonActive") {  
        StatusMenu = true;
    }
    var dStatus = {
        RoleID: RoleID,
        MenuID: MenuID,
        Status: StatusMenu
    }
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + tmpUrlink.StatusMenu,
        type: 'post',
        datatype: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dStatus),
    })
        .then(function (response) {
            if (response.status == 500) {
                Swal.fire({
                    title: 'Gagal!',
                    icon: 'error',
                    html: 'Status Menu Gagal DiUpdate',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK'
                })
                window.location.reload(true);
            } else
                if (response.status == 200) {
                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'Status Menu Berhasil Diupdate',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.reload(true);
                            return false;
                        }
                    })
                }
        });
}
function statusCRUD(RoleID, MenuID, tmpStat,type) {
   // console.log(RoleID, MenuID, tmpStat,type)
    if (tmpStat == "Active") {
        StatusMenu = false;
    } else if (tmpStat == "NonActive") {
        StatusMenu = true;
    }
    var dStatus = {
        RoleID: RoleID,
        MenuID: MenuID,
        Status: StatusMenu
    }
    var base_url = window.location.origin;
    if (type == 'View') {
        UrlLink = tmpUrlink.StatusView;
    } else if (type == 'Create') {
        UrlLink = tmpUrlink.StatusCreate;
    } else if (type == 'Update') {
        UrlLink = tmpUrlink.StatusUpdate;
    } else if (type == 'Delete') {
        UrlLink = tmpUrlink.StatusDelete;
    }
    $.ajax({
        url: base_url + UrlLink,
        type: 'post',
        datatype: 'json',
        contentType: 'application/json',
        data: JSON.stringify(dStatus),
    })
        .then(function (response) {
            if (response.status == 500) {
                $('#danger-alert').removeClass('d-none')
                $("#danger-alert").fadeTo(1000, 1000).slideUp(500, function () {
                    $("#danger-alert").slideUp(1000);
                });
            } else
                if (response.status == 200) {
                    $('#success-alert').removeClass('d-none')
                    $("#success-alert").fadeTo(1000, 1000).slideUp(500, function () {
                        $("#success-alert").slideUp(1000);
                    });
                }
        });
   /* console.log(RoleID, MenuID, tmpStat);*/
}