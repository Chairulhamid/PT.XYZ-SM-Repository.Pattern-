
//Masuk Kedalam Detail Riwayat
$('.table-riwayatPenilaian tbody').on('click', ' .hoverclk', function () {
    $('#ACADCareer').val($(this).closest("tr").find(".dataAcad").text());
    $('#TahunSemester').val($(this).closest("tr").find(".dataSemester").text());
    $('#NamaPeriode').val($(this).closest("tr").find(".dataPeriode").text());
    $('#TanggalUjian').val($(this).closest("tr").find(".dataTglUjian").text());
    $('#NIPDosenInti').val($(this).closest("tr").find(".dataNipDosen").text());
    $('#DetailRiwayat').submit();
})

