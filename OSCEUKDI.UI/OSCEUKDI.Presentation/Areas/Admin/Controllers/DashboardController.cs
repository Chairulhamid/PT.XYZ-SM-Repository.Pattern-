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
    public class DashboardController : Controller
    {
        private IMenuService _menuService;

        public DashboardController(IMenuService menuService)
        {
            _menuService = menuService;

        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}