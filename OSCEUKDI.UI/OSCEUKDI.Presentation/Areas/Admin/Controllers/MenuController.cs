using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Presentation.Helper;
using OSCEUKDI.Presentation.models;
using OSCEUKDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace OSCEUKDI.Presentation.Areas.Admin.Controllers
{
    [HRISAuthorize]
    public class MenuController : Controller
    {
        private IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewData["isCreate"] = HttpContext.Session["isCreate"].ToString();
            ViewData["isUpdate"] = HttpContext.Session["isUpdate"].ToString();
            ViewData["isDelete"] = HttpContext.Session["IsDelete"].ToString();
            return View();
        }
        
        //get data table
        [HttpPost]
        public JsonResult GetDataMenu(DataTableAjaxPostModel model)
        {
            //VMListMenu vMListMenu = _menuService.getMenu(model);
            //return Json(new
            //{
            //    // this is what datatables wants sending back
            //    draw = model.draw,
            //    recordsTotal = vMListMenu.TotalCount,
            //    recordsFiltered = vMListMenu.TotalFilterCount,
            //    data = vMListMenu.gridDatas
            //});
            var data = _menuService.getAllData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //GET
        public ActionResult CreatePage()
        {         
            var objMParent = _menuService.getListMenuParent();
            ViewData["MenuParent"] = objMParent;
            return View();
        }
        [HttpPost]
        public ActionResult AddMenu(Menu menu)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var tmpName = _menuService.Find(ip => ip.MenuName.ToLower() == menu.MenuName.ToLower() &&  ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                var tmpUrl = _menuService.Find(ip => ip.MenuUrl.ToLower() == menu.MenuUrl.ToLower() && ip.MenuUrl != "#"  &&  ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                if (tmpName != null)
                {
                    status = 400;
                    message = "Failed to add data. The Menu Name has been registered!";
                    goto SkipAct;
                } else if (tmpUrl != null ) 
                {
                    status = 400;
                    message = "Failed to add data. The Menu Url has been registered!";
                    goto SkipAct;
                }
                    if (menu.MenuParent == "parent") {
                        menu.MenuParent = null;
                    }
                    menu.CreatedBy = Session["username"] as string;
                    menu.UpdatedBy = Session["username"] as string;
                    menu.CreatedDate = DateTime.Now;
                    menu.UpdatedDate = DateTime.Now;
                    menu.IsActive = true;
                    _menuService.Save(menu);
                    message = "Data Successfully Added!";
                    SkipAct:
                    return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult UpdatePage (int id)
        {
            var tmpData = _menuService.Get(id);
            var tmpMenuParent = _menuService.Get(id).MenuParent;
            var objMParent = _menuService.getListMenuParent();
            if (tmpMenuParent != null) { 
            ViewData["MenuParent"] = objMParent.Where(i => i.Nama != _menuService.Get(Int64.Parse(tmpMenuParent)).MenuName);
            ViewData["MenuParentUpdt"] = _menuService.Get(Int64.Parse(tmpMenuParent)).MenuName;
            }
            else
            {
            ViewData["MenuParent"] = objMParent;
            }

            return View(tmpData);
        }
        [HttpPost]
        public ActionResult UpdateMenu(Menu menu)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var tmpName = _menuService.Find(ip => ip.MenuName.ToLower() == menu.MenuName.ToLower() && ip.ID != menu.ID && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                var tmpUrl = _menuService.Find(ip => ip.MenuUrl.ToLower() == menu.MenuUrl.ToLower() && ip.ID != menu.ID && ip.MenuUrl != "#" && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                if (tmpName != null)
                {
                    status = 400;
                    message = "Failed to update data. The Menu Name has been registered!";
                    goto SkipAct;
                }
                else if (tmpUrl != null)
                {
                    status = 400;
                    message = "Failed to update data. The Menu Url has been registered!";
                    goto SkipAct;
                }
                Menu data = _menuService.Get(menu.ID);
                data.MenuName = menu.MenuName;
                data.MenuDescription = menu.MenuDescription;
                data.MenuUrl = menu.MenuUrl;
                data.MenuIcon = menu.MenuIcon;
                if (menu.MenuParent == "parent")
                {
                    menu.MenuParent = null;
                }
                data.MenuParent = menu.MenuParent;
                data.MenuOrder = menu.MenuOrder;
                data.UpdatedBy = Session["username"] as string;
                data.UpdatedDate = DateTime.Now;
                _menuService.Save(data);
                message = "Data Successfully Updated!";
                SkipAct:
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        /*Delete*/
        [HttpPost]
        public ActionResult DeleteMenu(int id)
        {
            var data = _menuService.Get(id);
            data.IsDeleted = true;
            data.IsActive = false;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;

            _menuService.Save(data);
            return Json(data);
        }
    }
}