using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListJadwalTest
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridJadwalTest> gridDatas { get; set; }
    }
    public class GridJadwalTest
    {
        public Int64 ID { get; set; }  
        public string ACADCareer { get; set; }
        public Int64 TahunSemester { get; set; }
        public string NamaPeriode { get; set; }
        public string KategoriOsce { get; set; }
        public string TanggalUjian { get; set; }
        public bool Status { get; set; }
    }

}
