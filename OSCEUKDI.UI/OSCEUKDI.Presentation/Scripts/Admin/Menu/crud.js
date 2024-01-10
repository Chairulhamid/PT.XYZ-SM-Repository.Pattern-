//1.Deklarasi data
const tmpUrlink = {
    Load: "/Admin/Menu",
    CreatePage: "Menu/CreatePage",
    PostCreate: "/Admin/Menu/AddMenu",
    PostUpdate: "/Admin/Menu/UpdateMenu",
    PostDelete: "/Admin/Menu/DeleteMenu",
    statusMenu: "/Admin/Menu/StatusMenu",
};
var data = {}
function getValueOnForm() {
    data.MenuName = $('#MenuName').val();
    data.MenuDescription = $('#MenuDescription').val();
    data.MenuUrl = $('#MenuUrl').val();
    if ($('#JenisMenu').val() == "parent") {
       data.MenuParent = "parent";
    } else {
    data.MenuParent = $('#MenuParent').val();
    }
    data.MenuIcon = $('#MenuIcon').val();
    data.MenuOrder = $('#MenuOrder').val();
}
function clearValueOnForm() {
    $("#MenuName").val('');
    $("#MenuDescription").val('');
    $("#MenuUrl").val('');
    $("#MenuParent").val('');
    $("#MenuIcon").val('');
    $("#MenuOrder").val('');

}
function CreateMenu() {
    window.location.href = tmpUrlink.CreatePage;
}
function PostCreate() {
    var JMenu = $("#JenisMenu").val();
    var PMenu = $("#MenuParent").val();
    if ((JMenu == "parent" && PMenu == null) || (JMenu == "parent" && PMenu != null) || (JMenu == "sub" && PMenu != null)) {
        PostDataAjax(data, tmpUrlink.PostCreate, tmpUrlink.Load);
    } else {
        document.getElementById('alertText').innerText = "Anda harus mengisi semua kolom yang wajib diisi!";
        $('#danger-alert').removeClass('d-none')
        $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#danger-alert").slideUp(500);
        });
    }
}
function PostUpdate() {
    var JMenu = $("#JenisMenu").val();
    var PMenu = $("#MenuParent").val();
    if ((JMenu == "parent" && PMenu == null) || (JMenu == "parent" && PMenu != null) || (JMenu == "sub" && PMenu != null)) {
        UpdateDataAjax($('#id_Menu').val() ,data, tmpUrlink.PostUpdate, tmpUrlink.Load);
    } else {
        document.getElementById('alertText').innerText = "Anda harus mengisi semua kolom yang wajib diisi!";
        $('#danger-alert').removeClass('d-none')
        $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#danger-alert").slideUp(500);
        });
    }
}
function Delete(id) {
    DeleteDataAjax(id, tmpUrlink.PostDelete, "/Admin/Menu");
}
