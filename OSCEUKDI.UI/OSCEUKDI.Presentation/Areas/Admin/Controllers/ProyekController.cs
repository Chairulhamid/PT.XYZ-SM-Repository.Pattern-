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
    public class ProyekController : Controller
    {
        private IProyekService _proyekService;
        private IUserService _userService;
        public ProyekController(IProyekService ProyekService, IUserService UserService)
        {
            _proyekService = ProyekService;
            _userService = UserService;
        }
        // GET: Admin/Company
        public ActionResult Index()
        {
            ViewData["isCreate"] = HttpContext.Session["isCreate"].ToString();
            ViewData["isUpdate"] = HttpContext.Session["isUpdate"].ToString();
            ViewData["isDelete"] = HttpContext.Session["IsDelete"].ToString();
            var email = Session["email"].ToString();
            ViewData["StatusApproval"] = "";
            var checkUser = _userService.Find(xx => xx.Email == email).FirstOrDefault();
            if (checkUser.RoleID == 19)
            {
            ViewData["StatusApproval"] = checkUser.StatusApproval.ToString();
            }
            return View();
        }
        public ActionResult CreatePage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddData(Proyek p)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                p.IsActive = true;
                p.CreatedBy = Session["email"] as string;
                p.UpdatedBy = Session["email"] as string;
                p.CreatedDate = DateTime.Now;
                p.UpdatedDate = DateTime.Now;
                _proyekService.Save(p);
                message = "Data Berhasil Ditambahkan!";

                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult GetDataALL()
        {
            int RoleId = int.Parse(Session["RoleID"].ToString());
            var email = Session["email"].ToString();
            IEnumerable<Proyek> data = null;
            if (RoleId == 19)
            {
                data = _proyekService.Find(xx => xx.CreatedBy == email && xx.IsActive == true).ToList();
            }
            else
            {
                data = _proyekService.Find(xx => xx.IsActive == true).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePage(int id)
        {
            var tmpData = _proyekService.Get(id);
            return View(tmpData);
        }
        [HttpPost]
        public ActionResult UpdateData(Proyek pp)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                Proyek data = _proyekService.Get(pp.ID);
                data.Nama = pp.Nama;
                data.Deskripsi = pp.Deskripsi;
                data.UpdatedBy = Session["email"] as string;
                data.UpdatedDate = DateTime.Now;
                _proyekService.Save(data);
                return Json(new ServiceResponse { status = 200, message = "Data Berhasil Diupdate!" });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            var data = _proyekService.Get(id);
            data.IsDeleted = true;
            data.IsActive = false;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;
            _proyekService.Save(data);
            return Json(data);
        }
    }
}