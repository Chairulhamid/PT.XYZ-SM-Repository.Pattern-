using OSCEUKDI.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OSCEUKDI.Entities.Models
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage ="testing")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        //[Required]
        //public string NoPegawai { get; set; }
        public string Phone { get; set; }
        public string BidangUsaha { get; set; }
        public string JenisPerusahaan { get; set; }
        public string Photo { get; set; }
        public string StatusApproval { get; set; }
        [Required]
        public Int64 RoleID { get; set; }
        public virtual Role Roles { get; set; }
    }
}
