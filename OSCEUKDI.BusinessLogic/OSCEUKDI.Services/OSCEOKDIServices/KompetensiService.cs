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
    public interface IKompetensiService : IEntityService<Kompetensi>
    {
        IEnumerable<GridKompetensi> getAllData();
        IEnumerable<VMDropDown> dropDownKompetensi();

    }
   
        public class KompetensiService : EntityService<Kompetensi>, IKompetensiService
    {
        IUnitOfWork _unitOfWork;
        IKompetensiRepository _KompetensiRepository;

        public KompetensiService(IUnitOfWork unitOfWork, IKompetensiRepository KompetensiRepository)
            : base(unitOfWork, KompetensiRepository)
        {
            _unitOfWork = unitOfWork;
            _KompetensiRepository = KompetensiRepository;

        }
        public IEnumerable<GridKompetensi> getAllData()
        {
            return _KompetensiRepository.getAllData();
        }
        public IEnumerable<VMDropDown> dropDownKompetensi()
        {
            return _KompetensiRepository.dropDownKompetensi();
        }
    }
}
