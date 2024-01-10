using OSCEUKDI.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OSCEUKDI.Entities.Models
{
    public class MenuRole : BaseEntity
    {
        public Boolean IsView { get; set; }
        public Boolean IsCreate { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsUpdate { get; set; }
        public Int64 RoleID { get; set; }
       
        public Int64? UserID { get; set; }
        public Int64 MenuID { get; set; }
        public virtual Menu Menus { get; set; }
        public virtual Role Roles { get; set; }
        public virtual User Users { get; set; }
    }
}
