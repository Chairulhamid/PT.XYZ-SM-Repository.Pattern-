
const tmpUrlink = {
    LoadInit: "/Admin/RubrikKompetensi",
    CreatePage: "RubrikKompetensi/CreatePage",
    PostCreate: "/Admin/RubrikKompetensi/AddData",
    PostUpdate: "/Admin/RubrikKompetensi/UpdateData",
    PostDelete: "/Admin/RubrikKompetensi/DeleteData",
    DetailKompetensi: "/Admin/RubrikKompetensi/DetailKompetensi",
    ModalPage: "/Admin/RubrikKompetensi/NilaiModal",
};
var data = {}
var tmpKompetensiName = [];
var obj = {
    Master: [data],
    detail: tmpKompetensiName,
    IdCopyRubrik: 0
}

//2.Validasi
function getValueOnForm() {
    data.TahunSemester = $('#TahunSemester').val();
    data.ACADCareer = $('#ACADCareer').val();
    data.KompetensiID = $('#Kompetensi').val();
    data.JudulStation = $('#JudulStation').val();
    data.AlokasiWaktu = $('#AlokasiWaktu').val();
    data.TingkatKemampuan = $('#TingkatKemampuan').val();
    data.Classification = (CKEDITOR.instances.Classification.getData());
    data.InstruksiPeserta = (CKEDITOR.instances.InstruksiPeserta.getData());
    data.InstruksiPenguji = (CKEDITOR.instances.InstruksiPenguji.getData());
}
function clearValueOnForm() {
    $("#TahunSemester").val('');
    $("#ACADCareer").val('');
    $("#Kompetensi").val('');
    $("#JudulStation").val('');
    $("#AlokasiWaktu").val('');
    $("#TingkatKemampuan").val('');
    $("#Classification").val('');
    $("#InstruksiPeserta").val('');
    $("#InstruksiPenguji").val('');
}
function CreateData() {
    window.location.href = tmpUrlink.CreatePage;
}
function PostCreate() {
    obj.detail = tmpKompetensiName
    if ($('#CpyRubrik').val() != null) {
        obj.IdCopyRubrik = $('#CpyRubrik').val();
    }
    if (obj.detail.length > 0) {
        PostDataAjaxRubrik(obj, tmpUrlink.PostCreate, tmpUrlink.LoadInit);
    } else {
        Swal.fire({
            title: `Kompetensi belum dipilih.`,
            icon: 'warning',
            confirmButtonColor: '#3c7a6b',
        });
        return false;
    }
    
}

