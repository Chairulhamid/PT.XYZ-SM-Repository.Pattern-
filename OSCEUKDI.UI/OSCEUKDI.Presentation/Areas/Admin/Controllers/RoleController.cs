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
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OSCEUKDI.Presentation.Areas.Admin.Controllers
{
    [HRISAuthorize]
    public class RoleController : Controller
    {
        private IMenuService _menuService;
        private IRoleService _roleService;
        private IMenuRoleService _menuRoleService;
        public RoleController(IMenuService menuService, IRoleService roleService,IMenuRoleService menuRoleService)
        {
            _menuService = menuService;
            _roleService = roleService;
            _menuRoleService= menuRoleService;

        }
        // GET: Admin/Role
        public ActionResult Index()
        {
            ViewData["isCreate"] = HttpContext.Session["isCreate"].ToString();
            ViewData["isUpdate"] = HttpContext.Session["isUpdate"].ToString();
            ViewData["isDelete"] = HttpContext.Session["IsDelete"].ToString();
            return View();
        }
        [HttpPost]
        public JsonResult GetDataRole(DataTableAjaxPostModel model)
        {
            var data = _roleService.getRole();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreatePage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var tmpCode = _roleService.Find(ip => ip.Code.ToLower() == role.Code.ToLower() && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                if (tmpCode == null) { 
                    role.IsActive = true;
                    role.CreatedBy = Session["username"] as string;
                    role.UpdatedBy = Session["username"] as string;
                    role.CreatedDate = DateTime.Now;
                    role.UpdatedDate = DateTime.Now;
                    _roleService.Save(role);
                    message = "Data Successfully Added!";
                    return Json(new ServiceResponse { status = status, message = message });
                }
                status = 400;
                message = "Failed to add data. The code has been registered!";
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }

        }

        public ActionResult UpdatePage(int id)
        {
            var tmpData = _roleService.Get(id);
            return View(tmpData);
        }

        [HttpPost]
        public ActionResult UpdateRole(Role role)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var tmpCode = _roleService.Find(ip => ip.Code.ToLower() == role.Code.ToLower()&& ip.ID != role.ID && ip.IsActive == true && ip.IsDeleted == false ).FirstOrDefault();
                if (tmpCode == null)
                {
                    Role data = _roleService.Get(role.ID);
                    data.RoleName = role.RoleName;
                    data.UpdatedBy = Session["username"] as string;
                    data.UpdatedDate = DateTime.Now;
                    _roleService.Save(data);
                    message = "Data Successfully Updated!";
                    return Json(new ServiceResponse { status = 200, message = message });
                }
                status = 400;
                message = "Failed to update data. The code has been registered!";
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        /*Delete*/
        [HttpPost]
        public ActionResult DeleteRole(int id)
        {
            var data = _roleService.Get(id);
            data.IsDeleted = true;
            data.IsActive = false;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;
            _roleService.Save(data);
            return Json(data);
        }
        public ActionResult AccessMenu(int idRole, string name)
        {
            ViewData["RoleName"] = name;
            ViewData["RoleID"] = idRole;
            var id = idRole;
            var model = _roleService.getRoleAccessMenu(id);
            List<VMRoleAccessMenu> obj = new List<VMRoleAccessMenu>();
            foreach (var tmp in model)
            {
                obj.Add(new VMRoleAccessMenu {
                    MenuRoleID = tmp.MenuRoleID,
                    CodeRole = tmp.CodeRole,
                    MenuID = tmp.MenuID,
                    MenuName = tmp.MenuName,
                    RoleName = tmp.RoleName,
                    RoleID = tmp.RoleID,
                    IsView = tmp.IsView,
                    IsCreate = tmp.IsCreate,
                    IsUpdate = tmp.IsUpdate,
                    IsDelete = tmp.IsDelete,
                    IsDeleted = tmp.IsDeleted,
                    IsActive = tmp.IsActive,
                });
            }
            ViewData["DataRoleAccess"] = obj;

            return View();
        }
        [HttpPost]
        public ActionResult StatusMenu(VMRoleAccessMenuStatus role)
        {
            try
            {
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.MenuID == role.MenuID && m.UserID == null).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null) {
                    tmp = _menuRoleService.Get(find.ID);
                    if ( role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsActive = role.Status;
                        tmp.IsView = true;
                        tmp.IsDeleted = false;
                        tmp.IsCreate = false;
                        tmp.IsUpdate = false;
                        tmp.IsDelete = false;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200, message = "Menu Role Berhasil diupdate!" });
                    } else{
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsActive = role.Status;
                        tmp.IsCreate = false;
                        tmp.IsView = false;
                        tmp.IsUpdate = false;
                        tmp.IsDelete = false;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        tmp.IsDeleted= true;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200, message = "Menu Role Berhasil diupdate!" });
                    }
                }
                else
                {
                    tmp.RoleID = role.RoleID;
                    tmp.MenuID = role.MenuID;
                    tmp.IsView = true;
                    tmp.IsCreate = false;
                    tmp.IsUpdate = false;
                    tmp.IsDelete = false;
                    tmp.CreatedBy = Session["username"] as string;
                    tmp.CreatedDate = DateTime.Now;
                    tmp.UpdatedDate = DateTime.Now;
                    tmp.UpdatedBy = Session["username"] as string;
                    tmp.IsActive = true;
                    tmp.IsDeleted = false;
                    _menuRoleService.Save(tmp);
                    return Json(new ServiceResponse { status = 200, message = "Menu Role Berhasil ditambahkan!" });
                }
            }

            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };
        }
        [HttpPost]
        public ActionResult StatusView(VMRoleAccessMenuStatus role) {
            try
            {
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsView = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsView = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    return Json(new ServiceResponse { status = 500, message = "Data Role Menu Access Gagagl Diupdate!" });
                }
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };
        }
        [HttpPost]
        public ActionResult StatusCreate(VMRoleAccessMenuStatus role)
        {
            try
            {
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsCreate = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsCreate = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    return Json(new ServiceResponse { status = 500, message = "Data Role Menu Access Gagagl Diupdate!" });
                }
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };
        }
        [HttpPost]
        public ActionResult StatusUpdate(VMRoleAccessMenuStatus role)
        {
            try
            {
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsUpdate = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsUpdate = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    return Json(new ServiceResponse { status = 500, message = "Data Role Menu Access Gagagl Diupdate!" });
                }
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };
        }
        [HttpPost]
        public ActionResult StatusDelete(VMRoleAccessMenuStatus role)
        {
            try
            {
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsDelete = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.IsDelete = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    return Json(new ServiceResponse { status = 500, message = "Data Role Menu Access Gagagl Diupdate!" });
                }
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };
        }
    }
}