using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces
{
    
    public interface IKompetensiRepository : IGenericRepository<Kompetensi>
    {
        IEnumerable<GridKompetensi> getAllData();
        IEnumerable<VMDropDown> dropDownKompetensi();
    }
}
