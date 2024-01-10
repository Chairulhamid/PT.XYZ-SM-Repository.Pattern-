using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMDropDown
    {
        public string Nama { get; set; }
        public string Nilai { get; set; }
    }
    public class VMDropDownTahunSemester
    {
        public string Nama { get; set; }
        public double Nilai { get; set; }
    }
    public class VMDropDownGetRuangUjian
    {
        public string Nama { get; set; }
        public Int64 Nilai { get; set; }
    }
}
