using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListRuangUjian
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridRuangUjian> gridDatas { get; set; }
    }
    public class GridRuangUjian
    {
        public Int64 ID { get; set; }  
        public string NamaRuangan { get; set; }
        public bool Status { get; set; }
    }

}
