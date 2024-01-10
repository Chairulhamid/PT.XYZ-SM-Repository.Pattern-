using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;

namespace OSCEUKDI.Repository.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext _db) : base(_db)
        {
        }

        public List<VMLookup> getLookupRole()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Roles.Where(x => x.IsDeleted == false && x.IsActive == true);
                var listmodel = result.Select(
                    x => new VMLookup
                    {
                        Nama = x.RoleName,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }
        public IEnumerable<GridRole> getRole()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Roles.Where(x => x.IsDeleted == false && x.IsActive == true);
                var listmodel = result.Select(
                    z => new GridRole
                    {
                        ID = z.ID,
                        Code = z.Code,
                        Status = z.IsActive,
                        RoleName = z.RoleName
                    });
                return listmodel.ToList();
            }
        }
        public IEnumerable<VMRoleAccessMenu> getRoleAccessMenu(int id)
        {
            using (var context = new OSCEUKDIContext())
            {
                var  paramId = new SqlParameter("@roleID", id);
                var result = context.Database
                    .SqlQuery<VMRoleAccessMenu>("GetRoleAccessMenu @roleID", paramId).ToList();

                return result;
            }
        }
        
        public List<VMDropDown> getDropDownRole()
        {
            using (var context = new OSCEUKDIContext())
            {
                var result = context.Roles.Where(x => x.IsDeleted == false && x.IsActive == true );
                var listmodel = result.Select(
                    x => new VMDropDown
                    {
                        Nama = x.RoleName,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }
    }
}
