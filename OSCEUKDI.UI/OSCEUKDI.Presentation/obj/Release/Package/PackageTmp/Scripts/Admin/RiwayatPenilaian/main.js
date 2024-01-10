var tableJadwalTest = $('.table-riwayatPenilaian').DataTable({
    "columnDefs": [{
        "searchable": false,
        "orderable": false,
        "paging": false,
        "targets": 0
    }],
    "order": [[0, 'asc']],
    "ajax": {
        url: '/Admin/RiwayatPenilaian/GetDataALL',
        type: 'POST',
        dataSrc: ''
    },
    "columns": [      
        {
            "data": null,
            "render": function (data, type, full, meta) {
                 num =  meta.row + 1;
                return '<strong class="center">' + num + '</strong>';
            }
        },
        {
            "data": "TahunSemester",
            "name": "TahunSemester",
            "render": function (data, type, row, meta) {
                return '<div class="text-center hoverclk dataSemester"><strong class = "text-muted">' + data + '</strong></div>';
            }
        }, 
        {
            "data": "NamaPeriode",
            "name": "NamaPeriode",
            "render": function (data, type, row, meta) {
                return "<div class='d-flex text-center flex-column line-height-normal hoverclk ' style='min-width:0'><div class='text-secondary'> <strong class='text-muted text-center dataPeriode'>"+data+"</br></strong> <em class='dataTglUjian'>" + row.TanggalUjian + "</em></br><div class='d-flex text-center flex-column line-height-normal ' style='min-width:0'><div class='text-secondary'> <strong class='text-success text-center'>" + row.NamaRuangan + " </strong> ";
            }
        }, 
        {
            "data": "Nama1",
            "name": "Nama1",
            "render": function (data, type, row, meta) {
                if (row.Nama2 != null && row.Nama3 == null) {
                    return "<div class='d-flex text-center flex-column line-height-normal hoverclk' style='min-width:0'><div class='text-secondary'> <strong class='text-muted text-center'>" + data + " - " + row.NIPDosenInti + " </br> <span hidden  class='d-none dataNipDosen'>" + row.NIPDosenInti +"</span>   </strong><div class='d-flex text-center flex-column line-height-normal ' style='min-width:0'><div class='text-secondary'>  <span class='text-muted text-center'>" + row.Nama2 + " - " + row.NIPDosen2 +  " ";
                } else if (row.Nama3 != null) {
                    return "<div class='d-flex text-center flex-column line-height-normal hoverclk' style='min-width:0'><div class='text-secondary'> <strong class='text-muted text-center'>" + data + " - " + row.NIPDosenInti + " </br><span hidden  class='d-none dataNipDosen'>" + row.NIPDosenInti + "</span></strong>  <div class='d-flex text-center flex-column line-height-normal ' style='min-width:0'><div class='text-secondary'><span class='text-muted text-center'>" + row.Nama2 + " - " + row.NIPDosen2 +  " </span><span class='d-flex text-center flex-column line-height-normal'>" + row.Nama3 + " - " + row.NIPDosen3 +  "</span> ";
                } else {
                    return "<div class='d-flex text-center flex-column line-height-normal hoverclk' style='min-width:0'><div class='text-secondary'> <strong class='text-muted text-center'>" + data + " - " + row.NIPDosenInti + "<span hidden  class='d-none dataNipDosen'>" + row.NIPDosenInti +"</span>";
                }
            }
        },
        {
            "data": "KategoriOsce",
            "name": "KategoriOsce",
            "render": function (data, type, row, meta) {
                return "<div class='d-flex text-center flex-column line-height-normal hoverclk' style='min-width:0'><div class='text-secondary'> <strong class='text-muted text-center'>" + data + " </br></strong> <em > Station " + row.NoStation + "</em></br><div class='d-flex text-center flex-column line-height-normal ' style='min-width:0'><div class='text-secondary'> <strong class='text-success text-center dataAcad'>" +row.ACADCareer +"</strong> ";
            }
        },
       
        
    ],
    "initComplete": function (d) {
        var notApplyFilterOnColumn = [0,2,3,4];
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
