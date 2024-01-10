using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Common.Interfaces.RepoInterfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<GridData> getAllData();
        IEnumerable<GridData> getAllVendor(int RoleId);
        VMListUser getAllUser(int Skip, int Length, string SearchParam, string SortBy, bool SortDir);
        IEnumerable<VMRoleAccessMenu> getUserAccessMenu(int id);

        List<VMDropDown> GetAllUser();

    }
}
