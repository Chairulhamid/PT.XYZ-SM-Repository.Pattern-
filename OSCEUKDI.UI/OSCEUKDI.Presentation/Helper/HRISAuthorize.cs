using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OSCEUKDI.Presentation.Helper
{
    public class HRISAuthorize : ActionFilterAttribute
    {
        private IMenuRoleService _menuRoleService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            string area = filterContext.RequestContext.RouteData.DataTokens["area"].ToString();
            
            if (area.ToLower() == "admin")
            {
                filterContext.HttpContext.Session["emailMahasiswa"] = null;
                if (filterContext.HttpContext.Session["email"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Admin/Adminlogin/Login");
                    return;
                }
                string controllername = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                string actionname = filterContext.RequestContext.RouteData.Values["action"].ToString();
                bool isauthorize = true;
                string url = "/" + area + "/" + controllername;
                filterContext.HttpContext.Session["urlActive"] = url;
                if (controllername.ToLower() == "jadwalkuliah")
                {
                    if (actionname.ToLower() == "manage")
                        url = url + "/" + actionname;
                }
                if (filterContext.HttpContext.Session["MenuList"] == null)
                {
                    //TODO mengambil data menu berdasarkan Role ID
                    
                    _menuRoleService = DependencyResolver.Current.GetService<IMenuRoleService>();
                    double RoleId = Double.Parse(filterContext.HttpContext.Session["RoleID"].ToString());
                    double UserID = Double.Parse(filterContext.HttpContext.Session["userid"].ToString());

                    var menuRoleListByUserIdActive = _menuRoleService.Find(x => x.IsActive == true && x.IsDeleted == false && x.RoleID == RoleId && x.IsView == true && x.UserID == UserID).ToList();

                    var menuRoleListByUserIdNoActive = _menuRoleService.Find(x => x.IsActive == false && x.IsDeleted == true && x.RoleID == RoleId && x.IsView == false && x.UserID == UserID).ToList();

                    menuRoleListByUserIdNoActive.AddRange(menuRoleListByUserIdActive);

                    var menuRoleList = _menuRoleService.Find(x => x.IsActive == true && x.IsDeleted == false && x.RoleID == RoleId && x.IsView == true && x.UserID == null).ToList();

                    var menuRoleListShow = menuRoleList.Where(ds => !menuRoleListByUserIdNoActive.Any(db => db.MenuID == ds.MenuID) ).ToList();

                    menuRoleListShow.AddRange(menuRoleListByUserIdActive);

                    filterContext.HttpContext.Session["MenuList"] = menuRoleListShow.Select(x => x.Menus).Where(y => y.MenuParent == null && y.IsActive == true && y.IsDeleted == false).OrderBy(x=>x.MenuOrder).ToList();

                    filterContext.HttpContext.Session["MenuListSub"] = menuRoleListShow.Select(x => x.Menus).Where(y => y.MenuParent != null && y.IsActive == true && y.IsDeleted == false).OrderBy(x => x.MenuOrder).ToList();
                    isauthorize = menuRoleListShow.Any(x => x.IsView == true && x.Menus.MenuUrl.ToLower() == url.ToLower());
                    filterContext.HttpContext.Session["MenuRole"] = menuRoleListShow;
                    var accessMenu = menuRoleListShow.Where(x => x.Menus.MenuUrl.ToLower() == url.ToLower()).FirstOrDefault();
                    if (accessMenu != null)
                    {
                        filterContext.HttpContext.Session["isCreate"] = accessMenu.IsCreate;
                        filterContext.HttpContext.Session["isUpdate"] = accessMenu.IsUpdate;
                        filterContext.HttpContext.Session["isDelete"] = accessMenu.IsDelete;
                        filterContext.HttpContext.Session["MenuParentID"] = (accessMenu.Menus.MenuParent == null) ? "0" : accessMenu.Menus.MenuParent.ToString();
                    }
                    if (!isauthorize)
                    {
                        filterContext.Result = new RedirectResult("~/UnAuthorized");
                        return;
                    }
                }
                else
                {
                    var ListMenuRole = (List<MenuRole>)filterContext.HttpContext.Session["MenuRole"];
                    isauthorize = ListMenuRole.Any(x => x.IsView == true && x.Menus.MenuUrl.ToLower() == url.ToLower());
                    var accessMenu = ListMenuRole.Where(x => x.Menus.MenuUrl.ToLower() == url.ToLower()).FirstOrDefault();
                    if (accessMenu != null)
                    {
                        filterContext.HttpContext.Session["isCreate"] = accessMenu.IsCreate;
                        filterContext.HttpContext.Session["isUpdate"] = accessMenu.IsUpdate;
                        filterContext.HttpContext.Session["isDelete"] = accessMenu.IsDelete;
                        filterContext.HttpContext.Session["MenuParentID"] = (accessMenu.Menus.MenuParent == null) ? "0" : accessMenu.Menus.MenuParent.ToString();
                    }
                    if (!isauthorize)
                    {
                        filterContext.Result = new RedirectResult("~/UnAuthorized");
                        return;
                    }
                }
            }
            else if (area.ToLower() == "portal")
            {
                if (filterContext.HttpContext.Session["emailMahasiswa"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Portal/Home");
                    return;
                }
            }
        }     
    }
}