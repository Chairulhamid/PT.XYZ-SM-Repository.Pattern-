using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMMappingMahasiswa
    {
        public List<MasterMappingMhs> Master { get; set; }
        public List<DetailMappingMhs> Detail { get; set; }
    }
    public class MasterMappingMhs
    {
        public Int64 ID { get; set; }
        public Int64 TahunSemester { get; set; }
        public Int64 TahunAngkatanMhs { get; set; }
        public string ACADCareer { get; set; }
        public string AcadProg { get; set; }
        public string NamaMahasiswa { get; set; }
        public Int64 JadwalTestID { get; set; }
        public Int64 RuangUjianID { get; set; }
    }
    public class DetailMappingMhs
    {
        public string CampusID { get; set; }
        public Int64 TahunAngkatan { get; set; }
    }
}
