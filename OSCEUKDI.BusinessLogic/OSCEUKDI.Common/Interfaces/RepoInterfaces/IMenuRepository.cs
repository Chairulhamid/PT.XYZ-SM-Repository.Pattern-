using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Common.Interfaces.RepoInterfaces
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        IEnumerable<GridMenu> getAllData();
        List<VMMenu> getListMenu();
        List<VMMenu> getListMenuParent();
        VMListMenu getMenu(int skip, int take, string searchBy, string sortBy, bool sortDir);
    }
}
