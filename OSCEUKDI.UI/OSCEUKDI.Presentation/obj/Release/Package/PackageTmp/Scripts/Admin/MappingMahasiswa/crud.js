
const tmpUrlink = {
    LoadInit: "/Admin/MappingMahasiswa",
    CreatePage: "MappingMahasiswa/CreatePage",
    PostCreate: "/Admin/MappingMahasiswa/AddData",
    PostUpdate: "/Admin/MappingMahasiswa/UpdateData",
    PostDelete: "/Admin/MappingMahasiswa/DeleteData",
    JadwalTest: "/Admin/MappingMahasiswa/GetJadwalTest",
    Mahasiswa: "/Admin/MappingMahasiswa/GetMahasiswa",
    TahunSemester: "/Admin/MappingMahasiswa/GetTahunAngkatan",
    DetailMahasiswa: "/Admin/MappingMahasiswa/DetailMahasiswa",
};
var data = {}
var NimMhs = [];
var obj = {
    Master: [data],
    detail: NimMhs
}
//2.Validasi
function getValueOnForm() {
    data.ACADCareer = $('#ACADCareer').val();
    data.TahunSemester = $('#TahunSemester').val();
    data.JadwalTestID = $('#JadwalTestID').val();
    data.AcadProg = $('#AcadProg').val();
    data.TahunAngkatanMhs = $('#TahunAngkatanMhs').val();
}
function clearValueOnForm() {
    $("#TahunSemester").val('');
    $("#ACADCareer").val('');
    $("#JadwalTestID").val('');
    $("#AcadProg").val('');
    $('#TahunAngkatanMhs').val('');
}
function CreateData() {
    window.location.href = tmpUrlink.CreatePage;
}
function PostCreate() {
    if (NimMhs.length == 0) {
        Swal.fire({
            title: 'Tidak ada mahasiswa yang dipilih!',
            icon: 'warning',
            confirmButtonColor: '#3c7a6b',

        })
    return false;
    }
    obj.detail = NimMhs;
    PostDataAjax(obj, tmpUrlink.PostCreate, tmpUrlink.LoadInit);
}

function PostUpdate() {
    if (NimMhs.length == 0) {
        Swal.fire({
            title: 'Tidak ada mahasiswa yang dipilih!',
            icon: 'warning',
            confirmButtonColor: '#3c7a6b',
        })
        return false;
    }
    obj.detail = NimMhs;
    obj.Master[0].ID = $('#ID').val();
    UpdateDataAjax($('#ID').val(), obj, tmpUrlink.PostUpdate, tmpUrlink.LoadInit);
}
function Delete(id) {
    DeleteDataAjaxJson(id, tmpUrlink.PostDelete, tmpUrlink.LoadInit);
}
$("#ACADCareer").change(function () {
    $("#TahunSemester").prop("disabled", false);
    $('#TahunSemester').append($('<option disabled selected></option>').attr('value', "").text("Tahun Semester"))
    $('#TahunAngkatanMhs').prop("disabled", true);
    $('#TahunAngkatanMhs').append($('<option disabled selected></option>').attr('value', "").text("Pilih Tahun Angkatan"))
    $("#JadwalTestID").prop("disabled", true);
    $("#JadwalTestID").val('');
    $('#AcadProg').prop("disabled", true);
    $("#AcadProg").val('');
    $('.table-responsive').hide();
    $('#nodata').removeClass('d-none')
});
$("#TahunSemester").change(function () {
    $("#JadwalTestID").prop("disabled", false);
    $("#AcadProg").prop("disabled", false);
    $('#TahunAngkatanMhs').prop("disabled", false)
    $('#TahunAngkatanMhs').append($('<option disabled selected></option>').attr('value', "").text("Pilih Tahun Angkatan"))
    $("#JadwalTestID").empty();
    $("#AcadProg").val('');
    getJadwalTest();
    NimMhs = [];
    $("input[name='id[]']").prop('checked', false);
    $('#lengthRecord').html(NimMhs.length);
    $('.table-responsive').hide();
    $('#nodata').removeClass('d-none')

});
function getJadwalTest() {
    dataPost = {
        TahunSemester: $('#TahunSemester').val(),
        ACADCareer: $('#ACADCareer').val(),
    }
    getDataAjax(tmpUrlink.JadwalTest, dataPost, true, setJadwalTest);
    setTahunAngkatan();
}
function getJadwalTestUpdate() {
    dataPost = {
        TahunSemester: $('#TahunSemester').val(),
        ACADCareer: $('#ACADCareer').val(),
    }
    getDataAjax(tmpUrlink.JadwalTest, dataPost, true, setJadwalTestUpdate);
    setTahunAngkatan();
}


