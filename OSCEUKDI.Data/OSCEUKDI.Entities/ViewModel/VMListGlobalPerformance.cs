using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMListGlobalPerformance
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridGlobalPerformance> gridDatas { get; set; }
    }
    public class GridGlobalPerformance
    {
        public Int64 ID { get; set; }
        public Int64 TahunSemester { get; set; }
        public string ACADCareer { get; set; }
        public string JudulStation { get; set; }
        public string Nama { get; set; }
        public Int64 NMin { get; set; }
        public Int64 NMax { get; set; }
        public bool Status { get; set; }
    }

}
