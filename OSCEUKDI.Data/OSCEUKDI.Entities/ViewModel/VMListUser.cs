using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListUser
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridData> gridDatas { get; set; }
    }
    public class GridData
    {
        public Int64 ID { get; set; }
        public string NoPegawai { get; set; }
        public string UserName { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double RoleID { get; set; }
        public string RoleName { get; set; }
        public string Phone { get; set; }
        public string BidangUsaha { get; set; }
        public string JenisPerusahaan { get; set; }
        public string Photo { get; set; }
        public string StatusApproval { get; set; }
        public bool Status { get; set; }
    }
}
