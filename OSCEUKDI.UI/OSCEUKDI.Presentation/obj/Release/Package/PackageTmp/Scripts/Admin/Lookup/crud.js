
const tmpUrlink = {
    LoadInit: "/Admin/Lookup",
    CreatePage: "Lookup/CreatePage",
    PostCreate: "/Admin/Lookup/AddData",
    PostUpdate: "/Admin/Lookup/UpdateData",
    PostDelete: "/Admin/Lookup/DeleteData",
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.Tipe = $('#Tipe').val().toUpperCase();
    data.Nama = $('#Nama').val();
    data.Nilai = $('#Nilai').val();
}
function clearValueOnForm() {
    $("#Tipe").val('');
    $("#Nama").val('');
    $("#Nilai").val('');
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