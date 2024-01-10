using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Presentation.Helper;
using OSCEUKDI.Presentation.models;
using OSCEUKDI.Repository.BaseRepository;
using OSCEUKDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows;

namespace OSCEUKDI.Presentation.Areas.Admin.Controllers
{
    public class UserManageController : Controller
    {
        private IMenuService _menuService;
        private IUserService _userService;
        private IMenuRoleService _menuRoleService;
        private IRoleService _roleService;
        List<VMDropDown> objRole = null;

        public UserManageController(IMenuService menuService, IUserService userService, IMenuRoleService menuRoleService, IRoleService roleService)
        {
            _menuService = menuService;
            _userService = userService;
            _menuRoleService = menuRoleService;
            _roleService = roleService;
            objRole = _roleService.GetRoles();
            ViewData["Role"] = objRole;
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
        public JsonResult GetAllUser(DataTableAjaxPostModel model)
        {
            var data = _userService.getAllData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GenerateRandomPassword()
        {
            Random ran = new Random();
            
            var numericString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var characters = "!@#$%^&*~(),.?|";

            int length = 6;

            var random = "";

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(numericString.Length); //string.Lenght gets the size of string
                random = random + numericString.ElementAt(a);
            }

            for (int j = 0; j < 2; j++)
            {
                int sz = ran.Next(characters.Length);
                random = random + characters.ElementAt(sz);
            }

            return Json(new { data = random });
        }
    public ActionResult CreatePage() 
        {
            //var objUser = _userService.GetAllUser();
            //ViewData["User"] = objUser;
            var objRole = _roleService.Find(xx => xx.IsActive == true && xx.IsDeleted == false).ToList();
            ViewData["Role"] = objRole;
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            string message = System.String.Empty;
            string result = "";
            string resultPwd = "";

            int status = 200;
            try
            {
                //var tmpEmail = _userService.Find(ip => ip.Email.ToLower() == user.Email.ToLower() && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                //var tmpNoPeg = _userService.Find(ip => ip.NoPegawai.ToLower() == user.NoPegawai.ToLower() && ip.IsActive == true && ip.IsDeleted == false).FirstOrDefault();
                //if (tmpEmail != null ) 
                //{
                //    status = 400;
                //    message = "Email sudah terdaftar.";
                //    goto SkipAct;
                //}
                //if (tmpNoPeg != null)
                //{
                //    status = 400;
                //    message = "Pegawai sudah terdaftar.";
                //    goto SkipAct;
                //}

                user.Password = HashPasswordService.HashPassword(user.Password);
                user.IsActive = true;
                user.CreatedBy = Session["username"] as string;
                user.UpdatedBy = Session["username"] as string;
                user.CreatedDate = DateTime.Now;
                user.UpdatedDate = DateTime.Now;
                user.StatusApproval = "Pending";
                _userService.Save(user);
                message = "Data berhasil ditambahkan!";
            SkipAct:
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public string ValidatePassword(string password)
        {
            var input = password;
            string ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one lower case letter" + Environment.NewLine;
            }
            if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one upper case letter" + Environment.NewLine;
            }
            if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage += "Password should not be less than or greater than 8 characters" + Environment.NewLine;
            }
            if (!hasNumber.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one numeric value" + Environment.NewLine;
            }

            if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one special case characters" + Environment.NewLine;
            }
            
            return ErrorMessage;
        }

