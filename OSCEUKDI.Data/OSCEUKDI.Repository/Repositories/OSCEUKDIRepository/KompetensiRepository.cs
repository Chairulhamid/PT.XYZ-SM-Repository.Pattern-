
using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.Remoting.Contexts;

namespace OSCEUKDI.Repository.Repositories.OSCEUKDIRepository
{
    public class KompetensiRepository : GenericRepository<Kompetensi>, IKompetensiRepository
    {
        public KompetensiRepository(DbContext _db) : base(_db)
        {
        }
        public IEnumerable<GridKompetensi> getAllData()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Kompetensis.Where(x => x.IsDeleted == false && x.IsActive == true);
                var listmodel = result.Select(
                    x => new GridKompetensi
                    {
                        ID = x.ID,
                        NamaKompetensi = x.NamaKompetensi,
                        KategoriOsce = x.KategoriOsce,
                    });
                return listmodel.ToList();
            }
        }
        public IEnumerable<VMDropDown> dropDownKompetensi()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Kompetensis.Where(x => x.IsDeleted == false && x.IsActive == true);
                var listmodel = result.Select(
                    x => new VMDropDown
                    {
                        Nama = x.NamaKompetensi,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }
    }
}
