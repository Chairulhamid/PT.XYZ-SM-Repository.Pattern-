using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Common.Interfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Services.OSCEUKDIServices
{
    public interface IRuangUjianService : IEntityService<RuangUjian>
    {
        IEnumerable<GridRuangUjian> getAllData();
        IEnumerable<VMDropDown> dropDownRuangUjian();
        IEnumerable<VMDropDownGetRuangUjian> getRuangUjianForMappingMahasiswa(int TahunSemester, string ACADCareer, int JadwaTest);

    }
   
        public class RuangUjianService : EntityService<RuangUjian>, IRuangUjianService
    {
        IUnitOfWork _unitOfWork;
        IRuangUjianRepository _RuangUjianRepository;

        public RuangUjianService(IUnitOfWork unitOfWork, IRuangUjianRepository RuangUjianRepository)
            : base(unitOfWork, RuangUjianRepository)
        {
            _unitOfWork = unitOfWork;
            _RuangUjianRepository = RuangUjianRepository;

        }
        public IEnumerable<GridRuangUjian> getAllData()
        {
            return _RuangUjianRepository.getAllData();
        }
        public IEnumerable<VMDropDown> dropDownRuangUjian()
        {
            return _RuangUjianRepository.dropDownRuangUjian();
        }
        public IEnumerable<VMDropDownGetRuangUjian> getRuangUjianForMappingMahasiswa(int TahunSemester, string ACADCareer, int JadwaTest)
        {
            return _RuangUjianRepository.getRuangUjianForMappingMahasiswa(TahunSemester, ACADCareer, JadwaTest);
        }
    }
}
