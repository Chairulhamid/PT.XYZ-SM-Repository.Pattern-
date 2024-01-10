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
    public class KompetensiController : Controller
    {
        private IKompetensiService _kompetensiService;
        public KompetensiController(IKompetensiService KompetensiService)
        {
            _kompetensiService = KompetensiService;

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
            var data = _kompetensiService.getAllData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult AddData(Kompetensi kompetensi)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var temp = _kompetensiService.Find(ip => ip.NamaKompetensi.ToLower() == kompetensi.NamaKompetensi.ToLower() && ip.KategoriOsce == kompetensi.KategoriOsce && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                if (temp == null)
                {
                    kompetensi.IsActive = true;
                    kompetensi.CreatedBy = Session["username"] as string;
                    kompetensi.UpdatedBy = Session["username"] as string;
                    kompetensi.CreatedDate = DateTime.Now;
                    kompetensi.UpdatedDate = DateTime.Now;
                    _kompetensiService.Save(kompetensi);
                    message = "Data Berhasil Ditambahkan!";
                }
                else
                {
                    status = 400;
                    message = "Gagal ditambahkan, Kompetensi Sudah Terdaftar!";
                }
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
       
        [HttpPost]
        public ActionResult UpdateData(Kompetensi kompetensi)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var temp = _kompetensiService.Find(ip => ip.NamaKompetensi.ToLower() == kompetensi.NamaKompetensi.ToLower() && ip.KategoriOsce == kompetensi.KategoriOsce && ip.IsActive == true && ip.IsDeleted == false && ip.ID != kompetensi.ID ).FirstOrDefault();
                var dataID = _kompetensiService.Get(kompetensi.ID).NamaKompetensi;
                if (temp == null)
                {
                    Kompetensi data = _kompetensiService.Get(kompetensi.ID);
                    data.NamaKompetensi = kompetensi.NamaKompetensi;
                    data.KategoriOsce = kompetensi.KategoriOsce;
                    data.UpdatedBy = Session["username"] as string;
                    data.UpdatedDate = DateTime.Now;
                    _kompetensiService.Save(data);
                    return Json(new ServiceResponse { status = 200, message = "Data Berhasil Diupdate!" });
                }
                else
                {
                    status = 400;
                    message = "Gagal ditambahkan, Kompetensi Sudah Terdaftar!";
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
            var data = _kompetensiService.Get(id);
            data.IsDeleted = true;
            data.IsActive = false;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;
            _kompetensiService.Save(data);
            return Json(data);
        }
    }
}