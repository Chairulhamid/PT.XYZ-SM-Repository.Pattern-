using OSCEUKDI.Entities.Models.OSCEUKDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
   
    public class VMDosenPenilaian
    {
        public Int64 ID { get; set; }
        public Int64 TahunSemester { get; set; }
        public string ACADCareer { get; set; }
        public Int64 NoStation { get; set; }
        public string NIPDosenInti { get; set; }
        public string Nama1 { get; set; }
        public string NIPDosen2 { get; set; }
        public string Nama2 { get; set; }
        public string NIPDosen3 { get; set; }
        public string Nama3 { get; set; }
        public string NamaRuangan { get; set; }
        public string KategoriOsce { get; set; }
        public string NamaPeriode { get; set; }
        public string TanggalUjian { get; set; }

    }

}
