//1.Deklarasi data
const tmpUrlink = {
    LoadInit: "/Admin/UserManage",
    CreatePage: "UserManage/CreatePage",
    PostCreate: "/Admin/UserManage/AddUser",
    PostUpdate: "/Admin/UserManage/UpdateUser",
    PostDelete: "/Admin/UserManage/DeleteUser",
    StatusMenu: "/Admin/UserManage/StatusMenu",
    StatusView: "/Admin/UserManage/StatusView",
    StatusCreate: "/Admin/UserManage/StatusCreate",
    StatusUpdate: "/Admin/UserManage/StatusUpdate",
    StatusDelete: "/Admin/UserManage/StatusDelete",
    ResetAccess: "/Admin/UserManage/ResetAccess",
};
var data = {}

//2.Validasi
function getValueOnForm() {
    data.RoleID = $('#Role').val();
    data.UserName = $('#NamaUser').val();
    data.Password = $('#Password').val();
    data.Email = $('#Email').val();
    data.NoPegawai = $('#NipUser').val();
}
function clearValueOnForm() {
    $("#Role").val('');
    $("#NamaUser").val('');
    $("#Email").val('');
    $("#NipUser").val('');
}
function CreateUser() {
    window.location.href = tmpUrlink.CreatePage;
}

//validasi email
const emailInput = document.getElementById('Email');
const emailError = document.getElementById('email-error');
const emailSuccess = document.getElementById('email-success');
var ValidateEmail;
if (emailInput != null) {
    emailInput.addEventListener('input', function () {
        if (!emailInput.checkValidity()) {
            emailError.style.display = 'block';
            emailSuccess.style.display = 'none';
            ValidateEmail = false;
        } else {
            emailError.style.display = 'none';
            emailSuccess.style.display = 'block';
            ValidateEmail = true;
        }
    });
}


//validasi Password
const passwordInput = document.getElementById('Password');
const passwordError = document.getElementById('password-error');
const passwordSuccess = document.getElementById('password-success');
var ValidatePassword;
if (passwordInput != null) {
    passwordInput.addEventListener('input', function () {
        const password = passwordInput.value;
        const validationMessage = validatePassword(password);

        if (validationMessage) {
            if (validationMessage != 'Password sesuai.') {
                passwordError.textContent = validationMessage;
                passwordError.style.display = 'block';
                passwordSuccess.style.display = 'none';
                ValidatePassword = false;
            } else {
                passwordSuccess.textContent = validationMessage;
                passwordError.style.display = 'none';
                passwordSuccess.style.display = 'block';
                ValidatePassword = true;
            }
        } else {
            passwordError.style.display = 'none';
            passwordSuccess.style.display = 'block';
        }
    });
}

