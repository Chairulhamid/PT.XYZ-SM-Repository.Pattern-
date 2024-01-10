using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.Models.MBKM;
using MBKM.Common.Helpers;
using System.IO;
using MBKM.Presentation.Helper;
using MBKM.Repository.BaseRepository;
using MBKM.Presentation.models;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace MBKM.Presentation.Areas.Portal.Controllers.AdministrasiMbkm
{
    [MBKMAuthorize]
    public class AdministrasiMbkmController : Controller
    {
        private IAttachmentKegiatanMbkmService _attachmentKegiatanMbkmService;
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;
        private IAttachmentService _attachmentService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private IJadwalKuliahService _jkService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        public AdministrasiMbkmController(IAttachmentKegiatanMbkmService attachmentKegiatanMbkmService,IJadwalUjianMBKMService jadwalUjianMBKMService,IMahasiswaService mahasiswaService, ILookupService lookupService, IAttachmentService attachmentService, IPerjanjianKerjasamaService perjanjianKerjasamaService, IJadwalKuliahService jkService)
        {
            _attachmentKegiatanMbkmService = attachmentKegiatanMbkmService;
            _lookupService = lookupService;
            _attachmentService = attachmentService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _jkService = jkService;
            _jkService = jkService;
            _mahasiswaService = mahasiswaService;
        }
        // GET: Portal/AdministrasiMbkm
        public ActionResult Index()
        {
            return View();
        }
        /*Modal Created*/
        public ActionResult ModalCreateKegiatan()

        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["firstSemester"] = _mahasiswaService.GetDataSemester(null).First().Nilai;
            ViewData["semester"] = data;
            return View("ModalCreateKegiatan");
        }
        public ActionResult GetSemesterAll2()
        {
            var result = _jkService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }

       [HttpPost]
        public ActionResult GetKegiatanMbkm()
        {
            string emailMahasiswa = Session["emailMahasiswa"] as string;
            var dataMahasiswa = _mahasiswaService.GetKegiatanMbkmPortalList(emailMahasiswa);
            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                final.Add(new String[] {
                    d.ID.ToString(),
                    d.STRM,
                    d.FileName,
                    d.Keterangan,
                });
            }
            return Json(final);
        }
        //[HttpPost]
        public ActionResult SaveKegiatanMbkm(HttpPostedFileBase[] file, MBKM.Entities.Models.MBKM.AttachmentAdmKegiatanMbkm attachmentAdmKegiatanMbkm )
        {
            string[] ExtentionFile = { ".xlsx", ".xls",".pdf",".doc",".docx", ".jpg", ".jpeg", ".png", ".pptx", ".bitmap", ".txt" };
            var data = attachmentAdmKegiatanMbkm;
            var ID  = Session["idMahasiswa"];
            int ValData=   Int32.Parse((string)ID);
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    List<AttachmentAdmKegiatanMbkm> attachments = new List<AttachmentAdmKegiatanMbkm>();
                    //{
                        var files = file[0];
                        var fileName = Path.GetFileName(files.FileName);
                        var checkextension = Path.GetExtension(fileName).ToLower();
                    if (files != null && files.ContentLength > 3145728)
                    {
                        ViewBag.Message = String.Format("File yang terupload lebih dari 2MB");
                    }
                    else if (!ExtentionFile.Contains(checkextension))
                    {
                        return Json(new ServiceResponse { status = 400, message = "Jenis File yang anda masukan salah!" });
                    }
                    else if (files != null && files.ContentLength < 3145728)
                    {

                        AttachmentAdmKegiatanMbkm attch = new AttachmentAdmKegiatanMbkm()
                        {
                            FileName = fileName,
                            FileExt = Path.GetExtension(fileName),
                            FileSize = files.ContentLength.ToString(),
                            IsActive = true,
                            IsDeleted = false
                        };
                        attachments.Add(attch);
                        var path = Path.Combine(Server.MapPath("~/Upload/KegiatanMbkm/"), attch.FileName);
                        files.SaveAs(path);
                        attachmentAdmKegiatanMbkm.STRM = data.STRM;
                        attachmentAdmKegiatanMbkm.FileName = attch.FileName;
                        attachmentAdmKegiatanMbkm.FileExt = attch.FileExt;
                        attachmentAdmKegiatanMbkm.FileSize = attch.FileSize;
                        attachmentAdmKegiatanMbkm.Keterangan = data.Keterangan;
                        attachmentAdmKegiatanMbkm.MahasiswaID = ValData;
                        attachmentAdmKegiatanMbkm.CreatedBy = Session["nama"] as string;
                        attachmentAdmKegiatanMbkm.UpdatedBy = Session["nama"] as string;
                        attachmentAdmKegiatanMbkm.IsActive = true;
                        _attachmentKegiatanMbkmService.Save(attachmentAdmKegiatanMbkm);
                    }
                    return Json(new ServiceResponse { status = 200, message = "Save Berhasil!" });
                }
                else
                {
                    return Json(new ServiceResponse { status = 400, message = "Save Gagal!" });
                }
            }
            else
            {
                return Json(new ServiceResponse { status = 500, message = "Save Gagal!" });
            }
        }
    }
}