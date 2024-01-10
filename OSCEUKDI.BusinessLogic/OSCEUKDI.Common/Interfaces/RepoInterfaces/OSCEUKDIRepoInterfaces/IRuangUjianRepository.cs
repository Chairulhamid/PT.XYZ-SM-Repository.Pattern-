using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces
{
    
    public interface IRuangUjianRepository : IGenericRepository<RuangUjian>
    {
        IEnumerable<GridRuangUjian> getAllData();
        IEnumerable<VMDropDown> dropDownRuangUjian();
        IEnumerable<VMDropDownGetRuangUjian> getRuangUjianForMappingMahasiswa(int TahunSemester, string ACADCareer, int JadwaTest);
    }
}