function validatePassword(password) {
    const minLength = 8; // Minimum length
    const hasUpperCase = /[A-Z]/.test(password); // At least one uppercase letter
    const hasLowerCase = /[a-z]/.test(password); // At least one lowercase letter
    const hasNumber = /[0-9]/.test(password); // At least one digit
    const hasSpecialChar = /[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/.test(password); // At least one special character

    // Check if password meets all criteria
    if (password.length < minLength) {
        return 'Panjang password minimal harus 8 karakter.';
    } else if (!hasUpperCase) {
        return 'Password harus berisi setidaknya satu huruf besar.';
    } else if (!hasLowerCase) {
        return 'Password harus berisi setidaknya satu huruf kecil.';
    } else if (!hasNumber) {
        return 'Password harus mengandung setidaknya satu digit.';
    } else if (!hasSpecialChar) {
        return 'Password harus mengandung setidaknya satu karakter khusus.';
    }
    // Password is valid
    return 'Password sesuai.';
}


function PostCreate() {
    PostDataAjax(data, tmpUrlink.PostCreate, tmpUrlink.LoadInit);
}
function Delete(id) {
    DeleteDataAjaxJson(id, tmpUrlink.PostDelete, tmpUrlink.LoadInit);
}
//function PostUpdate() {
//    dUser = {}
//    getValueOnForm();
//    dUser.ID = $('#id_User').val();
//    var base_url = window.location.origin;
//    $.ajax({
//        url: base_url + tmpUrlink.PostUpdate,
//        type: 'post',
//        datatype: 'json',
//        data: JSON.stringify(dUser),
//        cache: false,
//        contentType: false,
//        processData: false,
//        contentType: 'application/json',
//    }).then(function (response) {
//        if (response.status == 500) {
//            document.getElementById('alertText').innerText = response.message;
//            $('#danger-alert').removeClass('d-none')
//            $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
//                $("#danger-alert").slideUp(500);
//            });
//            table.ajax.reload(null, false);
//        } else
//            if (response.status == 200) {
//                Swal.fire({
//                    title: 'Success',
//                    icon: 'success',
//                    html: response.message,
//                    showCloseButton: true,
//                    showCancelButton: false,
//                    focusConfirm: false,
//                    confirmButtonText: 'OK'
//                }).then((result) => {
//                    if (result.isConfirmed) {
//                        clearValueOnForm();
//                        history.back();
//                        $('#table-user').DataTable().ajax.reload();
//                    }
//                })
//            } else if (response.status == 400) {
//                document.getElementById('alertText').innerText = response.message;
//                $('#danger-alert').removeClass('d-none')
//                $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
//                    $("#danger-alert").slideUp(500);
//                });
//            } 
//    });
//}

//function Delete(id) {
//    Swal.fire({
//        title: "Apakah anda yakin?",
//        text: "warning",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#3c7a6b",
//        confirmButtonText: "Yes, delete it!",
//        closeOnConfirm: false
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: tmpUrlink.PostDelete,
//                type: "POST",
//                data: { id: id }
//                ,
//                dataType: "json",
//                success: function () {
//                    Swal.fire({
//                        title: 'Success',
//                        icon: 'success',
//                        html: 'Data Berhasil Terhapus',
//                        showCloseButton: true,
//                        showCancelButton: false,
//                        focusConfirm: false,
//                        confirmButtonText: 'OK'
//                    })
//                    tableMenuUser.ajax.reload(null, false);
//                }
//            });
//        } else if (result.isDenied) { }
//    })
//}

//function statusMenu(RoleID, UserID, MenuID, tmpStat) {

//    if (tmpStat == "Active") {
//        StatusMenu = false;
//    } else if (tmpStat == "NonActive") {
//        StatusMenu = true;
//    }
//    var dStatus = {
//        RoleID: RoleID,
//        UserID: UserID,
//        MenuID: MenuID,
//        Status: StatusMenu
//    }
//    var base_url = window.location.origin;
//    $.ajax({
//        url: base_url + tmpUrlink.StatusMenu,
//        type: 'post',
//        datatype: 'json',
//        contentType: 'application/json',
//        data: JSON.stringify(dStatus),
//    })
//        .then(function (response) {
//            if (response.status == 500) {
//                Swal.fire({
//                    title: 'Failed!',
//                    icon: 'error',
//                    html: 'Status Menu Failed to Updated!',
//                    showCloseButton: true,
//                    showCancelButton: false,
//                    focusConfirm: false,
//                    confirmButtonText: 'OK'
//                })
//                window.location.reload(true);
//            } else
//                if (response.status == 200) {
//                    Swal.fire({
//                        title: 'Success',
//                        icon: 'success',
//                        html: 'Status Menu Updated successfully! ',
//                        showCloseButton: true,
//                        showCancelButton: false,
//                        focusConfirm: false,
//                        confirmButtonText: 'OK'
//                    }).then((result) => {
//                        if (result.isConfirmed) {
//                            window.location.reload(true);
//                            return false;
//                        }
//                    })
//                }

//        });
//}

//function statusCRUD(RoleID,UserID, MenuID, tmpStat, type) {
//    //console.log(RoleID, MenuID, tmpStat,type)
    
//    if (tmpStat == "Active") {
//        StatusMenu = false;
//    } else if (tmpStat == "NonActive") {
//        StatusMenu = true;
//    }
//    var dStatus = {
//        RoleID: RoleID,
//        UserID: UserID,
//        MenuID: MenuID,
//        Status: StatusMenu
//    }
//    var base_url = window.location.origin;
//    if (type == 'View') {
//        UrlLink = tmpUrlink.StatusView;
//    } else if (type == 'Create') {
//        UrlLink = tmpUrlink.StatusCreate;
//    } else if (type == 'Update') {
//        UrlLink = tmpUrlink.StatusUpdate;
//    } else if (type == 'Delete') {
//        UrlLink = tmpUrlink.StatusDelete;
//    }
//    $.ajax({
//        url: base_url + UrlLink,
//        type: 'post',
//        datatype: 'json',
//        contentType: 'application/json',
//        data: JSON.stringify(dStatus),
//    })
//        .then(function (response) {
//            if (response.status == 500) {
//                $('#danger-alert').removeClass('d-none')
//                //$("#danger-alert").fadeTo(1000, 1000).slideUp(500, function () {
//                //    $("#danger-alert").slideUp(1000);
//                //});
//            } else
//                if (response.status == 200) {
//                    $('#success-alert').removeClass('d-none')
//                    //$("#success-alert").fadeTo(1000, 1000).slideUp(500, function () {
//                    //    $("#success-alert").slideUp(1000);
//                    //});
//                }
//        });
//    /* console.log(RoleID, MenuID, tmpStat);*/
//}
//function ResetAccess(id) {
//    Swal.fire({
//        title: "Are you sure?",
//        text: "Reset Access Menu",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#3c7a6b",
//        confirmButtonText: "Yes",
//        closeOnConfirm: false
//    }).then((result) => {
//        if (result.isConfirmed) {
//            var base_url = window.location.origin;
//            $.ajax({
//                url: base_url + tmpUrlink.ResetAccess,
//                type: "POST",
//                data: { id: id },
//                dataType: "json",
//            }).then(function (response) {
//                if (response.status == 200) {
//                    Swal.fire({
//                        title: 'Success',
//                        icon: 'success',
//                        html: response.message,
//                        showCloseButton: true,
//                        showCancelButton: false,
//                        focusConfirm: false,
//                        confirmButtonText: 'OK'
//                    }).then((result) => {
//                        if (result.isConfirmed) {
//                            window.location.reload(true);
//                            return false;
//                        }
//                    })
//                }
//                else if (response.status == 500) {
//                    Swal.fire({
//                        title: 'Oppss',
//                        icon: 'error',
//                        html: response.message,
//                        showCloseButton: true,
//                        showCancelButton: false,
//                        focusConfirm: false,
//                        confirmButtonText: 'OK'
//                    })
//                }
//                })
//        } else if (result.isDenied) { }
//    })
//}

$(document).ready(function () {
    $("#Nama").select2({
        placeholder: "-- Pilih Karyawan --",
        width: "70%"
    });
    $("#Role").select2({
        placeholder: "-- Pilih Role --",
        width: "50%"
    });
})

