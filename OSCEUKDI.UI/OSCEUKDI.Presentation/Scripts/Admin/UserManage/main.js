var tableMenuUser = $('.table-user').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0
    }],
    "order": [[0, 'asc']],
    "aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/UserManage/GetAllUser',
        type: 'POST',
        dataSrc: ''
    },
    "language": {
        "emptyTable": "No record found.",
        "processing":
            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
        "search": "Search:",
        "searchPlaceholder": "",
        "paginate": {
            "previous": "<<",
            "next": ">>"
        }
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                num = meta.row + meta.settings._iDisplayStart + 1;
                return '<strong class="center">' + num + '</strong>';
            }
        },
        {
            "data": "UserName",
            "name": "UserName",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Email",
            "name": "Email",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "RoleName",
            "name": "RoleName",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Status",
            "name": "Status",
            "render": function (data, type, row, meta) {
                if (data === true) {
                    return '<div class="center">Aktif</div>';
                } else {
                    return '<div class="center">Tidak Aktif</div>';
                }

            }
        },
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                if ($('#isUpdate').val() == "False") {
                    $('.btn-update').addClass('d-none')
                }
                if ($('#isDelete').val() == "False") {
                    $('.btn-delete').addClass('d-none')
                }
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">        
                                <a class="btn-delete" rel="tooltip" title="remove" onclick="Delete('${data}')" class="table-action remove" href="javascript:void(0)" title="Delete"><i class="fa fa-trash-o" aria-hidden="true"style="font-size:20px"></i></a>
                            </div>
                        </div>`;
            }
        }
    ],
                               /* <a class="btn-update" href="javascript:void(0)" onclick="javascript:$('#id').val('${data}');$('#UpdateRole').submit();" rel="tooltip" title="Edit" ><i class="fa fa-pencil-square-o" aria-hidden="true"style="font-size:20px"></i></a>*/
    "initComplete": function (d) {
        var notApplyFilterOnColumn = [0, 5];
        var inputFilterOnColumn = [];
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
});


var tableUserPopup = $('#table-user-popup').DataTable({

    "proccessing": true,
    "serverSide": true,
    "stateSave": true,
    //"order": [[1, 'asc']],
    //"aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/UserManage/GetAllUserPopup',
        type: 'POST'
        //dataSrc: 'data'
    },
    "language": {
        "emptyTable": "No record found.",
        "processing":
            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
        "search": "Search:",
        "searchPlaceholder": "",
        "paginate": {
            "previous": "<<",
            "next": ">>"
        }
    },
    "columns": [
        {
            "data": null,
            "render": function (data, type, full, meta) {
                return meta.row + 1;
            }
        },
        //{
        //    "data": "ID",
        //    "render": function (data, type, row, meta) {
        //        return '<div class="center">' + data + '</div>';
        //    }
        //},
        {
            "data": "NoPegawai",
            "name": "NoPegawai",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "Nama",
            "name": "Nama",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "ID",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a href="javascript:void(0)" style="color:black" onclick="SelectedUserPopup('${data}', '${row.Nama}','${row.NoPegawai}', '${row.Email}','${row.RoleID}')">
                                    <button type="button" class="btn btn-primary" rel="tooltip" title="Select User">
                                    <span class="material-icons">check</span></i></a>
                                    </button>
                                </a>
                            </div>
                        </div>`;
            }
        },
    ],
});

function SelectedUserPopup(id,Nama, NoPegawai, Email, RoleID) {
    $('#modalEmployee').modal('hide');
        
    $('#UserName').val(Nama);
    $('#NoPegawai').val(NoPegawai);
    $('#Email').val(Email);
    $('#RoleID').val(RoleID);

    randomString();
}

function randomString() {
    var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
    var string_length = 8;
    var randomstring = '';
    for (var i = 0; i < string_length; i++) {
        var rnum = Math.floor(Math.random() * chars.length);
        randomstring += chars.substring(rnum, rnum + 1);
    }
    //document.randform.randomfield.value = randomstring;
    $('#Password').val(randomstring);
    
}

function GetRandomPassword() {


    $.ajax({
        type: "GET",
        url: '/Admin/UserManage/GenerateRandomPassword',
        //data: {},
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        //beforeSend: function () {
        //    Show(); // Show loader icon  
        //},
        success: function (data) {
            alert(data);
            // Looping over emloyee list and display it  
            //$.each(response, function (index, emp) {
                //$('#output').append('<p>Id: ' + emp.ID + '</p>' +
                //    '<p>Id: ' + emp.Name + '</p>');
            //});
        },
        //complete: function () {
        //    Hide(); // Hide loader icon  
        //},
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message  
        }
    });
}
