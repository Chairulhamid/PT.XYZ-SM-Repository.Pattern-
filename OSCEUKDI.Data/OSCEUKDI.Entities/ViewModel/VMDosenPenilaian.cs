using OSCEUKDI.Entities.Models.OSCEUKDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
   
    public class VMSoalPenilaian
    {
        public Int64 ID { get; set; }
        public string JudulStation { get; set; }
        public Int64 AlokasiWaktu { get; set; }
        public string TingkatKemampuan { get; set; }
        public string SistemKategoriTubuh { get; set; }
        public string InstruksiPeserta { get; set; }
        public string InstruksiPenguji { get; set; }
        public Int64 NomorStation { get; set; }
    }

}
