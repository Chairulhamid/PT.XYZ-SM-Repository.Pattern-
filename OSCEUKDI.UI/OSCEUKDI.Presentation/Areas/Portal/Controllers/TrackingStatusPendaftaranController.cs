using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Presentation.Helper;
using MBKM.Entities.ViewModel;
using Newtonsoft.Json;

namespace MBKM.Presentation.Areas.Portal.Controllers {
  [MBKMAuthorize]
  public class TrackingStatusPendaftaranController : Controller {
    private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
    private ICPLMKPendaftaranService _cPLMKPendaftaranService;
    private ICPLMatakuliahService _cPLMatakuliahService;
    private IMahasiswaService _mahasiswaService;
    private IApprovalPendaftaranService _approvalPendaftaranService;
    private IInformasiPertukaranService _informasiPertukaranService;
    private IFeedbackMatkulService _feedbackMatkulService;
    // 20221127 Sam
    private IPendaftaranMataKuliahService _pmkService;



    public TrackingStatusPendaftaranController(IPendaftaranMataKuliahService pmkService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, ICPLMKPendaftaranService cPLMKPendaftaranService, ICPLMatakuliahService cPLMatakuliahService, IMahasiswaService mahasiswaService, IApprovalPendaftaranService approvalPendaftaranService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService) {
      _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
      _cPLMKPendaftaranService = cPLMKPendaftaranService;
      _cPLMatakuliahService = cPLMatakuliahService;
      _mahasiswaService = mahasiswaService;
      _approvalPendaftaranService = approvalPendaftaranService;
      _informasiPertukaranService = informasiPertukaranService;
      _feedbackMatkulService = feedbackMatkulService;
      // 20221127 Sam
      _pmkService = pmkService;
    }




    // GET: Portal/TrackingStatusPendaftaran
    public ActionResult Index() {
      return View();
    }

    [HttpPost]
    public JsonResult GetPendaftaranMakul(/*DataTableAjaxPostModel model*/) {
      string emailMahasiswa = Session["emailMahasiswa"] as string;
      /*var data = _pendaftaranMataKuliahService.GetPendaftaranMahasiswaDataTableByMahasiswa(model,emailMahasiswa);*/
      var dataPendaftaran = _pendaftaranMataKuliahService.Find(c => c.mahasiswas.Email == emailMahasiswa).ToList();
      var checkInformasiPertukaran = _informasiPertukaranService.Find(z => z.Mahasiswas.Email == emailMahasiswa).FirstOrDefault();
      List<String[]> final = new List<String[]>();

      if (checkInformasiPertukaran != null) {
        if (checkInformasiPertukaran.JenisKerjasama.ToLower().Contains("ke luar")) {
          foreach (var d in dataPendaftaran) {
            final.Add(new String[]{
                            d.ID.ToString(),
                            "-",
                            "-",
                            "-",
                            d.MatkulAsal,
                            "-",
                            d.StatusPendaftaran,
                        });
          }
        }
        else {
          foreach (var d in dataPendaftaran) {
            final.Add(new String[]{
                            d.ID.ToString(),
                            d.JadwalKuliahs.NamaFakultas,
                            d.JadwalKuliahs.NamaProdi,
                            d.JadwalKuliahs.KodeMataKuliah,
                            d.JadwalKuliahs.NamaMataKuliah,
                            d.JadwalKuliahs.NamaDosen,
                            d.StatusPendaftaran,
                        });
          }
        }
      }
      else {
        foreach (var d in dataPendaftaran) {
          final.Add(new String[]{
                            d.ID.ToString(),
                            d.JadwalKuliahs.NamaFakultas,
                            d.JadwalKuliahs.NamaProdi,
                            d.JadwalKuliahs.KodeMataKuliah,
                            d.JadwalKuliahs.NamaMataKuliah,
                            d.JadwalKuliahs.NamaDosen,
                            d.StatusPendaftaran,
                        });
        }
      }

      return Json(final);
      /*return Json(new
          {
              draw = model.draw,
              recordsTotal = data.TotalCount,
              recordsFiltered = data.TotalFilterCount,
              data = data.gridDatas
          }
      );*/
    }

