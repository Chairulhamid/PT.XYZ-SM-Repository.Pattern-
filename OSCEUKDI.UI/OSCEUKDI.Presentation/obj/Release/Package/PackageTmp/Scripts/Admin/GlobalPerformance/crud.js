
const tmpUrlink = {
    LoadInit: "/Admin/GlobalPerformance",
    CreatePage: "GlobalPerformance/CreatePage",
    PostCreate: "/Admin/GlobalPerformance/AddData",
    PostUpdate: "/Admin/GlobalPerformance/UpdateData",
    PostDelete: "/Admin/GlobalPerformance/DeleteData",
    JudulStation: "/Admin/GlobalPerformance/GetJudulStation"
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.TahunSemester = $('#TahunSemester').val(); 
    data.RubrikKompetensiID = $('#JudulStation').val(); 
    data.ACADCareer = $('#ACADCareer').val();
    data.Nama = $('#Nama').val().toUpperCase();
    data.NMin = $('#NMin').val();
    data.NMax = $('#NMax').val();
}
function clearValueOnForm() {
    $('#TahunSemester').val('');
    $('#JudulStation').val('');
    $('#ACADCareer').val('');
    $("#Nama").val('');
    $("#NMin").val('');
    $("#NMax").val('');
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
    DeleteDataAjaxJson(id, tmpUrlink.PostDelete, tmpUrlink.LoadInit);
}
$("#ACADCareer").change(function () {
    $("#TahunSemester").prop("disabled", false);
    $("#Nama").prop("disabled", false);
    $("#NMin").prop("disabled", false);
    $("#NMax").prop("disabled", false);
    $("#TahunSemester").val('');
    $("#JudulStation").val('');
    $('#JudulStation').append($('<option disabled selected></option>').attr('value', "").text("Judul Station"))
    $("#JudulStation").prop("disabled", true);
});
$("#TahunSemester").change(function () {
    $("#JudulStation").prop("disabled", false);
    $("#JudulStation").empty();
    dataPost = {
        TahunSemester: $('#TahunSemester').val(),
        ACADCareer: $('#ACADCareer').val(),
        }
    getDataAjax(tmpUrlink.JudulStation, dataPost, true, setJudulSattion);
});


function setJudulSattion(data) {
        $('#JudulStation').append($('<option disabled selected></option>').attr('value',"").text("Judul Station"))
    if (data != "Empty") {
        $.each(data, function (index, data) {
            $('#JudulStation').append($('<option></option>').attr('value', data.ID).text(data.JudulStation));
        });
    } 
}