        public ActionResult UpdatePage(int id)
        {
            var tmpData = _userService.Get(id);
            return View(tmpData);
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            string message = System.String.Empty;
            string resultPwd = "";
            int status = 200;

            try
            {
                var tmpUserName = _userService.Find(ip => ip.UserName == user.UserName && ip.ID != user.ID).FirstOrDefault();
                //var tmpNoPeg = _userService.Find(ip => ip.NoPegawai == user.NoPegawai && ip.ID != user.ID).FirstOrDefault();
                if (tmpUserName != null)
                {
                    message = "User Name";
                }
                //if (tmpNoPeg != null)
                //{
                //    message += "No Pegawai";
                //}

                //resultPwd = ValidatePassword(user.Password);
                //resultPwd = resultPwd != "" ? ("Password should : " + Environment.NewLine + resultPwd) : "";

                if (message != "")// || resultPwd != "")
                {
                    status = 400;
                    message = (message != "" ? ("Failed to update data. Some Fields below has been registered :" + Environment.NewLine + message) : "") + Environment.NewLine + resultPwd;
                    goto end_off;
                }

                User data = _userService.Get(user.ID);
                data.UserName = user.UserName;
                data.Password = HashPasswordService.HashPassword(user.Password); 
               // data.NoPegawai = user.NoPegawai;
                data.RoleID = user.RoleID;
                data.Email = user.Email;
                data.IsActive = true; // user.IsActive;
                data.UpdatedBy = Session["username"] as string;
                data.UpdatedDate = DateTime.Now;
                _userService.Save(data);
                message = "Data Updated Successfully!";
            end_off:
                return Json(new ServiceResponse { status = status, message = message });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        [HttpPost]
        public ActionResult ResetAccess(int id)
        {
            string message = System.String.Empty;
            int status = 200;
            try
            {
                var context = new OSCEUKDIContext();
                var res = context.MenuRoles.Where(x => x.UserID == id).ToList();
                foreach (var rem in res)
                {
                    context.MenuRoles.Remove(rem);
                    context.SaveChanges();
                }
                message = "Data Deleted Successfully!";
                return Json(new ServiceResponse { status = status, message = message });

            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }

        public ActionResult AccessMenu(int idRole, int idUser, string name)
        {
            ViewData["RoleID"] = idRole;
            ViewData["UserName"] = name;
            ViewData["UserID"] = idUser;
            
            var model = _userService.getUserAccessMenu(idUser);
            List<VMRoleAccessMenu> obj = new List<VMRoleAccessMenu>();
            foreach (var tmp in model)
            {
                obj.Add(new VMRoleAccessMenu
                {
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
            ViewData["DataUserAccess"] = obj;

            return View();
        }
        [HttpPost]
        public ActionResult StatusMenu(VMRoleAccessMenuStatus role)
        {
            try
            {
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.MenuID == role.MenuID && m.UserID == role.UserID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.RoleID = role.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;
                        tmp.IsView = true;
                        tmp.IsCreate = false;
                        tmp.IsUpdate = false;
                        tmp.IsDelete = false;
                        tmp.IsActive = role.Status;
                        tmp.IsDeleted = false;
                        tmp.CreatedBy = tmp.CreatedBy;
                        tmp.CreatedDate = tmp.CreatedDate;                       
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;

                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200, message = "Menu User Berhasil diupdate!" });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.IsActive = role.Status;
                        tmp.IsCreate = false;
                        tmp.IsView = false;
                        tmp.IsDeleted = true;

                        tmp.IsUpdate = false;
                        tmp.IsDelete = false;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;

                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200, message = "Menu User Berhasil diupdate!" });
                    }
                }
                else
                {
                    if (role.Status == true)
                    {
                        tmp.RoleID = role.RoleID;
                        tmp.MenuID = role.MenuID;
                        tmp.UserID = role.UserID;
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
                        return Json(new ServiceResponse { status = 200, message = "Menu User Berhasil ditambahkan!" });
                    }
                    else
                    {
                        tmp.RoleID = role.RoleID;
                        tmp.MenuID = role.MenuID;
                        tmp.UserID = role.UserID;
                        tmp.IsView = false;
                        tmp.IsCreate = false;
                        tmp.IsUpdate = false;
                        tmp.IsDelete = false;
                        tmp.CreatedBy = Session["username"] as string;
                        tmp.CreatedDate = DateTime.Now;
                        tmp.UpdatedDate = DateTime.Now;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.IsActive = false;
                        tmp.IsDeleted = true;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200, message = "Menu User Berhasil diupdate!" });
                    }
                }
            }

            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };

        }
        [HttpPost]
        public ActionResult StatusView(VMRoleAccessMenuStatus role)
        {
            try
            {
                var find = _menuRoleService.Find(m => m.MenuID == role.MenuID && m.UserID == role.UserID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.RoleID = tmp.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;
                        tmp.IsView = role.Status;
                        tmp.IsCreate = tmp.IsCreate;
                        tmp.IsUpdate = tmp.IsUpdate;
                        tmp.IsDelete = tmp.IsDelete;
                        tmp.IsActive = tmp.IsActive;
                        tmp.IsDeleted = tmp.IsDeleted;
                        tmp.CreatedBy = tmp.CreatedBy;
                        tmp.CreatedDate = tmp.CreatedDate;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.RoleID = tmp.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;                        
                        tmp.IsView = role.Status;
                        tmp.IsCreate = tmp.IsCreate;
                        tmp.IsUpdate = tmp.IsUpdate;
                        tmp.IsDelete = tmp.IsDelete;
                        tmp.IsActive = tmp.IsActive;
                        tmp.IsDeleted = tmp.IsDeleted;
                        tmp.CreatedBy = tmp.CreatedBy;
                        tmp.CreatedDate = tmp.CreatedDate;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    return Json(new ServiceResponse { status = 500, message = "Data User Menu Access Gagagl Diupdate!" });
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
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.UserID == role.UserID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.RoleID = tmp.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;
                        tmp.IsView = tmp.IsView;
                        tmp.IsCreate = role.Status;
                        tmp.IsUpdate = tmp.IsUpdate;
                        tmp.IsDelete = tmp.IsDelete;
                        tmp.IsActive = tmp.IsActive;
                        tmp.IsDeleted = tmp.IsDeleted;
                        tmp.CreatedBy = tmp.CreatedBy;
                        tmp.CreatedDate = tmp.CreatedDate;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.RoleID = tmp.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;
                        tmp.IsView = tmp.IsView;
                        tmp.IsCreate = role.Status;
                        tmp.IsUpdate = tmp.IsUpdate;
                        tmp.IsDelete = tmp.IsDelete;
                        tmp.IsActive = tmp.IsActive;
                        tmp.IsDeleted = tmp.IsDeleted;
                        tmp.CreatedBy = tmp.CreatedBy;
                        tmp.CreatedDate = tmp.CreatedDate;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    if (role.Status == true)
                    {
                        tmp.RoleID = role.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;
                        tmp.IsView = true;
                        tmp.IsCreate = role.Status;
                        tmp.IsUpdate = tmp.IsUpdate;
                        tmp.IsDelete = tmp.IsDelete;
                        tmp.IsActive = true;
                        tmp.IsDeleted = tmp.IsDeleted;
                        tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                        tmp.CreatedDate = tmp.CreatedDate;
                        tmp.UpdatedBy = tmp.UpdatedBy;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.RoleID = role.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.MenuID = role.MenuID;
                        tmp.IsView = true;
                        tmp.IsCreate = role.Status;
                        tmp.IsUpdate = tmp.IsUpdate;
                        tmp.IsDelete = tmp.IsDelete;
                        tmp.IsActive = true;
                        tmp.IsDeleted = tmp.IsDeleted;
                        tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                        tmp.CreatedDate = tmp.CreatedDate;
                        tmp.UpdatedBy = tmp.UpdatedBy;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
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
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.UserID == role.UserID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);

                    tmp.MenuID = role.MenuID;
                    tmp.UserID = role.UserID;
                    tmp.IsUpdate = role.Status;
                    tmp.UpdatedBy = Session["username"] as string;
                    tmp.UpdatedDate = DateTime.Now;
                    _menuRoleService.Save(tmp);
                    return Json(new ServiceResponse { status = 200 });

                    #region create find data
                    //if (role.Status == true)
                    //{
                    //    tmp.MenuID = role.MenuID;
                    //    tmp.UserID = role.UserID;

                    //    tmp.IsUpdate = role.Status;

                    //    tmp.UpdatedBy = Session["username"] as string;
                    //    tmp.UpdatedDate = DateTime.Now;
                    //    _menuRoleService.Save(tmp);
                    //    return Json(new ServiceResponse { status = 200 });
                    //}
                    //else
                    //{
                    //    tmp.MenuID = role.MenuID;
                    //    tmp.UserID = role.UserID;

                    //    tmp.IsUpdate = role.Status;

                    //    tmp.UpdatedBy = Session["username"] as string;
                    //    tmp.UpdatedDate = DateTime.Now;
                    //    _menuRoleService.Save(tmp);
                    //    return Json(new ServiceResponse { status = 200 });
                    //}
                    #endregion
                }
                else
                {
                    tmp.MenuID = role.MenuID;
                    tmp.RoleID = role.RoleID;
                    tmp.UserID = role.UserID;
                    tmp.IsView = true;
                    tmp.IsActive = true;
                    tmp.IsUpdate = role.Status;
                    tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                    tmp.UpdatedDate = DateTime.Now;
                    _menuRoleService.Save(tmp);
                    return Json(new ServiceResponse { status = 200 });

                    #region create data
                    //if (role.Status == true)
                    //{
                    //    tmp.MenuID = role.MenuID;
                    //    tmp.RoleID = role.RoleID;
                    //    tmp.UserID = role.UserID;
                    //    tmp.IsView = true;
                    //    tmp.IsActive = true;
                    //    tmp.IsUpdate = role.Status;
                    //    tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                    //    tmp.UpdatedDate = DateTime.Now;
                    //    _menuRoleService.Save(tmp);
                    //    return Json(new ServiceResponse { status = 200 });
                    //}
                    //else
                    //{
                    //    tmp.MenuID = role.MenuID;
                    //    tmp.RoleID = role.RoleID;
                    //    tmp.UserID = role.UserID;
                    //    tmp.IsView = true;
                    //    tmp.IsActive = true;
                    //    tmp.IsUpdate = role.Status;
                    //    tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                    //    tmp.UpdatedDate = DateTime.Now;
                    //    _menuRoleService.Save(tmp);
                    //    return Json(new ServiceResponse { status = 200 });
                    //}
                    #endregion
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
                var find = _menuRoleService.Find(m => m.RoleID == role.RoleID && m.UserID == role.UserID && m.MenuID == role.MenuID).FirstOrDefault();
                MenuRole tmp = new MenuRole();
                if (find != null)
                {
                    tmp = _menuRoleService.Get(find.ID);
                    if (role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.UserID = role.UserID;
                        tmp.IsDelete = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.UserID = role.UserID;
                        tmp.IsDelete = role.Status;
                        tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                }
                else
                {
                    if (role.Status == true)
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.IsView = true;
                        tmp.IsActive = true;
                        tmp.IsDelete = role.Status;
                        tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    else
                    {
                        tmp.MenuID = role.MenuID;
                        tmp.RoleID = role.RoleID;
                        tmp.UserID = role.UserID;
                        tmp.IsView = true;
                        tmp.IsActive = true;
                        tmp.IsDelete = role.Status;
                        tmp.CreatedBy = tmp.UpdatedBy = Session["username"] as string;
                        tmp.UpdatedDate = DateTime.Now;
                        _menuRoleService.Save(tmp);
                        return Json(new ServiceResponse { status = 200 });
                    }
                    //return Json(new ServiceResponse { status = 500, message = "Data User Menu Access Gagagl Diupdate!" });
                }
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            };
        }

        public void VisibilityStatusCRUD(MenuRole tmp, int RoleID, int UserID, int MenuID, bool IsView, bool IsCreate, bool IsUpdate, bool IsDelete, bool CreatedBy, bool UpdatedDate)
        {
            tmp.RoleID = RoleID;
            tmp.UserID = UserID;
            tmp.MenuID = MenuID;
            tmp.IsView = IsView;
            tmp.IsCreate = IsCreate;
            tmp.IsUpdate = IsUpdate;
            tmp.IsDelete = IsDelete;
            tmp.IsActive = tmp.IsActive;
            tmp.IsDeleted = tmp.IsDeleted;
            //tmp.CreatedBy = CreatedBy;
            tmp.CreatedDate = tmp.CreatedDate;
            tmp.UpdatedBy = Session["username"] as string;
            //tmp.UpdatedDate = UpdatedDate;
        }
      
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var data = _userService.Get(id);
                data.IsDeleted = true;
                data.IsActive = false;
                data.UpdatedBy = Session["username"] as string;
                data.UpdatedDate = DateTime.Now;
                _userService.Save(data);
                return Json(new ServiceResponse { status = 200, message = "Data Berhasil Dihapus!" });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }

        //New
        public ActionResult Vendor()
        {
            ViewData["isCreate"] = HttpContext.Session["isCreate"].ToString();
            ViewData["isUpdate"] = HttpContext.Session["isUpdate"].ToString();
            ViewData["isDelete"] = HttpContext.Session["IsDelete"].ToString();
            return View();
        }
        [HttpPost]
        public JsonResult GetAllVendor()
        {
            int RoleId = int.Parse(Session["RoleID"].ToString());

            var data = _userService.getAllVendor(RoleId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateVendor(int id)
        {
            var tmpData = _userService.Get(id);
            return View(tmpData);
        }
        [HttpPost]
        public ActionResult UpdateDataVendor(User user)
        {
            try
            {
                User data = _userService.Get(user.ID);
                data.UserName= user.UserName;
                data.Phone= user.Phone;
                data.BidangUsaha= user.BidangUsaha;
                data.JenisPerusahaan= user.JenisPerusahaan;
                data.StatusApproval= user.StatusApproval;
                data.UpdatedBy = Session["username"] as string;
                data.UpdatedDate = DateTime.Now;
                _userService.Save(data);
                return Json(new ServiceResponse { status = 200, message = "Data Berhasil Diupdate!" });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
    }
}

