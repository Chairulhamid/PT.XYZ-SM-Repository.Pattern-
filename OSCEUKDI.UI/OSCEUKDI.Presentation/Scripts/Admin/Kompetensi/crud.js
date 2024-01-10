
const tmpUrlink = {
    LoadInit: "/Admin/Kompetensi",
    CreatePage: "Kompetensi/CreatePage",
    PostCreate: "/Admin/Kompetensi/AddData",
    PostUpdate: "/Admin/Kompetensi/UpdateData",
    PostDelete: "/Admin/Kompetensi/DeleteData",
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.NamaKompetensi = $('#NamaKompetensi').val();
    data.KategoriOsce = $('#KategoriOsce').val();
}
function clearValueOnForm() {
    $("#NamaKompetensi").val('');
    $("#KategoriOsce").val('');
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