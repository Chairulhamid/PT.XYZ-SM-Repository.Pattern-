using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Presentation.Models;
using OSCEUKDI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSCEUKDI.Presentation.Areas.Admin.Controllers
{
//controller admin
    public class AdminLoginController : Controller
    {
        private readonly IUserService _userService;
       // private readonly ILogActivityService _logActService;

        public AdminLoginController(IUserService userService)
        {
            _userService = userService;
           // _logActService = logActService;
        }
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVMAdmin model)
        {

            if (ModelState.IsValid)
            {
                OSCEUKDI.Entities.Models.User modeldata = _userService.Find(x => x.Email == model.Username ).FirstOrDefault();
                if (modeldata != null)
                {
                    if (modeldata.IsActive == false)
                    {
                        ModelState.AddModelError("", "User Inactive!");
                    }
                    else
                    {
                        if (HashPasswordService.ValidatePassword(model.Password, modeldata.Password))
                        {
                            Session["userid"] = modeldata.ID.ToString();
                            Session["username"] = modeldata.UserName;
                           // Session["nopegawai"] = modeldata.NoPegawai.ToString();
                            Session["email"] = modeldata.Email;
                            Session["RoleID"] = modeldata.RoleID.ToString();

                            return RedirectToAction("Index", "Home");

                            //SaveActivityLog(model);
                            //return RedirectToAction("ResetPassword", "AdminLogin");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Password.");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }
            return View(model);
        }

        private void SaveActivityLog(LoginVMAdmin model)
        {
            //string message = System.String.Empty;
            //string result = "";

            //int status = 200;
            //try
            //{
            //    model.
            //    user.Password = HashPasswordService.HashPassword(user.Password);
            //    user.IsActive = true;
            //    user.CreatedBy = Session["username"] as string;
            //    user.UpdatedBy = Session["username"] as string;
            //    user.CreatedDate = DateTime.Now;
            //    user.UpdatedDate = DateTime.Now;
            //    _userService.Save(user);

            //    message = "Data Added Successfully!";

            //    return Json(new ServiceResponse { status = status, message = message });
            //}
            //catch (Exception e)
            //{
            //    return Json(new ServiceResponse { status = 500, message = e.Message });
            //}
        }

        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["username"] = null;
            //Session["nopegawai"] = null;
            Session["email"] = null;
            Session["MenuList"] = null;
            Session["NamaProdi"] = null;
            Session["KodeProdi"] = null;
            Session["NamaFakultas"] = null;
            Session["KodeFakultas"] = null;
            return RedirectToAction("Login", "AdminLogin");
        }
        public ActionResult DownloadFileUserGuideDosen()
        {

            string path = ConfigurationManager.AppSettings["PathAttachmentMahasiswa"].ToString();
            string fullName = Server.MapPath(path + "/UG-Dosen.pdf");
            byte[] fileBytes = GetFile(fullName);
            return File(
            fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "User Guide Dosen.pdf");
        }
        public ActionResult DownloadFileUserGuideKaprodi()
        {

            string path = ConfigurationManager.AppSettings["PathAttachmentMahasiswa"].ToString();
            string fullName = Server.MapPath(path + "/UG-Kaprodi.pdf");
            byte[] fileBytes = GetFile(fullName);
            return File(
            fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "User Guide Kaprodi.pdf");
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}