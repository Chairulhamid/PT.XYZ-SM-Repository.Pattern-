
const tmpUrlink = {
    LoadInit: "/Admin/JadwalTest",
    CreatePage: "JadwalTest/CreatePage",
    PostCreate: "/Admin/JadwalTest/AddData",
    PostUpdate: "/Admin/JadwalTest/UpdateData",
    PostDelete: "/Admin/JadwalTest/DeleteData",
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.ACADCareer = $('#ACADCareer').val();
    data.TahunSemester = $('#TahunSemester').val();
    data.NamaPeriode = $('#NamaPeriode').val();
    data.KategoriOsce = $('#KategoriOsce').val();
    data.TanggalUjian = $('#TanggalUjian').val();
}
function clearValueOnForm() {
    $("#ACADCareer").val('');
    $('#TahunSemester').val('');
    $('#NamaPeriode').text('');
    $('#KategoriOsce').text('');
    $('#TanggalUjian').val('');
}
function CreateData() {
    window.location.href = tmpUrlink.CreatePage;
}
function PostCreate() {
    PostDataAjax(data, tmpUrlink.PostCreate, tmpUrlink.LoadInit);
}

function PostUpdate() {
    UpdateDataAjax($('#ID').val(), data, tmpUrlink.PostUpdate, tmpUrlink.LoadInit);
}
function Delete(id) {
    DeleteDataAjax(id, tmpUrlink.PostDelete, tmpUrlink.LoadInit);
}