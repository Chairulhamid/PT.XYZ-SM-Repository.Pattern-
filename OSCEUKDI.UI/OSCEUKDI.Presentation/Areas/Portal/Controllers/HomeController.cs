using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Presentation.models;
using OSCEUKDI.Services;
using OSCEUKDI.Services.OSCEUKDIServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSCEUKDI.Presentation.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Portal/Home 
        public ActionResult Index()
        {
            return View();
        }
    }
}