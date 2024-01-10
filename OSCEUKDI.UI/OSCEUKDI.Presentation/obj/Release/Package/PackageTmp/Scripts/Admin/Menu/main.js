﻿var tableMenu = $('.table-menu').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0
    }],
    "order": [[0, 'asc']],
    "ajax": {
        url: '/Admin/Menu/GetDataMenu',
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
            "data": "MenuName",
            "name": "MenuName",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "MenuUrl",
            "name": "MenuUrl",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        },
        {
            "data": "MenuIcon",
            "name": "MenuIcon",
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return '<div class="center">-</div>';
                } else {
                    return '<div class="center">' + data + '</div>';
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
                                <a class="btn-update" href="javascript:void(0)" onclick="javascript:$('#id').val('${data}');$('#updateMenu').submit();" rel="tooltip" title="Edit" ><i class="fa fa-pencil-square-o" aria-hidden="true"style="font-size:20px"></i></a>
                                <a class="btn-delete" rel="tooltip" title="remove" onclick="Delete('${data}')" class="table-action remove" href="javascript:void(0)" title="Delete"><i class="fa fa-trash-o" aria-hidden="true"style="font-size:20px"></i></a>
                            </div>
                        </div>`;
            }
        }
    ],
    "initComplete": function (d) {
        var notApplyFilterOnColumn = [0, 4];
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

$("#JenisMenu").change(function () {
    if ($('#JenisMenu').val() == "sub") {
        $('#DivMenuParent').removeClass('d-none')
    }
    else {
        $('#DivMenuParent').addClass('d-none')
    }
});



