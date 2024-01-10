using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Common.Interfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces;
//using OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.ViewModel;
using OSCEUKDI.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Services
{
    public interface ILogActivityService : IEntityService<LogActivity>
    {
        //VMListUser getListUser (DataTableAjaxPostModel model);
        //VMListUser getListUserPopup(DataTableAjaxPostModel model);
        //IEnumerable<VMRoleAccessMenu> getUserAccessMenu(int id);
        List<VMDropDown> GetAllUser();
    }

    public class LogActivityService : EntityService<LogActivity>, ILogActivityService
    {
        IUnitOfWork _unitOfWork;
        ILogActivityRepository _logActivityRepository;

        public LogActivityService(IUnitOfWork unitOfWork, ILogActivityRepository logActivityRepository)
            : base(unitOfWork, logActivityRepository)
        {
            _unitOfWork = unitOfWork;
            _logActivityRepository = logActivityRepository;
        }  

        public List<VMDropDown> GetAllUser()
        {
            throw new NotImplementedException();
        }
    }
}
