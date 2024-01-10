using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListKompetensi
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridKompetensi> gridDatas { get; set; }
    }
    public class GridKompetensi 
    {
        public Int64 ID { get; set; }  
        public string NamaKompetensi { get; set; }
        public string KategoriOsce { get; set; }
        public bool Status { get; set; }
    }

}
