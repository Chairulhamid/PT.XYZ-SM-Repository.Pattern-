using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMRubrikNilai
    {
        public Int64 ID { get; set; }
        
        public Int64 RubrikKompetensiID { get; set; }
        public Int64 TahunSemester { get; set; }
        public Int64 KompetensiID { get; set; }
        public string ACADCareer { get; set; }
        public string DESCR { get; set; }
        public string Kompetensi { get; set; }
        public string JudulStation { get; set; }
        public Int64 AlokasiWaktu { get; set; }
        public string TingkatKemampuan { get; set; }
        public Int64? IDNilaiRubrik { get; set; }
        public string Nilai0 { get; set; }
        public string Nilai1 { get; set; }
        public string Nilai2 { get; set; }
        public string Nilai3 { get; set; }
        public string Nilai4 { get; set; }
        public string Nilai5 { get; set; }
        public Int64? Bobot { get; set; }
    }
}
