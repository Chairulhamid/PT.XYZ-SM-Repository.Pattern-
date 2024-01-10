using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Common.Interfaces.RepoInterfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        List<VMLookup> getLookupRole();
        IEnumerable<GridRole> getRole();
        IEnumerable<VMRoleAccessMenu> getRoleAccessMenu(int id);
        List<VMDropDown> getDropDownRole();
    }
}
