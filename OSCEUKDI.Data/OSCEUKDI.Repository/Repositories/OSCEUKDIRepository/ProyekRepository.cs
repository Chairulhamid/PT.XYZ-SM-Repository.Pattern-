
using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.Remoting.Contexts;

namespace OSCEUKDI.Repository.Repositories.OSCEUKDIRepository
{
    public class ProyekRepository : GenericRepository<Proyek>, IProyekRepository
    {
        public ProyekRepository(DbContext _db) : base(_db)
        {
        }
        //public IEnumerable<GridRuangUjian> getAllData()
        //{
        //    using (var context = new OSCEUKDIContext())
        //    {
        //        var result = context.RuangUjians.Where(x => x.IsDeleted == false && x.IsActive == true);
        //        var listmodel = result.Select(
        //            x => new GridRuangUjian
        //            {
        //                ID = x.ID,
        //                NamaRuangan = x.NamaRuangan,
        //            });
        //        return listmodel.ToList();
        //    }
        //}
        //public IEnumerable<VMDropDown> dropDownRuangUjian()
        //{
        //    using (var context = new OSCEUKDIContext())
        //    {
        //        var result = context.RuangUjians.Where(x => x.IsDeleted == false && x.IsActive == true);
        //        var listmodel = result.Select(
        //            x => new VMDropDown
        //            {
        //                Nama = x.NamaRuangan,
        //                Nilai = x.ID.ToString()
        //            });
        //        return listmodel.ToList();
        //    }
        //}
        //public IEnumerable<VMDropDownGetRuangUjian> getRuangUjianForMappingMahasiswa(int TahunSemester, string ACADCareer, int JadwaTest)
        //{
        //    using (var context = new OSCEUKDIContext())
        //    {
        //        var Acad = ACADCareer;
        //        //var dsfdsewf = "SELECT B.ID Nilai, B.NamaRuangan Nama FROM MappingPertanyaan A JOIN RuangUjian B ON B.ID = A.RuangUjianID WHERE A.TahunSemester = " + TahunSemester + " AND A.ACADCareer = '" + Acad + "' AND A.JadwalTestID = " + JadwaTest + " AND B.IsActive = 1 AND B.IsDeleted = 0";
        //        var getData = context.Database.SqlQuery<VMDropDownGetRuangUjian>("SELECT B.ID Nilai, B.NamaRuangan Nama FROM MappingPertanyaan A JOIN RuangUjian B ON B.ID = A.RuangUjianID WHERE A.TahunSemester = '"+TahunSemester+"' AND A.ACADCareer = '"+Acad+"' AND A.JadwalTestID = '"+JadwaTest+"' AND B.IsActive = 1 AND B.IsDeleted = 0").ToList();

        //        return getData;
        //    }
        //}
    }
}
