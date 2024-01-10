
const tmpUrlink = {
    LoadInit: "/Admin/RubrikKompetensi",
    PostCreateNilai: "/Admin/RubrikKompetensi/AddDataNilai",
    GetNilai: "/Admin/RubrikKompetensi/GetDataNilai",
};
var data = {}


function getData() {
    dataPost = {
        Id: $('#nID').val()
    }
    getDataAjax(tmpUrlink.GetNilai, dataPost, true, setData);
}
function setData(data) {
    $('.table-rubrikNilai').DataTable().destroy();
    var groupColumn = 0;
    var table = $('.table-rubrikNilai').DataTable({
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
                "data": "ID",
                "render": function (data, type, row, meta) {
                    return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a class="btn-update " href="javascript:void(0)" onclick="javascript:$('#idKomp').val('${row.KompetensiID}');$('#idRubrik').val('${data}');$('#PostRubrikNilaiKompetensi').submit();" rel="tooltip" title="Nilai Rubrik" ><i class="fa fa-pencil-square-o" aria-hidden="true"style="font-size:20px"></i></a>
                            </div>
                        </div>`;
                }
            },
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
                "data": "Bobot",
                "name": "Bobot",
                "render": function (data, type, row, meta) {
                    var cekData = ''
                    if (data == 0) {
                        cekData == "-"
                    } else {
                        cekData = data
                    }
                    return '<div class="center">' + cekData + '</div>';
                }
            },
            {
                "data": "Nilai0",
                "name": "Nilai0",
                "render": function (data, type, row, meta) {
                    return '<div class="center"> <span class="badge badge-info mr-1">' + data + '</span><span class="badge badge-secondary mr-1">' + row.Nilai1 + '</span> <span class="badge badge-warning mr-1">' + row.Nilai2 + '</span><span class="badge badge-success mr-1">' + row.Nilai3 + '</span><span class="badge badge-danger mr-1">' + row.Nilai4 + '</span><span class="badge badge-dark mr-1">' + row.Nilai5 + '</span></div>';
            }
            },
           
        ],
        "initComplete": function (d) {
            var notApplyFilterOnColumn = [0, 1,3];
            var inputFilterOnColumn = [3];
            var showFilterBox = 'afterHeading'; //beforeHeading, afterHeading
            $('.gtp-dt-filter-row').remove();
            var theadSecondRow = '<tr class="gtp-dt-filter-row">';
            $(this).find('thead tr th').each(function (index, result) {
                theadSecondRow += '<td class="gtp-dt-select-filter-' + index + ' bg-light"></td>';
            });
            theadSecondRow += '</tr>';

            if (showFilterBox === 'beforeHeading') {
                $(this).find('thead').prepend(theadSecondRow);
            } else if (showFilterBox === 'afterHeading') {
                $(theadSecondRow).insertAfter($(this).find('thead tr'));
            }
            this.api().columns().every(function (index) {
                var column = this;

                if (inputFilterOnColumn.indexOf(index) >= 0 && notApplyFilterOnColumn.indexOf(index) < 0) {
                    $('td.gtp-dt-select-filter-' + index).html('<input type="text" class="rounded gtp-dt-input-filter border border-secondary form-control">');
                    $('td input.gtp-dt-input-filter').on('keyup change clear', function () {
                        if (column.search() !== this.value) {
                            column
                                .search(this.value)
                                .draw();
                        }
                    });
                } else if (notApplyFilterOnColumn.indexOf(index) < 0) {

                    var select = $('<select class="form-control"><option value="">All</option></select>')
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });
                    $('td.gtp-dt-select-filter-' + index).html(select);
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                }
            });
        }
    })

}


//2.Validasi
function getValueOnForm() {
    data.Nilai0 = (CKEDITOR.instances.Nilai0.getData());
    data.Nilai1 = (CKEDITOR.instances.Nilai1.getData());
    data.Nilai2 = (CKEDITOR.instances.Nilai2.getData());
    data.Nilai3 = (CKEDITOR.instances.Nilai3.getData());
    data.Nilai4 = (CKEDITOR.instances.Nilai4.getData());
    data.Nilai5 = (CKEDITOR.instances.Nilai5.getData());
    data.Bobot = $('#Bobot').val();
    data.KompetensiID = $('#KompetensiID').val();
    data.RubrikKompetensiID = $('#RubrikKompetensiID').val();
    data.ID = $('#ID').val();

}
function clearValueOnForm() {
    $('#Nilai0').val('');
    $('#Nilai1').val('');
    $('#Nilai2').val('');
    $('#Nilai3').val('');
    $('#Nilai4').val('');
    $('#Nilai5').val('');
    $('#Bobot').val('');
    $('#KompetensiID').val('');
    $('#RubrikKompetensiID').val('');
    $('#ID').val('');
}
function CreateData() {
    window.location.href = tmpUrlink.CreatePage;
}
function PostCreate() {

    PostDataAjaxNilai(data, tmpUrlink.PostCreateNilai, tmpUrlink.LoadInit);
}

function PostUpdate() {
    UpdateDataAjax($('#ID').val(), data, tmpUrlink.PostUpdate, tmpUrlink.LoadInit);
}
function Delete(id) {
    DeleteDataAjaxJson(id, tmpUrlink.PostDelete, tmpUrlink.LoadInit);
}

function validationCustomPostNilai() {
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
function notValid() {
    document.getElementById('alertText').innerText = "Ada beberapa inputan (*) yang masih kosong!";
    $('#danger-alert').removeClass('d-none')
    $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#danger-alert").slideUp(500);
    });
    return false;
}


function PostDataAjaxNilai(datapost, urlpath, urlload) {
    dTemp = datapost;
    getValueOnForm();
    if ($('#checkbox0').is(':checked') && datapost.Nilai0 == '') {
        notValid();
    } else if ($('#checkbox1').is(':checked') && datapost.Nilai1 == '') {
        notValid();
    } else if ($('#checkbox2').is(':checked') && datapost.Nilai2 == '') {
        notValid();
    } else if ($('#checkbox3').is(':checked') && datapost.Nilai3 == '') {
        notValid();
    } else if ($('#checkbox4').is(':checked') && datapost.Nilai4 == '') {
        notValid();
    } else if ($('#checkbox5').is(':checked') && datapost.Nilai5 == '') {
        notValid();
    }  else {
        if (datapost.Bobot != 0) {
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
                            $('#idKomp').val(dTemp.RubrikKompetensiID); $('#RubrikNilaiKompetensi').submit();
                            /*window.location.replace(urlload);*/
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
}


$("#checkbox0").on('change', function () {
    if ($(this).is(':checked')) {
        $('.textarea0').removeClass('d-none')
    } else {
        $('.textarea0').addClass('d-none')
        CKEDITOR.instances.Nilai0.setData('');
    }
});
$("#checkbox1").on('change', function () {
    if ($(this).is(':checked')) {
        $('.textarea1').removeClass('d-none')
    } else {
        $('.textarea1').addClass('d-none')
        CKEDITOR.instances.Nilai1.setData('');
    }
});
$("#checkbox2").on('change', function () {
    if ($(this).is(':checked')) {
        $('.textarea2').removeClass('d-none')
    } else {
        $('.textarea2').addClass('d-none')
        CKEDITOR.instances.Nilai2.setData('');
    }
});
$("#checkbox3").on('change', function () {
    if ($(this).is(':checked')) {
        $('.textarea3').removeClass('d-none')
    } else {
        $('.textarea3').addClass('d-none')
        CKEDITOR.instances.Nilai3.setData('');
    }
});
$("#checkbox4").on('change', function () {
    if ($(this).is(':checked')) {
        $('.textarea4').removeClass('d-none')
    } else {
        $('.textarea4').addClass('d-none')
        CKEDITOR.instances.Nilai4.setData('');
    }
});
$("#checkbox5").on('change', function () {
    if ($(this).is(':checked')) {
        $('.textarea5').removeClass('d-none')
    } else {
        $('.textarea5').addClass('d-none')
        CKEDITOR.instances.Nilai5.setData('');
    }
});