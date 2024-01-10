
const tmpUrlink = {
    LoadInit: "/Admin/MappingPertanyaan",
    CreatePage: "MappingPertanyaan/CreatePage",
    PostCreate: "/Admin/MappingPertanyaan/AddData",
    PostUpdate: "/Admin/MappingPertanyaan/UpdateData",
    PostDelete: "/Admin/MappingPertanyaan/DeleteData",
    JadwalTest: "/Admin/MappingPertanyaan/GetJadwalTest",
    Pertanyaan: "/Admin/MappingPertanyaan/GetPertanyaan",
    Karyawan: "/Admin/MappingPertanyaan/GetKaryawan"
};
var data = {}
//2.Validasi
function getValueOnForm() {
    data.TahunSemester = $('#TahunSemester').val();
    data.ACADCareer = $('#ACADCareer').val();
    data.JadwalTestID = $('#JadwalTest').val();
    data.RuangUjianID = $('#RuangUjian').val();
    data.RubrikKompetensiID = $('#Pertanyaan').val();
    data.NomorStation = $('#NomorStation').val();
    data.NIPDosenInti = $('#NIPDosenInti').val();
    data.NIPDosen2 = $('#NIPDosen2').val();
    data.NIPDosen3 = $('#NIPDosen3').val();
}
function clearValueOnForm() {
    $("#TahunSemester").val('');
    $("#ACADCareer").val('');
    $("#JadwalTest").val('');
    $("#RuangUjian").val('');
    $("#Pertanyaan").val('');
    $("#NomorStation").val('');
    $("#NIPDosenInti").val('');
    $("#NIPDosen2").val('');
    $("#NIPDosen3").val('');
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
    $('#TahunSemester').append($('<option disabled selected></option>').attr('value', "").text("Tahun Semester"))
    $('#JadwalTest').append($('<option disabled selected></option>').attr('value', "").text("Jadwal Test"))
    $('#Pertanyaan').append($('<option disabled selected></option>').attr('value', "").text("Pertanyaan"))
    $("#JadwalTest").prop("disabled", true);
    $("#Pertanyaan").prop("disabled", true);
});
$("#TahunSemester").change(function () {
    $("#JadwalTest").prop("disabled", false);
    $("#Pertanyaan").prop("disabled", false);
    $("#JadwalTest").empty();
    $("#Pertanyaan").empty();
    getJadwalTest();
    getDataAjax(tmpUrlink.Pertanyaan, dataPost, true, setPertanyaan);
});

function getJadwalTest() {
    dataPost = {
        TahunSemester: $('#TahunSemester').val(),
        ACADCareer: $('#ACADCareer').val(),
    }
    getDataAjax(tmpUrlink.JadwalTest, dataPost, true, setJadwalTest);
}

function getJadwalTestUpdate() {
    dataPost = {
        TahunSemester: $('#TahunSemester').val(),
        ACADCareer: $('#ACADCareer').val(),
    }
    getDataAjax(tmpUrlink.JadwalTest, dataPost, true, setJadwalTestUpdate);
}
function getPertanyaanUpdate() {
    dataPost = {
        TahunSemester: $('#TahunSemester').val(),
        ACADCareer: $('#ACADCareer').val(),
    }
    getDataAjax(tmpUrlink.Pertanyaan, dataPost, true, setPertanyaanUpdate);
}


function setJadwalTest(data) {
    $('#JadwalTest').append($('<option disabled selected></option>').attr('value', "").text("Jadwal Test"))
    if (data != "Empty") {
        $.each(data, function (index, data) {
            $('#JadwalTest').append($('<option></option>').attr('value', data.ID).text(data.NamaPeriode + " (" + moment(data.TanggalUjian).format("DD-MMM-YYYY") +")" ));
        });
    }
}
function setJadwalTestUpdate(data) {
    if (data != "Empty") {
        $.each(data, function (index, data) {
            if ($('[name="JadwalTestUpdate"]').val() != data.ID) {
                $('#JadwalTest').append($('<option></option>').attr('value', data.ID).text(data.NamaPeriode + " (" + moment(data.TanggalUjian).format("DD-MMM-YYYY") + ")"));
            }
        });
    }
}
function setPertanyaan(data) {
    $('#Pertanyaan').append($('<option disabled selected></option>').attr('value', "").text("Pertanyaan"))
    if (data != "Empty") {
        $.each(data, function (index, data) {
            $('#Pertanyaan').append($('<option></option>').attr('value', data.ID).text(data.JudulStation));
        });
    }
}
function setPertanyaanUpdate(data) {
    if (data != "Empty") {
        $.each(data, function (index, data) {
            if ($('[name="PertanyaanUpdate"]').val() != data.ID) {
                $('#Pertanyaan').append($('<option></option>').attr('value', data.ID).text(data.JudulStation));
            }
        });
    }
}
   