$(document).ready(function ()
{
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $('#dataTable').DataTable({
        "language": {
            "paginate": {
                "previous": "<<",
                "next": ">>"
            }
        },
    });
    $('#dataTableTwo').DataTable({
        "language": {
            "paginate": {
                "previous": "<<",
                "next": ">>"
            }
        },
    });
    if ($('#isCreate').val() == "False") {
        $('.btn-create').addClass('d-none')
    }


})
function validationCustom()
{
    var isValid;
    $(".input-data").each(function () {
                var element = $(this);
        if (element.val() == "" || element.val() == undefined || element.val() == null) {
                    return isValid = false;
                } else {
                    return isValid = true;
                }
            });
    return isValid;
}
$('#table-role-access').DataTable({
    "language": {
        "paginate": {
            "previous": "<<",
            "next": ">>"
        }
    },
});
function PostDataAjax(datapost, urlpath, urlload) {
    dTemp = datapost;
    getValueOnForm();
    if (validationCustom()) {
        ShowLoading();
        var base_url = window.location.origin;
        $.ajax({
            url: base_url + urlpath,
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dTemp),
            cache: false,
            contentType: false,
            processData: false,
            contentType: 'application/json',
        }).then(function (response) {
            if (response.status == 500) {
                $.LoadingOverlay("hide");  
                document.getElementById('alertText').innerText = response.message;
                $('#danger-alert').removeClass('d-none')
                $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#danger-alert").slideUp(500);
                });
            } else if (response.status == 200) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: response.message,
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK',
                    confirmButtonColor: '#3c7a6b',
                }).then((result) => {
                    if (result.isConfirmed) {
                        clearValueOnForm();
                        window.location.replace(urlload);
                    }
                })
                $.LoadingOverlay("hide");
            } else if (response.status == 400) {
                $.LoadingOverlay("hide");
                document.getElementById('alertText').innerText = response.message;
                $('#danger-alert').removeClass('d-none')
                $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                $("#danger-alert").slideUp(500);
                });
            }
        })
    } else {
        $.LoadingOverlay("hide");  
        document.getElementById('alertText').innerText = "Ada beberapa inputan (*) yang masih kosong!";
        $('#danger-alert').removeClass('d-none')
        $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#danger-alert").slideUp(500);
        });
    }
  
    
}
function UpdateDataAjax(id, datapost, urlpath, urlload) {
    dTemp = datapost;
    getValueOnForm();
    dTemp.ID = id;
    var base_url = window.location.origin;
    if (validationCustom()) {
        ShowLoading();
        $.ajax({
            url: base_url + urlpath,
            type: 'post',
            datatype: 'json',
            data: JSON.stringify(dTemp),
            cache: false,
            contentType: false,
            processData: false,
            contentType: 'application/json',
        }).then(function (response) {
            if (response.status == 500) {
                document.getElementById('alertText').innerText = response.message;
                $('#danger-alert').removeClass('d-none')
                $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#danger-alert").slideUp(500);
                });
                table.ajax.reload(null, false);
            } else if (response.status == 200) {
                Swal.fire({
                    title: 'Berhasil',
                    icon: 'success',
                    html: response.message,
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: false,
                    confirmButtonText: 'OK',
                    confirmButtonColor: '#3c7a6b',
                }).then((result) => {
                    if (result.isConfirmed) {
                        clearValueOnForm();
                        window.location.replace(urlload);
                    }
                })
                $.LoadingOverlay("hide");
            } else if (response.status == 400) {
                $.LoadingOverlay("hide");
                document.getElementById('alertText').innerText = response.message;
                $('#danger-alert').removeClass('d-none')
                $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#danger-alert").slideUp(500);
                });
            }
        });
           
            
    } else {
        document.getElementById('alertText').innerText = "Ada beberapa inputan (*) yang masih kosong!";
        $('#danger-alert').removeClass('d-none')
        $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#danger-alert").slideUp(500);
        });
    }
   
   
   
}
function DeleteDataAjaxJson(id, urlpath, urlload) {
    Swal.fire({
        title: "Anda Yakin?",
        type: "question",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: "#3c7a6b",
        confirmButtonText: "Ya",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            ShowLoading();
            $.ajax({
                url:urlpath,
                type: 'post',
                datatype: 'json',
                data: { id: id },
            }).then(function (response) {
                if (response.status == 500) {
                    Swal.fire({
                        title: 'Error',
                        icon: 'error',
                        html: response.message,
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK',
                        confirmButtonColor: '#3c7a6b',
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.replace(urlload);
                        }
                    })
                } else if (response.status == 200) {
                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: response.message,
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK',
                        confirmButtonColor: '#3c7a6b',
                    }).then((result) => {
                        if (result.isConfirmed) {
                            clearValueOnForm();
                            window.location.replace(urlload);
                        }
                    })
                    $.LoadingOverlay("hide");
                } 
            });
        } else if (result.isDenied) { }
    })
        $.LoadingOverlay("hide");
   
}
function DeleteDataAjax(id, urlpath, urlload) {
    Swal.fire({
        title: "Anda Yakin?",
        type: "question",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: "#3c7a6b",
        confirmButtonText: "Ya",
        closeOnConfirm: false
    }).then((result) => {
        if (result.isConfirmed) {
            ShowLoading();
            $.ajax({
                url: urlpath,
                type: "POST",
                data: { id: id }
                ,
                dataType: "json",
                success: function () {
                    Swal.fire({
                        title: 'Berhasil',
                        icon: 'success',
                        html: 'Data Berhasil Dihapus!',
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                        confirmButtonText: 'OK',
                        confirmButtonColor: '#3c7a6b',
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.replace(urlload);
                        }
                    })
                },
            });
            $.LoadingOverlay("hide");
        } else if (result.isDenied) { }
    })
        $.LoadingOverlay("hide");
    
}

function ShowLoading() {
    $.LoadingOverlay("show");
}

function getDataAjax(urlpath, datapost, cachemode, setAjaxSuccess, shownotif = false) {
    ShowLoading()
    var base_url = window.location.origin;
    $.ajax({
        url: base_url + urlpath,
        dataType: "json",
        data: datapost,
        type: 'POST',
        cache: cachemode,
        success: function (data) {
            if (data != []) {
                shownotif = true
            }
            if ($.trim(data)) {
                setAjaxSuccess(data);
            } else if (shownotif) {
                Swal.fire({
                    icon: 'error',
                    text: 'Data tidak tersedia.',
                    showConfirmButton: false,
                    showCloseButton: true
                });
            }
            $.LoadingOverlay("hide");
        },
        error: function (xhr) {
            $.LoadingOverlay("hide");
            console.log(xhr);
            Swal.fire({
                icon: 'error',
                text: 'Data tidak dapat dibaca. Silakan Dicoba Kembali. Jika kesalahan terjadi terus menerus, hubungi Admin!',
                showConfirmButton: false,
                showCloseButton: true,
                showCancelButton: true,
                showDenyButton: true,
                cancelButtonText: 'Kembali',
            }).then((result) => {
                console.log("error");
            });
        },
    });
    setTimeout(function () {
        $.LoadingOverlay("hide");
    }, 1500);
}




