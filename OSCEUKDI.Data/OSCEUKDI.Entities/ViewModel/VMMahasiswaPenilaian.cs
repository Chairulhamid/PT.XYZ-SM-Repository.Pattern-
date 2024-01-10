using OSCEUKDI.Entities.Models.OSCEUKDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.ViewModel
{

    public class VMMahasiswaPenilaian
    {
        public Int64 MhsID { get; set; }
        public string Nama { get; set; }
        public string MahasiswaID { get; set; }
        public Int64 TahunAngkatan { get; set; }
        public string ProgramStudi { get; set; }
        public Int32 Status { get; set; }
        public string TanggalUjian { get; set; }
    }
    public class VMDetailMhs
    {
        public Int64 MhsID { get; set; }
        public string Nama { get; set; }
        public string MahasiswaID { get; set; }
        public string TahunAngkatan { get; set; }
        public string ProgramStudi { get; set; }
    }

    public class VMListPenilaianMhs
    {
        public List<VMPenilaianDatatable> datatable { get; set; }
        public List<VMPdataGlobal> dataGlobal { get; set; }

    }
    public class VMPenilaianDatatable
    {
        public string MahasiswaID { get; set; }
        public Int64 TahunAngkatan { get; set; }
        public Int64 Nilai { get; set; }
        public Int64 RubrikKompetensiID { get; set; }
        public Int64 RubrikNilaiKompID { get; set; }
        public Int64 Bobot { get; set; }
        public Int64 NomorStation { get; set; }
    }
    public class VMPdataGlobal
    {
        public string GlobalRating { get; set; }
        public string GlobalRatingName { get; set; }
    }
    public class VMDetailModalMhs
    {
        public Int64 Id { get; set; }
        public string MahasiswaID { get; set; }
        public string Nama { get; set; }
        public Int32 Status { get; set; }
        public string Tanggal { get; set; }
    }
    public class VMDetailNilaiModalMhs
    {
        public String NamaKompetensi { get; set; }
        public Int64 NilaiKompetensi { get; set; }
        public string GlobalRating { get; set; }
    }
}
