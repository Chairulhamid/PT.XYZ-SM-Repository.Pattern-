using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Presentation.Helper;
using OSCEUKDI.Presentation.models;
using OSCEUKDI.Services;
using OSCEUKDI.Services.OSCEUKDIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace OSCEUKDI.Presentation.Areas.Admin.Controllers
{
    [HRISAuthorize]
    public class RuanganUjianController : Controller
    {
        private IRuangUjianService _ruangUjianService;
        public RuanganUjianController(IRuangUjianService RuangUjianService)
        {
            _ruangUjianService = RuangUjianService;
        }
        // GET: Admin/Company
        public ActionResult Index()
        {
            ViewData["isCreate"] = HttpContext.Session["isCreate"].ToString();
            ViewData["isUpdate"] = HttpContext.Session["isUpdate"].ToString();
            ViewData["isDelete"] = HttpContext.Session["IsDelete"].ToString();
            return View();
        }
        [HttpPost]
        public JsonResult GetDataALL(DataTableAjaxPostModel model)
        {
            var data = _ruangUjianService.getAllData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreatePage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddData(RuangUjian ruangUjian)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var temp = _ruangUjianService.Find(ip => ip.NamaRuangan.ToLower() == ruangUjian.NamaRuangan.ToLower() && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                if (temp == null)
                {
                    ruangUjian.IsActive = true;
                    ruangUjian.CreatedBy = Session["username"] as string;
                    ruangUjian.UpdatedBy = Session["username"] as string;
                    ruangUjian.CreatedDate = DateTime.Now;
                    ruangUjian.UpdatedDate = DateTime.Now;
                    _ruangUjianService.Save(ruangUjian);
                    message = "Data Berhasil Ditambahkan!";
                }
                else
                {
                    status = 400;
                    message = "Gagal ditambahkan, Nama Ruangan Sudah Terdaftar!";
                }
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult UpdatePage(int id)
        {
            var tmpData = _ruangUjianService.Get(id);
            return View(tmpData);
        }
        [HttpPost]
        public ActionResult UpdateData(RuangUjian ruangUjian)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var temp = _ruangUjianService.Find(ip => ip.NamaRuangan.ToLower() == ruangUjian.NamaRuangan.ToLower() && ip.IsActive == true && ip.IsDeleted == false ).FirstOrDefault();
                var dataID = _ruangUjianService.Get(ruangUjian.ID).NamaRuangan;
                if (temp == null || dataID == ruangUjian.NamaRuangan)
                {
                RuangUjian data = _ruangUjianService.Get(ruangUjian.ID);
                data.NamaRuangan = ruangUjian.NamaRuangan;
                data.UpdatedBy = Session["username"] as string;
                data.UpdatedDate = DateTime.Now;
                _ruangUjianService.Save(data);
                return Json(new ServiceResponse { status = 200, message = "Data Berhasil Diupdate!" });
                }
                else
                {
                    status = 400;
                    message = "Gagal ditambahkan, Nama Ruangan Sudah Terdaftar!";
                }
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            var data = _ruangUjianService.Get(id);
            data.IsDeleted = true;
            data.IsActive= false;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;
            _ruangUjianService.Save(data);
            return Json(data);
        }
    }
}