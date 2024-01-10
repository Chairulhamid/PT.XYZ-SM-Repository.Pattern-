
const tmpUrlink = {
    LoadInit: "/Admin/RuanganUjian",
    CreatePage: "RuanganUjian/CreatePage",
    PostCreate: "/Admin/RuanganUjian/AddData",
    PostUpdate: "/Admin/RuanganUjian/UpdateData",
    PostDelete: "/Admin/RuanganUjian/DeleteData",
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.NamaRuangan = $('#NamaRuangan').val();
}
function clearValueOnForm() {
    $("#NamaRuangan").val('');
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