function PostUpdate() {
    obj.detail = tmpKompetensiName
    if (obj.detail.length > 0) {
        UpdateDataAjaxRubrik($('#ID').val(), obj, tmpUrlink.PostUpdate, tmpUrlink.LoadInit);
    } else {
        Swal.fire({
            title: `Kompetensi belum dipilih.`,
            icon: 'warning',
            confirmButtonColor: '#3c7a6b',
        });
        return false;
    }
    
}
function Delete(id) {
    DeleteDataAjaxJson(id, tmpUrlink.PostDelete, tmpUrlink.LoadInit);
}
function PostDataAjaxRubrik(datapost, urlpath, urlload) {
    dTemp = datapost;
    getValueOnForm();
    if (validationCustom() && Object.keys(dTemp.detail).length > 0 && dTemp.Master.Classification != "" && dTemp.Master.InstruksiPeserta != "" && dTemp.Master.InstruksiPenguji != ""  ) {
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
            } else if (response.status == 400) {
                document.getElementById('alertText').innerText = response.message;
                $('#danger-alert').removeClass('d-none')
                $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                    $("#danger-alert").slideUp(500);
                });
            }
        })
    } else {
        document.getElementById('alertText').innerText = "Ada beberapa inputan (*) yang masih kosong!";
        $('#danger-alert').removeClass('d-none')
        $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#danger-alert").slideUp(500);
        });
    }
}
function UpdateDataAjaxRubrik(id, datapost, urlpath, urlload) {
    dTemp = datapost;
    getValueOnForm();
    dTemp.Master[0].ID = id;
    var base_url = window.location.origin;
    if (validationCustom() && Object.keys(dTemp.detail).length > 0 && dTemp.Master.Classification != "") {
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
            } else if (response.status == 400) {
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
function setData(data) {
    $.each(data, function (index, result) {
        tmpKompetensiName.push({
            ID: result.Nilai,
            Kompetensi: result.Nama,
        });
    });
    loadTableReq(tmpKompetensiName)
}

//Data Temp
$('#BtnAddKomp').click(function () {
    for (key in tmpKompetensiName) {
        if (tmpKompetensiName[key].ID == $('#Kompetensi').val()) {
            Swal.fire({
                title: 'Data sudah masuk kedalam list daftar kompetensi!',
                icon: 'warning',
                confirmButtonColor: '#3c7a6b',
            })
            return false
        }
    }  
    if ($('#Kompetensi').val() != null) {
        tmpKompetensiName.push({
            ID: $('#Kompetensi').val(),
            Kompetensi: $("#Kompetensi option:selected").text(),
        });
    }
    loadTableReq(tmpKompetensiName)
});
$('#Kompetensi').on('change', function () {
    $("#BtnAddKomp").prop("disabled", false);
})
var loadTableReq = function (data) {
    $('.table-temp').DataTable().destroy();
    var groupColumn = 0;
    var table = $('.table-temp').DataTable({
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "paging": false,
            "targets": 0
        }],
        "order": [[0, 'asc']],
        "aaSorting": [[0, "asc"]],
        "data": data,
        "columns": [
            {
                "data": null,
                "orderable": "false",
                "render": function (data, type, full, meta) {
                    num = meta.row + 1;
                    return '<strong class="center">' + num + '</strong>';
                }
            },
            {
                "data": "Kompetensi",
                "name": "Kompetensi",
                "render": function (data, type, row, meta) {
                    return '<div class="center">' + data + '</div>';
                }
            },
            {
                "data": "ID",
                "render": function (data, type, row, meta) {
                        return `<div class="row justify-content-center">
                            <div class="col btnndelete" style="text-align:center">
                                <a class="btn-delete" rel="tooltip" title="remove" onclick="Deletetmp('${data}')" class="table-action remove" href="javascript:void(0)" title="Delete"><i class="fa fa-trash-o" aria-hidden="true"style="font-size:20px"></i></a>
                            </div>
                        </div>`;
                }
            }
        ],
    })
}
function Deletetmp(data) {
    tmpKompetensiName = tmpKompetensiName.filter((kom) => {
        return kom.ID !== data
    })
    loadTableReq(tmpKompetensiName)
}
function CopyStation() {
    if ($('#CpyRubrik').val() != null) {
        getDataAjax(tmpUrlink.ModalPage, { Id: $('#CpyRubrik').val() }, true, setDataStation);
       
    } else {
        Swal.fire({
            title: `Judul belum dipilih.`,
            icon: 'warning',
            confirmButtonColor: '#3c7a6b',
        });
        return false;
    }
    //$("#JudulStation").prop("disabled", true);
    //$("#AlokasiWaktu").prop("disabled", true);
    //$("#TingkatKemampuan").prop("disabled", true);
    //$("#Kompetensi").prop("disabled", true);
    //CKEDITOR.instances.Classification.setReadOnly(true);
    //CKEDITOR.instances.InstruksiPeserta.setReadOnly(true);
    //CKEDITOR.instances.InstruksiPenguji.setReadOnly(true);
}
function setDataStation(data) {
    clearValueOnForm();
    tmpKompetensiName = [];
    $("#JudulStation").val(data.JudulStation);
    $("#AlokasiWaktu").val(data.AlokasiWaktu);
    $("#TingkatKemampuan").val(data.TingkatKemampuan);
    CKEDITOR.instances.Classification.setData($("#Classification").val(data.Classification));
    CKEDITOR.instances.InstruksiPeserta.setData($("#InstruksiPeserta").val(data.InstruksiPeserta));
    CKEDITOR.instances.InstruksiPenguji.setData($("#InstruksiPenguji").val(data.InstruksiPenguji));
    //$("#BtnAddKomp").hide();
    getDataAjax(tmpUrlink.DetailKompetensi, { Id: $('#CpyRubrik').val() }, true, setData);
    
}