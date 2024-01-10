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
    public interface IProyekService : IEntityService<Proyek>
    {
        //IEnumerable<GridRuangUjian> getAllData(string email);

    }
   
        public class ProyekService : EntityService<Proyek>, IProyekService
    {
        IUnitOfWork _unitOfWork;
        IProyekRepository _ProyekRepository;

        public ProyekService(IUnitOfWork unitOfWork, IProyekRepository ProyekRepository)
            : base(unitOfWork, ProyekRepository)
        {
            _unitOfWork = unitOfWork;
            _ProyekRepository = ProyekRepository;

        }
        //public IEnumerable<GridRuangUjian> getAllData(string email)
        //{
        //    return _ProyekRepository.getAllData(email);
        //} 
    }
}
