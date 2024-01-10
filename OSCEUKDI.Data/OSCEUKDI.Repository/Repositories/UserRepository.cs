using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Data.SqlClient;

namespace OSCEUKDI.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext _db) : base(_db)
        {
        }
        public IEnumerable<GridData> getAllData()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Users.Where(x => x.IsDeleted == false && x.IsActive == true && x.RoleID !=19);
                var listmodel = result.Select(
                    z => new GridData
                    {
                        ID = z.ID,
                        Email = z.Email,
                        Nama = z.UserName,
                        Password = z.Password,
                        UserName = z.UserName,

                        RoleID = z.RoleID,
                        RoleName = z.Roles.RoleName,
                       // NoPegawai = z.NoPegawai,
                        Status = z.IsActive
                    });
                return listmodel.ToList();
            }
        }

        public VMListUser getAllUser(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListUser mListUser = new VMListUser();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new OSCEUKDIContext())
            {

                var result = context.Users.Where(x => x.IsDeleted == false);
                mListUser.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.UserName.Contains(SearchParam) ||
                y.Password.Contains(SearchParam) || y.Email.Contains(SearchParam) || y.Roles.RoleName.Contains(SearchParam))
                    .Select(z => new GridData
                    {
                        ID = z.ID,
                        Email = z.Email,
                        Nama = z.UserName,
                        Password = z.Password,
                        UserName = z.UserName,
                        //KodeProdi = Convert.ToDouble(z.KodeProdi),

                        RoleID = z.RoleID,
                        RoleName = z.Roles.RoleName,
                        //NoPegawai = z.NoPegawai,
                        Status = z.IsActive
                    }).OrderBy(SortBy, SortDir);
                mListUser.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListUser.TotalFilterCount = gridfilter.Count();
                return mListUser;
            }
        }
        public IEnumerable<VMRoleAccessMenu> getUserAccessMenu(int id)
        {
            using (var context = new OSCEUKDIContext())
            {
                var paramId = new SqlParameter("@userID", id);
                var result = context.Database
                    .SqlQuery<VMRoleAccessMenu>("GetUserAccessMenu @userID", paramId).ToList();

                return result;
            }
        }

        public List<VMDropDown> GetAllUser()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Database.SqlQuery<VMDropDown>("SELECT CAST(NIP AS VARCHAR(50)) Nilai, Nama FROM SDM_KARYAWAN_TB").ToList();
                var listmodel = result.Select(
                    x => new VMDropDown
                    {
                        Nama = x.Nama,
                        Nilai = x.Nilai.ToString()
                    });
                return listmodel.ToList();
            }
        }
        public IEnumerable<GridData> getAllVendor(int RoleId)
        {
            using (var context = new OSCEUKDIContext())
            {
                IQueryable<User> result = null;
                if (RoleId == 2)
                {
                     result = context.Users.Where(x => x.IsDeleted == false && x.IsActive == true && x.RoleID == 19 && x.StatusApproval != "Approval");
                }
                else if (RoleId == 18) { 
                     result = context.Users.Where(x => x.IsDeleted == false && x.IsActive == true && x.RoleID == 19 && x.StatusApproval != "Pending");
                }
                var listmodel = result.Select(
                    z => new GridData
                    {
                        ID = z.ID,
                        Email = z.Email,
                        Nama = z.UserName,
                        Password = z.Password,
                        UserName = z.UserName,
                        RoleID = z.RoleID,
                        RoleName = z.Roles.RoleName,
                        Phone = z.Phone,
                        BidangUsaha = z.BidangUsaha,
                        JenisPerusahaan = z.JenisPerusahaan,
                        Photo = z.Photo,
                        StatusApproval = z.StatusApproval,
                        Status = z.IsActive
                    });
                return listmodel.ToList();
            }
        }

    }
}