$("#TahunAngkatanMhs").change(function () {
    getMhs();
});
$("#AcadProg").change(function () {
    $('#TahunAngkatanMhs').append($('<option disabled selected></option>').attr('value', "").text("Pilih Tahun Angkatan"))
    NimMhs = [];
    $("input[name='id[]']").prop('checked', false);
    $('#lengthRecord').html(NimMhs.length);
    $('.table-responsive').hide();
    $('#nodata').removeClass('d-none');
})
function getMhs() {
    dataTmp = {
        TahunSemester: $('#TahunAngkatanMhs').val()  ,
        ACADCareer: $('#ACADCareer').val() ,
        AcadProg: $('#AcadProg').val() ,
    }
    getDataAjaxMhs(tmpUrlink.Mahasiswa, dataTmp, true, setMhs);
}


function setJadwalTest(data) {
    $('#JadwalTestID').append($('<option disabled selected></option>').attr('value', "").text("Pilih Jadwal Test"))
    if (data != "Empty") {
        $.each(data, function (index, data) {
            $('#JadwalTestID').append($('<option></option>').attr('value', data.ID).text(data.NamaPeriode + " (" + moment(data.TanggalUjian).format("DD-MMM-YYYY") + ") " ));
        });
    }
}

function ShowTmpData() {
    $('#ModalTmp').modal({ backdrop: 'static', keyboard: false, show: true });

    return false;
}
function setTahunAngkatan() {
    $("#TahunAngkatanMhs").select2({
        width: "100%",
        ajax: {
            url: "/Admin/MappingMahasiswa/GetTahunAngkatan",
            dataType: 'json',
            method: "POST",
            delay: 250,
            cache: false,
            data: function (params) {
                return {
                    Search: params.term || "",
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) { return { id: item.Nilai, value: item.Nilai, text: item.Nama } })
                };
            },
        }
    });
}
function setMhs(data) {
        $('.table-temp').DataTable().destroy();
    if (data.length > 0) {
        $('.table-responsive').show();
        $('#nodata').addClass('d-none')
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
            "aLengthMenu": [[10, 25, 75, 100, 200, -1], [10, 25, 75, 100, 200,"All"]],iDisplayLength:-1,
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
                    "data": "ADMIT_YEAR",
                    "name": "ADMIT_YEAR",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    "data": "EmpID",
                    "name": "EmpID",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    "data": "CampusID",
                    "name": "CampusID",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    "data": "Name",
                    "name": "Name",
                    "render": function (data, type, row, meta) {
                        return '<div class="center">' + data + '</div>';
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        let obj = NimMhs.find(o => o.CampusID === String(row.CampusID));
                        if (obj == undefined) {
                            return $("<div />").append(
                                $("<input />").attr("type", "checkbox").attr("name", "id[]").val(row.CampusID + "|" + row.ADMIT_YEAR + "|" + row.Name).attr('onclick', `InOut("${row.CampusID}",${row.ADMIT_YEAR},"${row.Name}")`)
                            ).html();
                        } else {
                            return $("<div />").append(
                                $("<input />").attr("type", "checkbox").attr("name", "id[]").val(row.CampusID + "|" + row.ADMIT_YEAR + "|" + row.Name).attr('onclick', `InOut("${row.CampusID}","${row.ADMIT_YEAR}","${row.Name}")`).attr('checked', true)
                            ).html();
                        }
                    },
                    targets: 12,
                    orderable: false
                }
            ],
        })
    } else {
        Swal.fire({
            icon: 'error',
            text: 'Data tidak tersedia.',
            showConfirmButton: false,
            showCloseButton: true
        });
        $('.table-responsive').hide();
        $('#nodata').removeClass('d-none');
        return false;
    }
}
function InOut(dt1, dt2, dt3) {
  let obj = NimMhs.find(o => o.CampusID === String(dt1));
  if (obj == undefined) {
    NimMhs.push({
      CampusID: String(dt1),
      TahunAngkatan: String(dt2),
      Name: String(dt3),
    });
    $(this).prop('checked', true);
    loadTableReq(NimMhs);
    $('#lengthRecord').html(NimMhs.length)
  } else {
    NimMhs = NimMhs.filter((kom) => {
      return kom.CampusID !== String(dt1)
    })
    $(this).prop('checked', false);
    loadTableReq(NimMhs);
    $('#lengthRecord').html(NimMhs.length)
  }

  return false;
}
$("#btnSelectAll").click(function () {
  Swal.fire({
    title: "Anda Yakin?",
    icon: 'question',
    showCancelButton: true,
    confirmButtonColor: "#3c7a6b",
    cancelButtonColor: '#972232',
    confirmButtonText: "Ya"
  }).then((result) => {
    if (result.isConfirmed) {
      NimMhs = [];
      if ($("input[name='id[]']").length > 0)
        $("input[name='id[]']").each(function () {
          $(this).prop('checked', true);

        });
      $("input[name='id[]']:checked").each(function () {
        var arrCB = $(this).val().split('|');
        NimMhs.push({
          CampusID: arrCB[0],
          TahunAngkatan: arrCB[1],
          Name: arrCB[2],
        });
      });
      loadTableReq(NimMhs);
      $('#lengthRecord').html(NimMhs.length);
    }
  })
  return false;
});
$("#btnDeselectAll").click(function () {
    NimMhs = [];
    $('#lengthRecord').html(NimMhs.length);
    $("input[name='id[]']").prop('checked', false);
    loadTableReq(NimMhs);
    $('#lengthRecord').html(NimMhs.length)

    return false;
});
var loadTableReq = function (data) {
  $('.table-tempDetail').DataTable().destroy();
  var groupColumn = 0;
  var table = $('.table-tempDetail').DataTable({
    "columnDefs": [{
      "searchable": false,
      "orderable": false,
      "paging": false,
      "targets": 0
    }],
      "aLengthMenu": [[10, 25, 75, 100, 200, -1], [10, 25, 75, 100, 200, "All"]], iDisplayLength: -1,
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
        "data": "TahunAngkatan",
        "name": "TahunAngkatan",
        "render": function (data, type, row, meta) {
          return '<div class="center">' + data + '</div>';
        }
      },
      {
        "data": "CampusID",
        "name": "CampusID",
        "render": function (data, type, row, meta) {
          return '<div class="center">' + data + '</div>';
        }
      },
      {
        "data": "Name",
        "name": "Name",
        "render": function (data, type, row, meta) {
          return '<div class="center">' + data + '</div>';
        }
      },
    ],
  })
}


function getDataAjaxMhs(urlpath, datapost, cachemode, setAjaxSuccess, shownotif = false) {
  $.LoadingOverlay("show");
  var base_url = window.location.origin;
  $.ajax({
    url: base_url + urlpath,
    dataType: "json",
    data: datapost,
    type: 'POST',
    cache: cachemode,
    success: function (data) {
      setAjaxSuccess(data);
    },
    error: function (xhr) {
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