using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Presentation.Helper;
using OSCEUKDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSCEUKDI.Presentation.Areas.Admin.Controllers
{
    [HRISAuthorize]
    public class HomeController : Controller
    {
        private IMenuService _menuService;
        private IUserService _userService;

        public HomeController(IMenuService menuService, IUserService userService)
        {
            _menuService = menuService;
            _userService = userService;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            var email = Session["email"].ToString();
            var checkUser = _userService.Find(xx => xx.Email == email).FirstOrDefault();
            if (checkUser.RoleID == 19)
            {
                ViewData["StatusApproval"] = checkUser.StatusApproval.ToString();
                ViewData["BidangUsaha"] = "";
                ViewData["JenisPerusahaan"] = "";
                if (checkUser.BidangUsaha != null)
                {
                    ViewData["BidangUsaha"] = checkUser.BidangUsaha.ToString();
                    ViewData["JenisPerusahaan"] = checkUser.JenisPerusahaan.ToString();
                }
            }
            return View();
        }

        //TESTING HOME GIT
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult PerjanjianKerjasama()
        {
            return View();
        }

    }
}