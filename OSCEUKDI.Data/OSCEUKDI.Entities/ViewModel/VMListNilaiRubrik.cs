using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListNilaiRubrik
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridKompetensi> gridDatas { get; set; }
    }
    public class GridNilaiRubrik
    {
        public Int64 ID { get; set; }
        public Int64 KompetensiID { get; set; }
        public string Nilai0 { get; set; }
        public string Nilai1 { get; set; }
        public string Nilai2 { get; set; }
        public string Nilai3 { get; set; }
        public string Nilai4 { get; set; }
        public string Nilai5 { get; set; }
        public Int64 Bobot { get; set; }
        public bool Status { get; set; }
    }

}
