using OSCEUKDI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMRoleAccessMenu
    {
        public Int64? ID { get; set; }
        public Int64? MenuRoleID { get; set; }
        public string CodeRole { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public Int64? MenuID { get; set; }
        public string MenuName { get; set; }
        public Int64? RoleID { get; set; }
        public string RoleName { get; set; }
        public bool? IsView { get; set; }
        public bool? IsCreate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsUpdate { get; set; }
    }
    public class VMRoleAccessMenuStatus
    {
        public Boolean IsView { get; set; }
        public Boolean IsCreate { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsUpdate { get; set; }
        public Boolean Status { get; set; }
        public Int64 RoleID { get; set; }
        public Int64 MenuID { get; set; }
        public Int64 UserID { get; set; }
    }
}
