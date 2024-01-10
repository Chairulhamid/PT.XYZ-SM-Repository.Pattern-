using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{
    public class VMRubrikNilaiKomp
    {
        public List<Master> Master { get; set; }
        public List<Detail> Detail { get; set; }
    }
    public class Master
    {
        public Int64 ID { get; set; }
        public Int32 TahunSemester { get; set; }
        public string ACADCareer { get; set; }
        public string JudulStation { get; set; }
        public Int64 AlokasiWaktu { get; set; }
        public string TingkatKemampuan { get; set; }
        public string Classification { get; set; }
        public string InstruksiPeserta { get; set; }
        public string InstruksiPenguji { get; set; }

        public string InsNama { get; set; }
        public Int64 InsUsia { get; set; }
        public string Insgender { get; set; }
        public string InsPekerjaan { get; set; }
        public string InsStatusPerkawinan { get; set; }
        public string InsPendidikan { get; set; }
        public string InsRiwayatPenySekarang { get; set; }
        public string InsRiwayatPenyDahulu { get; set; }
        public string InsRiwayatKeluarga { get; set; }
        public string InsRiwayatKebiasaan { get; set; }
        public string InsPertanyaanPasien { get; set; }
        public string InsPeranPasien { get; set; }

        public string Kompetensi { get; set; }
        public string N0 { get; set; }
        public string N1 { get; set; }
        public string N2 { get; set; }
        public string N3 { get; set; }
        public string N4 { get; set; }
        public string N5 { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }

    }
    public class Detail
    {
        public Int64 ID { get; set; }
        public string Kompetensi { get; set; }
    }
}
