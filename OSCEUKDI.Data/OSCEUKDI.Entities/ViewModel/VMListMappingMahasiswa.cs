using OSCEUKDI.Entities.Models.OSCEUKDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListMappingMahasiswa
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridMappingMahasiswa> gridDatas { get; set; }
    }
    public class GridMappingMahasiswa
    {
        public Int64 ID { get; set; }
        public Int64 TahunSemester { get; set; }
        public Int64 JadwalTestID { get; set; }
        public string ACADCareer { get; set; }
        public string AcadProg { get; set; }
        public string NamaMahasiswa { get; set; }
        public string JadwalTest { get; set; }
        public bool Status { get; set; }
    }
    public class GridMappingMahasiswaDetail
    {
        public Int64 ID { get; set; }
        public Int64 MappingMahasiswaID { get; set; }
        public Int64 TahunAngkatan { get; set; }
        public string MahasiswaID { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

}