    [HttpPost]
    public ActionResult _getIndexDetailView(int id) {
      var data = _pendaftaranMataKuliahService.Get(id);
      // 20221117 Bite
      if (data.StatusPendaftaran.Contains("MENUNGGU") || data.StatusPendaftaran.Contains("BATAL")) {
        ViewData["disabled"] = "true";
      }
      else {
        ViewData["disabled"] = "";
      }

      IList<ApprovalPendaftaran> dataApproval = _approvalPendaftaranService.Find(x => x.PendaftaranMataKuliahID == id).ToList();
      ViewData["lastStatus"] = _approvalPendaftaranService.Find(x => x.PendaftaranMataKuliahID == id).Last().StatusPendaftaran;
      ViewData["status"] = dataApproval;
      var email = HttpContext.Session["emailMahasiswa"].ToString();
      var dataInformasiPertukaran = _informasiPertukaranService.Find(x => x.Mahasiswas.Email == email).OrderByDescending(x => x.ID).FirstOrDefault();
      if (dataInformasiPertukaran != null) {
        if (dataInformasiPertukaran.JenisKerjasama.ToLower().Contains("internal ke luar")) {
          ViewData["mahasiswaInternalKeluar"] = true;
        }
        else {
          ViewData["mahasiswaInternalKeluar"] = false;
        }

        if (dataInformasiPertukaran.JenisPertukaran.ToLower().Contains("non")) {
          ViewData["mahasiswaNonPertukaran"] = true;
        }
        else {
          ViewData["mahasiswaNonPertukaran"] = false;
        }
      }
      else {
        ViewData["mahasiswaInternalKeluar"] = false;
        ViewData["mahasiswaNonPertukaran"] = false;
      }
      return View(data);
    }

    public ActionResult IndexDetailPendaftaran(int id) {
      var data = _cPLMKPendaftaranService.Find(x => x.IsDeleted == false && x.PendaftaranMataKuliahID == id).First();
      ViewData["semesterMahasiswa"] = _feedbackMatkulService.GetSemesterByStrm(data.PendaftaranMataKuliahs.JadwalKuliahs.STRM.ToString()).Nama;
      var namaTahunSemester = ViewData["semesterMahasiswa"].ToString();

      IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID && x.NamaTahunSemester == namaTahunSemester && x.IsActive == true && x.IsDeleted == false).ToList();
      /*IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == data.PendaftaranMataKuliahs.JadwalKuliahs.ProdiID).ToList();*/
      IList<CPLMatakuliah> capaianTujuan = tempId;
      //IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID && x.IsActive == true && x.IsDeleted == false).ToList();
      /*IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == data.PendaftaranMataKuliahs.JadwalKuliahs.ProdiID).ToList();*/
      //IList<CPLMatakuliah> capaianTujuan = tempId;
      var tmpInformasiPertukaran1 = _informasiPertukaranService.Find(x => x.MahasiswaID == data.PendaftaranMataKuliahs.MahasiswaID).Count();

      if (tmpInformasiPertukaran1 == 0) {
        ViewData["jenisProgram"] = "Pertukaran";
        ViewData["jenisKegiatan"] = "Eksternal Dari Luar Atma Jaya";

        ViewData["capaianTujuan"] = capaianTujuan;
        ViewData["countCPTujuan"] = capaianTujuan.Count();

      }
      else {
        var tmpInformasiPertukaran = _informasiPertukaranService.Find(x => x.MahasiswaID == data.PendaftaranMataKuliahs.MahasiswaID).OrderByDescending(x => x.ID).FirstOrDefault();

        if (tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("internal") && !tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("luar")) {
          ViewData["jenisProgram"] = "Pertukaran";
          ViewData["jenisKegiatan"] = "Internal";
        }
        else if (tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("magang")) {
          ViewData["jenisProgram"] = "Non-Pertukaran";
          ViewData["jenisKegiatan"] = "Magang";
        }
        else if (tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("internal") && tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("luar")) {
          ViewData["jenisProgram"] = "Pertukaran";
          ViewData["jenisKegiatan"] = "Internal Ke Luar Atma Jaya";
        }
        else {
          ViewData["jenisProgram"] = "Non-Pertukaran";
          ViewData["jenisKegiatan"] = tmpInformasiPertukaran.JenisPertukaran;
        }

        if (data.CPLMatakuliahID != null && !tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("internal ke luar")) {
          IList<CPLMatakuliah> tempIDAsal = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.CPLMatakuliahs.IDMataKUliah && x.NamaTahunSemester == namaTahunSemester && x.IsActive == true && x.IsDeleted == false).ToList();

          //WIList<CPLMatakuliah> tempIDAsal = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.CPLMatakuliahs.IDMataKUliah && x.IsActive == true && x.IsDeleted == false).ToList();
          /*IList<CPLMatakuliah> capaianAsal = tempIDAsal.Where(x => x.MasterCapaianPembelajarans.ProdiID == data.CPLMatakuliahs.MasterCapaianPembelajarans.ProdiID).ToList();*/
          IList<CPLMatakuliah> capaianAsal = tempIDAsal;
          ViewData["capaianAsal"] = capaianAsal;
          ViewData["countCPAsal"] = capaianAsal.Count();

          ViewData["capaianTujuan"] = capaianTujuan;
          ViewData["countCPTujuan"] = capaianTujuan.Count();
        }
        else if (tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("internal") && tmpInformasiPertukaran.JenisKerjasama.ToLower().Contains("luar")) {
          ViewData["capaianAsal"] = capaianTujuan;
          ViewData["countCPAsal"] = capaianTujuan.Count();
        }
        else {
          ViewData["capaianTujuan"] = capaianTujuan;
          ViewData["countCPTujuan"] = capaianTujuan.Count();
        }

      }

