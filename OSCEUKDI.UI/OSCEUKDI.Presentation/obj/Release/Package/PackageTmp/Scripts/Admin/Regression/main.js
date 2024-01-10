var tableRegression = $('.table-Regression').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0
    }],
    "order": [[0, 'asc']],
    //"aaSorting": [[0, "asc"]],
    "ajax": {
        url: '/Admin/Regression/GetDataALL',
        type: 'POST',
        dataSrc: ''
    },
    "columns": [  
        {
            "data": "TanggalUjian",
            "render": function (data, type, row, meta) {
                return `<div class="row justify-content-center">
                            <div class="col" style="text-align:center">
                                <a class="btn-update " href="javascript:void(0)" onclick="javascript:$('#TanggalUjian').val('${data}');$('#NomorStation').val('${row.NomorStation}');$('#DownloadRegression').submit();" rel="tooltip" title="Edit" ><i class="fa fa-download" aria-hidden="true"style="font-size:20px"></i></a>                      
                            </div>
                        </div>`;
            }
        },
        {
            "data": null,
            "render": function (data, type, full, meta) {
                 num =  meta.row + 1;
                return '<strong class="center">' + num + '</strong>';
            }
        },
        {
            "data": "TanggalUjian",
            "name": "TanggalUjian",
            "render": function (data, type, row, meta) {
                return '<div class="center">' + data + '</div>';
            }
        }, 
        {
            "data": "NomorStation",
            "name": "NomorStation",
            "render": function (data, type, row, meta) {
                return '<div class="center">Station '  + data + '</div>';
            }
        }, 
        {
            "data": "NilaiRegression",
            "name": "NilaiRegression",
            "render": function (data, type, row, meta) {
                return '<div class="center">y = ' + data + '</div>';
            }
        },  
        
    ],
    "initComplete": function (d) {
        var notApplyFilterOnColumn = [0];
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
