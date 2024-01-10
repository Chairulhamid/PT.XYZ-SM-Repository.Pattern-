using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListMappingPertanyaan
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridMappingPertanyaan> gridDatas { get; set; }
    }
    public class GridMappingPertanyaan
    {
        public Int64 ID { get; set; }
        public Int64 TahunSemester { get; set; }
        public string ACADCareer { get; set; }
        public Int64 NomorStation { get; set; }
        public string NIPDosenInti { get; set; }
        public string NIPDosen2 { get; set; }
        public string NIPDosen3 { get; set; }
        public string JadwalTest { get; set; }
        public string JudulStation { get; set; }
        public string NamaRuangan { get; set; }
        public bool Status { get; set; }
    }

}