      if (data.PendaftaranMataKuliahs.StatusPendaftaran.Contains("MAHASISWA") || data.PendaftaranMataKuliahs.StatusPendaftaran.Contains("REJECTED")) {
        ViewData["disabled"] = "true";
      }
      else {
        ViewData["disabled"] = "";
      }
      ViewData["catatanBaru"] = _approvalPendaftaranService.Find(x => x.PendaftaranMataKuliahID == data.PendaftaranMataKuliahID && (x.StatusPendaftaran.Contains("APPROVED") || x.StatusPendaftaran.Contains("REJECTED") || x.StatusPendaftaran.Contains("ACCEPTED"))).FirstOrDefault().Catatan;
      ViewData["semesterMahasiswa"] = _feedbackMatkulService.GetSemesterByStrm(data.PendaftaranMataKuliahs.JadwalKuliahs.STRM.ToString()).Nama;

      if (data.PendaftaranMataKuliahs.mahasiswas.NoKerjasama == null) {
        var informasiNoKerjasama = _informasiPertukaranService.Find(x => x.MahasiswaID == data.PendaftaranMataKuliahs.MahasiswaID).FirstOrDefault();
        if (informasiNoKerjasama == null || informasiNoKerjasama.NoKerjasama == null) {
          ViewData["InformasiKerjasama"] = "-";
        }
        else {
          ViewData["InformasiKerjasama"] = informasiNoKerjasama.NoKerjasama;
        }
      }
      else {

        ViewData["InformasiKerjasama"] = data.PendaftaranMataKuliahs.mahasiswas.NoKerjasama;
      }
      return View(data);
    }

    [HttpPost]
    public JsonResult PostApprovalAccepted(int id) {
      Int64 idMhs = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string)).ID;
      try {
        CPLMKPendaftaran TmpApproval = _cPLMKPendaftaranService.Get(id);
        PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
        pendaftaran.StatusPendaftaran = "ACCEPTED BY MAHASISWA";
        pendaftaran.UpdatedDate = DateTime.Now;
        var dataPendaftaranJadwal = pendaftaran.JadwalKuliahs;
        //var cplmkPendaftaran = pendaftaran.CPLMKPendaftarans;
        var datasemester = _mahasiswaService.GetDataSemester(null).First().Nilai;
        try {
          _pendaftaranMataKuliahService.Save(pendaftaran);
          var NimBaru = _mahasiswaService.GetNim(Convert.ToInt32(datasemester));

          if (pendaftaran.mahasiswas.NIM == null /*|| (pendaftaran.mahasiswas.NIM != pendaftaran.mahasiswas.NIMAsal && !pendaftaran.mahasiswas.NIM.Contains("MBKM"))*/) {
            Mahasiswa tmpMhs = _mahasiswaService.Get(pendaftaran.MahasiswaID);
            tmpMhs.NIM = NimBaru;
            _mahasiswaService.Save(tmpMhs);
            string data = NimBaru.Substring(NimBaru.Length - 4);
            /*string[] tempNimArray = NimBaru.Split(new string[] { "MBKM" }, StringSplitOptions.None);*/
            int CountNim = Int32.Parse(data);
            _mahasiswaService.UpdateNim(CountNim, Convert.ToInt32(datasemester));
          }

          ApprovalPendaftaran tempHistoryApproval = new ApprovalPendaftaran();
          tempHistoryApproval.Approval = "MAHASISWA";
          tempHistoryApproval.Catatan = "-";
          tempHistoryApproval.StatusPendaftaran = "ACCEPTED BY MAHASISWA";
          tempHistoryApproval.IsActive = true;
          tempHistoryApproval.IsDeleted = false;
          tempHistoryApproval.PendaftaranMataKuliahID = TmpApproval.PendaftaranMataKuliahID;
          tempHistoryApproval.CreatedBy = HttpContext.Session["nama"].ToString();
          tempHistoryApproval.UpdatedBy = HttpContext.Session["nama"].ToString();
          tempHistoryApproval.UpdatedDate = DateTime.Now;
          tempHistoryApproval.CreatedDate = DateTime.Now;

          try {
            _approvalPendaftaranService.Save(tempHistoryApproval);
            try {
              //diisini untuk generate ke tabel absesnsi, dan di tabel pendaftaran.jadwakuliahs ambil datanya nanti 
              _mahasiswaService.GenerateAbsence(pendaftaran.JadwalKuliahID, pendaftaran.MahasiswaID, pendaftaran.JadwalKuliahs.KodeMataKuliah, pendaftaran.JadwalKuliahs.ClassSection, pendaftaran.JadwalKuliahs.STRM.ToString(), pendaftaran.JadwalKuliahs.FakultasID.ToString());
            }
            catch (Exception e) {
              return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Absensi" });
            }
            // Insert data matakuliah more 1 component
            try {
              var dataPendaftaranFinal = _pendaftaranMataKuliahService.GetMataKuliahMoreComponent(dataPendaftaranJadwal.STRM, dataPendaftaranJadwal.NamaProdi, dataPendaftaranJadwal.JenjangStudi, dataPendaftaranJadwal.ProdiID, dataPendaftaranJadwal.KodeMataKuliah, dataPendaftaranJadwal.ClassSection);
              foreach (var tmp in dataPendaftaranFinal) {

                ApprovalPendaftaran tempapprovalPendaftaranComp = new ApprovalPendaftaran();
                PendaftaranMataKuliah temppendaftaranMataKuliahComp = new PendaftaranMataKuliah();
                Int64 ID = 0;
                if (pendaftaran.MatkulKodeAsal != null) {
                  temppendaftaranMataKuliahComp.MatkulIDAsal = pendaftaran.MatkulIDAsal;
                  temppendaftaranMataKuliahComp.MatkulKodeAsal = pendaftaran.MatkulKodeAsal;
                  temppendaftaranMataKuliahComp.MatkulAsal = pendaftaran.MatkulAsal;
                }
                temppendaftaranMataKuliahComp.MahasiswaID = idMhs;
                temppendaftaranMataKuliahComp.IsActive = true;
                temppendaftaranMataKuliahComp.IsDeleted = false;
                temppendaftaranMataKuliahComp.JadwalKuliahID = tmp.ID;
                if (pendaftaran.Kesenjangan == null) {
                  temppendaftaranMataKuliahComp.Kesenjangan = "-";
                }
                else {
                  temppendaftaranMataKuliahComp.Kesenjangan = pendaftaran.Kesenjangan;
                }
                if (pendaftaran.Hasil == null) {
                  temppendaftaranMataKuliahComp.Hasil = "-";
                }
                else {
                  temppendaftaranMataKuliahComp.Hasil = pendaftaran.Hasil;
                }
                if (pendaftaran.Konversi == null) {
                  temppendaftaranMataKuliahComp.Konversi = "-";
                }
                else {
                  temppendaftaranMataKuliahComp.Konversi = pendaftaran.Konversi;
                }
                temppendaftaranMataKuliahComp.DosenID = tmp.DosenID;
                temppendaftaranMataKuliahComp.DosenPembimbing = tmp.NamaDosen;
                temppendaftaranMataKuliahComp.CreatedBy = HttpContext.Session["nama"].ToString();
                temppendaftaranMataKuliahComp.CreatedDate = DateTime.Now;
                temppendaftaranMataKuliahComp.UpdatedBy = HttpContext.Session["nama"].ToString();
                temppendaftaranMataKuliahComp.UpdatedDate = DateTime.Now;
                temppendaftaranMataKuliahComp.StatusPendaftaran = "ACCEPTED BY MAHASISWA";
                _pendaftaranMataKuliahService.Save(temppendaftaranMataKuliahComp);
                ID = temppendaftaranMataKuliahComp.ID;

                if (TmpApproval.CPLMatakuliahs != null) {
                  string kodeMatkul = TmpApproval.CPLMatakuliahs.KodeMataKuliah;
                  string prodi = Session["prodiAsal"] as string;
                  var list = _cPLMatakuliahService.Find(cplmk => cplmk.MasterCapaianPembelajarans.IsActive && !cplmk.MasterCapaianPembelajarans.IsDeleted && cplmk.IsActive && !cplmk.IsDeleted && cplmk.KodeMataKuliah == kodeMatkul && cplmk.MasterCapaianPembelajarans.NamaProdi == prodi).ToList();
                  foreach (var item in list) {
                    CPLMKPendaftaran res = new CPLMKPendaftaran();
                    res.CPLMatakuliahID = item.ID;
                    res.PendaftaranMataKuliahID = ID;
                    res.IsActive = true;
                    res.IsDeleted = false;
                    res.CreatedDate = DateTime.Now;
                    res.UpdatedDate = DateTime.Now;
                    _cPLMKPendaftaranService.Save(res);
                  }
                }
                else {
                  CPLMKPendaftaran res = new CPLMKPendaftaran();
                  res.CPLAsal = TmpApproval.CPLAsal;
                  res.PendaftaranMataKuliahID = ID;
                  res.IsActive = true;
                  res.IsDeleted = false;
                  res.CreatedDate = DateTime.Now;
                  res.UpdatedDate = DateTime.Now;
                  _cPLMKPendaftaranService.Save(res);
                }

                tempapprovalPendaftaranComp.StatusPendaftaran = "ACCEPTED BY MAHASISWA";
                tempapprovalPendaftaranComp.PendaftaranMataKuliahID = ID;
                tempapprovalPendaftaranComp.Approval = "MAHASISWA";
                tempapprovalPendaftaranComp.Catatan = "-";
                tempapprovalPendaftaranComp.IsActive = true;
                tempapprovalPendaftaranComp.IsDeleted = false;
                tempapprovalPendaftaranComp.CreatedBy = HttpContext.Session["nama"].ToString();
                tempapprovalPendaftaranComp.CreatedDate = DateTime.Now;
                tempapprovalPendaftaranComp.UpdatedBy = HttpContext.Session["nama"].ToString();
                tempapprovalPendaftaranComp.UpdatedDate = DateTime.Now;
                _approvalPendaftaranService.Save(tempapprovalPendaftaranComp);
                try {
                  _mahasiswaService.GenerateAbsence(tmp.ID, idMhs, tmp.KodeMataKuliah, tmp.ClassSection, tmp.STRM.ToString(), tmp.FakultasID.ToString());
                }
                catch (Exception e) {
                  return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Absensi" });
                }
              }
            }
            catch (Exception e) {

              return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
            }
          }
          catch (Exception e) {
            return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
          }

        }
        catch (Exception e) {
          return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan Saat Proses Accepted" });
        }



        return Json(new ServiceResponse { status = 200, message = "Done" });
      }
      catch (Exception e) {
        return Json(new ServiceResponse { status = 500, message = "Error" });
      }
    }

    [HttpPost]
    public JsonResult PostApprovalRejected(int id) {
      try {
        CPLMKPendaftaran TmpApproval = _cPLMKPendaftaranService.Get(id);
        PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
        pendaftaran.StatusPendaftaran = "REJECTED BY MAHASISWA";
        pendaftaran.UpdatedDate = DateTime.Now;
        var datasemester = _mahasiswaService.GetDataSemester(null).First().Nilai;
        try {
          _pendaftaranMataKuliahService.Save(pendaftaran);
          var NimBaru = _mahasiswaService.GetNim(Convert.ToInt32(datasemester));

          ApprovalPendaftaran tempHistoryApproval = new ApprovalPendaftaran();
          tempHistoryApproval.Approval = "MAHASISWA";
          tempHistoryApproval.Catatan = "-";
          tempHistoryApproval.StatusPendaftaran = "REJECTED BY MAHASISWA";
          tempHistoryApproval.IsActive = true;
          tempHistoryApproval.IsDeleted = false;
          tempHistoryApproval.PendaftaranMataKuliahID = TmpApproval.PendaftaranMataKuliahID;
          tempHistoryApproval.CreatedBy = HttpContext.Session["nama"].ToString();
          tempHistoryApproval.UpdatedBy = HttpContext.Session["nama"].ToString();
          tempHistoryApproval.UpdatedDate = DateTime.Now;
          tempHistoryApproval.CreatedDate = DateTime.Now;
          try {
            _approvalPendaftaranService.Save(tempHistoryApproval);
          }
          catch (Exception e) {
            return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
          }
        }
        catch (Exception e) {
          return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan Saat Proses Accepted" });
        }
        return Json(new ServiceResponse { status = 200, message = "Done" });
      }
      catch (Exception e) {
        return Json(new ServiceResponse { status = 500, message = "Error" });
      }
    }
    public Mahasiswa GetMahasiswaById(long id) {
      return _mahasiswaService.Find(m => m.ID == id).FirstOrDefault();
    }

    // 20221127 Sam
    public ActionResult GetTotalSKS() {
      Int64 id = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string)).ID;
      var checkData = _pmkService.GetTotalSKS(id);

      return new ContentResult { Content = JsonConvert.SerializeObject(checkData), ContentType = "application/json" };
    }
  }
}