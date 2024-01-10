using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListRubrikKompetensi
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridRubrikKompetensi> gridDatas { get; set; }
    }
    public class GridRubrikKompetensi
    {
        public Int64 ID { get; set; }
        public Int64 TahunSemester { get; set; }
        public string ACADCareer { get; set; }
        public string JudulStation { get; set; }
        public Int64 AlokasiWaktu { get; set; }
        public string TingkatKemampuan { get; set; }
        public string Kompetensi { get; set; }
        public string Classification { get; set; }
        public string N0 { get; set; }
        public string N1 { get; set; }
        public string N2 { get; set; }
        public string N3 { get; set; }
        public string N4 { get; set; }
        public string N5 { get; set; }
        public bool Status { get; set; }
    }

}
