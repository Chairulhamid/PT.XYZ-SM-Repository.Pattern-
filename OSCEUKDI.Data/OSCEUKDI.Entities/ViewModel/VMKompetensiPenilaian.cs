using OSCEUKDI.Entities.Models.OSCEUKDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
   
    public class VMKompetensiPenilaian
    {
        public Int64 ID { get; set; }
        public Int64 RubrikKompetensiID { get; set; }
        public Int64 KompetensiID { get; set; }
        public string NamaKompetensi { get; set; }
        public Int32? N0 { get; set; }
        public Int32? N1 { get; set; }
        public Int32? N2 { get; set; }
        public Int32? N3 { get; set; }
        public Int32? N4 { get; set; }
        public Int32? N5 { get; set; }
        public string Desc0 { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string Desc4 { get; set; }
        public string Desc5 { get; set; }
        public Int64? Bobot { get; set; }
    }

}
