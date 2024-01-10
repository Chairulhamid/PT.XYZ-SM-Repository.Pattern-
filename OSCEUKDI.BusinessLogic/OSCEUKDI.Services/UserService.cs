using OSCEUKDI.Common.Helpers;
using OSCEUKDI.Common.Interfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces;
using OSCEUKDI.Common.Interfaces.RepoInterfaces.OSCEUKDIRepoInterfaces;
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
    public interface IUserService : IEntityService<User>
    {
        IEnumerable<GridData> getAllData();
        IEnumerable<GridData> getAllVendor(int RoleId);
        VMListUser getListUser (DataTableAjaxPostModel model);
        IEnumerable<VMRoleAccessMenu> getUserAccessMenu(int id);
        List<VMDropDown> GetAllUser();
    }

    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public List<VMDropDown> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }
        public IEnumerable<GridData> getAllData()
        {
            return _userRepository.getAllData();
        }
        public VMListUser getListUser(DataTableAjaxPostModel model)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _userRepository.getAllUser(skip, take, searchBy, sortBy, sortDir);
        }
        public IEnumerable<VMRoleAccessMenu> getUserAccessMenu(int id)
        {
            return _userRepository.getUserAccessMenu(id);
        }
        public IEnumerable<GridData> getAllVendor(int RoleId)
        {
            return _userRepository.getAllVendor(RoleId);
        }
    }
}
